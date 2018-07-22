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
    class DisplayAdapter : BaseAdapter<GpaRecord>
    {
        List<GpaRecord> myRecord; 
        Activity context;

        public DisplayAdapter(Activity context, List<GpaRecord> gpaRecord) : base()
        {
            this.myRecord = gpaRecord;
            this.context = context;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override GpaRecord this[int position]
        {
            get
            {
                return myRecord[position];
            }
        }

        public override int Count
        {
            get
            {
                return myRecord.Count;
            }
        }        

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //this sets up to re-use an existing view if one is available
            View view = convertView;
            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            }

            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = myRecord[position].ToString();
            return view;
        }
    }
}
