using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Text.Json.Serialization;

namespace MyBackend.ModAuxNomina.Models.Bajas
{
    public class TipoBaja
    {
        [Key]
        [Column("S_CDTIPO")]
        public string CdTipo { get; set; }

        [Column("S_DSTIPO")]
        public string DescripcionTipo { get; set; }
        [Column("S_DCTIPO")]
        public string DcTipo { get; set; }

        [Column("D_ALTA")]
        public DateTime FechaAlta { get; set; }

        [Column("S_USUARIO_ALTA")]
        public string UsuarioAlta { get; set; }
        [Column("D_BAJA")]
        public DateTime FechaBaja { get; set; }

        [Column("S_USUARIO_BAJA")]
        public string UsuarioBaja { get; set; }
        
           [Column("D_GM")]
        public DateTime FechaModificacion { get; set; }

        [Column("S_USUARIOGM")]
        public string UsuarioModificacion { get; set; }
    }
    }
