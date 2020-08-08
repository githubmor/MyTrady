using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeData
{
    public class Repository : IRepository
    {
        public List<Core.Namad> GetNamadList(int period)
        {
            List<Core.Namad> result = new List<Core.Namad>();
            using (var db = new Context())
            {
                var r = db.Namads.Include("Moamelatss").ToList();

                foreach (var item in r)
                {
                    //TODO اینجا باید ایتم های معاملاتی تبدیل به نماد شوند
                    //List<IOhlcv> ohlcvs = new List<IOhlcv>();
                    //result.Add(new Core.Namad(item.Name,item.Moamelatss.))
                }
            }

            return result;
        }

        public void SaveNamads(List<Core.Namad> namads)
        {
            throw new NotImplementedException();
        }
    }
}
