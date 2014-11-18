using System;
using System.Data;
using MobileTech.Data;
using System.Collections.Generic;


namespace MobileTech.Domain
{
    public partial class InventoryTransaction
    {
        #region Constructors

        public InventoryTransaction()
        {

        }

        public InventoryTransaction(BusinessTransaction businessTransaction, InventoryTransactionTypeEnum type)
        {
            BusinessTransaction = businessTransaction;
            Type = type;
        }

        #endregion

        #region Extra fields
        BusinessTransaction m_businessTransaction;
        public BusinessTransaction BusinessTransaction
        {
            get
            {
                if (m_businessTransaction == null)
                {
                    if (m_businessTransactionId == 0
                        || m_sessionId == 0)
                    {
                        throw new MobileTechException("BusinessTransactionId or Session field not initialized");
                    }

                    m_businessTransaction
                        = BusinessTransaction.FindByPrimaryKey(
                        m_sessionId,
                        m_businessTransactionId);
                }

                return m_businessTransaction;
            }
            set
            {
                m_businessTransaction = value;

                if (value != null)
                {
                    m_businessTransactionId = value.BusinessTransactionId;
                    m_sessionId = value.SessionId;
                }
            }
        }

        public InventoryTransactionTypeEnum Type
        {
            get
            {
                return (InventoryTransactionTypeEnum)m_inventoryTransactionTypeId;
            }
            set
            {
                m_inventoryTransactionTypeId = (int)value;
            }
        }

        #endregion

        #region Finders
        public static int? FindAll(DataTable dtInventoryTransaction)
        {
            IDbCommand command = Database.PrepareCommand(SqlSelectAll);

            IDataReader dataReader = command.ExecuteReader(CommandBehavior.SingleRow);
            dtInventoryTransaction.Load(dataReader);
            return dtInventoryTransaction.Rows.Count;
        }




        #region SqlFindUncommited

        public static List<InventoryTransaction> FindUncommited(InventoryTransactionTypeEnum type)
        {
            return FindUncommited(Route.Current, type);
        }

        const String SqlFindUncommited = "Select InventoryTransaction.SessionId, InventoryTransaction.BusinessTransactionId, " +
            "InventoryTransactionTypeId from InventoryTransaction "+
            "Inner Join BusinessTransaction "+
            "on BusinessTransaction.BusinessTransactionId = InventoryTransaction.BusinessTransactionId "+
            "and BusinessTransactionTypeId = @BusinessTransactionTypeId "+
            "and BusinessTransactionStatusId = @BusinessTransactionStatusId "+
            "and InventoryTransactionTypeId = @InventoryTransactionTypeId "+
            "and RouteNumber=@RouteNumber and LocationId = @LocationId";
        public static List<InventoryTransaction> FindUncommited(Route route, InventoryTransactionTypeEnum type)
        {
            IDbCommand dbCommand = Database.PrepareCommand(SqlFindUncommited);

            Database.PutParameter(dbCommand,"@BusinessTransactionTypeId", 
                (int)BusinessTransactionTypeEnum.Inventory);

            Database.PutParameter(dbCommand, "@BusinessTransactionStatusId", 
                (int)BusinessTransactionStatusEnum.Created);

            Database.PutParameter(dbCommand, "@RouteNumber", 
                route.RouteNumber);

            Database.PutParameter(dbCommand, "@LocationId",
                route.LocationId);

            Database.PutParameter(dbCommand, "@InventoryTransactionTypeId",
                (int)type);

            List<InventoryTransaction> rv = new List<InventoryTransaction>();

            using (IDataReader dataReader = dbCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    rv.Add(Load(dataReader));
                }
            }

            return rv;
        }

        #endregion

        #endregion

        #region Service
        public static InventoryTransaction Prepare(Route route,
            Session session,
            InventoryTransactionTypeEnum type)
        {
            InventoryTransaction transaction =
                            new InventoryTransaction(
                            BusinessTransaction.Prepare(
                            route,
                            session,
                            BusinessTransactionTypeEnum.Inventory),type);


            return transaction;
        }

        public static InventoryTransaction Prepare(InventoryTransactionTypeEnum type)
        {
            return InventoryTransaction.Prepare(
                            Route.Current,
                            Session.ActiveSession, 
                            type);
        }
        #endregion
    }
}
