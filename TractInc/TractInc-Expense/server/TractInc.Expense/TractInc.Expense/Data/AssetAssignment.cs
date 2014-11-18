using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class AssetAssignment
    {

        private static AssetAssignment c_AssetAssignment = new AssetAssignment();

        public static AssetAssignment GetInstance()
        {
            return c_AssetAssignment;
        }

        private AssetAssignment()
        {
        }

        private const string SQL_SELECT_BY_ASSET = @"
            select  distinct
                    aa.[AssetAssignmentId],
                    aa.[AFE],
                    aa.[SubAFE],
                    aa.[AssetId],
                    aa.[Deleted],
                    c.[Active],
                    a.[AFEStatus],
                    sa.[SubAFEStatus],
                    a.[ClientId],
                    c.[ClientName]
            from    [AssetAssignment] aa
                    inner join [AFE] a
                            on a.[AFE] = aa.[AFE]
                    inner join [SubAFE] sa
                            on aa.[SubAFE] = sa.[SubAFE]
                    inner join [Client] c
                            on a.[ClientId] = c.[ClientId]
            where   aa.[AssetId] = @AssetId";

        public List<AssetAssignmentDataObject> GetAssignments(SqlTransaction tran, int assetId)
        {
            List<AssetAssignmentDataObject> result = new List<AssetAssignmentDataObject>();

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@AssetId", assetId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_ASSET, parms))
            {
                while (dataReader.Read())
                {
                    AssetAssignmentDataObject assignmentInfo = new AssetAssignmentDataObject();

                    assignmentInfo.AssetAssignmentId = (int)dataReader.GetValue(0);
                    assignmentInfo.AFE = (string)dataReader.GetValue(1);
                    assignmentInfo.SubAFE = (string)dataReader.GetValue(2);
                    assignmentInfo.AssetId = (int)dataReader.GetValue(3);
                    assignmentInfo.Deleted = (bool)dataReader.GetValue(4);
                    assignmentInfo.IsClientActive = (bool)dataReader.GetValue(5);
                    assignmentInfo.AFEStatus = (string)dataReader.GetValue(6);
                    assignmentInfo.ProjectStatus = (string)dataReader.GetValue(7);
                    assignmentInfo.ClientId = (int)dataReader.GetValue(8);
                    assignmentInfo.ClientName = (string)dataReader.GetValue(9);

                    assignmentInfo.Rates = RateByAssignment.GetInstance().GetRatesByAssignment(tran, assignmentInfo.AssetAssignmentId);

                    result.Add(assignmentInfo);
                }
            }

            return result;
        }

        public List<AssetAssignmentDataObject> GetAssignments(int assetId)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    List<AssetAssignmentDataObject> result = GetAssignments(tran, assetId);

                    tran.Commit();

                    return result;
                }
                catch (SqlException ex)
                {
                    try
                    {
                        tran.Rollback();
                    }
                    catch (Exception)
                    {
                    }

                    throw ex;
                }
            }
        }

        private const string SQL_SELECT_CURRENT = @"
            select  aa.[AssetAssignmentId],
                    aa.[AFE],
                    aa.[SubAFE],
                    aa.[AssetId],
                    aa.[Deleted],
                    a.[AFEStatus],
                    sa.[SubAFEStatus],
                    a.[ClientId],
                    c.[ClientName]
            from    [AssetAssignment] aa
                    inner join [AFE] a
                            on a.[AFE] = aa.[AFE]
                    inner join [SubAFE] sa
                            on aa.[SubAFE] = sa.[SubAFE]
                    inner join [Client] c
                            on a.[ClientId] = c.[ClientId]
            where   aa.[Deleted] = 0";

        public List<AssetAssignmentDataObject> GetCurrentAssignments(SqlTransaction tran)
        {
            List<AssetAssignmentDataObject> result = new List<AssetAssignmentDataObject>();

            DbParameter[] parms = new DbParameter[0] { };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_CURRENT, parms))
            {
                while (dataReader.Read())
                {
                    AssetAssignmentDataObject assignmentInfo = new AssetAssignmentDataObject();

                    assignmentInfo.AssetAssignmentId = (int)dataReader.GetValue(0);
                    assignmentInfo.AFE = (string)dataReader.GetValue(1);
                    assignmentInfo.SubAFE = (string)dataReader.GetValue(2);
                    assignmentInfo.AssetId = (int)dataReader.GetValue(3);
                    assignmentInfo.Deleted = (bool)dataReader.GetValue(4);
                    assignmentInfo.AFEStatus = (string)dataReader.GetValue(5);
                    assignmentInfo.ProjectStatus = (string)dataReader.GetValue(6);
                    assignmentInfo.ClientId = (int)dataReader.GetValue(7);
                    assignmentInfo.ClientName = (string)dataReader.GetValue(8);

                    assignmentInfo.Rates = RateByAssignment.GetInstance().GetRatesByAssignment(tran, assignmentInfo.AssetAssignmentId);

                    result.Add(assignmentInfo);
                }
            }

            return result;
        }

        private const string SQL_SELECT_CREW = @"
            select  aa.[AssetAssignmentId],
                    aa.[AFE],
                    aa.[SubAFE],
                    aa.[AssetId],
                    aa.[Deleted],
                    a.[AFEStatus],
                    sa.[SubAFEStatus],
                    a.[ClientId],
                    c.[ClientName]
            from    [AssetAssignment] aa
                    inner join [Asset] ass
                            on aa.[AssetId] = ass.[AssetId]
                    inner join [AFE] a
                            on a.[AFE] = aa.[AFE]
                    inner join [SubAFE] sa
                            on aa.[SubAFE] = sa.[SubAFE]
                    inner join [Client] c
                            on a.[ClientId] = c.[ClientId]
            where   aa.[Deleted] = 0
            and     ass.[ChiefAssetId] = @ChiefAssetId";

        public List<AssetAssignmentDataObject> GetCrewAssignments(SqlTransaction tran, int chiefAssetId)
        {
            List<AssetAssignmentDataObject> result = new List<AssetAssignmentDataObject>();

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@ChiefAssetId", chiefAssetId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_CREW, parms))
            {
                while (dataReader.Read())
                {
                    AssetAssignmentDataObject assignmentInfo = new AssetAssignmentDataObject();

                    assignmentInfo.AssetAssignmentId = (int)dataReader.GetValue(0);
                    assignmentInfo.AFE = (string)dataReader.GetValue(1);
                    assignmentInfo.SubAFE = (string)dataReader.GetValue(2);
                    assignmentInfo.AssetId = (int)dataReader.GetValue(3);
                    assignmentInfo.Deleted = (bool)dataReader.GetValue(4);
                    assignmentInfo.AFEStatus = (string)dataReader.GetValue(5);
                    assignmentInfo.ProjectStatus = (string)dataReader.GetValue(6);
                    assignmentInfo.ClientId = (int)dataReader.GetValue(7);
                    assignmentInfo.ClientName = (string)dataReader.GetValue(8);

                    result.Add(assignmentInfo);
                }
            }

            return result;
        }

        private const string SQL_CHECK_DELETE_ASSIGNMENT = @"
            select  bi2.[BillItemId]
            from    [BillItem] bi2
            where   bi2.[Status] <> 'CONFIRMED'
            and     bi2.[AssetAssignmentId] = @AssignmentId
            union
            select	bi.[BillItemId]
            from	[BillItem] bi
            where	bi.[AssetAssignmentId] in (
            			select	aa2.[AssetAssignmentId]
            			from	[AssetAssignment] aa2
            			where	aa2.[AssetId] in (
						            select	a2.[AssetId]
            						from	[Asset] a2
            						where	a2.[ChiefAssetId] in (
            									select	a.AssetId
            									from	[Asset] a
            											inner join [AssetAssignment] aa
            													on aa.[AssetId] = a.[AssetId]
            									where	aa.[AssetAssignmentId] = @AssignmentId ) )
            			and		aa2.[SubAFE] in (
            						select	aa3.[SubAFE]
            						from	[AssetAssignment] aa3
            						where	aa3.[AssetAssignmentId] = @AssignmentId ) )
            and		bi.[Status] <> 'CONFIRMED'";

        public bool CanDeleteAssignment(SqlTransaction tran, int assignmentId)
        {
            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@AssignmentId", assignmentId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_CHECK_DELETE_ASSIGNMENT, parms))
            {
                if (dataReader.Read())
                {
                    return false;
                }

                return true;
            }
        }

        private const string SQL_MARK_DELETED = @"
            update  [AssetAssignment]
            set     [Deleted] = 1
            where   [AssetAssignmentId] = @AssignmentId

   			update	[AssetAssignment]
            set     [Deleted] = 1
   			where	[AssetId] in (
                        select	a2.[AssetId]
            			from	[Asset] a2
            			where	a2.[ChiefAssetId] in (
            						select	a.AssetId
            						from	[Asset] a
            								inner join [AssetAssignment] aa
            										on aa.[AssetId] = a.[AssetId]
            						where	aa.[AssetAssignmentId] = @AssignmentId ) )
   			and		[SubAFE] in (
   						select	aa3.[SubAFE]
   						from	[AssetAssignment] aa3
   						where	aa3.[AssetAssignmentId] = @AssignmentId )";

        public void MarkDeleted(SqlTransaction tran, int assignmentId)
        {
            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@AssignmentId", assignmentId) };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_MARK_DELETED, parms);
        }

        private const string SQL_INSERT = @"
            insert  into [AssetAssignment] (
                    [AFE],
                    [SubAFE],
                    [AssetId],
                    [Deleted])
            values( @AFE,
                    @SubAFE,
                    @AssetId,
                    0);
            select  cast(scope_identity() as int)";

        public void Insert(SqlTransaction tran, AssetAssignmentDataObject assignmentInfo)
        {
            DbParameter[] parms = new DbParameter[3] {
                new SqlParameter("@AFE", assignmentInfo.AFE),
                new SqlParameter("@SubAFE", assignmentInfo.SubAFE),
                new SqlParameter("@AssetId", assignmentInfo.AssetId)
            };

            assignmentInfo.AssetAssignmentId = (int)SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);
        }

        private const string SQL_SELECT_FOR_INVOICE = @"
            select  distinct
                    aa.[AssetAssignmentId],
                    aa.[AFE],
                    aa.[SubAFE],
                    aa.[AssetId],
                    aa.[Deleted]
            from    [AssetAssignment] aa
                    inner join [InvoiceItem] ii
                            on ii.[InvoiceId] = @InvoiceId
                    inner join [BillItem] bi
                            on bi.[BillItemId] = ii.[BillItemId]";

        public List<AssetAssignmentDataObject> GetAssignmentsForInvoice(SqlTransaction tran, int invoiceId)
        {
            List<AssetAssignmentDataObject> result = new List<AssetAssignmentDataObject>();

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@InvoiceId", invoiceId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_FOR_INVOICE, parms))
            {
                while (dataReader.Read())
                {
                    AssetAssignmentDataObject assignmentInfo = new AssetAssignmentDataObject();

                    assignmentInfo.AssetAssignmentId = (int)dataReader.GetValue(0);
                    assignmentInfo.AFE = (string)dataReader.GetValue(1);
                    assignmentInfo.SubAFE = (string)dataReader.GetValue(2);
                    assignmentInfo.AssetId = (int)dataReader.GetValue(3);
                    assignmentInfo.Deleted = (bool)dataReader.GetValue(4);

                    result.Add(assignmentInfo);
                }
            }

            return result;
        }

    }

}
