using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrinterLib.Test
{
    public partial class Form1 : Form
    {
        public Printer prt = new Printer();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<string> printers = prt.GetPrintersList();

            foreach (string printer in printers)
            {
                cbPrinter.Items.Add(printer);
            }
        }

        private void CbPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPrinter.SelectedIndex > -1)
            {
                txtStatus.Text = "Status: " + prt.GetPrinterInfo(cbPrinter.SelectedItem.ToString());
            }
        }
    }
}
