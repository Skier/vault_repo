
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Common.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class Campaign : ICloneable
    {

        #region Store


        #region Save

        public static Campaign Save(Campaign campaign, IDbConnection connection)
        {
        	if (!Exists(campaign, connection))
        		Insert(campaign, connection);
        	else
        		Update(campaign, connection);
        	return campaign;
        }

        public static Campaign Save(Campaign campaign)
        {
        	if (!Exists(campaign))
        		Insert(campaign);
        	else
        		Update(campaign);
        	return campaign;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into Campaign ( " +
        
          " CampaignName, " +
        
          " DateCreated, " +
        
          " DateStart, " +
        
          " DateEnd, " +
        
          " BusinessPartnerId, " +
        
          " UserId " +
        
        ") Values (" +
        
          " ?CampaignName, " +
        
          " ?DateCreated, " +
        
          " ?DateStart, " +
        
          " ?DateEnd, " +
        
          " ?BusinessPartnerId, " +
        
          " ?UserId " +
        
        ")";

        public static void Insert(Campaign campaign, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?CampaignName", campaign.CampaignName);
            
            	Database.PutParameter(dbCommand,"?DateCreated", campaign.DateCreated);
            
            	Database.PutParameter(dbCommand,"?DateStart", campaign.DateStart);
            
            	Database.PutParameter(dbCommand,"?DateEnd", campaign.DateEnd);
            
            	Database.PutParameter(dbCommand,"?BusinessPartnerId", campaign.BusinessPartnerId);
            
            	Database.PutParameter(dbCommand,"?UserId", campaign.UserId);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		campaign.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(Campaign campaign)
        {
          	Insert(campaign, null);
        }

        public static void Insert(List<Campaign>  campaignList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(Campaign campaign in  campaignList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?CampaignName", campaign.CampaignName);
                
                  	Database.PutParameter(dbCommand,"?DateCreated", campaign.DateCreated);
                
                  	Database.PutParameter(dbCommand,"?DateStart", campaign.DateStart);
                
                  	Database.PutParameter(dbCommand,"?DateEnd", campaign.DateEnd);
                
                  	Database.PutParameter(dbCommand,"?BusinessPartnerId", campaign.BusinessPartnerId);
                
                  	Database.PutParameter(dbCommand,"?UserId", campaign.UserId);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?CampaignName",campaign.CampaignName);
                
                	Database.UpdateParameter(dbCommand,"?DateCreated",campaign.DateCreated);
                
                	Database.UpdateParameter(dbCommand,"?DateStart",campaign.DateStart);
                
                	Database.UpdateParameter(dbCommand,"?DateEnd",campaign.DateEnd);
                
                	Database.UpdateParameter(dbCommand,"?BusinessPartnerId",campaign.BusinessPartnerId);
                
                	Database.UpdateParameter(dbCommand,"?UserId",campaign.UserId);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	campaign.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<Campaign>  campaignList)
        {
        	Insert(campaignList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update Campaign Set "
          
            + " CampaignName = ?CampaignName, "
          
            + " DateCreated = ?DateCreated, "
          
            + " DateStart = ?DateStart, "
          
            + " DateEnd = ?DateEnd, "
          
            + " BusinessPartnerId = ?BusinessPartnerId, "
          
            + " UserId = ?UserId "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(Campaign campaign, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", campaign.Id);
            
            	Database.PutParameter(dbCommand,"?CampaignName", campaign.CampaignName);
            
            	Database.PutParameter(dbCommand,"?DateCreated", campaign.DateCreated);
            
            	Database.PutParameter(dbCommand,"?DateStart", campaign.DateStart);
            
            	Database.PutParameter(dbCommand,"?DateEnd", campaign.DateEnd);
            
            	Database.PutParameter(dbCommand,"?BusinessPartnerId", campaign.BusinessPartnerId);
            
            	Database.PutParameter(dbCommand,"?UserId", campaign.UserId);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(Campaign campaign)
        {
          	Update(campaign, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " CampaignName, "
        
          + " DateCreated, "
        
          + " DateStart, "
        
          + " DateEnd, "
        
          + " BusinessPartnerId, "
        
          + " UserId "
        
          + " From Campaign "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static Campaign FindByPrimaryKey(
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

            throw new DataNotFoundException("Campaign not found, search by primary key");
        }

        public static Campaign FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(Campaign campaign, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",campaign.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(Campaign campaign)
        {
        	return Exists(campaign, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from Campaign limit 1";

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

        public static Campaign Load(IDataReader dataReader, int offset)
        {
              Campaign campaign = new Campaign();

              campaign.Id = dataReader.GetInt32(0 + offset);
                  campaign.CampaignName = dataReader.GetString(1 + offset);
                  campaign.DateCreated = dataReader.GetDateTime(2 + offset);
                  campaign.DateStart = dataReader.GetDateTime(3 + offset);
                  
                    if(!dataReader.IsDBNull(4 + offset))
                    campaign.DateEnd = dataReader.GetDateTime(4 + offset);
                  
                    if(!dataReader.IsDBNull(5 + offset))
                    campaign.BusinessPartnerId = dataReader.GetInt32(5 + offset);
                  campaign.UserId = dataReader.GetInt32(6 + offset);
                  

            return campaign;
        }

        public static Campaign Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From Campaign "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(Campaign campaign, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", campaign.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(Campaign campaign)
        {
        	Delete(campaign, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From Campaign ";

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
              
                + " CampaignName, "
              
                + " DateCreated, "
              
                + " DateStart, "
              
                + " DateEnd, "
              
                + " BusinessPartnerId, "
              
                + " UserId "
              
                + " From Campaign ";

        public static List<Campaign> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<Campaign> rv = new List<Campaign>();

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

        public static List<Campaign> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<Campaign> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<Campaign> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Campaign));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(Campaign item in itemsList)
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

        public static List<Campaign> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(Campaign));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<Campaign> itemsList = new List<Campaign>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is Campaign)
              		itemsList.Add(deserializedObject as Campaign);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected String m_campaignName;
              
        protected DateTime m_dateCreated;
              
        protected DateTime m_dateStart;
              
        protected DateTime? m_dateEnd;
              
        protected int? m_businessPartnerId;
              
        protected int m_userId;
              
        #endregion

        #region Constructors

        public Campaign(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public Campaign(
                int 
                  id,String 
                  campaignName,DateTime 
                  dateCreated,DateTime 
                  dateStart,DateTime? 
                  dateEnd,int? 
                  businessPartnerId,int 
                  userId
                ) : this()
        {
            
        	m_id = id;
            
        	m_campaignName = campaignName;
            
        	m_dateCreated = dateCreated;
            
        	m_dateStart = dateStart;
            
        	m_dateEnd = dateEnd;
            
        	m_businessPartnerId = businessPartnerId;
            
        	m_userId = userId;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public String CampaignName
        {
        	get { return m_campaignName;}
            set { m_campaignName = value; }
        }
        
        public DateTime DateCreated
        {
        	get { return m_dateCreated;}
            set { m_dateCreated = value; }
        }
        
        public DateTime DateStart
        {
        	get { return m_dateStart;}
            set { m_dateStart = value; }
        }
        
        public DateTime? DateEnd
        {
        	get { return m_dateEnd;}
            set { m_dateEnd = value; }
        }
        
        public int? BusinessPartnerId
        {
        	get { return m_businessPartnerId;}
            set { m_businessPartnerId = value; }
        }
        
        public int UserId
        {
        	get { return m_userId;}
            set { m_userId = value; }
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

    