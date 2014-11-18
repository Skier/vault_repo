using System;
using System.Text;
using System.Collections;

using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable]
	public class Order : IOrder
	{
		int id;		
		int payInfoId;
		string name;
		string phone;
		DateTime date;
		string confNumber;
		int    accNumber;
		string orderType;

		/*		Properties		*/
		public int Id				{ get { return id;			}}		
		public string Name			{ get { return name;		}}
		public string Phone			{ get { return phone;		}}
		public DateTime Date		{ get { return date;		}}
		public int PayInfoId        { get { return payInfoId;   }}
		public string OrderType		{ get { return orderType;	}}
		public string ConfNumber	
		{ 
			get { return confNumber; }
			set { confNumber = value; }	
		}
		public int	  AccNumber		
		{
			get { return accNumber;	}
			set { accNumber = value; }
		}
		/*		Constructors		*/
		public Order(int id, string orderType, int payInfoId, DateTime date) 
		{
			this.id   = id;
			this.orderType = orderType;
			this.payInfoId = payInfoId;
			this.date = date;
		}
		/*		Methods		*/
		public void Setup(CustData cd)
		{
			this.name = cd.NameFirst + " " + cd.NameLast;
			this.phone = FormatPhone.Format(cd.PhNumber);
			this.accNumber = cd.AccNumber;
		}
		public void Setup(ICustInfo ci)
		{
			this.name =  ci.FormattedName;
			this.phone = string.Empty;
		}
	}
}