using System;
using System.Data;
using Dalworth.Server.SDK;
  
namespace Dalworth.Server.Domain
{
    public partial class WorkDetailLog
    {
        public WorkDetailLog()
        {

        }

        public static void Insert(WorkDetail workDetail, IDbConnection connection)
        {
            try
            {
                WorkDetailLog log = new WorkDetailLog(-1, Configuration.CurrentDispatchId, DateTime.Now,
                                                  new System.Diagnostics.StackTrace(true).ToString(),
                                                  workDetail.ID, workDetail.WorkId, workDetail.VisitId,
                                                  workDetail.TimeBegin, workDetail.TimeEnd, workDetail.Sequence,
                                                  workDetail.WorkDetailStatusId,
                                                  workDetail.TimeDispatch, workDetail.TimeArrive,
                                                  workDetail.TimeComplete, workDetail.TimeBeginAssigned,
                                                  workDetail.TimeEndAssigned);
                Insert(log, connection);
            }
            catch (Exception)
            {
            }
            
        }
    }
}
      