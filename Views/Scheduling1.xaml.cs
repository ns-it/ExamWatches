using ExamWatches.Models;
using ExamWatches.ViewModels;
using System;
using System.Collections.Generic;
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

namespace ExamWatches.Views
{
    /// <summary>
    /// Interaction logic for Scheduling1.xaml
    /// </summary>
    public partial class Scheduling1 : UserControl
    {
        ExamWatchesDBContext db = new ExamWatchesDBContext();
        WorkLocation wl = new WorkLocation();
        Watcher wch = new Watcher();

        public static List<WatcherViewModel> SelectedWatcher;

        public Scheduling1()
        {
            InitializeComponent();
            initialEmpListBox();
            initialRoomListBox();
            //  watcherViewModels = new List<WatcherViewModel>();
            SelectedWatcher = new List<WatcherViewModel>();

        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            Scheduling2 scheduling2 = new Scheduling2();
            this.Content = scheduling2.Content;
        }



        public void initialEmpListBox()
        {


            var list = db.WorkLocations.ToList();

            foreach (var item in list)
            {

                employeelist.Items.Add(item.Name);
            }
        }

        public void initialRoomListBox()
        {

            var list = db.WorkLocations.ToList();

            foreach (var item in list)
            {

                roomlist.Items.Add(item.Name);
            }

        }



        private void employeelist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<WatcherViewModel> watcherViewModels = new List<WatcherViewModel>();
            wl = db.WorkLocations.Where(x => x.Name == employeelist.SelectedItem).FirstOrDefault();

            // Employees.ItemsSource = db.Watchers.Where(x => x.WorkLocationId == wl.Id).ToList();
            List<Watcher> watchers = db.Watchers.Where(x => x.WorkLocationId == wl.Id).ToList();

            foreach (Watcher w in watchers)
            {

                watcherViewModels.Add(new WatcherViewModel { FullName = w.FullName, Class = w.Class, IsSelected = false });

            }
            Employees.ItemsSource = watcherViewModels;


        }

        private void roomlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            wl = db.WorkLocations.Where(x => x.Name == roomlist.SelectedItem).FirstOrDefault();

            RoomsGrid.ItemsSource = db.Rooms.Where(x => x.WorkLocationId == wl.Id).ToList();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //  List<WatcherViewModel> selectedWatchers = new List<WatcherViewModel>();
            foreach (WatcherViewModel model in Employees.ItemsSource)
            {
                if (model.IsSelected)
                {
                    // if(  !SelectedWatcher.Contains(model))
                    SelectedWatcher.Add(model);

                }
            }




        }



    }
}
