using System;

namespace DPI.Interfaces
{
	public interface IWorkflow
	{
		string Name      { get; }
		string ImageTag  { get; }
		int Count        { get; }

		int CurrStep(IWipStep curr);
		IWipStep FirstStep { get; }
	} 
}