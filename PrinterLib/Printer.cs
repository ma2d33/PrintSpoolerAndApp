using System;
using System.Collections.Generic;
using System.Management;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace PrinterLib
{
    public enum PrinterStatus : ushort
    {
        Other = 1,
        Unknown = 2,
        Idle = 3,
        Printing = 4,
        Warming_Up = 5,
        Stopped_printing = 6,
        Offline = 7
    }

    public class Printer
    {
        private T ObjectToPrintStatus<T>(object o)
        {
            T enumVal = (T)Enum.Parse(typeof(T), o.ToString());
            return enumVal;
        }

        public List<string> GetPrintersList()
        {
            List<string> printers = new List<string>();

            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                printers.Add(printer);
            }

            return printers;
        }

        public string GetPrinterInfo(string printerName)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_Printer WHERE Name = '" + printerName + "'");

            foreach (ManagementObject mo in searcher.Get())
            {
                UInt16 status = Convert.ToUInt16(mo.GetPropertyValue("PrinterStatus"));
                return Enum.GetName(typeof(PrinterStatus), status).Replace("_", " ");
            }

            return "Unknown";
        }

        public string GetPrintJobStatus(string printerName)
        {
            string l_ReturnStatus = "Status: Unknown";

            try
            {
                string query = string.Format("SELECT * from Win32_Printer WHERE Name = '{0}'", printerName);
                ObjectQuery wmiQuery = new ObjectQuery(query);// ("Select * from Win32_PrintJob");
                ManagementObjectSearcher objManagementobjectSearcher = new ManagementObjectSearcher(wmiQuery);
                ManagementObjectCollection objManagementObjectCollection = objManagementobjectSearcher.Get();

                foreach (ManagementBaseObject printer in objManagementObjectCollection)
                {
                    string Name = Convert.ToString(printer["Name"]); // wmi print job is combination of "printerName , job number"
                    string[] namesplit = Name.Split(',');
                    string l_printerName = namesplit[0];
                    //string l_jobNumber = namesplit[1];

                    if (printerName != "")
                    {
                        if (l_printerName == printerName)
                        {
                            //string Status = printer.Properties["Status"].Value.ToString();
                            PrinterStatus status = ObjectToPrintStatus<PrinterStatus>(printer.Properties["PrinterStatus"].Value);
                            string extendedPrinterStatus = printer.Properties["ExtendedPrinterStatus"].Value.ToString();

                            if (status == PrinterStatus.Idle)
                            {
                                string l_jobStatus = Convert.ToString(printer["JobStatus"]);

                                switch (l_jobStatus)
                                {
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
                                }
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

        /*
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

        */
        /*

        /// <summary>
        /// check printer is online
        /// </summary>
        private static bool IsOnline(ManagementBaseObject printer)
        {
            bool isOnlineprinter = true;
            PrinterNative.PrinterNative.PrinterNative printerNative = new PrinterNative.PrinterNative.PrinterNative();
            var PrinterName = printerNative.GetPrinterName();
            var PrinterNameProperty = printer.Properties["DeviceId"].Value.ToString();
            var ResultPrinter01 = printer.Properties["ExtendedPrinterStatus"].Value.ToString();
            var ResultPrinter02 = printer.Properties["PrinterState"].Value.ToString();

            if (PrinterNameProperty == PrinterName)
            {
                //(no internet connection or printer switched off):PrinterState
                if (ResultPrinter02 == "128" || ResultPrinter02 == "4096")
                {
                    isOnlineprinter = false;
                }



                ////printer is initializing....
                //if (ResultPrinter02 == "16")
                //{
                //    isOnlineprinter = false;
                //}

                //(no internet connection or printer switched off):ExtendedPrinterStatus
                if (ResultPrinter01 == "7")
                {
                    isOnlineprinter = false;
                }

            }

            return isOnlineprinter;
        }




        /// <summary>
        /// check for out of paper
        /// </summary>
        private static bool IspaperOK(ManagementBaseObject printer)
        {
            bool PaperOK = true;

            PrinterNative.PrinterNative.PrinterNative printerNative = new PrinterNative.PrinterNative.PrinterNative();
            var PrinterName = printerNative.GetPrinterName();
            var PrinterNameProperty = printer.Properties["DeviceId"].Value.ToString();

            var PaperStatus = printer.Properties["PrinterState"].Value.ToString();

            if (PrinterNameProperty == PrinterName)
            {
                //(PrinterState)16 = Out of Paper
                //(PrinterState)5 = Out of paper
                //(PrinterState)4 = paperjam
                //(PrinterState)144 = Out of paper
                if ((PaperStatus == "5") || (PaperStatus == "16") || (PaperStatus == "144"))
                {
                    PaperOK = false;
                }

            }

            return PaperOK;
        }



        /// <summary>
        /// Verify still printing state or not
        /// </summary>
        private static bool Isprinting(ManagementBaseObject printer)
        {
            bool Isprintingnow = false;
            PrinterNative.PrinterNative.PrinterNative printerNative = new PrinterNative.PrinterNative.PrinterNative();
            var PrinterName = printerNative.GetPrinterName();
            var PrinterNameProperty = printer.Properties["DeviceId"].Value.ToString();
            var printing01 = printer.Properties["PrinterState"].Value.ToString();
            var printing02 = printer.Properties["PrinterStatus"].Value.ToString();
            if (PrinterNameProperty == PrinterName)
            {
                //(PrinterState)11 = Printing
                //(PrinterState)1024 = printing
                //(PrinterStatus)4 = printing
                if (printing01 == "11" || printing01 == "1024" || printing02 == "4")
                {
                    Isprintingnow = true;
                }

            }

            return Isprintingnow;
        }

        /// <summary>
        /// check for error (Printer)
        /// </summary>
        private static bool IsPrinterError(ManagementBaseObject printer)
        {
            bool PrinterOK = true;

            PrinterNative.PrinterNative.PrinterNative printerNative = new PrinterNative.PrinterNative.PrinterNative();
            var PrinterName = printerNative.GetPrinterName();
            var PrinterNameProperty = printer.Properties["DeviceId"].Value.ToString();

            var PrinterSpecificError = printer.Properties["PrinterState"].Value.ToString();
            var otherError = printer.Properties["ExtendedPrinterStatus"].Value.ToString();
            if (PrinterNameProperty == PrinterName)
            {
                //(PrinterState)2 - error of printer
                //(PrinterState)131072 - Toner Low
                //(PrinterState)18 - Toner Low
                //(PrinterState)19 - No Toner

                if ((PrinterSpecificError == "131072") || (PrinterSpecificError == "18") || (PrinterSpecificError == "19") || (PrinterSpecificError == "2") || (PrinterSpecificError == "7"))
                {
                    PrinterOK = false;
                }

                //(ExtendedPrinterStatus) 2 - no error
                if (otherError == "2")
                {
                    PrinterOK = true;
                }
                else
                {
                    PrinterOK = false;
                }

            }
            return PrinterOK;
        }

        /// <summary>
        /// check Network or USB
        /// </summary>
        private static bool IsNetworkPrinter(ManagementBaseObject printer)
        {
            bool IsNetwork = true;
            PrinterNative.PrinterNative.PrinterNative printerNative = new PrinterNative.PrinterNative.PrinterNative();
            var PrinterName = printerNative.GetPrinterName();
            var PrinterNameProperty = printer.Properties["DeviceId"].Value.ToString();

            var network = printer.Properties["Network"].Value.ToString();
            var local = printer.Properties["Local"].Value.ToString();

            if (PrinterNameProperty == PrinterName)
            {
                if (network == "True")
                {
                    IsNetwork = true;
                }

                if (network == "True" && local == "True")
                {
                    IsNetwork = true;
                }

                if (local == "True" && network == "False")
                {
                    IsNetwork = false;
                }
            }

            return IsNetwork;
        }
        */

    }
}
