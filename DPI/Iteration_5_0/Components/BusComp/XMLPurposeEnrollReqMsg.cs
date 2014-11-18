using System;
using System.ComponentModel;
using System.Xml;
using System.IO;
using System.Text;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.WebServices
{
	public class XMLPurposeEnrollReqMsg : XMLMessage
	{

	#region Constructors
		public XMLPurposeEnrollReqMsg(IProdPrice debitCard, 
										IDemand dmd, ICustInfo custInfo, 
										IProdPrice[] fees, IPaymentInfo2 payInfo,  
										DebitCardApp app, IUser user)
		{
			CreateMessage(debitCard, dmd, custInfo, fees, payInfo, app, user);
		}
	#endregion
		
	#region Methods
		
		void CreateMessage(IProdPrice debitCard, 
									IDemand dmd, ICustInfo custInfo, 
									IProdPrice[] fees, IPaymentInfo2 payInfo,  
									DebitCardApp app, IUser user)
		{						
			
			this.StartXmlDocument("request", GetRootAtrNames(), GetRootAtrVals(user));

			this.EndXmlDocument();
		}		
		string[] GetRootAtrNames()
		{
			string[] rootAtrNames = {"clerkid", "mid", "tid", "tran"};

			return rootAtrNames;
		}
		string[] GetRootAtrVals(IUser user)
		{
			string[] rootAtrVals = {user.ClerkId, user.LoginStoreCode, user.LoginStoreCode + "01", "ENROLL" };

			return rootAtrVals;
		}


	#endregion

	}
}
