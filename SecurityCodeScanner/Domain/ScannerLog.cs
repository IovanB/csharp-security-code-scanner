namespace SecurityCodeScanner.Domain
{
    public class ScannerLog
    {
        public string LogType { get;  private set; }
        public string File { get; private set; }
        public int Line { get; private set; }

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
