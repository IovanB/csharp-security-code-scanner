namespace SecurityCodeScanner.Domain
{
    public class ScannerLog
    {
        public string LogType { get;  set; }
        public string File { get;  set; }
        public int Line { get;  set; }

        public ScannerLog(string logType, string file, int lineNumber)
        {
            LogType = logType;
            File = file;
            Line = lineNumber;
        }

        public static ScannerLog AddLog(string logType, string file, int lineNumber)
            => new ScannerLog(logType, file, lineNumber);
    }
}
