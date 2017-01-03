using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAgent2017
{
    public struct Quote
    {
        public decimal price;
        public DateTime date;
        public Int64 volume;
    }

    public enum Recommendation
    {
        Buy, Sell, Hold
    }

    class Stock
    {
        private const int RANGE = 500;

        public string ticker;
        public Quote[] quote;
        private int index;

        public decimal MovingAverage50;
        public decimal MovingAverage200;

        public decimal ThirtyWeekHigh;
        public decimal ThirtyWeekLow;

        public decimal PricePrevious
        {
            get { return quote[index - 2].price; }
        }
        public decimal PriceClose
        {
            get { return quote[index - 1].price; }
        }


        public Stock(EndOfDayData eodd)
        {
            ticker = eodd.ticker;
            quote = new Quote[RANGE];

            quote[0].price = eodd.close;
            quote[0].date = eodd.date;
            index = 1;
        }


        public Stock(string Ticker)
        {
            ticker = Ticker;
            quote = new Quote[RANGE];

            MovingAverage200 = 0;
            MovingAverage50 = 0;

        }

        // Retruns true if the stock has sufficient and decent data in it. (a very basic test)
        public bool isLegit()
        {
            if (index == 0)
            {
                return false;
            }
            if (quote[index - 1].volume < 100000)
            {
                return false;
            }
            if (index < 10)
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(ticker);
            sb.Append(", 200DMA=" + MovingAverage200.ToString("#.00"));
            sb.Append(", 50DMA=" + MovingAverage50.ToString("#.00"));

            string s = sb.ToString();
            return s;
        }

        public void Add(EndOfDayData eodd)
        {
            if (index < RANGE)
            {
                quote[index].price = eodd.close;
                quote[index].date = eodd.date;
                quote[index].volume = eodd.volume;
                index++;
            }
            else
            {
                Exception ex = new Exception("Too much data, increase Range or delete old EODD files.");
                // todo: complain
            }
        }

        public void FillPrices()
        {
            for (int i = 0; i < RANGE; i++)
            {
                quote[i].price = i;
            }
        }

        private decimal ComputeMovingAverage(int days)
        {
            decimal total = 0;
            decimal average = 0;

            if (0 < days)
            {
                if (index < days)
                {
                    days = index - 1;
                }

                for (int i = index - 1; (index - 1 - days) < i; i--)
                {
                    total += quote[i].price;
                }

                average = total / days;
            }

            return average;
        }

        /// <summary>
        /// Returns the min and max closing price of the past X days
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        private decimal[] ComputeXDaysMinMax(int days)
        {
            decimal[] minmax = new decimal[2];

            if (index < days)
            {
                days = index - 1;
            }

            minmax[0] = quote[index - 1].price;
            minmax[1] = quote[index - 1].price;

            for (int i = index - 1; (index - 1 - days) < i; i--)
            {
                if (minmax[1] < quote[i].price)
                {
                    //update max
                    minmax[1] = quote[i].price;
                }
                else if (quote[i].price < minmax[0])
                {
                    //update min
                    minmax[0] = quote[i].price;
                }
            }
            return minmax;
        }

        /// <summary>
        ///  Computes the 50 and 200 day moving averages. Computes the 30 week high and low.
        /// </summary>
        public void ComputeStatistics()
        {
            decimal[] minmax;

            //SortQuotes();

            if (ticker == "MITK")
            {
                MovingAverage50 = ComputeMovingAverage(50);
            }

            MovingAverage50 = ComputeMovingAverage(50);
            MovingAverage200 = ComputeMovingAverage(200);

            minmax = ComputeXDaysMinMax(30 * 5);
            ThirtyWeekLow = minmax[0];
            ThirtyWeekHigh = minmax[1];
        }

        public void SortQuotes()
        {
            Array.Sort(quote, delegate (Quote q1, Quote q2) { return q1.date.CompareTo(q2.date); });
        }

        public string Prices()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < index; i++)
            {
                sb.AppendLine(quote[i].date.ToString("yyyy/MM/dd") + " " +
                    quote[i].price.ToString("#.00"));
            }
            string s = sb.ToString();
            return s;
        }

        /// <summary>
        /// Figures out if we crossed the 200DMA upwards
        /// </summary>
        /// <returns></returns>
        public Recommendation runCross200()
        {
            decimal priceClosing = quote[index - 1].price;
            decimal pricePrevious = quote[index - 2].price;

            // check if positive crossing of 200DMA (buy)
            if ((MovingAverage200 <= priceClosing) &&
                (pricePrevious < MovingAverage200))
            {
                return Recommendation.Buy;
            }

            // check if negative crossing of the 200DMA (sell)
            if ((priceClosing < MovingAverage200) &&
                (MovingAverage200 <= pricePrevious))
            {
                return Recommendation.Sell;
            }

            // check if still above 200DMA (hold)
            if (MovingAverage200 <= priceClosing)
            {
                return Recommendation.Hold;
            }

            return Recommendation.Sell;
        }

        public Recommendation runCross50WhileUnder200Screen()
        {
            decimal priceClosing = quote[index - 1].price;
            decimal pricePrevious = quote[index - 2].price;

            if (MovingAverage50 < MovingAverage200)
            {
                if ((pricePrevious < MovingAverage50) &&
                    (MovingAverage50 < priceClosing))
                {
                    return Recommendation.Buy;
                }
            }

            return Recommendation.Sell;
        }

        public Recommendation runCross50WhileAbove200Screen()
        {
            decimal priceClosing = quote[index - 1].price;
            decimal pricePrevious = quote[index - 2].price;

            if (MovingAverage200 < MovingAverage50)
            {
                if ((MovingAverage50 < pricePrevious) &&
                    (priceClosing < MovingAverage50))
                {
                    return Recommendation.Buy;
                }
            }

            return Recommendation.Sell;
        }

        public Recommendation runScreenBreachThirthyWeekHigh()
        {
            decimal priceClosing = quote[index - 1].price;
            decimal priceYesterday = quote[index - 2].price;

            if ((priceYesterday < ThirtyWeekHigh) &&
                (ThirtyWeekHigh <= priceClosing))
            {
                return Recommendation.Buy;
            }

            return Recommendation.Sell;
        }

        /// <summary>
        /// Returns a sell (short) recommendation if the stock price is lower than the 30 week low
        /// </summary>
        /// <returns></returns>
        public Recommendation runScreenBreachThirthyWeekLow()
        {
            decimal priceClosing = quote[index - 1].price;
            decimal priceYesterday = quote[index - 2].price;

            if ((priceYesterday > ThirtyWeekLow) &&
                (priceClosing <= ThirtyWeekLow))
            {
                return Recommendation.Sell;
            }

            return Recommendation.Hold;
        }

    }
}
