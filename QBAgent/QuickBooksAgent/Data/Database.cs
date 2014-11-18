using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Xml;

#if WINCE
using SqlCe = System.Data.SqlServerCe;
using System.Data.Common;
#endif

namespace QuickBooksAgent.Data
{
    public enum ForeignKeyAction { Create, Drop };

	public static class Database
	{
        private const String DbSchemaCommandDelimiter = "GO";
        public static bool useMaster = false;
        public static bool IsDatabaseExist()
        {
            return File.Exists(Configuration.DBFullPath);
        }

        public static bool UnderTransaction
        {
            get
            {
                return Connection.Instance.Transaction != null;
            }
        }

        #region IsSchemaExist
        const String SqlIsSchemaExist = "Select sysobjects.name from sysobjects where sysobjects.name = 'Route'";

        public static bool IsSchemaExist()
        {
            IDbCommand dbCommand = Database.PrepareCommand(SqlIsSchemaExist);

            using (IDataReader dataReader = dbCommand.ExecuteReader())
            {
                return dataReader.Read();
            }
        }
        #endregion

        public static void RemoveDbReadOnlyAttribute()
        {
            FileInfo fileInfo = new FileInfo(Configuration.DBFullPath);

            if ((fileInfo.Attributes & FileAttributes.ReadOnly) != 0)
                fileInfo.Attributes -= System.IO.FileAttributes.ReadOnly;
        }

        public static void ExecuteScript(String filePath)
        {

            if (!File.Exists(filePath))
                throw new QuickBooksAgentException("Stript file " + filePath + " not found");

            Begin();

            StringBuilder sqlBuffer = new StringBuilder();

            try
            {
                IDbCommand dbCommand = Connection.Instance.DbConnection.CreateCommand();
                dbCommand.Transaction = Connection.Instance.Transaction;

                StreamReader streamReader = File.OpenText(filePath);

   

                while (!streamReader.EndOfStream)
                {
                    String line = streamReader.ReadLine();

                    if(DbSchemaCommandDelimiter.Equals(line))
                    {
                        dbCommand.CommandText = sqlBuffer.ToString();

                        dbCommand.ExecuteNonQuery();

                        sqlBuffer.Remove(0, sqlBuffer.Length);
                    }
                    else if (line.StartsWith("--"))
                    {
                        continue;
                    }
                    else
                        sqlBuffer.Append(line);

                }


                Commit(); 
            }
            catch (Exception ex)
            {
                Rollback();

                throw new QuickBooksAgentException(sqlBuffer.ToString(), ex); ;
            }
        }
        public static void ExecuteScript(String filePath, bool useTransaction)
        {

            if (!File.Exists(filePath))
                throw new QuickBooksAgentException("Stript file " + filePath + " not found");

            if (useTransaction)
                Begin();

            StringBuilder sqlBuffer = new StringBuilder();

            try
            {
                IDbCommand dbCommand = Connection.Instance.DbConnection.CreateCommand();
                if (useTransaction)
                    dbCommand.Transaction = Connection.Instance.Transaction;

                StreamReader streamReader = File.OpenText(filePath);



                while (!streamReader.EndOfStream)
                {
                    String line = streamReader.ReadLine();

                    if (DbSchemaCommandDelimiter.Equals(line))
                    {
                        dbCommand.CommandText = sqlBuffer.ToString();

                        dbCommand.ExecuteNonQuery();

                        sqlBuffer.Remove(0, sqlBuffer.Length);
                    }
                    else if (line.StartsWith("--"))
                    {
                        continue;
                    }
                    else
                        sqlBuffer.Append(line);

                }

                if (useTransaction)
                    Commit();
            }
            catch (Exception ex)
            {
                Rollback();

                throw new QuickBooksAgentException(sqlBuffer.ToString(), ex); ;
            }
        }
        public static void ExecuteScript(String filePath, bool useTransaction, String parameter1)
        {

            if (!File.Exists(filePath))
                throw new QuickBooksAgentException("Stript file " + filePath + " not found");
            if (useTransaction)
                Begin();

            StringBuilder sqlBuffer = new StringBuilder();

            try
            {
                IDbCommand dbCommand = Connection.Instance.DbConnection.CreateCommand();
                if (useTransaction)
                    dbCommand.Transaction = Connection.Instance.Transaction;

                StreamReader streamReader = File.OpenText(filePath);

                while (!streamReader.EndOfStream)
                {
                    String line = streamReader.ReadLine();

                    if (DbSchemaCommandDelimiter.Equals(line))
                    {
                        dbCommand.CommandText = sqlBuffer.ToString();

                        dbCommand.ExecuteNonQuery();

                        sqlBuffer.Remove(0, sqlBuffer.Length);
                    }
                    else if (line.StartsWith("--"))
                    {
                        continue;
                    }
                    else
                        sqlBuffer.Append(line);

                }

                if (useTransaction)
                    Commit();
            }
            catch (Exception ex)
            {
                Rollback();

                throw new QuickBooksAgentException(sqlBuffer.ToString(), ex); ;
            }
        }
        public static void ExecuteScript(String filePath, bool useTransaction, String parameter1, String parameter2)
        {

            if (!File.Exists(filePath))
                throw new QuickBooksAgentException("Stript file " + filePath + " not found");
            if (useTransaction)
                Begin();

            StringBuilder sqlBuffer = new StringBuilder();

            try
            {
                IDbCommand dbCommand = Connection.Instance.DbConnection.CreateCommand();
                if (useTransaction)
                    dbCommand.Transaction = Connection.Instance.Transaction;

                StreamReader streamReader = File.OpenText(filePath);



                while (!streamReader.EndOfStream)
                {
                    String line = streamReader.ReadLine();

                    if (DbSchemaCommandDelimiter.Equals(line))
                    {
                        dbCommand.CommandText = String.Format(sqlBuffer.ToString(), parameter1, parameter2);

                        dbCommand.ExecuteNonQuery();

                        sqlBuffer.Remove(0, sqlBuffer.Length);
                    }
                    else if (line.StartsWith("--"))
                    {
                        continue;
                    }
                    else
                        sqlBuffer.Append(line);

                }

                if (useTransaction)
                    Commit();
            }
            catch (Exception ex)
            {
                Rollback();

                throw new QuickBooksAgentException(sqlBuffer.ToString(), ex); ;
            }
        }
        public static void Close()
        {
            Connection.Instance.CloseConnections();
        }
        public static void ForeignKeys(ForeignKeyAction action)
        {
            if (action == ForeignKeyAction.Create)
            {

            }
            else
            {

            }
        }
        public static void CreateDatabase(bool createSchema)
        {
#if WINCE
            if (IsDatabaseExist())
                throw new QuickBooksAgentException("Database already exists");

            SqlCe.SqlCeEngine sqlCeEngine = new System.Data.SqlServerCe.SqlCeEngine(Configuration.ConnectionString);

            sqlCeEngine.CreateDatabase();
#else
            Database.Close();
            useMaster = true;
            String dbCreateDatabase = @"Database\dbCreateDatabase.regular.sql";
            try
            {
                ExecuteScript(dbCreateDatabase, false, Configuration.DBFullPath,Configuration.DBLogFullPath);
            }
            catch (Exception e)
            {
                throw e;
            }
            useMaster = false;
            Database.Close();
#endif
            if (createSchema)
            {
                try
                {
                    string path = Path.GetDirectoryName(QuickBooksAgent.Configuration.AppNameFullPath);
#if WINCE
                    String dbSchemaFilePath = path + @"\Database\dbSchema.mobile.sql";
#else
                RemoveDbReadOnlyAttribute();

                String dbSchemaFilePath = @"Database\dbSchema.regular.sql";
#endif
                    ExecuteScript(dbSchemaFilePath);

                }
                catch (Exception e)
                {

                    throw e;
                }
            }

        }
        public static void DetachDatabase()
        {
#if WIN32
            try
            {
                Database.Close();
                String dbDetachDatabase = @"Database\dbDetachDatabase.regular.sql";
                Database.useMaster = true;
                Database.ExecuteScript(dbDetachDatabase, false);
                Database.useMaster = false;
                Database.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
#endif
        }
        public static void AttachDatabase()
        {
#if WIN32
            try
            {
                Database.Close();
                String dbDetachDatabase = @"Database\dbAttachDatabase.regular.sql";
                Database.useMaster = true;
                Database.ExecuteScript(dbDetachDatabase, false, Configuration.DBFullPath, Configuration.DBLogFullPath);
                Database.useMaster = false;
                Database.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
#endif
        }
		public static void Begin()
		{
            Connection.Instance.BeginSystemTransaction();
		}

		public static void Commit()
		{
            Connection.Instance.CommitSystemTransaction();
		}

		public static void Rollback()
		{
            Connection.Instance.RollbackSystemTransaction();
		}

		public static IDbCommand PrepareCommand(String sql,IDbConnection dbConnection)
		{
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

		public static IDbCommand PrepareCommand(String sql)
		{
            return PrepareCommand(sql, Connection.Instance.DbConnection, Connection.Instance.Transaction);
		}

		public static IDbDataParameter CreateParameter(IDbCommand dbCommand, String name, System.Guid guidValue)
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

			if (!DateTime.MinValue.Equals(dateValue))
				dbParameter.Value = dateValue;
			else
				dbParameter.Value = DBNull.Value;

			dbParameter.Direction = ParameterDirection.Input;

			return dbParameter;
		}



        public static IDbDataParameter CreateParameter(IDbCommand dbCommand, String name, DateTime? dateValue)
        {
            IDbDataParameter dbParameter = dbCommand.CreateParameter();

            dbParameter.DbType = DbType.DateTime;
            dbParameter.ParameterName = name;

            if (dateValue != null && !DateTime.MinValue.Equals(dateValue))
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

		public static void PutParameter(IDbCommand dbCommand, String name, System.Guid guidValue)
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
