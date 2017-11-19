using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Serialization;

namespace src
{
    public static class DateTimeHelper
    {
        public static IEnumerable<DateTime> DiasUteisDoMes(this DateTime data)
        {
            var ano = data.Year;
            var mes = data.Month;
            var diasFinalDeSemana = new[] { DayOfWeek.Saturday, DayOfWeek.Sunday };
            var totalDiasMes = DateTime.DaysInMonth(ano, mes);
            var dias = Enumerable.Range(1, totalDiasMes)
                        .Select(n => new DateTime(ano, mes, n))
                        .Where(d => !diasFinalDeSemana.Contains(d.DayOfWeek));

            return dias;
        }


         public static List<DateTime> FeriadosDoMes(this DateTime data)
        {
             var ano = data.Year;
            var mes = data.Month;
            
            var estado = "df".ToUpperInvariant();
            var cidade = "brasilia".ToUpperInvariant();
            var endpointApiFeriado =
                $"http://www.calendario.com.br/api/api_feriados.php?ano={ano}&estado={estado}&cidade={cidade}&token=cmFtb3MuZGFuaWVsZmVycmVpcmFAZ21haWwuY29tJmhhc2g9MTE1MjM5MzM0";

            using (var httpClient = new WebClient())
            {
                var feriadosNoMes = new List<DateTime>();
                var stringXmlDiasAnoFeriado = httpClient.DownloadString(endpointApiFeriado);
                var xmlSerializar = new XmlSerializer(typeof(Events));
                var apiResponse = (Events)xmlSerializar.Deserialize(new StringReader(stringXmlDiasAnoFeriado));

                if (apiResponse == null) return feriadosNoMes;

                feriadosNoMes = apiResponse.Event.Where(d => d.Date.Month == mes && d.Type_code == ((int)TipoFeriado.FeriadoNacional).ToString()).Select(d => d.Date).ToList();

                return feriadosNoMes;
            }
        }
    }
}