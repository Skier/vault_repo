using System;
using DPI.Interfaces;

namespace DPI.Components
{
	/// <summary>
	/// Summary description for CustInfoExt2.
	/// </summary>
	public class CustInfoExt2 : CustInfoExt, ICustInfoExt2

	{
		DateTime activDate;
		public CustInfoExt2()
		{
		}

		public CustInfoExt2(ICustInfo2 custInfo, IAddr2 mailAddr, IAddr2 servAddr, DateTime activDate): 
			base(custInfo, mailAddr, servAddr)
		{
			this.activDate = activDate;
		}

		public DateTime ActivDate
		{
			get { return this.activDate;}
			set 
			{
				this.activDate = value;
			}
		}
	}
}
