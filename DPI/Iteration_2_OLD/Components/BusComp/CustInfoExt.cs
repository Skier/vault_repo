using System;
using System.Text;
using System.Collections;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Components
{
	[Serializable] 
	public class CustInfoExt : ICustInfoExt
	{
		ICustInfo2 custInfo;
		IAddr2 mailAddr;
		IAddr2 servAddr;

		public ICustInfo2 CustInfo { get { return custInfo;} }
		public IAddr MailAddr { get{ return mailAddr;} }
		public IAddr ServAddr { get{ return servAddr;} }

		public CustInfoExt()
		{
		}
		public CustInfoExt(ICustInfo2 custInfo, IAddr2 mailAddr, IAddr2 servAddr)
		{
			
			this.custInfo = custInfo;
			this.mailAddr = mailAddr;
			this.servAddr = servAddr;
		}
		public ICustInfoExt FindCustInfoById(IUOW uow, int id)
		{
			CustInfo ci = DPI.Components.CustInfo.find((UOW)uow, id); 
			
			if (ci.MailAddID > 0)
				mailAddr = CustAddress.find((UOW)uow, ci.MailAddID);
	
			if (ci.ServAddID > 0)
				servAddr = CustAddress.find((UOW)uow, ci.ServAddID);

			return (new CustInfoExt(ci, mailAddr, servAddr));
		}
		public ICustInfoExt FindCustInfoByDmd(IUOW uow, int dmdId)
		{
			IDemand dmd = Demand.find((UOW)uow, dmdId);			
			return new CustInfoExt().FindCustInfoById(uow, dmd.ConsId);
		}
	}
}