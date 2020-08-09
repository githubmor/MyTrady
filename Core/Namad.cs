using System;
using System.Collections.Generic;
using Trady.Core.Infrastructure;

namespace Core
{
    public class Namad
    {
        public Namad(string name, List<IOhlcv> ohlcvs)
        {
            Name = name;
            _ohlcvs = ohlcvs;
        }
        public string Name { get; set; }

        public List<IOhlcv> _ohlcvs { get; set; }

    }

    
}