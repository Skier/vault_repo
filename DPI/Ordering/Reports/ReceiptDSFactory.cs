using System;
using System.Data;
using System.Text;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;

using DPI.Components;
using DPI.Interfaces;
using DPI.Ordering;
using DPI.Services;

namespace DPI.Reports
{
	public class ReceiptDSFactory
	{
	#region Methods
		public static ReportClass SetDataSet(IWIP wip, IUser user, ReportClass rc, IReceipt2 rct)
		{
			if (rc is InfNewCell_Rcpt)
			{				
				rc.SetDataSource(new InfActReceipt(wip, user, rc, rct));
				return rc;
			}
			if (rc is InfMonthly_Rcpt)
			{
				rc.SetDataSource(new InfActReceipt(wip, user, rc, rct));
				return rc;
			}

			return null;
		}
	#endregion

	#region Implementation

		
	#endregion
	}
}