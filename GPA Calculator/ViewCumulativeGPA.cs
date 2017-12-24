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
using System.IO;
using SQLite;

namespace GPA_Calculator
{
    [Activity(Label = "View Cumulative GPA", Icon = "@drawable/gpaShow")]
    class ViewCumulativeGPA : Activity
    {
        private Button viewGPA;
        private TextView displayCumGPA;

        double CumGPA, TotalQualityPoints, toltalCredits;

        //string that holds the path information for our database
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbGPAs.db3");

        List<GpaRecord> myRecord;
        //int id;

        protected override void OnCreate(Bundle savedInstanceState) 
        {
            base.OnCreate(savedInstanceState);

            //set the view from the Cumulative GPA layout
            SetContentView(Resource.Layout.CumulativeGPA);

            viewGPA = FindViewById<Button>(Resource.Id.ViewCumGPA_btn);
            displayCumGPA = FindViewById<TextView>(Resource.Id.display_CumGPA);

            //setup the db connection
            var db = new SQLiteConnection(dbPath);

            //setup a table
            db.CreateTable<GpaRecord>();

            myRecord = new List<GpaRecord>();
            var table = db.Table<GpaRecord>();

            foreach (var item in table)
            {
                if (item.Points == 0 || item.SemCredits == 0)
                {
                    TotalQualityPoints = 0;
                    toltalCredits = 0;
                }
                else
                {
                    TotalQualityPoints += item.Points;
                    toltalCredits += item.SemCredits;
                }               
            }

            //calculate cumulative GPA
            CumGPA = Math.Round(TotalQualityPoints / toltalCredits, 3);

            //diaply cumulative GPAs : view button activity
            viewGPA.Click += delegate
            {                              
                displayCumGPA.Text = Convert.ToString(CumGPA);                           
            };
        }
    }
}