using System.Linq;
using Model.DbModel;
using RSS_db;
using System.Collections.Generic;

namespace RSS_db
{
    public class ChannelRepository
    {

        public IQueryable<Channel> Channel { get; set; }

        public ChannelRepository()
        {
            using (var _dbContext = new RAPDbContext())
            {
                Channel = _dbContext.Channel;
            }
        }
        public void Add(Channel channel)
        {
            using (var _dbContext = new RAPDbContext())
            {
                _dbContext.Channel.Add(channel);
                _dbContext.SaveChanges();

              
            }
        }
        public List<Channel> GetChannels()
        {
            using (var _dbContext = new RAPDbContext())
            {
               return _dbContext.Channel.ToList();
            }

        }
        public Channel FindChannel(string baseUrl)
        {
            var channel = Channel.FirstOrDefault(x => x.BaseUrl == baseUrl);
            return channel ?? new Channel();
        }
       
       
    }
}
