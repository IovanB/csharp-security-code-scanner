using Newtonsoft.Json;
using SecurityCodeScanner.Application.Service;
using System;
using System.IO;
using System.Linq;

namespace SecurityCodeScanner.Application.UseCase.Scanner
{
    public static class ScannerUseCase
    {
        public static void ProcessRequest(ScannerRequest scannerRequest)
        {
            try
            {
                string[] sourceCode = Directory.GetFiles(scannerRequest.InputPath);

                foreach (var code in sourceCode)
                {
                    scannerRequest.CurrentPath = code;
                    ExecuteScanner(scannerRequest);
                }

                ExecuteReport(scannerRequest);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error has occured while scanning the code: {ex.Message}. StackTrace: {ex.StackTrace}");
            }
        }

        public static void ExecuteScanner(ScannerRequest scannerRequest)
        {
            scannerRequest.LineNumber = 0;
            scannerRequest.File = scannerRequest.CurrentPath.Split('\\').Last();

            var readLine = new StreamReader(scannerRequest.CurrentPath);

            while ((scannerRequest.Line = readLine.ReadLine()) != null)
            {
                SecurityCheck.SecurityCheckSteps(scannerRequest);
                scannerRequest.LineNumber++;
            }
        }

        public static void ExecuteReport(ScannerRequest scannerRequest)
        {
            switch (scannerRequest.OutputFormat.ToUpper())
            {
                case "JSON":
                    Console.WriteLine(JsonConvert.SerializeObject(scannerRequest.Logs, Formatting.Indented));
                    break;
                default:
                    foreach (Domain.ScannerLog log in scannerRequest.Logs)
                    {
                        Console.WriteLine($"[{log.LogType}] in file \"{log.File}\" on line {log.Line}");
                    }
                    break;
            }
        }
    }
}
