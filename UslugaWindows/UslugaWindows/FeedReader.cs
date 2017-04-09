using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.IO;
using Model;
using System.Text.RegularExpressions;
using System.Data.Entity;
using Model.DbModel;
using RSS_db;

namespace UslugaWindows
{
    
    public class FeedReader   // ppbranie kanału i danej wiadomosci oraz parsowanie wiadomosci na itema
    {                          // przeksztaucenie xml z www na obiekt typu item 
        private SyndicationFeed _feed;
        private string _feedUrl;
     
        private Channel _channel;
        private ItemRepository itemRepository;
        private ChannelRepository channelRepository;
        public FeedReader()
        {
            itemRepository = new ItemRepository();
            channelRepository = new ChannelRepository();
        }
        
        public void StartFeed(string url)
        {
            _feedUrl = url;
            LoadFeed();
            _channel = createOrFindChannel();
            setItems();
        }
        private void LoadFeed()
        {
            XmlReader reader = XmlReader.Create(_feedUrl); //pobranie kodu xml z www
            _feed = SyndicationFeed.Load(reader);
        }

        private Channel createOrFindChannel()
        {
            var channel = channelRepository.GetChannels().FirstOrDefault(x => x.BaseUrl == _feedUrl)??null;   

            if (channel == null)             // funkcja pobierająca kanał i wysyłająca do bazy
            {                                   // jesli nie istniej kanał to tworzy nowy
                channel = new Channel
                {
                    BaseUrl = _feedUrl,
                    Description = _feed.Description.Text,
                    Language = _feed.Language,
                    Title = _feed.Title.Text,
                    ImageUrl = "",
                };

                channelRepository.Add(channel);
            }

            return channel;
        }

        private void setItems()
        {
            foreach (var feedItem in _feed.Items)
            {
                var item = itemRepository.GetItems().FirstOrDefault(x => x.ExternalGuid == feedItem.Id);

                if (item == null)       //tworzenie itema z rss i zapisuje, lub update do bazy
                {                                           //jesli item juz istnieje
                    createItems(feedItem);
                }
                else
                {
                    updateItem(feedItem, ref item);
                }
            }
        }

        private void createItems(SyndicationItem feedItem)
        {
            var newItem = prepareItem(feedItem, new Item());

            newItem.Channel = _channel;
            newItem.Published = feedItem.PublishDate.DateTime;
            newItem.ExternalGuid = feedItem.Id;

            itemRepository.Add(newItem);        // stworzenie nowego itema
        }

        private void updateItem(SyndicationItem feedItem, ref Item item)
        {
            if (!compareItems(feedItem, item))
            {
                var updatedItem = prepareItem(feedItem, item);
                itemRepository.Attach(updatedItem);             // jesli item jest pobrany to porownuje itemy
            }
        }

        private Item prepareItem(SyndicationItem feedItem, Item item)
        {
            item.Title = feedItem.Title.Text;           //przeksztaucenie kodu na itema wraz z właściwosciami ktore sa w modelu
            item.Content = stripHtml(feedItem.Summary.Text);
            item.Link = feedItem.Links[0].Uri.ToString();
            item.ImageUrl = extractImageUrl(feedItem.Summary.Text);
            item.Updated = updatedDateTime(feedItem);
            return item;
        }

        private DateTime updatedDateTime(SyndicationItem feedItem)
        {
            return feedItem.LastUpdatedTime.DateTime == DateTime.MinValue  // update daty
                    ? feedItem.PublishDate.DateTime
                    : feedItem.LastUpdatedTime.DateTime;
        }

        private bool compareItems(SyndicationItem feedItem, Item item)      // porownywanie wlasciwosci itemow
        {
            return feedItem.Title.Text == item.Title &&
                   stripHtml(feedItem.Summary.Text) == item.Content &&
                   updatedDateTime(feedItem) == item.Updated &&
                   extractImageUrl(feedItem.Summary.Text) == item.ImageUrl &&
                   feedItem.Links[0].Uri.ToString() == item.Link;
        }

        private string extractImageUrl(string text)  // pobranie url do zdjecia z xml
        {
            Regex imageSourceRegex = new Regex("<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase);
            return imageSourceRegex.Match(text).Groups[1].Value;
        }

        private string stripHtml(string text) //usuniecie zbednych elementow html
        {
            return Regex.Replace(text, @"<[^>]*(>|$)|&nbsp;|&zwnj;|&raquo;|&laquo;", string.Empty).Trim();
        }
    }
    
}

