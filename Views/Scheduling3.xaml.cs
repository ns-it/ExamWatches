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
        public ObservableCollection<decimal> periodDurationList;

        TimeSpan startTime;
        ObservableCollection<DateTime> dateTimeLoop;
        Scheduling3ViewModel svm3;

        public Scheduling3()
        {
            DatesList = new ObservableCollection<Scheduling3ViewModel>();
            periodList = new ObservableCollection<int>();
            dateTimeLoop = new ObservableCollection<DateTime>();
            
            periodList.Add(1);
            periodList.Add(2);
            periodList.Add(3);
            periodDurationList = new ObservableCollection<decimal>();
            periodDurationList.Add(new decimal(60));
            periodDurationList.Add(new decimal(90));
            periodDurationList.Add(new decimal(120));
            periodDurationList.Add(new decimal(150));

            periodDurationList.Add(new decimal(180));
            periodDurationList.Add(new decimal(210));
            periodDurationList.Add(new decimal(240));







            InitializeComponent();

            Periods.ItemsSource = DatesList;
        }

        private void ExamDays_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

            // dateTimeLoop.Add(ExamDays.SelectedDate);
            dateTimeLoop = ExamDays.SelectedDates;
            ExamDay_selectionChange_Loop();




        }

        public void ExamDay_selectionChange_Loop() {
            
            foreach (DateTime selectedDate in dateTimeLoop)
            {

                svm3 = new Scheduling3ViewModel();
                svm3.day = selectedDate.ToString("dd//MM//yyyy");
                //   dateTimeLoop.Remove(dt);
                // svm3.day = ExamDays.SelectedDate.Value.Date;
                svm3.periodListItem = periodList;
                svm3.startTime = startTime;
                svm3.periodDuration = periodDurationList;
                DatesList.Add(svm3);
                // Periods.ItemsSource = DatesList;
              // 
            }
            dateTimeLoop.Clear();



        }

        private void ExamDays_SelectedDatesChanged_1(object sender, SelectionChangedEventArgs e)
        {

            // dateTimeLoop.Add(ExamDays.SelectedDate);
            dateTimeLoop = ExamDays.SelectedDates;
            ExamDay_selectionChange_Loop();

        }

        private void Periods_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ExamDays_SelectedDatesChanged_2(object sender, SelectionChangedEventArgs e)
        {  // dateTimeLoop.Add(ExamDays.SelectedDate);
            dateTimeLoop = ExamDays.SelectedDates;
            ExamDay_selectionChange_Loop();


        }
    }
}
