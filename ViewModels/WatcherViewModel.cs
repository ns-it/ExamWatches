using ExamWatches.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamWatches.ViewModels
{
    public class WatcherViewModel
    {

        public string FullName { get; set; }
        public short? Class { get; set; }
        public bool IsSelected { get; set; }
    }
}
