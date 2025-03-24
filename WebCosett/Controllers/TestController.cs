using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;

namespace WebCosett.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpPost("procesar-descargarexcel")]
        public async Task<IActionResult> ProcesarArchivo(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            {
                return BadRequest("No se ha proporcionado un archivo.");
            }

            // Guardar el archivo subido en una ubicación temporal
            var filePath = Path.GetTempFileName();

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            var resultado = new List<string[]>();

            // Procesar el archivo
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(stream))
                {
                    string linea;
                    while ((linea = await reader.ReadLineAsync()) != null)
                    {
                        int offset = 0; // Para mantener el control de qué parte de la línea estamos leyendo
                        var registroLinea = new RegistroLinea();
                        bool primeraVuelta = true;
                        var fila = new List<string>();

                        while (offset < linea.Length)
                        {
                            if (primeraVuelta)
                            {
                                // Extraer los metadatos
                                registroLinea.Fecha = linea.Substring(offset + 0, 8);  // 5-12
                                registroLinea.Hora = linea.Substring(offset + 8, 5); // 13-17
                                registroLinea.Calidad = linea.Substring(offset + 13, 3); // 18-20
                                registroLinea.LongitudDatos = linea.Substring(offset + 16, 3);// Los registros siempre tienen 95 caracteres (se puede ajustar si es variable)

                                // Agregar metadatos a la fila
                                fila.Add(registroLinea.Fecha);
                                fila.Add(registroLinea.Hora);
                                fila.Add(registroLinea.Calidad);
                                fila.Add(registroLinea.LongitudDatos);

                                // Establecer que ya no estamos en la primera vuelta
                                primeraVuelta = false;

                                // Avanzamos el offset después de los metadatos (24 primeros caracteres)
                                offset += 19;
                            }
                            else
                            {
                                // Procesar el resto de los datos como registros
                                var parte5 = linea.Substring(offset, 1);  // 24
                                var parte6 = linea.Substring(offset + 1, 6); // 25-30
                                var parte7 = linea.Substring(offset + 7, 1); // 31
                                var parte8 = linea.Substring(offset + 8, 1); // 32
                                var parte9 = linea.Substring(offset + 9, 7); // 33-39
                                var parte10 = linea.Substring(offset + 16, 7); // 40-46
                                var parte11 = linea.Substring(offset + 23, 7); // 47-53
                                var parte12 = linea.Substring(offset + 30, 7); // 54-60
                                var parte13 = linea.Substring(offset + 37, 7); // 61-67
                                var parte14 = linea.Substring(offset + 44, 7); // 68-74
                                var parte15 = linea.Substring(offset + 51, 7); // 75-81
                                var parte16 = linea.Substring(offset + 58, 7); // 82-88
                                var parte17 = linea.Substring(offset + 65, 7); // 89-95

                                // Agregar los registros a la fila
                                fila.Add(parte5);
                                fila.Add(parte6);
                                fila.Add(parte7);
                                fila.Add(parte8);
                                fila.Add(parte9);
                                fila.Add(parte10);
                                fila.Add(parte11);
                                fila.Add(parte12);
                                fila.Add(parte13);
                                fila.Add(parte14);
                                fila.Add(parte15);
                                fila.Add(parte16);
                                fila.Add(parte17);

                                // Avanzamos el offset en 72 caracteres
                                offset += 72;
                            }
                        }

                        // Agregar la fila al resultado
                        resultado.Add(fila.ToArray());
                    }
                }
            }

            // Crear el archivo Excel
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Datos");
                for (int i = 0; i < resultado.Count; i++)
                {
                    for (int j = 0; j < resultado[i].Length; j++)
                    {
                        worksheet.Cell(i + 1, j + 1).Value = resultado[i][j];
                    }
                }

                // Guardar el archivo Excel en una ubicación temporal
                var excelFilePath = Path.Combine(Path.GetTempPath(), "Resultado.xlsx");
                workbook.SaveAs(excelFilePath);

                // Devolver el archivo Excel como respuesta
                var memory = new MemoryStream();
                using (var stream = new FileStream(excelFilePath, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Resultado.xlsx");
            }
        }
    }

    public class RegistroLinea
    {
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Calidad { get; set; }
        public string LongitudDatos { get; set; }
        public List<string> Registros { get; set; } = new List<string>();
    }
}
