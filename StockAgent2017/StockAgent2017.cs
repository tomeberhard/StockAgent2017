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
using System.IO;

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

        private void RunAllScreensButton_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            AllStocks allStocks = new AllStocks();

#warning TODO: Warn if we don't have the latest data
            //if (! GotLatestData(files))
            //{
            //    MessageBox.Show("Today's 
            //    ;
            //}

            //List<FileInfo> files = GetFileList(@"C:\Users\Tom\Documents\Tom\Investing\eoddata\TestData");
            //List<FileInfo> files = GetFileList(@"C:\Users\Tom\Documents\Tom\Investing\eoddata\Test-AMEX-250");

            List<FileInfo> files = new List<FileInfo>();
            // more data should be here: @"C:\Users\Tom\Documents\Tom\Investing\eoddata\AllData\AMEX"));
            files.AddRange(FileIO.GetFileList(@"C:\Users\Tom\Downloads\StockData\TEST_all"));
            //files.AddRange(FileIO.GetFileList(@"C:\Users\Tom\Downloads\StockData\AMEX_all"));
            //files.AddRange(FileIO.GetFileList(@"C:\Users\Tom\Downloads\StockData\NASDAQ_all"));
            //files.AddRange(FileIO.GetFileList(@"C:\Users\Tom\Downloads\StockData\NYSE_all"));

            // gobble up all the files into allStocks, and gobble up all the data for each stock into memory.
            foreach (FileInfo fileInfo in files)
            {
                if (!fileInfo.Name.Contains("SymbolList"))
                {
                    ArrayList lines = FileIO.LoadLines(fileInfo.FullName);      //@"C:\Users\Tom\Documents\Tom\Investing\eoddata\AMEX_20110711.txt");

                    foreach (string line in lines)
                    {
                        EndOfDayData eodd = new EndOfDayData(line);  //"ACU,20110711,9.67,9.78,9.66,9.71,3800");
                        allStocks.Add(eodd);
                    }
                }
            }

            ResultsTextBox.AppendText(allStocks.RemoveBadStocks());
            allStocks.ComputeStatistics();

            ResultsTextBox.AppendText(allStocks.Summary());
            //ResultsTextBox.AppendText(allStocks.Summarize("MSFT") + "\n");

            Screen screen = new Screen(allStocks);
            ResultsTextBox.AppendText("\n=================================\n");
            ResultsTextBox.AppendText(screen.Run200Dma());
            ResultsTextBox.AppendText("\n=================================\n");
            ResultsTextBox.AppendText(screen.RunCross50WhileUnder200());
            ResultsTextBox.AppendText("\n=================================\n");
            ResultsTextBox.AppendText(screen.RunCross50WhileAbove200());
            ResultsTextBox.AppendText("\n=================================\n");
            ResultsTextBox.AppendText(screen.RunBreachThirtyWeekHigh());
            ResultsTextBox.AppendText("\n=================================\n");
            ResultsTextBox.AppendText(screen.RunBreachThirtyWeekLow());
        }
    }
}
