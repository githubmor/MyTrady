using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trady.Core.Infrastructure;
using Trady.Analysis.Extension;
using Trady.Analysis;
using MyTradyExtension;

namespace Core.Strategys
{
    public class Sar_Ema : Strategy
    {
        public Sar_Ema(Namad namad) : base(namad)
        {
        }

        protected override Signal GetBearishSignal()
        {
            throw new NotImplementedException();
        }

        protected override Signal GetBullishSignal()
        {
            throw new NotImplementedException();
        }

        protected override Signal GetCurrentState()
        {
            throw new NotImplementedException();
        }
    }
}
