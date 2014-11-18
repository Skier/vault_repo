
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Common.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class ApplicationStatus : ICloneable
    {

        #region Store


        #region Save

        public static ApplicationStatus Save(ApplicationStatus applicationStatus, IDbConnection connection)
        {
        	if (!Exists(applicationStatus, connection))
        		Insert(applicationStatus, connection);
        	else
        		Update(applicationStatus, connection);
        	return applicationStatus;
        }

        public static ApplicationStatus Save(ApplicationStatus applicationStatus)
        {
        	if (!Exists(applicationStatus))
        		Insert(applicationStatus);
        	else
        		Update(applicationStatus);
        	return applicationStatus;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into ApplicationStatus ( " +
        
          " ServmanCustomerId, " +
        
          " BillingStatus, " +
        
          " LastPaymentDate " +
        
        ") Values (" +
        
          " ?ServmanCustomerId, " +
        
          " ?BillingStatus, " +
        
          " ?LastPaymentDate " +
        
        ")";

        public static void Insert(ApplicationStatus applicationStatus, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?ServmanCustomerId", applicationStatus.ServmanCustomerId);
            
            	Database.PutParameter(dbCommand,"?BillingStatus", applicationStatus.BillingStatus);
            
            	Database.PutParameter(dbCommand,"?LastPaymentDate", applicationStatus.LastPaymentDate);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		applicationStatus.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(ApplicationStatus applicationStatus)
        {
          	Insert(applicationStatus, null);
        }

        public static void Insert(List<ApplicationStatus>  applicationStatusList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(ApplicationStatus applicationStatus in  applicationStatusList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?ServmanCustomerId", applicationStatus.ServmanCustomerId);
                
                  	Database.PutParameter(dbCommand,"?BillingStatus", applicationStatus.BillingStatus);
                
                  	Database.PutParameter(dbCommand,"?LastPaymentDate", applicationStatus.LastPaymentDate);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?ServmanCustomerId",applicationStatus.ServmanCustomerId);
                
                	Database.UpdateParameter(dbCommand,"?BillingStatus",applicationStatus.BillingStatus);
                
                	Database.UpdateParameter(dbCommand,"?LastPaymentDate",applicationStatus.LastPaymentDate);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	applicationStatus.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<ApplicationStatus>  applicationStatusList)
        {
        	Insert(applicationStatusList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update ApplicationStatus Set "
          
            + " ServmanCustomerId = ?ServmanCustomerId, "
          
            + " BillingStatus = ?BillingStatus, "
          
            + " LastPaymentDate = ?LastPaymentDate "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(ApplicationStatus applicationStatus, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", applicationStatus.Id);
            
            	Database.PutParameter(dbCommand,"?ServmanCustomerId", applicationStatus.ServmanCustomerId);
            
            	Database.PutParameter(dbCommand,"?BillingStatus", applicationStatus.BillingStatus);
            
            	Database.PutParameter(dbCommand,"?LastPaymentDate", applicationStatus.LastPaymentDate);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(ApplicationStatus applicationStatus)
        {
          	Update(applicationStatus, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " ServmanCustomerId, "
        
          + " BillingStatus, "
        
          + " LastPaymentDate "
        
          + " From ApplicationStatus "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static ApplicationStatus FindByPrimaryKey(
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

            throw new DataNotFoundException("ApplicationStatus not found, search by primary key");
        }

        public static ApplicationStatus FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(ApplicationStatus applicationStatus, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",applicationStatus.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(ApplicationStatus applicationStatus)
        {
        	return Exists(applicationStatus, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from ApplicationStatus limit 1";

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

        public static ApplicationStatus Load(IDataReader dataReader, int offset)
        {
              ApplicationStatus applicationStatus = new ApplicationStatus();

              applicationStatus.Id = dataReader.GetInt32(0 + offset);
                  applicationStatus.ServmanCustomerId = dataReader.GetInt32(1 + offset);
                  applicationStatus.BillingStatus = dataReader.GetString(2 + offset);
                  applicationStatus.LastPaymentDate = dataReader.GetDateTime(3 + offset);
                  

            return applicationStatus;
        }

        public static ApplicationStatus Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From ApplicationStatus "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(ApplicationStatus applicationStatus, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", applicationStatus.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(ApplicationStatus applicationStatus)
        {
        	Delete(applicationStatus, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From ApplicationStatus ";

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
              
                + " ServmanCustomerId, "
              
                + " BillingStatus, "
              
                + " LastPaymentDate "
              
                + " From ApplicationStatus ";

        public static List<ApplicationStatus> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<ApplicationStatus> rv = new List<ApplicationStatus>();

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

        public static List<ApplicationStatus> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<ApplicationStatus> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<ApplicationStatus> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ApplicationStatus));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(ApplicationStatus item in itemsList)
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

        public static List<ApplicationStatus> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(ApplicationStatus));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<ApplicationStatus> itemsList = new List<ApplicationStatus>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is ApplicationStatus)
              		itemsList.Add(deserializedObject as ApplicationStatus);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected int m_servmanCustomerId;
              
        protected String m_billingStatus;
              
        protected DateTime m_lastPaymentDate;
              
        #endregion

        #region Constructors

        public ApplicationStatus(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public ApplicationStatus(
                int 
                  id,int 
                  servmanCustomerId,String 
                  billingStatus,DateTime 
                  lastPaymentDate
                ) : this()
        {
            
        	m_id = id;
            
        	m_servmanCustomerId = servmanCustomerId;
            
        	m_billingStatus = billingStatus;
            
        	m_lastPaymentDate = lastPaymentDate;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public int ServmanCustomerId
        {
        	get { return m_servmanCustomerId;}
            set { m_servmanCustomerId = value; }
        }
        
        public String BillingStatus
        {
        	get { return m_billingStatus;}
            set { m_billingStatus = value; }
        }
        
        public DateTime LastPaymentDate
        {
        	get { return m_lastPaymentDate;}
            set { m_lastPaymentDate = value; }
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

    