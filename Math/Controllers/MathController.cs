using Math.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Math.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class MathController : ControllerBase
    {
        public IMathService MathService { get; }

        public MathController(IMathService mathService)
        {
            MathService = mathService;
        }

        /// <summary>
        /// Adds two numbers
        /// </summary>
        /// <param name="a">first number</param>
        /// <param name="b">second number</param>
        /// <returns>the result</returns>
        [HttpGet]
        public async Task<ActionResult<int>> Addition(int a, int b)
        {
            int value = MathService.Addition(a, b);
            return Ok(value);
        }


        [HttpGet]
        public async Task<ActionResult<int>> Substraction(int a, int b)
        {
            int value = MathService.Substraction(a, b);
            return Ok(value);
        }

        [HttpGet]
        public async Task<ActionResult<int>> Multiplication(int a, int b)
        {
            int value = MathService.Multiplication(a, b);
            return Ok(value);
        }

        [HttpGet]
        public async Task<ActionResult<int>> Divison(int a, int b)
        {
            if (b == 0) throw new InvalidOperationException("cannot divide by 0");

            int value = MathService.Division(a, b);
            return Ok(value);
        }
    }
}
