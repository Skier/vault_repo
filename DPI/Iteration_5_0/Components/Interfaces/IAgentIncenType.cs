using System;

namespace DPI.Interfaces
{
	public interface IAgentIncenType
	{
		string IncentiveType		{ get; set; }
		bool IsRegReq				{ get; set; }
		bool IsEarlyRegAllowed		{ get; set; }
		bool IsOnePerStore			{ get; set; }
		bool IsOnePerPeriod			{ get; set; }
	}
}