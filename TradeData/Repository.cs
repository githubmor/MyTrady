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
                    List<Ohcv> ohlcvs = new List<Ohcv>();
                    foreach (var i in item.Moamelatss)
                    {
                        ohlcvs.Add(new Ohcv() { Close = i.Close, High = i.High, Low = i.Low, Open = i.Open, Volume = i.Vol });
                    }
                    result.Add(new Mapper().GetNamad(item.Name, ohlcvs));
                }
            }

            return result;
        }

        public void SaveNamads(List<Core.Namad> namads)
        {
            foreach (var namad in namads)
            {
                var ms = new Mapper().GetMoamelat(namad);
                using (var db = new Context())
                {
                    db.Namads.Add(new Namad() { Code = namad.Code,ISin = namad.ISin ,Name=namad.Name,
                        Moamelatss =ms.ConvertAll<Moamelat>(p=>
                        new Moamelat() {Arzesh=p.Arzesh,Close=p.Close,High=p.High,Last=p.Last,Low=p.Low,Open=p.Open,Tedad })})
                }
            }
            
            
        }
    }
}
