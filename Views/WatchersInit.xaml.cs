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
    /// Interaction logic for WatchersInit.xaml
    /// </summary>
    public partial class WatchersInit : UserControl
    {
        public WatchersInit()
        {
            InitializeComponent();
        }

        private void ItemsList_CollectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WatchersInitVM.SelectedItemChangedAction(null);
        }
    }
}
