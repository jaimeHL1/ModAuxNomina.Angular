using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class SalidaCalculoAnticipo
{
    [Column(TypeName = "decimal(10, 2)")]
    public decimal Sueldo { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Trienios { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal PExtra { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal ImpPaga { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal IRPF { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal ImpLiquido { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal ImpTotal { get; set; }

    public int? idError { get; set; }
    public string? mensajeError { get; set; }

}
