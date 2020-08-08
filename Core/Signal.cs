using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Signal
    {
        public string StrategyName { get; set; }
        public bool IsBuySignal { get; set; }
        public short Etebar { get; set; }
        public string Description { get; set; }
    }
}
