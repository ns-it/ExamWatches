using ExamWatches.Utilities;
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
    /// Interaction logic for SchedulingFinal.xaml
    /// </summary>
    public partial class SchedulingFinal : UserControl
    {
        public SchedulingFinal()
        {
            InitializeComponent();
        }





        private FlowDocument CreateFlowDocument()
        {
            // Create a FlowDocument  
            FlowDocument doc = new FlowDocument();
            // Create a Section  
            Section sec = new Section();
            // Create first Paragraph  
            Paragraph p1 = new Paragraph();
            // Create and add a new Bold, Italic and Underline  
            Bold bld = new Bold();
            bld.Inlines.Add(new Run("First Paragraph"));
            Italic italicBld = new Italic();
            italicBld.Inlines.Add(bld);
            Underline underlineItalicBld = new Underline();
            underlineItalicBld.Inlines.Add(italicBld);
            // Add Bold, Italic, Underline to Paragraph  
            p1.Inlines.Add(underlineItalicBld);
            // Add Paragraph to Section  
            sec.Blocks.Add(p1);
            // Add Section to FlowDocument  
            doc.Blocks.Add(sec);
            return doc;
        }


        private void Print_Click(object sender, RoutedEventArgs e)
        {


            //PrintDG print = new PrintDG();

            //print.printDG(WatchesSchedule, "Title");
            PrintDialog printDlg = new PrintDialog();

            //PrintRTL.PrintDialodPrintVisualWithRTLWorkaround(printDlg, this, "wewewe");


            //PrintDialog printDlg = new PrintDialog();

            ////FlowDocument flow = new FlowDocument(new Paragraph(new Run("Test Text for printing")));
            ////flow.Name = "FlowDoc";


            //printDlg.PrintVisual(PrintArea, "User Control Printing.");

            //printDlg.


            //FixedDocument fixedDocument = Utilities.PrintHelper.GetFixedDocument(this, printDlg);


            //Utilities.PrintHelper.ShowPrintPreview(fixedDocument);

            //Utilities.PrintHelper.ShowPrintPreview()

            //// Create a PrintDialog  
            //PrintDialog printDlg = new PrintDialog();
            //// Create a FlowDocument dynamically.  
            //FlowDocument doc = CreateFlowDocument();
            //doc.Name = "FlowDoc";
            //// Create IDocumentPaginatorSource from FlowDocument  
            //IDocumentPaginatorSource idpSource = doc;
            //// Call PrintDocument method to send document to printer  
            //printDlg.PrintDocument(idpSource.DocumentPaginator, "Hello WPF Printing.");

        }

        //private void OpenWatcherScheduling_Click(object sender, RoutedEventArgs e)
        //{
        //    WatcherScheduling watcherScheduling = new WatcherScheduling();
        //    this.Content = watcherScheduling.Content;
        //}
    }
}
