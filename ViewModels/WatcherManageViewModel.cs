using ExamWatches.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamWatches.ViewModels
{
    public class WatcherManageViewModel : ObservableObject
    {

        private string _fullName;
        private Watcher _model;
        public Watcher Model
        {
            get { return _model; }
            set { _model = value; OnPropertyChanged("Model"); }
        }

        public WatcherManageViewModel(Watcher watcher) { _model = watcher; }
        public WatcherManageViewModel() : this(new Watcher()) { }



        public long Id
        {
            get { return _model.Id; }
            set { _model.Id = value; OnPropertyChanged("Id"); }
        }
        public string FirstName
        {
            get { return _model.FirstName; }
            set
            {
                _model.FirstName = value; OnPropertyChanged("FirstName");
                OnPropertyChanged("FullName");
            }
        }
        public string MiddleName
        {
            get { return _model.MiddleName; }
            set
            {
                _model.MiddleName = value; OnPropertyChanged("MiddleName");
                OnPropertyChanged("FullName");
            }
        }
        public string LastName
        {
            get { return _model.LastName; }
            set
            {
                _model.LastName = value; OnPropertyChanged("LastName");
                OnPropertyChanged("FullName");
            }
        }
        public string FullName
        {
            get
            {
                //return _model.FullName;
                _fullName = _model.FirstName + " " + _model.MiddleName + " " + _model.LastName;
                return _fullName;

            }
            set
            {
                //_model.FullName = value;
                _fullName = _model.FirstName + " " + _model.MiddleName + " " + _model.LastName;
                OnPropertyChanged("FullName");
            }
        }
        public string Job
        {
            get { return _model.Job; }
            set { _model.Job = value; OnPropertyChanged("Job"); }
        }
        public string Class
        {
            get
            {
                return _model.Class switch
                {
                    1 => "أولى",
                    2 => "ثانية",
                    3 => "ثالثة",
                    4 => "رابعة",
                    5 => "خامسة",
                    _ => "",
                };
            }
            set
            {
                _model.Class = value switch
                {
                    "أولى" => 1,
                    "ثانية" => 2,
                    "ثالثة" => 3,
                    "رابعة" => 4,
                    "خامسة" => 5,
                    _ => 0,
                };
                ;
                OnPropertyChanged("Class");
            }
        }
        public string Certificate
        {
            get { return _model.Certificate; }
            set { _model.Certificate = value; OnPropertyChanged("Certificate"); }
        }
        public short? WorkLocationId
        {
            get { return _model.WorkLocationId; }
            set { _model.WorkLocationId = value; OnPropertyChanged("WorkLocationId"); }
        }

    }
}
