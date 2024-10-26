using System.ComponentModel.DataAnnotations;
namespace MyBackend.ModAuxNomina.Models.Anticipos
{
    public class AnticipoCompleto
    {
        public string? usuarioAlta { get; set; }
        [Key]
        public int idAnticipo { get; set; }            // I_ID_Anticipo
        public string codigoHabilitacion { get; set; }  // S_CdHabiL
        public string claseNomina { get; set; }         // S_CdClasnm
        public string descripcionClaseNomina { get; set; }
        public string duplicado { get; set; }
        public string dni { get; set; }
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string apellidosNombre { get; set; }
        public int? annoSolicitud { get; set; }         // I_AnnoSolicitud
        public int? mesSolicitud { get; set; }          // I_MesSolicitud
        public string descripcionMes { get; set; }               //HAF_DevolverDescripcionMes
        public int? numPagasSolicitadas { get; set; }   //I_NumPagasSolicitadas    
        public decimal? irpf { get; set; }         //N_DedIRPF
        public decimal? sueldo { get; set; }          //N_Sueldo
        public decimal? trienios { get; set; }        //N_Trienios
        public decimal? pagaExtra { get; set; }          //N_PExtra
        public decimal? importePaga { get; set; }         //N_ImpPaga
        public decimal? importeLiquido { get; set; }      //N_ImpLiquido
        public decimal? importeTotal { get; set; }    // N_ImpTotalAnticipo
        public decimal? totalLiquidoPorNumPagas { get; set; }  //totalLiquidoPorNumPagas ?????
        public int? idEstado { get; set; }       //I_ID_Estado
        public DateTime? fechaBaja { get; set; }
        public decimal? importeAmortizado { get; set; }     //N_ImpAmortizado
        public decimal? cuotaMesAmortizacion { get; set; }
        public string? fechaCertificacion { get; set; }     //D_FechaCertificacion
        public string? fechaConcesion { get; set; }
        public string? descripcionEstado { get; set; }
        public string? fechaAlta { get; set; }
        public string? mesSiguiente { get; set; } //mesSiguiente
        public int? annoSiguiente { get; set; } //añoSiguiente
        public string? fechaInicioAmortizacion { get; set; }
        public decimal? importeRestante { get; set; }
        public int? numPagasAmortizadas { get; set; }   //I_NumPagasAmortizadas
        public int? numMesesDevolucion { get; set; }
        public int numMesesPendientes { get; set; }
        public string? grupo { get; set; }
        public string? nivel { get; set; }
        public string? observaciones { get; set; }
        public int? mesConcesion { get; set; }
        public int? annoConcesion { get; set; }
        public string? fechaNominaActual { get; set; }
        public string? fechaFinAmortizacion { get; set; }
    }


    public class AnticipoCompletoSP
    {
        public string? s_USUARIO_ALTA { get; set; }      // S_USUARIO_ALTA
        [Key]
        public int i_ID_Anticipo { get; set; }          // I_ID_Anticipo
        public string S_CdHabiL { get; set; }
        public string? s_CdClasnm { get; set; }
        public string dsclasnm { get; set; }
        public string S_CdDup { get; set; }
        public string s_CdDni { get; set; }             // S_CdDni
        public string dsNOMBRE { get; set; }
        public string dsAPEll1 { get; set; }
        public string dsAPEll2 { get; set; }
        public string dsApellidosNombre { get; set; }
        public int? i_Anno { get; set; }        // I_AnnoSolicitud
        public int? i_Mes { get; set; }         // I_MesSolicitud  
        public string dsMes { get; set; }
        public int? NUM_PAGAS { get; set; }
        public decimal? IRPF { get; set; }          // N_DedIRPF
        public decimal? SUELDO { get; set; }       // N_ImpLiquido
        public decimal? TRIENIOS { get; set; }         // N_Trienios
        public decimal? PPE { get; set; }           // N_PExtra 
        public decimal? Integro { get; set; }           // N_Sueldo
        public decimal? ANT_REIN { get; set; }
        public decimal? importeTotal { get; set; } // N_ImpTotalAnticipo
        public decimal? totalLiquidoPorNumPagas { get; set; }
        public int? i_Id_Estado { get; set; }            // I_Id_Estado
        public DateTime? d_BAJA { get; set; }            // D_BAJA
        public decimal? impAmortizado { get; set; }    // N_ImpAmortizado 
        public decimal? cuotaMesAmortizacion { get; set; }
        public string? fechaCertificacion { get; set; } // D_FechaCertificacion
        public string? fechaConcesion { get; set; }  // D_FechaConcesion
        public string S_Descripcion { get; set; }
        public string? fechaAlta { get; set; }
        public string? I_MesSiguiente { get; set; } //mesSiguiente
        public int? I_AnnoSiguiente { get; set; } //añoSiguiente
        public string? fechaInicioAmortizacion { get; set; }
        public decimal? importeRestante { get; set; }
        public int? numPagasAmortizadas { get; set; }  // I_NumPagasAmortizadas
        public int? numMesesDevolucion { get; set; }   // I_NumMesesDevolucion
        public int numMesesPendientes { get; set; }
        public string? grupo { get; set; }
        public string? nivel { get; set; }
        public string? observaciones { get; set; }     // S_Observaciones
        public int? I_mesConcesion { get; set; }
        public int? I_annoConcesion { get; set; }
        public string? fechaNominaActual { get; set; }
        public string? fechaFinAmortizacion { get; set; } // D_FechaFinAmortizacion    
    }
}