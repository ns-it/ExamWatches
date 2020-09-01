using ExamWatches.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamWatches.ViewModels
{
    public class RoomViewModel : ObservableObject
    {

        private Room _model;
        public RoomViewModel(Room room) { _model = room; }
        public RoomViewModel() : this(new Room()) { }


        public Room Model
        {
            get { return _model; }
            set { _model = value; OnPropertyChanged("Model"); }
        }
        public int Id
        {
            get { return _model.Id; }
            set { _model.Id = value; OnPropertyChanged("Id"); }
        }
        public string Name
        {
            get { return _model.Name; }
            set { _model.Name = value; OnPropertyChanged("Name"); }
        }
        public short? Capacity
        {
            get { return _model.Capacity; }
            set { _model.Capacity = value; OnPropertyChanged("Capacity"); }
        }

        public short? WorkLocationId
        {
            get { return _model.WorkLocationId; }
            set { _model.WorkLocationId = value; OnPropertyChanged("WorkLocationId"); }
        }

        public string Type
        {
            get
            {
                return _model.Type switch {
                    1 => "صغيرة",
                    2 => "متوسطة",
                    3 => "كبيرة",
                    4 => "مدرج",
                    _ => "",
                };
            }

            set
            {
                _model.Type = value switch
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
