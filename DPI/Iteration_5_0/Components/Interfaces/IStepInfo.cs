using System;

namespace DPI.Interfaces
{
	public interface IStepInfo
	{
		string   Workflow	  { get; set; } 
		int      WipId		  { get; set; }
		string   StepName	  { get; set; }
		DateTime StepStart	  { get; set; }
		DateTime StepEnd	  { get; set; }
		string   BusObj		  { get; set; }
		string   BusObjType   { get; set; }
		string   NextStepName { get; set; }
		bool     IsCompleted  { get; set; }
		string   User		  { get; set; }
	}
}	