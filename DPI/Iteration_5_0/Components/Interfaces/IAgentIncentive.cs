using System;

namespace DPI.Interfaces
{
	public interface IAgentIncentive
	{
		int Id					{ get;		}
		string IncentType		{ get; set; }
		string IncentName		{ get; set; }
		string IncentDescr		{ get; set; }
		DateTime EffStartDate	{ get; set; }
		DateTime EffEndDate		{ get; set; }
		string IncentCond		{ get; set; }
		string Eligibility		{ get; set; }
		string Status			{ get; set; }
	}
}