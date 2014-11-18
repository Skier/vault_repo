using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace QuickBooksAgent.Data
{
    public class Connection
    {
        private const String ConnectionKey = "default";

        private Dictionary<String, IDbConnection> m_connections;

        private IDbTransaction m_transaction;

        private Connection()
        {
            m_connections = new Dictionary<string, IDbConnection>();
        }


        #region Instance

        private const String DataSlotName = "Connection";
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
                return GetDbConnection(ConnectionKey);
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

        public IDbConnection GetDbConnection(String key)
        {
            if (m_connections == null)
            {
                m_connections = new Dictionary<string, IDbConnection>();
            }
            if (!m_connections.ContainsKey(key))
            {
#if !WINCE
				IDbConnection dbConnection = new System.Data.SqlClient.SqlConnection();
#else
                IDbConnection dbConnection = new System.Data.SqlServerCe.SqlCeConnection();
#endif
                if (Database.useMaster)
                {
                    dbConnection.ConnectionString = Configuration.MasterConnectionString;
                }
                else
                {
                    dbConnection.ConnectionString = Configuration.ConnectionString;
                }

                m_connections.Add(key, dbConnection);

            }

            if (m_connections[key].State == ConnectionState.Closed)
            {
                m_connections[key].Open();
            }


            return m_connections[key];
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
                throw new QuickBooksAgentException("Parallel transactions does not supported");
        }

        public void RollbackSystemTransaction()
        {
            if (m_transaction == null)
                return;

            try
            {
                Transaction.Rollback();
            }
            catch (Exception e)
            {
                throw new QuickBooksAgentException(e);
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
            }
            catch (Exception e)
            {
                Transaction.Rollback();

                throw new QuickBooksAgentException(e);
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
