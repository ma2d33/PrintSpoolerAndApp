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
            StringBuilder printInfo = new StringBuilder();
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
                        if (printers.Any<PrintObject>(a => a.JobId == Convert.ToInt32(manObj.Properties["JobId"].Value))) //Any checks if object exists
                        {

                        }
                        else
                        {

                        }

                        printInfo.AppendLine("Printer: " + manObj.Properties["Name"].Value.ToString());
                        printInfo.AppendLine("Document: " + manObj.Properties["Document"].Value.ToString());
                        printInfo.AppendLine("Id: " + manObj.Properties["JobId"].Value.ToString());
                        printInfo.AppendLine("TotalPages: " + manObj.Properties["TotalPages"].Value.ToString());

                        Console.Write(printInfo.ToString());
                        jobId = Convert.ToInt32(manObj.Properties["JobId"].Value);

                    }
                }

                //looking for jobs here , THEY TOOK OUR JAAAABS!!!
            }

        }
    }
}
