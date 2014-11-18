using System;

namespace DPI.Interfaces
{
	public interface IWebSvcQueue : IMapObj
	{
		int Id					{ get; }
		string QueType			{ get; set; }
		string WSProvider		{ get; set; }
		string WebMethod		{ get; set; }
		string ReversalMethod   { get; set; }
		string StoreCode		{ get; set; }
		string ClerkId			{ get; set; }
		string BusObject		{ get; set; }
		string BusObjId			{ get; set; }
		DateTime InitDate		{ get; }
		DateTime LastAccessDate	{ get; set; } 
		string InitReasonCode   { get; set; }
		string LastReasonCode   { get; set; }
		string InitialMsg		{ get; set; }
		string LastMsg			{ get; set; }
		int Attemps				{ get;  }
		string Xml				{ get; set; }
		string ReversalXml		{ get; set; }
		string Status			{ get; set; }
		string InitRespXml		{ get; set; }
		string LastRespXml		{ get; set; }
		IId Dom					{ get; set; }

		// Methods
		void FollowUp();
		void SaveEntry();
	}
}