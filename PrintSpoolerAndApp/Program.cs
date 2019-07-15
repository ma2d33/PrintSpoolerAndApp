using PrinterLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;


namespace PrintSpoolerAndApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            //Printer prt = new Printer();
            //List<string> printers = prt.GetPrintersList();
            //short Index = 0;

            
;
            // Create a local print server
            LocalPrintServer ps = new LocalPrintServer();

            // List the print server's queues
            PrintQueueCollection myPrintQueues = ps.GetPrintQueues();
           
            String printQueueNames = "My Print Queues:\n\n";


            foreach (PrintQueue pq in myPrintQueues)
            {
                pq.Refresh();
                printQueueNames += "\t" + pq.Name + "\n";

                PrintJobInfoCollection jobs = pq.GetPrintJobInfoCollection();

                bool hasJobs = jobs.Any();

                

                foreach (PrintSystemJobInfo job in jobs)
                {
                    // Since the user may not be able to articulate which job is problematic,
                    // present information about each job the user has submitted.
                    
                }
            }
                Console.WriteLine(printQueueNames);
                // Console.WriteLine(PrintSystemJobInfo.Get(myPrintQueues,1));
                Console.WriteLine("\nPress Return to continue.");


                //foreach (string printer in printers)
                //{
                //    Console.WriteLine(Index.ToString() + " = " + printer);
                //    Index++;
                //}

                //Console.WriteLine(Environment.NewLine);
                //Console.WriteLine("Enter Printer Index:");
                //short printerIndex = Convert.ToInt16(Console.ReadLine());
                //string selectedPrinter = printers[printerIndex];

                //Console.WriteLine(prt.GetPrintJobStatus(selectedPrinter));
                //Console.WriteLine();

                //Console.WriteLine("Press any key to exit...");
            
            Console.ReadLine();
        }
    }
}
