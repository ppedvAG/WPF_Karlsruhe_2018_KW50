using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ppedv.Weihnachtsbaeckerei.Model;

namespace ppedv.Weihnachtsbaeckerei.Data.EF.Tests
{
    [TestClass]
    public class EfContextTests
    {
        [TestMethod]
        public void EfContext_can_create_database()
        {
            using (var con = new EfContext())
            {
                if (con.Database.Exists())
                    con.Database.Delete();

                con.Database.Create();
                Assert.IsTrue(con.Database.Exists());
            }
        }

        [TestMethod]
        public void EfContext_can_add_cookie()
        {
            var cookie = new Cookie()
            {
                Name = "Lecker",
                Form = Form.Mond,
                Herstellung = new DateTime(2000, 12, 24)
            };

            using (var con = new EfContext())
            {
                con.Cookies.Add(cookie);
                int result = con.SaveChanges();
                Assert.AreEqual(1, result);
            }

            using (var con = new EfContext())
            {
                var loaded =  con.Cookies.Find(cookie.Id);
                Assert.AreEqual(cookie.Name, loaded.Name);

                loaded.Name = "Viel leckerererer";
                con.SaveChanges();
            }

        }
    }
}
