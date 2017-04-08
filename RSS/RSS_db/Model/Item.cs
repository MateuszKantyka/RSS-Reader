using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_db.Model
{
    class Item
    {
        class Item
        {
            [Key]
            public int Id { get; set; }
            public string Title { get; set; }
            public DateTime Updated { get; set; }
            public string Link { get; set; }
            public string Content { get; set; }
            //public string ImageUrl { get; set; }
            public DateTime Published { get; set; }

            public string ExternalGuid { get; set; }

            public virtual Channel Channel { get; set; }
        }
    }
}
