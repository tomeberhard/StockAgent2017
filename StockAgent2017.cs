using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockAgent2017
{
    public partial class StockAgent2017 : Form
    {
        public StockAgent2017()
        {
            InitializeComponent();
        }

        private void RunScan200DMA_Click(object sender, EventArgs e)
        {
            ResultsTextBox.Text = "200 DMA Scan: to be implemented";
        }

    }
}
