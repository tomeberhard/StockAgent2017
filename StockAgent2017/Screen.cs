using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockAgent2017
{
    class Screen
    {
        private AllStocks allStocks;

        public int StocksAbove200DMA;
        public int StocksBelow200DMA;
        public int StocksCrossing200DMA;
        public int StocksCrossing50WhileUnder200;
        public int StocksCrossing50WhileAbove200;

        public int StocksBreachedThirtyWeekHigh;
        public int StocksBreachedThirtyWeekLow;

        public Screen(AllStocks Stocks)
        {
            StocksAbove200DMA = 0;
            StocksBelow200DMA = 0;

            StocksCrossing200DMA = 0;
            StocksCrossing50WhileUnder200 = 0;
            StocksCrossing50WhileAbove200 = 0;

            StocksBreachedThirtyWeekHigh = 0;
            StocksBreachedThirtyWeekLow = 0;

            allStocks = Stocks;
        }

        public string Run200Dma()
        {
            StringBuilder sb = new StringBuilder();

            StocksAbove200DMA = 0;
            StocksCrossing200DMA = 0;
            foreach (string key in allStocks.stocksDictionary.Keys)
            {
                Stock stock = allStocks.stocksDictionary[key];

                Recommendation rec = stock.runCross200();
                if (rec == Recommendation.Buy)
                {
                    StocksAbove200DMA++;
                    StocksCrossing200DMA++;
                    sb.AppendLine(stock.ticker + " crossed 200DMA. " + 
                        "(prev=" + stock.PricePrevious.ToString("#.00") + 
                        ", 200DMA=" + stock.MovingAverage200.ToString("#.00") + 
                        ", close=" + stock.PriceClose.ToString("#.00") + ")" );
                }
                else if (rec == Recommendation.Hold)
                {
                    StocksAbove200DMA++;
                }
                else
                {
                    StocksBelow200DMA++;
                }
            }

            sb.AppendLine("Number of Stocks above 200DMA: " + StocksAbove200DMA);
            sb.AppendLine("Number of Stocks below 200DMA: " + StocksBelow200DMA);
            sb.AppendLine("Number of Stocks Crossing 200DMA: " + StocksCrossing200DMA);

            string s = sb.ToString();
            return s;
        }

        public string RunCross50WhileUnder200()
        {
            StringBuilder sb = new StringBuilder();

            StocksCrossing50WhileUnder200 = 0;
            foreach (string key in allStocks.stocksDictionary.Keys)
            {
                Stock stock = allStocks.stocksDictionary[key];

                Recommendation rec = stock.runCross50WhileUnder200Screen();
                if (rec == Recommendation.Buy)
                {
                    StocksCrossing50WhileUnder200++;
                    sb.AppendLine(stock.ticker + " crossed 50 while under 200. " +
                        "(prev=" + stock.PricePrevious.ToString("#.00") +
                        ", 50DMA=" + stock.MovingAverage50.ToString("#.00") +
                        ", close=" + stock.PriceClose.ToString("#.00") +
                        ", 200DMA=" + stock.MovingAverage200.ToString("#.00") + ")");
                }
            }

            sb.AppendLine("Number of Stocks Crossing 50 While Under 200: " + StocksCrossing50WhileUnder200 );

            string s = sb.ToString();
            return s;
        }

        public string RunCross50WhileAbove200()
        {
            StringBuilder sb = new StringBuilder();

            StocksCrossing50WhileAbove200 = 0;
            foreach (string key in allStocks.stocksDictionary.Keys)
            {
                Stock stock = allStocks.stocksDictionary[key];

                Recommendation rec = stock.runCross50WhileAbove200Screen();
                if (rec == Recommendation.Buy)
                {
                    StocksCrossing50WhileAbove200++;
                    sb.AppendLine(stock.ticker + " crossed 50 while above 200. " +
                        "(prev=" + stock.PricePrevious.ToString("#.00") +
                        ", 50DMA=" + stock.MovingAverage50.ToString("#.00") +
                        ", close=" + stock.PriceClose.ToString("#.00") +
                        ", 200DMA=" + stock.MovingAverage200.ToString("#.00") + ")");
                }
            }

            sb.AppendLine("Number of Stocks Crossing 50 While Above 200: " + StocksCrossing50WhileAbove200);

            string s = sb.ToString();
            return s;
        }

        public string RunBreachThirtyWeekHigh()
        {
            StringBuilder sb = new StringBuilder();

            StocksBreachedThirtyWeekHigh = 0;
            foreach (string key in allStocks.stocksDictionary.Keys)
            {
                Stock stock = allStocks.stocksDictionary[key];

                Recommendation rec = stock.runScreenBreachThirthyWeekHigh();
                if (rec == Recommendation.Buy)
                {
                    StocksBreachedThirtyWeekHigh++;
                    sb.AppendLine(stock.ticker + " breached 30 week high. " +
                        "(close=" + stock.PriceClose.ToString("#.00") +
                        ", 30WeekHigh=" + stock.ThirtyWeekHigh.ToString("#.00") + ")");
                }
            }

            sb.AppendLine("Number of Stocks Breached 30 Week High: " + StocksBreachedThirtyWeekHigh);

            string s = sb.ToString();
            return s;

        }
        public string RunBreachThirtyWeekLow()
        {
            StringBuilder sb = new StringBuilder();

            StocksBreachedThirtyWeekLow = 0;
            foreach (string key in allStocks.stocksDictionary.Keys)
            {
                Stock stock = allStocks.stocksDictionary[key];

                Recommendation rec = stock.runScreenBreachThirthyWeekLow();
                if (rec == Recommendation.Sell)
                {
                    StocksBreachedThirtyWeekLow++;
                    if ((decimal)5.00 < stock.PriceClose)
                    {
                        sb.AppendLine(stock.ticker + " breached 30 week low. " +
                            "(close=" + stock.PriceClose.ToString("#.00") +
                            ", 30WeekLow=" + stock.ThirtyWeekLow.ToString("#.00") + ")");
                    }
                }
            }

            sb.AppendLine("Number of Stocks Breached 30 Week Low (adjusted): " + StocksBreachedThirtyWeekLow);

            string s = sb.ToString();
            return s;

        }
        
    }
}

