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
        
        public static int user_id =0;
        User login_user;
        ExamWatchesDBContext db;
        MainUI mainui;


        public LogIn()
        {
            InitializeComponent();

        }
       

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            login_user = new User();
            db = new ExamWatchesDBContext();

            string name = username.Text;
            string pw = password.Password;
            

           

           int num= db.Users.Where(x => x.Username == name && x.Password == pw).Count();

            if (num == 1)
            {
                login_user =db.Users.Where(x => x.Username == name && x.Password == pw).FirstOrDefault();
         
            App.Current.Properties["user_id"] =  login_user.Id;

                user_id = login_user.Id;
                MessageBox.Show("أهلا بك " + login_user.FirstName);
                mainui = new MainUI(user_id);
                mainui.Show();
                this.Close();
            }
            else
                MessageBox.Show("اسم المستخدم أو كلمة المرور غير صالحة");
          //  db.Users.Select(x => x);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


    }
}
