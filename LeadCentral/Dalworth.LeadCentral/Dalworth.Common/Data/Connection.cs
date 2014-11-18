using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using Dalworth.Common.SDK;
using MySql.Data.MySqlClient;

namespace Dalworth.Common.Data
{
    public enum ConnectionKeyEnum
    {
        Default
    }

    public class Connection
    {
        private Dictionary<ConnectionKeyEnum, IDbConnection> Connections;

        private Connection()
        {
            Connections = new Dictionary<ConnectionKeyEnum, IDbConnection>();
        }


        #region Instance

        private static Connection CurrentConnection;
        public static Connection Instance
        {
            get { return CurrentConnection ?? (CurrentConnection = new Connection()); }
        }

        #endregion

        #region Connection
        /// <summary>
        /// Initialized and opened database connection
        /// </summary>
        public void CloseConnections()
        {

            if (Connections != null)
            {
                foreach (IDbConnection dbConnection in Connections.Values)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                Connections.Clear();
                Connections = null;
                GC.Collect();
            }
   
        }

        public IDbConnection GetTemporaryDbConnection(ConnectionKeyEnum connectionKey)
        {
            IDbConnection dbConnection = null;

            if (connectionKey == ConnectionKeyEnum.Default)
            {
                dbConnection = new MySqlConnection
                                   {
                                       ConnectionString = Configuration.ConnectionString
                                   };
            }

            return dbConnection;
        }

        public IDbConnection GetTemporaryDbConnection()
        {
            return GetTemporaryDbConnection(ConnectionKeyEnum.Default);
        }

        public IDbConnection GetDbConnection(ConnectionKeyEnum connectionKey)
        {
            if (Connections == null)
            {
                Connections = new Dictionary<ConnectionKeyEnum, IDbConnection>();
            }
            if (!Connections.ContainsKey(connectionKey))
            {
                IDbConnection dbConnection = null;
                    

                if (connectionKey == ConnectionKeyEnum.Default)
                {
                    dbConnection = new MySqlConnection
                                       {
                                           ConnectionString = Configuration.ConnectionString
                                       };
                }
                    
                Connections.Add(connectionKey, dbConnection);
            }

            if (Connections[connectionKey].State == ConnectionState.Closed)
            {
                Connections[connectionKey].Open();
            }


            return Connections[connectionKey];
        }

        public IDbConnection GetDbConnection()
        {
            return GetDbConnection(ConnectionKeyEnum.Default);
        }

        #endregion

        #region Transaction

        public IDbTransaction Transaction { get; private set; }

        public void BeginSystemTransaction(ConnectionKeyEnum connectionKey)
        {
            if (Transaction == null)
            {
                Transaction = GetDbConnection(connectionKey).BeginTransaction();
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
            if (Transaction == null)
            {
                Transaction = GetDbConnection(connectionKey).BeginTransaction(isolationLevel);
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
            if (Transaction == null)
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
                Transaction = null;
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
                Transaction = null;
            }
        }

        #endregion
    }
}
