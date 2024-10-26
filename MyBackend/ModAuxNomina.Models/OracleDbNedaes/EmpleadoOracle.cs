using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
namespace MyBackend.ModAuxNomina.Models.Oracle
{
    public class EmpleadoOracle
    {
        public string CDHABIL { get; set; }
        public string CDCLASNM { get; set; } 
        public string CDDUP { get; set; } 
        public string CDDNI { get; set; } 
        public string DSNOMBRE { get; set; }
        public string DSAPELL1 { get; set; }
        public string DSAPELL2 { get; set; }
        public string? nombreCompleto => $"{DSAPELL1} {DSAPELL2}, {DSNOMBRE}";
        public string? CDGRUPO { get; set; }
        public string? CDNIVEL { get; set; }
    }
}