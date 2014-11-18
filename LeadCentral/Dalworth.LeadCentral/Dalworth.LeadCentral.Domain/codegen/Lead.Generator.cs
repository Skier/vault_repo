
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Common.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
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
        
          " CampaignId, " +
        
          " BusinessPartnerId, " +
        
          " DateCreated, " +
        
          " FirstName, " +
        
          " LastName, " +
        
          " Phone, " +
        
          " Address, " +
        
          " CustomerNotes, " +
        
          " SourceId " +
        
        ") Values (" +
        
          " ?LeadStatusId, " +
        
          " ?CampaignId, " +
        
          " ?BusinessPartnerId, " +
        
          " ?DateCreated, " +
        
          " ?FirstName, " +
        
          " ?LastName, " +
        
          " ?Phone, " +
        
          " ?Address, " +
        
          " ?CustomerNotes, " +
        
          " ?SourceId " +
        
        ")";

        public static void Insert(Lead lead, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?LeadStatusId", lead.LeadStatusId);
            
            	Database.PutParameter(dbCommand,"?CampaignId", lead.CampaignId);
            
            	Database.PutParameter(dbCommand,"?BusinessPartnerId", lead.BusinessPartnerId);
            
            	Database.PutParameter(dbCommand,"?DateCreated", lead.DateCreated);
            
            	Database.PutParameter(dbCommand,"?FirstName", lead.FirstName);
            
            	Database.PutParameter(dbCommand,"?LastName", lead.LastName);
            
            	Database.PutParameter(dbCommand,"?Phone", lead.Phone);
            
            	Database.PutParameter(dbCommand,"?Address", lead.Address);
            
            	Database.PutParameter(dbCommand,"?CustomerNotes", lead.CustomerNotes);
            
            	Database.PutParameter(dbCommand,"?SourceId", lead.SourceId);
            
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
                
                  	Database.PutParameter(dbCommand,"?CampaignId", lead.CampaignId);
                
                  	Database.PutParameter(dbCommand,"?BusinessPartnerId", lead.BusinessPartnerId);
                
                  	Database.PutParameter(dbCommand,"?DateCreated", lead.DateCreated);
                
                  	Database.PutParameter(dbCommand,"?FirstName", lead.FirstName);
                
                  	Database.PutParameter(dbCommand,"?LastName", lead.LastName);
                
                  	Database.PutParameter(dbCommand,"?Phone", lead.Phone);
                
                  	Database.PutParameter(dbCommand,"?Address", lead.Address);
                
                  	Database.PutParameter(dbCommand,"?CustomerNotes", lead.CustomerNotes);
                
                  	Database.PutParameter(dbCommand,"?SourceId", lead.SourceId);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?LeadStatusId",lead.LeadStatusId);
                
                	Database.UpdateParameter(dbCommand,"?CampaignId",lead.CampaignId);
                
                	Database.UpdateParameter(dbCommand,"?BusinessPartnerId",lead.BusinessPartnerId);
                
                	Database.UpdateParameter(dbCommand,"?DateCreated",lead.DateCreated);
                
                	Database.UpdateParameter(dbCommand,"?FirstName",lead.FirstName);
                
                	Database.UpdateParameter(dbCommand,"?LastName",lead.LastName);
                
                	Database.UpdateParameter(dbCommand,"?Phone",lead.Phone);
                
                	Database.UpdateParameter(dbCommand,"?Address",lead.Address);
                
                	Database.UpdateParameter(dbCommand,"?CustomerNotes",lead.CustomerNotes);
                
                	Database.UpdateParameter(dbCommand,"?SourceId",lead.SourceId);
                
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
          
            + " CampaignId = ?CampaignId, "
          
            + " BusinessPartnerId = ?BusinessPartnerId, "
          
            + " DateCreated = ?DateCreated, "
          
            + " FirstName = ?FirstName, "
          
            + " LastName = ?LastName, "
          
            + " Phone = ?Phone, "
          
            + " Address = ?Address, "
          
            + " CustomerNotes = ?CustomerNotes, "
          
            + " SourceId = ?SourceId "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(Lead lead, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", lead.Id);
            
            	Database.PutParameter(dbCommand,"?LeadStatusId", lead.LeadStatusId);
            
            	Database.PutParameter(dbCommand,"?CampaignId", lead.CampaignId);
            
            	Database.PutParameter(dbCommand,"?BusinessPartnerId", lead.BusinessPartnerId);
            
            	Database.PutParameter(dbCommand,"?DateCreated", lead.DateCreated);
            
            	Database.PutParameter(dbCommand,"?FirstName", lead.FirstName);
            
            	Database.PutParameter(dbCommand,"?LastName", lead.LastName);
            
            	Database.PutParameter(dbCommand,"?Phone", lead.Phone);
            
            	Database.PutParameter(dbCommand,"?Address", lead.Address);
            
            	Database.PutParameter(dbCommand,"?CustomerNotes", lead.CustomerNotes);
            
            	Database.PutParameter(dbCommand,"?SourceId", lead.SourceId);
            
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
        
          + " CampaignId, "
        
          + " BusinessPartnerId, "
        
          + " DateCreated, "
        
          + " FirstName, "
        
          + " LastName, "
        
          + " Phone, "
        
          + " Address, "
        
          + " CustomerNotes, "
        
          + " SourceId "
        
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
                    lead.CampaignId = dataReader.GetInt32(2 + offset);
                  
                    if(!dataReader.IsDBNull(3 + offset))
                    lead.BusinessPartnerId = dataReader.GetInt32(3 + offset);
                  lead.DateCreated = dataReader.GetDateTime(4 + offset);
                  
                    if(!dataReader.IsDBNull(5 + offset))
                    lead.FirstName = dataReader.GetString(5 + offset);
                  
                    if(!dataReader.IsDBNull(6 + offset))
                    lead.LastName = dataReader.GetString(6 + offset);
                  
                    if(!dataReader.IsDBNull(7 + offset))
                    lead.Phone = dataReader.GetString(7 + offset);
                  
                    if(!dataReader.IsDBNull(8 + offset))
                    lead.Address = dataReader.GetString(8 + offset);
                  
                    if(!dataReader.IsDBNull(9 + offset))
                    lead.CustomerNotes = dataReader.GetString(9 + offset);
                  lead.SourceId = dataReader.GetInt32(10 + offset);
                  

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
              
                + " CampaignId, "
              
                + " BusinessPartnerId, "
              
                + " DateCreated, "
              
                + " FirstName, "
              
                + " LastName, "
              
                + " Phone, "
              
                + " Address, "
              
                + " CustomerNotes, "
              
                + " SourceId "
              
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
              
        protected int? m_campaignId;
              
        protected int? m_businessPartnerId;
              
        protected DateTime m_dateCreated;
              
        protected String m_firstName;
              
        protected String m_lastName;
              
        protected String m_phone;
              
        protected String m_address;
              
        protected String m_customerNotes;
              
        protected int m_sourceId;
              
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
                  campaignId,int? 
                  businessPartnerId,DateTime 
                  dateCreated,String 
                  firstName,String 
                  lastName,String 
                  phone,String 
                  address,String 
                  customerNotes,int 
                  sourceId
                ) : this()
        {
            
        	m_id = id;
            
        	m_leadStatusId = leadStatusId;
            
        	m_campaignId = campaignId;
            
        	m_businessPartnerId = businessPartnerId;
            
        	m_dateCreated = dateCreated;
            
        	m_firstName = firstName;
            
        	m_lastName = lastName;
            
        	m_phone = phone;
            
        	m_address = address;
            
        	m_customerNotes = customerNotes;
            
        	m_sourceId = sourceId;
            
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
        
        public int? CampaignId
        {
        	get { return m_campaignId;}
            set { m_campaignId = value; }
        }
        
        public int? BusinessPartnerId
        {
        	get { return m_businessPartnerId;}
            set { m_businessPartnerId = value; }
        }
        
        public DateTime DateCreated
        {
        	get { return m_dateCreated;}
            set { m_dateCreated = value; }
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
        
        public int SourceId
        {
        	get { return m_sourceId;}
            set { m_sourceId = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 11; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    