using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trady.Core.Infrastructure;

namespace Core
{
    public abstract class Strategy
    {
        Namad _namad;
        public Strategy(Namad namad)
        {
            _namad = namad;
        }
        public List<Signal> GetSignals()
        {
            return new List<Signal>() { GetBearishSignal(), GetBullishSignal(), GetCurrentState() };
        }
        protected abstract Signal GetBullishSignal();
        protected abstract Signal GetBearishSignal();
        protected abstract Signal GetCurrentState();
    }
}
