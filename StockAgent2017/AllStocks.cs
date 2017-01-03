using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;

namespace StockAgent2017
{
    class AllStocks
    {
        public Dictionary<String, Stock> stocksDictionary;

        public AllStocks()
        {
            stocksDictionary = new Dictionary<string,Stock>();
        }

        public void Add(EndOfDayData eodd)
        {
            if (eodd.ticker != null)
            {
                if (stocksDictionary.ContainsKey(eodd.ticker))
                {
                    stocksDictionary[eodd.ticker].Add(eodd);
                }
                else
                {
                    Stock stock = new Stock(eodd.ticker);
                    stocksDictionary.Add(eodd.ticker, stock);
                }
            }
        }

        /// <summary>
        /// Prints out the number of stocks scanned into the Dictionary, and all the tickers.
        /// </summary>
        /// <returns></returns>
        public string Summary()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Number of Stocks (Keys): " + stocksDictionary.Keys.Count.ToString());

            /*
            foreach (string key in stocksDictionary.Keys)
            {
                sb.AppendLine(key);
                //" " + stocksDictionary[key]);
            }
             */

            string s = sb.ToString();
            return s;

        }

        public string Summarize(string ticker)
        {
            StringBuilder sb = new StringBuilder();

            if ( stocksDictionary.ContainsKey(ticker) )
            {
                Stock stock = stocksDictionary[ticker];

                sb.AppendLine(stock.ToString());
                sb.AppendLine(stock.Prices());
                stock.ComputeStatistics();
                sb.AppendLine("50DMA: " + stock.MovingAverage50.ToString("#.00"));
                sb.AppendLine("200DMA: " + stock.MovingAverage200.ToString("#.00"));
            }
            else
            {
                sb.AppendLine("No such ticker in stocksDictionary. (" + ticker + ")");
            }

            string s = sb.ToString();
            return s;
        }

        public string RemoveBadStocks()
        {
            StringBuilder sb = new StringBuilder();
            int stocksRemoved = 0;
            List<String> keysToRemove = new List<String>();
            
            // find the stocks to remove
            foreach (string key in stocksDictionary.Keys)
            {
                if (!stocksDictionary[key].isLegit())
                {
                    keysToRemove.Add(key);
                }
            }

            // remove the stocks to remove
            foreach (string key in keysToRemove)
            {
                stocksDictionary.Remove(key);
                stocksRemoved++;

#warning add verbose logging
                //sb.AppendLine("Removed " + key);
            }

            sb.AppendLine("Total Stocks Removed: " + stocksRemoved);
            string s = sb.ToString();
            return s;
        }

        public void ComputeStatistics()
        {
#warning Do this in parallel
            foreach (string key in stocksDictionary.Keys)
            {
                stocksDictionary[key].ComputeStatistics();
            }
        }

        /// <summary>
        /// Puts all the tickers to ignore into a list. 
        /// Includes certain ETFs, mutual funds, etc.
        /// </summary>
        /// <returns></returns>
        public List<String> IgnoreList()
        {

            List<String> ignore = new List<String>();
            ignore.Add("DXD");  // ProShares UltraShort Dow30 (DXD)
            ignore.Add("ERY");  // Direxion Daily Energy Bear 3X Shares (ERY)
            ignore.Add("TWM");  // ProShares UltraShort Russell2000 (TWM)
            ignore.Add("TZA");  //  Direxion Daily Small Cap Bear 3X Shares (TZA)


            return ignore;
        }

    }
}
