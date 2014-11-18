
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class CallWorkflow : ICloneable
    {

        #region Store


        #region Save

        public static CallWorkflow Save(CallWorkflow callWorkflow, IDbConnection connection)
        {
        	if (!Exists(callWorkflow, connection))
        		Insert(callWorkflow, connection);
        	else
        		Update(callWorkflow, connection);
        	return callWorkflow;
        }

        public static CallWorkflow Save(CallWorkflow callWorkflow)
        {
        	if (!Exists(callWorkflow))
        		Insert(callWorkflow);
        	else
        		Update(callWorkflow);
        	return callWorkflow;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into CallWorkflow ( " +
        
          " Description " +
        
        ") Values (" +
        
          " ?Description " +
        
        ")";

        public static void Insert(CallWorkflow callWorkflow, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Description", callWorkflow.Description);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		callWorkflow.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(CallWorkflow callWorkflow)
        {
          	Insert(callWorkflow, null);
        }

        public static void Insert(List<CallWorkflow>  callWorkflowList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(CallWorkflow callWorkflow in  callWorkflowList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?Description", callWorkflow.Description);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?Description",callWorkflow.Description);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	callWorkflow.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<CallWorkflow>  callWorkflowList)
        {
        	Insert(callWorkflowList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update CallWorkflow Set "
          
            + " Description = ?Description "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(CallWorkflow callWorkflow, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", callWorkflow.Id);
            
            	Database.PutParameter(dbCommand,"?Description", callWorkflow.Description);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(CallWorkflow callWorkflow)
        {
          	Update(callWorkflow, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " Description "
        
          + " From CallWorkflow "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static CallWorkflow FindByPrimaryKey(
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

            throw new DataNotFoundException("CallWorkflow not found, search by primary key");
        }

        public static CallWorkflow FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(CallWorkflow callWorkflow, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",callWorkflow.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(CallWorkflow callWorkflow)
        {
        	return Exists(callWorkflow, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from CallWorkflow limit 1";

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

        public static CallWorkflow Load(IDataReader dataReader, int offset)
        {
              CallWorkflow callWorkflow = new CallWorkflow();

              callWorkflow.Id = dataReader.GetInt32(0 + offset);
                  
                    if(!dataReader.IsDBNull(1 + offset))
                    callWorkflow.Description = dataReader.GetString(1 + offset);
                  

            return callWorkflow;
        }

        public static CallWorkflow Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From CallWorkflow "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(CallWorkflow callWorkflow, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", callWorkflow.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(CallWorkflow callWorkflow)
        {
        	Delete(callWorkflow, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From CallWorkflow ";

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
              
                + " Description "
              
                + " From CallWorkflow ";

        public static List<CallWorkflow> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<CallWorkflow> rv = new List<CallWorkflow>();

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

        public static List<CallWorkflow> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<CallWorkflow> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<CallWorkflow> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CallWorkflow));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(CallWorkflow item in itemsList)
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

        public static List<CallWorkflow> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(CallWorkflow));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<CallWorkflow> itemsList = new List<CallWorkflow>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is CallWorkflow)
              		itemsList.Add(deserializedObject as CallWorkflow);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected String m_description;
              
        #endregion

        #region Constructors

        public CallWorkflow(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public CallWorkflow(
                int 
                  id,String 
                  description
                ) : this()
        {
            
        	m_id = id;
            
        	m_description = description;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public String Description
        {
        	get { return m_description;}
            set { m_description = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 2; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    