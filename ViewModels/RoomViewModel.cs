using ExamWatches.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamWatches.ViewModels
{
    public class RoomViewModel : ObservableObject
    {

        private Room _room;
        public RoomViewModel(Room room) { _room = room; }
        public RoomViewModel() : this(new Room()) { }
        public Room Room
        {
            get { return _room; }
            set { _room = value; OnPropertyChanged("Room"); }
        }
        public int Id
        {
            get { return _room.Id; }
            set { _room.Id = value; OnPropertyChanged("Id"); }
        }
        public string Name
        {
            get { return _room.Name; }
            set { _room.Name = value; OnPropertyChanged("Name"); }
        }
        public short? Capacity
        {
            get { return _room.Capacity; }
            set { _room.Capacity = value; OnPropertyChanged("Capacity"); }
        }

        public short? WorkLocationId
        {
            get { return _room.WorkLocationId; }
            set { _room.WorkLocationId = value; OnPropertyChanged("WorkLocationId"); }
        }

        public string Type
        {
            get
            {
                return _room.Type switch {
                    1 => "صغيرة",
                    2 => "متوسطة",
                    3 => "كبيرة",
                    4 => "مدرج",
                    _ => "",
                };
            }

            set
            {
                _room.Type = value switch
                {
                    "صغيرة" => 1,
                    "متوسطة" => 2,
                    "كبيرة" => 3,
                    "مدرج" => 4,
                    _ => 0,
                };
                ;
                OnPropertyChanged("Type");
            }
        }



    }




}
