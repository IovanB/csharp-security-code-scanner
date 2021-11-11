using SecurityCodeScanner.Application.UseCase.Scanner;
using System.Linq;
using System.Text.RegularExpressions;

namespace SecurityCodeScanner.Application.Service
{
    public static class SecurityCheck
    {
        public static void SecurityCheckSteps(ScannerRequest scannerRequest)
        {
            FindSensitiveData(scannerRequest);

            FindSQLInjection(scannerRequest);

            FindSiteScripting(scannerRequest);

        }

        public static void FindSensitiveData(ScannerRequest scannerRequest)
        {
            string[] findSensitiveData = new string[3] { "Checkmarx", "Hellman & Friedman", "$1.15b" };

            if (findSensitiveData.All(scannerRequest.Line.Contains))
                scannerRequest.AddLog("Sensitive Data Exposure", scannerRequest.File, scannerRequest.LineNumber);
        }

        public static void FindSQLInjection(ScannerRequest scannerRequest)
        {
            var pattern = "^\"(SELECT|Select|select)\\b.*\\b(WHERE|Where|where)\\b\\s.*(%s)\"$";

            if (Regex.IsMatch(scannerRequest.Line, pattern))
                scannerRequest.AddLog("SQL Injection", scannerRequest.File, scannerRequest.LineNumber);
        }

        public static void FindSiteScripting(ScannerRequest scannerRequest)
        {
            string[] crossSiteScripting = new string[1] { "Alert()" };

            if (crossSiteScripting.All(scannerRequest.Line.Contains))
                scannerRequest.AddLog("Cross Site Scripting", scannerRequest.File, scannerRequest.LineNumber);
        }
    }
}
