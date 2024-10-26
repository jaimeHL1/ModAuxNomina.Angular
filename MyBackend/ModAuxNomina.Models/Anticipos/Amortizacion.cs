
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyBackend.ModAuxNomina.Models.Anticipos
{

    public class Amortizacion
    {
        [Key]
        [Column("I_ID_Amortizacion")]
        public int idAmortizacion { get; set; }

        [Column("I_ID_Anticipo")]
        public int idAnticipo { get; set; }

        [ForeignKey("idAnticipo")]
        public Anticipo Anticipo { get; set; }

        [Column("I_AnnoAmortizado")]
        public int annoAmortizado { get; set; }

        [Column("I_MesAmortizado")]
        public int mesAmortizado { get; set; }

        [Column("I_NumPagasAmortizarMes")]
        public int numPagasAmortizarMes { get; set; }

        [Column("N_ImpMes")]
        public decimal? importeMes { get; set; }

        [Column("N_ImpMesReal")]
        public decimal? importeMesReal { get; set; }

        [Column("S_Observaciones")]
        public string? observaciones { get; set; }

        [Column("S_AmortizacionReal")]
        public string? amortizacionReal { get; set; }

        [Column("D_ALTA")]
        public DateTime? fechaAlta { get; set; }

        [Column("S_USUARIO_ALTA")]
        public string? usuarioAlta { get; set; }

        [Column("D_GM")]
        public DateTime? fechaModificacion { get; set; }

        [Column("S_USUARIOGM")]
        public string? usuarioModificacion { get; set; }

        [Column("D_BAJA")]
        public DateTime? fechaBaja { get; set; }

        [Column("S_USUARIO_BAJA")]
        public string? usuarioBaja { get; set; }

        [Column("I_ID_Tipo_Amortizacion")]
        public int? tipoAmortizacion { get; set; }

        [ForeignKey("tipoAmortizacion")]
        public TiposObservacion TiposObservacion { get; set; }
    }

    public class AmortizacionDetalle
    {
        public int annoAmortizacion { get; set; }
        public string real { get; set; }    //AmortizacionReal
        public string importeEnero { get; set; }
        public string? observacionesEnero { get; set; }
        public string importeFebrero { get; set; }
        public string? observacionesFebrero { get; set; }
        public string importeMarzo { get; set; }
        public string? observacionesMarzo { get; set; }
        public string importeAbril { get; set; }
        public string? observacionesAbril { get; set; }
        public string importeMayo { get; set; }
        public string? observacionesMayo { get; set; }
        public string importeJunio { get; set; }
        public string? observacionesJunio { get; set; }
        public string importeJulio { get; set; }
        public string? observacionesJulio { get; set; }
        public string importeAgosto { get; set; }
        public string? observacionesAgosto { get; set; }
        public string importeSeptiembre { get; set; }
        public string? observacionesSeptiembre { get; set; }
        public string importeOctubre { get; set; }
        public string? observacionesOctubre { get; set; }
        public string importeNoviembre { get; set; }
        public string? observacionesNoviembre { get; set; }
        public string importeDiciembre { get; set; }
        public string? observacionesDiciembre { get; set; }

    }
}