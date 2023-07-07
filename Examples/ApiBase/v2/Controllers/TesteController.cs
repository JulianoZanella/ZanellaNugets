using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ApiBase.v2.Controllers
{
    [ApiVersion("2")]
    public class TesteController : BaseApiController
    {
        /// <summary>
        /// Testando
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("ok");
        }
    }
}
