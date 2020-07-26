using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Roz
    {
        public Roz()
        {
            Moamelatss = new HashSet<Moamelat>();
        }
        public int ID { get; set; }
        public string Miladi { get; set; }
        public string Shamsi { get; set; }
        public string Step { get; set; }
        public virtual ICollection<Moamelat> Moamelatss { get; set; }
    }
}