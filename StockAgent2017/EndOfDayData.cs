using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAgent2017
{
    class EndOfDayData
    {
        public string ticker;
        public DateTime date;
        public Decimal close;
        public int volume;

        //ACU,20110711,9.67,9.78,9.66,9.71,3800

        public EndOfDayData(string line)
        {
            ParseLine(line);
        }

        public void ParseLine(string line)
        {
            if (line != "")
            {
                string[] token = line.Split(',');

                ticker = token[0];
                date = ToDateTime(token[1]);
                close = Decimal.Parse(token[5]);
                volume = int.Parse(token[6]);
            }
        }

        public override string ToString()
        {
            StringBuilder b = new StringBuilder();
            b.Append(ticker + ", ");
            b.Append(date.ToString("yyyy/MM/dd") + ", ");
            b.Append(close.ToString() + ", ");
            b.Append(volume.ToString());

            string s = b.ToString();
            return s;
        }

        private DateTime ToDateTime(string s)
        {
            //parses this: 20110711
            int year = int.Parse(s.Substring(0, 4));
            int month = int.Parse(s.Substring(4, 2));
            int day = int.Parse(s.Substring(6, 2));

            DateTime dt = new DateTime(year, month, day);
            return dt;
        }
    }
}
