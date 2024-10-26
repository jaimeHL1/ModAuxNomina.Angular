
using MyBackend.ModAuxNomina.DA.OracleNedaes;
using MyBackend.ModAuxNomina.BL.UTilidades;
using MyBackend.ModAuxNomina.Models.Oracle;
using Microsoft.EntityFrameworkCore;

namespace MyBackend.ModAuxNomina.BL.OracleDbNedaes
{
    public class OracleDBNedaes
    {
        private readonly OracleDbNedaesContext _oracleDbNedaesContext;
        private static List<TnomiCur> _nominasEnCurso = null;

        public OracleDBNedaes(OracleDbNedaesContext oracleDbNedaesContext)
        {
            _oracleDbNedaesContext = oracleDbNedaesContext;

            if (_nominasEnCurso == null)
            {
                // Carga los datos filtrados en memoria
                _nominasEnCurso = _oracleDbNedaesContext.TNOMICUR
                    .AsNoTracking()
                    .ToList();
            }
        }

        //Obtener nómina en curso de HAC para claseNom.  
        public string ObtenerNominaEnCurso(string claseNom)
        {
            string fecha = null;
            var nominaActiva = _nominasEnCurso
                .Where(c => c.CDCLASNM == claseNom)
                .FirstOrDefault();

            fecha = Utilidades.JulianToDateTime(nominaActiva.FECONANT).ToString(("dd/MM/yyyy")); 
            return fecha;
        }

        /// <summary>
        /// Obtiene los datos del empleado
        /// </summary>
        /// <param name="dni"></param>
        /// <returns></returns>
        public EmpleadoOracle ObtenerEmpleado(string dni)
        {
            EmpleadoOracle emp = _oracleDbNedaesContext.VM_DATOS_EMPLEADOS_NEDAES.Where(x => x.CDDNI == dni).FirstOrDefault();
            return emp;
        }
    }
}

