using System;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
	public class WorkflowSvc 
	{
		public static void LogStepInfo(IStepInfo step)  
		{
			UOW uow = null; 
  
			try
			{
				uow = new UOW("WorkflowSvc.LogStepInfo"); 

				if (step == null)
					throw new ApplicationException("Step info is required");

				new WIP_History(uow, step).add();
			}
			catch (Exception e) 
			{
				string s = e.Message;
			}
			finally
			{	
				uow.close();
			}					
		}
	}
}