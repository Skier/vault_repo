using System;

namespace DPI.Interfaces
{
	public interface IPastReminderNotice
	{
		int AccNumber		{get;}
		string PhNumber		{get;}		
		DateTime Bill_Date	{get;}
		string Filename		{get;}
	}
}