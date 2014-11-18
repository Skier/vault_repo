using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Text;
using System.Data;
using System.Threading;
using SmartSchedule.SDK;
using MySql.Data.MySqlClient;

namespace SmartSchedule.Data
{
    public enum ConnectionKeyEnum
    {
        Default,
        Servman
    }

    public class Connection
    {
        private IDbConnection m_connection;
        private IDbTransaction m_transaction;
        private ConnectionKeyEnum m_connectionKey;

        private Connection(ConnectionKeyEnum connectionKey)
        {
            m_connectionKey = connectionKey;
        }

        #region Instance

        private static Dictionary<ConnectionKeyEnum, Connection> s_connections;
        public static Connection GetInstance(ConnectionKeyEnum connectionKey)
        {
            if (s_connections == null)
                s_connections = new Dictionary<ConnectionKeyEnum, Connection>();

            if (!s_connections.ContainsKey(connectionKey))
                s_connections.Add(connectionKey, new Connection(connectionKey));
            
            return s_connections[connectionKey];
        }

        public static Connection DefaultInstance
        {
            get
            {
                return GetInstance(ConnectionKeyEnum.Default);
            }
        }

        public static IEnumerable<Connection> Instances
        {
            get
            {
                return new List<Connection>(s_connections.Values);
            }
        }

        public static void DeleteInstances()
        {
            s_connections.Clear();
            s_connections = null;
            GC.Collect();
        }

        public static void DeleteInstance(ConnectionKeyEnum connectionKey)
        {
            if (s_connections.ContainsKey(connectionKey))
            {
                s_connections[connectionKey].Close();
                s_connections.Remove(connectionKey);
            }
        }


        #endregion

        #region Connection

        public void Close()
        {            
            m_connection.Close();
            m_connection.Dispose();
        }

        public static IDbConnection GetTemporaryDbConnection(ConnectionKeyEnum connectionKey)
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
            return GetTemporaryDbConnection(m_connectionKey);
        }

        public IDbConnection GetDbConnection()
        {
            if (m_connection == null)
                m_connection = GetTemporaryDbConnection();

            if (m_connection.State == ConnectionState.Closed)
            {
                try
                {
                    m_connection.Open();
                }
                catch (Exception e)
                {
                    Host.Trace("GetDbConnection", "Exception " + e);
                    throw;
                }
            }                

            return m_connection;
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
                m_transaction = GetDbConnection().BeginTransaction();
            else
                throw new DalworthException("Unable to start transaction. Transaction is already exist");
        }

        public void BeginSystemTransaction(IsolationLevel isolationLevel)
        {   
            if (m_transaction == null)
                m_transaction = GetDbConnection().BeginTransaction(isolationLevel);
            else
                throw new DalworthException("Unable to start transaction. Transaction is already exist");
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
