using ExamWatches.Utilities;
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


            //PrintDialog printDlg = new PrintDialog();

            //printDlg.PrintVisual(PrintArea, "User Control Printing.");




            //----------------< Print_Document() >-----------------------

            //----< Get_Print_Dialog_and_Printer >----

            PrintDialog printDialog = new PrintDialog();

            printDialog.PrintQueue = LocalPrintServer.GetDefaultPrintQueue();

            printDialog.PrintTicket = printDialog.PrintQueue.DefaultPrintTicket;

    

            printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;

            printDialog.PrintTicket.PageScalingFactor = 90;

            printDialog.PrintTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4);

            printDialog.PrintTicket.PageBorderless = PageBorderless.None;
            

            if (printDialog.ShowDialog() == true)

            {

                //----< print >----   

                // set the print ticket for the document sequence and write it to the printer.

                //-< send_document_to_printer >-

                XpsDocumentWriter writer = PrintQueue.CreateXpsDocumentWriter(printDialog.PrintQueue);

                writer.WriteAsync(PrintArea, printDialog.PrintTicket);
                

                //-</ send_document_to_printer >-

                //----</ print >----   

            }


        }

      
    }
}
