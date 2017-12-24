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
    [Activity(Label ="Calculate GPA", Icon = "@drawable/gpaCalc")]
    public class GpaActivity: Activity
    {
        public double value;
        public double gradePoint;
        public double TotalGradePoints;
        public double TotalCreditHours;
        public double WeightedGPA;

        private TextView displayGPA;
        private EditText semester; 
        private Button calcBtn, clearBtn, addBtn;

        private Spinner spinner1, spinner2, spinner3, spinner4, spinner5, spinner6, spinner7;
        private EditText course1, course2, course3, course4, course5, course6, course7;
        private EditText credit1, credit2, credit3, credit4, credit5, credit6, credit7;

        //initialize array that save letter grades
        public string[] GradesLetter = new string [7];

        //initialize array that store the credits
        double[] CreditsArray = new double[7];

        public double[] pointsArray = new double[] { 4.0, 4.0, 3.7, 3.3, 2.0, 1.7, 1.3, 1.0, 0.7};

        //string that holds the path information for our database
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbGPAs.db3");
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //set the view from the current GPA layout
            SetContentView(Resource.Layout.CurrentGPA);       

            calcBtn = FindViewById<Button>(Resource.Id.calculate_btn);
            clearBtn = FindViewById<Button>(Resource.Id.clear_btn);
            addBtn = FindViewById<Button>(Resource.Id.record_btn);
            semester = FindViewById<EditText>(Resource.Id.semester);
            displayGPA = FindViewById<TextView>(Resource.Id.gpaDisplay_textView);

            spinner1 = FindViewById<Spinner>(Resource.Id.spinner1);
            spinner2 = FindViewById<Spinner>(Resource.Id.spinner2);
            spinner3 = FindViewById<Spinner>(Resource.Id.spinner3);
            spinner4 = FindViewById<Spinner>(Resource.Id.spinner4);
            spinner5 = FindViewById<Spinner>(Resource.Id.spinner5);
            spinner6 = FindViewById<Spinner>(Resource.Id.spinner6);
            spinner7 = FindViewById<Spinner>(Resource.Id.spinner7);

            course1 = FindViewById<EditText>(Resource.Id.course1_textView);
            course2 = FindViewById<EditText>(Resource.Id.course2_textView);
            course3 = FindViewById<EditText>(Resource.Id.course3_textView);
            course4 = FindViewById<EditText>(Resource.Id.course4_textView);
            course5 = FindViewById<EditText>(Resource.Id.course5_textView);
            course6 = FindViewById<EditText>(Resource.Id.course6_textView);
            course7 = FindViewById<EditText>(Resource.Id.course7_textView);

            credit1 = FindViewById<EditText>(Resource.Id.credit1_textView);
            credit2 = FindViewById<EditText>(Resource.Id.credit2_textView);
            credit3 = FindViewById<EditText>(Resource.Id.credit3_textView);
            credit4 = FindViewById<EditText>(Resource.Id.credit4_textView);
            credit5 = FindViewById<EditText>(Resource.Id.credit5_textView);
            credit6 = FindViewById<EditText>(Resource.Id.credit6_textView);
            credit7 = FindViewById<EditText>(Resource.Id.credit7_textView);

            displayGPA = FindViewById<TextView>(Resource.Id.gpaDisplay_textView);

            //check for spinner value not selected or equal to null
            if(spinner1 != null)
            {
                spinner1.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            }
            else
            {
                //set the grade letter to empty
                GradesLetter[0] = "";
            }

            //check for spinner value not selected or equal to null
            if (spinner2 != null)
            {
                spinner2.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected1);
            }
            else
            {
                //set the grade letter to empty
                GradesLetter[1] = "";
            }

            //check for spinner value not selected or equal to null
            if (spinner3 != null)
            {
                spinner3.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected2);
            }
            else
            {
                //set the grade letter to empty
                GradesLetter[2] = "";
            }

            //check for spinner value not selected or equal to null
            if (spinner4 != null)
            {
                spinner4.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected3);
            }
            else
            {
                //set the grade letter to empty
                GradesLetter[3] = "";
            }

            //check for spinner value not selected or equal to null
            if (spinner5 != null)
            {
                spinner5.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected4);
            }
            else
            {
                //set the grade letter to empty
                GradesLetter[4] = "";
            }

            //check for spinner value not selected or equal to null
            if (spinner6 != null)
            {
                spinner6.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected5);
            }
            else
            {
                //set the grade letter to empty
                GradesLetter[5] = "";
            }

            //check for spinner value not selected or equal to null
            if (spinner7 != null)
            {
                spinner7.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected6);
            }
            else
            {
                //set the grade letter to empty
                GradesLetter[6] = "";
            }

            //create adpater for the spinner
            var adapter = ArrayAdapter.CreateFromResource(this,
                Resource.Array.grade_letters, Android.Resource.Layout.SimpleSpinnerDropDownItem);

            //specify the layout when the list of choices appears
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);           
          
            //calculate GPA : calc button
            calcBtn.Click += delegate
            {
                //get credit values and check for null Edittext
                if (!string.IsNullOrEmpty(credit1.Text))
                {
                    //convert user input to double and add it to the credit array
                    double cred1 = Convert.ToDouble(credit1.Text);
                    CreditsArray[0] = cred1;
                }
                else
                {
                    //set the credit value to 0 in the array
                    CreditsArray[0] = 0.00;
                }

                if (!string.IsNullOrEmpty(credit2.Text))
                {
                    //convert user input to double and add it to the credit array
                    double cred2 = Convert.ToDouble(credit2.Text);
                    CreditsArray[1] = cred2;
                }
                else
                {
                    //set the credit value to 0 in the array
                    CreditsArray[1] = 0.00;
                }

                if (!string.IsNullOrEmpty(credit3.Text))
                {
                    //convert user input to double and add it to the credit array
                    double cred3 = Convert.ToDouble(credit3.Text);
                    CreditsArray[2] = cred3;
                }
                else
                {
                    //set the credit value to 0 in the array
                    CreditsArray[2] = 0.00;
                }

                if (!string.IsNullOrEmpty(credit4.Text))
                {
                    //convert user input to double and add it to the credit array
                    double cred4 = Convert.ToDouble(credit4.Text);
                    CreditsArray[3] = cred4;
                }
                else
                {
                    //set the credit value to 0 in the array
                    CreditsArray[3] = 0.00;
                }

                if (!string.IsNullOrEmpty(credit5.Text))
                {
                    //convert user input to double and add it to the credit array
                    double cred5 = Convert.ToDouble(credit5.Text);
                    CreditsArray[4] = cred5;
                }
                else
                {
                    //set the credit value to 0 in the array
                    CreditsArray[4] = 0.00;
                }

                if (!string.IsNullOrEmpty(credit6.Text))
                {
                    //convert user input to double and add it to the credit array
                    double cred6 = Convert.ToDouble(credit6.Text);
                    CreditsArray[5] = cred6;
                }
                else
                {
                    //set the credit value to 0 in the array
                    CreditsArray[5] = 0.00;
                }

                if (!string.IsNullOrEmpty(credit7.Text))
                {
                    //convert user input to double and add it to the credit array
                    double cred7 = Convert.ToDouble(credit7.Text);
                    CreditsArray[6] = cred7;
                }
                else
                {
                    //set the credit value to 0 in the array
                    CreditsArray[6] = 0.00;
                }

                //calculation
                for ( int i = 0; i < GradesLetter.Length; i++)
                {
                    //get the grade letter from the grade letter array
                    string grade = GradesLetter[i];

                    //call the the get letter grade value with the grade letter
                    double point = calculateGPA(grade);

                    double credit = CreditsArray[i];

                    //calculate total credit hours
                    TotalCreditHours += credit;

                    //calculate grade point for each class
                    gradePoint = credit * point;

                    //calculate total grade point 
                    TotalGradePoints += gradePoint;
                }                                                               

                //calculate GPA
                WeightedGPA = Math.Round(TotalGradePoints / TotalCreditHours, 3);

                //display GPA
                displayGPA.Text = Convert.ToString(WeightedGPA);
            };

            //clear all entries : cler button
            clearBtn.Click += delegate
            {
                //call the function to clear everything
                Clearentries();
            };

            //store GPA to record : add gpa button
            addBtn.Click += delegate
            {
                string semesterStr = Convert.ToString(semester.Text);
                string gpaStr = Convert.ToString(displayGPA.Text);

                if (semesterStr == "" || gpaStr == "")
                {
                    Toast.MakeText(this, string.Format("You must indicate what semester"), ToastLength.Short).Show();
                }
                else
                {
                    //string that holds the path information for our database
                    string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbGPAs.db3");
                    //setup the db connection
                    var db = new SQLiteConnection(dbPath);
                    //create a contac object
                    GpaRecord record = new GpaRecord(semesterStr, gpaStr, TotalGradePoints, TotalCreditHours);
                    //store the objects in the table
                    db.Insert(record);

                    Toast.MakeText(this, string.Format("GPA was added to record"), ToastLength.Short).Show();

                    //clear everything
                    Clearentries();
                }
            };
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string letter = string.Format("{0}", spinner.GetItemAtPosition(e.Position));

            //add grade letter to the array
            GradesLetter[0] = letter;
        }

        private void spinner_ItemSelected1(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string letter = string.Format("{0}", spinner.GetItemAtPosition(e.Position));

            //add grade letter to the array
            GradesLetter[1] = letter;           
        }

        private void spinner_ItemSelected2(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string letter = string.Format("{0}", spinner.GetItemAtPosition(e.Position));

            //add grade letter to the array
            GradesLetter[2] = letter;
        }

        private void spinner_ItemSelected3(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string letter = string.Format("{0}", spinner.GetItemAtPosition(e.Position));

            //add grade letter to the array
            GradesLetter[3] = letter;
        }

        private void spinner_ItemSelected4(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string letter = string.Format("{0}", spinner.GetItemAtPosition(e.Position));

            //add grade letter to the array
            GradesLetter[4] = letter;           
        }

        private void spinner_ItemSelected5(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string letter = string.Format("{0}", spinner.GetItemAtPosition(e.Position));

            //add grade letter to the array
            GradesLetter[5] = letter;
        }

        private void spinner_ItemSelected6(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string letter = string.Format("{0}", spinner.GetItemAtPosition(e.Position));

            //add grade letter to the array
            GradesLetter[6] = letter;
        }

        public double calculateGPA(string g)
        {
            if (g == "A+")
            {
                value = 4.0;
            }
            else if (g == "A")
            {
                value = 4.0;
            }
            else if (g == "A-")
            {
                value = 3.7;
            }
            else if (g == "B+")
            {
                value = 3.3;
            }
            else if (g == "B")
            {
                value = 3.0;
            }
            else if (g == "B-")
            {
                value = 2.7;
            }
            else if (g == "C+")
            {
                value = 2.3;
            }
            else if (g == "C")
            {
                value = 2.0;
            }
            else if (g == "C-")
            {
                value = 1.7;
            }
            else if (g == "D+")
            {
                value = 1.3;
            }
            else if (g == "D")
            {
                value = 1.0;
            }
            else if (g == "D-")
            {
                value = 0.7;
            }
            else if (g == "")
            {
                value = 0.0;
            }

            return value;
        }

        protected void Clearentries()
        {
            //create adpater for the spinner
            var adapter = ArrayAdapter.CreateFromResource(this,
                Resource.Array.grade_letters, Android.Resource.Layout.SimpleSpinnerDropDownItem);

            //specify the layout when the list of choices appears
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            //apply the adapter to the spinner
            spinner1.Adapter = adapter;
            spinner2.Adapter = adapter;
            spinner3.Adapter = adapter;
            spinner4.Adapter = adapter;
            spinner5.Adapter = adapter;
            spinner6.Adapter = adapter;
            spinner7.Adapter = adapter;

            //clear all the courses 
            course1.Text = "";
            course2.Text = "";
            course3.Text = "";
            course4.Text = "";
            course5.Text = "";
            course6.Text = "";
            course7.Text = "";

            //and credits entered
            credit1.Text = "";
            credit2.Text = "";
            credit3.Text = "";
            credit4.Text = "";
            credit5.Text = "";
            credit6.Text = "";
            credit7.Text = "";

            //clear the gpa calculation result, if any and the semester
            displayGPA.Text = "";
            semester.Text = "";

            course1.RequestFocus();
        }
    }
}