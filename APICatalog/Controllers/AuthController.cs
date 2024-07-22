using Infrastructure.Domain;
using Application.DTOs;
using APICatalog.Filters;
using Application.Interfaces;
using Application.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace APICatalog.Controllers;

[ServiceFilter(typeof(ApiLoggingFilter))]
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IUserClaimService _claimService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<AuthController> _logger;
    private readonly IConfiguration _configuration;

    public AuthController(ITokenService tokenService,
                          IUserClaimService claimService,
                          UserManager<ApplicationUser> userManager,
                          ILogger<AuthController> logger,
                          RoleManager<IdentityRole> roleManager,
                          IConfiguration configuration)
    {
        _tokenService = tokenService;
        _claimService = claimService;
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
        _configuration = configuration;
    }

    /// <summary>
    /// Check a user's credentials
    /// </summary>
    /// <param name="modelDTO">Login model (DTO)</param>
    /// <returns></returns>
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModelDTO modelDTO) 
    {
        var user = await _userManager.FindByNameAsync(modelDTO.UserName!);

        if(user is not null && await _userManager.CheckPasswordAsync(user!, modelDTO.Password!)) 
        {
            return await LoginResponse(user!);
        }

        return Unauthorized();
    }

    [HttpGet]
    private async Task<IActionResult> LoginResponse(ApplicationUser user) 
    {
        var authClaims = _claimService.CreateAuthClaims(user);

        IList<string> roles = await _userManager.GetRolesAsync(user);
        _claimService.AddUserRolesToClaims(roles, authClaims);

        var token = _tokenService.GenerateAccessToken(authClaims, _configuration);

        var refreshToken = await _tokenService.SaveRefreshTokenAsync(user, _configuration);

        return Ok(new
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = refreshToken,
            Expiration = token.ValidTo
        });
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModelDTO registerModelDTO)
    {
        var userExists = await _userManager.FindByNameAsync(registerModelDTO.UserName!);

        if (userExists == null)
        {
            var user = registerModelDTO.MapToApplicatioUser();
            IdentityResult result = await _userManager.CreateAsync(user, registerModelDTO.Password!);

            if (result.Succeeded)
            {

                return CreatedAtAction("Login", new { Username = user.UserName }, new ResponseDTO
                {
                    Status = "Success 201",
                    Message = "User created successfully"
                });
            }

            var errors = string.Join(", ", result.Errors.Select(error => error.Description));
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO
            {
                Status = "Error 500",
                Message = $"User creation failed because: {errors}"
            });
        }

        return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO
        {
            Status = "Error 500",
            Message = "User already exists"
        });
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenModelDTO tokenModelDTO)
    {
        if (tokenModelDTO == null)
            return BadRequest("Invalid client request");

        string? accessToken = tokenModelDTO.AccessToken ??
                              throw new ArgumentNullException(nameof(tokenModelDTO));

        string? refreshToken = tokenModelDTO.RefreshToken ??
                               throw new ArgumentNullException(nameof(tokenModelDTO));

        ClaimsPrincipal principal = _tokenService.GetPrincipalFromExpiredToken(accessToken, _configuration);

        var user = await _userManager.FindByNameAsync(principal.Identity!.Name!);

        if (user is null || user.RefreshToken != refreshToken
                        || user.RefreshTokenExpiryTime < DateTime.UtcNow)
        {
            return BadRequest("Invalid token/refresh token");
        }

        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList(), _configuration);

        var newRefreshToken = await _tokenService.SaveRefreshTokenAsync(user, _configuration);

        return new ObjectResult(new
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            RefreshToken = newRefreshToken,
        });
    }

    [Authorize(policy: "AdminOnly")]
    [HttpPost]
    [Route("revoke/{username}")]
    public async Task<IActionResult> Revoke(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null)
            return BadRequest("Invalid user name");

        user.RefreshToken = null;

        await _userManager.UpdateAsync(user);

        return NoContent();
    }

    [HttpPost]
    [Route("createRole")]
    public async Task<IActionResult> CreateRole([FromQuery] string roleName)
    {
        var roleExist = await _roleManager.RoleExistsAsync(roleName);

        if (!roleExist)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (result.Succeeded)
            {
                _logger.LogInformation(1, $"Role {roleName} added successfully");
                return StatusCode(StatusCodes.Status200OK, new ResponseDTO
                {
                    Status = "Successfully: 200",
                    Message = "Role added successfully"
                });
            }

            var errors = string.Join(", ", result.Errors.Select(error => error.Description));
            _logger.LogInformation(2, $"An error ocurred in creation role becuase this errors: {errors}");
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO
            {
                Status = "Error: 400",
                Message = $"Error in creation role with name = {roleName}",
            });
        }
        return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO
        {
            Status = "Error: 400",
            Message = $"Role {roleName} already exist."
        });
    }

    [HttpPost]
    [Route("addUserToRole")]
    [Authorize("AdminOnly")]
    public async Task<IActionResult> AddUserToRole([FromQuery] string email, [FromQuery] string roleName)
    {
        if (email == null || roleName == null)
        {
            return BadRequest("Any property is null");
        }

        var user = await _userManager.FindByEmailAsync(email);

        if (user != null)
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                _logger.LogInformation(1, $"User {user.Email} added to the {roleName} role");

                return StatusCode(StatusCodes.Status200OK, new ResponseDTO
                {
                    Status = "Successfully: 200",
                    Message = $"User {user.Email} added to the {roleName} role"
                });
            }

            var errors = result.Errors.Select(error => error.Description);
            _logger.LogInformation(2, $"An error ocurred in add user {user.Email} in to the {roleName} role, because this errors: {errors}");

            return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO
            {
                Status = "Error: 400",
                Message = $"An error ocurred in add user {user.Email} in to the {roleName} role"
            });
        }

        return BadRequest(new 
        {
            error = $"Unable to find user {email}"
        });
    
    }
}
