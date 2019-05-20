using System;
using System.Collections.Generic;
using System.Management;
using System.Drawing.Printing;

namespace PrinterLib
{
    public class Printer
    {
        public List<string> GetPrintersList()
        {
            List<string> printers = new List<string>();

            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                printers.Add(printer);
            }

            return printers;
        }

        public string GetPrintJobStatus(string printerName)
        {
            ObjectQuery wmiQuery = null;
            ManagementObjectSearcher objManagementobjectSearcher = null;
            ManagementObjectCollection objManagementObjectCollection = null;

            string Name = ""; // wmi print job is combination of "printerName , job number"
            string[] namesplit = null;
            string l_printerName = "";
            string l_jobNumber = "";
            string l_jobStatus = "";
            string l_status = "";

            string l_ReturnStatus = "";
            try
            {
                wmiQuery = new ObjectQuery("Select * from Win32_PrintJob");
                objManagementobjectSearcher = new ManagementObjectSearcher(wmiQuery);
                objManagementObjectCollection = objManagementobjectSearcher.Get();
                foreach (ManagementObject printJob in objManagementObjectCollection)
                {
                    Name = Convert.ToString(printJob["Name"]);
                    namesplit = Name.Split(',');
                    l_printerName = namesplit[0];
                    l_jobNumber = namesplit[1];
                    if (printerName != "")
                    {
                        if (l_printerName == printerName)
                        {
                            l_status = Convert.ToString(printJob["Status"]);

                            if (l_status.ToUpper().Equals("OK"))
                            {
                                l_jobStatus = Convert.ToString(printJob["JobStatus"]);

                                //switch (l_jobStatus)
                                //{
                                //    case PrintJobStatus.Blocked:
                                //        l_ReturnStatus = "Printer Job is Blocked";
                                //        break;

                                //    case PrintJobStatus.Completed:
                                //        l_ReturnStatus = "Printing is Completed";
                                //        break;

                                //    case PrintJobStatus.Deleted:
                                //        l_ReturnStatus = "Printer Job is Deleted";
                                //        break;

                                //    case PrintJobStatus.Deleting:
                                //        l_ReturnStatus = "Printer Job is Deleting";
                                //        break;

                                //    case PrintJobStatus.Error:
                                //        l_ReturnStatus = "Printer is on Error";
                                //        break;

                                //    case PrintJobStatus.Offline:
                                //        l_ReturnStatus = "Printer is on Offline";
                                //        break;

                                //    case PrintJobStatus.PaperOut:
                                //        l_ReturnStatus = "Printer is on PaperOut";
                                //        break;

                                //    case PrintJobStatus.Paused:
                                //        l_ReturnStatus = "Printer Job is Paused";
                                //        break;

                                //    case PrintJobStatus.Printed:
                                //        l_ReturnStatus = "Printing is Completed";
                                //        break;

                                //    case PrintJobStatus.Printing:
                                //        l_ReturnStatus = "Printing is Completed";
                                //        break;

                                //    case PrintJobStatus.Spooling:
                                //        l_ReturnStatus = "Printing is Completed";
                                //        break;

                                //    case PrintJobStatus.UserIntervention:
                                //        l_ReturnStatus = "Printing is on User Intervention";
                                //        break;
                                //}
                            }
                            else
                            {
                                l_ReturnStatus = "Can't Print.Error in Printer,Contact your Network Administrator.";
                            }
                        }

                    }
                    else
                    {
                        l_ReturnStatus = "No Default Printer is Selected.";
                    }

                }

                return l_ReturnStatus;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void PausePrintJob()
        {
            string searchQuery = "SELECT * FROM Win32_PrintJob";
            ManagementObjectSearcher searchPrintJobs = new ManagementObjectSearcher(searchQuery);
            ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();
            foreach (ManagementObject prntJob in prntJobCollection)
            {
                string Document = prntJob.Properties["Document"].Value.ToString();

                string JobId = prntJob.Properties["JobId"].Value.ToString();
                string name = prntJob.Properties["Name"].Value.ToString();
                string PagePrinted = prntJob.Properties["PagesPrinted"].Value.ToString();
                string Status = prntJob.Properties["Status"].Value.ToString();
                string Totalpages = prntJob.Properties["TotalPages"].Value.ToString();

                string[] row = new string[] { Document, JobId, name, PagePrinted, Status, Totalpages };
                bool present = false;
                int i = 0;

                //foreach (DataGridViewRow item in dataGridView1.Rows)
                //{
                //    if (Convert.ToString(item.Cells["JobId"].Value) == JobId)
                //    {
                //        present = true;
                //        break;
                //    }
                //    ++i;
                //}

                //if (present)
                //{
                //    dataGridView1.Rows.RemoveAt(i);

                //}

                //dataGridView1.Rows.Add(row);

            }

        }
    }
}
