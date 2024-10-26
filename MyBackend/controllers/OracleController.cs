using Microsoft.AspNetCore.Mvc; 
using MyBackend.ModAuxNomina.BL.OracleDbNedaes; 
using MyBackend.ModAuxNomina.Models.Oracle; 

namespace MyBackend.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OracleController : ControllerBase
    {
        private readonly OracleDBNedaes _oracleDbNedaes;

        public OracleController(OracleDBNedaes oracleDbNedaes)
        {
            _oracleDbNedaes = oracleDbNedaes;
        }

        //https://localhost:5001/api/Oracle/Empleado/51402751
        [HttpGet("Empleado/{dni}")]
        public EmpleadoOracle ObtenerEmpleado(string dni)
        {
            EmpleadoOracle empleadoOracle = _oracleDbNedaes.ObtenerEmpleado(dni);
            return empleadoOracle;
        }


        // https://localhost:5001/api/Oracle/NominaActiva/03
        [HttpGet("NominaActiva/{claseNom}")]
        public string ObtenerNominaEnCurso(string claseNom)
        {
            return _oracleDbNedaes.ObtenerNominaEnCurso(claseNom);

        }

    }
}
