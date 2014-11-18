
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Common.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class User : ICloneable
    {

        #region Store


        #region Save

        public static User Save(User user, IDbConnection connection)
        {
        	if (!Exists(user, connection))
        		Insert(user, connection);
        	else
        		Update(user, connection);
        	return user;
        }

        public static User Save(User user)
        {
        	if (!Exists(user))
        		Insert(user);
        	else
        		Update(user);
        	return user;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into User ( " +
        
          " DateCreated, " +
        
          " Email, " +
        
          " FirstName, " +
        
          " LastName, " +
        
          " ScreenName, " +
        
          " Phone, " +
        
          " Address, " +
        
          " QbUserId, " +
        
          " QbRoleName, " +
        
          " BusinessPartnerId, " +
        
          " DateLastAccess, " +
        
          " IsActive " +
        
        ") Values (" +
        
          " ?DateCreated, " +
        
          " ?Email, " +
        
          " ?FirstName, " +
        
          " ?LastName, " +
        
          " ?ScreenName, " +
        
          " ?Phone, " +
        
          " ?Address, " +
        
          " ?QbUserId, " +
        
          " ?QbRoleName, " +
        
          " ?BusinessPartnerId, " +
        
          " ?DateLastAccess, " +
        
          " ?IsActive " +
        
        ")";

        public static void Insert(User user, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?DateCreated", user.DateCreated);
            
            	Database.PutParameter(dbCommand,"?Email", user.Email);
            
            	Database.PutParameter(dbCommand,"?FirstName", user.FirstName);
            
            	Database.PutParameter(dbCommand,"?LastName", user.LastName);
            
            	Database.PutParameter(dbCommand,"?ScreenName", user.ScreenName);
            
            	Database.PutParameter(dbCommand,"?Phone", user.Phone);
            
            	Database.PutParameter(dbCommand,"?Address", user.Address);
            
            	Database.PutParameter(dbCommand,"?QbUserId", user.QbUserId);
            
            	Database.PutParameter(dbCommand,"?QbRoleName", user.QbRoleName);
            
            	Database.PutParameter(dbCommand,"?BusinessPartnerId", user.BusinessPartnerId);
            
            	Database.PutParameter(dbCommand,"?DateLastAccess", user.DateLastAccess);
            
            	Database.PutParameter(dbCommand,"?IsActive", user.IsActive);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		user.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(User user)
        {
          	Insert(user, null);
        }

        public static void Insert(List<User>  userList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(User user in  userList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?DateCreated", user.DateCreated);
                
                  	Database.PutParameter(dbCommand,"?Email", user.Email);
                
                  	Database.PutParameter(dbCommand,"?FirstName", user.FirstName);
                
                  	Database.PutParameter(dbCommand,"?LastName", user.LastName);
                
                  	Database.PutParameter(dbCommand,"?ScreenName", user.ScreenName);
                
                  	Database.PutParameter(dbCommand,"?Phone", user.Phone);
                
                  	Database.PutParameter(dbCommand,"?Address", user.Address);
                
                  	Database.PutParameter(dbCommand,"?QbUserId", user.QbUserId);
                
                  	Database.PutParameter(dbCommand,"?QbRoleName", user.QbRoleName);
                
                  	Database.PutParameter(dbCommand,"?BusinessPartnerId", user.BusinessPartnerId);
                
                  	Database.PutParameter(dbCommand,"?DateLastAccess", user.DateLastAccess);
                
                  	Database.PutParameter(dbCommand,"?IsActive", user.IsActive);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?DateCreated",user.DateCreated);
                
                	Database.UpdateParameter(dbCommand,"?Email",user.Email);
                
                	Database.UpdateParameter(dbCommand,"?FirstName",user.FirstName);
                
                	Database.UpdateParameter(dbCommand,"?LastName",user.LastName);
                
                	Database.UpdateParameter(dbCommand,"?ScreenName",user.ScreenName);
                
                	Database.UpdateParameter(dbCommand,"?Phone",user.Phone);
                
                	Database.UpdateParameter(dbCommand,"?Address",user.Address);
                
                	Database.UpdateParameter(dbCommand,"?QbUserId",user.QbUserId);
                
                	Database.UpdateParameter(dbCommand,"?QbRoleName",user.QbRoleName);
                
                	Database.UpdateParameter(dbCommand,"?BusinessPartnerId",user.BusinessPartnerId);
                
                	Database.UpdateParameter(dbCommand,"?DateLastAccess",user.DateLastAccess);
                
                	Database.UpdateParameter(dbCommand,"?IsActive",user.IsActive);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	user.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<User>  userList)
        {
        	Insert(userList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update User Set "
          
            + " DateCreated = ?DateCreated, "
          
            + " Email = ?Email, "
          
            + " FirstName = ?FirstName, "
          
            + " LastName = ?LastName, "
          
            + " ScreenName = ?ScreenName, "
          
            + " Phone = ?Phone, "
          
            + " Address = ?Address, "
          
            + " QbUserId = ?QbUserId, "
          
            + " QbRoleName = ?QbRoleName, "
          
            + " BusinessPartnerId = ?BusinessPartnerId, "
          
            + " DateLastAccess = ?DateLastAccess, "
          
            + " IsActive = ?IsActive "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(User user, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", user.Id);
            
            	Database.PutParameter(dbCommand,"?DateCreated", user.DateCreated);
            
            	Database.PutParameter(dbCommand,"?Email", user.Email);
            
            	Database.PutParameter(dbCommand,"?FirstName", user.FirstName);
            
            	Database.PutParameter(dbCommand,"?LastName", user.LastName);
            
            	Database.PutParameter(dbCommand,"?ScreenName", user.ScreenName);
            
            	Database.PutParameter(dbCommand,"?Phone", user.Phone);
            
            	Database.PutParameter(dbCommand,"?Address", user.Address);
            
            	Database.PutParameter(dbCommand,"?QbUserId", user.QbUserId);
            
            	Database.PutParameter(dbCommand,"?QbRoleName", user.QbRoleName);
            
            	Database.PutParameter(dbCommand,"?BusinessPartnerId", user.BusinessPartnerId);
            
            	Database.PutParameter(dbCommand,"?DateLastAccess", user.DateLastAccess);
            
            	Database.PutParameter(dbCommand,"?IsActive", user.IsActive);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(User user)
        {
          	Update(user, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " DateCreated, "
        
          + " Email, "
        
          + " FirstName, "
        
          + " LastName, "
        
          + " ScreenName, "
        
          + " Phone, "
        
          + " Address, "
        
          + " QbUserId, "
        
          + " QbRoleName, "
        
          + " BusinessPartnerId, "
        
          + " DateLastAccess, "
        
          + " IsActive "
        
          + " From User "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static User FindByPrimaryKey(
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

            throw new DataNotFoundException("User not found, search by primary key");
        }

        public static User FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(User user, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",user.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(User user)
        {
        	return Exists(user, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from User limit 1";

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

        public static User Load(IDataReader dataReader, int offset)
        {
              User user = new User();

              user.Id = dataReader.GetInt32(0 + offset);
                  user.DateCreated = dataReader.GetDateTime(1 + offset);
                  
                    if(!dataReader.IsDBNull(2 + offset))
                    user.Email = dataReader.GetString(2 + offset);
                  
                    if(!dataReader.IsDBNull(3 + offset))
                    user.FirstName = dataReader.GetString(3 + offset);
                  
                    if(!dataReader.IsDBNull(4 + offset))
                    user.LastName = dataReader.GetString(4 + offset);
                  
                    if(!dataReader.IsDBNull(5 + offset))
                    user.ScreenName = dataReader.GetString(5 + offset);
                  
                    if(!dataReader.IsDBNull(6 + offset))
                    user.Phone = dataReader.GetString(6 + offset);
                  
                    if(!dataReader.IsDBNull(7 + offset))
                    user.Address = dataReader.GetString(7 + offset);
                  
                    if(!dataReader.IsDBNull(8 + offset))
                    user.QbUserId = dataReader.GetString(8 + offset);
                  
                    if(!dataReader.IsDBNull(9 + offset))
                    user.QbRoleName = dataReader.GetString(9 + offset);
                  
                    if(!dataReader.IsDBNull(10 + offset))
                    user.BusinessPartnerId = dataReader.GetInt32(10 + offset);
                  
                    if(!dataReader.IsDBNull(11 + offset))
                    user.DateLastAccess = dataReader.GetDateTime(11 + offset);
                  user.IsActive = dataReader.GetBoolean(12 + offset);
                  

            return user;
        }

        public static User Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From User "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(User user, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", user.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(User user)
        {
        	Delete(user, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From User ";

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
              
                + " DateCreated, "
              
                + " Email, "
              
                + " FirstName, "
              
                + " LastName, "
              
                + " ScreenName, "
              
                + " Phone, "
              
                + " Address, "
              
                + " QbUserId, "
              
                + " QbRoleName, "
              
                + " BusinessPartnerId, "
              
                + " DateLastAccess, "
              
                + " IsActive "
              
                + " From User ";

        public static List<User> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<User> rv = new List<User>();

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

        public static List<User> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<User> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<User> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(User));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(User item in itemsList)
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

        public static List<User> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(User));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<User> itemsList = new List<User>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is User)
              		itemsList.Add(deserializedObject as User);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected DateTime m_dateCreated;
              
        protected String m_email;
              
        protected String m_firstName;
              
        protected String m_lastName;
              
        protected String m_screenName;
              
        protected String m_phone;
              
        protected String m_address;
              
        protected String m_qbUserId;
              
        protected String m_qbRoleName;
              
        protected int? m_businessPartnerId;
              
        protected DateTime? m_dateLastAccess;
              
        protected bool m_isActive;
              
        #endregion

        #region Constructors

        public User(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public User(
                int 
                  id,DateTime 
                  dateCreated,String 
                  email,String 
                  firstName,String 
                  lastName,String 
                  screenName,String 
                  phone,String 
                  address,String 
                  qbUserId,String 
                  qbRoleName,int? 
                  businessPartnerId,DateTime? 
                  dateLastAccess,bool 
                  isActive
                ) : this()
        {
            
        	m_id = id;
            
        	m_dateCreated = dateCreated;
            
        	m_email = email;
            
        	m_firstName = firstName;
            
        	m_lastName = lastName;
            
        	m_screenName = screenName;
            
        	m_phone = phone;
            
        	m_address = address;
            
        	m_qbUserId = qbUserId;
            
        	m_qbRoleName = qbRoleName;
            
        	m_businessPartnerId = businessPartnerId;
            
        	m_dateLastAccess = dateLastAccess;
            
        	m_isActive = isActive;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public DateTime DateCreated
        {
        	get { return m_dateCreated;}
            set { m_dateCreated = value; }
        }
        
        public String Email
        {
        	get { return m_email;}
            set { m_email = value; }
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
        
        public String ScreenName
        {
        	get { return m_screenName;}
            set { m_screenName = value; }
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
        
        public String QbUserId
        {
        	get { return m_qbUserId;}
            set { m_qbUserId = value; }
        }
        
        public String QbRoleName
        {
        	get { return m_qbRoleName;}
            set { m_qbRoleName = value; }
        }
        
        public int? BusinessPartnerId
        {
        	get { return m_businessPartnerId;}
            set { m_businessPartnerId = value; }
        }
        
        public DateTime? DateLastAccess
        {
        	get { return m_dateLastAccess;}
            set { m_dateLastAccess = value; }
        }
        
        public bool IsActive
        {
        	get { return m_isActive;}
            set { m_isActive = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 13; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    