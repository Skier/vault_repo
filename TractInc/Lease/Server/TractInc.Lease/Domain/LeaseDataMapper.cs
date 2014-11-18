using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Weborb.Data.Management;

namespace TractInc.Lease.Domain
{
    public partial class LeaseDataMapper
    {

        private const string SqlSelectByUserId =
            @" Select
                    LeaseId,LCN,DocumentNumber,Volume,PAGE,LeaseeName,AssigneeName,LeassorName,AssignorName,StateFips,CountyFips,UnitDepth,FromDepth,FromFrom,ToDepth,ToFrom,WorkInt,OrrInt,NetAcres,GrossAcres,NriAssign,RcdDate,Term,HBR,Encumbrances,EffDate,PughClause,DepthLimitation,ShutInClau,PoolingClau,MinimumPmt,Author,Status
             From [Lease]
            where @userId in (
                    select userId 
                      from userRole ur
                            inner join permissionAssignment pa on ur.roleId = pa.roleId
                            inner join permission p on p.permissionId = pa.permissionId
                                   and p.code = 'EDIT_ALL_LEASES')
               or Lease.Author = @userId";


        public List<Lease> getByUserId(int userId)
        {
            List<Lease> result = new List<Lease>();

            using (new DatabaseConnectionMonitor(Database))
            {
                using (SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByUserId))
                {
                    sqlCommand.Parameters.AddWithValue("@userId", userId);

                    using (IDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                            result.Add(doLoad(dataReader));
                    }
                }
            }

            return result;
        }
    }
}
        