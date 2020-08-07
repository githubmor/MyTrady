using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trady.Core.Infrastructure;

namespace Core
{
    public interface IStrategy
    {
        List<StrategicPoint> GetStrategicPoints(List<IOhlcv> ohlcvs);
    }
}
