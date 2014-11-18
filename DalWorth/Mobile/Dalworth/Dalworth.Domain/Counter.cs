using System;
using System.Data;
using Dalworth.Data;
using Dalworth.SDK;

namespace Dalworth.Domain
{

    public partial class Counter
    {
        #region Costructors

        public Counter()
        {

        }

        #endregion

        #region Assign

        public static int Assign(ICounterField counterField, IDbTransaction transaction)
        {
            counterField.CounterValue = Increment(counterField.CounterName, transaction);

            return counterField.CounterValue;
        }

        public static int Assign(ICounterField counterField)
        {
            return Assign(counterField, null);
        }

        #endregion

        #region Increment

        private const String SqlIncrement = "update counter set val = val+1 where CounterName = @countername";
        private const String SqlFind = "select val from counter where CounterName = @countername";

        private static int Increment(String counterName, IDbTransaction transaction)
        {
            int rv = 0;

            using (IDbCommand dbCommandIncrement = Database.PrepareCommand(SqlIncrement, transaction))
            {
                Database.PutParameter(dbCommandIncrement, "@countername", counterName);

                if (dbCommandIncrement.ExecuteNonQuery() < 1)
                {
                    using (IDbCommand dbCommandInsert = Database.PrepareCommand(SqlInsert, transaction))
                    {

                        Database.PutParameter(dbCommandInsert, "@countername", counterName);
                        Database.PutParameter(dbCommandInsert, "@val", 1);
                        dbCommandInsert.ExecuteNonQuery();
                    }
                    rv = 1;
                }

            }

            using (IDbCommand dbCommandSearch = Database.PrepareCommand(SqlFind, transaction))
            {
                Database.PutParameter(dbCommandSearch, "@countername", counterName);
                rv = (int)dbCommandSearch.ExecuteScalar();
            }
                
            if (rv == 0)
                throw new DalworthException("Update counter error");

            return rv;
        }

        #endregion

        #region Private methods

        private static void UpdateIfGreater(String counterName, int value, IDbTransaction transaction)
        {
            Counter counter = new Counter(counterName, value);

            if (Exists(counter, transaction))
            {
                if (value > GetValue(counterName, transaction))
                    Update(counterName, value, transaction);
            }                
            else
            {
                Update(counterName, value, transaction);
            }
        }

        private static void Update(String counterName, int value, IDbTransaction transaction)
        {
            Counter counter = new Counter(counterName, value);

            if (Exists(counter, transaction))
                Update(counter, transaction);
            else
                Insert(counter, transaction);
        }

        private static int GetValue(String counterName, IDbTransaction transaction)
        {
            return FindByPrimaryKey(counterName, transaction).Val;
        }

        #endregion

        public static void UpdateIfGreater(ICounterField iCounterObject, IDbTransaction transaction)
        {
            UpdateIfGreater(iCounterObject.CounterName, iCounterObject.CounterValue, transaction);
        }

        public static void UpdateIfGreater(ICounterField iCounterObject)
        {
            UpdateIfGreater(iCounterObject, null);
        }

    }
}
