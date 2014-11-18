using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace TractInc.Server.Data
{
    public enum DbConnectionEnum
    {
        TractIncDb,
        WaltDb,
        MapOptixDb
    }
    
    public class Connection
    {
//        private const String ConnectionKey = "default";

        private Dictionary<DbConnectionEnum, IDbConnection> m_connections;

        private IDbTransaction m_transaction;

        private Connection()
        {
            m_connections = new Dictionary<DbConnectionEnum, IDbConnection>();
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

        public IDbConnection GetDbConnection(DbConnectionEnum connectionKey)
        {
            if (m_connections == null)
            {
                m_connections = new Dictionary<DbConnectionEnum, IDbConnection>();
            }
            if (!m_connections.ContainsKey(connectionKey))
            {
                IDbConnection dbConnection = new System.Data.SqlClient.SqlConnection();

                if (connectionKey == DbConnectionEnum.WaltDb)
                {
                    dbConnection.ConnectionString = SQLHelper.GetConnectionString(SQLHelper.WALT_CONNECTION_STRING_KEY);
                }
                else if (connectionKey == DbConnectionEnum.MapOptixDb)
                {
                    dbConnection.ConnectionString = SQLHelper.GetConnectionString(SQLHelper.MAPOPTIX_CONNECTION_STRING_KEY);
                }
                else 
                {
                    dbConnection.ConnectionString = SQLHelper.GetConnectionString(SQLHelper.TRACTINC_CONNECTION_STRING_KEY);
                }

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

        public void BeginSystemTransaction(DbConnectionEnum connectionKey)
        {
            if (m_transaction == null)
            {
                m_transaction = GetDbConnection(connectionKey).BeginTransaction();
            }
            else
            {
                while (m_transaction != null)
                {
                    Thread.Sleep(100);
                }
                BeginSystemTransaction(connectionKey);                                                
            }
                
        }
        
        public void BeginSystemTransaction()
        {
            BeginSystemTransaction(DbConnectionEnum.TractIncDb);
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
                throw new DataException("Transaction rollback error.", e);
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

                throw new DataException("Transaction commit error.", e);
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
