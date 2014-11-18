using System;

public interface ISvcPlanDataResp
{
	string PlanStatus				{ get; }
	string Esn						{ get; }
	string ControlNumber			{ get; }
	DateTime CustomerSince			{ get; }
	DateTime StartDate				{ get; }
	DateTime ExpirationDate			{ get; }
	string Mdn						{ get; set; }
	decimal CashBalance				{ get; }
	string PlanType					{ get; }
	string PlanName					{ get; }
	string PinItemNumber			{ get; }
	string NWUsedMins				{ get; }
	string AnytimeUsedMins			{ get; }
	string WebUsedMins				{ get; }
	string TextUsedMins				{ get; }
	string ThreeGWebUsedMins		{ get; }
	string ThreeGPictureUsedMins	{ get; }
	string ThreeGPTTUsedMins		{ get; }
	bool   Pass						{ get; }
}