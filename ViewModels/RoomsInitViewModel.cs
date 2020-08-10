using ExamWatches.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ExamWatches.ViewModels
{
    //public class RoomsInitViewModel : INotifyPropertyChanged
        public class RoomsInitViewModel
    {
        ////public ObservableCollection<Room> RoomsList { get; set; }
        public List<Room> RoomsList { get; set; }


        //private ObservableCollection<Room> _roomsList;

        //public event PropertyChangedEventHandler PropertyChanged;

        //public ObservableCollection<Room> RoomsList
        //{
        //    get { return _roomsList; }
        //    set { _roomsList = value; OnPropertyChanaged("RoomsList"); }
        //}


        public RoomsInitViewModel()
        {
            using(ExamWatchesDBContext db = new ExamWatchesDBContext())
            {
                //db.Rooms.Load();
                //RoomsList = db.Rooms.Local.ToObservableCollection();
                RoomsList = db.Rooms.ToList();
            }
        }

        //protected void OnPropertyChanaged(string name)
        //{
        //    if(PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(name));
        //    }
        //}

    }
}
