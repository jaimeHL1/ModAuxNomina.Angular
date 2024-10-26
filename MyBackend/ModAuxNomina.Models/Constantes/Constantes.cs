 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks; 

public static class ConstantesCDCLASNOM
{
  
    public const string
        ALTOS_CARGOS = "01",
        FUNCIONARIOS = "02",
        FUNCIONARIOS_EN_EL_EXTRANJERO = "03",
        FUNCIONARIOS_DOCENTES_UNIVERSITARIOS = "04",
        CONTRATADOS_LABORALES = "05",
        CONTRATADOS_LABORALES_EN_EL_EXTRANJERO = "06",
        CONTRATADOS_ADMINISTRATIVOS = "07",
        CONTRATADOS_ADMINISTRATIVOS_DOCENTES_UNIVERSITARIOS = "08",
        LABORALES_EN_EL_EXTRANJERO = "09",
        COSTOS_TODAS_LAS_CLASES_DE_NOMINA = "99",
        RGS_SOCIAL = "SS";
}

public static class ConstantesEstadosAnticipos
{  
    public const int 
        BORRADOR = 1,
        CERTIFICACION = 2,
        NEDAES = 3,
        CONCEDIDO = 4,
        ENAMORTIZACION =5,
        AMORTIZADO = 6,
        CIERRE = 7,
        AMORTIZADOFICTICIO = 55,
        ESTADOSESPECIALES = 99;
}

public static class ConstantesHabilitacion
{
    public const string HACIENDA = "HAC";
} 

public static class ConstantesMeses
{  
    public const int 
        Enero = 1,
        Febrero = 2,
        Marzo = 3,
        Abril = 4,
        Mayo =5,
        Junio = 6,
        Julio = 7,
        Agosto = 8,
        Septiembre = 9,
        Octubre = 10,
        Noviembre = 11,
        Diciembre = 12;
}