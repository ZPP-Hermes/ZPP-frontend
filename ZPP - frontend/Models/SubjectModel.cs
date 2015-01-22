using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;

namespace ZPP___frontend.Models
{
    public class SubjectModel
    {
        public String SelectedSubject1 { get; set; }
        public String SelectedSubject2 { get; set; }
        public String SelectedSubject3 { get; set; }
        public String SelectedSubject4 { get; set; }
        public String SelectedSubject5 { get; set; }
        public String SelectedSubject6 { get; set; }
        public String SelectedSubject7 { get; set; }
        public String SelectedSubject8 { get; set; }
        public String SelectedSubject9 { get; set; }
        public String SelectedSubject10 { get; set; }
        public String SelectedMark1 { get; set; }
        public String SelectedMark2 { get; set; }
        public String SelectedMark3 { get; set; }
        public String SelectedMark4 { get; set; }
        public String SelectedMark5 { get; set; }
        public String SelectedMark6 { get; set; }
        public String SelectedMark7 { get; set; }
        public String SelectedMark8 { get; set; }
        public String SelectedMark9 { get; set; }
        public String SelectedMark10 { get; set; }
        public List<SelectListItem> SubjectsList { get; set; }
        public List<SelectListItem> MarksList { get; set; }
    }
}