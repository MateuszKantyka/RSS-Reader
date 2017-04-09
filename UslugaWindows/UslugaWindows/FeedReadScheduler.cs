using Topshelf.Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Quartz;

namespace UslugaWindows
{
    
   public class FeedReadSchedule : IJob   // interfejs z pobierany z quartza 
    {

       
        public FeedReader feedreader;
       
      
        public void Execute(IJobExecutionContext context) // zadanie ktore wykonuje sie w quartzu co 5 sek.
        {
            //zaczytywanie kanałów RSS i podział ich na 3 kategorie SPORT, INFORMACJE, ROZRYWKA
            feedreader = new FeedReader();

                        //SPORT
            feedreader.StartFeed(@"http://sport.wp.pl/kat,1726,rss.xml");
            Console.WriteLine("to jest {1} i wszystko jest ok!");  

            feedreader.StartFeed(@"http://sport.wp.pl/kat,1912,rss.xml");
            Console.WriteLine("to jest {2} i wszystko jest ok!");


                    //INFORMACJE
            feedreader.StartFeed(@"http://wiadomosci.wp.pl/kat,1342,ver,rss,rss.xml");
            Console.WriteLine("to jest {3} i wszystko jest ok!");

            feedreader.StartFeed(@"http://wiadomosci.wp.pl/kat,1356,ver,rss,rss.xml");
            Console.WriteLine("to jest {4} i wszystko jest ok!");

                      //ROZRYWKA
            feedreader.StartFeed(@"http://film.wp.pl/rss.xml?prem=1");
            Console.WriteLine("to jest {5} i wszystko jest ok!");

            feedreader.StartFeed(@"http://gry.wp.pl/rss/artykuly.xml");
            Console.WriteLine("to jest {6} i wszystko jest ok!");
        }

       
    }




}
