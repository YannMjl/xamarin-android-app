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
    [Activity (Label ="View Record GPA", Icon = "@drawable/gpaShow")]
    public class CumulativeGPActivity: Activity
    {   
        //string that holds the path information for our database
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbGPAs.db3");

        List<GpaRecord> myRecord;
        int id;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //set the view from the view score layout
            SetContentView(Resource.Layout.ViewGPAs);

            //setup the db connection
            var db = new SQLiteConnection(dbPath);

            //setup a table
            db.CreateTable<GpaRecord>();

            myRecord = new List<GpaRecord>();
            var table = db.Table<GpaRecord>();

            foreach (var item in table)
            {
                myRecord.Add(item);
            }

            //get our listview 
            var lv = FindViewById<ListView>(Resource.Id.gpaRecordView);

            //assign the adapter
            lv.Adapter = new DisplayAdapter(this, myRecord);

            //we have to register the listview for context menu
            //so that the system knows the behavior to use
            RegisterForContextMenu(lv);
        }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            if (v.Id == Resource.Id.gpaRecordView)
            {
                var info = (AdapterView.AdapterContextMenuInfo)menuInfo;
                menu.SetHeaderTitle("Menu Title");
                var menuItems = Resources.GetStringArray(Resource.Array.menu);
                for (int i = 0; i < menuItems.Length; i++)
                {
                    menu.Add(Menu.None, i, i, menuItems[i]);
                }
            }
        }

        public override bool OnContextItemSelected(IMenuItem item)
        {
            var info = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;
            var index = item.ItemId;
            var menuItem = Resources.GetStringArray(Resource.Array.menu);
            var menuItemName = menuItem[index];
            GpaRecord taskName = myRecord[info.Position];
            id = info.Position;
          
            if (menuItemName == "Delete")
            {
                //delete the contact from the DB and update the displayed contacts
                var builder = new AlertDialog.Builder(this);
                builder.SetMessage("This will delete Activity: " + taskName.Semester + " from the record. OK?");
                builder.SetPositiveButton("OK", (s, e) => {
                    //they clicked OK
                    //delete the contact from the db
                    var db = new SQLiteConnection(dbPath);
                    db.Delete(taskName);

                    //delete the contact from the list
                    myRecord.Remove(taskName);

                    //Since we are in the main activity
                    this.Recreate();
                });
                builder.SetNegativeButton("Cancel", (s, e) =>
                {
                    //they clicked cancel
                    return;
                });
                builder.Create().Show();

            }

            return true;
        }
    }
}