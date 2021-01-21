using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BlogsConsole
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Location> Locations {get;set;}
        public DbSet<Event> Events {get;set;}

        public void AddBlog(Blog blog)
        {
            this.Blogs.Add(blog);
            this.SaveChanges();
        }
        public void AddLocation(Location loc){
            this.Locations.Add(loc);
            this.SaveChanges();
        }
        public void AddEvent(Event e){
            this.Events.Add(e);
            this.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            optionsBuilder.UseSqlServer(@config["BloggingContext:ConnectionString"]);
        }
    }
}
