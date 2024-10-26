using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
namespace MyBackend.ModAuxNomina.Models.Anticipos
{ 
    public class Nomina
    {
        [Key]        
        [Column("CDCLASNM")]
        public string claseNomina { get; set; }  
      
        [Column("DSCLASNM")]
        public string  descripcionClaseNomina { get; set; }  

        public ICollection<Anticipo> Nominas { get; set; }
           
    }
}