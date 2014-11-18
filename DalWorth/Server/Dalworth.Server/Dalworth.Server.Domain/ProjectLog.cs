using System;
using System.Data;
using Dalworth.Server.SDK;
  
namespace Dalworth.Server.Domain
{
    public partial class ProjectLog
    {
        public ProjectLog()
        {

        }

        public static void Insert (Project project, IDbConnection connection)
        {
            try
            {
                ProjectLog log = new ProjectLog(-1, Configuration.CurrentDispatchId, DateTime.Now, 
                    new System.Diagnostics.StackTrace(true).ToString(),project.ID, project.ParentProdjectId, project.ProjectTypeId, project.CustomerId,
                    project.ServiceAddressId,project.ProjectStatusId, project.LeadId, project.Description, project.InsuranceCompany, project.InsuranceAgency,
                    project.InsuranceAgencyPhone, project.InsuranceAgent, project.InsuranceAdjustor, project.InsuranceAdjustorPhone, project.ClaimNumber, project.PolicyNumber, 
                    project.DeductibleAmount, project.AdvertisingSourceId, project.AdvertisingTechnicianId, project.DumpedProjectId, project.DumpWorkTransactionId, 
                    project.ClosedAmount, project.PaidAmount, project.CreateDate, project.QbCustomerTypeListId, project.QbSalesRepListId);
                Insert(log, connection);
            }
            catch (Exception)
            {
            }
        }
    }
}
      