using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trady.Core.Infrastructure;
using Trady.Analysis.Extension;
using Trady.Analysis;
using MyTradyExtension;

namespace Core.Strategy
{
    public class Sar_Ema : IStrategy
    {
        public List<StrategicPoint> GetStrategicPoints(List<IOhlcv> ohlcvs)
        {
            List<StrategicPoint> points = new List<StrategicPoint>();

            var rule = Rule.Create(c => c.IsAboveEma(50)).And(k => k.IsMacdBearishCross());

            // Use context here for caching indicator results
            using (var ctx = new AnalyzeContext(ohlcvs))
            {
                var validObjects = new SimpleRuleExecutor(ctx, rule).Execute();

                foreach (var item in validObjects)
                {
                    points.Add(new StrategicPoint() { IsSoudi =item.})
                    Console.WriteLine(item.GetPersianDate() + " - " + item.IsBullish());
                }

            }


        }
    }
}
