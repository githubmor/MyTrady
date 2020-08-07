using System;
using System.Linq;
using System.Collections.Generic;

namespace Core
{
    internal class SignalTrade
    {
        private List<StrategicPoint> s;

        public SignalTrade(List<StrategicPoint> s)
        {
            this.s = s;
        }

        internal Signal GetSignal()
        {
            Signal signal = new Signal();

            var BullSumPoint = s.Where(p => p.IsSoudi).Sum(p => p.TenPoint);
            var BearSumPoint = s.Where(p=>!p.IsSoudi).Sum(p => p.TenPoint);

            var BullCount = s.Count(p => p.IsSoudi);
            var BearCount = s.Count(p => !p.IsSoudi);

            string se = "";
            signal.IsBuySignal = BullSumPoint > BearSumPoint;
            if (BullSumPoint > BearSumPoint)
            {
                signal.Etebar = (short)( BullSumPoint * 100 / (BullCount * 10));
                s.Where(p => p.IsSoudi).ToList().ForEach(p => se = se + p.Tozihat);

                signal.Description = se;
            }
            else
            {
                signal.Etebar = (short)(BearSumPoint * 100 / (BearCount * 10));
                s.Where(p => !p.IsSoudi).ToList().ForEach(p => se = se + p.Tozihat);

                signal.Description = se;
            }

            return signal;
        }
    }
}