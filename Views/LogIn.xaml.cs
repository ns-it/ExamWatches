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
    public partial  class LogIn : Window
    {
        
        public  static int user_id;
        User login_user;
        ExamWatchesDBContext db;
        MainUI mainui;


        public LogIn()
        {
            InitializeComponent();
            user_id = 0;
            login_user = new User();
            db = new ExamWatchesDBContext();
        }
       

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string name = username.Text;
            string pw = password.Password;
            

           

           int num= db.Users.Where(x => x.Username == name && x.Password == pw).Count();

            if (num == 1)
            {
                login_user =db.Users.Where(x => x.Username == name && x.Password == pw).FirstOrDefault();
         
            App.Current.Properties["user_id"] =  login_user.Id;


                MessageBox.Show("أهلا بك");
                mainui = new MainUI();
                mainui.Show();
                this.Close();
            }
            else
                MessageBox.Show("اسم المستخدم أو كلمة المرور غير صالحة");
          //  db.Users.Select(x => x);
        }
    }
}
