using System;

namespace DPI.Interfaces
{
	public interface IWirelessDeviceData
	{
		bool   Pass						{ get; }
		string ErrMessage				{ get; }
		string ESN						{ get; }
		string ESNHex					{ get; }
		string MDN						{ get; }
		string MSID						{ get; }
		string CarrierMSL				{ get; }
		string CurrentMSL				{ get; }
		string CSA						{ get; }
		string SubscriberID				{ get; }
		DpiWLPlanStatus PlanStatus		{ get; }
		bool   StatusPending			{ get; }
		string CarrierName				{ get; }
		string Provider					{ get; set; }
	}
}