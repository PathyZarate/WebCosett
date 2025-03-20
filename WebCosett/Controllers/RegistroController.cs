using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace WebCosett.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegistroController : ControllerBase
{
    [HttpPost("procesar-archivo")]
    public async Task<IActionResult> ProcesarArchivo() //IFormFile archivo
    {
        string filePath = @"D:\PracticaCoset\TGRP ENERO\ENERO 2024\TXT\TGRP010124.txt";  // Ruta del archivo a procesar

        if (!System.IO.File.Exists(filePath))
        {
            return BadRequest("El archivo no existe en la ruta especificada.");
        }

        //if (archivo == null || archivo.Length == 0)
        //{
        //    return BadRequest("No se ha proporcionado un archivo.");
        //}

        using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            //await archivo.CopyToAsync(stream);
           // stream.Seek(0, SeekOrigin.Begin);
            // Usamos un MemoryStream para almacenar el contenido del archivo
            using (var reader = new StreamReader(stream))
            {
                string linea;
                while ((linea = await reader.ReadLineAsync()) != null)
                {
                    int offset = 0; // Para mantener el control de qué parte de la línea estamos leyendo
                    var registroLinea = new RegistroLinea();
                    bool primeraVuelta = true;

                    while (offset < linea.Length)
                    {
                        if (primeraVuelta)
                        {   
                            // Extraer los metadatos
                            registroLinea.Fecha = linea.Substring(offset + 0, 8);  // 5-12
                            registroLinea.Hora = linea.Substring(offset + 8, 5); // 13-17
                            registroLinea.Calidad = linea.Substring(offset + 13, 3); // 18-20
                            registroLinea.LongitudDatos = linea.Substring(offset + 16, 3);// Los registros siempre tienen 95 caracteres (se puede ajustar si es variable)

                            // Muestra los metadatos
                            Console.WriteLine($"Metadatos - Fecha: {registroLinea.Fecha}, Hora: {registroLinea.Hora}, Calidad: {registroLinea.Calidad}, Longitud: {registroLinea.LongitudDatos}");

                            // Establecer que ya no estamos en la primera vuelta
                            primeraVuelta = false;

                            // Avanzamos el offset después de los metadatos (24 primeros caracteres)
                            offset += 19;
                        }
                        else
                        {
                            Console.WriteLine(linea.Substring(offset, 72));
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

                            // Agregar los registros a la lista de registros
                            registroLinea.Registros.Add(parte5);
                            registroLinea.Registros.Add(parte6);
                            registroLinea.Registros.Add(parte7);
                            registroLinea.Registros.Add(parte8);
                            registroLinea.Registros.Add(parte9);
                            registroLinea.Registros.Add(parte10);
                            registroLinea.Registros.Add(parte11);
                            registroLinea.Registros.Add(parte12);
                            registroLinea.Registros.Add(parte13);
                            registroLinea.Registros.Add(parte14);
                            registroLinea.Registros.Add(parte15);
                            registroLinea.Registros.Add(parte16);
                            registroLinea.Registros.Add(parte17);

                            // Muestra los registros
                            Console.WriteLine($"Registros: {string.Join(", ", registroLinea.Registros)}");

                            // Avanzamos el offset en 95 caracteres
                            offset += 72;
                        }
                    }
                }
            }
        }

        return Ok("Archivo procesado correctamente.");
    }


}

