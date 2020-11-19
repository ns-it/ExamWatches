using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using ExamWatches.Models;
using ExamWatches.ViewModels;

namespace ExamWatches.Views
{
    /// <summary>
    /// Interaction logic for Scheduling3.xaml
    /// </summary>
    public partial class Scheduling3 : UserControl
    {
        ObservableCollection<fillFirstDataGridmodel> FirstDataGramData;
        ObservableCollection<Scheduling3ViewModel> SecondDataGramData;
        public ObservableCollection<int> periodList;
        public ObservableCollection<decimal> periodDurationList;
        DateTime selelectedDate;

        short EXAM_ID = 211;
        short WORK_LOCATION_ID;

        TimeSpan startTime;
        public WorkLocation UserWorkLocation { get; set; }
        public User CurrentUser { get; set; }

        Scheduling3ViewModel svm3;

        fillFirstDataGridmodel fillFirstDataGridmodel;

        public ExamWatchesDBContext db;


        public Scheduling3()
        {
            FirstDataGramData = new ObservableCollection<fillFirstDataGridmodel>();
            SecondDataGramData = new ObservableCollection<Scheduling3ViewModel>();
            periodList = new ObservableCollection<int>
            {
                1,
                2,
                3
            };
            periodDurationList = new ObservableCollection<decimal>();
            periodDurationList.Add(new decimal(60));
            periodDurationList.Add(new decimal(90));
            periodDurationList.Add(new decimal(120));
            periodDurationList.Add(new decimal(150));

            periodDurationList.Add(new decimal(180));
            periodDurationList.Add(new decimal(210));
            periodDurationList.Add(new decimal(240));







            InitializeComponent();

            Periods.ItemsSource = FirstDataGramData;

            Periods2.ItemsSource = SecondDataGramData;

            db = new ExamWatchesDBContext();

            int userid = Int32.Parse(App.Current.Properties["user_id"].ToString());
            CurrentUser = db.Users.Find(userid);
            UserWorkLocation = db.WorkLocations.Find(CurrentUser.WorkLocationId);
            WORK_LOCATION_ID = UserWorkLocation.Id;

            //ComboBox cb= Periods.Columns[1].GetCellContent(Periods.Items[0]) as ComboBox;

        }



        public void ExamDay_selectionChange_Loop()
        {



            fillFirstDataGridmodel = new fillFirstDataGridmodel();
            fillFirstDataGridmodel.dayDate = selelectedDate;

            fillFirstDataGridmodel.DateNumList = periodList;
            fillFirstDataGridmodel.SelectedNum = 1;
            FirstDataGramData.Add(fillFirstDataGridmodel);


        }




        private void ExamDays_SelectedDatesChanged_2(object sender, SelectionChangedEventArgs e)
        {

            selelectedDate = (DateTime)ExamDays.SelectedDate;

            ExamDay_selectionChange_Loop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (fillFirstDataGridmodel fm in FirstDataGramData)
            {
                for (short i = 1; i <= fm.SelectedNum; i++)
                {

                    Scheduling3ViewModel SV = new Scheduling3ViewModel();
                    SV.day = fm.dayDate;
                    SV.periodID = i;
                    MessageBox.Show(fm.SelectedNum.ToString());
                    SV.startTime = startTime;
                    SV.periodDuration = periodDurationList;
                    SecondDataGramData.Add(SV);

                }


            }

        }

        private void addToSecondGrid(object sender, RoutedEventArgs e)
        {
            DataRowView dg = Periods.SelectedItem as DataRowView;
            MessageBox.Show(dg["اليوم"].ToString());
        }

        List<Watch> Watches = new List<Watch>();
        Watch w;


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {



            foreach (Scheduling3ViewModel s in SecondDataGramData)
            {
                //foreach (RoomView roomView in Scheduling1.SelectedRooms)
                //{
                    w = new Watch()
                    {
                        Duration = s.PD,
                        ExamId = EXAM_ID,
                        PeriodId = s.periodID,
                        StartTime = s.startTime,
                        WorkLocationId = WORK_LOCATION_ID,
                        WatchDate = s.day,
                        WatcherWatches = new List<WatcherWatch>()
                        //RoomId = roomView.Id

                    };
                    Watches.Add(w);
                //}
            }

            foreach (Watch w in Watches)
            {
                Queue<Watcher> RoomChiefs = new Queue<Watcher>();
                Queue<Watcher> RoomSecretaries = new Queue<Watcher>();
                Queue<Watcher> RoomWatchers = new Queue<Watcher>();

                foreach (WatcherViewModel wv in Scheduling2.BossWatcherList)
                    RoomChiefs.Enqueue(db.Watchers.Find(wv.Id));
                foreach (WatcherViewModel wv in Scheduling2.SecretaryWatchersList)
                    RoomSecretaries.Enqueue(db.Watchers.Find(wv.Id));
                foreach (WatcherViewModel wv in Scheduling2.OrdinaryWatchersList)
                    RoomWatchers.Enqueue(db.Watchers.Find(wv.Id));


                foreach (RoomView roomView in Scheduling1.SelectedRooms)
                {
                    //foreach(WatcherViewModel wvm in Scheduling2.BossWatcherList)
                    //{
                        w.WatcherWatches.Add(
                            new WatcherWatch()
                            {
                                WatchId = w.Id,
                                WatcherId = RoomChiefs.Dequeue().Id,
                                //WatcherId = wvm.Id,
                                WatcherType = "1",
                                RoomId = roomView.Id
                            }
                            );
                    //}
                    //foreach (WatcherViewModel wvm in Scheduling2.SecretaryWatchersList)
                    //{
                        w.WatcherWatches.Add(
                            new WatcherWatch()
                            {
                                WatchId = w.Id,
                                WatcherId = RoomSecretaries.Dequeue().Id,
                                //WatcherId = wvm.Id,
                                WatcherType = "2",
                                RoomId = roomView.Id
                            }
                            );
                    //}
                    //foreach (WatcherViewModel wvm in Scheduling2.OrdinaryWatchersList)
                    //{
                        w.WatcherWatches.Add(
                            new WatcherWatch()
                            {
                                WatchId = w.Id,
                                WatcherId = RoomWatchers.Dequeue().Id,
                                //WatcherId = wvm.Id,
                                WatcherType = "3",
                                RoomId = roomView.Id
                            }
                            );
                    //}




                    //if (RoomChiefs.Count > 0)
                    //    w.WatcherWatches.Add(
                    //    new WatcherWatch()
                    //    {
                    //        WatchId = w.Id,
                    //        WatcherId = RoomChiefs.Dequeue().Id,
                    //        WatcherType = "1",
                    //        RoomId = roomView.Id
                    //    }
                    //    );
                    //if (RoomSecretaries.Count > 0)
                    //    w.WatcherWatches.Add(
                    //    new WatcherWatch()
                    //    {
                    //        WatchId = w.Id,
                    //        WatcherId = RoomSecretaries.Dequeue().Id,
                    //        WatcherType = "2",
                    //        RoomId = roomView.Id
                    //    }
                    //        );
                    //if (RoomWatchers.Count > 0)
                    //    w.WatcherWatches.Add(
                    //        new WatcherWatch()
                    //        {
                    //            WatchId = w.Id,
                    //            WatcherId = RoomWatchers.Dequeue().Id,
                    //            WatcherType = "3",
                    //            RoomId = roomView.Id
                    //        }
                    //        );
                }
            }

            foreach(Watch w in Watches)
            {
                db.Watches.Add(w);
                db.SaveChanges();
            }

        }
    }
}
