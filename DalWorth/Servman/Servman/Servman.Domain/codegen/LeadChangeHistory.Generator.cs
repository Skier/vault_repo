
using System;
using System.Data;
using System.Collections.Generic;
using Servman.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Servman.Domain
{

    public partial class LeadChangeHistory : ICloneable
    {

        #region Store


        #region Save

        public static LeadChangeHistory Save(LeadChangeHistory leadChangeHistory, IDbConnection connection)
        {
        	if (!Exists(leadChangeHistory, connection))
        		Insert(leadChangeHistory, connection);
        	else
        		Update(leadChangeHistory, connection);
        	return leadChangeHistory;
        }

        public static LeadChangeHistory Save(LeadChangeHistory leadChangeHistory)
        {
        	if (!Exists(leadChangeHistory))
        		Insert(leadChangeHistory);
        	else
        		Update(leadChangeHistory);
        	return leadChangeHistory;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into LeadChangeHistory ( " +
        
          " LeadId, " +
        
          " LeadStatusId, " +
        
          " DateChanged, " +
        
          " UserId, " +
        
          " Action, " +
        
          " Description " +
        
        ") Values (" +
        
          " ?LeadId, " +
        
          " ?LeadStatusId, " +
        
          " ?DateChanged, " +
        
          " ?UserId, " +
        
          " ?Action, " +
        
          " ?Description " +
        
        ")";

        public static void Insert(LeadChangeHistory leadChangeHistory, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?LeadId", leadChangeHistory.LeadId);
            
            	Database.PutParameter(dbCommand,"?LeadStatusId", leadChangeHistory.LeadStatusId);
            
            	Database.PutParameter(dbCommand,"?DateChanged", leadChangeHistory.DateChanged);
            
            	Database.PutParameter(dbCommand,"?UserId", leadChangeHistory.UserId);
            
            	Database.PutParameter(dbCommand,"?Action", leadChangeHistory.Action);
            
            	Database.PutParameter(dbCommand,"?Description", leadChangeHistory.Description);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		leadChangeHistory.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(LeadChangeHistory leadChangeHistory)
        {
          	Insert(leadChangeHistory, null);
        }

        public static void Insert(List<LeadChangeHistory>  leadChangeHistoryList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(LeadChangeHistory leadChangeHistory in  leadChangeHistoryList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?LeadId", leadChangeHistory.LeadId);
                
                  	Database.PutParameter(dbCommand,"?LeadStatusId", leadChangeHistory.LeadStatusId);
                
                  	Database.PutParameter(dbCommand,"?DateChanged", leadChangeHistory.DateChanged);
                
                  	Database.PutParameter(dbCommand,"?UserId", leadChangeHistory.UserId);
                
                  	Database.PutParameter(dbCommand,"?Action", leadChangeHistory.Action);
                
                  	Database.PutParameter(dbCommand,"?Description", leadChangeHistory.Description);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?LeadId",leadChangeHistory.LeadId);
                
                	Database.UpdateParameter(dbCommand,"?LeadStatusId",leadChangeHistory.LeadStatusId);
                
                	Database.UpdateParameter(dbCommand,"?DateChanged",leadChangeHistory.DateChanged);
                
                	Database.UpdateParameter(dbCommand,"?UserId",leadChangeHistory.UserId);
                
                	Database.UpdateParameter(dbCommand,"?Action",leadChangeHistory.Action);
                
                	Database.UpdateParameter(dbCommand,"?Description",leadChangeHistory.Description);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	leadChangeHistory.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<LeadChangeHistory>  leadChangeHistoryList)
        {
        	Insert(leadChangeHistoryList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update LeadChangeHistory Set "
          
            + " LeadId = ?LeadId, "
          
            + " LeadStatusId = ?LeadStatusId, "
          
            + " DateChanged = ?DateChanged, "
          
            + " UserId = ?UserId, "
          
            + " Action = ?Action, "
          
            + " Description = ?Description "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(LeadChangeHistory leadChangeHistory, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", leadChangeHistory.Id);
            
            	Database.PutParameter(dbCommand,"?LeadId", leadChangeHistory.LeadId);
            
            	Database.PutParameter(dbCommand,"?LeadStatusId", leadChangeHistory.LeadStatusId);
            
            	Database.PutParameter(dbCommand,"?DateChanged", leadChangeHistory.DateChanged);
            
            	Database.PutParameter(dbCommand,"?UserId", leadChangeHistory.UserId);
            
            	Database.PutParameter(dbCommand,"?Action", leadChangeHistory.Action);
            
            	Database.PutParameter(dbCommand,"?Description", leadChangeHistory.Description);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(LeadChangeHistory leadChangeHistory)
        {
          	Update(leadChangeHistory, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " LeadId, "
        
          + " LeadStatusId, "
        
          + " DateChanged, "
        
          + " UserId, "
        
          + " Action, "
        
          + " Description "
        
          + " From LeadChangeHistory "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static LeadChangeHistory FindByPrimaryKey(
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

            throw new DataNotFoundException("LeadChangeHistory not found, search by primary key");
        }

        public static LeadChangeHistory FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(LeadChangeHistory leadChangeHistory, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",leadChangeHistory.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(LeadChangeHistory leadChangeHistory)
        {
        	return Exists(leadChangeHistory, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from LeadChangeHistory limit 1";

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

        public static LeadChangeHistory Load(IDataReader dataReader, int offset)
        {
              LeadChangeHistory leadChangeHistory = new LeadChangeHistory();

              leadChangeHistory.Id = dataReader.GetInt32(0 + offset);
                  leadChangeHistory.LeadId = dataReader.GetInt32(1 + offset);
                  
                    if(!dataReader.IsDBNull(2 + offset))
                    leadChangeHistory.LeadStatusId = dataReader.GetInt32(2 + offset);
                  leadChangeHistory.DateChanged = dataReader.GetDateTime(3 + offset);
                  leadChangeHistory.UserId = dataReader.GetInt32(4 + offset);
                  leadChangeHistory.Action = dataReader.GetString(5 + offset);
                  leadChangeHistory.Description = dataReader.GetString(6 + offset);
                  

            return leadChangeHistory;
        }

        public static LeadChangeHistory Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From LeadChangeHistory "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(LeadChangeHistory leadChangeHistory, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", leadChangeHistory.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(LeadChangeHistory leadChangeHistory)
        {
        	Delete(leadChangeHistory, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From LeadChangeHistory ";

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
              
                + " LeadId, "
              
                + " LeadStatusId, "
              
                + " DateChanged, "
              
                + " UserId, "
              
                + " Action, "
              
                + " Description "
              
                + " From LeadChangeHistory ";

        public static List<LeadChangeHistory> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<LeadChangeHistory> rv = new List<LeadChangeHistory>();

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

        public static List<LeadChangeHistory> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<LeadChangeHistory> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<LeadChangeHistory> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(LeadChangeHistory));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(LeadChangeHistory item in itemsList)
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

        public static List<LeadChangeHistory> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(LeadChangeHistory));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<LeadChangeHistory> itemsList = new List<LeadChangeHistory>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is LeadChangeHistory)
              		itemsList.Add(deserializedObject as LeadChangeHistory);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected int m_leadId;
              
        protected int? m_leadStatusId;
              
        protected DateTime m_dateChanged;
              
        protected int m_userId;
              
        protected String m_action;
              
        protected String m_description;
              
        #endregion

        #region Constructors

        public LeadChangeHistory(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public LeadChangeHistory(
                int 
                  id,int 
                  leadId,int? 
                  leadStatusId,DateTime 
                  dateChanged,int 
                  userId,String 
                  action,String 
                  description
                ) : this()
        {
            
        	m_id = id;
            
        	m_leadId = leadId;
            
        	m_leadStatusId = leadStatusId;
            
        	m_dateChanged = dateChanged;
            
        	m_userId = userId;
            
        	m_action = action;
            
        	m_description = description;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public int LeadId
        {
        	get { return m_leadId;}
            set { m_leadId = value; }
        }
        
        public int? LeadStatusId
        {
        	get { return m_leadStatusId;}
            set { m_leadStatusId = value; }
        }
        
        public DateTime DateChanged
        {
        	get { return m_dateChanged;}
            set { m_dateChanged = value; }
        }
        
        public int UserId
        {
        	get { return m_userId;}
            set { m_userId = value; }
        }
        
        public String Action
        {
        	get { return m_action;}
            set { m_action = value; }
        }
        
        public String Description
        {
        	get { return m_description;}
            set { m_description = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 7; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    