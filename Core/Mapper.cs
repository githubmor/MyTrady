using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trady.Core.Infrastructure;

namespace Core
{
    public class Mapper
    {
        public Namad GetNamad(string name , List<Ohcv> ohcvs)
        {
            return new Namad(name, ohcvs.ToList<IOhlcv>());
        }

        public List<Ohcv> GetMoamelat(Namad named)
        {
            List<Ohcv> re = new List<Ohcv>();
            foreach (var item in named._ohlcvs)
            {
                re.Add(new Ohcv() { Close = item.Close, High = item.High, Low = item.Low, Open = item.Open, Volume = item.Volume });
            }
            return re;
        }
    }

    public class Ohcv : IOhlcv
    {
        public decimal Open { get; set; }
        public decimal High { get; set ; }
        public decimal Low { get; set ; }
        public decimal Close { get ; set; }
        public decimal Volume { get ; set; }

        public DateTimeOffset DateTime { get; }
    }
}
