namespace DPI.Interfaces
{	
	public interface IDebCardProvider
	{
		IDebitCardReceipt Enroll(IUOW uow, IPayInfo payInfo, ICardApp app, IWireless_Transactions tran);
		IDebitCardReceipt Reload(IUOW uow, IPayInfo payInfo, ICardApp app, IWireless_Transactions tran);
		IReceipt Send(IUOW uow, IUser user, string action, IDomObj[] objects); 
	}
	public interface IWebSvcProvider
	{
		string Provider { get; set; }
		bool IsWirelessXactPosted { get; }
		bool IsActive(IUOW uow, ICellPhoneInfo ci, IWebSvcQueue logQue);
		bool IsDateValid(IUOW uow, ICellPhoneInfo ci, IWebSvcQueue logQue);
		void ProcessQueue(IWebSvcQueue entry);
		ISvcPlanDataResp GetAvailableBalance(string phone, string esn);
		IReceipt Send(IUOW uow, IUser user, string action, IDomObj[] objects); 
		IPinProduct[] GetProducts(IUOW uow, IUser user, IWebSvcQueue webQue, int vendorID, int areaCode, int prefix);
		IPinProduct[] GetProducts(IUOW uow, IUser user, IWebSvcQueue webQue, int vendorID);		
	}	
}