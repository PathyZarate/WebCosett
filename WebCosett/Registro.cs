namespace WebCosett
{
    public class RegistroLinea
    {
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Calidad { get; set; }
        public string LongitudDatos { get; set; }
        public List<string> Registros { get; set; }

        public RegistroLinea()
        {
            Registros = new List<string>();
        }
    }
}
