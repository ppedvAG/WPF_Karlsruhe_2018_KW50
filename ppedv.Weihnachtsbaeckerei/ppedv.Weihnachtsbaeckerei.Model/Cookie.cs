using System;
using System.Collections.Generic;

namespace ppedv.Weihnachtsbaeckerei.Model
{
    public class Cookie : Entity
    {
        public string Name { get; set; }
        public Form Form { get; set; }
        public DateTime Herstellung { get; set; }
        public int KCal { get; set; }
        public virtual HashSet<Zutat> Zutaten { get; set; } = new HashSet<Zutat>();
        public virtual Glasur Glasur { get; set; }
    }
}
