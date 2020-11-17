using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ExamWatches.ViewModels
{
   public class fillFirstDataGridmodel:ObservableObject



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

        private int _selectedNum;
        public int SelectedNum
        {
            get { return _selectedNum; }
            set { _selectedNum = value; OnPropertyChanged("SelectedNum"); }
        }



    }
}
