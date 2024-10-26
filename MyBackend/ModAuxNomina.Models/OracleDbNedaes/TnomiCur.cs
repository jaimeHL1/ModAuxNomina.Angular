using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace MyBackend.ModAuxNomina.Models.Oracle
{
    public partial class TnomiCur
    {
        public string CDHABIL { get; set; }
        public string CDCLASNM { get; set; }
        public double FECONANT { get; set; }

        [NotMapped]
        public DateTime FechaConAnt { get; set; }
        [NotMapped]
        public string Año { get; set; }
        [NotMapped]
        public string Mes { get; set; }
    }
}