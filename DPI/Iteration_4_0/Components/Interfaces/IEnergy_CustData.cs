using System;

namespace DPI.Interfaces
{
	public interface IEnergy_CustData
	{
		IDomKey		IKey					{ get; }
		int			ID						{ get; }
		string		SiteCode				{ get; set; }
		string		QuoteId					{ get; set; }
		int			AccountNumber			{ get; set; }
		string		Pin						{ get; set; }
		string		Address1				{ get; set; }
		string		Address2				{ get; set; }
		string		City					{ get; set; }
		string		State					{ get; set; }
		string		Zip						{ get; set; }
		string		Zip4					{ get; set; }
		string		SAddress1				{ get; set; }
		string		SAddress2				{ get; set; }
		string		SCity					{ get; set; }
		string		SState					{ get; set; }
		string		SZip					{ get; set; }
		string		SZip4					{ get; set; }
		string		NameFirst				{ get; set; }
		string		NameLast				{ get; set; }
		string		NameMiddle				{ get; set; }
		string		Ph1						{ get; set; }
		string		Ph2						{ get; set; }
		string		Email					{ get; set; }
		string		Fax						{ get; set; }
		string		PreferedContactMethod	{ get; set; }
		string		Ssn						{ get; set; }
		string		DL						{ get; set; }
		string		DlState					{ get; set; }
		DateTime	DOB						{ get; set; }
		string		PermitName				{ get; set; }
		string		CustomerNumberRef		{ get; set; }
		string		DoingBusAs				{ get; set; }
		bool		SpecialNeedsReq			{ get; set; }
		bool		LowIncomeCustomer		{ get; set; }
		string		Language				{ get; set; }
		string		Status					{ get; set; }
		DateTime	DateInserted			{ get; set; }
		DateTime	DateModified			{ get; set; }
		DateTime	ServiceStartDate		{ get; set; }
		string		FullMailingAddress		{ get; }
		string		FullServiceAddress		{ get; }
	}
}