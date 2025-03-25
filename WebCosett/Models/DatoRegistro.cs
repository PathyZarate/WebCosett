using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebCosett.Models
{
    public class DatoRegistro
    {
        [Key]
        public int Id { get; set; }
        public int RegistroLineaId { get; set; }
        [ForeignKey(nameof(RegistroLineaId))]
        public RegistroLinea? RegistroLinea { get; set; }
        public string Valor1 { get; set; }
        public string Valor2 { get; set; }
        public string Valor3 { get; set; }
        public string Valor4 { get; set; }
        public string Valor5 { get; set; }
        public string Valor6 { get; set; }
        public string Valor7 { get; set; }
        public string Valor8 { get; set; }
        public string Valor9 { get; set; }
        public string Valor10 { get; set; }
        public string Valor11 { get; set; }
        public string Valor12 { get; set; }
        public string Valor13 { get; set; }
    }
}