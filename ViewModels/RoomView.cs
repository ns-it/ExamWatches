using System;
using System.Collections.Generic;
using System.Text;

namespace ExamWatches.ViewModels
{
    public class RoomView : ObservableObject
    {

        public int Id { get; set; }

        private bool _isSelected;
        public string Name { get; set; }
        public int? Type { get; set; }
        public int? Capacity { get; set; }
        public int? WorkLocation { set; get; }
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; OnPropertyChanged("IsSelected"); }
        }


        public string TypeString
        {
            get
            {
                return Type switch
                {
                    1 => "صغيرة",
                    2 => "متوسطة",
                    3 => "كبيرة",
                    4 => "مدرج",
                    _ => "",
                };
            }

            set
            {
                Type = value switch
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
