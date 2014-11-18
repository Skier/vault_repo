using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Text;

using DPI.Services;
using DPI.Interfaces;

namespace DPI.ClientComp
{
	public class VoidManager
	{

	#region Static Methods
		public static Table GetVoidableTrans(IMap imap, string storeCode, string tranType, EventHandler eh)
		{
			VoidTranType type = GetVoidType(tranType);

			switch (type)
			{
				case VoidTranType.Verifone :
					return new TableTransVoid(eh, 
						LocalTransactionSvc.GetVoidableTransactions(imap, 
						storeCode, DateTime.Now), VoidTranType.Verifone);
									
				case VoidTranType.Wireless :
					return new TableIntTransVoid(eh, 
						WirelessTranSvc.GetVdblTransByStore(imap, storeCode), VoidTranType.Wireless);

				case VoidTranType.FinProd :
					return new Table();
		
				default :
					throw new ArgumentException("Uknown Transaction Type: " + tranType.ToString());
			}
		}
		public static VoidTranType GetVoidType(string type)
		{
			return (VoidTranType)Enum.Parse(typeof(VoidTranType), type);
		}

		public static void VoidTransaction(IMap imap, IUser user, VoidTranType tranType, int tranId)
		{
			switch (tranType)
			{
				case VoidTranType.Verifone :
					LocalTransactionSvc.VoidTransaction(imap, user, tranId);
					break;
				
				case VoidTranType.Wireless :
					WirelessTranSvc.VoidTransaction(imap, user, tranId);
					break;
		
				case VoidTranType.FinProd :
					break;
		
				default :
					throw new ArgumentException("Uknown Transaction Type: " + tranType.ToString());
			}
		}
		public static Table PrintVoidedTran(IMap imap, VoidTranType tranType, int tranId)
		{
			switch (tranType)
			{
				case VoidTranType.Verifone :
					return new Table();
				
				case VoidTranType.Wireless :
					return new TableWLVoidRcpt
						(WirelessTranSvc.GetWSTran(imap, tranId), tranType);
				
		
				case VoidTranType.FinProd :
					return new Table();
		
				default :
					throw new ArgumentException("Uknown Transaction Type: " + tranType.ToString());
			}
		}
	#endregion
	
	}
}