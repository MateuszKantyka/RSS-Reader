using System.Linq;
using Model.DbModel;
using RSS_db;
using System.Data.Entity;
using System.Collections.Generic;

namespace RSS_db
{
    public class ItemRepository
    {
        public IQueryable<Item> Items { get; set; }


      public List<Item> GetItems()
        {
            using (var _dbContext = new RAPDbContext())
            {
                return  _dbContext.Item.ToList();
            }
        }
        public ItemRepository()
        {
            using (var _dbContext = new RAPDbContext())
            {
                Items = _dbContext.Item;
            }
        }

        public void Add(Item item)
        {

            using (var _dbContext = new RAPDbContext())
            {
                _dbContext.Item.Add(item);
                _dbContext.SaveChanges();

            }
        }

        public void Attach(Item item)
        {
            using (var _dbContext = new RAPDbContext())
            {
                _dbContext.Item.Attach(item);
                _dbContext.Entry(item).State = EntityState.Modified;
                _dbContext.SaveChanges();              
            }
        }
    }
}
