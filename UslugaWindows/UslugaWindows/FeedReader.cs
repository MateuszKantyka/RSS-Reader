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
using UslugaWindows.Model;
using System.Text.RegularExpressions;
using System.Data.Entity;


namespace UslugaWindows
{
    
    public class FeedReader
    {
        private SyndicationFeed _feed;
        private string _feedUrl;
     
        private Channel _channel;

        public FeedReader()
        {
                 
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
            XmlReader reader = XmlReader.Create(_feedUrl);
            _feed = SyndicationFeed.Load(reader);
        }

        private Channel createOrFindChannel()
        {
            var channel = channelRepository.Channel.FirstOrDefault(x => x.BaseUrl == _feedUrl);

            if (channel == null)
            {
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
                var item = itemRepository.Items.FirstOrDefault(x => x.ExternalGuid == feedItem.Id);

                if (item == null)
                {
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

            itemRepository.Add(newItem);
        }

        private void updateItem(SyndicationItem feedItem, ref Item item)
        {
            if (!compareItems(feedItem, item))
            {
                var updatedItem = prepareItem(feedItem, item);
                itemRepository.Attach(updatedItem);
            }
        }

        private Item prepareItem(SyndicationItem feedItem, Item item)
        {
            item.Title = feedItem.Title.Text;
            item.Content = stripHtml(feedItem.Summary.Text);
            item.Link = feedItem.Links[0].Uri.ToString();
            item.ImageUrl = extractImageUrl(feedItem.Summary.Text);
            item.Updated = updatedDateTime(feedItem);
            return item;
        }

        private DateTime updatedDateTime(SyndicationItem feedItem)
        {
            return feedItem.LastUpdatedTime.DateTime == DateTime.MinValue
                    ? feedItem.PublishDate.DateTime
                    : feedItem.LastUpdatedTime.DateTime;
        }

        private bool compareItems(SyndicationItem feedItem, Item item)
        {
            return feedItem.Title.Text == item.Title &&
                   stripHtml(feedItem.Summary.Text) == item.Content &&
                   updatedDateTime(feedItem) == item.Updated &&
                   extractImageUrl(feedItem.Summary.Text) == item.ImageUrl &&
                   feedItem.Links[0].Uri.ToString() == item.Link;
        }

        private string extractImageUrl(string text)
        {
            Regex imageSourceRegex = new Regex("<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase);
            return imageSourceRegex.Match(text).Groups[1].Value;
        }

        private string stripHtml(string text)
        {
            return Regex.Replace(text, @"<[^>]*(>|$)|&nbsp;|&zwnj;|&raquo;|&laquo;", string.Empty).Trim();
        }
    }
    
}

