using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebCosett.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubirController : ControllerBase
    {
        [HttpPost("procesar-archivo")]
        public async Task<IActionResult> ProcesarArchivo(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            {
                return BadRequest("No se ha proporcionado un archivo válido.");
            }

            using (var stream = archivo.OpenReadStream())
            using (var reader = new StreamReader(stream))
            {
                string linea;
                while ((linea = await reader.ReadLineAsync()) != null)
                {
                    int offset = 0;
                    var registroLinea = new RegistroLinea();
                    bool primeraVuelta = true;

                    while (offset < linea.Length)
                    {
                        if (primeraVuelta)
                        {
                            registroLinea.Fecha = linea.Substring(offset, 8);
                            registroLinea.Hora = linea.Substring(offset + 8, 5);
                            registroLinea.Calidad = linea.Substring(offset + 13, 3);
                            registroLinea.LongitudDatos = linea.Substring(offset + 16, 3);

                            Console.WriteLine($"Metadatos - Fecha: {registroLinea.Fecha}, Hora: {registroLinea.Hora}, Calidad: {registroLinea.Calidad}, Longitud: {registroLinea.LongitudDatos}");

                            primeraVuelta = false;
                            offset += 19;
                        }
                        else
                        {
                            Console.WriteLine(linea.Substring(offset, 72));
                            registroLinea.Registros = new List<string>
                            {
                                linea.Substring(offset, 1),
                                linea.Substring(offset + 1, 6),
                                linea.Substring(offset + 7, 1),
                                linea.Substring(offset + 8, 1),
                                linea.Substring(offset + 9, 7),
                                linea.Substring(offset + 16, 7),
                                linea.Substring(offset + 23, 7),
                                linea.Substring(offset + 30, 7),
                                linea.Substring(offset + 37, 7),
                                linea.Substring(offset + 44, 7),
                                linea.Substring(offset + 51, 7),
                                linea.Substring(offset + 58, 7),
                                linea.Substring(offset + 65, 7)
                            };

                            Console.WriteLine($"Registros: {string.Join(", ", registroLinea.Registros)}");
                            offset += 72;
                        }
                    }
                }
            }

            return Ok("Archivo procesado correctamente.");
        }
    }
}
