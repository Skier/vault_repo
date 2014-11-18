using System;
using System.Data;
using MobileTech.Data;

namespace MobileTech.Domain
{
    public partial class PeriodTransaction
    {
        #region Constructors

        public PeriodTransaction()
        {

        }

        public PeriodTransaction(BusinessTransaction businessTransaction,PeriodTransactionTypeEnum type )
        {
            m_sessionId = businessTransaction.SessionId;
            m_businessTransactionId = businessTransaction.BusinessTransactionId;
            m_periodTransactionTypeId = (int)type;
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
        #endregion

        #region Change period
        [RefactoringRequired]
        [TransactionRequired]
        public static void ChangePeriod(PeriodTransactionTypeEnum periodTransactionType)
        {
            Session session = Session.FindActive();

            //RouteScheduleQueueStore routeScheduleQueueStore = new RouteScheduleQueueStore();

            BusinessTransaction bizTransaction = BusinessTransaction.Prepare
                (Route.Current, session, BusinessTransactionTypeEnum.Period);
            
            Counter.Assign(bizTransaction);

            BusinessTransaction.Insert(bizTransaction);

            PeriodTransaction periodTransaction = new PeriodTransaction(
               bizTransaction , periodTransactionType);

            switch (periodTransactionType)
            {
                case PeriodTransactionTypeEnum.SOP:

                    Route.ChangeStatus( RouteStatusEnum.SOP_DONE );

                    bizTransaction.PeriodIndex++;

                    break;
                case PeriodTransactionTypeEnum.EOP:

                    Route.ChangeStatus( RouteStatusEnum.EOP_DONE );

                    break;
                default:
                    throw new Exception("Unknown transaction type");
            }

            PeriodTransaction.Insert(periodTransaction);

            bizTransaction.DateCommited = DateTime.Now;
            bizTransaction.Status = BusinessTransactionStatusEnum.Commited;

            

            BusinessTransaction.AssignDocumentNumber(bizTransaction);
        }

        #endregion
    }
}
