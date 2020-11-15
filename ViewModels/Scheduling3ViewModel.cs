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
      
        private int _periodID;
        public int periodID
        {
            get { return _periodID; }
            set { _periodID = value; OnPropertyChanged("periodID"); }
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
