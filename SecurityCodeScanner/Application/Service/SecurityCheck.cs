using SecurityCodeScanner.Application.UseCase.Scanner;
using System.Linq;
using System.Text.RegularExpressions;

namespace SecurityCodeScanner.Application.Service
{
    public static class SecurityCheck
    {
        public static void SecurityCheckSteps(ScannerRequest scannerRequest, string input, string file, int lineNumber)
        {
            FindSensitiveData(scannerRequest, input, file, lineNumber);

            FindSQLInjection(scannerRequest, input, file, lineNumber);

            FindSiteScripting(scannerRequest, input, file, lineNumber);

        }

        public static void FindSensitiveData(ScannerRequest scannerRequest, string input, string file, int lineNumber)
        {
            string[] findSensitiveData = new string[] { "Checkmarx", "Hellman & Friedman", "$1.15b" };

            if (findSensitiveData.All(input.Contains))
                scannerRequest.AddLog("Sensitive Data Exposure", file, lineNumber);
        }

        public static void FindSQLInjection(ScannerRequest scannerRequest, string input, string file, int lineNumber)
        {
            var pattern = "^\"(SELECT|Select|select)\\b.*\\b(WHERE|Where|where)\\b\\s.*(%s)\"$";

            if (Regex.IsMatch(input, pattern))
                scannerRequest.AddLog("SQL Injection", file, lineNumber);
        }

        public static void FindSiteScripting(ScannerRequest scannerRequest, string input, string file, int lineNumber)
        {
            string[] crossSiteScripting = new string[] { "Alert()" };

            if (crossSiteScripting.All(input.Contains))
                scannerRequest.AddLog("Cross Site Scripting", file, lineNumber);
        }
    }
}
