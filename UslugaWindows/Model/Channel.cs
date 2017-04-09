using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DbModel
{
    public class Channel               //przechowywanie modelu właściwości kanału 
    {
        public Channel()
        {
            Items = new List<Item>();
        }
        [Key]
        public int Id { get; set; }
        public string BaseUrl { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Language { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
