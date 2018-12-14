using System.Collections.Generic;

namespace ppedv.Weihnachtsbaeckerei.Model
{
    public class Glasur : Entity
    {
        public string Name { get; set; }
        public string Farbe { get; set; }
        public virtual HashSet<Cookie> Cookies { get; set; } = new HashSet<Cookie>();
    }
}
