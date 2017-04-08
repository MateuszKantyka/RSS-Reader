using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using System.Timers;
using System.IO;
using Quartz;
using Topshelf.Quartz;
using UslugaWindows;




//zamiast timera trzeba wykorzystac w kalsie TownCrier biblioteke quartz.net, dodać 
//przygotowanie serwisu do zapisu feedow do bazy danych - baza microsoft sql server
// zrobienie serwisu ktory laczy sie z url i pobiera feedy z www 
// wyslanie danych do bazy za pomoca Entity Framework, Mateuysz robi Code First


namespace UslugaWindows
    {
      
    public class MyService
        {
          
            public void Start() {  }
            public void Stop() { }
        }

        public class Program
        {
            public static void Main()
            {
                HostFactory.Run(x =>
                {
                    x.Service<MyService>(s =>
                    {
                        s.ConstructUsing(name => new MyService());
                        s.WhenStarted(tc => tc.Start());
                        s.WhenStopped(tc => tc.Stop());

                        s.ScheduleQuartzJob(q =>
                   q.WithJob(() =>
                       JobBuilder.Create<FeedReadSchedule>().Build())
                   .AddTrigger(() =>
                       TriggerBuilder.Create()
                           .WithSimpleSchedule(builder => builder
                               .WithIntervalInSeconds(5)
                               .RepeatForever())
                           .Build())
                    );
                    });
                    x.RunAsLocalService();

                    x.SetDescription("Sample Topshelf Host");
                    x.SetDisplayName("LukaszUsluga");
                    x.SetServiceName("LukaszUsluga");
                });
            }
        }
    }

