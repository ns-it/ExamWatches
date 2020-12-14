using ExamWatches.Models;
using ExamWatches.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for WatcherScheduling.xaml
    /// </summary>
    public partial class WatcherScheduling : UserControl
    {

        ExamWatchesDBContext db = new ExamWatchesDBContext();
        public WatcherScheduling()
        {
            InitializeComponent();
        }

       

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            List<Print> flist = new List<Print>();
          
          flist=  db.Prints.ToList<Print>();
            foreach (Print i in flist)
            {
                db.Prints.Remove(i);
                db.SaveChanges();
            }


            foreach (WatcherWatchTableViewModel wtvm in WatcherSchedule.Items)
            {
                Print f = new Print();
                f.FullName = WatcherName.Text;
                f.worklocation = worloc.Text;
                f.WatchDate = wtvm.Watch.WatchDate.ToString();
                f.PeriodId = wtvm.Watch.PeriodId.ToString();
                f.Name = wtvm.WatchRoom.Name;
                f.WatchType = wtvm.WatchType;
                f.RoomChief = wtvm.RoomChief;
                f.attendence = wtvm.Attendence;


                db.Prints.Add(f);
                db.SaveChanges();

            }
            report2 r = new report2();
            r.Show();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new WatcherSchedulingViewModel();
        }
    }
}
