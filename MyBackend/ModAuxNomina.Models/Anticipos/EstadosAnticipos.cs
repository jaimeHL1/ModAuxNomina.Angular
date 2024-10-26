 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace MyBackend.ModAuxNomina.Models.Anticipos
{ 
    public class EstadosAnticipos
    { 
        [Key]
        [Column("I_ID_ESTADO")]
        public int idEstado { get; set; }       
        
        [Column("S_DESCRIPCION")]
        public string? descripcionEstado { get; set; }   
        
        public ICollection<Anticipo> Anticipos { get; set; }
    }
}