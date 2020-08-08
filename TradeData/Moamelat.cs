using System.Collections.Generic;

namespace TradeData
{
    public class Moamelat
    {
        //public int id { get; set; }
        public int RozId { get; set; }
        public Roz Roz { get; set; }
        public int NamadId { get; set; }
        public Namad Namad { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Vol { get; set; }
        public decimal Tedad { get; set; }
        public decimal Arzesh { get; set; }
        public decimal Last { get; set; }
    }
}