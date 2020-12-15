using ExamWatches.Models;
using ExamWatches.ViewModels;
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

        public static ObservableCollection<WatcherViewModel> SelectedWatcher;
        public static ObservableCollection<RoomView> SelectedRooms;

        public Scheduling1()
        {
            InitializeComponent();
            initialEmpListBox();
            initialRoomListBox();
            //  watcherViewModels = new List<WatcherViewModel>();
            SelectedWatcher = new ObservableCollection<WatcherViewModel>();
            SelectedRooms = new ObservableCollection<RoomView>();


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
            wl = db.WorkLocations.Where(x => x.Name == employeelist.SelectedItem.ToString()).FirstOrDefault();

            // Employees.ItemsSource = db.Watchers.Where(x => x.WorkLocationId == wl.Id).ToList();
            List<Watcher> watchers = db.Watchers.Where(x => x.WorkLocationId == wl.Id).ToList();
            if (SelectedWatcher.Count() == 0)
            {
                foreach (Watcher w in watchers)
                    watcherViewModels.Add(new WatcherViewModel { Id = w.Id, FullName = w.FullName + " - ف " + w.Class, Class = w.Class, IsSelected = false });

            }
            else
            {

                foreach (Watcher w in watchers)
                {
                    Boolean b = false;

                    foreach (WatcherViewModel wvm in SelectedWatcher)
                    {

                        if (w.Id == wvm.Id)
                        {
                            b = true;
                            // watcherViewModels.Add(new WatcherViewModel { FullName = w.FullName, Class = w.Class, IsSelected = true });
                        }
                    }

                    if (b == true)
                        watcherViewModels.Add(new WatcherViewModel { Id = w.Id, FullName = w.FullName + " - ف " + w.Class, Class = w.Class, IsSelected = true });
                    else
                        watcherViewModels.Add(new WatcherViewModel { Id = w.Id, FullName = w.FullName + " - ف " + w.Class, Class = w.Class, IsSelected = false });




                }
            }

            Employees.ItemsSource = watcherViewModels;


        }

        private void roomlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<RoomView> roomViewModels = new List<RoomView>();
            wl = db.WorkLocations.Where(x => x.Name == roomlist.SelectedItem.ToString()).FirstOrDefault();

            //RoomsGrid.ItemsSource = db.Rooms.Where(x => x.WorkLocationId == wl.Id).ToList();
            List<Room> rooms = db.Rooms.Where(x => x.WorkLocationId == wl.Id).ToList();

            if (SelectedRooms.Count() == 0)
            {
                foreach (Room w in rooms)
                    roomViewModels.Add(new RoomView { Id=w.Id, Name = w.Name, Type = w.Type, Capacity = w.Capacity, WorkLocation = w.WorkLocationId, IsSelected = false });

            }
            else
            {

                foreach (Room w in rooms)
                {
                    Boolean b = false;

                    foreach (RoomView wvm in SelectedRooms)
                    {

                        if (w.Name == wvm.Name && w.WorkLocationId == wvm.WorkLocation)
                        {
                            b = true;
                            // watcherViewModels.Add(new WatcherViewModel { FullName = w.FullName, Class = w.Class, IsSelected = true });
                        }
                    }

                    if (b == true)
                        roomViewModels.Add(new RoomView { Id=w.Id, Name = w.Name, Type = w.Type, Capacity = w.Capacity, WorkLocation = w.WorkLocationId, IsSelected = true });
                    else
                        roomViewModels.Add(new RoomView {Id=w.Id, Name = w.Name, Type = w.Type, Capacity = w.Capacity, WorkLocation = w.WorkLocationId, IsSelected = false });




                }
            }
            RoomsGrid.ItemsSource = roomViewModels;
        }

        private void addWtcher_button(object sender, RoutedEventArgs e)
        {
            WatcherViewModel watcherView = Employees.SelectedItem as WatcherViewModel;
            Boolean b = false;
            foreach (WatcherViewModel wv in SelectedWatcher)
            {
                if (wv.Id == watcherView.Id)
                {

                    b = true;
                }
            }
            if (b == true)
                MessageBox.Show("هذا العنصر مضاف مسبقا");
            else
            {
                watcherView.IsSelected = true;
                SelectedWatcher.Add(watcherView);
            }

        }

        private void deleteWtcher_button(object sender, RoutedEventArgs e)
        {
            WatcherViewModel watcherView = Employees.SelectedItem as WatcherViewModel;

            Boolean b = false;
            foreach (WatcherViewModel wv in SelectedWatcher)
            {


                if (wv.Id == watcherView.Id)
                {

                    b = true;
                }



            }
            if (b == true)
            {
                foreach (WatcherViewModel slw in SelectedWatcher.ToList())
                {
                    if (slw.Id == watcherView.Id)
                    {

                        SelectedWatcher.Remove(slw);
                        watcherView.IsSelected = false;
                        MessageBox.Show("تم");
                        if (Scheduling2.BossWatcherList.Contains(slw))
                            Scheduling2.BossWatcherList.Remove(slw);
                        if (Scheduling2.SecretaryWatchersList.Contains(slw))
                            Scheduling2.SecretaryWatchersList.Remove(slw);
                        if (Scheduling2.OrdinaryWatchersList.Contains(slw))
                            Scheduling2.OrdinaryWatchersList.Remove(slw);

                    }
                }
            }
            else
                MessageBox.Show("هذا العنصر غير مضاف مسبقا");

        }

        private void addRoom_button(object sender, RoutedEventArgs e)
        {
            RoomView roomViewModel = RoomsGrid.SelectedItem as RoomView;
            Boolean b = false;
            foreach (RoomView wv in SelectedRooms)
            {


                if (wv.Name == roomViewModel.Name && wv.WorkLocation == roomViewModel.WorkLocation)
                {

                    b = true;
                }



            }
            if (b == true)
                MessageBox.Show("هذا العنصر مضاف مسبقا");

            else
            {
                roomViewModel.IsSelected = true;
                // MessageBox.Show(roomViewModel.Name.ToString());
                SelectedRooms.Add(roomViewModel);

            }

        }

        private void deleteRoom_button(object sender, RoutedEventArgs e)
        {
            RoomView watcherView = RoomsGrid.SelectedItem as RoomView;

            Boolean b = false;
            foreach (RoomView wv in SelectedRooms)
            {


                if (wv.Name == watcherView.Name && wv.WorkLocation == watcherView.WorkLocation)
                {

                    b = true;
                }



            }
            if (b == true)
            {
                foreach (RoomView slw in SelectedRooms.ToList())
                {
                    if (slw.Name == watcherView.Name && slw.WorkLocation == watcherView.WorkLocation)
                    {

                        SelectedRooms.Remove(slw);
                        watcherView.IsSelected = false;
                        MessageBox.Show("تم");
                    }
                }
            }
            else
                MessageBox.Show("هذا العنصر غير مضاف مسبقا");

        }



    }
}
