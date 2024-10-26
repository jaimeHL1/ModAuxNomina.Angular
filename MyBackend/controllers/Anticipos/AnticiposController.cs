using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MyBackend.ModAuxNomina.BL.Anticipos;
using MyBackend.ModAuxNomina.Models.Anticipos;


namespace MyBackend;

[ApiController]
[Route("api/[controller]")]
public class AnticiposController : ControllerBase
{
   private readonly Anticipos_BL _anticipos_BL;

   public AnticiposController(Anticipos_BL anticipos_BL)
   {
      _anticipos_BL = anticipos_BL;
   }

   [HttpGet]
   public string Get()
   {
      return JsonSerializer.Serialize("Ejecutando Anticipos Controller OK");
   }
   //https://localhost:5001/api/Anticipos/ListaNominas
   [HttpGet("ListaNominas")]
   public List<Nomina> ObtenerListaNominas()
   {
      List<Nomina> listaNominas = _anticipos_BL.ObtenerListaNominas();
      return listaNominas;
   }

   [HttpGet("ListaAnticiposPorEstado/{idEstado?}/{idAnticipo?}")]
   public List<AnticipoCompleto> ObtenerListaAnticiposPorEstado(int? idEstado, int? idAnticipo)
   {
      List<AnticipoCompleto> listaAnticiposPorEstado = _anticipos_BL.ObtenerListaAnticiposPorEstado(idEstado, idAnticipo);

      return listaAnticiposPorEstado;
   }

   //https://localhost:5001/api/Anticipos/AmortizacionDetalle/6500
   [HttpGet("AmortizacionDetalle/{idAnticipo}")]
   public List<AmortizacionDetalle> ObtenerAmortizacionDetalle(int idAnticipo)
   {
      List<AmortizacionDetalle> listaAnticiposPorEstado = _anticipos_BL.ObtenerAmortizacionDetalle(idAnticipo);
      return listaAnticiposPorEstado;
   }


   // https://localhost:5001/api/Anticipos/ObtenerHistoricoAnticipo/6500
   [HttpGet("ObtenerHistoricoAnticipo/{idAnticipo}")]
   public List<AnticipoHistorial> ObtenerHistoricoAnticipo(int idAnticipo)
   {
      return _anticipos_BL.ObtenerHistoricoAnticipo(idAnticipo);

   }

   //https://localhost:5001/api/Anticipos/CalcularAnticipo/2024/10/51402751/"02"/2
   [HttpGet("CalcularAnticipo/{anno}/{mes}/{dni}/{cdClasnm}/{pagas}")]
   public SalidaCalculoAnticipo CalcularAnticipo(string anno, string mes, string dni, string cdClasnm, int pagas)
   {

      SalidaCalculoAnticipo salidaCalculoAnticipo = new SalidaCalculoAnticipo();
      try
      {
         salidaCalculoAnticipo = _anticipos_BL.CalcularAnticipo(anno, mes, dni, cdClasnm, pagas);
         return salidaCalculoAnticipo;
      }
      catch (Exception e)
      {
         salidaCalculoAnticipo.mensajeError = e.HResult + ' ' + e.Message;
         return salidaCalculoAnticipo;
      }
   }

   [HttpPost("InsertarAnticipo/{anno}/{mes}/{dni}/{cdClasnm}/{pagas}/{usuarioAlta}")]
   public IActionResult InsertarAnticipo(string anno, string mes, string dni, string cdClasnm, int pagas, string usuarioAlta)
   {
      try
      {
         var resultado = _anticipos_BL.InsertarAnticipo(anno, mes, dni, cdClasnm, pagas, usuarioAlta);

         if (resultado == -1)
         {
            return BadRequest("Error en el proceso de inserción del Anticipo.");
         }
         if (resultado == -2)
         {
            return BadRequest("No se puede dar más pagas al empleado.");
         }
         if (resultado == -3)
         {
            return BadRequest("Ya existe algún anticipo activo para este DNI.");
         }

         return Ok(new { identificador = resultado });
      }
      catch (Exception ex)
      {
         return StatusCode(500, new { error = ex.Message });
      }
   }

   [HttpDelete("BorrarAnticipo/{idAnticipo}")]
   public IActionResult BorrarAnticipo(int idAnticipo)
   {
      try
      {
         var resultado = _anticipos_BL.BorrarAnticipo(idAnticipo);

         switch (resultado)
         {
            case 1:
               return Ok(new { mensaje = "Anticipo eliminado correctamente" });
            case -1:
               return NotFound(new { error = "No se encontró el anticipo especificado" });
            case -2:
               return BadRequest(new { error = "El anticipo no se puede eliminar debido a su estado actual" });
            default:
               return StatusCode(500, new { error = "Error al intentar eliminar el anticipo" });
         }
      }
      catch (Exception ex)
      {
         return StatusCode(500, new { error = ex.Message });
      }
   }

}