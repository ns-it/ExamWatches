//using ExamWatches.Models;
using ExamWatches.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        public LogIn()
        {
            InitializeComponent();
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string name = username.Text;
            string pw = password.Text;
            ExamWatchesDBContext db = new ExamWatchesDBContext();

            string s = db.Users.Find(1).Username.ToString();

           int num= db.Users.Where(x => x.Username == name && x.Password == pw).Count();
            if (num == 1)
            {



                MessageBox.Show("أهلا بك");
                MainUI mainui = new MainUI();
                mainui.Show();
                this.Close();


                    


            }
            else
                MessageBox.Show("اسم المستخدم أو كلمة المرور غير صالحة");
          //  db.Users.Select(x => x);




        }
    }
}
