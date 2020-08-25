using ExamWatches.Models;
using ExamWatches.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for MainUI.xaml
    /// </summary>
    public partial class MainUI : Window
    {
        readonly Scheduling1 scheduling1;
        readonly Scheduling2 scheduling2;
        readonly Scheduling3 scheduling3;
        readonly SchedulingFinal schedulingFinal;
        readonly WatcherScheduling watcherScheduling;

     public string CurrentUserName { get; private set; }

        //Scheduling1ViewModel scheduling1ViewModel;
        //Scheduling2ViewModel scheduling2ViewModel;
        //Scheduling3ViewModel scheduling3ViewModel;
        //SchedulingFinalViewModel schedulingFinalViewModel;
        //WatcherSchedulingViewModel watcherSchedulingViewModel;



        public MainUI()
        {
            InitializeComponent();
            scheduling1 = new Scheduling1();
            scheduling2 = new Scheduling2();
            scheduling3 = new Scheduling3();
            schedulingFinal = new SchedulingFinal();
            watcherScheduling = new WatcherScheduling();

            //scheduling1ViewModel = new Scheduling1ViewModel();
            //scheduling2ViewModel = new Scheduling2ViewModel();
            //scheduling3ViewModel = new Scheduling3ViewModel();
            //schedulingFinalViewModel = new SchedulingFinalViewModel();
            //watcherSchedulingViewModel = new WatcherSchedulingViewModel();

            DataContext = scheduling1;

            //int id = LogIn.user_id;
            //using (ExamWatchesDBContext db = new ExamWatchesDBContext())
            //{
            //    CurrentUserName = db.Users.Find(id).FirstName;
            //}


            //DataContext = scheduling1ViewModel;


        }

        public MainUI(int userid) : this()
        {
            User user;
            WorkLocation location;

            using (ExamWatchesDBContext db = new ExamWatchesDBContext())
            {
                //db.WorkLocations.Include(loc => loc.Users);

                user = db.Users.Find(userid);
                location = db.WorkLocations.Find(user.WorkLocationId);
                CurrentUserName = user.FirstName + " " + user.LastName;
            }
            //MessageBox.Show(CurrentUserName);
            UserName.Content = "تم تسجيل الدخول باسم: " + CurrentUserName + " - " + location.Name;
        }

        private void ShowScheduling1_Click(object sender, RoutedEventArgs e)
        {
            DataContext = scheduling1;
            //DataContext = scheduling1ViewModel;
        }

        private void ShowScheduling2_Click(object sender, RoutedEventArgs e)
        {
            DataContext = scheduling2;
            //DataContext = scheduling2ViewModel;
        }

        private void ShowScheduling3_Click(object sender, RoutedEventArgs e)
        {
            DataContext = scheduling3;
            //DataContext = scheduling3ViewModel;
        }

        private void ShowSchedulingFinal_Click(object sender, RoutedEventArgs e)
        {
            DataContext = schedulingFinal;
            //DataContext = schedulingFinalViewModel;

        }

        private void ShowWatcherScheduling_Click(object sender, RoutedEventArgs e)
        {
            DataContext = watcherScheduling;
            //DataContext = watcherSchedulingViewModel;
        }

        private void ChangeUser_Click(object sender, RoutedEventArgs e)
        {
           
            new LogIn().Show();
            this.Close();
        }
    }
}
