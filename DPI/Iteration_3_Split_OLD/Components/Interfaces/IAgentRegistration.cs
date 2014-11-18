using System;

namespace DPI.Interfaces
{
	public interface IAgentRegistration
	{
		int Id					{ get;		}
		string RegType			{ get; set; }
		int ExclusiveIncentive	{ get; set; }
		DateTime RegDate		{ get; set; }
		DateTime EffStartDate	{ get; set; }
		DateTime EffEnddate		{ get; set; }
		bool IsAgreed			{ get; set; }
		string FirstName		{ get; set; }
		string LastName			{ get; set; }
		int Title				{ get; set; }
		string Phone			{ get; set; }
		string Email			{ get; set; }
		int AddrId			    { get; set; }
		IAddr2 Address			{ get; set; }
		int ConfNum				{ get; set; }
		int UserAcct			{ get; set; }
		string StoreCode		{ get; set; }
		int CorpId				{ get; set; }
		string Status			{ get; set; }
	}
}