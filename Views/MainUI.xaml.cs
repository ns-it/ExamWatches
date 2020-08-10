using ExamWatches.ViewModels;
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
        Scheduling1 scheduling1;
        Scheduling2 scheduling2;
        Scheduling3 scheduling3;
        SchedulingFinal schedulingFinal;
        WatcherScheduling watcherScheduling;

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

            //DataContext = scheduling1;

            //DataContext = scheduling1ViewModel;
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
    }
}
