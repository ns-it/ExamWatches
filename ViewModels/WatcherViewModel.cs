using ExamWatches.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamWatches.ViewModels
{
    public class WatcherViewModel : ObservableObject
    {
        private bool _isSelected;

        public string FullName { get; set; }
        public short? Class { get; set; }
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; OnPropertyChanged("IsSelected"); }
        }
    }
}
