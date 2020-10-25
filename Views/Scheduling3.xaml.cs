using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ExamWatches.ViewModels;

namespace ExamWatches.Views
{
    /// <summary>
    /// Interaction logic for Scheduling3.xaml
    /// </summary>
    public partial class Scheduling3 : UserControl
    {
        ObservableCollection<Scheduling3ViewModel> DatesList;
        public ObservableCollection<int> periodList;
        DateTime dt;

        public Scheduling3()
        {
            DatesList = new ObservableCollection<Scheduling3ViewModel>();
            periodList = new ObservableCollection<int>();
            periodList.Add(1);
            periodList.Add(2);
            periodList.Add(3);



            InitializeComponent();
        }

        private void ExamDays_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {


            Scheduling3ViewModel svm3 = new Scheduling3ViewModel();

            //svm3.day=  ExamDays.SelectedDate.Value.ToString("dd//MM//yyyy");

            svm3.day = ExamDays.SelectedDate.Value.Date;
            svm3.periodListItem = periodList;
            svm3.timeItem = dt.ToLongTimeString();
            DatesList.Add(svm3);
            Periods.ItemsSource = DatesList;


        }

        //private void Next_Click(object sender, RoutedEventArgs e)
        //{
        //    SchedulingFinal schedulingFinal = new SchedulingFinal();
        //    this.Content = schedulingFinal.Content;
        //}

        //private void Previous_Click(object sender, RoutedEventArgs e)
        //{
        //    Scheduling2 scheduling2 = new Scheduling2();
        //    this.Content = scheduling2.Content;
        //}
    }
}
