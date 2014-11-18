using System;
 
namespace DPI.Interfaces
{	
	public interface ICustInfoExt
	{
		ICustInfo2 CustInfo { get; }
		IAddr MailAddr { get; }
		IAddr ServAddr { get; }

		ICustInfoExt FindCustInfoById(IUOW uow, int id);
		ICustInfoExt FindCustInfoByDmd(IUOW uow, int dmdId);
	}
}