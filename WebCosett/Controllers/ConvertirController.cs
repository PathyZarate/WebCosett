using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace WebCosett.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ConvertirController : ControllerBase
    {
        [HttpPost("convertir-y-descargar-txt")]
        public async Task<IActionResult> ConvertirYDescargarTxt(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            {
                return BadRequest("No se ha proporcionado ningún archivo.");
            }

            // Directorio temporal para almacenar el archivo TXT
            var directorioDestino = Path.GetTempPath();
            var nombreOriginal = Path.GetFileNameWithoutExtension(archivo.FileName);

            // Extraer la parte después del punto
            var extension = Path.GetExtension(archivo.FileName).Replace(".", "");

            // Unir la parte extraída al nombre del archivo
            var nombreConvertido = nombreOriginal + extension + ".txt";
            var filePath = Path.Combine(directorioDestino, nombreConvertido);

            // Leer el contenido del archivo y eliminar la cabecera si está presente
            using (var stream = new MemoryStream())
            {
                await archivo.CopyToAsync(stream);
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    var contenido = await reader.ReadToEndAsync();
                    var lineas = contenido.Split('\n');
                    if (lineas.Length > 0 && lineas[0].StartsWith("1562"))
                    {
                        contenido = string.Join("\n", lineas.Skip(1));
                    }

                    // Guardar el contenido sin la cabecera en el archivo TXT
                    await System.IO.File.WriteAllTextAsync(filePath, contenido);
                }
            }

            // Preparar el archivo para descargar
            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory.ToArray(), "text/plain", nombreConvertido);
        }
    }
}
