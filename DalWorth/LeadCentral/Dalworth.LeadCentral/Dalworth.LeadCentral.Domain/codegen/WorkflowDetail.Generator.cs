
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class WorkflowDetail : ICloneable
    {

        #region Store


        #region Save

        public static WorkflowDetail Save(WorkflowDetail workflowDetail, IDbConnection connection)
        {
        	if (!Exists(workflowDetail, connection))
        		Insert(workflowDetail, connection);
        	else
        		Update(workflowDetail, connection);
        	return workflowDetail;
        }

        public static WorkflowDetail Save(WorkflowDetail workflowDetail)
        {
        	if (!Exists(workflowDetail))
        		Insert(workflowDetail);
        	else
        		Update(workflowDetail);
        	return workflowDetail;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into WorkflowDetail ( " +
        
          " CallWorkflowId, " +
        
          " PropertyName, " +
        
          " PropertyValue " +
        
        ") Values (" +
        
          " ?CallWorkflowId, " +
        
          " ?PropertyName, " +
        
          " ?PropertyValue " +
        
        ")";

        public static void Insert(WorkflowDetail workflowDetail, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?CallWorkflowId", workflowDetail.CallWorkflowId);
            
            	Database.PutParameter(dbCommand,"?PropertyName", workflowDetail.PropertyName);
            
            	Database.PutParameter(dbCommand,"?PropertyValue", workflowDetail.PropertyValue);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		workflowDetail.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(WorkflowDetail workflowDetail)
        {
          	Insert(workflowDetail, null);
        }

        public static void Insert(List<WorkflowDetail>  workflowDetailList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(WorkflowDetail workflowDetail in  workflowDetailList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?CallWorkflowId", workflowDetail.CallWorkflowId);
                
                  	Database.PutParameter(dbCommand,"?PropertyName", workflowDetail.PropertyName);
                
                  	Database.PutParameter(dbCommand,"?PropertyValue", workflowDetail.PropertyValue);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?CallWorkflowId",workflowDetail.CallWorkflowId);
                
                	Database.UpdateParameter(dbCommand,"?PropertyName",workflowDetail.PropertyName);
                
                	Database.UpdateParameter(dbCommand,"?PropertyValue",workflowDetail.PropertyValue);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	workflowDetail.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<WorkflowDetail>  workflowDetailList)
        {
        	Insert(workflowDetailList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update WorkflowDetail Set "
          
            + " CallWorkflowId = ?CallWorkflowId, "
          
            + " PropertyName = ?PropertyName, "
          
            + " PropertyValue = ?PropertyValue "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(WorkflowDetail workflowDetail, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", workflowDetail.Id);
            
            	Database.PutParameter(dbCommand,"?CallWorkflowId", workflowDetail.CallWorkflowId);
            
            	Database.PutParameter(dbCommand,"?PropertyName", workflowDetail.PropertyName);
            
            	Database.PutParameter(dbCommand,"?PropertyValue", workflowDetail.PropertyValue);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(WorkflowDetail workflowDetail)
        {
          	Update(workflowDetail, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " CallWorkflowId, "
        
          + " PropertyName, "
        
          + " PropertyValue "
        
          + " From WorkflowDetail "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static WorkflowDetail FindByPrimaryKey(
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

            throw new DataNotFoundException("WorkflowDetail not found, search by primary key");
        }

        public static WorkflowDetail FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(WorkflowDetail workflowDetail, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",workflowDetail.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(WorkflowDetail workflowDetail)
        {
        	return Exists(workflowDetail, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from WorkflowDetail limit 1";

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

        public static WorkflowDetail Load(IDataReader dataReader, int offset)
        {
              WorkflowDetail workflowDetail = new WorkflowDetail();

              workflowDetail.Id = dataReader.GetInt32(0 + offset);
                  workflowDetail.CallWorkflowId = dataReader.GetInt32(1 + offset);
                  workflowDetail.PropertyName = dataReader.GetString(2 + offset);
                  workflowDetail.PropertyValue = dataReader.GetString(3 + offset);
                  

            return workflowDetail;
        }

        public static WorkflowDetail Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From WorkflowDetail "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(WorkflowDetail workflowDetail, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", workflowDetail.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(WorkflowDetail workflowDetail)
        {
        	Delete(workflowDetail, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From WorkflowDetail ";

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
              
                + " CallWorkflowId, "
              
                + " PropertyName, "
              
                + " PropertyValue "
              
                + " From WorkflowDetail ";

        public static List<WorkflowDetail> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<WorkflowDetail> rv = new List<WorkflowDetail>();

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

        public static List<WorkflowDetail> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<WorkflowDetail> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<WorkflowDetail> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkflowDetail));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(WorkflowDetail item in itemsList)
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

        public static List<WorkflowDetail> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkflowDetail));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<WorkflowDetail> itemsList = new List<WorkflowDetail>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is WorkflowDetail)
              		itemsList.Add(deserializedObject as WorkflowDetail);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected int m_callWorkflowId;
              
        protected String m_propertyName;
              
        protected String m_propertyValue;
              
        #endregion

        #region Constructors

        public WorkflowDetail(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public WorkflowDetail(
                int 
                  id,int 
                  callWorkflowId,String 
                  propertyName,String 
                  propertyValue
                ) : this()
        {
            
        	m_id = id;
            
        	m_callWorkflowId = callWorkflowId;
            
        	m_propertyName = propertyName;
            
        	m_propertyValue = propertyValue;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public int CallWorkflowId
        {
        	get { return m_callWorkflowId;}
            set { m_callWorkflowId = value; }
        }
        
        public String PropertyName
        {
        	get { return m_propertyName;}
            set { m_propertyName = value; }
        }
        
        public String PropertyValue
        {
        	get { return m_propertyValue;}
            set { m_propertyValue = value; }
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

    