
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Common.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class TrackingPhoneRotation : ICloneable
    {

        #region Store


        #region Save

        public static TrackingPhoneRotation Save(TrackingPhoneRotation trackingPhoneRotation, IDbConnection connection)
        {
        	if (!Exists(trackingPhoneRotation, connection))
        		Insert(trackingPhoneRotation, connection);
        	else
        		Update(trackingPhoneRotation, connection);
        	return trackingPhoneRotation;
        }

        public static TrackingPhoneRotation Save(TrackingPhoneRotation trackingPhoneRotation)
        {
        	if (!Exists(trackingPhoneRotation))
        		Insert(trackingPhoneRotation);
        	else
        		Update(trackingPhoneRotation);
        	return trackingPhoneRotation;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into TrackingPhoneRotation ( " +
        
          " CampaignId, " +
        
          " TrackingPhoneId, " +
        
          " ShowedPhoneNumber, " +
        
          " TimeRotation, " +
        
          " SessionUid, " +
        
          " UserHosAddress, " +
        
          " RotationPageUri, " +
        
          " ReferralUri " +
        
        ") Values (" +
        
          " ?CampaignId, " +
        
          " ?TrackingPhoneId, " +
        
          " ?ShowedPhoneNumber, " +
        
          " ?TimeRotation, " +
        
          " ?SessionUid, " +
        
          " ?UserHosAddress, " +
        
          " ?RotationPageUri, " +
        
          " ?ReferralUri " +
        
        ")";

        public static void Insert(TrackingPhoneRotation trackingPhoneRotation, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?CampaignId", trackingPhoneRotation.CampaignId);
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneId", trackingPhoneRotation.TrackingPhoneId);
            
            	Database.PutParameter(dbCommand,"?ShowedPhoneNumber", trackingPhoneRotation.ShowedPhoneNumber);
            
            	Database.PutParameter(dbCommand,"?TimeRotation", trackingPhoneRotation.TimeRotation);
            
            	Database.PutParameter(dbCommand,"?SessionUid", trackingPhoneRotation.SessionUid);
            
            	Database.PutParameter(dbCommand,"?UserHosAddress", trackingPhoneRotation.UserHosAddress);
            
            	Database.PutParameter(dbCommand,"?RotationPageUri", trackingPhoneRotation.RotationPageUri);
            
            	Database.PutParameter(dbCommand,"?ReferralUri", trackingPhoneRotation.ReferralUri);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		trackingPhoneRotation.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(TrackingPhoneRotation trackingPhoneRotation)
        {
          	Insert(trackingPhoneRotation, null);
        }

        public static void Insert(List<TrackingPhoneRotation>  trackingPhoneRotationList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(TrackingPhoneRotation trackingPhoneRotation in  trackingPhoneRotationList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?CampaignId", trackingPhoneRotation.CampaignId);
                
                  	Database.PutParameter(dbCommand,"?TrackingPhoneId", trackingPhoneRotation.TrackingPhoneId);
                
                  	Database.PutParameter(dbCommand,"?ShowedPhoneNumber", trackingPhoneRotation.ShowedPhoneNumber);
                
                  	Database.PutParameter(dbCommand,"?TimeRotation", trackingPhoneRotation.TimeRotation);
                
                  	Database.PutParameter(dbCommand,"?SessionUid", trackingPhoneRotation.SessionUid);
                
                  	Database.PutParameter(dbCommand,"?UserHosAddress", trackingPhoneRotation.UserHosAddress);
                
                  	Database.PutParameter(dbCommand,"?RotationPageUri", trackingPhoneRotation.RotationPageUri);
                
                  	Database.PutParameter(dbCommand,"?ReferralUri", trackingPhoneRotation.ReferralUri);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?CampaignId",trackingPhoneRotation.CampaignId);
                
                	Database.UpdateParameter(dbCommand,"?TrackingPhoneId",trackingPhoneRotation.TrackingPhoneId);
                
                	Database.UpdateParameter(dbCommand,"?ShowedPhoneNumber",trackingPhoneRotation.ShowedPhoneNumber);
                
                	Database.UpdateParameter(dbCommand,"?TimeRotation",trackingPhoneRotation.TimeRotation);
                
                	Database.UpdateParameter(dbCommand,"?SessionUid",trackingPhoneRotation.SessionUid);
                
                	Database.UpdateParameter(dbCommand,"?UserHosAddress",trackingPhoneRotation.UserHosAddress);
                
                	Database.UpdateParameter(dbCommand,"?RotationPageUri",trackingPhoneRotation.RotationPageUri);
                
                	Database.UpdateParameter(dbCommand,"?ReferralUri",trackingPhoneRotation.ReferralUri);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	trackingPhoneRotation.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<TrackingPhoneRotation>  trackingPhoneRotationList)
        {
        	Insert(trackingPhoneRotationList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update TrackingPhoneRotation Set "
          
            + " CampaignId = ?CampaignId, "
          
            + " TrackingPhoneId = ?TrackingPhoneId, "
          
            + " ShowedPhoneNumber = ?ShowedPhoneNumber, "
          
            + " TimeRotation = ?TimeRotation, "
          
            + " SessionUid = ?SessionUid, "
          
            + " UserHosAddress = ?UserHosAddress, "
          
            + " RotationPageUri = ?RotationPageUri, "
          
            + " ReferralUri = ?ReferralUri "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(TrackingPhoneRotation trackingPhoneRotation, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", trackingPhoneRotation.Id);
            
            	Database.PutParameter(dbCommand,"?CampaignId", trackingPhoneRotation.CampaignId);
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneId", trackingPhoneRotation.TrackingPhoneId);
            
            	Database.PutParameter(dbCommand,"?ShowedPhoneNumber", trackingPhoneRotation.ShowedPhoneNumber);
            
            	Database.PutParameter(dbCommand,"?TimeRotation", trackingPhoneRotation.TimeRotation);
            
            	Database.PutParameter(dbCommand,"?SessionUid", trackingPhoneRotation.SessionUid);
            
            	Database.PutParameter(dbCommand,"?UserHosAddress", trackingPhoneRotation.UserHosAddress);
            
            	Database.PutParameter(dbCommand,"?RotationPageUri", trackingPhoneRotation.RotationPageUri);
            
            	Database.PutParameter(dbCommand,"?ReferralUri", trackingPhoneRotation.ReferralUri);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(TrackingPhoneRotation trackingPhoneRotation)
        {
          	Update(trackingPhoneRotation, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " CampaignId, "
        
          + " TrackingPhoneId, "
        
          + " ShowedPhoneNumber, "
        
          + " TimeRotation, "
        
          + " SessionUid, "
        
          + " UserHosAddress, "
        
          + " RotationPageUri, "
        
          + " ReferralUri "
        
          + " From TrackingPhoneRotation "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static TrackingPhoneRotation FindByPrimaryKey(
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

            throw new DataNotFoundException("TrackingPhoneRotation not found, search by primary key");
        }

        public static TrackingPhoneRotation FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(TrackingPhoneRotation trackingPhoneRotation, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",trackingPhoneRotation.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(TrackingPhoneRotation trackingPhoneRotation)
        {
        	return Exists(trackingPhoneRotation, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from TrackingPhoneRotation limit 1";

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

        public static TrackingPhoneRotation Load(IDataReader dataReader, int offset)
        {
              TrackingPhoneRotation trackingPhoneRotation = new TrackingPhoneRotation();

              trackingPhoneRotation.Id = dataReader.GetInt32(0 + offset);
                  trackingPhoneRotation.CampaignId = dataReader.GetInt32(1 + offset);
                  trackingPhoneRotation.TrackingPhoneId = dataReader.GetInt32(2 + offset);
                  trackingPhoneRotation.ShowedPhoneNumber = dataReader.GetString(3 + offset);
                  trackingPhoneRotation.TimeRotation = dataReader.GetDateTime(4 + offset);
                  trackingPhoneRotation.SessionUid = dataReader.GetString(5 + offset);
                  trackingPhoneRotation.UserHosAddress = dataReader.GetString(6 + offset);
                  trackingPhoneRotation.RotationPageUri = dataReader.GetString(7 + offset);
                  trackingPhoneRotation.ReferralUri = dataReader.GetString(8 + offset);
                  

            return trackingPhoneRotation;
        }

        public static TrackingPhoneRotation Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From TrackingPhoneRotation "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(TrackingPhoneRotation trackingPhoneRotation, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", trackingPhoneRotation.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(TrackingPhoneRotation trackingPhoneRotation)
        {
        	Delete(trackingPhoneRotation, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From TrackingPhoneRotation ";

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
              
                + " ShowedPhoneNumber, "
              
                + " TimeRotation, "
              
                + " SessionUid, "
              
                + " UserHosAddress, "
              
                + " RotationPageUri, "
              
                + " ReferralUri "
              
                + " From TrackingPhoneRotation ";

        public static List<TrackingPhoneRotation> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<TrackingPhoneRotation> rv = new List<TrackingPhoneRotation>();

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

        public static List<TrackingPhoneRotation> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<TrackingPhoneRotation> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<TrackingPhoneRotation> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TrackingPhoneRotation));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(TrackingPhoneRotation item in itemsList)
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

        public static List<TrackingPhoneRotation> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(TrackingPhoneRotation));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<TrackingPhoneRotation> itemsList = new List<TrackingPhoneRotation>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is TrackingPhoneRotation)
              		itemsList.Add(deserializedObject as TrackingPhoneRotation);
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
              
        protected String m_showedPhoneNumber;
              
        protected DateTime m_timeRotation;
              
        protected String m_sessionUid;
              
        protected String m_userHosAddress;
              
        protected String m_rotationPageUri;
              
        protected String m_referralUri;
              
        #endregion

        #region Constructors

        public TrackingPhoneRotation(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public TrackingPhoneRotation(
                int 
                  id,int 
                  campaignId,int 
                  trackingPhoneId,String 
                  showedPhoneNumber,DateTime 
                  timeRotation,String 
                  sessionUid,String 
                  userHosAddress,String 
                  rotationPageUri,String 
                  referralUri
                ) : this()
        {
            
        	m_id = id;
            
        	m_campaignId = campaignId;
            
        	m_trackingPhoneId = trackingPhoneId;
            
        	m_showedPhoneNumber = showedPhoneNumber;
            
        	m_timeRotation = timeRotation;
            
        	m_sessionUid = sessionUid;
            
        	m_userHosAddress = userHosAddress;
            
        	m_rotationPageUri = rotationPageUri;
            
        	m_referralUri = referralUri;
            
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
        
        public String ShowedPhoneNumber
        {
        	get { return m_showedPhoneNumber;}
            set { m_showedPhoneNumber = value; }
        }
        
        public DateTime TimeRotation
        {
        	get { return m_timeRotation;}
            set { m_timeRotation = value; }
        }
        
        public String SessionUid
        {
        	get { return m_sessionUid;}
            set { m_sessionUid = value; }
        }
        
        public String UserHosAddress
        {
        	get { return m_userHosAddress;}
            set { m_userHosAddress = value; }
        }
        
        public String RotationPageUri
        {
        	get { return m_rotationPageUri;}
            set { m_rotationPageUri = value; }
        }
        
        public String ReferralUri
        {
        	get { return m_referralUri;}
            set { m_referralUri = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 9; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    