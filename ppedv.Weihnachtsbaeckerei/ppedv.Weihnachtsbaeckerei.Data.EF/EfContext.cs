using ppedv.Weihnachtsbaeckerei.Model;
using System.Data.Entity;

namespace ppedv.Weihnachtsbaeckerei.Data.EF
{
    public class EfContext : DbContext
    {
        public DbSet<Cookie> Cookies { get; set; }
        public DbSet<Glasur> Glasuren { get; set; }
        public DbSet<Zutat> Zutaten { get; set; }

    }
}
