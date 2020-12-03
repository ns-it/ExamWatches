using ExamWatches.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamWatches.ViewModels
{
    public class WatchTableViewModel
    {
        public Watch Watch { get; set; }
        public Room Room { get; set; }
        //public int RoomId { get; set; }
        //public Watcher Watcher { get; set; }

        public string RoomChiefs { get; set; }
        public string RoomSecretaries { get; set; }
        public string RoomWatchers { get; set; }
        public List<string> RoomChiefsList { get; set; }
        public List<string> RoomSecretariesList { get; set; }
        public List<string> RoomWatchersList { get; set; }

        public List<WatcherWatch> WatcherWatchesList { get; set; }

        public WatchTableViewModel()
        {
            RoomChiefsList = new List<string>();
            RoomSecretariesList = new List<string>();
            RoomWatchersList = new List<string>();
            WatcherWatchesList = new List<WatcherWatch>();
            Watch = new Watch();
        }
    }
}
