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

namespace GPA_Calculator
{
    [Activity(Label = "Help", Icon = "@drawable/Help")]
    public class HelpActivity: Activity
    {
        private TextView helpTxt;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //set the view from the help layout
            SetContentView(Resource.Layout.Help);

            helpTxt = FindViewById<TextView>(Resource.Id.help_textView);

            //display help instructions
            helpTxt.Text = "Your GPA is calculated by dividing the total amount of grade points earned by the total amount of credit hours attempted."
                         + " Your grade point average may range from 0.0 to a 4.0.";
        }
    }
}