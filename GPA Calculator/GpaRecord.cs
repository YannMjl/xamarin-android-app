using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace GPA_Calculator
{
    class GpaRecord
    {
        [PrimaryKey, AutoIncrement]
        public int EmpID { get; set; }
        public string Semester { get; set; }
        public string GPA { get; set; }
        public double Points { get; set; }
        public double SemCredits { get; set; }

        public GpaRecord(string semester, string gpa, double p, double c)
        {
            Semester = semester;
            GPA = gpa;
            Points = p;
            SemCredits = c;
        }

        public GpaRecord()
        {

        }

        public override string ToString()
        {
            return Semester + " GPA is : " + GPA;
        }
    }
}