using System.ComponentModel.DataAnnotations;

namespace WebCosett.Data
{
    public class Archivo
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaSubida { get; set; } = DateTime.UtcNow;
    }

}
