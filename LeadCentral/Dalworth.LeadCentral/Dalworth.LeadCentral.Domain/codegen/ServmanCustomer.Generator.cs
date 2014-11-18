
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Common.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class ServmanCustomer : ICloneable
    {

        #region Store


        #region Save

        public static ServmanCustomer Save(ServmanCustomer servmanCustomer, IDbConnection connection)
        {
        	if (!Exists(servmanCustomer, connection))
        		Insert(servmanCustomer, connection);
        	else
        		Update(servmanCustomer, connection);
        	return servmanCustomer;
        }

        public static ServmanCustomer Save(ServmanCustomer servmanCustomer)
        {
        	if (!Exists(servmanCustomer))
        		Insert(servmanCustomer);
        	else
        		Update(servmanCustomer);
        	return servmanCustomer;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into ServmanCustomer ( " +
        
          " RealmId, " +
        
          " CreationDate, " +
        
          " LastLoginDate, " +
        
          " DbName, " +
        
          " Login, " +
        
          " Password, " +
        
          " Name, " +
        
          " Email, " +
        
          " Phone, " +
        
          " Description, " +
        
          " AppDbId, " +
        
          " IsLeadSourcesInited, " +
        
          " IsOAuthInited, " +
        
          " IsCompanyProfileInited, " +
        
          " IsQBO, " +
        
          " ContactPerson " +
        
        ") Values (" +
        
          " ?RealmId, " +
        
          " ?CreationDate, " +
        
          " ?LastLoginDate, " +
        
          " ?DbName, " +
        
          " ?Login, " +
        
          " ?Password, " +
        
          " ?Name, " +
        
          " ?Email, " +
        
          " ?Phone, " +
        
          " ?Description, " +
        
          " ?AppDbId, " +
        
          " ?IsLeadSourcesInited, " +
        
          " ?IsOAuthInited, " +
        
          " ?IsCompanyProfileInited, " +
        
          " ?IsQBO, " +
        
          " ?ContactPerson " +
        
        ")";

        public static void Insert(ServmanCustomer servmanCustomer, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?RealmId", servmanCustomer.RealmId);
            
            	Database.PutParameter(dbCommand,"?CreationDate", servmanCustomer.CreationDate);
            
            	Database.PutParameter(dbCommand,"?LastLoginDate", servmanCustomer.LastLoginDate);
            
            	Database.PutParameter(dbCommand,"?DbName", servmanCustomer.DbName);
            
            	Database.PutParameter(dbCommand,"?Login", servmanCustomer.Login);
            
            	Database.PutParameter(dbCommand,"?Password", servmanCustomer.Password);
            
            	Database.PutParameter(dbCommand,"?Name", servmanCustomer.Name);
            
            	Database.PutParameter(dbCommand,"?Email", servmanCustomer.Email);
            
            	Database.PutParameter(dbCommand,"?Phone", servmanCustomer.Phone);
            
            	Database.PutParameter(dbCommand,"?Description", servmanCustomer.Description);
            
            	Database.PutParameter(dbCommand,"?AppDbId", servmanCustomer.AppDbId);
            
            	Database.PutParameter(dbCommand,"?IsLeadSourcesInited", servmanCustomer.IsLeadSourcesInited);
            
            	Database.PutParameter(dbCommand,"?IsOAuthInited", servmanCustomer.IsOAuthInited);
            
            	Database.PutParameter(dbCommand,"?IsCompanyProfileInited", servmanCustomer.IsCompanyProfileInited);
            
            	Database.PutParameter(dbCommand,"?IsQBO", servmanCustomer.IsQBO);
            
            	Database.PutParameter(dbCommand,"?ContactPerson", servmanCustomer.ContactPerson);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		servmanCustomer.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(ServmanCustomer servmanCustomer)
        {
          	Insert(servmanCustomer, null);
        }

        public static void Insert(List<ServmanCustomer>  servmanCustomerList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(ServmanCustomer servmanCustomer in  servmanCustomerList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?RealmId", servmanCustomer.RealmId);
                
                  	Database.PutParameter(dbCommand,"?CreationDate", servmanCustomer.CreationDate);
                
                  	Database.PutParameter(dbCommand,"?LastLoginDate", servmanCustomer.LastLoginDate);
                
                  	Database.PutParameter(dbCommand,"?DbName", servmanCustomer.DbName);
                
                  	Database.PutParameter(dbCommand,"?Login", servmanCustomer.Login);
                
                  	Database.PutParameter(dbCommand,"?Password", servmanCustomer.Password);
                
                  	Database.PutParameter(dbCommand,"?Name", servmanCustomer.Name);
                
                  	Database.PutParameter(dbCommand,"?Email", servmanCustomer.Email);
                
                  	Database.PutParameter(dbCommand,"?Phone", servmanCustomer.Phone);
                
                  	Database.PutParameter(dbCommand,"?Description", servmanCustomer.Description);
                
                  	Database.PutParameter(dbCommand,"?AppDbId", servmanCustomer.AppDbId);
                
                  	Database.PutParameter(dbCommand,"?IsLeadSourcesInited", servmanCustomer.IsLeadSourcesInited);
                
                  	Database.PutParameter(dbCommand,"?IsOAuthInited", servmanCustomer.IsOAuthInited);
                
                  	Database.PutParameter(dbCommand,"?IsCompanyProfileInited", servmanCustomer.IsCompanyProfileInited);
                
                  	Database.PutParameter(dbCommand,"?IsQBO", servmanCustomer.IsQBO);
                
                  	Database.PutParameter(dbCommand,"?ContactPerson", servmanCustomer.ContactPerson);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?RealmId",servmanCustomer.RealmId);
                
                	Database.UpdateParameter(dbCommand,"?CreationDate",servmanCustomer.CreationDate);
                
                	Database.UpdateParameter(dbCommand,"?LastLoginDate",servmanCustomer.LastLoginDate);
                
                	Database.UpdateParameter(dbCommand,"?DbName",servmanCustomer.DbName);
                
                	Database.UpdateParameter(dbCommand,"?Login",servmanCustomer.Login);
                
                	Database.UpdateParameter(dbCommand,"?Password",servmanCustomer.Password);
                
                	Database.UpdateParameter(dbCommand,"?Name",servmanCustomer.Name);
                
                	Database.UpdateParameter(dbCommand,"?Email",servmanCustomer.Email);
                
                	Database.UpdateParameter(dbCommand,"?Phone",servmanCustomer.Phone);
                
                	Database.UpdateParameter(dbCommand,"?Description",servmanCustomer.Description);
                
                	Database.UpdateParameter(dbCommand,"?AppDbId",servmanCustomer.AppDbId);
                
                	Database.UpdateParameter(dbCommand,"?IsLeadSourcesInited",servmanCustomer.IsLeadSourcesInited);
                
                	Database.UpdateParameter(dbCommand,"?IsOAuthInited",servmanCustomer.IsOAuthInited);
                
                	Database.UpdateParameter(dbCommand,"?IsCompanyProfileInited",servmanCustomer.IsCompanyProfileInited);
                
                	Database.UpdateParameter(dbCommand,"?IsQBO",servmanCustomer.IsQBO);
                
                	Database.UpdateParameter(dbCommand,"?ContactPerson",servmanCustomer.ContactPerson);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	servmanCustomer.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<ServmanCustomer>  servmanCustomerList)
        {
        	Insert(servmanCustomerList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update ServmanCustomer Set "
          
            + " RealmId = ?RealmId, "
          
            + " CreationDate = ?CreationDate, "
          
            + " LastLoginDate = ?LastLoginDate, "
          
            + " DbName = ?DbName, "
          
            + " Login = ?Login, "
          
            + " Password = ?Password, "
          
            + " Name = ?Name, "
          
            + " Email = ?Email, "
          
            + " Phone = ?Phone, "
          
            + " Description = ?Description, "
          
            + " AppDbId = ?AppDbId, "
          
            + " IsLeadSourcesInited = ?IsLeadSourcesInited, "
          
            + " IsOAuthInited = ?IsOAuthInited, "
          
            + " IsCompanyProfileInited = ?IsCompanyProfileInited, "
          
            + " IsQBO = ?IsQBO, "
          
            + " ContactPerson = ?ContactPerson "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(ServmanCustomer servmanCustomer, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", servmanCustomer.Id);
            
            	Database.PutParameter(dbCommand,"?RealmId", servmanCustomer.RealmId);
            
            	Database.PutParameter(dbCommand,"?CreationDate", servmanCustomer.CreationDate);
            
            	Database.PutParameter(dbCommand,"?LastLoginDate", servmanCustomer.LastLoginDate);
            
            	Database.PutParameter(dbCommand,"?DbName", servmanCustomer.DbName);
            
            	Database.PutParameter(dbCommand,"?Login", servmanCustomer.Login);
            
            	Database.PutParameter(dbCommand,"?Password", servmanCustomer.Password);
            
            	Database.PutParameter(dbCommand,"?Name", servmanCustomer.Name);
            
            	Database.PutParameter(dbCommand,"?Email", servmanCustomer.Email);
            
            	Database.PutParameter(dbCommand,"?Phone", servmanCustomer.Phone);
            
            	Database.PutParameter(dbCommand,"?Description", servmanCustomer.Description);
            
            	Database.PutParameter(dbCommand,"?AppDbId", servmanCustomer.AppDbId);
            
            	Database.PutParameter(dbCommand,"?IsLeadSourcesInited", servmanCustomer.IsLeadSourcesInited);
            
            	Database.PutParameter(dbCommand,"?IsOAuthInited", servmanCustomer.IsOAuthInited);
            
            	Database.PutParameter(dbCommand,"?IsCompanyProfileInited", servmanCustomer.IsCompanyProfileInited);
            
            	Database.PutParameter(dbCommand,"?IsQBO", servmanCustomer.IsQBO);
            
            	Database.PutParameter(dbCommand,"?ContactPerson", servmanCustomer.ContactPerson);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(ServmanCustomer servmanCustomer)
        {
          	Update(servmanCustomer, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " RealmId, "
        
          + " CreationDate, "
        
          + " LastLoginDate, "
        
          + " DbName, "
        
          + " Login, "
        
          + " Password, "
        
          + " Name, "
        
          + " Email, "
        
          + " Phone, "
        
          + " Description, "
        
          + " AppDbId, "
        
          + " IsLeadSourcesInited, "
        
          + " IsOAuthInited, "
        
          + " IsCompanyProfileInited, "
        
          + " IsQBO, "
        
          + " ContactPerson "
        
          + " From ServmanCustomer "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static ServmanCustomer FindByPrimaryKey(
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

            throw new DataNotFoundException("ServmanCustomer not found, search by primary key");
        }

        public static ServmanCustomer FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(ServmanCustomer servmanCustomer, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",servmanCustomer.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(ServmanCustomer servmanCustomer)
        {
        	return Exists(servmanCustomer, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from ServmanCustomer limit 1";

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

        public static ServmanCustomer Load(IDataReader dataReader, int offset)
        {
              ServmanCustomer servmanCustomer = new ServmanCustomer();

              servmanCustomer.Id = dataReader.GetInt32(0 + offset);
                  servmanCustomer.RealmId = dataReader.GetString(1 + offset);
                  servmanCustomer.CreationDate = dataReader.GetDateTime(2 + offset);
                  servmanCustomer.LastLoginDate = dataReader.GetDateTime(3 + offset);
                  servmanCustomer.DbName = dataReader.GetString(4 + offset);
                  servmanCustomer.Login = dataReader.GetString(5 + offset);
                  servmanCustomer.Password = dataReader.GetString(6 + offset);
                  
                    if(!dataReader.IsDBNull(7 + offset))
                    servmanCustomer.Name = dataReader.GetString(7 + offset);
                  
                    if(!dataReader.IsDBNull(8 + offset))
                    servmanCustomer.Email = dataReader.GetString(8 + offset);
                  
                    if(!dataReader.IsDBNull(9 + offset))
                    servmanCustomer.Phone = dataReader.GetString(9 + offset);
                  
                    if(!dataReader.IsDBNull(10 + offset))
                    servmanCustomer.Description = dataReader.GetString(10 + offset);
                  
                    if(!dataReader.IsDBNull(11 + offset))
                    servmanCustomer.AppDbId = dataReader.GetString(11 + offset);
                  
                    if(!dataReader.IsDBNull(12 + offset))
                    servmanCustomer.IsLeadSourcesInited = dataReader.GetBoolean(12 + offset);
                  
                    if(!dataReader.IsDBNull(13 + offset))
                    servmanCustomer.IsOAuthInited = dataReader.GetBoolean(13 + offset);
                  
                    if(!dataReader.IsDBNull(14 + offset))
                    servmanCustomer.IsCompanyProfileInited = dataReader.GetBoolean(14 + offset);
                  
                    if(!dataReader.IsDBNull(15 + offset))
                    servmanCustomer.IsQBO = dataReader.GetBoolean(15 + offset);
                  
                    if(!dataReader.IsDBNull(16 + offset))
                    servmanCustomer.ContactPerson = dataReader.GetString(16 + offset);
                  

            return servmanCustomer;
        }

        public static ServmanCustomer Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From ServmanCustomer "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(ServmanCustomer servmanCustomer, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", servmanCustomer.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(ServmanCustomer servmanCustomer)
        {
        	Delete(servmanCustomer, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From ServmanCustomer ";

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
              
                + " RealmId, "
              
                + " CreationDate, "
              
                + " LastLoginDate, "
              
                + " DbName, "
              
                + " Login, "
              
                + " Password, "
              
                + " Name, "
              
                + " Email, "
              
                + " Phone, "
              
                + " Description, "
              
                + " AppDbId, "
              
                + " IsLeadSourcesInited, "
              
                + " IsOAuthInited, "
              
                + " IsCompanyProfileInited, "
              
                + " IsQBO, "
              
                + " ContactPerson "
              
                + " From ServmanCustomer ";

        public static List<ServmanCustomer> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<ServmanCustomer> rv = new List<ServmanCustomer>();

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

        public static List<ServmanCustomer> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<ServmanCustomer> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<ServmanCustomer> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ServmanCustomer));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(ServmanCustomer item in itemsList)
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

        public static List<ServmanCustomer> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(ServmanCustomer));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<ServmanCustomer> itemsList = new List<ServmanCustomer>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is ServmanCustomer)
              		itemsList.Add(deserializedObject as ServmanCustomer);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected String m_realmId;
              
        protected DateTime m_creationDate;
              
        protected DateTime m_lastLoginDate;
              
        protected String m_dbName;
              
        protected String m_login;
              
        protected String m_password;
              
        protected String m_name;
              
        protected String m_email;
              
        protected String m_phone;
              
        protected String m_description;
              
        protected String m_appDbId;
              
        protected bool m_isLeadSourcesInited;
              
        protected bool m_isOAuthInited;
              
        protected bool m_isCompanyProfileInited;
              
        protected bool m_isQBO;
              
        protected String m_contactPerson;
              
        #endregion

        #region Constructors

        public ServmanCustomer(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public ServmanCustomer(
                int 
                  id,String 
                  realmId,DateTime 
                  creationDate,DateTime 
                  lastLoginDate,String 
                  dbName,String 
                  login,String 
                  password,String 
                  name,String 
                  email,String 
                  phone,String 
                  description,String 
                  appDbId,bool 
                  isLeadSourcesInited,bool 
                  isOAuthInited,bool 
                  isCompanyProfileInited,bool 
                  isQBO,String 
                  contactPerson
                ) : this()
        {
            
        	m_id = id;
            
        	m_realmId = realmId;
            
        	m_creationDate = creationDate;
            
        	m_lastLoginDate = lastLoginDate;
            
        	m_dbName = dbName;
            
        	m_login = login;
            
        	m_password = password;
            
        	m_name = name;
            
        	m_email = email;
            
        	m_phone = phone;
            
        	m_description = description;
            
        	m_appDbId = appDbId;
            
        	m_isLeadSourcesInited = isLeadSourcesInited;
            
        	m_isOAuthInited = isOAuthInited;
            
        	m_isCompanyProfileInited = isCompanyProfileInited;
            
        	m_isQBO = isQBO;
            
        	m_contactPerson = contactPerson;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public String RealmId
        {
        	get { return m_realmId;}
            set { m_realmId = value; }
        }
        
        public DateTime CreationDate
        {
        	get { return m_creationDate;}
            set { m_creationDate = value; }
        }
        
        public DateTime LastLoginDate
        {
        	get { return m_lastLoginDate;}
            set { m_lastLoginDate = value; }
        }
        
        public String DbName
        {
        	get { return m_dbName;}
            set { m_dbName = value; }
        }
        
        public String Login
        {
        	get { return m_login;}
            set { m_login = value; }
        }
        
        public String Password
        {
        	get { return m_password;}
            set { m_password = value; }
        }
        
        public String Name
        {
        	get { return m_name;}
            set { m_name = value; }
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
        
        public String AppDbId
        {
        	get { return m_appDbId;}
            set { m_appDbId = value; }
        }
        
        public bool IsLeadSourcesInited
        {
        	get { return m_isLeadSourcesInited;}
            set { m_isLeadSourcesInited = value; }
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
        
        public bool IsQBO
        {
        	get { return m_isQBO;}
            set { m_isQBO = value; }
        }
        
        public String ContactPerson
        {
        	get { return m_contactPerson;}
            set { m_contactPerson = value; }
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

    