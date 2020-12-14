using ExamWatches.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace ExamWatches.ViewModels
{
    public class WatcherSchedulingViewModel : ObservableObject
    {
        //private Watcher _watcher;
        //private 

        private ExamWatchesDBContext db;
        public WorkLocation UserWorkLocation { get; set; }
        public User CurrentUser { get; set; }
        public string CollegeName { get; set; }
        public short CurrentExamId { get; set; }

        private Watcher _selectedWatcher;
        public Watcher SelectedWatcher
        {
            get { return _selectedWatcher; }
            set
            {
                _selectedWatcher = value; OnPropertyChanged("SelectedWatcher");
                //OnPropertyChanged("SelectedWatch");
                //OnPropertyChanged("SelectedWatchDuration"); OnPropertyChanged("WatchPeriodString");

                //if(!WatchesListView.IsEmpty)
                { WatchesListView.Filter = new Predicate<object>(WatcherFilter); }

            }

        }
        public string SelectedWatcherName { get; set; }
        public string SelectedWatcherWorkLocation { get; set; }
        public ObservableCollection<WatcherWatchTableViewModel> WatchesList { get; set; }
        public List<WatcherWatch> WatcherWatches { get; set; }

        public List<Watcher> Watchers { get; set; }

        public ICollectionView WatchesListView { get; set; }
        public bool WatcherFilter(object de)
        {
            bool accepted = false;
            if (de is WatcherWatchTableViewModel watch)
            {
                accepted = watch.Watcher.Equals(SelectedWatcher);
                //accepted = student.Name.Contains(SearchField) || student.Address.Contains(SearchField);
            }
            return accepted;
        }

        public WatcherSchedulingViewModel()
        {
            db = new ExamWatchesDBContext();
            int userid = Int32.Parse(App.Current.Properties["user_id"].ToString());
            CurrentUser = db.Users.Find(userid);
            UserWorkLocation = db.WorkLocations.Find(CurrentUser.WorkLocationId);
            CollegeName = CurrentUser.WorkLocation.Name;

            CurrentExamId = Views.ExamInit.examID;

            WatcherWatches = db.WatcherWatches.Include(x => x.Watcher).Include(ww=>ww.Room).ThenInclude(x => x.WorkLocation).Include(x => x.Watch)
                //.ThenInclude(x => x.Room)
                .Where(x => x.Watch.ExamId.Equals(CurrentExamId)).ToList();

            Watchers = WatcherWatches.Select(w => w.Watcher).Distinct().ToList();


            WatchesList = new ObservableCollection<WatcherWatchTableViewModel>();

            WatcherWatchTableViewModel row;
            List<string> demoList;
            foreach (WatcherWatch ww in WatcherWatches)
            {
                demoList = new List<string>();
                WatchesList.Add(row = new WatcherWatchTableViewModel()
                {
                    Watch = ww.Watch,
                    Watcher = ww.Watcher,
                    WatchRoom = ww.Room,
                    //WatchType = ww.WatcherType,
                    //WatchDate = ww.Watch.WatchDate,
                    //WatchPeriodId = ww.Watch.PeriodId,
                    Attendence = ww.Attendence

                });

                if (ww.WatcherType.Equals("1"))
                    row.WatchType = "رئيس قاعة";
                else if (ww.WatcherType.Equals("2"))
                    row.WatchType = "أمين سر";
                else if (ww.WatcherType.Equals("3"))
                    row.WatchType = "مراقب";


                foreach (WatcherWatch x in db.WatcherWatches.Where(w => w.WatchId.Equals(ww.WatchId)&&w.RoomId.Equals(ww.RoomId)).Include(x => x.Watcher).ToList())
                {
                    if (x.WatcherType.Equals("1"))
                    {
                        demoList.Add(x.Watcher.FullName);
                    }

                    //if(w.Id.Equals(ww.WatchId)&& ww.WatcherType.Equals("1"))
                    //{
                    //    row.RoomChief= w.
                    //}
                }

                row.RoomChief = string.Join(", ", demoList.ToArray());
            }

            WatchesListView = CollectionViewSource.GetDefaultView(WatchesList);
            SelectedWatcher = Watchers.FirstOrDefault();
            //db.WatcherWatches.Where()


        }



    }
}
