using SecurityCodeScanner.Application.UseCase.Scanner;
using System;

namespace SecurityCodeScanner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a source code file path: ");
            string sourcePath = Console.ReadLine();

            var request = new ScannerRequest(sourcePath, "text");

            ScannerUseCase.ProcessRequest(request);
        }
    }
}
