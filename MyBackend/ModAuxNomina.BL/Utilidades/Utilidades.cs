using System.Globalization;
namespace MyBackend.ModAuxNomina.BL.UTilidades
{
    public static class Utilidades
    {
        //pasar fecha Juliana a DateTime
        public static DateTime JulianToDateTime(double julianDate)
        {
            double unixTime = (julianDate - 2440587.5) * 86400;

            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTime);

            return dtDateTime;
        } 

        public static double DateTimeToJulian(DateTime fecha)
        {
            // Fecha base del 01/01/1900 en formato juliano
            int baseJuliana = 2415021;

            // Fecha base en C#
            DateTime baseDate = new DateTime(1900, 1, 1);

            // Calcular la diferencia en días entre la fecha dada y la fecha base
            int daysDifference = (fecha - baseDate).Days;

            // Retornar el cálculo de la fecha juliana
            return baseJuliana + daysDifference;
        }

        public static string RecuperaNombreMes(int? numMes)
        {
            switch (numMes)
            {
                case 1: return "Enero";
                case 2: return "Febrero";
                case 3: return "Marzo";
                case 4: return "Abril";
                case 5: return "Mayo";
                case 6: return "Junio";
                case 7: return "Julio";
                case 8: return "Agosto";
                case 9: return "Septiembre";
                case 10: return "Octubre";
                case 11: return "Noviembre";
                case 12: return "Diciembre";
                default: return string.Empty;
            }
        }
    }
}