using System;
using System.Collections.Generic;
using System.Text;

namespace ExamWatches.ViewModels
{
    class tset : ObservableObject
    {

        private string _dayDate;
        public string dayDate
        {
            get { return _dayDate; }
            set { _dayDate = value; OnPropertyChanged("dayDate"); }
        }


        private int _PeriodNum;
        public int PeriodNum
        {
            get { return _PeriodNum; }
            set { _PeriodNum = value; OnPropertyChanged("PeriodNum"); }
        }






    }
}
