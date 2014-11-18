using System;

namespace DPI.Interfaces
{
	public interface IServicePlan
	{
		string Pin						{ get; }
		string ControlNumber			{ get; }
		string Description				{ get; }
		bool   CurrentlyInUse			{ get; }
		DpiWLPlanStatus PlanStatus		{ get; }
		DateTime LoadDate				{ get; }
		DateTime StartDate				{ get; }
		DateTime ExpirationDate			{ get; }		
	}
}