using MyBackend.ModAuxNomina.Models.Anticipos;
using MyBackend.ModAuxNomina.DA;
using MyBackend.ModAuxNomina.DA.OracleNedaes;
using Microsoft.EntityFrameworkCore;
using MyBackend.ModAuxNomina.BL.UTilidades;
using MyBackend.ModAuxNomina.BL.OracleDbNedaes;
using System.Globalization;
using System.Data;
using Microsoft.Data.SqlClient;

namespace MyBackend.ModAuxNomina.BL.Anticipos
{
    public class Anticipos_BL
    {
        private readonly DbNedaesContext _anticiposContext;
        private readonly OracleDbNedaesContext _oracleDBNedaesContext;
        private readonly OracleDBNedaes _oracleDBNedaes;

        // Lista estática para almacenar la lista general de anticipos
        private static List<AnticipoCompleto> _listaAnticiposPorEstadoGeneral = null;

        public Anticipos_BL(DbNedaesContext anticiposContext, OracleDbNedaesContext oracleDBNedaesContext, OracleDBNedaes oracleDBNedaes)
        {
            _anticiposContext = anticiposContext;
            _oracleDBNedaesContext = oracleDBNedaesContext;
            _oracleDBNedaes = oracleDBNedaes;
        }

        /// <summary>
        /// Obtiene una lista con las distintas nóminas
        /// </summary> 
        /// <returns></returns>
        public List<Nomina> ObtenerListaNominas()
        {
            //Recuperamos las nóminas con los siguientes códigos
            var codigosNomina = new List<string> { "01", "02", "03", "05", "06" };

            var listaNominas = _anticiposContext.V_SQL_TCLASNOM
                              .Where(v => codigosNomina.Contains(v.claseNomina))
                              .ToList();

            return listaNominas;
        }

        public List<AnticipoCompleto> ObtenerListaAnticiposPorEstado(int? idEstado, int? idAnticipo)
        {
            var listaAnticipos = FiltrarAnticipos(idEstado, idAnticipo)
                         .Include(a => a.EstadosAnticipos)
                         .Include(a => a.Nomina)
                         .Include(a => a.Empleado)
                         .Include(a => a.TPersonales)
                         .Include(a => a.Amortizaciones)
                         .ToList();

            var listaAnticiposPorEstado =
            (
                    from anticipo in listaAnticipos
                    select new AnticipoCompleto
                    {
                        usuarioAlta = anticipo.usuarioAlta,
                        idAnticipo = anticipo.idAnticipo,
                        codigoHabilitacion = ConstantesHabilitacion.HACIENDA,
                        claseNomina = anticipo.claseNomina,
                        descripcionClaseNomina = anticipo.Nomina.descripcionClaseNomina,
                        duplicado = "N",
                        dni = anticipo.dni,
                        nombre = anticipo.TPersonales?.nombre,
                        apellido1 = anticipo.TPersonales?.apellido1,
                        apellido2 = anticipo.TPersonales?.apellido2,
                        apellidosNombre = anticipo.TPersonales != null ? $"{anticipo.TPersonales.apellido1} {anticipo.TPersonales.apellido2}, {anticipo.TPersonales.nombre}" : string.Empty,
                        annoSolicitud = anticipo.annoSolicitud,
                        mesSolicitud = anticipo.mesSolicitud,
                        descripcionMes = Utilidades.RecuperaNombreMes(anticipo.mesSolicitud),
                        numPagasSolicitadas = anticipo.numeroPagasSolicitadas,
                        irpf = anticipo.irpf,
                        sueldo = anticipo.sueldo,
                        trienios = anticipo.trienios,
                        pagaExtra = anticipo.pagaExtra,
                        importePaga = anticipo.importePaga,
                        importeLiquido = anticipo.importeLiquido,
                        importeTotal = anticipo.importeTotal,
                        totalLiquidoPorNumPagas = anticipo.importeLiquido * anticipo.importeLiquido,
                        idEstado = anticipo.idEstado,
                        fechaBaja = anticipo.fechaBaja,
                        importeAmortizado = anticipo.importeAmortizado,
                        cuotaMesAmortizacion = (anticipo.numeroPagasAmortizadas != anticipo.numeroMesesDevolucion)
                                ? (anticipo.importeTotal / anticipo.numeroMesesDevolucion)
                                : 0,
                        fechaCertificacion = anticipo.fechaCertificacion?.ToString("dd/MM/yyyy") ?? string.Empty,
                        fechaConcesion = anticipo.fechaConcesion?.ToString("dd/MM/yyyy") ?? string.Empty,
                        descripcionEstado = anticipo.EstadosAnticipos.descripcionEstado,
                        fechaAlta = anticipo.fechaAlta?.ToString("dd/MM/yyyy") ?? string.Empty,

                        mesSiguiente = (anticipo.idEstado == ConstantesEstadosAnticipos.CONCEDIDO)
                            ? (anticipo.fechaConcesion.HasValue
                                ? Utilidades.RecuperaNombreMes(anticipo.fechaConcesion.Value.AddMonths(1).Month)
                                : null)
                            : (anticipo.idEstado == ConstantesEstadosAnticipos.ENAMORTIZACION && anticipo.numeroPagasAmortizadas != anticipo.numeroMesesDevolucion)
                            ? (anticipo.Amortizaciones
                                .Where(am => am.amortizacionReal == "S")
                                .OrderByDescending(am => am.idAmortizacion)
                                .Select(am => Utilidades.RecuperaNombreMes(
                                    (am.mesAmortizado + 1) % 12 == 0 ? 12 : (am.mesAmortizado + 1) % 12))
                                .FirstOrDefault())
                            : null,

                        annoSiguiente = (anticipo.idEstado == ConstantesEstadosAnticipos.CONCEDIDO)
                            ? (anticipo.fechaConcesion.HasValue
                                ? anticipo.fechaConcesion.Value.AddMonths(1).Year
                                : (int?)null)
                            : (anticipo.idEstado == ConstantesEstadosAnticipos.ENAMORTIZACION && anticipo.numeroPagasAmortizadas != anticipo.numeroMesesDevolucion)
                            ? (anticipo.Amortizaciones
                                .Where(am => am.amortizacionReal == "S")
                                .OrderByDescending(am => am.idAmortizacion)
                                .Select(am => am.annoAmortizado + ((am.mesAmortizado == 12) ? 1 : 0))
                                .FirstOrDefault())
                            : (int?)null,

                        fechaInicioAmortizacion = anticipo.fechaConcesion?.AddMonths(1).ToString("dd/MM/yyyy") ?? string.Empty,
                        importeRestante = (anticipo.importeTotal ?? 0) - (anticipo.importeAmortizado ?? 0),
                        numPagasAmortizadas = anticipo.numeroPagasAmortizadas ?? 0,
                        numMesesDevolucion = anticipo.numeroMesesDevolucion ?? 0,
                        numMesesPendientes = (anticipo.numeroMesesDevolucion ?? 0) - (anticipo.numeroPagasAmortizadas ?? 0),
                        grupo = anticipo.Empleado?.grupo ?? string.Empty,
                        nivel = anticipo.Empleado?.nivel ?? string.Empty,
                        observaciones = anticipo.observaciones ?? string.Empty,
                        mesConcesion = anticipo.fechaConcesion?.Month ?? 0,
                        annoConcesion = anticipo.fechaConcesion?.Year ?? 0,
                        fechaNominaActual = _oracleDBNedaes.ObtenerNominaEnCurso(anticipo.claseNomina),
                        fechaFinAmortizacion = anticipo.fechaFinAmortizacion?.ToString("dd/MM/yyyy") ?? string.Empty
                    }).ToList();

            listaAnticiposPorEstado = listaAnticiposPorEstado
            //.Where(c=>c.idAnticipo==6125)
            .OrderBy(a => a.idAnticipo)
            .ToList();

            return listaAnticiposPorEstado;
        }

        private IQueryable<Anticipo> FiltrarAnticipos(int? idEstadoParametro, int? idAnticipoParametro)
        {
            var listaAnticipos = _anticiposContext.HAT_ANTICIPOS.AsQueryable();

            if (idEstadoParametro == null || idEstadoParametro == -1)
            {
                // Si idEstadoParametro es null o -1, no se aplica ningún filtro, se devuelven todos los anticipos
            }
            else if (idEstadoParametro == 0)
            {
                // Filtrar por BORRADOR y CERTIFICACION
                listaAnticipos = listaAnticipos.Where(a =>
                    a.idEstado == ConstantesEstadosAnticipos.BORRADOR ||
                    a.idEstado == ConstantesEstadosAnticipos.CERTIFICACION);
            }
            else if (idEstadoParametro == 3)
            {
                // Filtrar por NEDAES
                listaAnticipos = listaAnticipos.Where(a => a.idEstado == ConstantesEstadosAnticipos.NEDAES);
            }
            else if (idEstadoParametro == 5)
            {
                // Filtrar por ENAMORTIZACION
                listaAnticipos = listaAnticipos.Where(a => a.idEstado == ConstantesEstadosAnticipos.ENAMORTIZACION);
            }
            else if (idEstadoParametro == ConstantesEstadosAnticipos.ESTADOSESPECIALES)
            {
                // Filtrar por CONCEDIDO o CIERRE con importeTotal != importeAmortizado
                listaAnticipos = listaAnticipos.Where(a =>
                    a.idEstado == ConstantesEstadosAnticipos.CONCEDIDO ||
                    (a.idEstado == ConstantesEstadosAnticipos.CIERRE && a.importeTotal != a.importeAmortizado));
            }
            else
            {
                // Para cualquier otro idEstadoParametro, filtrar directamente por el valor recibido
                listaAnticipos = listaAnticipos.Where(a => a.idEstado == idEstadoParametro);
            }

            // Filtrar por idAnticipoParametro si se proporciona
            if (idAnticipoParametro.HasValue)
            {
                listaAnticipos = listaAnticipos.Where(a => a.idAnticipo == idAnticipoParametro);
            }

            return listaAnticipos;
        }

        public List<AmortizacionDetalle> ObtenerAmortizacionDetalle(int id)
        {
            var amortizaciones = _anticiposContext.HAT_AMORTIZACIONES
                .Where(a => a.idAnticipo == id && a.amortizacionReal == "S")
                .ToList();

            var resultadoReal = amortizaciones
                .GroupBy(a => a.annoAmortizado)
                .Select(g =>
                {
                    var amortizacionEne = g.FirstOrDefault(m => m.mesAmortizado == 1);
                    var amortizacionFeb = g.FirstOrDefault(m => m.mesAmortizado == 2);
                    var amortizacionMar = g.FirstOrDefault(m => m.mesAmortizado == 3);
                    var amortizacionAbr = g.FirstOrDefault(m => m.mesAmortizado == 4);
                    var amortizacionMay = g.FirstOrDefault(m => m.mesAmortizado == 5);
                    var amortizacionJun = g.FirstOrDefault(m => m.mesAmortizado == 6);
                    var amortizacionJul = g.FirstOrDefault(m => m.mesAmortizado == 7);
                    var amortizacionAgo = g.FirstOrDefault(m => m.mesAmortizado == 8);
                    var amortizacionSep = g.FirstOrDefault(m => m.mesAmortizado == 9);
                    var amortizacionOct = g.FirstOrDefault(m => m.mesAmortizado == 10);
                    var amortizacionNov = g.FirstOrDefault(m => m.mesAmortizado == 11);
                    var amortizacionDic = g.FirstOrDefault(m => m.mesAmortizado == 12);

                    return new AmortizacionDetalle
                    {
                        annoAmortizacion = g.Key,
                        real = "S",
                        importeEnero = amortizacionEne != null && amortizacionEne.importeMesReal.HasValue
                            ? ((int)(amortizacionEne.importeMesReal.Value * amortizacionEne.numPagasAmortizarMes)).ToString()
                            : null,
                        observacionesEnero = amortizacionEne?.observaciones,

                        importeFebrero = amortizacionFeb != null && amortizacionFeb.importeMesReal.HasValue
                            ? ((int)(amortizacionFeb.importeMesReal.Value * amortizacionFeb.numPagasAmortizarMes)).ToString()
                            : null,
                        observacionesFebrero = amortizacionMar?.observaciones,

                        importeMarzo = amortizacionMar != null && amortizacionMar.importeMesReal.HasValue
                            ? ((int)(amortizacionMar.importeMesReal.Value * amortizacionFeb.numPagasAmortizarMes)).ToString()
                            : null,
                        observacionesMarzo = amortizacionMar?.observaciones,

                        importeAbril = amortizacionAbr != null && amortizacionAbr.importeMesReal.HasValue
                            ? ((int)(amortizacionAbr.importeMesReal.Value * amortizacionAbr.numPagasAmortizarMes)).ToString()
                            : null,
                        observacionesAbril = amortizacionAbr?.observaciones,

                        importeMayo = amortizacionMay != null && amortizacionMay.importeMesReal.HasValue
                            ? ((int)(amortizacionMay.importeMesReal.Value * amortizacionMay.numPagasAmortizarMes)).ToString()
                            : null,
                        observacionesMayo = amortizacionMay?.observaciones,

                        importeJunio = amortizacionJun != null && amortizacionJun.importeMesReal.HasValue
                            ? ((int)(amortizacionJun.importeMesReal.Value * amortizacionJun.numPagasAmortizarMes)).ToString()
                            : null,
                        observacionesJunio = amortizacionJun?.observaciones,

                        importeJulio = amortizacionJul != null && amortizacionJul.importeMesReal.HasValue
                            ? ((int)(amortizacionJul.importeMesReal.Value * amortizacionJul.numPagasAmortizarMes)).ToString()
                            : null,
                        observacionesJulio = amortizacionJul?.observaciones,

                        importeAgosto = amortizacionAgo != null && amortizacionAgo.importeMesReal.HasValue
                            ? ((int)(amortizacionAgo.importeMesReal.Value * amortizacionAgo.numPagasAmortizarMes)).ToString()
                            : null,
                        observacionesAgosto = amortizacionAgo?.observaciones,

                        importeSeptiembre = amortizacionSep != null && amortizacionSep.importeMesReal.HasValue
                            ? ((int)(amortizacionSep.importeMesReal.Value * amortizacionSep.numPagasAmortizarMes)).ToString()
                            : null,
                        observacionesSeptiembre = amortizacionSep?.observaciones,

                        importeOctubre = amortizacionOct != null && amortizacionOct.importeMesReal.HasValue
                            ? ((int)(amortizacionOct.importeMesReal.Value * amortizacionOct.numPagasAmortizarMes)).ToString()
                            : null,
                        observacionesOctubre = amortizacionOct?.observaciones,

                        importeNoviembre = amortizacionNov != null && amortizacionNov.importeMesReal.HasValue
                            ? ((int)(amortizacionNov.importeMesReal.Value * amortizacionNov.numPagasAmortizarMes)).ToString()
                            : null,
                        observacionesNoviembre = amortizacionNov?.observaciones,

                        importeDiciembre = amortizacionDic != null && amortizacionDic.importeMesReal.HasValue
                            ? ((int)(amortizacionDic.importeMesReal.Value * amortizacionDic.numPagasAmortizarMes)).ToString()
                            : null,
                        observacionesDiciembre = amortizacionDic?.observaciones
                    };
                })
                .ToList();

            var resultado = (resultadoReal).OrderBy(r => r.annoAmortizacion).ThenBy(r => r.real).ToList();

            return resultado;
        }

        //public async Task<List<AnticipoHistorial>> ObtenerHistoricoAnticipo(int idAnticipo)
        public List<AnticipoHistorial> ObtenerHistoricoAnticipo(int idAnticipo)
        {
            var listaAmortizaciones = _anticiposContext.HAT_AMORTIZACIONES
                .Where(am => am.idAnticipo == idAnticipo &&
                             am.amortizacionReal == "S" &&
                             (am.tipoAmortizacion != 4 || am.tipoAmortizacion == null))
                .Include(am => am.TiposObservacion)
                .Select(am => new AnticipoHistorial
                {
                    idAnticipo = am.idAnticipo,
                    idAmortizacion = am.idAmortizacion,
                    anno = am.annoAmortizado,
                    mes = am.mesAmortizado,
                    fecha = am.fechaAlta.HasValue ? am.fechaAlta.Value.ToString("dd/MM/yyyy") : string.Empty,
                    fechaD = am.fechaAlta.HasValue ? am.fechaAlta.Value : null,
                    importe = am.numPagasAmortizarMes * (am.importeMesReal ?? 0),
                    observaciones = am.observaciones,
                    tipoAmortizacion = am.tipoAmortizacion,
                    desTipoAmortizacion = am.TiposObservacion != null
                                          ? am.TiposObservacion.descripcion
                                          : "Amortización mensual"
                })
                .ToList();


            var listaOtrosConceptos = _anticiposContext.HAT_OTROS_CONCEPTOS
                .Where(oc => oc.idTipoAnticipo == idAnticipo)
                .Include(oc => oc.TiposObservacion)
                .Select(oc => new AnticipoHistorial
                {
                    idAnticipo = oc.idTipoAnticipo,
                    idAmortizacion = 0,
                    anno = oc.anno ?? null,
                    mes = oc.mes ?? null,
                    fecha = oc.fechaImporteMaual.HasValue ? oc.fechaImporteMaual.Value.ToString("dd/MM/yyyy") : string.Empty,
                    fechaD = oc.fechaImporteMaual.HasValue ? oc.fechaImporteMaual : null,
                    importe = oc.importeManual ?? null,
                    observaciones = oc.observaciones,
                    tipoAmortizacion = oc.idTipoObservacion,
                    desTipoAmortizacion = oc.TiposObservacion.descripcion
                })
                .ToList();

            var unionList = listaAmortizaciones
                .Union(listaOtrosConceptos)
                .OrderBy(a => a.fechaD)
                .ToList();

            return unionList;
        }


        public SalidaCalculoAnticipo CalcularAnticipo(string anno, string mes, string dni, string cdClasnm, int pagas)
        {

            var salidaCalculoAnticipo = new SalidaCalculoAnticipo();

            // Definir los parámetros de entrada y salida
            var paramAno = new SqlParameter("@Ano", anno);
            var paramMes = new SqlParameter("@Mes", mes);
            var paramDni = new SqlParameter("@Dni", dni);
            var paramCdClasnm = new SqlParameter("@cdClasnm", cdClasnm);
            var paramNPagas = new SqlParameter("@nPagas", pagas);

            // Parámetros de salida inicializados a 0
            var paramSueldo = new SqlParameter("@sueldo", SqlDbType.Decimal) { Direction = ParameterDirection.Output };
            var paramTrienios = new SqlParameter("@trienios", SqlDbType.Decimal) { Direction = ParameterDirection.Output };
            var paramPExtra = new SqlParameter("@PExtra", SqlDbType.Decimal) { Direction = ParameterDirection.Output };
            var paramImpPaga = new SqlParameter("@ImpPaga", SqlDbType.Decimal) { Direction = ParameterDirection.Output };
            var paramIrpf = new SqlParameter("@irpf", SqlDbType.Decimal) { Direction = ParameterDirection.Output };
            var paramImpLiquido = new SqlParameter("@ImpLiquido", SqlDbType.Decimal) { Direction = ParameterDirection.Output };
            var paramImpTotal = new SqlParameter("@ImpTotal", SqlDbType.Decimal) { Direction = ParameterDirection.Output };

            // Llamada al procedimiento almacenado utilizando FromSqlRaw 
            _anticiposContext.Database.ExecuteSqlRaw(
               "EXEC dbo.HAP_CalculaImportesAnticipo @Ano, @Mes, @Dni, @cdClasnm, @nPagas, " +
               "@sueldo OUTPUT, @trienios OUTPUT, @PExtra OUTPUT, @ImpPaga OUTPUT, @irpf OUTPUT, " +
               "@ImpLiquido OUTPUT, @ImpTotal OUTPUT",
               paramAno, paramMes, paramDni, paramCdClasnm, paramNPagas,
               paramSueldo, paramTrienios, paramPExtra, paramImpPaga, paramIrpf, paramImpLiquido, paramImpTotal
           );

            salidaCalculoAnticipo.Sueldo = (decimal)paramSueldo.Value;
            salidaCalculoAnticipo.Trienios = (decimal)paramTrienios.Value;
            salidaCalculoAnticipo.PExtra = (decimal)paramPExtra.Value;
            salidaCalculoAnticipo.ImpPaga = (decimal)paramImpPaga.Value;
            salidaCalculoAnticipo.IRPF = (decimal)paramIrpf.Value;
            salidaCalculoAnticipo.ImpLiquido = (decimal)paramImpLiquido.Value;
            salidaCalculoAnticipo.ImpTotal = (decimal)paramImpTotal.Value;

            return salidaCalculoAnticipo;

        }

        public int InsertarAnticipo(string anno, string mes, string dni, string cdClasnm, int pagas, string usuarioAlta)
        {
            int resultado = -1;

            // Comprobación de anticipos existentes no amortizados
            var anticiposExistentes = _anticiposContext.HAT_ANTICIPOS
                .Where(a => a.dni == dni &&
                        a.idEstado != ConstantesEstadosAnticipos.AMORTIZADO &&
                        a.idEstado != ConstantesEstadosAnticipos.CIERRE)
                .ToList();

            if (anticiposExistentes.Any())
            {
                // Ya existe algún anticipo activo para el empleado 
                return -3;
            }

            // Calcular el anticipo
            var salidaCalculo = CalcularAnticipo(anno, mes, dni, cdClasnm, pagas);

            if (salidaCalculo == null)
            {
                return -1; // Error en el cálculo
            }

            // Definir los meses de devolución según la clase de nómina y el número de pagas
            int mesesDevolucion = 0;

            if (cdClasnm == ConstantesCDCLASNOM.ALTOS_CARGOS ||
                cdClasnm == ConstantesCDCLASNOM.FUNCIONARIOS ||
                cdClasnm == ConstantesCDCLASNOM.FUNCIONARIOS_EN_EL_EXTRANJERO) // Funcionarios
            {
                if (pagas == 1) mesesDevolucion = 10;
                else if (pagas == 2) mesesDevolucion = 14;
                else return -2; // Error: número de pagas inválido para funcionarios
            }
            else if (cdClasnm == ConstantesCDCLASNOM.CONTRATADOS_LABORALES ||
                     cdClasnm == ConstantesCDCLASNOM.CONTRATADOS_LABORALES_EN_EL_EXTRANJERO)
            {
                if (pagas <= 4) mesesDevolucion = 24;
                else return -2; // Error: número de pagas inválido para laborales
            }

            // Preparar el objeto para insertar en HAT_ANTICIPOS
            var nuevoAnticipo = new Anticipo
            {
                dni = dni,
                annoSolicitud = int.Parse(anno),
                mesSolicitud = int.Parse(mes),
                claseNomina = cdClasnm,
                numeroPagasSolicitadas = pagas,
                usuarioAlta = usuarioAlta,
                fechaAlta = DateTime.Now,
                irpf = salidaCalculo.IRPF,
                importeLiquido = salidaCalculo.ImpLiquido,
                importePaga = salidaCalculo.ImpPaga,
                importeTotal = salidaCalculo.ImpTotal,
                pagaExtra = salidaCalculo.PExtra,
                sueldo = salidaCalculo.Sueldo,
                trienios = salidaCalculo.Trienios,
                idEstado = ConstantesEstadosAnticipos.BORRADOR,
                numeroMesesDevolucion = mesesDevolucion
            };

            // Insertar anticipo
            try
            {
                _anticiposContext.HAT_ANTICIPOS.Add(nuevoAnticipo);
                _anticiposContext.SaveChanges();

                // Devolver el ID del nuevo anticipo insertado
                resultado = nuevoAnticipo.idAnticipo;
            }
            catch (Exception)
            {
                // En caso de error, devolver -1
                resultado = -1;
            }

            return resultado;
        }

        public int BorrarAnticipo(int idAnticipo)
        {
            try
            {
                // Buscar el anticipo
                var anticipo = _anticiposContext.HAT_ANTICIPOS
                    .FirstOrDefault(a => a.idAnticipo == idAnticipo);

                if (anticipo == null)
                {
                    return -1; // No se encontró el anticipo
                }

                // Verificar si el anticipo puede ser eliminado
                // Solo se pueden eliminar anticipos en estado BORRADOR o estados iniciales
                if (anticipo.idEstado != ConstantesEstadosAnticipos.BORRADOR)
                {
                    return -2; // El anticipo no se puede eliminar debido a su estado
                }

                // Eliminar el anticipo
                _anticiposContext.HAT_ANTICIPOS.Remove(anticipo);
                _anticiposContext.SaveChanges();

                // Limpiar la caché de anticipos si existe
                if (_listaAnticiposPorEstadoGeneral != null)
                {
                    _listaAnticiposPorEstadoGeneral = null;
                }

                return 1; // Eliminación exitosa
            }
            catch (Exception)
            {
                return 0; // Error durante la eliminación
            }
        }


    }
}

