﻿using Newtonsoft.Json;
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
                    scannerRequest.SetCurrentPath(code);
                    ExecuteScanner(scannerRequest);
                }

                ExecuteReport(scannerRequest);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ExecuteScanner(ScannerRequest scannerRequest)
        {
            int lineNumber = 0;
            string line;
            string file = scannerRequest.CurrentPath.Split('\\').Last();

            var readLine = new StreamReader(scannerRequest.CurrentPath);

            while ((line = readLine.ReadLine()) != null)
            {
                SecurityCheck.SecurityCheckSteps(scannerRequest, line, file, lineNumber);
                lineNumber++;
            }
        }

        public static void ExecuteReport(ScannerRequest scannerRequest)
        {
            switch (scannerRequest.OutputFormat.ToUpper())
            {
                case "JSON":
                    Console.WriteLine(JsonConvert.SerializeObject(scannerRequest.Logs, Formatting.Indented));
                    break;
                case "PLAIN TEXT":
                    foreach (Domain.ScannerLog log in scannerRequest.Logs)
                    {
                        Console.WriteLine("[{0}] in file \"{1}\" on line {2}", log.LogType, log.File, log.Line);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
