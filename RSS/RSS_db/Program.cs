using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSS_db.Model;

namespace RSS_db
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new RAPDbContext())
            {
                  db.SaveChanges();

                // Wyświetlanie wszystkich kanałow jakie są w bazie 
                var query = from x in db.Channel
                            orderby x.Title
                            select x;

                Console.WriteLine("All source in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Title);
                }

            }
        }
    }


    class RAPDbContext : DbContext
    {
        //public RAPDbContext()
        //{
        //    string path = AppDomain.CurrentDomain.BaseDirectory;
        //    Console.WriteLine(path);
        //    AppDomain.CurrentDomain.SetData("DataDirectory", path);
        //}

        public DbSet<Item> Item { get; set; }
        public DbSet<Channel> Channel { get; set; }
    }
}

                // Zrobienie przykładowego kanału
                // var source = new Source { Name = "Wp.pl - sport", SourceId = 1, Category = "Sport", url = "http://sport.wp.pl/rss.xml" };
                // var source = new Source { Name = "Wp.pl - nauka", SourceId = 2, Category = "Nauka", url = "http://nauka.wp.pl/rss.xml" };
                //var news = new News { NewsId = 1, Category = "Sport", Description = "Ktos cos wygral", Link = "www.sport.wp.pl/lingcos", PubDate = new DateTime(2017, 04, 06), SourceId = 1, Title = "Wygrana!!!" };
                //var news1 = new News { NewsId = 2, Category = "Nauka", Description = "Ktos cos odkrył", Link = "www.nauka.wp.pl/lingcos", PubDate = new DateTime(2017, 04, 05), SourceId = 2, Title = "Odkrycie!!!" };
                //db.Source.Add(source);
                //db.News.Add(news);
                //db.News.Add(news1);
