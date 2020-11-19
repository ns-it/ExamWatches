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
      
        private short _periodID;
        public short periodID
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

        private decimal _PD;
        public decimal PD
        {
            get { return _PD; }
            set { _PD = value; OnPropertyChanged("PD"); }
        }


    }
}
