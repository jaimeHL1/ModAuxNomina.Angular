using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyBackend.ModAuxNomina.Models.Anticipos
{
    public class TPersonales
    {
        [Column("CDHABIL")]
        public string codigoHabilitacion { get; set; }

        [Column("CDCLASNM")]
        public string claseNomina { get; set; }
        
        [Column("CDDNI")]
        public string dni { get; set; }

        [Column("CDDUP")]
        public string duplicado { get; set; }

        [Column("DSNOMBRE")]
        public string nombre { get; set; }

        [Column("DSAPELL1")]
        public string apellido1 { get; set; }

        [Column("DSAPELL2")]
        public string apellido2 { get; set; }
 
    }
}