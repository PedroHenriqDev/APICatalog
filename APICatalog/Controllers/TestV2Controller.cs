using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace APICatalog.Controllers;

[ApiController]
[Route("api/v{version:ApiVersion}/test")]
[ApiVersion("2.0")]
public class TestV2Controller : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Get() 
    {
        return "Test - Get - v2";
    }
}
