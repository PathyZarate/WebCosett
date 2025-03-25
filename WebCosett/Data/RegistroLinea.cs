using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCosett.Data
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
        public List<DatoRegistro> Registros { get; set; }
    }


}
