
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Common.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class Customer : ICloneable
    {

        #region Store


        #region Save

        public static Customer Save(Customer customer, IDbConnection connection)
        {
        	if (!Exists(customer, connection))
        		Insert(customer, connection);
        	else
        		Update(customer, connection);
        	return customer;
        }

        public static Customer Save(Customer customer)
        {
        	if (!Exists(customer))
        		Insert(customer);
        	else
        		Update(customer);
        	return customer;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into Customer ( " +
        
          " CreationDate, " +
        
          " RealmId, " +
        
          " AppDbId, " +
        
          " IsQBO, " +
        
          " DbName, " +
        
          " DbLogin, " +
        
          " DbPassword, " +
        
          " Name, " +
        
          " ContactPerson, " +
        
          " Email, " +
        
          " Phone, " +
        
          " Description, " +
        
          " IsTrackingPhonesInited, " +
        
          " IsCampaignsInited, " +
        
          " IsOAuthInited, " +
        
          " IsCompanyProfileInited, " +
        
          " BillingStatus, " +
        
          " LastPaymentDate " +
        
        ") Values (" +
        
          " ?CreationDate, " +
        
          " ?RealmId, " +
        
          " ?AppDbId, " +
        
          " ?IsQBO, " +
        
          " ?DbName, " +
        
          " ?DbLogin, " +
        
          " ?DbPassword, " +
        
          " ?Name, " +
        
          " ?ContactPerson, " +
        
          " ?Email, " +
        
          " ?Phone, " +
        
          " ?Description, " +
        
          " ?IsTrackingPhonesInited, " +
        
          " ?IsCampaignsInited, " +
        
          " ?IsOAuthInited, " +
        
          " ?IsCompanyProfileInited, " +
        
          " ?BillingStatus, " +
        
          " ?LastPaymentDate " +
        
        ")";

        public static void Insert(Customer customer, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?CreationDate", customer.CreationDate);
            
            	Database.PutParameter(dbCommand,"?RealmId", customer.RealmId);
            
            	Database.PutParameter(dbCommand,"?AppDbId", customer.AppDbId);
            
            	Database.PutParameter(dbCommand,"?IsQBO", customer.IsQBO);
            
            	Database.PutParameter(dbCommand,"?DbName", customer.DbName);
            
            	Database.PutParameter(dbCommand,"?DbLogin", customer.DbLogin);
            
            	Database.PutParameter(dbCommand,"?DbPassword", customer.DbPassword);
            
            	Database.PutParameter(dbCommand,"?Name", customer.Name);
            
            	Database.PutParameter(dbCommand,"?ContactPerson", customer.ContactPerson);
            
            	Database.PutParameter(dbCommand,"?Email", customer.Email);
            
            	Database.PutParameter(dbCommand,"?Phone", customer.Phone);
            
            	Database.PutParameter(dbCommand,"?Description", customer.Description);
            
            	Database.PutParameter(dbCommand,"?IsTrackingPhonesInited", customer.IsTrackingPhonesInited);
            
            	Database.PutParameter(dbCommand,"?IsCampaignsInited", customer.IsCampaignsInited);
            
            	Database.PutParameter(dbCommand,"?IsOAuthInited", customer.IsOAuthInited);
            
            	Database.PutParameter(dbCommand,"?IsCompanyProfileInited", customer.IsCompanyProfileInited);
            
            	Database.PutParameter(dbCommand,"?BillingStatus", customer.BillingStatus);
            
            	Database.PutParameter(dbCommand,"?LastPaymentDate", customer.LastPaymentDate);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		customer.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(Customer customer)
        {
          	Insert(customer, null);
        }

        public static void Insert(List<Customer>  customerList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(Customer customer in  customerList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?CreationDate", customer.CreationDate);
                
                  	Database.PutParameter(dbCommand,"?RealmId", customer.RealmId);
                
                  	Database.PutParameter(dbCommand,"?AppDbId", customer.AppDbId);
                
                  	Database.PutParameter(dbCommand,"?IsQBO", customer.IsQBO);
                
                  	Database.PutParameter(dbCommand,"?DbName", customer.DbName);
                
                  	Database.PutParameter(dbCommand,"?DbLogin", customer.DbLogin);
                
                  	Database.PutParameter(dbCommand,"?DbPassword", customer.DbPassword);
                
                  	Database.PutParameter(dbCommand,"?Name", customer.Name);
                
                  	Database.PutParameter(dbCommand,"?ContactPerson", customer.ContactPerson);
                
                  	Database.PutParameter(dbCommand,"?Email", customer.Email);
                
                  	Database.PutParameter(dbCommand,"?Phone", customer.Phone);
                
                  	Database.PutParameter(dbCommand,"?Description", customer.Description);
                
                  	Database.PutParameter(dbCommand,"?IsTrackingPhonesInited", customer.IsTrackingPhonesInited);
                
                  	Database.PutParameter(dbCommand,"?IsCampaignsInited", customer.IsCampaignsInited);
                
                  	Database.PutParameter(dbCommand,"?IsOAuthInited", customer.IsOAuthInited);
                
                  	Database.PutParameter(dbCommand,"?IsCompanyProfileInited", customer.IsCompanyProfileInited);
                
                  	Database.PutParameter(dbCommand,"?BillingStatus", customer.BillingStatus);
                
                  	Database.PutParameter(dbCommand,"?LastPaymentDate", customer.LastPaymentDate);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?CreationDate",customer.CreationDate);
                
                	Database.UpdateParameter(dbCommand,"?RealmId",customer.RealmId);
                
                	Database.UpdateParameter(dbCommand,"?AppDbId",customer.AppDbId);
                
                	Database.UpdateParameter(dbCommand,"?IsQBO",customer.IsQBO);
                
                	Database.UpdateParameter(dbCommand,"?DbName",customer.DbName);
                
                	Database.UpdateParameter(dbCommand,"?DbLogin",customer.DbLogin);
                
                	Database.UpdateParameter(dbCommand,"?DbPassword",customer.DbPassword);
                
                	Database.UpdateParameter(dbCommand,"?Name",customer.Name);
                
                	Database.UpdateParameter(dbCommand,"?ContactPerson",customer.ContactPerson);
                
                	Database.UpdateParameter(dbCommand,"?Email",customer.Email);
                
                	Database.UpdateParameter(dbCommand,"?Phone",customer.Phone);
                
                	Database.UpdateParameter(dbCommand,"?Description",customer.Description);
                
                	Database.UpdateParameter(dbCommand,"?IsTrackingPhonesInited",customer.IsTrackingPhonesInited);
                
                	Database.UpdateParameter(dbCommand,"?IsCampaignsInited",customer.IsCampaignsInited);
                
                	Database.UpdateParameter(dbCommand,"?IsOAuthInited",customer.IsOAuthInited);
                
                	Database.UpdateParameter(dbCommand,"?IsCompanyProfileInited",customer.IsCompanyProfileInited);
                
                	Database.UpdateParameter(dbCommand,"?BillingStatus",customer.BillingStatus);
                
                	Database.UpdateParameter(dbCommand,"?LastPaymentDate",customer.LastPaymentDate);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	customer.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<Customer>  customerList)
        {
        	Insert(customerList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update Customer Set "
          
            + " CreationDate = ?CreationDate, "
          
            + " RealmId = ?RealmId, "
          
            + " AppDbId = ?AppDbId, "
          
            + " IsQBO = ?IsQBO, "
          
            + " DbName = ?DbName, "
          
            + " DbLogin = ?DbLogin, "
          
            + " DbPassword = ?DbPassword, "
          
            + " Name = ?Name, "
          
            + " ContactPerson = ?ContactPerson, "
          
            + " Email = ?Email, "
          
            + " Phone = ?Phone, "
          
            + " Description = ?Description, "
          
            + " IsTrackingPhonesInited = ?IsTrackingPhonesInited, "
          
            + " IsCampaignsInited = ?IsCampaignsInited, "
          
            + " IsOAuthInited = ?IsOAuthInited, "
          
            + " IsCompanyProfileInited = ?IsCompanyProfileInited, "
          
            + " BillingStatus = ?BillingStatus, "
          
            + " LastPaymentDate = ?LastPaymentDate "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(Customer customer, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", customer.Id);
            
            	Database.PutParameter(dbCommand,"?CreationDate", customer.CreationDate);
            
            	Database.PutParameter(dbCommand,"?RealmId", customer.RealmId);
            
            	Database.PutParameter(dbCommand,"?AppDbId", customer.AppDbId);
            
            	Database.PutParameter(dbCommand,"?IsQBO", customer.IsQBO);
            
            	Database.PutParameter(dbCommand,"?DbName", customer.DbName);
            
            	Database.PutParameter(dbCommand,"?DbLogin", customer.DbLogin);
            
            	Database.PutParameter(dbCommand,"?DbPassword", customer.DbPassword);
            
            	Database.PutParameter(dbCommand,"?Name", customer.Name);
            
            	Database.PutParameter(dbCommand,"?ContactPerson", customer.ContactPerson);
            
            	Database.PutParameter(dbCommand,"?Email", customer.Email);
            
            	Database.PutParameter(dbCommand,"?Phone", customer.Phone);
            
            	Database.PutParameter(dbCommand,"?Description", customer.Description);
            
            	Database.PutParameter(dbCommand,"?IsTrackingPhonesInited", customer.IsTrackingPhonesInited);
            
            	Database.PutParameter(dbCommand,"?IsCampaignsInited", customer.IsCampaignsInited);
            
            	Database.PutParameter(dbCommand,"?IsOAuthInited", customer.IsOAuthInited);
            
            	Database.PutParameter(dbCommand,"?IsCompanyProfileInited", customer.IsCompanyProfileInited);
            
            	Database.PutParameter(dbCommand,"?BillingStatus", customer.BillingStatus);
            
            	Database.PutParameter(dbCommand,"?LastPaymentDate", customer.LastPaymentDate);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(Customer customer)
        {
          	Update(customer, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " CreationDate, "
        
          + " RealmId, "
        
          + " AppDbId, "
        
          + " IsQBO, "
        
          + " DbName, "
        
          + " DbLogin, "
        
          + " DbPassword, "
        
          + " Name, "
        
          + " ContactPerson, "
        
          + " Email, "
        
          + " Phone, "
        
          + " Description, "
        
          + " IsTrackingPhonesInited, "
        
          + " IsCampaignsInited, "
        
          + " IsOAuthInited, "
        
          + " IsCompanyProfileInited, "
        
          + " BillingStatus, "
        
          + " LastPaymentDate "
        
          + " From Customer "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static Customer FindByPrimaryKey(
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

            throw new DataNotFoundException("Customer not found, search by primary key");
        }

        public static Customer FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(Customer customer, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",customer.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(Customer customer)
        {
        	return Exists(customer, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from Customer limit 1";

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

        public static Customer Load(IDataReader dataReader, int offset)
        {
              Customer customer = new Customer();

              customer.Id = dataReader.GetInt32(0 + offset);
                  customer.CreationDate = dataReader.GetDateTime(1 + offset);
                  customer.RealmId = dataReader.GetString(2 + offset);
                  customer.AppDbId = dataReader.GetString(3 + offset);
                  customer.IsQBO = dataReader.GetBoolean(4 + offset);
                  customer.DbName = dataReader.GetString(5 + offset);
                  customer.DbLogin = dataReader.GetString(6 + offset);
                  customer.DbPassword = dataReader.GetString(7 + offset);
                  
                    if(!dataReader.IsDBNull(8 + offset))
                    customer.Name = dataReader.GetString(8 + offset);
                  
                    if(!dataReader.IsDBNull(9 + offset))
                    customer.ContactPerson = dataReader.GetString(9 + offset);
                  
                    if(!dataReader.IsDBNull(10 + offset))
                    customer.Email = dataReader.GetString(10 + offset);
                  
                    if(!dataReader.IsDBNull(11 + offset))
                    customer.Phone = dataReader.GetString(11 + offset);
                  
                    if(!dataReader.IsDBNull(12 + offset))
                    customer.Description = dataReader.GetString(12 + offset);
                  customer.IsTrackingPhonesInited = dataReader.GetBoolean(13 + offset);
                  customer.IsCampaignsInited = dataReader.GetBoolean(14 + offset);
                  customer.IsOAuthInited = dataReader.GetBoolean(15 + offset);
                  customer.IsCompanyProfileInited = dataReader.GetBoolean(16 + offset);
                  
                    if(!dataReader.IsDBNull(17 + offset))
                    customer.BillingStatus = dataReader.GetString(17 + offset);
                  
                    if(!dataReader.IsDBNull(18 + offset))
                    customer.LastPaymentDate = dataReader.GetDateTime(18 + offset);
                  

            return customer;
        }

        public static Customer Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From Customer "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(Customer customer, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", customer.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(Customer customer)
        {
        	Delete(customer, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From Customer ";

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
              
                + " CreationDate, "
              
                + " RealmId, "
              
                + " AppDbId, "
              
                + " IsQBO, "
              
                + " DbName, "
              
                + " DbLogin, "
              
                + " DbPassword, "
              
                + " Name, "
              
                + " ContactPerson, "
              
                + " Email, "
              
                + " Phone, "
              
                + " Description, "
              
                + " IsTrackingPhonesInited, "
              
                + " IsCampaignsInited, "
              
                + " IsOAuthInited, "
              
                + " IsCompanyProfileInited, "
              
                + " BillingStatus, "
              
                + " LastPaymentDate "
              
                + " From Customer ";

        public static List<Customer> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<Customer> rv = new List<Customer>();

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

        public static List<Customer> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<Customer> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<Customer> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Customer));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(Customer item in itemsList)
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

        public static List<Customer> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(Customer));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<Customer> itemsList = new List<Customer>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is Customer)
              		itemsList.Add(deserializedObject as Customer);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected DateTime m_creationDate;
              
        protected String m_realmId;
              
        protected String m_appDbId;
              
        protected bool m_isQBO;
              
        protected String m_dbName;
              
        protected String m_dbLogin;
              
        protected String m_dbPassword;
              
        protected String m_name;
              
        protected String m_contactPerson;
              
        protected String m_email;
              
        protected String m_phone;
              
        protected String m_description;
              
        protected bool m_isTrackingPhonesInited;
              
        protected bool m_isCampaignsInited;
              
        protected bool m_isOAuthInited;
              
        protected bool m_isCompanyProfileInited;
              
        protected String m_billingStatus;
              
        protected DateTime? m_lastPaymentDate;
              
        #endregion

        #region Constructors

        public Customer(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public Customer(
                int 
                  id,DateTime 
                  creationDate,String 
                  realmId,String 
                  appDbId,bool 
                  isQBO,String 
                  dbName,String 
                  dbLogin,String 
                  dbPassword,String 
                  name,String 
                  contactPerson,String 
                  email,String 
                  phone,String 
                  description,bool 
                  isTrackingPhonesInited,bool 
                  isCampaignsInited,bool 
                  isOAuthInited,bool 
                  isCompanyProfileInited,String 
                  billingStatus,DateTime? 
                  lastPaymentDate
                ) : this()
        {
            
        	m_id = id;
            
        	m_creationDate = creationDate;
            
        	m_realmId = realmId;
            
        	m_appDbId = appDbId;
            
        	m_isQBO = isQBO;
            
        	m_dbName = dbName;
            
        	m_dbLogin = dbLogin;
            
        	m_dbPassword = dbPassword;
            
        	m_name = name;
            
        	m_contactPerson = contactPerson;
            
        	m_email = email;
            
        	m_phone = phone;
            
        	m_description = description;
            
        	m_isTrackingPhonesInited = isTrackingPhonesInited;
            
        	m_isCampaignsInited = isCampaignsInited;
            
        	m_isOAuthInited = isOAuthInited;
            
        	m_isCompanyProfileInited = isCompanyProfileInited;
            
        	m_billingStatus = billingStatus;
            
        	m_lastPaymentDate = lastPaymentDate;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public DateTime CreationDate
        {
        	get { return m_creationDate;}
            set { m_creationDate = value; }
        }
        
        public String RealmId
        {
        	get { return m_realmId;}
            set { m_realmId = value; }
        }
        
        public String AppDbId
        {
        	get { return m_appDbId;}
            set { m_appDbId = value; }
        }
        
        public bool IsQBO
        {
        	get { return m_isQBO;}
            set { m_isQBO = value; }
        }
        
        public String DbName
        {
        	get { return m_dbName;}
            set { m_dbName = value; }
        }
        
        public String DbLogin
        {
        	get { return m_dbLogin;}
            set { m_dbLogin = value; }
        }
        
        public String DbPassword
        {
        	get { return m_dbPassword;}
            set { m_dbPassword = value; }
        }
        
        public String Name
        {
        	get { return m_name;}
            set { m_name = value; }
        }
        
        public String ContactPerson
        {
        	get { return m_contactPerson;}
            set { m_contactPerson = value; }
        }
        
        public String Email
        {
        	get { return m_email;}
            set { m_email = value; }
        }
        
        public String Phone
        {
        	get { return m_phone;}
            set { m_phone = value; }
        }
        
        public String Description
        {
        	get { return m_description;}
            set { m_description = value; }
        }
        
        public bool IsTrackingPhonesInited
        {
        	get { return m_isTrackingPhonesInited;}
            set { m_isTrackingPhonesInited = value; }
        }
        
        public bool IsCampaignsInited
        {
        	get { return m_isCampaignsInited;}
            set { m_isCampaignsInited = value; }
        }
        
        public bool IsOAuthInited
        {
        	get { return m_isOAuthInited;}
            set { m_isOAuthInited = value; }
        }
        
        public bool IsCompanyProfileInited
        {
        	get { return m_isCompanyProfileInited;}
            set { m_isCompanyProfileInited = value; }
        }
        
        public String BillingStatus
        {
        	get { return m_billingStatus;}
            set { m_billingStatus = value; }
        }
        
        public DateTime? LastPaymentDate
        {
        	get { return m_lastPaymentDate;}
            set { m_lastPaymentDate = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 19; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    