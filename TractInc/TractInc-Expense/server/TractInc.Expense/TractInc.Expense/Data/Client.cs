using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class Client
    {

        private static Client c_Client = new Client();

        public static Client GetInstance()
        {
            return c_Client;
        }

        private Client()
        {
        }

        private const string SQL_SELECT_BY_ASSET = @"
            select  distinct
                    c.[ClientId],
                    c.[ClientName],
                    c.[ClientAddress],
                    c.[Active],
                    c.[Deleted]
            from    [Client] c
                    inner join [AFE] a
                            on c.[ClientId] = a.[ClientId]
                    inner join [AssetAssignment] aa
                            on a.[AFE] = aa.[AFE]
            where   aa.[AssetId] = @AssetId
              and   c.Deleted = 0";

        public List<ClientDataObject> GetAssetClients(SqlTransaction tran, int assetId)
        {
            List<ClientDataObject> result = new List<ClientDataObject>();

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@AssetId", assetId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_ASSET, parms))
            {
                while (dataReader.Read())
                {
                    ClientDataObject clientInfo = new ClientDataObject();

                    clientInfo.ClientId = (int)dataReader.GetValue(0);
                    clientInfo.ClientName = (string)dataReader.GetValue(1);
                    clientInfo.ClientAddress = (string)dataReader.GetValue(2);
                    clientInfo.Active = (bool)dataReader.GetValue(3);
                    clientInfo.Deleted = (bool)dataReader.GetValue(4);

                    result.Add(clientInfo);
                }
            }

            return result;
        }

        private const string SQL_SELECT_CURRENT = @"
            select  [ClientId],
                    [ClientName],
                    [ClientAddress],
                    [Active],
                    [Deleted]
            from    [Client]
            where   Deleted = 0";

        public List<ClientDataObject> GetCurrentClients(SqlTransaction tran)
        {
            List<ClientDataObject> result = new List<ClientDataObject>();

            DbParameter[] parms = new DbParameter[0] { };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_CURRENT, parms))
            {
                while (dataReader.Read())
                {
                    ClientDataObject clientInfo = new ClientDataObject();

                    clientInfo.ClientId = (int)dataReader.GetValue(0);
                    clientInfo.ClientName = (string)dataReader.GetValue(1);
                    clientInfo.ClientAddress = (string)dataReader.GetValue(2);
                    clientInfo.Active = (bool)dataReader.GetValue(3);
                    clientInfo.Deleted = (bool)dataReader.GetValue(4);

                    clientInfo.DefaultRates = DefaultInvoiceRate.GetInstance().GetClientRates(tran, clientInfo.ClientId);

                    result.Add(clientInfo);
                }
            }

            return result;
        }

        private const string SQL_SELECT_BY_ID = @"
            select  [ClientId],
                    [ClientName],
                    [ClientAddress],
                    [Active],
                    [Deleted]
            from    [Client]
            where   [ClientId] = @ClientId";

        public ClientDataObject GetClient(SqlTransaction tran, int clientId)
        {
            ClientDataObject clientInfo = null;

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@ClientId", clientId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_ID, parms))
            {
                if (dataReader.Read())
                {
                    clientInfo = new ClientDataObject();

                    clientInfo.ClientId = (int)dataReader.GetValue(0);
                    clientInfo.ClientName = (string)dataReader.GetValue(1);
                    clientInfo.ClientAddress = (string)dataReader.GetValue(2);
                    clientInfo.Active = (bool)dataReader.GetValue(3);
                    clientInfo.Deleted = (bool)dataReader.GetValue(4);
                }
            }

            return clientInfo;
        }

        private const string SQL_INSERT = @"
        insert  into [Client]
              ( [ClientName],
                [ClientAddress],
                [Active],
                [Deleted] )
        values( @ClientName,
                @ClientAddress,
                @Active,
                @Deleted);
        select  cast(scope_identity() as int)";

        public void Insert(SqlTransaction tran, ClientDataObject clientInfo)
        {
            DbParameter[] parms = new DbParameter[4] {
                new SqlParameter("@ClientName",     clientInfo.ClientName),
                new SqlParameter("@ClientAddress",  clientInfo.ClientAddress),
                new SqlParameter("@Active",         clientInfo.Active),
                new SqlParameter("@Deleted",        clientInfo.Deleted)
            };

            clientInfo.ClientId = (int)SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);
        }

        private const string SQL_UPDATE = @"
        update  [Client]
        set     [ClientName] = @ClientName,
                [ClientAddress] = @ClientAddress,
                [Active] = @Active,
                [Deleted] = @Deleted
        where   [ClientId] = @ClientId";

        public void Update(SqlTransaction tran, ClientDataObject clientInfo)
        {
            DbParameter[] parms = new DbParameter[5] {
                new SqlParameter("@ClientName",     clientInfo.ClientName),
                new SqlParameter("@ClientAddress",  clientInfo.ClientAddress),
                new SqlParameter("@Active",         clientInfo.Active),
                new SqlParameter("@Deleted",        clientInfo.Deleted),
                new SqlParameter("@ClientId",       clientInfo.ClientId)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, parms);
        }

        private const string SQL_REMOVE = @"
        update  [Client]
        set     [Deleted] = 1
        where   [ClientId] = @ClientId";

        public void Remove(SqlTransaction tran, int clientId)
        {
            DbParameter[] parms = new DbParameter[1] {
                new SqlParameter("@ClientId", clientId)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_REMOVE, parms);
        }

        private const string SQL_CAN_REMOVE_CLIENT = @"
        select  *
		from    [BillItem] bi
				inner join [AssetAssignment] aa
				        on bi.[AssetAssignmentId] = aa.[AssetAssignmentId]
				inner join [Afe] a
						on aa.[AFE] = a.[AFE]
		where   bi.[Status] <> 'CONFIRMED'
		and     a.[ClientId] = @ClientId";

        public bool CanRemoveClient(SqlTransaction tran, int clientId)
        {
            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@ClientId", clientId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_CAN_REMOVE_CLIENT, parms))
            {
                if (dataReader.Read())
                {
                    return false;
                }
            }

            return true;
		}

    }

}
