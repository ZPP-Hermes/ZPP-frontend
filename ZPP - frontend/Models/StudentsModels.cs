using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace ZPP___frontend.Models
{
    public class BaseSubjectsMarks
    {
        public int StudentId { get; set; }
        public String StudentName { get; set; }
        public int asdMark { get; set; }
        public int am1Mark { get; set; }
        public int am2Mark { get; set; }
        public int pmatMark { get; set; }
        public int galMark { get; set; }
        public int poMark { get; set; }
        public int ippMark { get; set; }
        public int wpiMark { get; set; }
    }
}