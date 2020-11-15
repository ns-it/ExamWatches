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
        ObservableCollection<fillFirstDataGridmodel> FirstDataGramData;
        ObservableCollection<Scheduling3ViewModel> SecondDataGramData;
        public ObservableCollection<int> periodList;
        public ObservableCollection<decimal> periodDurationList;
        DateTime selelectedDate;


        TimeSpan startTime;
        
        Scheduling3ViewModel svm3;
     
        fillFirstDataGridmodel fillFirstDataGridmodel;
        



        public Scheduling3()
        {
            FirstDataGramData = new ObservableCollection<fillFirstDataGridmodel>();
            SecondDataGramData = new ObservableCollection<Scheduling3ViewModel>();
            periodList = new ObservableCollection<int>();
            

            
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

            Periods.ItemsSource = FirstDataGramData;
            Periods2.ItemsSource = SecondDataGramData;
           //ComboBox cb= Periods.Columns[1].GetCellContent(Periods.Items[0]) as ComboBox;
            
        }

      

        public void ExamDay_selectionChange_Loop() {


           
            fillFirstDataGridmodel = new fillFirstDataGridmodel();
            fillFirstDataGridmodel.dayDate = selelectedDate.ToString("dd//MM//yyyy");
            fillFirstDataGridmodel.DateNumList = periodList;
            FirstDataGramData.Add(fillFirstDataGridmodel);


        }

       
       

        private void ExamDays_SelectedDatesChanged_2(object sender, SelectionChangedEventArgs e)
        {

            selelectedDate = (DateTime)ExamDays.SelectedDate;

            ExamDay_selectionChange_Loop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Periods.Columns[1].GetCellContent(Periods.Items[0]) as ComboBox;
            // for (int i = 0; i <= Periods.Items.Count; i++) {


            // svm3.day =
     //     string ss=  Periods.Columns[0].GetCellContent(Periods.Items[0]) as string;
          //  MessageBox.Show(Periods.Columns[0].GetCellContent(Periods.Items[0]).ToString());
            ComboBox cb = Periods.Columns[1].GetCellContent(Periods.Items[0]) as ComboBox;
            //svm3.periodID =int.Parse( cb.Text);
            //svm3.startTime = startTime;
            //svm3.periodDuration = periodDurationList;
            //SecondDataGramData.Add(svm3);



            //  }
        }
    }
}
