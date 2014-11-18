using System;

namespace DPI.Interfaces
{
	public interface IServicePlanList
	{
		bool   Pass						{ get; }
		string ErrMessage				{ get; }
		IServicePlan[] SvcPlans			{ get; }
	}
}