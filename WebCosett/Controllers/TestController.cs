using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebCosett.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpPost("procesar-archivo")]
        public async Task<IActionResult> ProcesarArchivo(IFormFile archivo)
        {
           return Ok("HOLA");
        }
    }
}
