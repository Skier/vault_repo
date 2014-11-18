
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class LeadSource : ICloneable
    {

        #region Store


        #region Save

        public static LeadSource Save(LeadSource leadSource, IDbConnection connection)
        {
        	if (!Exists(leadSource, connection))
        		Insert(leadSource, connection);
        	else
        		Update(leadSource, connection);
        	return leadSource;
        }

        public static LeadSource Save(LeadSource leadSource)
        {
        	if (!Exists(leadSource))
        		Insert(leadSource);
        	else
        		Update(leadSource);
        	return leadSource;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into LeadSource ( " +
        
          " Name, " +
        
          " UserId, " +
        
          " IsActive, " +
        
          " OwnedByUserId " +
        
        ") Values (" +
        
          " ?Name, " +
        
          " ?UserId, " +
        
          " ?IsActive, " +
        
          " ?OwnedByUserId " +
        
        ")";

        public static void Insert(LeadSource leadSource, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Name", leadSource.Name);
            
            	Database.PutParameter(dbCommand,"?UserId", leadSource.UserId);
            
            	Database.PutParameter(dbCommand,"?IsActive", leadSource.IsActive);
            
            	Database.PutParameter(dbCommand,"?OwnedByUserId", leadSource.OwnedByUserId);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		leadSource.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(LeadSource leadSource)
        {
          	Insert(leadSource, null);
        }

        public static void Insert(List<LeadSource>  leadSourceList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(LeadSource leadSource in  leadSourceList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?Name", leadSource.Name);
                
                  	Database.PutParameter(dbCommand,"?UserId", leadSource.UserId);
                
                  	Database.PutParameter(dbCommand,"?IsActive", leadSource.IsActive);
                
                  	Database.PutParameter(dbCommand,"?OwnedByUserId", leadSource.OwnedByUserId);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?Name",leadSource.Name);
                
                	Database.UpdateParameter(dbCommand,"?UserId",leadSource.UserId);
                
                	Database.UpdateParameter(dbCommand,"?IsActive",leadSource.IsActive);
                
                	Database.UpdateParameter(dbCommand,"?OwnedByUserId",leadSource.OwnedByUserId);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	leadSource.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<LeadSource>  leadSourceList)
        {
        	Insert(leadSourceList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update LeadSource Set "
          
            + " Name = ?Name, "
          
            + " UserId = ?UserId, "
          
            + " IsActive = ?IsActive, "
          
            + " OwnedByUserId = ?OwnedByUserId "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(LeadSource leadSource, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", leadSource.Id);
            
            	Database.PutParameter(dbCommand,"?Name", leadSource.Name);
            
            	Database.PutParameter(dbCommand,"?UserId", leadSource.UserId);
            
            	Database.PutParameter(dbCommand,"?IsActive", leadSource.IsActive);
            
            	Database.PutParameter(dbCommand,"?OwnedByUserId", leadSource.OwnedByUserId);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(LeadSource leadSource)
        {
          	Update(leadSource, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " Name, "
        
          + " UserId, "
        
          + " IsActive, "
        
          + " OwnedByUserId "
        
          + " From LeadSource "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static LeadSource FindByPrimaryKey(
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

            throw new DataNotFoundException("LeadSource not found, search by primary key");
        }

        public static LeadSource FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(LeadSource leadSource, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",leadSource.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(LeadSource leadSource)
        {
        	return Exists(leadSource, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from LeadSource limit 1";

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

        public static LeadSource Load(IDataReader dataReader, int offset)
        {
              LeadSource leadSource = new LeadSource();

              leadSource.Id = dataReader.GetInt32(0 + offset);
                  leadSource.Name = dataReader.GetString(1 + offset);
                  
                    if(!dataReader.IsDBNull(2 + offset))
                    leadSource.UserId = dataReader.GetInt32(2 + offset);
                  leadSource.IsActive = dataReader.GetBoolean(3 + offset);
                  
                    if(!dataReader.IsDBNull(4 + offset))
                    leadSource.OwnedByUserId = dataReader.GetInt32(4 + offset);
                  

            return leadSource;
        }

        public static LeadSource Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From LeadSource "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(LeadSource leadSource, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", leadSource.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(LeadSource leadSource)
        {
        	Delete(leadSource, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From LeadSource ";

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
              
                + " UserId, "
              
                + " IsActive, "
              
                + " OwnedByUserId "
              
                + " From LeadSource ";

        public static List<LeadSource> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<LeadSource> rv = new List<LeadSource>();

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

        public static List<LeadSource> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<LeadSource> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<LeadSource> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(LeadSource));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(LeadSource item in itemsList)
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

        public static List<LeadSource> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(LeadSource));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<LeadSource> itemsList = new List<LeadSource>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is LeadSource)
              		itemsList.Add(deserializedObject as LeadSource);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected String m_name;
              
        protected int? m_userId;
              
        protected bool m_isActive;
              
        protected int? m_ownedByUserId;
              
        #endregion

        #region Constructors

        public LeadSource(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public LeadSource(
                int 
                  id,String 
                  name,int? 
                  userId,bool 
                  isActive,int? 
                  ownedByUserId
                ) : this()
        {
            
        	m_id = id;
            
        	m_name = name;
            
        	m_userId = userId;
            
        	m_isActive = isActive;
            
        	m_ownedByUserId = ownedByUserId;
            
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
        
        public int? UserId
        {
        	get { return m_userId;}
            set { m_userId = value; }
        }
        
        public bool IsActive
        {
        	get { return m_isActive;}
            set { m_isActive = value; }
        }
        
        public int? OwnedByUserId
        {
        	get { return m_ownedByUserId;}
            set { m_ownedByUserId = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 5; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    