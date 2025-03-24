using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebCosett.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcatenarController : ControllerBase
    {
        [HttpPost("procesar-archivos")]
        public async Task<IActionResult> ProcesarArchivos(List<IFormFile> archivos, [FromQuery] string nombreArchivo)
        {
            if (archivos == null || archivos.Count == 0)
            {
                return BadRequest("No se han proporcionado archivos válidos.");
            }

            if (string.IsNullOrWhiteSpace(nombreArchivo))
            {
                nombreArchivo = "concatenado.txt";
            }
            else
            {
                nombreArchivo = Path.ChangeExtension(nombreArchivo, ".txt");
            }

            try
            {
                using (var outputStream = new FileStream(nombreArchivo, FileMode.Append, FileAccess.Write))
                using (var writer = new StreamWriter(outputStream))
                {
                    foreach (var archivo in archivos)
                    {
                        using (var stream = archivo.OpenReadStream())
                        using (var reader = new StreamReader(stream))
                        {
                            while (!reader.EndOfStream)
                            {
                                string linea = await reader.ReadLineAsync();
                                await writer.WriteLineAsync(linea);
                            }
                        }
                    }
                }

                if (!System.IO.File.Exists(nombreArchivo))
                {
                    return NotFound("El archivo concatenado no existe.");
                }

                var bytes = System.IO.File.ReadAllBytes(nombreArchivo);
                return File(bytes, "text/plain", nombreArchivo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al procesar los archivos: {ex.Message}");
            }
        }
    }
}
