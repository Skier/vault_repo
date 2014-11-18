using System;
 
namespace DPI.Interfaces
{	
	public interface IPymtPostProvider
	{
		void PostPymt(IUOW uow, IUser user, IPayInfo payInfo, string receitId);
		void PostReversal(IUOW uow, IUser user, int tranId);
	}
}