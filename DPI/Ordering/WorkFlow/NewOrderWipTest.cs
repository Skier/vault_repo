using System;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using DPI.Interfaces;
using DPI.Components;
using DPI.ClientComp;

namespace DPI.Ordering
{
		
	public class NewOrderWipTest
	{
		User user;
		WIP wip;

		string       zip = "75080";
		IILECInfo[]  avaIlecs = new IILECInfo[] { null, null };
		IProdPrice   selectedBasicService = new ProdPrice(); 
		IProdPrice[] topProducts = new 	IProdPrice[] { null, null, null };

		public static void Main()
		{
			NewOrderWipTest t = new NewOrderWipTest();
			t.user = new User();
			t.user.DisplayName = "test";
			t.user.ClerkId ="TEST";
			t.user.LoginStoreCode = "Enter code";
			t.wip = new NewOrderWip(t.user);
			t.loadData();
	//		t.getData();
			t.spin();
			t.rework();
			t.skip();
			Console.Read();
		}
		void loadData()
		{
			wip["Zip"] = zip;
			wip["AvaIlecs"] = avaIlecs;
			wip["SelectedBasicService"] = selectedBasicService;
			wip["TopProducts"] = topProducts;
		}
		void getData()
		{
			string       zip1 = (string)wip["Zip"];
			IILECInfo[]  avaIlecs1 = (IILECInfo[])wip["AvaIlecs"];
			IProdPrice   selectedBasicService1 = (IProdPrice )wip["SelectedBasicService"]; 
			IProdPrice[] topProducts1 = (IProdPrice[])wip["TopProducts"];
			
			try
			{
				zip1 = (string)wip["xxx"];
			}
			catch (ArgumentException ae)
			{
				Console.WriteLine(ae.Message);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			try
			{
				zip1 = (string)wip["AvaIlecs"];
			}
			catch (InvalidCastException ice)
			{
				Console.WriteLine(ice.Message);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
		void spin()
		{
			while(wip.HasNext)
				Console.WriteLine("Next step = " + wip.Next());
			
			while(wip.HasPrev)
				Console.WriteLine("Previous step = " + wip.Prev());
		}
		void rework()
		{
			wip = new NewOrderWip(user);
			while(!wip.HasRework)
				wip.Next();

			Console.WriteLine("Starting rework @ '{0}'", wip.Url);
			wip.Rework();
			Console.WriteLine("Current step: {0}, url '{1}'", wip.Title, wip.Url);
		}
		void skip()
		{
			wip = new NewOrderWip(user);
			while(!wip.HasSkip)
				wip.Next();

			Console.WriteLine("Starting ski @ '{0}'", wip.Url);
			wip.Skip();
			Console.WriteLine("Current step: {0}, url '{1}'", wip.Title, wip.Url);
		}
	}
}