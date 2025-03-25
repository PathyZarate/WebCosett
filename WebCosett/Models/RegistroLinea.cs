using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebCosett.Models
{
    public class RegistroLinea
    {
        [Key]
        public int Id { get; set; }
        public int ArchivoId { get; set; }
        [ForeignKey(nameof(ArchivoId))]
        public Archivo? Archivo { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Calidad { get; set; }
        public string LongitudDatos { get; set; }
    }
}