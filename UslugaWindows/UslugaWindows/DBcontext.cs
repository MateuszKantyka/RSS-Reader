
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using UslugaWindows.Model;

namespace UslugaWindows
{
    class RAPDbContext : DbContext
    {
        public RAPDbContext()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine(path);
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }

        public DbSet<Item> Item { get; set; }
        public DbSet<Channel> Channel { get; set; }
    }

}