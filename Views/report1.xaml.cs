using System;
using System.Collections.Generic;
using System.Data;
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
using Microsoft.Data.SqlClient;

using Microsoft.Reporting.WinForms;


namespace ExamWatches.Views
{
    /// <summary>
    /// Interaction logic for report1.xaml
    /// </summary>
    public partial class report1 : Window
    {

        ExamWatchesDBContext db = new ExamWatchesDBContext();
        public report1()
        {
          
            InitializeComponent();
       
        }


      public  DataTable getDataTable() {
            
           DataTable dt = new DataTable();
            //SqlCommand sql =new SqlCommand("select * from final;");
            //SqlDataAdapter adp = new SqlDataAdapter(sql);
            //adp.Fill(dt);
            
          SqlConnection nwindConn = new SqlConnection("Data Source=DESKTOP-9841DFE;Initial Catalog=exam_watches;Integrated Security=True");
           SqlCommand selectCMD = new SqlCommand("SELECT * FROM final", nwindConn);
            SqlDataAdapter customerDA = new SqlDataAdapter();
            customerDA.SelectCommand = selectCMD;
            nwindConn.Open();
            customerDA.Fill(dt);

            return dt;
        
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            repviewer.Reset();
            DataTable dt = getDataTable();
            ReportDataSource ds = new ReportDataSource("DataSet1", dt);
            repviewer.LocalReport.DataSources.Add(ds);
            repviewer.LocalReport.ReportEmbeddedResource = "ExamWatches.Report1.rdlc";
            repviewer.RefreshReport();
        }
    }
}
