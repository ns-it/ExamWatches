using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace ExamWatches.Utilities
{
    public static class PrintRTL
    {
        public static void PrintDialodPrintVisualWithRTLWorkaround(PrintDialog printDialog, FrameworkElement fe, string description)
        {
            FlowDirection parentFd = fe.FlowDirection;
            object temp = fe.ReadLocalValue(FrameworkElement.FlowDirectionProperty);
            FlowDirection localFd = (temp != DependencyProperty.UnsetValue) ? ((FlowDirection)temp) : FlowDirection.LeftToRight;
            object oldRenderTransform = fe.ReadLocalValue(UIElement.RenderTransformProperty);

            if (localFd != parentFd)
            {
                //apply implicit mirroring transform workaround
                fe.RenderTransform = new MatrixTransform(-1.0, 0.0, 0.0, 1.0, fe.RenderSize.Width, 0.0);

                // Wait for the render transform to take effect
                // Unfortunately this also means that the end user will see a temporaty mirroring effect
                DispatchPendingOperations();
            }

            printDialog.PrintVisual(fe, description);

            if (localFd != parentFd)
            {
                // undo workaround
                fe.SetValue(UIElement.RenderTransformProperty, oldRenderTransform);
            }
        }

        private static void DispatchPendingOperations()
        {
            DispatcherFrame frame = new DispatcherFrame(true);
            Dispatcher.CurrentDispatcher.BeginInvoke(
              DispatcherPriority.SystemIdle,
              (DispatcherOperationCallback)ExitDispatcherFrame,
              frame);
            Dispatcher.PushFrame(frame);
        }

        private static object ExitDispatcherFrame(object arg)
        {
            DispatcherFrame frame = (DispatcherFrame)arg;
            frame.Continue = false;
            return null;
        }
    }
}
