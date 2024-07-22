using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace APICatalog.Controllers;

[ApiController]
[Route("api/v{version:ApiVersion}/test")]
[ApiVersion("1.0")]
public class TestV1Controller : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Get() 
    {
        return "Test - Get - V1";
    }
}
