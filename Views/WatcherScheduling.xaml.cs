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
    /// Interaction logic for WatcherScheduling.xaml
    /// </summary>
    public partial class WatcherScheduling : UserControl
    {
        public WatcherScheduling()
        {
            InitializeComponent();
        }

        private void OpenWatcherScheduling_Click(object sender, RoutedEventArgs e)
        {
            SchedulingFinal schedulingFinal = new SchedulingFinal();
            this.Content = schedulingFinal.Content;
        }
    }
}
