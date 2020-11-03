using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

namespace ExamWatches.ViewModels
{
    public class Scheduling3ViewModel : ObservableObject
    {
        private string _day;
        public string day
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
        private TimeSpan _startTime;
        public TimeSpan startTime
        {
            get { return _startTime; }
            set { _startTime = value; OnPropertyChanged("startTime"); }
        }
        private ObservableCollection<decimal> _periodDuration;
        public ObservableCollection<decimal> periodDuration
        {
            get { return _periodDuration; }
            set { _periodDuration = value; OnPropertyChanged("periodDuration"); }
        }




    }
}
