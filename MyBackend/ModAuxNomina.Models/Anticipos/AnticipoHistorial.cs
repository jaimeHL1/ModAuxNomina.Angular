namespace MyBackend.ModAuxNomina.Models.Anticipos
{
    public class AnticipoHistorial
    {
        public int idAnticipo { get; set; }
        public int idAmortizacion { get; set; }
        public int? anno { get; set; }
        public int? mes { get; set; }
        public DateTime? fechaD { get; set; }
        public string? fecha { get; set; }
        public decimal? importe { get; set; }
        public string? observaciones { get; set; }
        public int? tipoAmortizacion { get; set; }
        public string? desTipoAmortizacion { get; set; }
    }
}