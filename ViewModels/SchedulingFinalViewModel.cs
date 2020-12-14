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

        private ObservableCollection<short?> _periodValues;
        public ObservableCollection<short?> PeriodValues
        {
            get { return _periodValues; }
            set
            {
                _periodValues = value; OnPropertyChanged("PeriodValues");
            }
        }


        public ObservableCollection<DateTime?> WatchDates { get; set; }


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
               
                PeriodValues = new ObservableCollection<short?>( db.Watches.Where(w=>w.WatchDate.Equals(value)).Select(x => x.PeriodId).Distinct().ToList());
                OnPropertyChanged("PeriodValues");
                //OnPropertyChanged("SelectedWatchDuration");
                //if (!WatchesListView.IsEmpty)
                {
                    WatchesListView.Filter = new Predicate<object>(DateAndPeriodFilter);
                    SelectedWatchDuration = GetWatchDurationString();
                }

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
            set
            {
                _selectedWatchDuration = value;
                //OnPropertyChanged("SelectedWatch");
                OnPropertyChanged("SelectedWatchDuration");
            }
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
        //public List<WatcherWatch> WatcherWatchesList { get; set; }
        public short CurrentExamId { get; set; }


        public SchedulingFinalViewModel()

        {
            CurrentExamId = Views.ExamInit.examID;
            db = new ExamWatchesDBContext();
            int userid = Int32.Parse(App.Current.Properties["user_id"].ToString());
            CurrentUser = db.Users.Find(userid);
            UserWorkLocation = db.WorkLocations.Find(CurrentUser.WorkLocationId);
            CollegeName = CurrentUser.WorkLocation.Name;
            WatchDates = new ObservableCollection<DateTime?>();
            PeriodValues = new ObservableCollection<short?>();
            WatchesList = new ObservableCollection<WatchTableViewModel>();

            Initialization();

            WatchesListView = CollectionViewSource.GetDefaultView(WatchesList);
            SelectedWatchDate = WatchDates.FirstOrDefault();
            SelectedWatchPeriod = PeriodValues.FirstOrDefault();

        }

        public void Initialization()
        {

            WatchDates.Clear();
            PeriodValues.Clear();
            WatchesList.Clear();


            Watches = db.Watches
            .Include(w => w.Exam)
            .Include(w => w.WatcherWatches).ThenInclude(ww => ww.Room)
            .Include(w => w.WatcherWatches).ThenInclude(ww => ww.Watcher)
           .Where(
               w => w.ExamId == CurrentExamId
          )
          .OrderByDescending(w => w.WatchDate).ToList();

            SelectedWatch = Watches.FirstOrDefault();

            foreach (DateTime? d in Watches.Select(x => x.WatchDate).Distinct().ToList())
            {
                WatchDates.Add(d);
            }
            foreach (short? p in Watches.Select(x => x.PeriodId).Distinct().ToList())
            {
                PeriodValues.Add(p);
            }            
            //WatcherWatchesList = new List<WatcherWatch>();

            WatchTableViewModel row;

            foreach (DateTime date in WatchDates)
                foreach (short p in PeriodValues)
                    foreach (Watch w in Watches)
                    {
                        if (w.PeriodId.Equals(p) && w.WatchDate.Equals(date))
                        {

                            var RoomsWatches =
                                w.WatcherWatches.GroupBy(ww => ww.RoomId)
                                .Select(grp => grp.ToList());

                            foreach (var r in RoomsWatches)
                            {
                                row = new WatchTableViewModel()
                                {
                                    Room = db.Rooms.Find(r.FirstOrDefault().RoomId),
                                    Watch = w
                                };
                                foreach (var ww in r)
                                {
                                    row.WatcherWatchesList.Add(ww);

                                    if (ww.WatcherType.Equals("1"))
                                        row.RoomChiefsList.Add(ww.Watcher.FullName);
                                    else if (ww.WatcherType.Equals("2"))
                                        row.RoomSecretariesList.Add(ww.Watcher.FullName);
                                    else if (ww.WatcherType.Equals("3"))
                                        row.RoomWatchersList.Add(ww.Watcher.FullName);
                                }

                                row.RoomChiefs = string.Join(", ", row.RoomChiefsList.ToArray());
                                row.RoomSecretaries = string.Join(", ", row.RoomSecretariesList.ToArray());
                                row.RoomWatchers = string.Join(", ", row.RoomWatchersList.ToArray());

                                WatchesList.Add(row);
                            }
                        }
                    }

        }


        private string GetWatchDurationString()
        {
            List<WatchTableViewModel> WatchesListDemo = new List<WatchTableViewModel>();
            Watch watch = new Watch();
            if (WatchesList.Count > 0)
                WatchesListDemo = WatchesList.Where(w => w.Watch.PeriodId.Equals(SelectedWatchPeriod) && w.Watch.WatchDate.Equals(SelectedWatchDate)).ToList();
            if (WatchesListDemo.Count > 0)
                watch = WatchesListDemo.FirstOrDefault().Watch;
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
