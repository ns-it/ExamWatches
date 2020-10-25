using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

namespace ExamWatches.ViewModels
{
    public class Scheduling3ViewModel : ObservableObject
    {
        private DateTime _day;
        public DateTime day
        {
            get { return _day; }
            set { _day = value; OnPropertyChanged("day"); }
        }
        private ObservableCollection<int> _periodListItem;
        public ObservableCollection<int> periodListItem
        {
            get { return _periodListItem; }
            set { _periodListItem = value; OnPropertyChanged("periodListItem"); }
        }
        private string _timeItem;
        public string timeItem
        {
            get { return _timeItem; }
            set { _timeItem = value; OnPropertyChanged("timeItem"); }
        }
        private string _periodItem;
        public string periodItem
        {
            get { return _periodItem; }
            set { _periodItem = value; OnPropertyChanged("periodItem"); }
        }




    }
}
