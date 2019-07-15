using System;
using System.Linq;
using System.Management;
using System.Printing;


namespace PrintSpoolerAndApp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool loopIt = true;
            string printInfo = "";
            int jobId = 0;

           string searchQuery = "SELECT * FROM Win32_PrintJob";
           ManagementObjectSearcher searchPrintJobs = new ManagementObjectSearcher(searchQuery);
           


            while (loopIt == true)
            {
                ManagementObjectCollection printJobCollection = searchPrintJobs.Get();
                foreach (ManagementObject manObj in printJobCollection)
                {
                    if (printJobCollection.Count != 0 && Convert.ToInt32(manObj.Properties["TotalPages"].Value) != 0 ) 
                    {
                        if (jobId != Convert.ToInt32(manObj.Properties["JobId"].Value))
                        {
                            printInfo += "\n" + "Printer: " + manObj.Properties["Name"].Value.ToString();
                            printInfo += "\n" + "Document: " + manObj.Properties["Document"].Value.ToString();
                            printInfo += "\n" + "Id: " + manObj.Properties["JobId"].Value.ToString();
                            printInfo += "\n" + "TotalPages: " + manObj.Properties["TotalPages"].Value.ToString();

                            Console.Write(printInfo);
                            jobId = Convert.ToInt32(manObj.Properties["JobId"].Value);
                        }
                    }
                }

                //looking for jobs here , THEY TOOK OUR JAAAABS!!!
            }

        }
    }
}
