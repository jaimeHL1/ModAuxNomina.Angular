using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Text.Json.Serialization;

namespace MyBackend.ModAuxNomina.Models.Anticipos
{
    public class Anticipo
    {
        [Key]
        [Column("I_ID_Anticipo")]
        public int idAnticipo { get; set; }


        [Column("S_CdDni")]
        public string dni { get; set; }


        [Column("I_AnnoSolicitud")]
        public int? annoSolicitud { get; set; }


        [Column("I_MesSolicitud")]
        public int? mesSolicitud { get; set; }


        [Column("S_CdClasnm")]
        public string? claseNomina { get; set; }

        [ForeignKey("dni, claseNomina")]
        public Empleado Empleado { get; set; }

        [ForeignKey("dni, claseNomina")]
        public TPersonales TPersonales { get; set; }

        [ForeignKey("claseNomina")]
        public Nomina Nomina { get; set; }

        [Column("I_Id_Estado")]
        public int? idEstado { get; set; }

        [ForeignKey("idEstado")]
        public EstadosAnticipos EstadosAnticipos { get; set; }


        [Column("I_NumPagasSolicitadas")]
        public int? numeroPagasSolicitadas { get; set; }


        [Column("I_NumMesesDevolucion")]
        public int? numeroMesesDevolucion { get; set; }

        [Column("N_ImpTotalAnticipo")]
        public decimal? importeTotal { get; set; }

        [Column("I_NumPagasAmortizadas")]
        public int? numeroPagasAmortizadas { get; set; }

        [Column("N_ImpAmortizado")]
        public decimal? importeAmortizado { get; set; }

        [Column("D_FechaPropuesta")]
        public DateTime? fechaPropuesta { get; set; }

        [Column("D_FechaCertificacion")]
        public DateTime? fechaCertificacion { get; set; }

        [Column("I_NumCertificacion")]
        public string? numeroCertificacion { get; set; }

        [Column("D_FechaConcesion")]
        public DateTime? fechaConcesion { get; set; }

        [Column("N_Sueldo")]
        public decimal? sueldo { get; set; }

        [Column("N_Trienios")]
        public decimal? trienios { get; set; }

        [Column("N_PExtra")]
        public decimal? pagaExtra { get; set; }

        [Column("N_ImpPaga")]
        public decimal? importePaga { get; set; }

        [Column("N_DedIRPF")]
        public decimal? irpf { get; set; }

        [Column("N_ImpLiquido")]
        public decimal? importeLiquido { get; set; }

        [Column("S_USUARIO_ALTA")]
        public string? usuarioAlta { get; set; }

        [Column("D_ALTA")]
        public DateTime? fechaAlta { get; set; }

        [Column("S_USUARIOGM")]
        public string? usuarioModificacion { get; set; }

        [Column("D_GM")]
        public DateTime? fechaModificacion { get; set; }

        [Column("S_USUARIO_BAJA")]
        public string? usuarioBaja { get; set; }

        [Column("D_BAJA")]
        public DateTime? fechaBaja { get; set; }

        [Column("S_Observaciones")]
        public string? observaciones { get; set; }

        [Column("B_VieneDeOtroMinisterio")]
        public bool vieneDeOtroMinisterio { get; set; }

        [Column("D_FechaFinAmortizacion")]
        public DateTime? fechaFinAmortizacion { get; set; }

        [Column("B_NoSaleEnDiscrepancias")]
        public bool? noSaleEnDiscrepancias { get; set; }

        // Un Anticipo puede tener varias Amortizaciones
        public ICollection<Amortizacion> Amortizaciones { get; set; }
    }
}