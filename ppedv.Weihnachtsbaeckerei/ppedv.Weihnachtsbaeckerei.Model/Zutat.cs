using System.Collections.Generic;

namespace ppedv.Weihnachtsbaeckerei.Model
{
    public class Zutat : Entity
    {
        public string Name { get; set; }
        public bool Vegan { get; set; }
        public bool Gluten { get; set; }
        public virtual HashSet<Cookie> Cookies { get; set; } = new HashSet<Cookie>();
    }
}
