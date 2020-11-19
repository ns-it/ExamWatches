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
    public class SchedulingFinalViewModel : ObservableObject
    {


        ExamWatchesDBContext db;
        public List<Watch> Watches { get; set; }
        public ObservableCollection<short> PeriodValues { get; set; }

        public List<DateTime?> WatchDates { get; set; }


        public ObservableCollection<WatchTableViewModel> WatchesList { get; set; }

        public ICollectionView WatchesListView { get; set; }



        public WorkLocation WorkLocation { get; set; }


        private Watch _selectedWatch;

        public Watch SelectedWatch
        {
            get { return _selectedWatch; }
            set
            {
                _selectedWatch = value; OnPropertyChanged("SelectedWatch"); 
                //OnPropertyChanged("SelectedWatchPeriod"); OnPropertyChanged("SelectedWatchDate"); OnPropertyChanged("SelectedWatchDuration");
                //SelectedWatchDuration = GetWatchDurationString();
                //OnPropertyChanged("WatchPeriodString");

            }
        }

        private DateTime? _selectedWatchDate;

        public DateTime? SelectedWatchDate
        {
            get { return _selectedWatchDate.GetValueOrDefault(); }
            set
            {
                _selectedWatchDate = value; OnPropertyChanged("SelectedWatchDate");
                //OnPropertyChanged("SelectedWatchDuration");
                //if (!WatchesListView.IsEmpty)
                { WatchesListView.Filter = new Predicate<object>(DateAndPeriodFilter); SelectedWatchDuration = GetWatchDurationString(); }
                
            }
        }


        private short? _selectedWatchPeriod;

        public short? SelectedWatchPeriod
        {
            get { return _selectedWatchPeriod.GetValueOrDefault(); }
            set
            {
                _selectedWatchPeriod = value; OnPropertyChanged("SelectedWatchPeriod");
                //OnPropertyChanged("SelectedWatch");
                //OnPropertyChanged("SelectedWatchDuration"); OnPropertyChanged("WatchPeriodString");
                
                //if(!WatchesListView.IsEmpty)
                { WatchesListView.Filter = new Predicate<object>(DateAndPeriodFilter); SelectedWatchDuration = GetWatchDurationString(); }
               
                
            }
        }
        private string _selectedWatchDuration;
        public string SelectedWatchDuration
        {
            get { return _selectedWatchDuration; }
            set { _selectedWatchDuration = value; 
                //OnPropertyChanged("SelectedWatch");
                OnPropertyChanged("SelectedWatchDuration"); }
        }

        //private string _wachPeriodString;

        //public string WatchPeriodString {
        //    get { return _wachPeriodString; }
        //    set { _wachPeriodString = value; OnPropertyChanged("WatchPeriodString"); }

        //}

        public WorkLocation UserWorkLocation { get; set; }
        public User CurrentUser { get; set; }
        public string CollegeName { get; set; }

        //List<DateTime> BlackoutDates { get; set; }

        public DateTime FirstDate { get; set; }
        public DateTime LastDate { get; set; }


        public SchedulingFinalViewModel()
        {
            //SelectedWatchDate = DateTime.Today;
            //SelectedWatchPeriod = 1;


            WatchesList = new ObservableCollection<WatchTableViewModel>();


            db = new ExamWatchesDBContext();

            int userid = Int32.Parse(App.Current.Properties["user_id"].ToString());
            CurrentUser = db.Users.Find(userid);
            UserWorkLocation = db.WorkLocations.Find(CurrentUser.WorkLocationId);
            CollegeName = CurrentUser.WorkLocation.Name;



            WatchDates = db.Watches.Select(x => x.WatchDate).Distinct().ToList();

            //DateTime first = watches.First().WatchDate.GetValueOrDefault();
            //DateTime last = watches.Last().WatchDate.GetValueOrDefault();

            //foreach(Watch watch in watches)
            //{
            //    if()
            //}
            PeriodValues = new ObservableCollection<short> { 1, 2, 3 };


            //WatchPeriod = Watches.FirstOrDefault();



            //SelectedWatch = new Watch()
            //{
            //    PeriodId = 1,
            //    Duration = 0,
            //    Exam = db.Exams.FirstOrDefault(),
            //    StartTime = new TimeSpan(9, 0, 0),
            //    Room = db.Rooms.FirstOrDefault()

            //};

            Watches = db.Watches
    .Include(w => w.Exam)
    //.Include(w => w.Room)
    .Include(w => w.WatcherWatches).ThenInclude(ww => ww.Watcher)
    .Where(
            w => w.Exam.WorkLocationId.Equals(CurrentUser.WorkLocationId)
           //&& w.WatchDate.Equals(SelectedWatchDate)
           //&& w.PeriodId.Equals(SelectedWatchPeriod)
           )
    .OrderByDescending(w => w.WatchDate).ToList();

            SelectedWatch = Watches.FirstOrDefault();



            WatchTableViewModel row;


            //WatcherWatch



            foreach (Watch w in Watches)
            {
                row = new WatchTableViewModel()
                {
                    Watch = w
                };


                WatchesList.Add(row);

                foreach (WatcherWatch ww in w.WatcherWatches)
                {
                    if (ww.WatcherType.Equals("1"))
                        //row.RoomChiefs += ww.Watcher.FullName + "\n";
                        row.RoomChiefsList.Add(ww.Watcher.FullName);
                    else if (ww.WatcherType.Equals("2"))
                        //row.RoomSecretaries += ww.Watcher.FullName + "\n";
                        row.RoomSecretariesList.Add(ww.Watcher.FullName);
                    else if (ww.WatcherType.Equals("3"))
                        //row.RoomWatchers += ww.Watcher.FullName + "\n";
                        row.RoomWatchersList.Add(ww.Watcher.FullName);
                }

                row.RoomChiefs = string.Join(", ", row.RoomChiefsList.ToArray());
                row.RoomSecretaries = string.Join(", ", row.RoomSecretariesList.ToArray());
                row.RoomWatchers = string.Join(", ", row.RoomWatchersList.ToArray());
            }

            WatchesListView = CollectionViewSource.GetDefaultView(WatchesList);

            //SelectedWatchDate = SelectedWatch.WatchDate;
            //SelectedWatchPeriod = SelectedWatch.PeriodId;
            //SelectedWatchDuration = GetWatchDurationString();

        }

        private string GetWatchDurationString()
        {
            List<WatchTableViewModel> WatchesListDemo = new List<WatchTableViewModel>() ;
            Watch watch =  new Watch() ;
            if (WatchesList.Count > 0)
                WatchesListDemo = WatchesList.Where(w => w.Watch.PeriodId.Equals(SelectedWatchPeriod) && w.Watch.WatchDate.Equals(SelectedWatchDate)).ToList(); 
            if(WatchesListDemo.Count>0)
                watch  = WatchesListDemo.FirstOrDefault().Watch;
            if (watch != null)
                    return watch.StartTime.GetValueOrDefault().ToString(@"hh\:mm")
                          + " - "
                          + (watch.StartTime.GetValueOrDefault()
                          + TimeSpan.FromMinutes(Decimal.ToDouble(watch.Duration.GetValueOrDefault()))).ToString(@"hh\:mm");
          
            return "";
        }

        public bool DateAndPeriodFilter(object de)
        {
            bool accepted = false;
            if (de is WatchTableViewModel watch)
            {
                accepted = watch.Watch.WatchDate.Equals(SelectedWatchDate) && watch.Watch.PeriodId.Equals(SelectedWatchPeriod);
                //accepted = student.Name.Contains(SearchField) || student.Address.Contains(SearchField);
            }
            return accepted;
        }

        //public bool PeriodFilter(object de)
        //{
        //    bool accepted = false;
        //    if (de is WatchTableViewModel watch)
        //    {
        //        accepted = watch.Watch.PeriodId.Equals(SelectedWatchPeriod);
        //        //accepted = student.Name.Contains(SearchField) || student.Address.Contains(SearchField);
        //    }
        //    return accepted;
        //}

    }
}
