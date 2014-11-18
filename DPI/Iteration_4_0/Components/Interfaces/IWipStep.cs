using System;

namespace DPI.Interfaces
{
	public interface IWipStep
	{
		IWipStep NextStep   { set; } 
		IWipStep PrevStep   { set; } 
		IWipStep ReworkStep { set; } 
		IWipStep SkipStep   { set; } 
		string Title        { get; }
		bool HasNext        { get; } 
		bool HasPrev        { get; } 
		bool HasRework      { get; } 
		bool HasSkip        { get; }
		bool IsCompleted    { get; }
		IWorkflow Workflow  { get; }
	}
}