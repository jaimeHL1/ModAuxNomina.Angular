public class Hperret
{
    public string CDHABIL { get; set; }
    public string CDCLASNM { get; set; }
    public string CDDNI { get; set; }
    public string CDDUP { get; set; }
    public string CDNUMNOM { get; set; }
    public int FENOMDES { get; set; }
    public int FENOMHAS { get; set; }
    public string CDTIPJOR { get; set; }
    public string KKGRUPO { get; set; }

    // Propiedad para la relación con Hpeirpfn
    public Hpeirpfn Hpeirpfn { get; set; }

    public Ttipjor Ttipjor { get; set; }

    public  Tcrcuant  Tcrcuant { get; set; }  
    public Tcrcuantpaex Tcrcuantpaex { get; set; } 
}
