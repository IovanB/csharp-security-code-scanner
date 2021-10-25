using SecurityCodeScanner.Application.UseCase.Scanner;
using System;

namespace SecurityCodeScanner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a source code file path: ");
            string sourcePath = Console.ReadLine();

            var request = new ScannerRequest(sourcePath, "PLAIN TEXT");

            if (String.IsNullOrEmpty(sourcePath))
            {
                Console.WriteLine("File path cannot be null or empty.");
                return;
            }

            ScannerUseCase.ProcessRequest(request);
        }
    }
}
