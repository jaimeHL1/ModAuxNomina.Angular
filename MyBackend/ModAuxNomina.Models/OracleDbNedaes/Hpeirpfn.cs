public class Hpeirpfn
{
    public string CDHABIL { get; set; }
    public string CDCLASNM { get; set; }
    public string CDDNI { get; set; }
    public string CDDUP { get; set; }
    public string CDNUMNOM { get; set; }
    public int FENOMDES { get; set; }
    public decimal? NMPORRET { get; set; }

    // Propiedad para relación inversa hacia Hperret
    public Hperret Hperret { get; set; }
}
