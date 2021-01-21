using System;
namespace BlogsConsole
{
    public class Event
    {
        public int EventId { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool Flagged { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}