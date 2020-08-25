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
        public ObservableCollection<RoomViewModel> RoomsList { get; set; }
        //

        WorkLocation location;
        User user;

        public ExamWatchesDBContext db;

        private RoomViewModel _currentRoom;
        private RoomViewModel _selectedRoom;

        private bool isEditModeOn;
        private bool isEditModeOff;

        public RelayCommand SelectedItemChangedCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand StartEditCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }


        //public List<RoomViewModel> RoomsList { get; set; }
        public List<Room> Rooms { get; set; }
        public string CollegeName { get; set; }

        public List<string> Types { get; set; }


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
        public RoomViewModel SelectedRoom
        {
            get { return _selectedRoom; }
            set { _selectedRoom = value; OnPropertyChanged("SelectedRoom"); }
        }

        public RoomViewModel CurrentRoom
        {
            get { return _currentRoom; }
            set { _currentRoom = value; OnPropertyChanged("CurrentRoom"); }
        }

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
            user = db.Users.Find(userid);
            location = db.WorkLocations.Find(user.WorkLocationId);
            CollegeName = user.WorkLocation.Name;
            CurrentRoom = new RoomViewModel();

            //CollegeName = db.WorkLocations.Find();
            //db.Rooms.Load();
            //RoomsList = db.Rooms.Local.ToObservableCollection();

            Rooms = db.Rooms.Where(s => s.WorkLocationId.Equals(user.WorkLocationId)).ToList();
            RoomsList = new ObservableCollection<RoomViewModel>();
            foreach (Room r in Rooms)
            {
                RoomsList.Add(new RoomViewModel(r));
            }

            RoomsList.CollectionChanged += RoomsList_CollectionChanged;


            Types = new List<string>();
            Types.AddRange(new List<string>() { "صغيرة", "متوسطة", "كبيرة", "مدرج" });

            SelectedItemChangedCommand = new RelayCommand(SelectedItemChangedAction);
            SaveCommand = new RelayCommand(SaveAction, null);
            StartEditCommand = new RelayCommand(StartEditAction, null);
            CancelCommand = new RelayCommand(StopEditAction, null);
            DeleteCommand = new RelayCommand(DeleteAction, null);
        }


        public void SaveAction(object selectedItem)
        {

            //Edit Action
            if (!CurrentRoom.Id.Equals(0))
            {
                SelectedRoom.Name = CurrentRoom.Name;
                SelectedRoom.Type = CurrentRoom.Type;
                SelectedRoom.Capacity = CurrentRoom.Capacity;

                db.SaveChanges();
            }
            else
            {
                //Room r = db.Rooms.Add(new Room() { Name = CurrentRoom.Name, Capacity = CurrentRoom.Capacity , Type = CurrentRoom.Room.Type, WorkLocationId = location.Id}).Entity;
                RoomViewModel room = new RoomViewModel()
                {
                    Name = CurrentRoom.Name,
                    Capacity = CurrentRoom.Capacity,
                    Type = CurrentRoom.Type,
                    WorkLocationId = location.Id
                };
                RoomsList.Add(room);
                db.Rooms.Add(room.Room);
                db.SaveChanges();

                //StudentsListView.Refresh();

                SelectedRoom = null;
            }

            IsEditModeOff = true;
            IsEditModeOn = false;

        }



        public void StartEditAction(object button)
        {

            if (button.ToString().Equals("Add"))
            {
                CurrentRoom.Id = 0;
                CurrentRoom.Name = "";
                CurrentRoom.Type = "";
                CurrentRoom.Capacity = 0;
            }
            IsEditModeOff = false;
            IsEditModeOn = true;

        }

        public void StopEditAction(object selectedItem)
        {
            IsEditModeOff = true;
            IsEditModeOn = false;

            if (SelectedRoom != null)
            {
                CurrentRoom.Id = SelectedRoom.Id;
                CurrentRoom.Name = SelectedRoom.Name;
                CurrentRoom.Type = SelectedRoom.Type;
                CurrentRoom.Capacity = SelectedRoom.Capacity;
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
                db.Rooms.Remove(SelectedRoom.Room);
                RoomsList.Remove(SelectedRoom);
                db.SaveChanges();
                SelectedRoom = null;
            }

          

        }



        public void SelectedItemChangedAction(object o)
        {
            if (SelectedRoom != null)
            {
                CurrentRoom.Id = SelectedRoom.Id;
                CurrentRoom.Name = SelectedRoom.Name;
                CurrentRoom.Type = SelectedRoom.Type;
                CurrentRoom.Capacity = SelectedRoom.Capacity;
            }

            else
            {
                CurrentRoom.Id = 0;
                CurrentRoom.Name = "";
                CurrentRoom.Type = null;
                CurrentRoom.Capacity = 0;
            }

        }


        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add
            {
                ((INotifyCollectionChanged)RoomsList).CollectionChanged += value;
            }

            remove
            {
                ((INotifyCollectionChanged)RoomsList).CollectionChanged -= value;
            }
        }
        private void RoomsList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

            MessageBox.Show("StudentsList_CollectionChanged");
        }

        //protected void OnPropertyChanaged(string name)
        //{
        //    if(PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(name));
        //    }
        //}

    }
}
