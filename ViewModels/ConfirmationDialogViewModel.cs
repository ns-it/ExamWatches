using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ExamWatches.ViewModels
{
  public class ConfirmationDialogViewModel : INotifyPropertyChanged
    {
        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged("Message"); }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)

        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }

        public ConfirmationDialogViewModel()
        {
            Message = "Are you sure you want to continue?";
        }

        public ConfirmationDialogViewModel(string message)
        {
            Message = message;
        }
    }
}
