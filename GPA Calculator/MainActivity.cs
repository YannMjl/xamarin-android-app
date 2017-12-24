using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;

namespace GPA_Calculator
{
    [Activity(Label = "GPA_Calculator", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button helpBtn;
        private Button gpaBtn;
        private Button GpaRecordBtn;
        private Button ViewCumGpaBtn; 

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            helpBtn = FindViewById<Button>(Resource.Id.help_btn);
            gpaBtn = FindViewById<Button>(Resource.Id.gpa_btn);
            GpaRecordBtn = FindViewById<Button>(Resource.Id.CumGPA_btn);
            ViewCumGpaBtn = FindViewById<Button>(Resource.Id.viewCumgpa_btn);

            //view help about the app : help button
            helpBtn.Click += (object sender, EventArgs e) =>
            {
                var intent = new Intent(this, typeof(HelpActivity));
                StartActivity(intent);
            };

            //input info and calculate GPA : gpa button
            gpaBtn.Click += (object sender, EventArgs e) =>
            {
                var intent = new Intent(this, typeof(GpaActivity));
                StartActivity(intent);
            };

            //view gpa record: gpa record button
            GpaRecordBtn.Click += (object sender, EventArgs e) =>
            {
                var intent = new Intent(this, typeof(CumulativeGPActivity));
                StartActivity(intent);
            };

            //view cumulative gpa: view cumulative gpa button activity
            ViewCumGpaBtn.Click += (object sender, EventArgs e) =>
            {
                var intent = new Intent(this, typeof(ViewCumulativeGPA));
                StartActivity(intent);
            };
        }
    }
}

