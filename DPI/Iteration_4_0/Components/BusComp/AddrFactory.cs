//using System;
//using System.Collections;
//using System.Data;
//using System.Text;
//using System.Windows.Forms;
//using System.Data.SqlClient;
//using System.Data.SqlTypes;
//using DPI.Components;
//using DPI.Interfaces;
// 
//namespace DPI.Components
//{
//	[Serializable]
//	public class AddrFactory
//	{
//		public static CustAddress GetAddress(AddressType type)
//		{
//			switch(type)
//			{
//				case AddressType.Mailing :
//					return new MailAddress(type);
//
//				case AddressType.Service :
//					return new ServAddress(type);
//				
//				default:
//					throw new ArgumentException("Unknown address type: " + type.ToString());
//			}
//		}
//	}
//}