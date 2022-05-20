using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ModularMonolithSpike.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {


        [HttpGet]
        public async Task<ActionResult> monolithEndpoint()
        {
            try
            {
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
            catch (Exception ex)
            {
                var errorobj = new { Error = ex.GetType().Name, ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, errorobj);
            }
        }

    }
}
