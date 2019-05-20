using PrinterLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintSpoolerAndApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            Printer prt = new Printer();
            List<string> printers = prt.GetPrintersList();
            short Index = 0;

            foreach (string printer in printers)
            {
                Console.WriteLine(Index.ToString() + " = " + printer);
                Index++;
            }

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Enter Printer Index:");
            short printerIndex = Convert.ToInt16(Console.ReadLine());
            string selectedPrinter = printers[printerIndex];

            Console.WriteLine(prt.GetPrintJobStatus(selectedPrinter));

            Console.WriteLine("Press ENTER to exit...");
            Console.ReadLine();
        }
    }
}
