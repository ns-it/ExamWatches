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

    }
}
