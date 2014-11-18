using System;
using DPI.Interfaces;
using CrystalDecisions.CrystalReports.Engine;

namespace DPI.Reports 
{
	public class Store_DayTotalsReportFactory
	{
		public static ReportClass GetDayTotalsReport(Store_DayTotalsStyle reportStyle)
		{
			if (reportStyle == Store_DayTotalsStyle.Summary)				
				return new DPI.Reports.Store_DayTotalsSummary();

			if (reportStyle == Store_DayTotalsStyle.Detail)
				return new DPI.Reports.Store_DayTotalsDetail();
				
			throw new ArgumentException("Invalid report style: " + reportStyle.ToString());
		}
	}
}
