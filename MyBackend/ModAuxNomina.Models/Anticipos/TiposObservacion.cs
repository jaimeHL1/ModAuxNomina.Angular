using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyBackend.ModAuxNomina.Models.Anticipos
{
    public class TiposObservacion
    {
        [Key]
        [Column("I_ID_Tipo_Observacion")]
        public int idTipoObservacion { get; set; }

        [Column("S_Descripcion")]
        public string descripcion { get; set; }

        [Column("S_Codigo")]
        public string codigo { get; set; }
    }

    public class OtrosConceptos
    {
        [Column("I_ID_Anticipo")]
        public int idTipoAnticipo { get; set; }

        [Column("I_ID_Tipo_Observacion")]
        public int idTipoObservacion { get; set; }

        [ForeignKey("idTipoObservacion")]
        public TiposObservacion TiposObservacion { get; set; }

        [Column("S_Observaciones")]
        public string? observaciones { get; set; }

        [Column("N_ImporteManual")]
        public decimal? importeManual { get; set; }

        [Column("D_FechaImporteMaual")]
        public DateTime? fechaImporteMaual { get; set; }

        [Column("I_Mes")]
        public int? mes { get; set; }

        [Column("I_Anno")]
        public int? anno { get; set; }
    }


}