using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;
using System.Data;
using System.IO;
using System.Xml;
using SmartSchedule.SDK;

namespace SmartSchedule.Data
{
    public enum ForeignKeyAction { Create, Drop };

	public static class Database
	{
        private static Stack<ConnectionKeyEnum> m_beginTransactionStack;

	    static Database()
	    {
            m_beginTransactionStack = new Stack<ConnectionKeyEnum>();
	    }

        public static bool IsUnderTransaction(ConnectionKeyEnum connectionKey)
        {
            return Connection.GetInstance(connectionKey).Transaction != null;
        }

        public static bool UnderTransaction
        {
            get
            {
                return IsUnderTransaction(ConnectionKeyEnum.Default);
            }
        }
        
        public static void Close()
        {
            foreach (Connection instance in Connection.Instances)
                instance.Close();

            Connection.DeleteInstances();
        }
	    
		public static void Begin()
		{
            Begin(ConnectionKeyEnum.Default);
		}

        public static void Begin(IsolationLevel isolationLevel)
        {
            Begin(isolationLevel, ConnectionKeyEnum.Default);
        }

		public static void Begin(ConnectionKeyEnum connectionKey)
		{
            Connection.GetInstance(connectionKey).BeginSystemTransaction();
            m_beginTransactionStack.Push(connectionKey);
		}

        public static void Begin(IsolationLevel isolationLevel, ConnectionKeyEnum connectionKey)
        {
            Connection.GetInstance(connectionKey).BeginSystemTransaction(isolationLevel);
            m_beginTransactionStack.Push(connectionKey);
        }

		public static void Commit()
		{
            Connection.GetInstance(m_beginTransactionStack.Pop()).CommitSystemTransaction();
		}

		public static void Rollback()
		{
            if (m_beginTransactionStack.Count > 0)
                Connection.GetInstance(m_beginTransactionStack.Pop()).RollbackSystemTransaction();
		}

		public static IDbCommand PrepareCommand(String sql, IDbConnection dbConnection)
		{
            if (dbConnection == null)
                return PrepareCommand(sql, Connection.DefaultInstance.GetDbConnection(), null);
            else
			    return PrepareCommand(sql,dbConnection,null);
		}

		public static IDbCommand PrepareCommand(String sql,IDbConnection dbConnection, IDbTransaction dbTransaction)
		{
			IDbCommand dbCommand = dbConnection.CreateCommand();

			dbCommand.CommandText = sql;

			dbCommand.Transaction = dbTransaction;
            
			dbCommand.Prepare();

			return dbCommand;
		}

		public static IDbCommand PrepareCommand(String sql, ConnectionKeyEnum connectionKey)
		{
            return PrepareCommand(sql, Connection.GetInstance(connectionKey).GetDbConnection(), 
                Connection.GetInstance(connectionKey).Transaction);
		}

		public static IDbCommand PrepareCommand(String sql)
		{
		    return PrepareCommand(sql, ConnectionKeyEnum.Default);
		}

		public static IDbDataParameter CreateParameter(IDbCommand dbCommand, String name, Guid guidValue)
		{
			IDbDataParameter dbParameter = dbCommand.CreateParameter();

			dbParameter.DbType = DbType.Guid;
			dbParameter.ParameterName = name;
			dbParameter.Value = guidValue;
			dbParameter.Direction = ParameterDirection.Input;
           
            
			return dbParameter;
		}

		public static IDbDataParameter CreateParameter(IDbCommand dbCommand, String name, String stringValue)
		{
			IDbDataParameter dbParameter = dbCommand.CreateParameter();

			dbParameter.DbType = DbType.String;
			dbParameter.ParameterName = name;

			if (stringValue != null)
			{
				//dbParameter.Size = stringValue.Length;
				dbParameter.Value = stringValue;
			}
			else
				dbParameter.Value = DBNull.Value;

			dbParameter.Direction = ParameterDirection.Input;

			return dbParameter;
		}

		public static IDbDataParameter CreateParameter(IDbCommand dbCommand, String name, DateTime dateValue)
		{
			IDbDataParameter dbParameter = dbCommand.CreateParameter();

			dbParameter.DbType = DbType.DateTime;
			dbParameter.ParameterName = name;
            dbParameter.Value = dateValue;
			dbParameter.Direction = ParameterDirection.Input;

			return dbParameter;
		}



        public static IDbDataParameter CreateParameter(IDbCommand dbCommand, String name, DateTime? dateValue)
        {
            IDbDataParameter dbParameter = dbCommand.CreateParameter();

            dbParameter.DbType = DbType.DateTime;
            dbParameter.ParameterName = name;

            if (dateValue != null)
                dbParameter.Value = dateValue;
            else
                dbParameter.Value = DBNull.Value;

            dbParameter.Direction = ParameterDirection.Input;

            return dbParameter;
        }

		public static IDbDataParameter CreateParameter(IDbCommand dbCommand, String name, int numericValue)
		{
			IDbDataParameter dbParameter = dbCommand.CreateParameter();

			dbParameter.DbType = DbType.Int32;
			dbParameter.ParameterName = name;
			dbParameter.Value = numericValue;
			dbParameter.Direction = ParameterDirection.Input;

			return dbParameter;
		}


        public static IDbDataParameter CreateParameter(IDbCommand dbCommand, String name, int? numericValue)
        {
            IDbDataParameter dbParameter = dbCommand.CreateParameter();

            dbParameter.DbType = DbType.Int32;
            dbParameter.ParameterName = name;

            if (numericValue != null)
                dbParameter.Value = numericValue;
            else
                dbParameter.Value = DBNull.Value;

            dbParameter.Direction = ParameterDirection.Input;

            return dbParameter;
        }

		public static IDbDataParameter CreateParameter(IDbCommand dbCommand, String name, double doubleValue)
		{
			IDbDataParameter dbParameter = dbCommand.CreateParameter();

			dbParameter.DbType = DbType.Double;
			dbParameter.ParameterName = name;
			dbParameter.Value = doubleValue;
			dbParameter.Direction = ParameterDirection.Input;

			return dbParameter;
		}

		public static IDbDataParameter CreateParameter(IDbCommand dbCommand, String name, byte? byteValue)
		{
			IDbDataParameter dbParameter = dbCommand.CreateParameter();

			dbParameter.DbType = DbType.Byte;
			dbParameter.ParameterName = name;
            
            if (byteValue != null)
                dbParameter.Value = byteValue;
            else
                dbParameter.Value = DBNull.Value;

			dbParameter.Direction = ParameterDirection.Input;

			return dbParameter;
		}


        public static IDbDataParameter CreateParameter(IDbCommand dbCommand, String name, byte byteValue)
        {
            IDbDataParameter dbParameter = dbCommand.CreateParameter();

            dbParameter.DbType = DbType.Byte;
            dbParameter.ParameterName = name;

            dbParameter.Value = byteValue;
 
            dbParameter.Direction = ParameterDirection.Input;

            return dbParameter;
        }

		public static IDbDataParameter CreateParameter(IDbCommand dbCommand, String name, bool boolValue)
		{
			IDbDataParameter dbParameter = dbCommand.CreateParameter();

			dbParameter.DbType = DbType.Boolean;
			dbParameter.ParameterName = name;
			dbParameter.Value = boolValue;
			dbParameter.Direction = ParameterDirection.Input;

            
			return dbParameter;
		}

        public static IDbDataParameter CreateParameter(IDbCommand dbCommand, String name, decimal decimalValue)
        {
            IDbDataParameter dbParameter = dbCommand.CreateParameter();

            dbParameter.DbType = DbType.Decimal;
            dbParameter.ParameterName = name;
            dbParameter.Value = decimalValue;
            dbParameter.Direction = ParameterDirection.Input;


            return dbParameter;
        }

        public static IDbDataParameter CreateParameter(IDbCommand dbCommand, String name, decimal? decimalValue)
        {
            IDbDataParameter dbParameter = dbCommand.CreateParameter();

            dbParameter.DbType = DbType.Decimal;
            dbParameter.ParameterName = name;

            if (decimalValue != null)
                dbParameter.Value = decimalValue;
            else
                dbParameter.Value = DBNull.Value;

            dbParameter.Direction = ParameterDirection.Input;

            return dbParameter;
        }

        public static IDbDataParameter CreateParameter(IDbCommand dbCommand, String name,  XmlDocument xmlValue)
        {
            IDbDataParameter dbParameter = dbCommand.CreateParameter();

            dbParameter.DbType = DbType.Xml;
            dbParameter.ParameterName = name;
            dbParameter.Value = xmlValue.ToString();
            dbParameter.Direction = ParameterDirection.Input;


            return dbParameter;
        }

        public static IDbDataParameter CreateParameter(IDbCommand dbCommand, String name, long longValue)
        {
            IDbDataParameter dbParameter = dbCommand.CreateParameter();

            dbParameter.DbType = DbType.Double;
            dbParameter.ParameterName = name;
            dbParameter.Value = longValue;
            dbParameter.Direction = ParameterDirection.Input;


            return dbParameter;
        }

        public static IDbDataParameter CreateParameter(IDbCommand dbCommand, String name, DbType dbType)
        {
            IDbDataParameter dbParameter = dbCommand.CreateParameter();

            dbParameter.DbType = dbType;
            dbParameter.ParameterName = name;
            dbParameter.Value = DBNull.Value;
            dbParameter.Direction = ParameterDirection.Input;

            return dbParameter;
        }

        public static void PutParameter(IDbCommand dbCommand, String name, DbType dbType)
        {
            IDbDataParameter dbParameter = CreateParameter(dbCommand, name, dbType);

            dbCommand.Parameters.Add(dbParameter);
        }

		public static void PutParameter(IDbCommand dbCommand, String name, Guid guidValue)
		{
			IDbDataParameter dbParameter = CreateParameter(dbCommand,name,guidValue);

			dbCommand.Parameters.Add(dbParameter);
		}
		public static void PutParameter(IDbCommand dbCommand, String name, bool boolValue)
		{
			IDbDataParameter dbParameter = CreateParameter(dbCommand, name, boolValue);

			dbCommand.Parameters.Add(dbParameter);
		}

        public static void PutParameter(IDbCommand dbCommand, String name, long longValue)
        {
            IDbDataParameter dbParameter = CreateParameter(dbCommand, name, longValue);

            dbCommand.Parameters.Add(dbParameter);
        }

		public static void PutParameter(IDbCommand dbCommand, String name, String stringValue)
		{
			IDbDataParameter dbParameter = CreateParameter(dbCommand, name, stringValue);

			dbCommand.Parameters.Add(dbParameter);
		}

		public static void PutParameter(IDbCommand dbCommand, String name, DateTime dateValue)
		{
			IDbDataParameter dbParameter = CreateParameter(dbCommand, name, dateValue);

			dbCommand.Parameters.Add(dbParameter);
		}

        public static void PutParameter(IDbCommand dbCommand, String name, DateTime? dateValue)
        {
            IDbDataParameter dbParameter = CreateParameter(dbCommand, name, dateValue);

            dbCommand.Parameters.Add(dbParameter);
        }

		public static void PutParameter(IDbCommand dbCommand, String name, int numericValue)
		{
			IDbDataParameter dbParameter = CreateParameter(dbCommand, name, numericValue);

			dbCommand.Parameters.Add(dbParameter);
		}

        public static void PutParameter(IDbCommand dbCommand, String name, int? numericValue)
        {
            IDbDataParameter dbParameter = CreateParameter(dbCommand, name, numericValue);

            dbCommand.Parameters.Add(dbParameter);
        }

		public static void PutParameter(IDbCommand dbCommand, String name, double doubleValue)
		{
			IDbDataParameter dbParameter = CreateParameter(dbCommand, name, doubleValue);

			dbCommand.Parameters.Add(dbParameter);
		}

        public static void PutParameter(IDbCommand dbCommand, String name, byte byteValue)
        {
            IDbDataParameter dbParameter = CreateParameter(dbCommand,
                name,
                byteValue);

            dbCommand.Parameters.Add(dbParameter);
        }

		public static void PutParameter(IDbCommand dbCommand, String name, byte? byteValue)
		{
			IDbDataParameter dbParameter = CreateParameter(dbCommand, 
                name, 
                byteValue);

			dbCommand.Parameters.Add(dbParameter);
		}

        public static void PutParameter(IDbCommand dbCommand, String name, decimal decimalValue)
        {
            IDbDataParameter dbParameter = CreateParameter(dbCommand, name, decimalValue);

            dbCommand.Parameters.Add(dbParameter);
        }

        public static void PutParameter(IDbCommand dbCommand, String name, decimal? decimalValue)
        {
            IDbDataParameter dbParameter = CreateParameter(dbCommand,
                name,
                decimalValue);

            dbCommand.Parameters.Add(dbParameter);
        }

        public static void PutParameter(IDbCommand dbCommand, String name, XmlDocument xmlValue)
        {
            IDbDataParameter dbParameter = CreateParameter(dbCommand, name, xmlValue);

            dbCommand.Parameters.Add(dbParameter);
        }

        public static void UpdateParameter(IDbCommand dbCommand, String name, Object value)
        {
            IDbDataParameter dataParameter = (IDbDataParameter)dbCommand.Parameters[name];


            if (value != null)
            {
                dataParameter.Value = value;
                
            }else
                dataParameter.Value = DBNull.Value;
        }

	}
}
