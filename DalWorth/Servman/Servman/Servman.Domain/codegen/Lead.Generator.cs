
using System;
using System.Data;
using System.Collections.Generic;
using Servman.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Servman.Domain
{

    public partial class Lead : ICloneable
    {

        #region Store


        #region Save

        public static Lead Save(Lead lead, IDbConnection connection)
        {
        	if (!Exists(lead, connection))
        		Insert(lead, connection);
        	else
        		Update(lead, connection);
        	return lead;
        }

        public static Lead Save(Lead lead)
        {
        	if (!Exists(lead))
        		Insert(lead);
        	else
        		Update(lead);
        	return lead;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into Lead ( " +
        
          " LeadStatusId, " +
        
          " LeadSourceId, " +
        
          " AssignedToUser, " +
        
          " FirstName, " +
        
          " LastName, " +
        
          " Phone, " +
        
          " Address, " +
        
          " CustomerNotes, " +
        
          " CreatedByUserId, " +
        
          " DateCreated, " +
        
          " DateContacted, " +
        
          " IsImportant, " +
        
          " PhoneCallId, " +
        
          " PhoneSmsId, " +
        
          " WebFormId, " +
        
          " DateLastUpdated " +
        
        ") Values (" +
        
          " ?LeadStatusId, " +
        
          " ?LeadSourceId, " +
        
          " ?AssignedToUser, " +
        
          " ?FirstName, " +
        
          " ?LastName, " +
        
          " ?Phone, " +
        
          " ?Address, " +
        
          " ?CustomerNotes, " +
        
          " ?CreatedByUserId, " +
        
          " ?DateCreated, " +
        
          " ?DateContacted, " +
        
          " ?IsImportant, " +
        
          " ?PhoneCallId, " +
        
          " ?PhoneSmsId, " +
        
          " ?WebFormId, " +
        
          " ?DateLastUpdated " +
        
        ")";

        public static void Insert(Lead lead, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?LeadStatusId", lead.LeadStatusId);
            
            	Database.PutParameter(dbCommand,"?LeadSourceId", lead.LeadSourceId);
            
            	Database.PutParameter(dbCommand,"?AssignedToUser", lead.AssignedToUser);
            
            	Database.PutParameter(dbCommand,"?FirstName", lead.FirstName);
            
            	Database.PutParameter(dbCommand,"?LastName", lead.LastName);
            
            	Database.PutParameter(dbCommand,"?Phone", lead.Phone);
            
            	Database.PutParameter(dbCommand,"?Address", lead.Address);
            
            	Database.PutParameter(dbCommand,"?CustomerNotes", lead.CustomerNotes);
            
            	Database.PutParameter(dbCommand,"?CreatedByUserId", lead.CreatedByUserId);
            
            	Database.PutParameter(dbCommand,"?DateCreated", lead.DateCreated);
            
            	Database.PutParameter(dbCommand,"?DateContacted", lead.DateContacted);
            
            	Database.PutParameter(dbCommand,"?IsImportant", lead.IsImportant);
            
            	Database.PutParameter(dbCommand,"?PhoneCallId", lead.PhoneCallId);
            
            	Database.PutParameter(dbCommand,"?PhoneSmsId", lead.PhoneSmsId);
            
            	Database.PutParameter(dbCommand,"?WebFormId", lead.WebFormId);
            
            	Database.PutParameter(dbCommand,"?DateLastUpdated", lead.DateLastUpdated);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		lead.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(Lead lead)
        {
          	Insert(lead, null);
        }

        public static void Insert(List<Lead>  leadList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(Lead lead in  leadList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?LeadStatusId", lead.LeadStatusId);
                
                  	Database.PutParameter(dbCommand,"?LeadSourceId", lead.LeadSourceId);
                
                  	Database.PutParameter(dbCommand,"?AssignedToUser", lead.AssignedToUser);
                
                  	Database.PutParameter(dbCommand,"?FirstName", lead.FirstName);
                
                  	Database.PutParameter(dbCommand,"?LastName", lead.LastName);
                
                  	Database.PutParameter(dbCommand,"?Phone", lead.Phone);
                
                  	Database.PutParameter(dbCommand,"?Address", lead.Address);
                
                  	Database.PutParameter(dbCommand,"?CustomerNotes", lead.CustomerNotes);
                
                  	Database.PutParameter(dbCommand,"?CreatedByUserId", lead.CreatedByUserId);
                
                  	Database.PutParameter(dbCommand,"?DateCreated", lead.DateCreated);
                
                  	Database.PutParameter(dbCommand,"?DateContacted", lead.DateContacted);
                
                  	Database.PutParameter(dbCommand,"?IsImportant", lead.IsImportant);
                
                  	Database.PutParameter(dbCommand,"?PhoneCallId", lead.PhoneCallId);
                
                  	Database.PutParameter(dbCommand,"?PhoneSmsId", lead.PhoneSmsId);
                
                  	Database.PutParameter(dbCommand,"?WebFormId", lead.WebFormId);
                
                  	Database.PutParameter(dbCommand,"?DateLastUpdated", lead.DateLastUpdated);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?LeadStatusId",lead.LeadStatusId);
                
                	Database.UpdateParameter(dbCommand,"?LeadSourceId",lead.LeadSourceId);
                
                	Database.UpdateParameter(dbCommand,"?AssignedToUser",lead.AssignedToUser);
                
                	Database.UpdateParameter(dbCommand,"?FirstName",lead.FirstName);
                
                	Database.UpdateParameter(dbCommand,"?LastName",lead.LastName);
                
                	Database.UpdateParameter(dbCommand,"?Phone",lead.Phone);
                
                	Database.UpdateParameter(dbCommand,"?Address",lead.Address);
                
                	Database.UpdateParameter(dbCommand,"?CustomerNotes",lead.CustomerNotes);
                
                	Database.UpdateParameter(dbCommand,"?CreatedByUserId",lead.CreatedByUserId);
                
                	Database.UpdateParameter(dbCommand,"?DateCreated",lead.DateCreated);
                
                	Database.UpdateParameter(dbCommand,"?DateContacted",lead.DateContacted);
                
                	Database.UpdateParameter(dbCommand,"?IsImportant",lead.IsImportant);
                
                	Database.UpdateParameter(dbCommand,"?PhoneCallId",lead.PhoneCallId);
                
                	Database.UpdateParameter(dbCommand,"?PhoneSmsId",lead.PhoneSmsId);
                
                	Database.UpdateParameter(dbCommand,"?WebFormId",lead.WebFormId);
                
                	Database.UpdateParameter(dbCommand,"?DateLastUpdated",lead.DateLastUpdated);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	lead.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<Lead>  leadList)
        {
        	Insert(leadList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update Lead Set "
          
            + " LeadStatusId = ?LeadStatusId, "
          
            + " LeadSourceId = ?LeadSourceId, "
          
            + " AssignedToUser = ?AssignedToUser, "
          
            + " FirstName = ?FirstName, "
          
            + " LastName = ?LastName, "
          
            + " Phone = ?Phone, "
          
            + " Address = ?Address, "
          
            + " CustomerNotes = ?CustomerNotes, "
          
            + " CreatedByUserId = ?CreatedByUserId, "
          
            + " DateCreated = ?DateCreated, "
          
            + " DateContacted = ?DateContacted, "
          
            + " IsImportant = ?IsImportant, "
          
            + " PhoneCallId = ?PhoneCallId, "
          
            + " PhoneSmsId = ?PhoneSmsId, "
          
            + " WebFormId = ?WebFormId, "
          
            + " DateLastUpdated = ?DateLastUpdated "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(Lead lead, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", lead.Id);
            
            	Database.PutParameter(dbCommand,"?LeadStatusId", lead.LeadStatusId);
            
            	Database.PutParameter(dbCommand,"?LeadSourceId", lead.LeadSourceId);
            
            	Database.PutParameter(dbCommand,"?AssignedToUser", lead.AssignedToUser);
            
            	Database.PutParameter(dbCommand,"?FirstName", lead.FirstName);
            
            	Database.PutParameter(dbCommand,"?LastName", lead.LastName);
            
            	Database.PutParameter(dbCommand,"?Phone", lead.Phone);
            
            	Database.PutParameter(dbCommand,"?Address", lead.Address);
            
            	Database.PutParameter(dbCommand,"?CustomerNotes", lead.CustomerNotes);
            
            	Database.PutParameter(dbCommand,"?CreatedByUserId", lead.CreatedByUserId);
            
            	Database.PutParameter(dbCommand,"?DateCreated", lead.DateCreated);
            
            	Database.PutParameter(dbCommand,"?DateContacted", lead.DateContacted);
            
            	Database.PutParameter(dbCommand,"?IsImportant", lead.IsImportant);
            
            	Database.PutParameter(dbCommand,"?PhoneCallId", lead.PhoneCallId);
            
            	Database.PutParameter(dbCommand,"?PhoneSmsId", lead.PhoneSmsId);
            
            	Database.PutParameter(dbCommand,"?WebFormId", lead.WebFormId);
            
            	Database.PutParameter(dbCommand,"?DateLastUpdated", lead.DateLastUpdated);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(Lead lead)
        {
          	Update(lead, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " LeadStatusId, "
        
          + " LeadSourceId, "
        
          + " AssignedToUser, "
        
          + " FirstName, "
        
          + " LastName, "
        
          + " Phone, "
        
          + " Address, "
        
          + " CustomerNotes, "
        
          + " CreatedByUserId, "
        
          + " DateCreated, "
        
          + " DateContacted, "
        
          + " IsImportant, "
        
          + " PhoneCallId, "
        
          + " PhoneSmsId, "
        
          + " WebFormId, "
        
          + " DateLastUpdated "
        
          + " From Lead "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static Lead FindByPrimaryKey(
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

            throw new DataNotFoundException("Lead not found, search by primary key");
        }

        public static Lead FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(Lead lead, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",lead.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(Lead lead)
        {
        	return Exists(lead, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from Lead limit 1";

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

        public static Lead Load(IDataReader dataReader, int offset)
        {
              Lead lead = new Lead();

              lead.Id = dataReader.GetInt32(0 + offset);
                  lead.LeadStatusId = dataReader.GetInt32(1 + offset);
                  
                    if(!dataReader.IsDBNull(2 + offset))
                    lead.LeadSourceId = dataReader.GetInt32(2 + offset);
                  
                    if(!dataReader.IsDBNull(3 + offset))
                    lead.AssignedToUser = dataReader.GetInt32(3 + offset);
                  
                    if(!dataReader.IsDBNull(4 + offset))
                    lead.FirstName = dataReader.GetString(4 + offset);
                  
                    if(!dataReader.IsDBNull(5 + offset))
                    lead.LastName = dataReader.GetString(5 + offset);
                  
                    if(!dataReader.IsDBNull(6 + offset))
                    lead.Phone = dataReader.GetString(6 + offset);
                  
                    if(!dataReader.IsDBNull(7 + offset))
                    lead.Address = dataReader.GetString(7 + offset);
                  
                    if(!dataReader.IsDBNull(8 + offset))
                    lead.CustomerNotes = dataReader.GetString(8 + offset);
                  
                    if(!dataReader.IsDBNull(9 + offset))
                    lead.CreatedByUserId = dataReader.GetInt32(9 + offset);
                  lead.DateCreated = dataReader.GetDateTime(10 + offset);
                  
                    if(!dataReader.IsDBNull(11 + offset))
                    lead.DateContacted = dataReader.GetDateTime(11 + offset);
                  
                    if(!dataReader.IsDBNull(12 + offset))
                    lead.IsImportant = dataReader.GetBoolean(12 + offset);
                  
                    if(!dataReader.IsDBNull(13 + offset))
                    lead.PhoneCallId = dataReader.GetInt32(13 + offset);
                  
                    if(!dataReader.IsDBNull(14 + offset))
                    lead.PhoneSmsId = dataReader.GetInt32(14 + offset);
                  
                    if(!dataReader.IsDBNull(15 + offset))
                    lead.WebFormId = dataReader.GetInt32(15 + offset);
                  
                    if(!dataReader.IsDBNull(16 + offset))
                    lead.DateLastUpdated = dataReader.GetDateTime(16 + offset);
                  

            return lead;
        }

        public static Lead Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From Lead "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(Lead lead, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", lead.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(Lead lead)
        {
        	Delete(lead, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From Lead ";

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
              
                + " LeadStatusId, "
              
                + " LeadSourceId, "
              
                + " AssignedToUser, "
              
                + " FirstName, "
              
                + " LastName, "
              
                + " Phone, "
              
                + " Address, "
              
                + " CustomerNotes, "
              
                + " CreatedByUserId, "
              
                + " DateCreated, "
              
                + " DateContacted, "
              
                + " IsImportant, "
              
                + " PhoneCallId, "
              
                + " PhoneSmsId, "
              
                + " WebFormId, "
              
                + " DateLastUpdated "
              
                + " From Lead ";

        public static List<Lead> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<Lead> rv = new List<Lead>();

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

        public static List<Lead> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<Lead> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<Lead> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Lead));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(Lead item in itemsList)
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

        public static List<Lead> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(Lead));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<Lead> itemsList = new List<Lead>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is Lead)
              		itemsList.Add(deserializedObject as Lead);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected int m_leadStatusId;
              
        protected int? m_leadSourceId;
              
        protected int? m_assignedToUser;
              
        protected String m_firstName;
              
        protected String m_lastName;
              
        protected String m_phone;
              
        protected String m_address;
              
        protected String m_customerNotes;
              
        protected int? m_createdByUserId;
              
        protected DateTime m_dateCreated;
              
        protected DateTime? m_dateContacted;
              
        protected bool m_isImportant;
              
        protected int? m_phoneCallId;
              
        protected int? m_phoneSmsId;
              
        protected int? m_webFormId;
              
        protected DateTime? m_dateLastUpdated;
              
        #endregion

        #region Constructors

        public Lead(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public Lead(
                int 
                  id,int 
                  leadStatusId,int? 
                  leadSourceId,int? 
                  assignedToUser,String 
                  firstName,String 
                  lastName,String 
                  phone,String 
                  address,String 
                  customerNotes,int? 
                  createdByUserId,DateTime 
                  dateCreated,DateTime? 
                  dateContacted,bool 
                  isImportant,int? 
                  phoneCallId,int? 
                  phoneSmsId,int? 
                  webFormId,DateTime? 
                  dateLastUpdated
                ) : this()
        {
            
        	m_id = id;
            
        	m_leadStatusId = leadStatusId;
            
        	m_leadSourceId = leadSourceId;
            
        	m_assignedToUser = assignedToUser;
            
        	m_firstName = firstName;
            
        	m_lastName = lastName;
            
        	m_phone = phone;
            
        	m_address = address;
            
        	m_customerNotes = customerNotes;
            
        	m_createdByUserId = createdByUserId;
            
        	m_dateCreated = dateCreated;
            
        	m_dateContacted = dateContacted;
            
        	m_isImportant = isImportant;
            
        	m_phoneCallId = phoneCallId;
            
        	m_phoneSmsId = phoneSmsId;
            
        	m_webFormId = webFormId;
            
        	m_dateLastUpdated = dateLastUpdated;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public int LeadStatusId
        {
        	get { return m_leadStatusId;}
            set { m_leadStatusId = value; }
        }
        
        public int? LeadSourceId
        {
        	get { return m_leadSourceId;}
            set { m_leadSourceId = value; }
        }
        
        public int? AssignedToUser
        {
        	get { return m_assignedToUser;}
            set { m_assignedToUser = value; }
        }
        
        public String FirstName
        {
        	get { return m_firstName;}
            set { m_firstName = value; }
        }
        
        public String LastName
        {
        	get { return m_lastName;}
            set { m_lastName = value; }
        }
        
        public String Phone
        {
        	get { return m_phone;}
            set { m_phone = value; }
        }
        
        public String Address
        {
        	get { return m_address;}
            set { m_address = value; }
        }
        
        public String CustomerNotes
        {
        	get { return m_customerNotes;}
            set { m_customerNotes = value; }
        }
        
        public int? CreatedByUserId
        {
        	get { return m_createdByUserId;}
            set { m_createdByUserId = value; }
        }
        
        public DateTime DateCreated
        {
        	get { return m_dateCreated;}
            set { m_dateCreated = value; }
        }
        
        public DateTime? DateContacted
        {
        	get { return m_dateContacted;}
            set { m_dateContacted = value; }
        }
        
        public bool IsImportant
        {
        	get { return m_isImportant;}
            set { m_isImportant = value; }
        }
        
        public int? PhoneCallId
        {
        	get { return m_phoneCallId;}
            set { m_phoneCallId = value; }
        }
        
        public int? PhoneSmsId
        {
        	get { return m_phoneSmsId;}
            set { m_phoneSmsId = value; }
        }
        
        public int? WebFormId
        {
        	get { return m_webFormId;}
            set { m_webFormId = value; }
        }
        
        public DateTime? DateLastUpdated
        {
        	get { return m_dateLastUpdated;}
            set { m_dateLastUpdated = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 17; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    