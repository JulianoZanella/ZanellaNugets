using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ApiBase.v1.Controllers
{
    [ApiVersion("1", Deprecated = true)]
    public class TesteController : BaseApiController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("ok");
        }
    }
}
