
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Common.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class CompaignTrackingPhone : ICloneable
    {

        #region Store


        #region Save

        public static CompaignTrackingPhone Save(CompaignTrackingPhone compaignTrackingPhone, IDbConnection connection)
        {
        	if (!Exists(compaignTrackingPhone, connection))
        		Insert(compaignTrackingPhone, connection);
        	else
        		Update(compaignTrackingPhone, connection);
        	return compaignTrackingPhone;
        }

        public static CompaignTrackingPhone Save(CompaignTrackingPhone compaignTrackingPhone)
        {
        	if (!Exists(compaignTrackingPhone))
        		Insert(compaignTrackingPhone);
        	else
        		Update(compaignTrackingPhone);
        	return compaignTrackingPhone;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into CompaignTrackingPhone ( " +
        
          " CampaignId, " +
        
          " TrackingPhoneId, " +
        
          " DateAssigned, " +
        
          " DateReleased " +
        
        ") Values (" +
        
          " ?CampaignId, " +
        
          " ?TrackingPhoneId, " +
        
          " ?DateAssigned, " +
        
          " ?DateReleased " +
        
        ")";

        public static void Insert(CompaignTrackingPhone compaignTrackingPhone, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?CampaignId", compaignTrackingPhone.CampaignId);
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneId", compaignTrackingPhone.TrackingPhoneId);
            
            	Database.PutParameter(dbCommand,"?DateAssigned", compaignTrackingPhone.DateAssigned);
            
            	Database.PutParameter(dbCommand,"?DateReleased", compaignTrackingPhone.DateReleased);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		compaignTrackingPhone.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(CompaignTrackingPhone compaignTrackingPhone)
        {
          	Insert(compaignTrackingPhone, null);
        }

        public static void Insert(List<CompaignTrackingPhone>  compaignTrackingPhoneList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(CompaignTrackingPhone compaignTrackingPhone in  compaignTrackingPhoneList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?CampaignId", compaignTrackingPhone.CampaignId);
                
                  	Database.PutParameter(dbCommand,"?TrackingPhoneId", compaignTrackingPhone.TrackingPhoneId);
                
                  	Database.PutParameter(dbCommand,"?DateAssigned", compaignTrackingPhone.DateAssigned);
                
                  	Database.PutParameter(dbCommand,"?DateReleased", compaignTrackingPhone.DateReleased);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?CampaignId",compaignTrackingPhone.CampaignId);
                
                	Database.UpdateParameter(dbCommand,"?TrackingPhoneId",compaignTrackingPhone.TrackingPhoneId);
                
                	Database.UpdateParameter(dbCommand,"?DateAssigned",compaignTrackingPhone.DateAssigned);
                
                	Database.UpdateParameter(dbCommand,"?DateReleased",compaignTrackingPhone.DateReleased);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	compaignTrackingPhone.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<CompaignTrackingPhone>  compaignTrackingPhoneList)
        {
        	Insert(compaignTrackingPhoneList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update CompaignTrackingPhone Set "
          
            + " CampaignId = ?CampaignId, "
          
            + " TrackingPhoneId = ?TrackingPhoneId, "
          
            + " DateAssigned = ?DateAssigned, "
          
            + " DateReleased = ?DateReleased "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(CompaignTrackingPhone compaignTrackingPhone, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", compaignTrackingPhone.Id);
            
            	Database.PutParameter(dbCommand,"?CampaignId", compaignTrackingPhone.CampaignId);
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneId", compaignTrackingPhone.TrackingPhoneId);
            
            	Database.PutParameter(dbCommand,"?DateAssigned", compaignTrackingPhone.DateAssigned);
            
            	Database.PutParameter(dbCommand,"?DateReleased", compaignTrackingPhone.DateReleased);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(CompaignTrackingPhone compaignTrackingPhone)
        {
          	Update(compaignTrackingPhone, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " CampaignId, "
        
          + " TrackingPhoneId, "
        
          + " DateAssigned, "
        
          + " DateReleased "
        
          + " From CompaignTrackingPhone "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static CompaignTrackingPhone FindByPrimaryKey(
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

            throw new DataNotFoundException("CompaignTrackingPhone not found, search by primary key");
        }

        public static CompaignTrackingPhone FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(CompaignTrackingPhone compaignTrackingPhone, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",compaignTrackingPhone.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(CompaignTrackingPhone compaignTrackingPhone)
        {
        	return Exists(compaignTrackingPhone, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from CompaignTrackingPhone limit 1";

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

        public static CompaignTrackingPhone Load(IDataReader dataReader, int offset)
        {
              CompaignTrackingPhone compaignTrackingPhone = new CompaignTrackingPhone();

              compaignTrackingPhone.Id = dataReader.GetInt32(0 + offset);
                  compaignTrackingPhone.CampaignId = dataReader.GetInt32(1 + offset);
                  compaignTrackingPhone.TrackingPhoneId = dataReader.GetInt32(2 + offset);
                  compaignTrackingPhone.DateAssigned = dataReader.GetDateTime(3 + offset);
                  
                    if(!dataReader.IsDBNull(4 + offset))
                    compaignTrackingPhone.DateReleased = dataReader.GetDateTime(4 + offset);
                  

            return compaignTrackingPhone;
        }

        public static CompaignTrackingPhone Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From CompaignTrackingPhone "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(CompaignTrackingPhone compaignTrackingPhone, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", compaignTrackingPhone.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(CompaignTrackingPhone compaignTrackingPhone)
        {
        	Delete(compaignTrackingPhone, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From CompaignTrackingPhone ";

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
              
                + " CampaignId, "
              
                + " TrackingPhoneId, "
              
                + " DateAssigned, "
              
                + " DateReleased "
              
                + " From CompaignTrackingPhone ";

        public static List<CompaignTrackingPhone> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<CompaignTrackingPhone> rv = new List<CompaignTrackingPhone>();

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

        public static List<CompaignTrackingPhone> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<CompaignTrackingPhone> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<CompaignTrackingPhone> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CompaignTrackingPhone));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(CompaignTrackingPhone item in itemsList)
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

        public static List<CompaignTrackingPhone> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(CompaignTrackingPhone));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<CompaignTrackingPhone> itemsList = new List<CompaignTrackingPhone>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is CompaignTrackingPhone)
              		itemsList.Add(deserializedObject as CompaignTrackingPhone);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected int m_campaignId;
              
        protected int m_trackingPhoneId;
              
        protected DateTime m_dateAssigned;
              
        protected DateTime? m_dateReleased;
              
        #endregion

        #region Constructors

        public CompaignTrackingPhone(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public CompaignTrackingPhone(
                int 
                  id,int 
                  campaignId,int 
                  trackingPhoneId,DateTime 
                  dateAssigned,DateTime? 
                  dateReleased
                ) : this()
        {
            
        	m_id = id;
            
        	m_campaignId = campaignId;
            
        	m_trackingPhoneId = trackingPhoneId;
            
        	m_dateAssigned = dateAssigned;
            
        	m_dateReleased = dateReleased;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public int CampaignId
        {
        	get { return m_campaignId;}
            set { m_campaignId = value; }
        }
        
        public int TrackingPhoneId
        {
        	get { return m_trackingPhoneId;}
            set { m_trackingPhoneId = value; }
        }
        
        public DateTime DateAssigned
        {
        	get { return m_dateAssigned;}
            set { m_dateAssigned = value; }
        }
        
        public DateTime? DateReleased
        {
        	get { return m_dateReleased;}
            set { m_dateReleased = value; }
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

    