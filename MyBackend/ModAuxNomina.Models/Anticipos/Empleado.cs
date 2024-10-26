using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
namespace MyBackend.ModAuxNomina.Models.Anticipos
{
    //Retorna el total a precibir de un empleado en un mes
    public class Empleado
    {
        [Column("CDDNI")]
        public string dni { get; set; }

        [Column("CDCLASNM")]
        public string claseNomina { get; set; }

        [Column("CDHABIL")]
        public string codigoHabilitacion { get; set; }

        [Column("CDDUP")]
        public string duplicado { get; set; }

        [Column("CDNUMNOM")]
        public string numeroNomina { get; set; }

        [Column("DSNOMBRE")]
        public string nombre { get; set; }

        [Column("DSAPELL1")]
        public string apellido1 { get; set; }

        [Column("DSAPELL2")]
        public string apellido2 { get; set; }

        [Column("NombreCompleto")]
        public string? nombreCompleto => $"{apellido1} {apellido2}, {nombre}";

        [Column("KKGRUPO")]
        public string? grupo { get; set; }

        [Column("CDNIVEL")]
        public string? nivel { get; set; }

 
    }
}