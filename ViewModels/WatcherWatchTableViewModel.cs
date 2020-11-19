using ExamWatches.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamWatches.ViewModels
{
  public class WatcherWatchTableViewModel
    {
        //private DateTime watchDate;
        //private short watchPeriodId;
        //private int roomId;
        //private string watchType;
        //private bool attendence;

        //public DateTime? WatchDate { get; set; }
        //public short? WatchPeriodId { get; set; }
        public Room WatchRoom { get; set; }

        public Watch Watch { get; set; }
        public Watcher Watcher { get; set; }

        public string WatchType { get; set; }
        public bool Attendence { get; set; }

        public string RoomChief { get; set; }


    }
}
