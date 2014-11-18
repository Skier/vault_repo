using System;
using System.Data;
using Dalworth.Server.Data;
using Dalworth.Server.SDK;

namespace Dalworth.Server.Domain
{
    public partial class Counter
    {
        public Counter(){}

        public const String ConnectionKey = "counter";

        #region Assign

        public static int Assign(ICounterField counterField)
        {
            counterField.CounterValue = Increment(counterField.CounterName);

            return counterField.CounterValue;
        }

        #endregion

        #region Increment

        private const String SqlIncrement = "update counter set val = val+1 where CounterName = ?countername";
        private const String SqlFind = "select val from counter where CounterName = ?countername";

        public static int Increment(String counterName)
        {
            IDbConnection dbConnection = Connection.Instance.GetDbConnection(ConnectionKeyEnum.Default);
            IDbTransaction dbTransaction = dbConnection.BeginTransaction();

            int rv = 0;

            try
            {

                using (IDbCommand dbCommandIncrement = Database.PrepareCommand(SqlIncrement, dbConnection, dbTransaction))
                {
                    Database.PutParameter(dbCommandIncrement, "?countername", counterName);

                    if (dbCommandIncrement.ExecuteNonQuery() < 1)
                    {
                        using (IDbCommand dbCommandInsert = Database.PrepareCommand(SqlInsert, dbConnection, dbTransaction))
                        {

                            Database.PutParameter(dbCommandInsert, "?countername", counterName);
                            Database.PutParameter(dbCommandInsert, "?val", 1);

                            dbCommandInsert.ExecuteNonQuery();
                        }

                        rv = 1;
                    }

                }

                using (IDbCommand dbCommandSearch = Database.PrepareCommand(SqlFind, dbConnection, dbTransaction))
                {
                    Database.PutParameter(dbCommandSearch, "?countername", counterName);

                    rv = (int)dbCommandSearch.ExecuteScalar();
                }

                dbTransaction.Commit();
            }
            catch (Exception e)
            {
                dbTransaction.Rollback();

                throw e;
            }

            if (rv == 0)
                throw new DalworthException("Update counter error");


            return rv;
        }

        #endregion

        #region Update

        public static void Update(String counterName, int value)
        {
            Counter counter = new Counter(counterName, value);

            if (Exists(counter))
                Update(counter);
            else
                Insert(counter);
        }

        #endregion

        #region GetValue

        public static int GetValue(String counterName)
        {
            return FindByPrimaryKey(counterName).Val;
        }

        #endregion

    }
}
      