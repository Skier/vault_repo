using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
using Dalworth.Server.Data;
using Dalworth.Server.SDK;

namespace Dalworth.Server.Domain
{
    public partial class WorkTransaction 
    {
        public WorkTransaction(){}

        #region WorkTransactionType

        [XmlIgnore]
        public WorkTransactionTypeEnum WorkTransactionType
        {
            get { return (WorkTransactionTypeEnum) m_workTransactionTypeId; }
            set { m_workTransactionTypeId = (int) value; }
        }

        #endregion

        #region FindNotSentTransactions

        private const string SqlFindNotSentTransactions =
            @"select * from WorkTransaction wt
                inner join Visit v on wt.VisitId = v.ID                
                where IsSentToServman = 0
                    and wt.VisitId is not null
                order by TransactionDate;";

        public static List<WorkTransaction> FindNotSentTransactions()
        {
            List<WorkTransaction> transactions = new List<WorkTransaction>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindNotSentTransactions))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        transactions.Add(Load(dataReader));
                    }
                }
            }
            return transactions;
        }

        #endregion

        #region SaveGpsInfo

        public static void SaveGpsInfo(int workId, float latitude, float longitude, DateTime gpsTime, IDbConnection connection)
        {
            Work work = Work.FindByPrimaryKey(workId, connection);

            WorkTransaction workTransaction = new WorkTransaction(0, workId, work.TechnicianEmployeeId,
                null, 0, DateTime.Now, decimal.Zero, false);
            workTransaction.WorkTransactionType = WorkTransactionTypeEnum.GPS;
            Insert(workTransaction, connection);

            WorkTransactionGps gps = new WorkTransactionGps(workTransaction.ID, latitude, longitude, gpsTime);
            WorkTransactionGps.Insert(gps, connection);
        }

        #endregion

        #region FindLatestGpsTransaction

        private const string SqlFindLatestGpsTransaction =
            @"SELECT * FROM WorkTransaction
                where WorkId = ?WorkId and WorkTransactionTypeId = 9
                order by ID desc
                limit 1";

        public static WorkTransaction FindLatestGpsTransaction(int workId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindLatestGpsTransaction))
            {
                Database.PutParameter(dbCommand, "?WorkId", workId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        return Load(dataReader);
                    }
                }
            }
            throw new DataNotFoundException("Gps transaction not found");
        }

        #endregion

        #region IsExistTodaysTransactionOrVisitCompleted

        private const string SqlIsExistTodaysTransaction =
            @"select * from WorkTransaction
                WHERE WorkId = ?WorkId
                and VisitId = ?VisitId            
                and (WorkTransactionTypeId = ?WorkTransactionTypeId
                or WorkTransactionTypeId = 2
                or WorkTransactionTypeId = 3
                or WorkTransactionTypeId = 8)";


        public static bool IsExistTodaysTransactionOrVisitCompleted(int visitId, int workId,
            WorkTransactionTypeEnum workTransactionType, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlIsExistTodaysTransaction))
            {
                Database.PutParameter(dbCommand, "?WorkId", workId);
                Database.PutParameter(dbCommand, "?VisitId", visitId);
                Database.PutParameter(dbCommand, "?WorkTransactionTypeId", (int)workTransactionType);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return true;
                }
            }

            return false;
        }

        #endregion

        #region FindBy Visit and Type

        private const string SqlFindByVisitAndTransactionType =
            @"select * from WorkTransaction
                WHERE VisitId = ?VisitId            
                and WorkTransactionTypeId = ?WorkTransactionTypeId";


        public static WorkTransaction FindBy(int visitId, WorkTransactionTypeEnum workTransactionType)
        {

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByVisitAndTransactionType))
            {
                Database.PutParameter(dbCommand, "?VisitId", visitId);
                Database.PutParameter(dbCommand, "?WorkTransactionTypeId", (int)workTransactionType);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            throw new DataNotFoundException("Work Transaction not found");
        }

        #endregion

        #region FindBy Visit

        private const string SqlFindByVisit =
            @"select * from WorkTransaction
                WHERE VisitId = ?VisitId";

        public static List<WorkTransaction> FindBy(Visit visit)
        {
            List<WorkTransaction> result = new List<WorkTransaction>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByVisit))
            {
                Database.PutParameter(dbCommand, "?VisitId", visit.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion

        #region FindBy Work and Type

        private const string SqlFindByWorkAndTransactionType =
            @"select * from WorkTransaction
                WHERE WorkId = ?WorkId            
                and WorkTransactionTypeId = ?WorkTransactionTypeId";

        public static WorkTransaction FindBy(Work work, WorkTransactionTypeEnum workTransactionType)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByWorkAndTransactionType))
            {
                Database.PutParameter(dbCommand, "?WorkId", work.ID);
                Database.PutParameter(dbCommand, "?WorkTransactionTypeId", (int)workTransactionType);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            throw new DataNotFoundException("Work Transaction not found");
        }

        #endregion
    }
}
      