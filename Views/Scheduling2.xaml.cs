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
    /// Interaction logic for Scheduling2.xaml
    /// </summary>
    public partial class Scheduling2 : UserControl
    {
        public Scheduling2()
        {
            InitializeComponent();
            //   Scheduling1.SelectedWatcher
            AllWatchers.ItemsSource = Scheduling1.SelectedWatcher;
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

        }
    }
}
