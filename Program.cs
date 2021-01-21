using System;
using NLog.Web;
using System.IO;
using System.Linq;

namespace BlogsConsole
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Program started");
            var db = new BloggingContext();
            if(db.Locations.Count()==0){
                logger.Info("Seeding Locations");
                var location=new Location{Name="Work"};
                db.AddLocation(location);
                location=new Location{Name="Home"};
                db.AddLocation(location);
                location=new Location{Name="Club"};
                db.AddLocation(location);
            }
            if(db.Events.Count()==0){
                logger.Info("Seeding Events");
                Random r=new Random();
                DateTime current=new DateTime(DateTime.Now.Year,DateTime.Now.Month,1);
                for(int i=1;i<=6;i++){
                    DateTime date=current.AddMonths(i);
                    int days=DateTime.DaysInMonth(date.Year,date.Month);
                    for(int d=0;d<days;d++){
                        int num=r.Next(6);
                        DateTime nextDate=date.AddDays(d);
                        for(int t=0;t<num;t++){
                            DateTime dateTime=new DateTime(nextDate.Year,nextDate.Month,nextDate.Day,r.Next(24),r.Next(60),r.Next(60));
                            Location location=db.Locations.ToArray()[r.Next(db.Locations.Count())];
                            var e=new Event{TimeStamp=dateTime,Flagged=false,LocationId=location.LocationId,Location=location};
                            db.AddEvent(e);
                        }
                    }
                }
            }
            Console.WriteLine("All Events:");
            var events=db.Events.OrderBy(events=>events.TimeStamp);
            foreach(var e in events){
                Console.WriteLine(e.TimeStamp.ToShortDateString()+" "+e.TimeStamp.ToShortTimeString()+" - "+e.Location.Name);
            }
            // try
            // {

            //     // Create and save a new Blog
            //     Console.Write("Enter a name for a new Blog: ");
            //     var name = Console.ReadLine();

            //     var blog = new Blog { Name = name };

        
            //     db.AddBlog(blog);
            //     logger.Info("Blog added - {name}", name);

            //     // Display all Blogs from the database
            //     var query = db.Blogs.OrderBy(b => b.Name);

            //     Console.WriteLine("All blogs in the database:");
            //     foreach (var item in query)
            //     {
            //         Console.WriteLine(item.Name);
            //     }
            // }
            // catch (Exception ex)
            // {
            //     logger.Error(ex.Message);
            // }

            logger.Info("Program ended");
        }
    }
}
