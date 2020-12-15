using ExamWatches.Models;
using ExamWatches.Utilities;
using ExamWatches.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            List<Final> flist = new List<Final>();
            flist = db.Finals.ToList<Final>();

            foreach (Final i in flist) {
                db.Finals.Remove(i);
                db.SaveChanges();          
            }

            foreach (WatchTableViewModel wtvm in WatchesSchedule.Items)
            {
                Final f = new Final();
                DateTime dtt =(DateTime) date.SelectedValue;

                f.Date = dtt.ToString("dd/MM/yyyy");
                f.Period = period.Text;
                f.Roomname = wtvm.Room.Name;
                f.RoomChief = wtvm.RoomChiefs;
                f.RoomSecretarie = wtvm.RoomSecretaries;
                f.RoomWatcher = wtvm.RoomWatchers;
                f.StartTime = time.Text;
                f.dean = ExamInit.deanName;
                f.manager =ExamInit.wl;

                db.Finals.Add(f);
                db.SaveChanges();

            }
            report1 r = new report1();
            r.Show();
        }

        private void WatchesSchedule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new SchedulingFinalViewModel();
        }
    }
}
