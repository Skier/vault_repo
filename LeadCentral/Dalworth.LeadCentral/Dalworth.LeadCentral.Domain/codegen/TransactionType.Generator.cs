
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Common.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class TransactionType : ICloneable
    {

        #region Store


        #region Save

        public static TransactionType Save(TransactionType transactionType, IDbConnection connection)
        {
        	if (!Exists(transactionType, connection))
        		Insert(transactionType, connection);
        	else
        		Update(transactionType, connection);
        	return transactionType;
        }

        public static TransactionType Save(TransactionType transactionType)
        {
        	if (!Exists(transactionType))
        		Insert(transactionType);
        	else
        		Update(transactionType);
        	return transactionType;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into TransactionType ( " +
        
          " TypeName, " +
        
          " BaseCost " +
        
        ") Values (" +
        
          " ?TypeName, " +
        
          " ?BaseCost " +
        
        ")";

        public static void Insert(TransactionType transactionType, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?TypeName", transactionType.TypeName);
            
            	Database.PutParameter(dbCommand,"?BaseCost", transactionType.BaseCost);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		transactionType.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(TransactionType transactionType)
        {
          	Insert(transactionType, null);
        }

        public static void Insert(List<TransactionType>  transactionTypeList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(TransactionType transactionType in  transactionTypeList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?TypeName", transactionType.TypeName);
                
                  	Database.PutParameter(dbCommand,"?BaseCost", transactionType.BaseCost);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?TypeName",transactionType.TypeName);
                
                	Database.UpdateParameter(dbCommand,"?BaseCost",transactionType.BaseCost);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	transactionType.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<TransactionType>  transactionTypeList)
        {
        	Insert(transactionTypeList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update TransactionType Set "
          
            + " TypeName = ?TypeName, "
          
            + " BaseCost = ?BaseCost "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(TransactionType transactionType, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", transactionType.Id);
            
            	Database.PutParameter(dbCommand,"?TypeName", transactionType.TypeName);
            
            	Database.PutParameter(dbCommand,"?BaseCost", transactionType.BaseCost);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(TransactionType transactionType)
        {
          	Update(transactionType, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " TypeName, "
        
          + " BaseCost "
        
          + " From TransactionType "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static TransactionType FindByPrimaryKey(
              int id, IDbConnection connection
              )
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
              
            	Database.PutParameter(dbCommand,"?Id", id);
              

              	using(IDataReader dataReader = dbCommand.ExecuteReader())
              	{
              		if(dataReader.Read())
              			return Load(dataReader);
              	}
            }

            throw new DataNotFoundException("TransactionType not found, search by primary key");
        }

        public static TransactionType FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(TransactionType transactionType, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",transactionType.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(TransactionType transactionType)
        {
        	return Exists(transactionType, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from TransactionType limit 1";

            using(IDbCommand dbCommand = Database.PrepareCommand(sql, connection))
            {
            	using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
              	{
              		return reader.Read();
              	}
            }
        }

        public static bool IsContainsData()
        {
        	return IsContainsData(null);
        }

        #endregion

        #region Load

        public static TransactionType Load(IDataReader dataReader, int offset)
        {
              TransactionType transactionType = new TransactionType();

              transactionType.Id = dataReader.GetInt32(0 + offset);
                  transactionType.TypeName = dataReader.GetString(1 + offset);
                  transactionType.BaseCost = dataReader.GetDecimal(2 + offset);
                  

            return transactionType;
        }

        public static TransactionType Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From TransactionType "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(TransactionType transactionType, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", transactionType.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(TransactionType transactionType)
        {
        	Delete(transactionType, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From TransactionType ";

        public static void Clear(IDbConnection connection)
        {
        	using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll, connection))
            {
             	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Clear()
        {
        	Clear(null);
        }

        #endregion

        #region Find

        private const String SqlSelectAll = "Select "
              
                + " Id, "
              
                + " TypeName, "
              
                + " BaseCost "
              
                + " From TransactionType ";

        public static List<TransactionType> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<TransactionType> rv = new List<TransactionType>();

              	using(IDataReader dataReader = dbCommand.ExecuteReader())
              	{
              		while(dataReader.Read())
              		{
              			rv.Add(Load(dataReader));
              		}
              	}

              	return rv;
        	}
        }

        public static List<TransactionType> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<TransactionType> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<TransactionType> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TransactionType));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(TransactionType item in itemsList)
            {
            	xmlSerializer.Serialize(xmlWriter, item);
            }

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();

            xmlWriter.Flush();
            xmlWriter.Close();

            return itemsList.Count;
        }

        #endregion

        #region Load from file

        public static List<TransactionType> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(TransactionType));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<TransactionType> itemsList = new List<TransactionType>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is TransactionType)
              		itemsList.Add(deserializedObject as TransactionType);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected String m_typeName;
              
        protected decimal m_baseCost;
              
        #endregion

        #region Constructors

        public TransactionType(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public TransactionType(
                int 
                  id,String 
                  typeName,decimal 
                  baseCost
                ) : this()
        {
            
        	m_id = id;
            
        	m_typeName = typeName;
            
        	m_baseCost = baseCost;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public String TypeName
        {
        	get { return m_typeName;}
            set { m_typeName = value; }
        }
        
        public decimal BaseCost
        {
        	get { return m_baseCost;}
            set { m_baseCost = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 3; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    