package md55facf9f1b2b88a7b06c0ba26a15da747;


public class GpaActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("GPA_Calculator.GpaActivity, GPA Calculator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", GpaActivity.class, __md_methods);
	}


	public GpaActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == GpaActivity.class)
			mono.android.TypeManager.Activate ("GPA_Calculator.GpaActivity, GPA Calculator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
