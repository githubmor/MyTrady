﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    interface ITradeService
    {
        List<Namad> GetNamads(int period);

         void SaveNamads(List<Namad> namads);
    }
}
