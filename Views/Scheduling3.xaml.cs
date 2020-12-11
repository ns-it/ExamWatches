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
       public static int numric = 0;
        ObservableCollection<fillFirstDataGridmodel> FirstDataGramData;
        ObservableCollection<Scheduling3ViewModel> SecondDataGramData;
        ObservableCollection<Scheduling3ViewModel> BaseList;

        public ObservableCollection<int> periodList;
        public ObservableCollection<decimal> periodDurationList;
        DateTime selelectedDate;

        public static short EXAM_ID = ExamInit.examID;
        short WORK_LOCATION_ID;
        Boolean exist;

        TimeSpan startTime;
        public WorkLocation UserWorkLocation { get; set; }
        public User CurrentUser { get; set; }

        

        fillFirstDataGridmodel fillFirstDataGridmodel;

        ExamWatchesDBContext db = new ExamWatchesDBContext();


        public Scheduling3()
        {
            //EXAM_ID= ExamInit.examID;
            FirstDataGramData = new ObservableCollection<fillFirstDataGridmodel>();
            SecondDataGramData = new ObservableCollection<Scheduling3ViewModel>();
            BaseList = new ObservableCollection<Scheduling3ViewModel>();
       

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
            bool t = false;
            foreach (Scheduling3ViewModel scheduling in SecondDataGramData)
            {
              
                if (scheduling.day == selelectedDate)
                {
                    t = true;
                }
            
            }
            if (t == false)
            {
                fillFirstDataGridmodel = new fillFirstDataGridmodel();
                fillFirstDataGridmodel.dayDate = selelectedDate;

                fillFirstDataGridmodel.DateNumList = periodList;
                fillFirstDataGridmodel.SelectedNum = 1;
                FirstDataGramData.Add(fillFirstDataGridmodel);
            }
            else {

                MessageBox.Show("التاريخ مضاف مسبقا");
            }


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
                exist = false;
                for (short i = 1; i <= fm.SelectedNum; i++)
                {
                    exist = false;

                    Scheduling3ViewModel SV = new Scheduling3ViewModel();
                    
                    foreach (Scheduling3ViewModel second in SecondDataGramData)
                    {
                        if (  second.day== fm.dayDate && second.periodID==i )
                        {
                            exist = true;
                        }

                    }
                    if (exist == false) {


                        SV.day = fm.dayDate;
                        SV.periodID = i;
                    MessageBox.Show(fm.SelectedNum.ToString());
                    SV.startTime = startTime;
                    SV.periodDuration = periodDurationList;
                    SecondDataGramData.Add(SV);
                        BaseList.Add(SV);
                    }
                else {
                        MessageBox.Show(" التاريخ   "+ fm.dayDate.ToString("dd/MM/yyyy")+ "   و الفترة   " +  i + "تمت اضافتهم مسبقاَ");
                
                }
 
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



            foreach (Scheduling3ViewModel s in BaseList)
            {
                //foreach (RoomView roomView in Scheduling1.SelectedRooms)
                //{
                    w = new Watch()
                    {
                        Duration = s.PD,
                        ExamId = ExamInit.examID,
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




                }
            }

            foreach(Watch w in Watches)
            {
                db.Watches.Add(w);
                
            }
            db.SaveChanges();

        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            FirstDataGramData.Clear();
           // SecondDataGramData.Clear();
            // UserControl_Loaded();

            //List<Watch> wachList = new List<Watch>();

            //wachList = db.Watches.Where(x => x.ExamId == ExamInit.examID).ToList<Watch>();
            //MessageBox.Show(wachList.Count().ToString());
            //foreach (Watch w in wachList)
            //{

            //    Scheduling3ViewModel model = new Scheduling3ViewModel();
            //    model.day = (DateTime)w.WatchDate;
            //    model.periodID = (short)w.PeriodId;
            //    model.startTime = (TimeSpan)w.StartTime;
            //    model.PD = (decimal)w.Duration;

            //    SecondDataGramData.Add(model);




            //}
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SecondDataGramData.Clear();
            
           
            List<Watch> wachList = new List<Watch>();

            wachList = db.Watches.Where(x => x.ExamId == ExamInit.examID).ToList<Watch>();
            MessageBox.Show(wachList.Count().ToString());
                foreach (Watch w in wachList)
                {

                    Scheduling3ViewModel model = new Scheduling3ViewModel();
                    model.day = (DateTime)w.WatchDate;
                    model.periodID = (short)w.PeriodId;
                    model.startTime = (TimeSpan)w.StartTime;
                model.periodDuration = periodDurationList;
                model.PD =(decimal) w.Duration;

                    SecondDataGramData.Add(model);
                }



            

        }

        private void delete_day_Click(object sender, RoutedEventArgs e)
        {
         Scheduling3ViewModel scheduling3ViewModel=   Periods2.SelectedItem as Scheduling3ViewModel;
            SecondDataGramData.Remove(scheduling3ViewModel);
            List<Watch> wachList = new List<Watch>();

            wachList = db.Watches.Where(x => x.ExamId == ExamInit.examID).ToList<Watch>();
            
            foreach (Watch w in wachList)
            {
                if(w.WatchDate==scheduling3ViewModel.day && w.PeriodId==scheduling3ViewModel.periodID && w.StartTime==scheduling3ViewModel.startTime && w.Duration==scheduling3ViewModel.PD)
                {
                    db.Watches.Remove(w);
                    db.SaveChanges();

                }


            }
            foreach (Scheduling3ViewModel scheduling3ViewModel1 in BaseList.ToList()) {
                if (scheduling3ViewModel.day==scheduling3ViewModel1.day && scheduling3ViewModel.periodID == scheduling3ViewModel1.periodID && scheduling3ViewModel.PD == scheduling3ViewModel1.PD && scheduling3ViewModel.startTime == scheduling3ViewModel1.startTime)
                {
                    BaseList.Remove(scheduling3ViewModel);
                   
                    MessageBox.Show(BaseList.Count().ToString());

                }

            }



        }
    }
}
