using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCosett.Data;

namespace WebCosett.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MostrarController : ControllerBase
    {
        [HttpPost("procesar-archivo")]
        public async Task<IActionResult> ProcesarArchivo(IFormFile archivo, [FromServices] AppDbContext context)
        {
            if (archivo == null || archivo.Length == 0)
            {
                return BadRequest("No se ha proporcionado un archivo válido.");
            }

            var nuevoArchivo = new Archivo { Nombre = archivo.FileName };
            context.Archivo.Add(nuevoArchivo);
            await context.SaveChangesAsync();

            using (var stream = archivo.OpenReadStream())
            using (var reader = new StreamReader(stream))
            {
                string linea;
                while ((linea = await reader.ReadLineAsync()) != null)
                {
                    int offset = 0;
                    var registroLinea = new RegistroLinea
                    {
                        ArchivoId = nuevoArchivo.Id,
                        Fecha = linea.Substring(offset, 8),
                        Hora = linea.Substring(offset + 8, 5),
                        Calidad = linea.Substring(offset + 13, 3),
                        LongitudDatos = linea.Substring(offset + 16, 3),
                        Registros = new List<DatoRegistro>()
                    };

                    offset += 19;
                    while (offset < linea.Length)
                    {
                        var dato = new DatoRegistro
                        {
                            Valor1 = linea.Substring(offset, 1),
                            Valor2 = linea.Substring(offset + 1, 6),
                            Valor3 = linea.Substring(offset + 7, 1),
                            Valor4 = linea.Substring(offset + 8, 1),
                            Valor5 = linea.Substring(offset + 9, 7),
                            Valor6 = linea.Substring(offset + 16, 7),
                            Valor7 = linea.Substring(offset + 23, 7),
                            Valor8 = linea.Substring(offset + 30, 7),
                            Valor9 = linea.Substring(offset + 37, 7),
                            Valor10 = linea.Substring(offset + 44, 7),
                            Valor11 = linea.Substring(offset + 51, 7),
                            Valor12 = linea.Substring(offset + 58, 7),
                            Valor13 = linea.Substring(offset + 65, 7)
                        };

                        registroLinea.Registros.Add(dato);
                        offset += 72;
                    }

                    context.RegistroLinea.Add(registroLinea);
                }
            }

            await context.SaveChangesAsync();
            return Ok("Archivo procesado y guardado correctamente.");
        }
    }
}
