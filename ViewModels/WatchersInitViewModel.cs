using ExamWatches.Commands;
using ExamWatches.Models;
using ExamWatches.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace ExamWatches.ViewModels
{
    public class WatchersInitViewModel : ObservableObject
    {

        // CONSTANT LISTS  

        public List<string> WorkTypes { get; set; }
        public List<string> Classes { get; set; }



        public ExamWatchesDBContext db;
        public ObservableCollection<WatcherManageViewModel> ItemsList { get; set; }
        public RelayCommand SelectedItemChangedCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand StartEditCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }


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

        private WatcherManageViewModel _currentItem;
        private WatcherManageViewModel _selectedItem;

        public WatcherManageViewModel SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged("SelectedItem"); }
        }

        public WatcherManageViewModel CurrentItem
        {
            get { return _currentItem; }
            set { _currentItem = value; OnPropertyChanged("CurrentItem"); }
        }



        public WorkLocation UserWorkLocation { get; set; }
        public User CurrentUser { get; set; }
        public string CollegeName { get; set; }

        public List<Watcher> Watchers { get; set; }


        public WatchersInitViewModel()
        {
            Classes = new List<string>() { "أولى", "ثانية", "ثالثة", "رابعة", "خامسة" };
            WorkTypes = new List<string>() { "صغيرة", "متوسطة", "كبيرة", "مدرج" };



            IsEditModeOn = false;
            IsEditModeOff = true;

            db = new ExamWatchesDBContext();

            int userid = Int32.Parse(App.Current.Properties["user_id"].ToString());
            CurrentUser = db.Users.Find(userid);
            UserWorkLocation = db.WorkLocations.Find(CurrentUser.WorkLocationId);
            CollegeName = CurrentUser.WorkLocation.Name;

            CurrentItem = new WatcherManageViewModel();

            Watchers = db.Watchers.Where(w => w.WorkLocationId.Equals(CurrentUser.WorkLocationId)).ToList();
            ItemsList = new ObservableCollection<WatcherManageViewModel>();
            foreach (Watcher w in Watchers)
            {
                ItemsList.Add(new WatcherManageViewModel(w));
            }

            ItemsList.CollectionChanged += ItemsList_CollectionChanged;

            SelectedItemChangedCommand = new RelayCommand(SelectedItemChangedAction);
            SaveCommand = new RelayCommand(SaveAction, null);
            StartEditCommand = new RelayCommand(StartEditAction, null);
            CancelCommand = new RelayCommand(StopEditAction, null);
            DeleteCommand = new RelayCommand(DeleteAction, null);

        }


        private void ItemsList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // FOR TESTING PURPOSES ONLY
            MessageBox.Show("ItemsList_CollectionChanged");
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


        public void SaveAction(object selectedItem)
        {

            //Edit Action
            if (!CurrentItem.Id.Equals(0))
            {
                SelectedItem.Id = CurrentItem.Id;
                SelectedItem.FirstName = CurrentItem.FirstName;
                SelectedItem.MiddleName = CurrentItem.MiddleName;
                SelectedItem.LastName = CurrentItem.LastName;
                SelectedItem.FullName = CurrentItem.FullName;
                SelectedItem.Job = CurrentItem.Job;
                SelectedItem.Class = CurrentItem.Class;
                SelectedItem.Certificate = CurrentItem.Certificate;
                //SelectedItem.WorkLocationId = CurrentItem.WorkLocationId;

                db.SaveChanges();
            }
            else
            {
                WatcherManageViewModel watcher = new WatcherManageViewModel()
                {
                    FirstName = CurrentItem.FirstName,
                    MiddleName = CurrentItem.MiddleName,
                    LastName = CurrentItem.LastName,
                    //FullName = CurrentItem.FullName,
                    Job = CurrentItem.Job,
                    Class = CurrentItem.Class,
                    Certificate = CurrentItem.Certificate,
                    WorkLocationId = UserWorkLocation.Id
                };

                ItemsList.Add(watcher);
                db.Watchers.Add(watcher.Model);



                //ItemsList.Add(new WatcherManageViewModel(watcher));
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
                CurrentItem.FirstName = "";
                CurrentItem.MiddleName = "";
                CurrentItem.LastName = "";
                CurrentItem.FullName = "";
                CurrentItem.Job = "";
                CurrentItem.Class = null;
                CurrentItem.Certificate = "";
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
                CurrentItem.FirstName = SelectedItem.FirstName;
                CurrentItem.MiddleName = SelectedItem.MiddleName;
                CurrentItem.LastName = SelectedItem.LastName;
                CurrentItem.FullName = SelectedItem.FullName;
                CurrentItem.Job = SelectedItem.Job;
                CurrentItem.Class = SelectedItem.Class;
                CurrentItem.Certificate = SelectedItem.Certificate;
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
                db.Watchers.Remove(SelectedItem.Model);
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
                CurrentItem.FirstName = SelectedItem.FirstName;
                CurrentItem.MiddleName = SelectedItem.MiddleName;
                CurrentItem.LastName = SelectedItem.LastName;
                CurrentItem.FullName = SelectedItem.FullName;
                CurrentItem.Job = SelectedItem.Job;
                CurrentItem.Class = SelectedItem.Class;
                CurrentItem.Certificate = SelectedItem.Certificate;
            }

            else
            {
                CurrentItem.Id = 0;
                CurrentItem.FirstName = "";
                CurrentItem.MiddleName = "";
                CurrentItem.LastName = "";
                CurrentItem.FullName = "";
                CurrentItem.Job = "";
                CurrentItem.Class = null;
                CurrentItem.Certificate = "";
            }

        }
    }
}
