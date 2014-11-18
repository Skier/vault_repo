
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Common.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class WebForm : ICloneable
    {

        #region Store


        #region Save

        public static WebForm Save(WebForm webForm, IDbConnection connection)
        {
        	if (!Exists(webForm, connection))
        		Insert(webForm, connection);
        	else
        		Update(webForm, connection);
        	return webForm;
        }

        public static WebForm Save(WebForm webForm)
        {
        	if (!Exists(webForm))
        		Insert(webForm);
        	else
        		Update(webForm);
        	return webForm;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into WebForm ( " +
        
          " CampagnId, " +
        
          " DateCreated, " +
        
          " FirstName, " +
        
          " LastName, " +
        
          " Phone, " +
        
          " Message, " +
        
          " WebPageUri " +
        
        ") Values (" +
        
          " ?CampagnId, " +
        
          " ?DateCreated, " +
        
          " ?FirstName, " +
        
          " ?LastName, " +
        
          " ?Phone, " +
        
          " ?Message, " +
        
          " ?WebPageUri " +
        
        ")";

        public static void Insert(WebForm webForm, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?CampagnId", webForm.CampagnId);
            
            	Database.PutParameter(dbCommand,"?DateCreated", webForm.DateCreated);
            
            	Database.PutParameter(dbCommand,"?FirstName", webForm.FirstName);
            
            	Database.PutParameter(dbCommand,"?LastName", webForm.LastName);
            
            	Database.PutParameter(dbCommand,"?Phone", webForm.Phone);
            
            	Database.PutParameter(dbCommand,"?Message", webForm.Message);
            
            	Database.PutParameter(dbCommand,"?WebPageUri", webForm.WebPageUri);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		webForm.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(WebForm webForm)
        {
          	Insert(webForm, null);
        }

        public static void Insert(List<WebForm>  webFormList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(WebForm webForm in  webFormList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?CampagnId", webForm.CampagnId);
                
                  	Database.PutParameter(dbCommand,"?DateCreated", webForm.DateCreated);
                
                  	Database.PutParameter(dbCommand,"?FirstName", webForm.FirstName);
                
                  	Database.PutParameter(dbCommand,"?LastName", webForm.LastName);
                
                  	Database.PutParameter(dbCommand,"?Phone", webForm.Phone);
                
                  	Database.PutParameter(dbCommand,"?Message", webForm.Message);
                
                  	Database.PutParameter(dbCommand,"?WebPageUri", webForm.WebPageUri);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?CampagnId",webForm.CampagnId);
                
                	Database.UpdateParameter(dbCommand,"?DateCreated",webForm.DateCreated);
                
                	Database.UpdateParameter(dbCommand,"?FirstName",webForm.FirstName);
                
                	Database.UpdateParameter(dbCommand,"?LastName",webForm.LastName);
                
                	Database.UpdateParameter(dbCommand,"?Phone",webForm.Phone);
                
                	Database.UpdateParameter(dbCommand,"?Message",webForm.Message);
                
                	Database.UpdateParameter(dbCommand,"?WebPageUri",webForm.WebPageUri);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	webForm.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<WebForm>  webFormList)
        {
        	Insert(webFormList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update WebForm Set "
          
            + " CampagnId = ?CampagnId, "
          
            + " DateCreated = ?DateCreated, "
          
            + " FirstName = ?FirstName, "
          
            + " LastName = ?LastName, "
          
            + " Phone = ?Phone, "
          
            + " Message = ?Message, "
          
            + " WebPageUri = ?WebPageUri "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(WebForm webForm, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", webForm.Id);
            
            	Database.PutParameter(dbCommand,"?CampagnId", webForm.CampagnId);
            
            	Database.PutParameter(dbCommand,"?DateCreated", webForm.DateCreated);
            
            	Database.PutParameter(dbCommand,"?FirstName", webForm.FirstName);
            
            	Database.PutParameter(dbCommand,"?LastName", webForm.LastName);
            
            	Database.PutParameter(dbCommand,"?Phone", webForm.Phone);
            
            	Database.PutParameter(dbCommand,"?Message", webForm.Message);
            
            	Database.PutParameter(dbCommand,"?WebPageUri", webForm.WebPageUri);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(WebForm webForm)
        {
          	Update(webForm, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " CampagnId, "
        
          + " DateCreated, "
        
          + " FirstName, "
        
          + " LastName, "
        
          + " Phone, "
        
          + " Message, "
        
          + " WebPageUri "
        
          + " From WebForm "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static WebForm FindByPrimaryKey(
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

            throw new DataNotFoundException("WebForm not found, search by primary key");
        }

        public static WebForm FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(WebForm webForm, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",webForm.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(WebForm webForm)
        {
        	return Exists(webForm, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from WebForm limit 1";

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

        public static WebForm Load(IDataReader dataReader, int offset)
        {
              WebForm webForm = new WebForm();

              webForm.Id = dataReader.GetInt32(0 + offset);
                  
                    if(!dataReader.IsDBNull(1 + offset))
                    webForm.CampagnId = dataReader.GetInt32(1 + offset);
                  webForm.DateCreated = dataReader.GetDateTime(2 + offset);
                  webForm.FirstName = dataReader.GetString(3 + offset);
                  webForm.LastName = dataReader.GetString(4 + offset);
                  webForm.Phone = dataReader.GetString(5 + offset);
                  webForm.Message = dataReader.GetString(6 + offset);
                  webForm.WebPageUri = dataReader.GetString(7 + offset);
                  

            return webForm;
        }

        public static WebForm Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From WebForm "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(WebForm webForm, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", webForm.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(WebForm webForm)
        {
        	Delete(webForm, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From WebForm ";

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
              
                + " CampagnId, "
              
                + " DateCreated, "
              
                + " FirstName, "
              
                + " LastName, "
              
                + " Phone, "
              
                + " Message, "
              
                + " WebPageUri "
              
                + " From WebForm ";

        public static List<WebForm> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<WebForm> rv = new List<WebForm>();

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

        public static List<WebForm> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<WebForm> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<WebForm> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(WebForm));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(WebForm item in itemsList)
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

        public static List<WebForm> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(WebForm));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<WebForm> itemsList = new List<WebForm>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is WebForm)
              		itemsList.Add(deserializedObject as WebForm);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected int? m_campagnId;
              
        protected DateTime m_dateCreated;
              
        protected String m_firstName;
              
        protected String m_lastName;
              
        protected String m_phone;
              
        protected String m_message;
              
        protected String m_webPageUri;
              
        #endregion

        #region Constructors

        public WebForm(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public WebForm(
                int 
                  id,int? 
                  campagnId,DateTime 
                  dateCreated,String 
                  firstName,String 
                  lastName,String 
                  phone,String 
                  message,String 
                  webPageUri
                ) : this()
        {
            
        	m_id = id;
            
        	m_campagnId = campagnId;
            
        	m_dateCreated = dateCreated;
            
        	m_firstName = firstName;
            
        	m_lastName = lastName;
            
        	m_phone = phone;
            
        	m_message = message;
            
        	m_webPageUri = webPageUri;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public int? CampagnId
        {
        	get { return m_campagnId;}
            set { m_campagnId = value; }
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
        
        public String Message
        {
        	get { return m_message;}
            set { m_message = value; }
        }
        
        public String WebPageUri
        {
        	get { return m_webPageUri;}
            set { m_webPageUri = value; }
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

    