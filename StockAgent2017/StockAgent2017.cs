using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Collections;

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
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("200 DMA Scan: to be implemented");

            string dataFolder = DataFolderTextBox.Text;

            ArrayList fileNames = FileIO.GetLatestFiles(dataFolder, 200);

            for (int i=0; i<20;i++ )
            {
                sb.AppendLine(fileNames[i].ToString());
            }


            ResultsTextBox.Text = sb.ToString();
        }

    }
}
