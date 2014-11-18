using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Text;
using System.Data;
using System.Threading;
using Dalworth.Server.SDK;
using MySql.Data.MySqlClient;

namespace Dalworth.Server.Data
{
    public enum ConnectionKeyEnum
    {
        Default,
        Servman
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
//        public IDbConnection DbConnection
//        {
//            get
//            {
//                return GetDbConnection(ConnectionKey);
//            }
//        }

        public void Close( ConnectionKeyEnum connectionKey)
        {
            if (!m_connections.ContainsKey(connectionKey))
                return;

            IDbConnection dbConnection  = m_connections[connectionKey];

            if (dbConnection.State != ConnectionState.Closed)
                dbConnection.Close();

            dbConnection.Dispose();

            m_connections.Remove(connectionKey);

            GC.Collect();
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

        public IDbConnection GetTemporaryDbConnection(ConnectionKeyEnum connectionKey)
        {
            IDbConnection dbConnection = null;

            if (connectionKey == ConnectionKeyEnum.Default)
            {
                dbConnection = new MySqlConnection();
                dbConnection.ConnectionString = Configuration.ConnectionString;
            }

            else if (connectionKey == ConnectionKeyEnum.Servman)
            {
                dbConnection = new OdbcConnection();
                dbConnection.ConnectionString = Configuration.ServmanConnectionString;
            }

            return dbConnection;
        }

        public IDbConnection GetTemporaryDbConnection()
        {
            return GetTemporaryDbConnection(ConnectionKeyEnum.Default);
        }

        public IDbConnection GetDbConnection(ConnectionKeyEnum connectionKey)
        {
            if (m_connections == null)
            {
                m_connections = new Dictionary<ConnectionKeyEnum, IDbConnection>();
            }
            if (!m_connections.ContainsKey(connectionKey))
            {
                IDbConnection dbConnection = null;
                    

                if (connectionKey == ConnectionKeyEnum.Default)
                {
                    dbConnection = new MySqlConnection();
                    dbConnection.ConnectionString = Configuration.ConnectionString;
                }
                    
                else if (connectionKey == ConnectionKeyEnum.Servman)
                {
                    dbConnection = new OdbcConnection();
                    dbConnection.ConnectionString = Configuration.ServmanConnectionString;
                }                    

                m_connections.Add(connectionKey, dbConnection);

            }

            if (m_connections[connectionKey].State == ConnectionState.Closed)
            {
                m_connections[connectionKey].Open();
            }


            return m_connections[connectionKey];
        }

        public IDbConnection GetDbConnection()
        {
            return GetDbConnection(ConnectionKeyEnum.Default);
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

        public void BeginSystemTransaction(ConnectionKeyEnum connectionKey)
        {
            if (m_transaction == null)
            {
                m_transaction = GetDbConnection(connectionKey).BeginTransaction();
            }
            else
            {
                throw new DalworthException("Unable to start transaction. Transaction is already exist");
            }                
        }

        public void BeginSystemTransaction()
        {
            BeginSystemTransaction(ConnectionKeyEnum.Default);
        }

        public void BeginSystemTransaction(IsolationLevel isolationLevel, ConnectionKeyEnum connectionKey)
        {            
            if (m_transaction == null)
            {
                m_transaction = GetDbConnection(connectionKey).BeginTransaction(isolationLevel);
            }
            else
            {
                throw new DalworthException("Unable to start transaction. Transaction is already exist");
            }                
        }

        public void BeginSystemTransaction(IsolationLevel isolationLevel)
        {
            BeginSystemTransaction(isolationLevel, ConnectionKeyEnum.Default);
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
