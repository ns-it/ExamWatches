using ExamWatches.Models;
using ExamWatches.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public static Exam CurrentExam = new Exam();





        public static string deanName;
        public static string wl;

        // متحول خاص بعدد أيام الامتحان
        private int _numValue = 0;
        int semNum;
        string yearNum;
        public static short examID;
      //  public static short exID;
        // متحول يحتفظ برقم المستخدم الذي قام بتسجيل الدخول
        int user_id = Convert.ToInt32(App.Current.Properties["user_id"]);
        // غرض للتعامل مع القاعدة
        ExamWatchesDBContext db = new ExamWatchesDBContext();
        //غرض من الصف الخاص بمكان العمل
        WorkLocation work_locationl = new WorkLocation();
        // غرض من الصف الخاص بالامتحان




        public int NumValue
        {
            get { return _numValue; }
            set
            {
                _numValue = value;
                days.Text = value.ToString();
            }
        }


        ObservableCollection<Exam> ExamList;

        public ExamInit()
        {





            InitializeComponent();
            add_collage_list();
            intialize_windows();
            SaveExamButton.IsEnabled = false;

            days.Text = _numValue.ToString();
            work_locationl = db.WorkLocations.Where(x => x.Name == collageList.Text).FirstOrDefault();
            string collage_name = collageList.Text.ToString();
            work_locationl = db.WorkLocations.Where(x => x.Name == collage_name).FirstOrDefault();
            dean_name.Text = work_locationl.Dean;
            deanName = work_locationl.Dean;
            manager_name.Text = work_locationl.Manager;
            wl = work_locationl.Manager;



            ExamList = new ObservableCollection<Exam>(db.Exams.OrderByDescending(x => x.Id).Where(e=>e.WorkLocationId.Equals(work_locationl.Id)).ToList());

            CurrentExam = ExamList.FirstOrDefault();

            SelectedExamComboBox.ItemsSource = ExamList;

            SelectedExamComboBox.SelectedValue = CurrentExam;

            getExamID();

            //SelectedExamComboBox.SelectedValue = CurrentExam.Id;
            //SelectedExamComboBox.SelectedValuePath = "Id";

            //SelectedExamComboBox.DisplayMemberPath = "Id";

        }


        public void getExamID() {
            Exam e = new Exam();
            e = db.Exams.OrderByDescending(x => x.Id).Where(x => x.WorkLocationId == work_locationl.Id).FirstOrDefault();


            CurrentExam = e;


            examID = db.Exams.OrderByDescending(x => x.Id).Where(x=>x.WorkLocationId==work_locationl.Id).FirstOrDefault().Id;
           // MessageBox.Show(examID.ToString());
            if (e.Semester == 1)
            { first.IsChecked = true; }
            if (e.Semester == 2)
            { second.IsChecked = true; }
            if (e.Semester == 3)
            { third.IsChecked = true; }

            year.Text = e.AcademicYear;
            days.Text = e.DaysNumber.ToString();
            startDate.Text = e.StartTime.ToString();
            endDate.Text = e.EndTime.ToString();

        
        
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
            deanName = work_locationl.Dean;
            manager_name.Text = work_locationl.Manager;
            wl = work_locationl.Manager;

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
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{

        //    UpdateExamButton.IsEnabled = true;
        //    SaveExamButton.IsEnabled = false;
        //    Exam exam = new Exam();
        //    Exam ex = new Exam();
        //    if (first.IsChecked == true)
        //    {
        //        exam.Semester = 1;

        //    }
        //    else if (second.IsChecked == true)
        //    {
        //        exam.Semester = 2;
        //    }
        //    else
        //    {
        //        exam.Semester = 3;
        //    }

        //    exam.AcademicYear = year.Text;
        //    exam.DaysNumber = short.Parse(days.Text);
        //    exam.StartTime = startDate.SelectedDate;
        //    exam.EndTime = endDate.SelectedDate;
        //    string collage_name = collageList.Text.ToString();
        //    work_locationl = db.WorkLocations.Where(x => x.Name == collage_name).FirstOrDefault();
        //    exam.WorkLocationId = work_locationl.Id;
        //    db.Add(exam);
        //    db.SaveChanges();
        //    MessageBox.Show("تمت عملية الحفظ بنجاح");
        //    ex = db.Exams.Where(x => x.Semester == exam.Semester && x.AcademicYear == exam.AcademicYear && x.DaysNumber == exam.DaysNumber && x.StartTime == exam.StartTime && x.EndTime == exam.EndTime && x.WorkLocationId == exam.WorkLocationId).FirstOrDefault();
        //    examID = ex.Id;
        ////    MessageBox.Show(ex.Id.ToString());

            

        //}









        //// فحص في حال تم ادخال بيانات الامتحان حسب رقم الفصل والسنة
        //private void year_DropDownClosed(object sender, EventArgs e)
        //{
            
        //    if (first.IsChecked == true)
        //    {
        //        semNum = 1;

        //    }
        //    else if (second.IsChecked == true)
        //    {
        //        semNum = 2;
        //    }
        //    else
        //    {
        //        semNum = 3;
        //    }

        //    ComboBox comboBox = sender as ComboBox;

        //    yearNum = comboBox.Text.ToString();
        //    // yearNum = year.Text;
        //    Exam exam = new Exam();
        //    int count = db.Exams.Where(x => x.AcademicYear == yearNum && x.Semester == semNum && x.WorkLocationId == user_id).Count();
        //    // جلب بيانات هذا الامتحان المحدد في حال كان موجود في القاعدة
        //    if (count == 1)
        //    {
        //        UpdateExamButton.IsEnabled = true;
        //        SaveExamButton.IsEnabled = false;
        //        exam = db.Exams.Where(x => x.AcademicYear == yearNum && x.Semester == semNum && x.WorkLocationId == user_id).FirstOrDefault();
        //        days.Text = exam.DaysNumber.ToString();
        //        startDate.Text = exam.StartTime.ToString();
        //        endDate.Text = exam.EndTime.ToString();
        //        examID = exam.Id;
        //      //  MessageBox.Show(examID.ToString());


        //    }
        //    //   اما في حال عدم ايجاد بيانات لهاد الفصل يتم ادخال بيانات جديدة و حفظها
        //    else
        //    {

        //        MessageBox.Show("ادخل بيانات الامتحان");
        //        SaveExamButton.IsEnabled = true;
        //        days.Text = null;
        //        startDate.Text = null;
        //        endDate.Text = null;
        //        UpdateExamButton.IsEnabled = false;
        //    }
        //}




        //// زر التعديل على بيانات الامتحان
        //private void UpdataButton_Click(object sender, RoutedEventArgs e)
        //{


        //    db.Exams.Where(x => x.AcademicYear == yearNum && x.Semester == semNum && x.WorkLocationId == user_id)
        //            .ToList().ForEach(x => x.DaysNumber = short.Parse(days.Text));
        //    db.Exams.Where(x => x.AcademicYear == yearNum && x.Semester == semNum && x.WorkLocationId == user_id)
        //        .ToList().ForEach(x => x.StartTime = startDate.SelectedDate);
        //    db.Exams.Where(x => x.AcademicYear == yearNum && x.Semester == semNum && x.WorkLocationId == user_id)
        //        .ToList().ForEach(x => x.EndTime = endDate.SelectedDate);
            
        //    db.SaveChanges();
        //    MessageBox.Show("تمت عملية التعديل بنجاح");

        //}

        //private void first_Checked(object sender, RoutedEventArgs e)
        //{
        //    //year.Text = null;
        //    //days.Text = null;
        //    //startDate.Text = null;
        //    //endDate.Text = null;
        //}

        //private void second_Checked(object sender, RoutedEventArgs e)
        //{
        //    //year.Text = null;
        //    //days.Text = null;
        //    //startDate.Text = null;
        //    //endDate.Text = null;

        //}

        //private void third_Checked(object sender, RoutedEventArgs e)
        //{
        //    //year.Text = null;
        //    //days.Text = null;
        //    //startDate.Text = null;
        //    //endDate.Text = null;
        //}

  

        private void ManagerNameStartEdit_Click(object sender, RoutedEventArgs e)
        {
            //ManagerNameCancel.IsEnabled = true;
            //ManagerNameSaveChanges.IsEnabled = true;
            //ManagerNameStartEdit.IsEnabled = false;

            ManagerNameCancel.Visibility = Visibility.Visible;
            ManagerNameSaveChanges.Visibility = Visibility.Visible;
            ManagerNameStartEdit.Visibility = Visibility.Hidden;

            manager_name.IsEnabled = true;
        }

        private void ManagerNameSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            
            work_locationl.Manager = manager_name.Text;
            db.WorkLocations.Update(work_locationl);
            db.SaveChanges();

            //ManagerNameCancel.IsEnabled = false;
            //ManagerNameSaveChanges.IsEnabled = false;
            //ManagerNameStartEdit.IsEnabled = true;

            ManagerNameCancel.Visibility = Visibility.Hidden;
            ManagerNameSaveChanges.Visibility = Visibility.Hidden;
            ManagerNameStartEdit.Visibility = Visibility.Visible;

            manager_name.IsEnabled = false;

        }
        private void ManagerNameCancel_Click(object sender, RoutedEventArgs e)
        {

            //ManagerNameCancel.IsEnabled = false;
            //ManagerNameSaveChanges.IsEnabled = false;
            //ManagerNameStartEdit.IsEnabled = true;


            ManagerNameCancel.Visibility = Visibility.Hidden;
            ManagerNameSaveChanges.Visibility = Visibility.Hidden;
            ManagerNameStartEdit.Visibility = Visibility.Visible;

            manager_name.IsEnabled = false;

        }

        private void DeanNameStartEdit_Click(object sender, RoutedEventArgs e)
        {
            //DeanNameCancel.IsEnabled = true;
            //DeanNameSaveChanges.IsEnabled = true;
            //DeanNameStartEdit.IsEnabled = false;

            DeanNameStartEdit.Visibility = Visibility.Hidden;
            DeanNameSaveChanges.Visibility = Visibility.Visible;
            DeanNameCancel.Visibility = Visibility.Visible;

            dean_name.IsEnabled = true;

        }

        private void DeanNameSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            work_locationl.Dean = dean_name.Text;
            db.WorkLocations.Update(work_locationl);
            db.SaveChanges();

            //DeanNameCancel.IsEnabled = false;
            //DeanNameSaveChanges.IsEnabled = false;
            //DeanNameStartEdit.IsEnabled = true;

            DeanNameStartEdit.Visibility = Visibility.Visible;
            DeanNameSaveChanges.Visibility = Visibility.Hidden;
            DeanNameCancel.Visibility = Visibility.Hidden;

            dean_name.IsEnabled = false;
        }

        private void DeanNameCancel_Click(object sender, RoutedEventArgs e)
        {
            //DeanNameCancel.IsEnabled = false;
            //DeanNameSaveChanges.IsEnabled = false;
            //DeanNameStartEdit.IsEnabled = true;


            DeanNameStartEdit.Visibility = Visibility.Visible;
            DeanNameSaveChanges.Visibility = Visibility.Hidden;
            DeanNameCancel.Visibility = Visibility.Hidden;

            dean_name.IsEnabled = false;
        }


        /*********************************************
         * 
         * 
         **********************************************/


        private void UpdateExamButton_Click(object sender, RoutedEventArgs e)
        {
            StartExamEdit();

        }

        static bool IsNewExam = false;

        private void AddExamButton_Click(object sender, RoutedEventArgs e)
        {
            StartExamEdit();
            ResetExamValues();

            year.IsEnabled = true;
            SemesterValue.IsEnabled = true;

            IsNewExam = true;
            



        }
        private void SaveExamButton_Click(object sender, RoutedEventArgs e)
        {
            StopExamEdit();

            if (IsNewExam)
            {
                Exam exam = new Exam();
                SetExamValues(ref exam);
                exam.WorkLocationId = work_locationl.Id;
                

               if(db.Exams.Any(x => x.AcademicYear == exam.AcademicYear && x.Semester == exam.Semester && x.WorkLocationId == work_locationl.Id))
                {
                    MessageBox.Show("لقد تمت تهيئة هذا الامتحان مسبقاً");
                    IsNewExam = false;
                    getCurrentExamValues();
                    return;

                }
                examID = db.Add(exam).Entity.Id;
                ExamList.Add(exam);

                SelectedExamComboBox.SelectedValue = exam;
            }
            else
            {
                Exam exam = SelectedExamComboBox.SelectedValue as Exam;
                examID = exam.Id;
                SetExamValues(ref exam);
                db.Exams.Update(exam);
            }

            db.SaveChanges();

            

            IsNewExam = false;

        }

        void SetExamValues(ref Exam  exam)
        {
            exam.AcademicYear = year.Text;
            exam.DaysNumber = short.Parse(days.Text);
            exam.StartTime = startDate.SelectedDate;
            exam.EndTime = endDate.SelectedDate;

            if (first.IsChecked == true) exam.Semester = 1;
            else if (second.IsChecked == true) exam.Semester = 2;
            else if (third.IsChecked == true) exam.Semester = 3;
        }

        private void CancelExamButton_Click(object sender, RoutedEventArgs e)
        {
            StopExamEdit();
            if (IsNewExam) getCurrentExamValues();
            IsNewExam = false;
        }


        private void StartExamEdit()
        {
            // Enable and Disable Buttons

            SelectedExamComboBox.IsEnabled = false;

            UpdateExamButton.IsEnabled = false;
            AddExamButton.IsEnabled = false;

            SaveExamButton.IsEnabled = true;
            CancelExamButton.IsEnabled = true;

            UpdateExamButton.Visibility = Visibility.Collapsed;
            AddExamButton.Visibility = Visibility.Collapsed;

            SaveExamButton.Visibility = Visibility.Visible;
            CancelExamButton.Visibility = Visibility.Visible;

            // Enable editing updatable fields

            ExamDays.IsEnabled = true;
            startDate.IsEnabled = true;
            endDate.IsEnabled = true;
        }

        private void StopExamEdit()
        {
            // Enable and Disable Buttons

            SelectedExamComboBox.IsEnabled = true;

            UpdateExamButton.IsEnabled = true;
            AddExamButton.IsEnabled = true;

            SaveExamButton.IsEnabled = false;
            CancelExamButton.IsEnabled = false;

            UpdateExamButton.Visibility = Visibility.Visible;
            AddExamButton.Visibility = Visibility.Visible;

            SaveExamButton.Visibility = Visibility.Collapsed;
            CancelExamButton.Visibility = Visibility.Collapsed;

            // Disable editing updatable fields

            ExamDays.IsEnabled = false;
            startDate.IsEnabled = false;
            endDate.IsEnabled = false;

            year.IsEnabled = false;
            SemesterValue.IsEnabled = false;

        }

        private void ResetExamValues()
        {

            first.IsChecked = false;
            second.IsChecked = false;
            third.IsChecked = false;

            year.SelectedIndex = -1;

            year.Text = null;
            days.Text = null;
            startDate.Text = null;
            endDate.Text = null;
            
        }

        private void SelectedExamComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            getCurrentExamValues();
        }

        void getCurrentExamValues()
        {
            Exam exam = SelectedExamComboBox.SelectedValue as Exam;

            examID = exam.Id;
            // set Semester value

            if (exam.Semester.Value == 1) first.IsChecked = true;
            else if (exam.Semester.Value == 2) second.IsChecked = true;
            else if (exam.Semester.Value == 3) third.IsChecked = true;

            // set year value


            year.Text = exam.AcademicYear;




            days.Text = exam.DaysNumber.GetValueOrDefault().ToString();

            startDate.SelectedDate = exam.StartTime.GetValueOrDefault();
            endDate.SelectedDate = exam.EndTime.GetValueOrDefault();
        }

    }

}
