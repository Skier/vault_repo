using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.package;
using Dalworth.Server.SDK;

namespace Dalworth.Server.Servman.Service
{
    public class NetServmanSyncModel
    {
        #region PrintTomorrowsVisits

        public static void PrintTomorrowsVisits()
        {
            if (!Configuration.AutomatedVisitPrint)
                return;

            if (DateTime.Now.TimeOfDay < Configuration.TomorrowVisitsPrintTime.TimeOfDay)
                return;

            SyncToolInfo syncToolInfo = SyncToolInfo.FindByPrimaryKey(1);
            if (syncToolInfo.LastTomorrowPrintJobDate.Date == DateTime.Now.Date)
                return;

            Host.Trace("Tomorrow visits print job", "Printing tomorrow Visits");
            List<Visit> visits = Visit.FindNotPrintedBySyncTool(DateTime.Now.AddDays(1));
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                visits.AddRange(Visit.FindNotPrintedBySyncTool(DateTime.Now.AddDays(2)));

            if (visits.Count == 0)
                Host.Trace("Tomorrow visits print job", "No visits to print");

            foreach (Visit visit in visits)
            {
                VisitSummaryPackage package = new VisitSummaryPackage(visit);

                try
                {
                    package.Print();
                    visit.SyncToolPrintDate = DateTime.Now;
                    Visit.Update(visit);
                    Host.Trace("Tomorrow visits print job", string.Format("Visit {0} printed. ", visit.ID));
                }
                catch (Exception ex)
                {
                    Host.Trace("Tomorrow visits print job",
                        string.Format("Visit {0} print failed. " + ex.Message + ex.StackTrace,
                        visit.ID));
                    return;
                }
            }

            syncToolInfo.LastTomorrowPrintJobDate = DateTime.Now;
            SyncToolInfo.Update(syncToolInfo);
        }

        #endregion
    }
}
