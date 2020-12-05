using System;
using System.Collections.Generic;
using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Xps;

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

        private void Print_Click(object sender, RoutedEventArgs e)
        {

            //PrintDialog printDialog = new PrintDialog();

            //printDialog.PrintQueue = LocalPrintServer.GetDefaultPrintQueue();

            //printDialog.PrintTicket = printDialog.PrintQueue.DefaultPrintTicket;



            //printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;

            //printDialog.PrintTicket.PageScalingFactor = 90;

            //printDialog.PrintTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4);



            //printDialog.PrintTicket.PageBorderless = PageBorderless.None;



            //if (printDialog.ShowDialog() == true)

            //{

            //    //----< print >----   

            //    // set the print ticket for the document sequence and write it to the printer.





            //    //-< send_document_to_printer >-

            //    XpsDocumentWriter writer = PrintQueue.CreateXpsDocumentWriter(printDialog.PrintQueue);

            //    writer.WriteAsync(parea, printDialog.PrintTicket);

            //    //-</ send_document_to_printer >-

            //    //----</ print >----  

            //}


            //-------------------------------------------------------------------------------------------------
            PrintDialog printDlg = new System.Windows.Controls.PrintDialog();
            if (printDlg.ShowDialog() == true)
            {
                //get selected printer capabilities
                System.Printing.PrintCapabilities capabilities = printDlg.PrintQueue.GetPrintCapabilities(printDlg.PrintTicket);
                //printDlg.PrintQueue = LocalPrintServer.GetDefaultPrintQueue();

                //printDlg.PrintTicket = printDlg.PrintQueue.DefaultPrintTicket;

                printDlg.PrintTicket.PageOrientation = PageOrientation.Landscape;

                printDlg.PrintTicket.PageScalingFactor = 90;

                printDlg.PrintTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4);
                //get scale of the print wrt to screen of WPF visual
                double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / this.ActualWidth, capabilities.PageImageableArea.ExtentHeight /
                       this.ActualHeight);

                //Transform the Visual to scale
                parea.LayoutTransform = new ScaleTransform(scale, scale);

                //get the size of the printer page
              //  Size sz = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);

                //update the layout of the visual to the printer page size.
               // parea.Measure(sz);
               // parea.Arrange(new Rect(new Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), sz));

                //now print the visual to printer to fit on the one page.
                printDlg.PrintVisual(parea, "First Fit to Page WPF Print");
            }
        }
    }
}
