
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class LeadType : ICloneable
    {

        #region Store


        #region Save

        public static LeadType Save(LeadType leadType, IDbConnection connection)
        {
        	if (!Exists(leadType, connection))
        		Insert(leadType, connection);
        	else
        		Update(leadType, connection);
        	return leadType;
        }

        public static LeadType Save(LeadType leadType)
        {
        	if (!Exists(leadType))
        		Insert(leadType);
        	else
        		Update(leadType);
        	return leadType;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into LeadType ( " +
        
          " Name, " +
        
          " QbJobTypeRecordId, " +
        
          " IsActive " +
        
        ") Values (" +
        
          " ?Name, " +
        
          " ?QbJobTypeRecordId, " +
        
          " ?IsActive " +
        
        ")";

        public static void Insert(LeadType leadType, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Name", leadType.Name);
            
            	Database.PutParameter(dbCommand,"?QbJobTypeRecordId", leadType.QbJobTypeRecordId);
            
            	Database.PutParameter(dbCommand,"?IsActive", leadType.IsActive);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		leadType.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(LeadType leadType)
        {
          	Insert(leadType, null);
        }

        public static void Insert(List<LeadType>  leadTypeList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(LeadType leadType in  leadTypeList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?Name", leadType.Name);
                
                  	Database.PutParameter(dbCommand,"?QbJobTypeRecordId", leadType.QbJobTypeRecordId);
                
                  	Database.PutParameter(dbCommand,"?IsActive", leadType.IsActive);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?Name",leadType.Name);
                
                	Database.UpdateParameter(dbCommand,"?QbJobTypeRecordId",leadType.QbJobTypeRecordId);
                
                	Database.UpdateParameter(dbCommand,"?IsActive",leadType.IsActive);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	leadType.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<LeadType>  leadTypeList)
        {
        	Insert(leadTypeList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update LeadType Set "
          
            + " Name = ?Name, "
          
            + " QbJobTypeRecordId = ?QbJobTypeRecordId, "
          
            + " IsActive = ?IsActive "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(LeadType leadType, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", leadType.Id);
            
            	Database.PutParameter(dbCommand,"?Name", leadType.Name);
            
            	Database.PutParameter(dbCommand,"?QbJobTypeRecordId", leadType.QbJobTypeRecordId);
            
            	Database.PutParameter(dbCommand,"?IsActive", leadType.IsActive);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(LeadType leadType)
        {
          	Update(leadType, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " Name, "
        
          + " QbJobTypeRecordId, "
        
          + " IsActive "
        
          + " From LeadType "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static LeadType FindByPrimaryKey(
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

            throw new DataNotFoundException("LeadType not found, search by primary key");
        }

        public static LeadType FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(LeadType leadType, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",leadType.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(LeadType leadType)
        {
        	return Exists(leadType, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from LeadType limit 1";

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

        public static LeadType Load(IDataReader dataReader, int offset)
        {
              LeadType leadType = new LeadType();

              leadType.Id = dataReader.GetInt32(0 + offset);
                  leadType.Name = dataReader.GetString(1 + offset);
                  
                    if(!dataReader.IsDBNull(2 + offset))
                    leadType.QbJobTypeRecordId = dataReader.GetString(2 + offset);
                  leadType.IsActive = dataReader.GetBoolean(3 + offset);
                  

            return leadType;
        }

        public static LeadType Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From LeadType "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(LeadType leadType, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", leadType.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(LeadType leadType)
        {
        	Delete(leadType, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From LeadType ";

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
              
                + " Name, "
              
                + " QbJobTypeRecordId, "
              
                + " IsActive "
              
                + " From LeadType ";

        public static List<LeadType> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<LeadType> rv = new List<LeadType>();

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

        public static List<LeadType> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<LeadType> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<LeadType> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(LeadType));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(LeadType item in itemsList)
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

        public static List<LeadType> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(LeadType));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<LeadType> itemsList = new List<LeadType>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is LeadType)
              		itemsList.Add(deserializedObject as LeadType);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected String m_name;
              
        protected String m_qbJobTypeRecordId;
              
        protected bool m_isActive;
              
        #endregion

        #region Constructors

        public LeadType(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public LeadType(
                int 
                  id,String 
                  name,String 
                  qbJobTypeRecordId,bool 
                  isActive
                ) : this()
        {
            
        	m_id = id;
            
        	m_name = name;
            
        	m_qbJobTypeRecordId = qbJobTypeRecordId;
            
        	m_isActive = isActive;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public String Name
        {
        	get { return m_name;}
            set { m_name = value; }
        }
        
        public String QbJobTypeRecordId
        {
        	get { return m_qbJobTypeRecordId;}
            set { m_qbJobTypeRecordId = value; }
        }
        
        public bool IsActive
        {
        	get { return m_isActive;}
            set { m_isActive = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 4; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    