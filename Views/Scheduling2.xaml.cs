using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Scheduling2.xaml
    /// </summary>
    public partial class Scheduling2 : UserControl
    {

        public static ObservableCollection<WatcherViewModel> AllWatcherList;
        public static ObservableCollection<WatcherViewModel> BossWatcherList;
        public static ObservableCollection<WatcherViewModel> SecretaryWatchersList;
        public static ObservableCollection<WatcherViewModel> OrdinaryWatchersList;
        WatcherViewModel selectedWatcher = new WatcherViewModel();
        int count;



        public Scheduling2()
        {
            InitializeComponent();
            AllWatcherList = new ObservableCollection<WatcherViewModel>();
            //AllWatcherList = Scheduling1.SelectedWatcher;

            AllWatchers.ItemsSource = AllWatcherList;
      //      AllWatcherList = new ObservableCollection<WatcherViewModel>();
            BossWatcherList = new ObservableCollection<WatcherViewModel>();
            BossWatchers.ItemsSource = BossWatcherList;
            SecretaryWatchersList = new ObservableCollection<WatcherViewModel>();
            SecretaryWathers.ItemsSource = SecretaryWatchersList;
            OrdinaryWatchersList = new ObservableCollection<WatcherViewModel>();
            OrdinaryWatchers.ItemsSource = OrdinaryWatchersList;

        }


        private void Next_Click(object sender, RoutedEventArgs e)
        {
            Scheduling3 scheduling3 = new Scheduling3();
            this.Content = scheduling3.Content;
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            Scheduling1 scheduling1 = new Scheduling1();
            this.Content = scheduling1.Content;
        }



        private void AllWatchers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AllWatchers.SelectedItem != null) { selectedWatcher = AllWatchers.SelectedItem as WatcherViewModel;
                count = 0;
            }


          
            

        }

        private void BossWatchers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BossWatchers.SelectedItem != null)
            {
                selectedWatcher = BossWatchers.SelectedItem as WatcherViewModel;
                count = 1;
            }
           
       
        }

        private void SecretaryWathers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SecretaryWathers.SelectedItem != null) { selectedWatcher = SecretaryWathers.SelectedItem as WatcherViewModel;
                count = 2;
            }

            
            
        }

        private void OrdinaryWatchers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(OrdinaryWatchers.SelectedItem != null) { selectedWatcher = OrdinaryWatchers.SelectedItem as WatcherViewModel;
                count = 3;
            }

            
          

        }

        private void moveRight_Click(object sender, RoutedEventArgs e)
        {
            count++;
            if (count == 0)
            {

            }
            if (count == 1)
            {
                
                    BossWatcherList.Add(selectedWatcher);
                

                AllWatcherList.Remove(selectedWatcher);

             
            }
            if (count == 2)
            {
              

                SecretaryWatchersList.Add(selectedWatcher);
             
             
                BossWatcherList.Remove(selectedWatcher);
            }
            if (count == 3)
            {

                OrdinaryWatchersList.Add(selectedWatcher);
             
                
                SecretaryWatchersList.Remove(selectedWatcher);
            }

        }

        private void moveLeft_Click(object sender, RoutedEventArgs e)
        {
            count--;
            if (count == 0)
            {
                AllWatcherList.Add(selectedWatcher);
                BossWatcherList.Remove(selectedWatcher);

            }
            if (count == 1)
            {
                BossWatcherList.Add(selectedWatcher);
                SecretaryWatchersList.Remove(selectedWatcher);

            }
            if (count == 2)
            {
                SecretaryWatchersList.Add(selectedWatcher);
                OrdinaryWatchersList.Remove(selectedWatcher);

                
            }
            if (count == 3)
            {
                
            }




        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
         

            foreach (WatcherViewModel watcherViewModel in Scheduling1.SelectedWatcher)
            {
                if(!AllWatcherList.Contains(watcherViewModel)&& !BossWatcherList.Contains(watcherViewModel)&& !SecretaryWatchersList.Contains(watcherViewModel)&& !OrdinaryWatchersList.Contains(watcherViewModel))
                AllWatcherList.Add(watcherViewModel);
              
       
            }
           
        }
    }
}


