using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_db
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new RssContext())
            {
                // Zrobienie przykładowego kanału
                // var source = new Source { Name = "Wp.pl - sport", SourceId = 1, Category = "Sport", url = "http://sport.wp.pl/rss.xml" };
                // var source = new Source { Name = "Wp.pl - nauka", SourceId = 2, Category = "Nauka", url = "http://nauka.wp.pl/rss.xml" };
                //var news = new News { NewsId = 1, Category = "Sport", Description = "Ktos cos wygral", Link = "www.sport.wp.pl/lingcos", PubDate = new DateTime(2017, 04, 06), SourceId = 1, Title = "Wygrana!!!" };
                //var news1 = new News { NewsId = 2, Category = "Nauka", Description = "Ktos cos odkrył", Link = "www.nauka.wp.pl/lingcos", PubDate = new DateTime(2017, 04, 05), SourceId = 2, Title = "Odkrycie!!!" };
                //db.Source.Add(source);
                //db.News.Add(news);
                //db.News.Add(news1);
                  db.SaveChanges();

                // Wyświetlanie wszystkich kanałow jakie są w bazie 

                var query = from b in db.Source
                            orderby b.Name
                            select b;

                Console.WriteLine("All source in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }

            }
        }
    }


    public class Source
    {
        [Key]
        public int SourceId { get; set; }
        public string Name { get; set; }
        public string url { get; set; }
        public string Category { get; set; }

        public virtual List<News> News { get; set; } //Lazy Loading feature of Entity Framework
    }

    public class News
    {
        [Key]
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime PubDate { get; set; }

        public int SourceId { get; set; }
        public virtual Source Source { get; set; } //Lazy Loading feature of Entity Framework
    }


    public class RssContext : DbContext
    {
        public DbSet<Source> Source { get; set; }
        public DbSet<News> News { get; set; }
    }

}

