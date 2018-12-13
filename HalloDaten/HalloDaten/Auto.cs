using System;

namespace HalloDaten
{
    public class Auto
    {
        public int Id { get; set; }
        public string Modell { get; set; }
        public DateTime Baujahr { get; set; }
        public Hersteller Hersteller { get; set; }
    }
}
