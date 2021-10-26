using SecurityCodeScanner.Application.UseCase.Scanner;
using System.Linq;
using System.Text.RegularExpressions;

namespace SecurityCodeScanner.Application.Service
{
    public static class SecurityCheck
    {
        public static void SecurityCheckSteps(ScannerRequest scannerRequest, string input, string file, int linenumber)
        {
            FindSensitiveData(scannerRequest, input, file, linenumber);

            FindSQLInjection(scannerRequest, input, file, linenumber);

            FindSiteScripting(scannerRequest, input, file, linenumber);

        }

        public static bool FindSensitiveData(ScannerRequest scannerRequest, string input, string file, int linenumber)
        {
            string[] findSensitiveData = new string[] { "Checkmarx", "Hellman & Friedman", "$1.15b" };

            if (findSensitiveData.All(input.Contains))
                scannerRequest.AddLog("Sensitive Data Exposure", file, linenumber);

            return false;
        }

        public static bool FindSQLInjection(ScannerRequest scannerRequest, string input, string file, int linenumber)
        {
            var pattern = "^\"(SELECT|Select|select)\\b.*\\b(WHERE|Where|where)\\b\\s.*(%s)\"$";

            if (Regex.IsMatch(input, pattern))
                scannerRequest.AddLog("SQL Injection", file, linenumber);

            return false;
        }

        public static bool FindSiteScripting(ScannerRequest scannerRequest, string input, string file, int linenumber)
        {
            string[] crossSiteScripting = new string[] { "Alert()" };

            if (crossSiteScripting.All(input.Contains))
                scannerRequest.AddLog("Cross Site Scripting", file, linenumber);

            return false;

        }
    }
}
