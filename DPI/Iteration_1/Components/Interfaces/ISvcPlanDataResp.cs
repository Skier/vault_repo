using System;

public interface ISvcPlanDataResp
{
	string PlanStatus				{ get; }
	string Esn						{ get; }
	string ControlNumber			{ get; }
	DateTime CustomerSince			{ get; }
	DateTime StartDate				{ get; }
	DateTime ExpirationDate			{ get; }
	string Mdn						{ get; }
	decimal CashBalance				{ get; }
	string PlanType					{ get; }
	string PlanName					{ get; }
	string PinItemNumber			{ get; }
	string NWUsedMins				{ get; }
	string AirtimeUsedMins			{ get; }
	string WebUsedMins				{ get; }
	string TextUsedMins				{ get; }
	string ThreeGWebUsedMins		{ get; }
	string ThreeGPictureUsedMins	{ get; }
	string ThreeGPTTUsedMins		{ get; }
}