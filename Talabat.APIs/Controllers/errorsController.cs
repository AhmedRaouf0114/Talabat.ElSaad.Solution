using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Error;

namespace Talabat.APIs.Controllers
{
    [Route("errors/{Code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class errorsController : ControllerBase
    {
        public ActionResult Error (int Code)
        {
            if (Code == 401)
                return Unauthorized(new ApiResponse(401));
            else if (Code == 404)
                return NotFound(new ApiResponse(404));
            else
                return StatusCode(Code);
        }
    }
}
