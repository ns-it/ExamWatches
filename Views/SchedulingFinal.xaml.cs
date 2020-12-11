using ExamWatches.Models;
using ExamWatches.Utilities;
using ExamWatches.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Xps;

namespace ExamWatches.Views
{
    /// <summary>
    /// Interaction logic for SchedulingFinal.xaml
    /// </summary>
    public partial class SchedulingFinal : UserControl
    {

        ExamWatchesDBContext db = new ExamWatchesDBContext();
        public SchedulingFinal()
        {
            InitializeComponent();
        }







        private void Print_Click(object sender, RoutedEventArgs e)
        {
            
         
            ObservableCollection<WatchTableViewModel> list = new ObservableCollection<WatchTableViewModel>();
          list=  WatchesSchedule.ItemsSource as ObservableCollection<WatchTableViewModel>;
            MessageBox.Show(list.Count.ToString());
            foreach (WatchTableViewModel wtvm in list) {
                Final f = new Final();

                f.Date = date.Text;
                f.Period = period.Text;
                f.Roomname=  wtvm.Room.Name;
             f.RoomChief=   wtvm.RoomChiefs;
            f.RoomSecretarie=    wtvm.RoomSecretaries;
              f.RoomWatcher=  wtvm.RoomWatchers;

                db.Finals.Add(f);
                db.SaveChanges();
            
            
            }




        }

        private void WatchesSchedule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
