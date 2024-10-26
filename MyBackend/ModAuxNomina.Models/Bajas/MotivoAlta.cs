using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Text.Json.Serialization;

namespace MyBackend.ModAuxNomina.Models.Bajas
{
    public class MotivoAlta
    {

        [Key]
        [Column("CDMOTIVO")]
        public string CdMotivo { get; set; }

        [Column("DSMOTIVO")]
        public string DescripcionMotivo { get; set; }
        
        [Column("FECHA_BAJA")]
        public DateTime FechaBaja { get; set; }
       
    }
    }
