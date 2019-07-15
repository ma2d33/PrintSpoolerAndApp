using System;
using System.Linq;
using System.Management;
using System.Printing;
using System.Text;
using System.Collections.Generic;

namespace PrintSpoolerAndApp
{
    static class Program
    {
        static void Main(string[] args)
        {
            bool loopIt = true;
            int jobId = 0;

            List<PrintObject> printers = new List<PrintObject>();

            string searchQuery = "SELECT * FROM Win32_PrintJob";
            ManagementObjectSearcher searchPrintJobs = new ManagementObjectSearcher(searchQuery);

            while (loopIt)
            {
                ManagementObjectCollection printJobCollection = searchPrintJobs.Get();

                foreach (ManagementObject manObj in printJobCollection.OfType<ManagementObject>())
                {
                    if (printJobCollection.Count != 0 && Convert.ToInt32(manObj.Properties["TotalPages"].Value) != 0 && jobId != Convert.ToInt32(manObj.Properties["JobId"].Value))
                    {
                        PrintObject updateInfo = new PrintObject
                        {
                            JobId = Convert.ToInt32(manObj.Properties["JobId"].Value),
                            PrinterName = manObj.Properties["Name"].Value.ToString(),
                            DocumentName = manObj.Properties["Document"].Value.ToString(),
                            TotalPages = Convert.ToInt32(manObj.Properties["TotalPages"].Value),
                        };

                        if (printers.Any<PrintObject>(a => a.JobId == updateInfo.JobId))
                        {
                            printers.Insert(printers.FindIndex(a => a.JobId.Equals(updateInfo.JobId)), updateInfo);
                        }
                        else
                        {
                            printers.Add(updateInfo);
                            
                        }
                        Console.Clear();
                        Console.WriteLine(updateInfo.GetInfoString());

                    }
                }

                /*
                foreach (PrintObject printer in printers)
                {
                    Console.WriteLine(printer.GetInfoString());
                }
                */

                //looking for jobs here , THEY TOOK OUR JAAAABS!!!
            }

        }
    }
}
