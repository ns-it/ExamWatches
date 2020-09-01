using ExamWatches.Models;
using ExamWatches.ViewModels;
using System;
using System.Collections.Generic;
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

namespace ExamWatches.Views
{
    /// <summary>
    /// Interaction logic for RoomsInit.xaml
    /// </summary>
    public partial class RoomsInit : UserControl
    {
        public RoomsInit()
        {
            InitializeComponent();

            //using (ExamWatchesDBContext db = new ExamWatchesDBContext())
            //{
            //    RoomsGrid.ItemsSource = db.Rooms.ToList();
            //}

        }

        private void ItemsList_CollectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RoomsInitVM.SelectedItemChangedAction(null);
        }
    }
}
