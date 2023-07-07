using ApiBase.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ApiBase
{
    [ApiController]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected ActionResult HandleResult<T>(ResultDTO<T> result)
        {
            if (result == null)
                return NotFound();

            if (result.IsSuccess)
            {
                if (result.Value != null)
                    return Ok(result.Value);

                return NotFound();
            }

            return BadRequest(result.Error);
        }

        public override BadRequestObjectResult BadRequest([ActionResultObjectValue] object? error)
        {
            if (error is string errString && !string.IsNullOrWhiteSpace(errString))
            {
                var errorObject = new ErrorDTO
                {
                    Message = errString,
                };
                return BadRequest(errorObject);
            }

            return base.BadRequest(error);
        }

        public override UnauthorizedObjectResult Unauthorized([ActionResultObjectValue] object? value)
        {
            if (value is string errString && !string.IsNullOrWhiteSpace(errString))
            {
                var errorObject = new ErrorDTO
                {
                    Message = errString,
                };
                return Unauthorized(errorObject);
            }

            return base.Unauthorized(value);
        }
    }
}
