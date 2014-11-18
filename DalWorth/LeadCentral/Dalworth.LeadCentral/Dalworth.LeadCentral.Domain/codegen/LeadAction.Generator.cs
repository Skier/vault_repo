
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class LeadAction : ICloneable
    {

        #region Store


        #region Save

        public static LeadAction Save(LeadAction leadAction, IDbConnection connection)
        {
        	if (!Exists(leadAction, connection))
        		Insert(leadAction, connection);
        	else
        		Update(leadAction, connection);
        	return leadAction;
        }

        public static LeadAction Save(LeadAction leadAction)
        {
        	if (!Exists(leadAction))
        		Insert(leadAction);
        	else
        		Update(leadAction);
        	return leadAction;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into LeadAction ( " +
        
          " Sequence, " +
        
          " FromLeadStatusId, " +
        
          " ToLeadStatusId, " +
        
          " Message " +
        
        ") Values (" +
        
          " ?Sequence, " +
        
          " ?FromLeadStatusId, " +
        
          " ?ToLeadStatusId, " +
        
          " ?Message " +
        
        ")";

        public static void Insert(LeadAction leadAction, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Sequence", leadAction.Sequence);
            
            	Database.PutParameter(dbCommand,"?FromLeadStatusId", leadAction.FromLeadStatusId);
            
            	Database.PutParameter(dbCommand,"?ToLeadStatusId", leadAction.ToLeadStatusId);
            
            	Database.PutParameter(dbCommand,"?Message", leadAction.Message);
            
            	dbCommand.ExecuteNonQuery();
            
            }
        }

        public static void Insert(LeadAction leadAction)
        {
          	Insert(leadAction, null);
        }

        public static void Insert(List<LeadAction>  leadActionList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(LeadAction leadAction in  leadActionList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?Sequence", leadAction.Sequence);
                
                  	Database.PutParameter(dbCommand,"?FromLeadStatusId", leadAction.FromLeadStatusId);
                
                  	Database.PutParameter(dbCommand,"?ToLeadStatusId", leadAction.ToLeadStatusId);
                
                  	Database.PutParameter(dbCommand,"?Message", leadAction.Message);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?Sequence",leadAction.Sequence);
                
                	Database.UpdateParameter(dbCommand,"?FromLeadStatusId",leadAction.FromLeadStatusId);
                
                	Database.UpdateParameter(dbCommand,"?ToLeadStatusId",leadAction.ToLeadStatusId);
                
                	Database.UpdateParameter(dbCommand,"?Message",leadAction.Message);
                
                }

                dbCommand.ExecuteNonQuery();

                
                }
            }
        }

        public static void Insert(List<LeadAction>  leadActionList)
        {
        	Insert(leadActionList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update LeadAction Set "
          
            + " ToLeadStatusId = ?ToLeadStatusId, "
          
            + " Message = ?Message "
          
            + " Where "
            
            + " Sequence = ?Sequence and  "
            
            + " FromLeadStatusId = ?FromLeadStatusId "
            ;

        public static void Update(LeadAction leadAction, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Sequence", leadAction.Sequence);
            
            	Database.PutParameter(dbCommand,"?FromLeadStatusId", leadAction.FromLeadStatusId);
            
            	Database.PutParameter(dbCommand,"?ToLeadStatusId", leadAction.ToLeadStatusId);
            
            	Database.PutParameter(dbCommand,"?Message", leadAction.Message);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(LeadAction leadAction)
        {
          	Update(leadAction, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Sequence, "
        
          + " FromLeadStatusId, "
        
          + " ToLeadStatusId, "
        
          + " Message "
        
          + " From LeadAction "
        
          + " Where "
          
          + " Sequence = ?Sequence and  "
          
          + " FromLeadStatusId = ?FromLeadStatusId "
          ;

        public static LeadAction FindByPrimaryKey(
              int sequence,int fromLeadStatusId, IDbConnection connection
              )
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
              
            	Database.PutParameter(dbCommand,"?Sequence", sequence);
              
            	Database.PutParameter(dbCommand,"?FromLeadStatusId", fromLeadStatusId);
              

              	using(IDataReader dataReader = dbCommand.ExecuteReader())
              	{
              		if(dataReader.Read())
              			return Load(dataReader);
              	}
            }

            throw new DataNotFoundException("LeadAction not found, search by primary key");
        }

        public static LeadAction FindByPrimaryKey(
              int sequence,int fromLeadStatusId
              )
        {
        	return FindByPrimaryKey(
              sequence,fromLeadStatusId, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(LeadAction leadAction, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Sequence",leadAction.Sequence);
            
              	Database.PutParameter(dbCommand,"?FromLeadStatusId",leadAction.FromLeadStatusId);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(LeadAction leadAction)
        {
        	return Exists(leadAction, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from LeadAction limit 1";

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

        public static LeadAction Load(IDataReader dataReader, int offset)
        {
              LeadAction leadAction = new LeadAction();

              leadAction.Sequence = dataReader.GetInt32(0 + offset);
                  leadAction.FromLeadStatusId = dataReader.GetInt32(1 + offset);
                  
                    if(!dataReader.IsDBNull(2 + offset))
                    leadAction.ToLeadStatusId = dataReader.GetInt32(2 + offset);
                  
                    if(!dataReader.IsDBNull(3 + offset))
                    leadAction.Message = dataReader.GetString(3 + offset);
                  

            return leadAction;
        }

        public static LeadAction Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From LeadAction "

              
                + " Where "
                
                  + " Sequence = ?Sequence and  "
                
                  + " FromLeadStatusId = ?FromLeadStatusId "
                ;

        public static void Delete(LeadAction leadAction, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Sequence", leadAction.Sequence);
              
            	Database.PutParameter(dbCommand,"?FromLeadStatusId", leadAction.FromLeadStatusId);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(LeadAction leadAction)
        {
        	Delete(leadAction, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From LeadAction ";

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
              
                + " Sequence, "
              
                + " FromLeadStatusId, "
              
                + " ToLeadStatusId, "
              
                + " Message "
              
                + " From LeadAction ";

        public static List<LeadAction> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<LeadAction> rv = new List<LeadAction>();

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

        public static List<LeadAction> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<LeadAction> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<LeadAction> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(LeadAction));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(LeadAction item in itemsList)
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

        public static List<LeadAction> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(LeadAction));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<LeadAction> itemsList = new List<LeadAction>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is LeadAction)
              		itemsList.Add(deserializedObject as LeadAction);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_sequence;
              
        protected int m_fromLeadStatusId;
              
        protected int? m_toLeadStatusId;
              
        protected String m_message;
              
        #endregion

        #region Constructors

        public LeadAction(
              int 
                  sequence,int 
                  fromLeadStatusId
              ) : this()
        {
            
        	m_sequence = sequence;
            
        	m_fromLeadStatusId = fromLeadStatusId;
            
        }

        

        public LeadAction(
                int 
                  sequence,int 
                  fromLeadStatusId,int? 
                  toLeadStatusId,String 
                  message
                ) : this()
        {
            
        	m_sequence = sequence;
            
        	m_fromLeadStatusId = fromLeadStatusId;
            
        	m_toLeadStatusId = toLeadStatusId;
            
        	m_message = message;
            
        }

        

        #endregion

        
        public int Sequence
        {
        	get { return m_sequence;}
            set { m_sequence = value; }
        }
        
        public int FromLeadStatusId
        {
        	get { return m_fromLeadStatusId;}
            set { m_fromLeadStatusId = value; }
        }
        
        public int? ToLeadStatusId
        {
        	get { return m_toLeadStatusId;}
            set { m_toLeadStatusId = value; }
        }
        
        public String Message
        {
        	get { return m_message;}
            set { m_message = value; }
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

    