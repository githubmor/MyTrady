using Core.Strategys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Trader
    {
        List<Strategy> strategies = new List<Strategy>();
        public Trader(Namad namad)
        {
            strategies.Add(new Sar_Ema(namad));
        }
        public List<Signal> GetNamadSignal()
        {
            List<Signal> result = new List<Signal>();
            foreach (var item in strategies)
            {
                result.AddRange(item.GetSignals());
            }

            return result;
        }
    }
}
