using ppedv.Weihnachtsbaeckerei.Model;
using System.Data.Entity;

namespace ppedv.Weihnachtsbaeckerei.Data.EF
{
    public class EfContext : DbContext
    {
        public DbSet<Cookie> Cookies { get; set; }
        public DbSet<Glasur> Glasuren { get; set; }
        public DbSet<Zutat> Zutaten { get; set; }

        public EfContext() : base("Server=.;Database=Cookies;Trusted_Connection=true;")
        { }


        public override int SaveChanges()
        {
            //todo Audit here
            return base.SaveChanges();
        }
    }
}
