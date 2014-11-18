//using System;
//using System.Text;
//using System.Collections;
//
//using DPI.Interfaces;
//using DPI.Components;
//using DPI.Services;
//
//namespace DPI.ClientComp
//{
//	public class Order : IOrder
//	{
//		int id;
//		string orderType;
//		string name;
//		string phone;
//
//		/*		Properties		*/
//		public int Id { get { return id; }}
//		public string OrderType { get { return orderType; }}
//		public string Name { get { return name; }}
//		public string Phone { get { return phone; }}
//
//		public Order(int id, string orderType) 
//		{
//			this.id   = id;
//			this.orderType = orderType; 
//		}
//		public void Setup(CustData cd)
//		{
//			name = cd.NameFirst + " " + cd.NameLast;
//			phone = FormatPhone.Format(cd.PhNumber);
//		}
//		public void Setup(ICustInfo ci)
//		{
//			name =  ci.FormattedName;
//			phone = ci.PhNumber;
//		}
//	}
//}