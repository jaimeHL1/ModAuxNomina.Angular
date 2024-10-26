using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Text.Json.Serialization;

namespace MyBackend.ModAuxNomina.Models.Bajas
{
    public class Bajas
    {

        [Key]
        [Column("I_ID_BAJA")]
        public int IdBaja { get; set; }

        [Column("S_CDHABIL")]
        public string CdHabil { get; set; }
        [Column("S_CDCLASNM")]
        public string CdClasNm { get; set; }
        [Column("S_CDDUP")]
        public string CdDup { get; set; }
        [Column("S_CDDNI")]
        public string CdDni { get; set; }
        
        [Column("I_ORDEN")]
        public int IOrden { get; set; }

        [Column("D_FECHA_BAJA")]
        public DateTime? DFechaBaja { get; set; }

        [Column("D_FECHA_ALTA")]
        
        public DateTime? DFechaAlta { get; set; }
        [Column("S_CDTIPO")]
        
        public string CdTipo { get; set; }
        [Column("S_CDMOTIVO")]
        public string CdMotivo { get; set; }

        [Column("S_CDRECAIDA")]
        public string CdRecaida { get; set; }
        [Column("I_ID_BAJA_PADRE_RECAIDA")]
        public int? IdBajaPadreRecaida { get; set; }

        [Column("S_OBSERVACIONES")]
        public string Observaciones { get; set; }


        [Column("D_MESES12")]
        public DateTime? Meses12 { get; set; }


        [Column("D_AGOTADO_P18")]
        public DateTime? AgotadoP18 { get; set; }
        [Column("D_MESES24")]
        public DateTime? Meses24 { get; set; }
        [Column("D_PROPUESTA_INVALIDEZ")]
        public DateTime? PropuestaInvalides { get; set; }
        [Column("S_PRORROGA")]
        public string Prorroga { get; set; }
        [Column("D_FECHA_INI_EXP")]
        public DateTime? FechaIniExp { get; set; }
        [Column("S_USUARIO_ALTA")]
        public string UsuarioAlta { get; set; }
        [Column("D_ALTA")]
        public DateTime? FechaAlta { get; set; }

        [Column("S_USUARIOGM")]
        public string SUsuarioGM { get; set; }
        [Column("D_GM")]
        public DateTime? FechaMoficicacion { get; set; }
        [Column("S_USUARIO_BAJA")]
        public string UsuarioBaja { get; set; }
        [Column("D_BAJA")]
        public DateTime? FechaBaja { get; set; }
        [Column("I_ID_PRIMERA_BAJA_RECAIDAS")]
        public int? IdPrimeraBajaRecaidas { get; set; }
    }
    }
