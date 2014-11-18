
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Common.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class NotificationType : ICloneable
    {

        #region Store


        #region Save

        public static NotificationType Save(NotificationType notificationType, IDbConnection connection)
        {
        	if (!Exists(notificationType, connection))
        		Insert(notificationType, connection);
        	else
        		Update(notificationType, connection);
        	return notificationType;
        }

        public static NotificationType Save(NotificationType notificationType)
        {
        	if (!Exists(notificationType))
        		Insert(notificationType);
        	else
        		Update(notificationType);
        	return notificationType;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into NotificationType ( " +
        
          " TypeName, " +
        
          " SendToAdmin, " +
        
          " SendToPartner, " +
        
          " SendToPartnerUsers, " +
        
          " SendToStaff, " +
        
          " SendToSalesRep, " +
        
          " SendToAccountant " +
        
        ") Values (" +
        
          " ?TypeName, " +
        
          " ?SendToAdmin, " +
        
          " ?SendToPartner, " +
        
          " ?SendToPartnerUsers, " +
        
          " ?SendToStaff, " +
        
          " ?SendToSalesRep, " +
        
          " ?SendToAccountant " +
        
        ")";

        public static void Insert(NotificationType notificationType, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?TypeName", notificationType.TypeName);
            
            	Database.PutParameter(dbCommand,"?SendToAdmin", notificationType.SendToAdmin);
            
            	Database.PutParameter(dbCommand,"?SendToPartner", notificationType.SendToPartner);
            
            	Database.PutParameter(dbCommand,"?SendToPartnerUsers", notificationType.SendToPartnerUsers);
            
            	Database.PutParameter(dbCommand,"?SendToStaff", notificationType.SendToStaff);
            
            	Database.PutParameter(dbCommand,"?SendToSalesRep", notificationType.SendToSalesRep);
            
            	Database.PutParameter(dbCommand,"?SendToAccountant", notificationType.SendToAccountant);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		notificationType.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(NotificationType notificationType)
        {
          	Insert(notificationType, null);
        }

        public static void Insert(List<NotificationType>  notificationTypeList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(NotificationType notificationType in  notificationTypeList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?TypeName", notificationType.TypeName);
                
                  	Database.PutParameter(dbCommand,"?SendToAdmin", notificationType.SendToAdmin);
                
                  	Database.PutParameter(dbCommand,"?SendToPartner", notificationType.SendToPartner);
                
                  	Database.PutParameter(dbCommand,"?SendToPartnerUsers", notificationType.SendToPartnerUsers);
                
                  	Database.PutParameter(dbCommand,"?SendToStaff", notificationType.SendToStaff);
                
                  	Database.PutParameter(dbCommand,"?SendToSalesRep", notificationType.SendToSalesRep);
                
                  	Database.PutParameter(dbCommand,"?SendToAccountant", notificationType.SendToAccountant);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?TypeName",notificationType.TypeName);
                
                	Database.UpdateParameter(dbCommand,"?SendToAdmin",notificationType.SendToAdmin);
                
                	Database.UpdateParameter(dbCommand,"?SendToPartner",notificationType.SendToPartner);
                
                	Database.UpdateParameter(dbCommand,"?SendToPartnerUsers",notificationType.SendToPartnerUsers);
                
                	Database.UpdateParameter(dbCommand,"?SendToStaff",notificationType.SendToStaff);
                
                	Database.UpdateParameter(dbCommand,"?SendToSalesRep",notificationType.SendToSalesRep);
                
                	Database.UpdateParameter(dbCommand,"?SendToAccountant",notificationType.SendToAccountant);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	notificationType.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<NotificationType>  notificationTypeList)
        {
        	Insert(notificationTypeList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update NotificationType Set "
          
            + " TypeName = ?TypeName, "
          
            + " SendToAdmin = ?SendToAdmin, "
          
            + " SendToPartner = ?SendToPartner, "
          
            + " SendToPartnerUsers = ?SendToPartnerUsers, "
          
            + " SendToStaff = ?SendToStaff, "
          
            + " SendToSalesRep = ?SendToSalesRep, "
          
            + " SendToAccountant = ?SendToAccountant "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(NotificationType notificationType, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", notificationType.Id);
            
            	Database.PutParameter(dbCommand,"?TypeName", notificationType.TypeName);
            
            	Database.PutParameter(dbCommand,"?SendToAdmin", notificationType.SendToAdmin);
            
            	Database.PutParameter(dbCommand,"?SendToPartner", notificationType.SendToPartner);
            
            	Database.PutParameter(dbCommand,"?SendToPartnerUsers", notificationType.SendToPartnerUsers);
            
            	Database.PutParameter(dbCommand,"?SendToStaff", notificationType.SendToStaff);
            
            	Database.PutParameter(dbCommand,"?SendToSalesRep", notificationType.SendToSalesRep);
            
            	Database.PutParameter(dbCommand,"?SendToAccountant", notificationType.SendToAccountant);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(NotificationType notificationType)
        {
          	Update(notificationType, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " TypeName, "
        
          + " SendToAdmin, "
        
          + " SendToPartner, "
        
          + " SendToPartnerUsers, "
        
          + " SendToStaff, "
        
          + " SendToSalesRep, "
        
          + " SendToAccountant "
        
          + " From NotificationType "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static NotificationType FindByPrimaryKey(
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

            throw new DataNotFoundException("NotificationType not found, search by primary key");
        }

        public static NotificationType FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(NotificationType notificationType, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",notificationType.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(NotificationType notificationType)
        {
        	return Exists(notificationType, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from NotificationType limit 1";

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

        public static NotificationType Load(IDataReader dataReader, int offset)
        {
              NotificationType notificationType = new NotificationType();

              notificationType.Id = dataReader.GetInt32(0 + offset);
                  notificationType.TypeName = dataReader.GetString(1 + offset);
                  notificationType.SendToAdmin = dataReader.GetBoolean(2 + offset);
                  notificationType.SendToPartner = dataReader.GetBoolean(3 + offset);
                  notificationType.SendToPartnerUsers = dataReader.GetBoolean(4 + offset);
                  notificationType.SendToStaff = dataReader.GetBoolean(5 + offset);
                  notificationType.SendToSalesRep = dataReader.GetBoolean(6 + offset);
                  notificationType.SendToAccountant = dataReader.GetBoolean(7 + offset);
                  

            return notificationType;
        }

        public static NotificationType Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From NotificationType "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(NotificationType notificationType, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", notificationType.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(NotificationType notificationType)
        {
        	Delete(notificationType, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From NotificationType ";

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
              
                + " TypeName, "
              
                + " SendToAdmin, "
              
                + " SendToPartner, "
              
                + " SendToPartnerUsers, "
              
                + " SendToStaff, "
              
                + " SendToSalesRep, "
              
                + " SendToAccountant "
              
                + " From NotificationType ";

        public static List<NotificationType> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<NotificationType> rv = new List<NotificationType>();

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

        public static List<NotificationType> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<NotificationType> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<NotificationType> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(NotificationType));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(NotificationType item in itemsList)
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

        public static List<NotificationType> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(NotificationType));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<NotificationType> itemsList = new List<NotificationType>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is NotificationType)
              		itemsList.Add(deserializedObject as NotificationType);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected String m_typeName;
              
        protected bool m_sendToAdmin;
              
        protected bool m_sendToPartner;
              
        protected bool m_sendToPartnerUsers;
              
        protected bool m_sendToStaff;
              
        protected bool m_sendToSalesRep;
              
        protected bool m_sendToAccountant;
              
        #endregion

        #region Constructors

        public NotificationType(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public NotificationType(
                int 
                  id,String 
                  typeName,bool 
                  sendToAdmin,bool 
                  sendToPartner,bool 
                  sendToPartnerUsers,bool 
                  sendToStaff,bool 
                  sendToSalesRep,bool 
                  sendToAccountant
                ) : this()
        {
            
        	m_id = id;
            
        	m_typeName = typeName;
            
        	m_sendToAdmin = sendToAdmin;
            
        	m_sendToPartner = sendToPartner;
            
        	m_sendToPartnerUsers = sendToPartnerUsers;
            
        	m_sendToStaff = sendToStaff;
            
        	m_sendToSalesRep = sendToSalesRep;
            
        	m_sendToAccountant = sendToAccountant;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public String TypeName
        {
        	get { return m_typeName;}
            set { m_typeName = value; }
        }
        
        public bool SendToAdmin
        {
        	get { return m_sendToAdmin;}
            set { m_sendToAdmin = value; }
        }
        
        public bool SendToPartner
        {
        	get { return m_sendToPartner;}
            set { m_sendToPartner = value; }
        }
        
        public bool SendToPartnerUsers
        {
        	get { return m_sendToPartnerUsers;}
            set { m_sendToPartnerUsers = value; }
        }
        
        public bool SendToStaff
        {
        	get { return m_sendToStaff;}
            set { m_sendToStaff = value; }
        }
        
        public bool SendToSalesRep
        {
        	get { return m_sendToSalesRep;}
            set { m_sendToSalesRep = value; }
        }
        
        public bool SendToAccountant
        {
        	get { return m_sendToAccountant;}
            set { m_sendToAccountant = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 8; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    