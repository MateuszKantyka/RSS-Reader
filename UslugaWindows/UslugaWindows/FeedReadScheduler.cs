using Topshelf.Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Quartz;

namespace UslugaWindows
{
    
   public class FeedReadSchedule : IJob
    {

       
        public FeedReader feedreader;
       
      
        public void Execute(IJobExecutionContext context)
        {
            feedreader.StartFeed(@"http://gry.wp.pl/rss/wiadomosci");
            Console.WriteLine("it is {0} and everything is ok!");

            feedreader.StartFeed(@"http://wiadomosci.wp.pl/ver,rss,rss");
            Console.WriteLine("it is {0} and everything is ok!");

            feedreader.StartFeed(@"http://pogoda.wp.pl/rss");
            Console.WriteLine("it is {0} and everything is ok!");

            feedreader.StartFeed(@"http://banki.wp.pl/rss");
            Console.WriteLine("it is {0} and everything is ok!");
        }

       
    }




}
