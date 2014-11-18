using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;
using Dalworth.SDK;

namespace Dalworth.Data
{
    public enum ConnectionKeyEnum
    {
        Default,
        Temporary1,
        Temporary2,
        Counter
    }

    public class Connection
    {
        private Dictionary<ConnectionKeyEnum, IDbConnection> m_connections;

        private IDbTransaction m_transaction;

        private Connection()
        {
            m_connections = new Dictionary<ConnectionKeyEnum, IDbConnection>();
        }


        #region Instance

        private static Connection s_connection;
        public static Connection Instance
        {
            get
            {
                if (s_connection == null)
                {
                    s_connection = new Connection();
                }

                return s_connection;
            }
        }

        #endregion

        #region Connection
        /// <summary>
        /// Initialized and opened database connection
        /// </summary>
        public IDbConnection DbConnection
        {
            get
            {
                return GetDbConnection(ConnectionKeyEnum.Default);
            }
        }

        public void CloseConnections()
        {

            if (m_connections != null)
            {
                foreach (IDbConnection dbConnection in m_connections.Values)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                m_connections.Clear();
                m_connections = null;
                GC.Collect();
            }
   
        }

        public IDbConnection GetDbConnection(ConnectionKeyEnum connectionKey)
        {
            if (m_connections == null)
            {
                m_connections = new Dictionary<ConnectionKeyEnum, IDbConnection>();
            }
            if (!m_connections.ContainsKey(connectionKey))
            {
                IDbConnection dbConnection = new System.Data.SqlServerCe.SqlCeConnection();
                dbConnection.ConnectionString = Configuration.ConnectionString;

                m_connections.Add(connectionKey, dbConnection);
            }

            if (m_connections[connectionKey].State == ConnectionState.Closed)
            {
                m_connections[connectionKey].Open();
            }

            return m_connections[connectionKey];
        }

        #endregion

        #region Transaction

        public IDbTransaction Transaction
        {
            get
            {
                return m_transaction;
            }
        }

        public void BeginSystemTransaction()
        {
            if (m_transaction == null)
            {
                m_transaction = DbConnection.BeginTransaction();                
            }
            else
            {
                throw new DalworthException("Transaction is alreadfy started");
            }
                
        }

        public void RollbackSystemTransaction()
        {
            if (m_transaction == null)
                return;

            try
            {
                Transaction.Rollback();
                Host.Trace("Transactions", "Transaction Rolled back");
            }
            catch (Exception e)
            {
                throw new DalworthException(e);
            }
            finally
            {
                Transaction.Dispose();
                m_transaction = null;
            }
        }

        public void CommitSystemTransaction()
        {
            try
            {
                Transaction.Commit();
                Host.Trace("Transactions", "Transaction Committed");
            }
            catch (Exception e)
            {
                Transaction.Rollback();

                throw new DalworthException(e);
            }
            finally
            {
                Transaction.Dispose();
                m_transaction = null;
            }
        }

        #endregion
    }
}
