public class Tcrcuantpaex
{
    public string CDHABIL { get; set; }
    public string CDCLASNM { get; set; }
    public string CDCLAPEX { get; set; }
    public string CDNUPAEX { get; set; }
    public string CDCONCRT { get; set; }
    public string CDIMPORT { get; set; }
    public DateTime FEVIDES { get; set; }
    public DateTime? FEVIHAS { get; set; }
    public decimal? PTIMPORTP { get; set; }

    // Relación con Hperret
    public Hperret Hperret { get; set; }
}
