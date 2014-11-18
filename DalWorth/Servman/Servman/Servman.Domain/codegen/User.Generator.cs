
using System;
using System.Data;
using System.Collections.Generic;
using Servman.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Servman.Domain
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
        
          " QbUserId, " +
        
          " Email, " +
        
          " Name, " +
        
          " FirstName, " +
        
          " LastName, " +
        
          " Phone, " +
        
          " Address, " +
        
          " PhotoFileId, " +
        
          " IsActive, " +
        
          " QbEmployeeRecordId, " +
        
          " QbVendorRecordId, " +
        
          " QbSalesRepRecordId, " +
        
          " RoleName, " +
        
          " DateLastAccess " +
        
        ") Values (" +
        
          " ?QbUserId, " +
        
          " ?Email, " +
        
          " ?Name, " +
        
          " ?FirstName, " +
        
          " ?LastName, " +
        
          " ?Phone, " +
        
          " ?Address, " +
        
          " ?PhotoFileId, " +
        
          " ?IsActive, " +
        
          " ?QbEmployeeRecordId, " +
        
          " ?QbVendorRecordId, " +
        
          " ?QbSalesRepRecordId, " +
        
          " ?RoleName, " +
        
          " ?DateLastAccess " +
        
        ")";

        public static void Insert(User user, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?QbUserId", user.QbUserId);
            
            	Database.PutParameter(dbCommand,"?Email", user.Email);
            
            	Database.PutParameter(dbCommand,"?Name", user.Name);
            
            	Database.PutParameter(dbCommand,"?FirstName", user.FirstName);
            
            	Database.PutParameter(dbCommand,"?LastName", user.LastName);
            
            	Database.PutParameter(dbCommand,"?Phone", user.Phone);
            
            	Database.PutParameter(dbCommand,"?Address", user.Address);
            
            	Database.PutParameter(dbCommand,"?PhotoFileId", user.PhotoFileId);
            
            	Database.PutParameter(dbCommand,"?IsActive", user.IsActive);
            
            	Database.PutParameter(dbCommand,"?QbEmployeeRecordId", user.QbEmployeeRecordId);
            
            	Database.PutParameter(dbCommand,"?QbVendorRecordId", user.QbVendorRecordId);
            
            	Database.PutParameter(dbCommand,"?QbSalesRepRecordId", user.QbSalesRepRecordId);
            
            	Database.PutParameter(dbCommand,"?RoleName", user.RoleName);
            
            	Database.PutParameter(dbCommand,"?DateLastAccess", user.DateLastAccess);
            
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
                
                  	Database.PutParameter(dbCommand,"?QbUserId", user.QbUserId);
                
                  	Database.PutParameter(dbCommand,"?Email", user.Email);
                
                  	Database.PutParameter(dbCommand,"?Name", user.Name);
                
                  	Database.PutParameter(dbCommand,"?FirstName", user.FirstName);
                
                  	Database.PutParameter(dbCommand,"?LastName", user.LastName);
                
                  	Database.PutParameter(dbCommand,"?Phone", user.Phone);
                
                  	Database.PutParameter(dbCommand,"?Address", user.Address);
                
                  	Database.PutParameter(dbCommand,"?PhotoFileId", user.PhotoFileId);
                
                  	Database.PutParameter(dbCommand,"?IsActive", user.IsActive);
                
                  	Database.PutParameter(dbCommand,"?QbEmployeeRecordId", user.QbEmployeeRecordId);
                
                  	Database.PutParameter(dbCommand,"?QbVendorRecordId", user.QbVendorRecordId);
                
                  	Database.PutParameter(dbCommand,"?QbSalesRepRecordId", user.QbSalesRepRecordId);
                
                  	Database.PutParameter(dbCommand,"?RoleName", user.RoleName);
                
                  	Database.PutParameter(dbCommand,"?DateLastAccess", user.DateLastAccess);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?QbUserId",user.QbUserId);
                
                	Database.UpdateParameter(dbCommand,"?Email",user.Email);
                
                	Database.UpdateParameter(dbCommand,"?Name",user.Name);
                
                	Database.UpdateParameter(dbCommand,"?FirstName",user.FirstName);
                
                	Database.UpdateParameter(dbCommand,"?LastName",user.LastName);
                
                	Database.UpdateParameter(dbCommand,"?Phone",user.Phone);
                
                	Database.UpdateParameter(dbCommand,"?Address",user.Address);
                
                	Database.UpdateParameter(dbCommand,"?PhotoFileId",user.PhotoFileId);
                
                	Database.UpdateParameter(dbCommand,"?IsActive",user.IsActive);
                
                	Database.UpdateParameter(dbCommand,"?QbEmployeeRecordId",user.QbEmployeeRecordId);
                
                	Database.UpdateParameter(dbCommand,"?QbVendorRecordId",user.QbVendorRecordId);
                
                	Database.UpdateParameter(dbCommand,"?QbSalesRepRecordId",user.QbSalesRepRecordId);
                
                	Database.UpdateParameter(dbCommand,"?RoleName",user.RoleName);
                
                	Database.UpdateParameter(dbCommand,"?DateLastAccess",user.DateLastAccess);
                
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
          
            + " QbUserId = ?QbUserId, "
          
            + " Email = ?Email, "
          
            + " Name = ?Name, "
          
            + " FirstName = ?FirstName, "
          
            + " LastName = ?LastName, "
          
            + " Phone = ?Phone, "
          
            + " Address = ?Address, "
          
            + " PhotoFileId = ?PhotoFileId, "
          
            + " IsActive = ?IsActive, "
          
            + " QbEmployeeRecordId = ?QbEmployeeRecordId, "
          
            + " QbVendorRecordId = ?QbVendorRecordId, "
          
            + " QbSalesRepRecordId = ?QbSalesRepRecordId, "
          
            + " RoleName = ?RoleName, "
          
            + " DateLastAccess = ?DateLastAccess "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(User user, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", user.Id);
            
            	Database.PutParameter(dbCommand,"?QbUserId", user.QbUserId);
            
            	Database.PutParameter(dbCommand,"?Email", user.Email);
            
            	Database.PutParameter(dbCommand,"?Name", user.Name);
            
            	Database.PutParameter(dbCommand,"?FirstName", user.FirstName);
            
            	Database.PutParameter(dbCommand,"?LastName", user.LastName);
            
            	Database.PutParameter(dbCommand,"?Phone", user.Phone);
            
            	Database.PutParameter(dbCommand,"?Address", user.Address);
            
            	Database.PutParameter(dbCommand,"?PhotoFileId", user.PhotoFileId);
            
            	Database.PutParameter(dbCommand,"?IsActive", user.IsActive);
            
            	Database.PutParameter(dbCommand,"?QbEmployeeRecordId", user.QbEmployeeRecordId);
            
            	Database.PutParameter(dbCommand,"?QbVendorRecordId", user.QbVendorRecordId);
            
            	Database.PutParameter(dbCommand,"?QbSalesRepRecordId", user.QbSalesRepRecordId);
            
            	Database.PutParameter(dbCommand,"?RoleName", user.RoleName);
            
            	Database.PutParameter(dbCommand,"?DateLastAccess", user.DateLastAccess);
            
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
        
          + " QbUserId, "
        
          + " Email, "
        
          + " Name, "
        
          + " FirstName, "
        
          + " LastName, "
        
          + " Phone, "
        
          + " Address, "
        
          + " PhotoFileId, "
        
          + " IsActive, "
        
          + " QbEmployeeRecordId, "
        
          + " QbVendorRecordId, "
        
          + " QbSalesRepRecordId, "
        
          + " RoleName, "
        
          + " DateLastAccess "
        
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
                  user.QbUserId = dataReader.GetString(1 + offset);
                  user.Email = dataReader.GetString(2 + offset);
                  
                    if(!dataReader.IsDBNull(3 + offset))
                    user.Name = dataReader.GetString(3 + offset);
                  
                    if(!dataReader.IsDBNull(4 + offset))
                    user.FirstName = dataReader.GetString(4 + offset);
                  
                    if(!dataReader.IsDBNull(5 + offset))
                    user.LastName = dataReader.GetString(5 + offset);
                  
                    if(!dataReader.IsDBNull(6 + offset))
                    user.Phone = dataReader.GetString(6 + offset);
                  
                    if(!dataReader.IsDBNull(7 + offset))
                    user.Address = dataReader.GetString(7 + offset);
                  
                    if(!dataReader.IsDBNull(8 + offset))
                    user.PhotoFileId = dataReader.GetInt32(8 + offset);
                  user.IsActive = dataReader.GetBoolean(9 + offset);
                  
                    if(!dataReader.IsDBNull(10 + offset))
                    user.QbEmployeeRecordId = dataReader.GetString(10 + offset);
                  
                    if(!dataReader.IsDBNull(11 + offset))
                    user.QbVendorRecordId = dataReader.GetString(11 + offset);
                  
                    if(!dataReader.IsDBNull(12 + offset))
                    user.QbSalesRepRecordId = dataReader.GetString(12 + offset);
                  user.RoleName = dataReader.GetString(13 + offset);
                  
                    if(!dataReader.IsDBNull(14 + offset))
                    user.DateLastAccess = dataReader.GetDateTime(14 + offset);
                  

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
              
                + " QbUserId, "
              
                + " Email, "
              
                + " Name, "
              
                + " FirstName, "
              
                + " LastName, "
              
                + " Phone, "
              
                + " Address, "
              
                + " PhotoFileId, "
              
                + " IsActive, "
              
                + " QbEmployeeRecordId, "
              
                + " QbVendorRecordId, "
              
                + " QbSalesRepRecordId, "
              
                + " RoleName, "
              
                + " DateLastAccess "
              
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
              
        protected String m_qbUserId;
              
        protected String m_email;
              
        protected String m_name;
              
        protected String m_firstName;
              
        protected String m_lastName;
              
        protected String m_phone;
              
        protected String m_address;
              
        protected int? m_photoFileId;
              
        protected bool m_isActive;
              
        protected String m_qbEmployeeRecordId;
              
        protected String m_qbVendorRecordId;
              
        protected String m_qbSalesRepRecordId;
              
        protected String m_roleName;
              
        protected DateTime? m_dateLastAccess;
              
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
                  id,String 
                  qbUserId,String 
                  email,String 
                  name,String 
                  firstName,String 
                  lastName,String 
                  phone,String 
                  address,int? 
                  photoFileId,bool 
                  isActive,String 
                  qbEmployeeRecordId,String 
                  qbVendorRecordId,String 
                  qbSalesRepRecordId,String 
                  roleName,DateTime? 
                  dateLastAccess
                ) : this()
        {
            
        	m_id = id;
            
        	m_qbUserId = qbUserId;
            
        	m_email = email;
            
        	m_name = name;
            
        	m_firstName = firstName;
            
        	m_lastName = lastName;
            
        	m_phone = phone;
            
        	m_address = address;
            
        	m_photoFileId = photoFileId;
            
        	m_isActive = isActive;
            
        	m_qbEmployeeRecordId = qbEmployeeRecordId;
            
        	m_qbVendorRecordId = qbVendorRecordId;
            
        	m_qbSalesRepRecordId = qbSalesRepRecordId;
            
        	m_roleName = roleName;
            
        	m_dateLastAccess = dateLastAccess;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public String QbUserId
        {
        	get { return m_qbUserId;}
            set { m_qbUserId = value; }
        }
        
        public String Email
        {
        	get { return m_email;}
            set { m_email = value; }
        }
        
        public String Name
        {
        	get { return m_name;}
            set { m_name = value; }
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
        
        public int? PhotoFileId
        {
        	get { return m_photoFileId;}
            set { m_photoFileId = value; }
        }
        
        public bool IsActive
        {
        	get { return m_isActive;}
            set { m_isActive = value; }
        }
        
        public String QbEmployeeRecordId
        {
        	get { return m_qbEmployeeRecordId;}
            set { m_qbEmployeeRecordId = value; }
        }
        
        public String QbVendorRecordId
        {
        	get { return m_qbVendorRecordId;}
            set { m_qbVendorRecordId = value; }
        }
        
        public String QbSalesRepRecordId
        {
        	get { return m_qbSalesRepRecordId;}
            set { m_qbSalesRepRecordId = value; }
        }
        
        public String RoleName
        {
        	get { return m_roleName;}
            set { m_roleName = value; }
        }
        
        public DateTime? DateLastAccess
        {
        	get { return m_dateLastAccess;}
            set { m_dateLastAccess = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 15; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    