using ExamWatches.Models;



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
    /// Interaction logic for ExamInit.xaml
    /// </summary>
    public partial class ExamInit : UserControl

    {
        // متحول خاص بعدد أيام الامتحان
        private int _numValue = 0;
        int semNum;
        string yearNum;
        // متحول يحتفظ برقم المستخدم الذي قام بتسجيل الدخول
        int user_id = Convert.ToInt32(App.Current.Properties["user_id"]);
        // غرض للتعامل مع القاعدة
        ExamWatchesDBContext db = new ExamWatchesDBContext();
        //غرض من الصف الخاص بمكان العمل
        WorkLocation work_locationl = new WorkLocation();
        // غرض من الصف الخاص بالامتحان
        Exam exam = new Exam();



        public int NumValue
        {
            get { return _numValue; }
            set
            {
                _numValue = value;
                days.Text = value.ToString();
            }
        }

        public ExamInit()
        {
            InitializeComponent();
            add_collage_list();
            intialize_windows();

            days.Text = _numValue.ToString();


        }
        // تهيئة القائمة الخاصة بأسماء الكليات
        public void add_collage_list()
        {   // ارجاع قائمة بأسماء الكليات
            var list = db.WorkLocations.ToList();

            foreach (var item in list)
            {

                collageList.Items.Add(item.Name.ToString());
            }
        }

        // تغيير اسم العميد ورئيس الدائرة تبعا لاسم الكلية
        private void collageList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string collage_name = collageList.Text.ToString();
            work_locationl = db.WorkLocations.Where(x => x.Name == collage_name).FirstOrDefault();
            dean_name.Text = work_locationl.Dean;
            manager_name.Text = work_locationl.Manager;

        }

        // تهيئة الواجهة
        public void intialize_windows()
        {
            // ارجاع مكان العمل بحسب المستخدم الذي قام بتسجيل الدخول
            work_locationl = db.WorkLocations.Where(x => x.Id == user_id).FirstOrDefault();
            collageList.Text = work_locationl.Name;
            dean_name.Text = work_locationl.Dean;
            manager_name.Text = work_locationl.Manager;


        }

        //private void collageList_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        //{
        //    string collage_name = collageList.Text.ToString();
        //    wl = db.WorkLocations.Where(x => x.Name == collage_name).FirstOrDefault();
        //    dean_name.Text = wl.Dean;
        //    manager_name.Text = wl.Manager;


        //}
        // عداد لأيام الامتحان
        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            NumValue++;
        }
        // عداد لأيام الامتحان
        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            NumValue--;
        }

        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (days == null)
            {
                return;
            }

            if (!int.TryParse(days.Text, out _numValue))
                days.Text = _numValue.ToString();
        }


        // زر الانتقال للصفحة التالية يتم فيه توليد غرض من الصف امتحان وحفظه في القاعدة
        private void Button_Click(object sender, RoutedEventArgs e)
        {


            if (first.IsChecked == true)
            {
                exam.Semester = 1;

            }
            else if (second.IsChecked == true)
            {
                exam.Semester = 2;
            }
            else
            {
                exam.Semester = 3;
            }

            exam.AcademicYear = year.Text;
            exam.DaysNumber = short.Parse(days.Text);
            exam.StartTime = startDate.SelectedDate;
            exam.EndTime = endDate.SelectedDate;
            string collage_name = collageList.Text.ToString();
            work_locationl = db.WorkLocations.Where(x => x.Name == collage_name).FirstOrDefault();
            exam.WorkLocationId = work_locationl.Id;
            db.Add(exam);
            db.SaveChanges();
        }





        private void years_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (first.IsChecked == true)
            {
                semNum = 1;

            }
            else if (second.IsChecked == true)
            {
                semNum = 2;
            }
            else
            {
                semNum = 3;
            }
            Exam ex = new Exam();
            yearNum = year.Text;
            //  MessageBox.Show(yearNum);
            int count = db.Exams.Where(x => x.AcademicYear == yearNum && x.Semester == semNum).Count();
            if (count == 1)
            {
                SaveButton.IsEnabled = false;
                ex = db.Exams.Where(x => x.AcademicYear == yearNum && x.Semester == semNum).FirstOrDefault();
                days.Text = ex.DaysNumber.ToString();
                startDate.Text = ex.StartTime.ToString();
                endDate.Text = ex.EndTime.ToString();


            }
            else
            {

                MessageBox.Show("insert new data and click save");
                SaveButton.IsEnabled = true;

            }
        }
    }

}
