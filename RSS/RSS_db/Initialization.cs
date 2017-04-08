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
    public static class Initialization
    {

        public static void initializationChannel()
        { 

        using (var db = new RAPDbContext())
            {
                var wp_sport = new Channel
                {
                    Id = 1,
                    BaseUrl = "Sport",
                    Description = "Wiadomości sportowe z całego kraju!",
                    //ImageUrl = "www.sport.wp.pl/lingcos221",
                    Language = "pl",
                    Title = "WP - sport"
    };

                db.Channel.Add(wp_sport);

                db.SaveChanges();
            }
        }
    }
}
