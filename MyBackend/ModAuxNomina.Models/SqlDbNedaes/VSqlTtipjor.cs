using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class VSqlTtipjor
{
    [Key]
    public string CDTIPJOR { get; set; }
    public decimal NMMULTI { get; set; }
    public decimal NMDIVI { get; set; }
    public string CDHABIL { get; set; }
}
