using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ExamWatches.ViewModels
{
    class fillFirstDataGridmodel:ObservableObject



    {


        private string _dayDate;
        public string dayDate
        {
            get { return _dayDate; }
            set { _dayDate = value; OnPropertyChanged("dayDate"); }
        }
        private ObservableCollection<int> _DateNumList;
        public ObservableCollection<int> DateNumList
        {
            get { return _DateNumList; }
            set { _DateNumList = value; OnPropertyChanged("DateNumList"); }
        }


    }
}
