using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintSpoolerAndApp
{
   public class PrintObject
    {
        public string DocumentName = "";
        public string PrinterName = "";
        public int JobId = 0;
        public int TotalPages = 0;

        public string GetInfoString()
        {
            StringBuilder printInfo = new StringBuilder();

            printInfo.AppendLine("Printer: " + this.PrinterName);
            printInfo.AppendLine("Document: " + this.DocumentName);
            printInfo.AppendLine("Id: " + this.JobId);
            printInfo.AppendLine("TotalPages: " + this.TotalPages);

            return printInfo.ToString();
        }

    }
}
