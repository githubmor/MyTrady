using System.Collections.Generic;

namespace TradeData
{
    public class Namad
    {
        public Namad()
        {
            Moamelatss = new HashSet<Moamelat>();
        }
        public int id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ISin { get; set; }
        public virtual ICollection<Moamelat> Moamelatss { get; set; }
    }
}