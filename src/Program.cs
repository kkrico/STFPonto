using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Serialization;
using OfficeOpenXml;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            if (IsProcessoAtivo())
            {
                Console.WriteLine(@"Execução em andamento.");
                Finalizar();
            }

            var dataAtual = DateTime.Now;

            var diasDeTrabalho = dataAtual.DiasUteisDoMes();
            var feriadosNoMes = dataAtual.FeriadosDoMes();

            var diasParaInserirNoPonto = diasDeTrabalho.Except(feriadosNoMes);
            var fi = new FileInfo(@"C:\myworkbook.xlsx");
            using (var p = new ExcelPackage(fi))
            {
                //A workbook must have at least on cell, so lets add one... 
                var ws = p.Workbook.Worksheets["Folha de Ponto"];
                //To set values in the spreadsheet use the Cells indexer.
                ws.Cells["D12"].Value = $"This is cell {diasParaInserirNoPonto.First().Date.ToShortDateString()}";
                //Save the new workbook. We haven't specified the filename so use the Save as method.
                p.Save();
            }
        }

        private static void Finalizar()
        {
            Console.WriteLine(@"Rotina de Geração da Folha de Ponto Finalizada!");
        }

        private static bool IsProcessoAtivo() => Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1;
    }
}
