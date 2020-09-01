using ExamWatches.Commands;
using ExamWatches.Models;
using ExamWatches.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace ExamWatches.ViewModels
{
    //public class RoomsInitViewModel : INotifyPropertyChanged
    public class RoomsInitViewModel : ObservableObject, INotifyCollectionChanged
    {
        public ExamWatchesDBContext db;
        public ObservableCollection<RoomViewModel> ItemsList { get; set; }

        public RelayCommand SelectedItemChangedCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand StartEditCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }

        private RoomViewModel _currentItem;
        private RoomViewModel _selectedItem;

        public RoomViewModel SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged("SelectedRoom"); }
        }

        public RoomViewModel CurrentItem
        {
            get { return _currentItem; }
            set { _currentItem = value; OnPropertyChanged("CurrentRoom"); }
        }

        private bool isEditModeOn;
        private bool isEditModeOff;

        public bool IsEditModeOn
        {
            get { return isEditModeOn; }
            set { isEditModeOn = value; OnPropertyChanged("IsEditModeOn"); }
        }
        public bool IsEditModeOff
        {
            get { return isEditModeOff; }
            set { isEditModeOff = value; OnPropertyChanged("IsEditModeOff"); }
        }


        //public List<RoomViewModel> RoomsList { get; set; }
        public List<Room> Rooms { get; set; }
        public List<string> Types { get; set; }
        public WorkLocation UserWorkLocation { get; set; }
        public User CurrentUser { get; set; }
        public string CollegeName { get; set; }



        //private ObservableCollection<Room> _roomsList;

        //public event PropertyChangedEventHandler PropertyChanged;

        //public ObservableCollection<Room> RoomsList
        //{
        //    get { return _roomsList; }
        //    set { _roomsList = value; OnPropertyChanaged("RoomsList"); }
        //}






        public RoomsInitViewModel()
        {
            IsEditModeOn = false;
            IsEditModeOff = true;

            db = new ExamWatchesDBContext();

            int userid = Int32.Parse(App.Current.Properties["user_id"].ToString());
            CurrentUser = db.Users.Find(userid);
            UserWorkLocation = db.WorkLocations.Find(CurrentUser.WorkLocationId);
            CollegeName = CurrentUser.WorkLocation.Name;
            CurrentItem = new RoomViewModel();

            //CollegeName = db.WorkLocations.Find();
            //db.Rooms.Load();
            //RoomsList = db.Rooms.Local.ToObservableCollection();

            Rooms = db.Rooms.Where(s => s.WorkLocationId.Equals(CurrentUser.WorkLocationId)).ToList();
            ItemsList = new ObservableCollection<RoomViewModel>();
            foreach (Room r in Rooms)
            {
                ItemsList.Add(new RoomViewModel(r));
            }

            ItemsList.CollectionChanged += ItemsList_CollectionChanged;


            Types = new List<string>() { "صغيرة", "متوسطة", "كبيرة", "مدرج" };

            SelectedItemChangedCommand = new RelayCommand(SelectedItemChangedAction);
            SaveCommand = new RelayCommand(SaveAction, null);
            StartEditCommand = new RelayCommand(StartEditAction, null);
            CancelCommand = new RelayCommand(StopEditAction, null);
            DeleteCommand = new RelayCommand(DeleteAction, null);
        }


        public void SaveAction(object selectedItem)
        {

            //Edit Action
            if (!CurrentItem.Id.Equals(0))
            {
                SelectedItem.Name = CurrentItem.Name;
                SelectedItem.Type = CurrentItem.Type;
                SelectedItem.Capacity = CurrentItem.Capacity;

                db.SaveChanges();
            }
            else
            {
                //Room r = db.Rooms.Add(new Room() { Name = CurrentRoom.Name, Capacity = CurrentRoom.Capacity , Type = CurrentRoom.Room.Type, WorkLocationId = location.Id}).Entity;
                RoomViewModel room = new RoomViewModel()
                {
                    Name = CurrentItem.Name,
                    Capacity = CurrentItem.Capacity,
                    Type = CurrentItem.Type,
                    WorkLocationId = UserWorkLocation.Id
                };
                ItemsList.Add(room);
                db.Rooms.Add(room.Model);
                db.SaveChanges();

                //StudentsListView.Refresh();

                SelectedItem = null;
            }

            IsEditModeOff = true;
            IsEditModeOn = false;

        }



        public void StartEditAction(object button)
        {

            if (button.ToString().Equals("Add"))
            {
                CurrentItem.Id = 0;
                CurrentItem.Name = "";
                CurrentItem.Type = "";
                CurrentItem.Capacity = 0;
            }
            IsEditModeOff = false;
            IsEditModeOn = true;

        }

        public void StopEditAction(object selectedItem)
        {
            IsEditModeOff = true;
            IsEditModeOn = false;

            if (SelectedItem != null)
            {
                CurrentItem.Id = SelectedItem.Id;
                CurrentItem.Name = SelectedItem.Name;
                CurrentItem.Type = SelectedItem.Type;
                CurrentItem.Capacity = SelectedItem.Capacity;
            }
        }


        public void DeleteAction(object item)
        {

            ConfirmationDialog confirmation = new ConfirmationDialog()
            {
                FlowDirection = FlowDirection.RightToLeft,
                DataContext = new ConfirmationDialogViewModel("Confirmation"),
                Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#BF4545")),
                Foreground = Brushes.GhostWhite,

                BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#99BF4545")),
                BorderThickness = new Thickness(5)
            };
            confirmation.ShowDialog();
            if (confirmation.DialogResult == true)
            {
                db.Rooms.Remove(SelectedItem.Model);
                ItemsList.Remove(SelectedItem);
                db.SaveChanges();
                SelectedItem = null;
            }



        }



        public void SelectedItemChangedAction(object o)
        {
            if (SelectedItem != null)
            {
                CurrentItem.Id = SelectedItem.Id;
                CurrentItem.Name = SelectedItem.Name;
                CurrentItem.Type = SelectedItem.Type;
                CurrentItem.Capacity = SelectedItem.Capacity;
            }

            else
            {
                CurrentItem.Id = 0;
                CurrentItem.Name = "";
                CurrentItem.Type = null;
                CurrentItem.Capacity = 0;
            }

        }


        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add
            {
                ((INotifyCollectionChanged)ItemsList).CollectionChanged += value;
            }

            remove
            {
                ((INotifyCollectionChanged)ItemsList).CollectionChanged -= value;
            }
        }
        private void ItemsList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

            MessageBox.Show("ItemsList_CollectionChanged");
        }



    }
}
