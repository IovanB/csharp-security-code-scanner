using SecurityCodeScanner.Domain;
using System.Collections.Generic;

namespace SecurityCodeScanner.Application.UseCase.Scanner
{
    public class ScannerRequest
    {
        public string InputPath { get; private set; }
        public List<ScannerLog> Logs { get; private set; }
        public string OutputFormat { get; private set; }
        public string CurrentPath { get;  set; }
        public string File { get;  set; }
        public string Line { get;  set; }
        public int LineNumber { get;  set; }

        public ScannerRequest(string inputPath, string output)
        {
            this.InputPath = inputPath;
            this.OutputFormat = output;
            Logs = new List<ScannerLog>();

        }

        public void AddLog(string logType, string file, int lineNumber)
            => Logs.Add(ScannerLog.AddLog( logType, file,  lineNumber));
        
    }
}
