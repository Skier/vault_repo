
    using System;
    using System.Data;
    using System.Collections.Generic;
    using Dalworth.Server.Data;
    using Dalworth.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Dalworth.Server.Domain
      {


      public partial class Message : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Message ( " +
      
        " EmployeeId, " +
      
        " MessageTypeId, " +
      
        " VisitId, " +
      
        " Notes " +
      
      ") Values (" +
      
        " ?EmployeeId, " +
      
        " ?MessageTypeId, " +
      
        " ?VisitId, " +
      
        " ?Notes " +
      
      ")";

      public static void Insert(Message message, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?EmployeeId", message.EmployeeId);
      
        Database.PutParameter(dbCommand,"?MessageTypeId", message.MessageTypeId);
      
        Database.PutParameter(dbCommand,"?VisitId", message.VisitId);
      
        Database.PutParameter(dbCommand,"?Notes", message.Notes);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        message.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(Message message)
      {
        Insert(message, null);
      }


      public static void Insert(List<Message>  messageList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Message message in  messageList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?EmployeeId", message.EmployeeId);
      
        Database.PutParameter(dbCommand,"?MessageTypeId", message.MessageTypeId);
      
        Database.PutParameter(dbCommand,"?VisitId", message.VisitId);
      
        Database.PutParameter(dbCommand,"?Notes", message.Notes);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?EmployeeId",message.EmployeeId);
      
        Database.UpdateParameter(dbCommand,"?MessageTypeId",message.MessageTypeId);
      
        Database.UpdateParameter(dbCommand,"?VisitId",message.VisitId);
      
        Database.UpdateParameter(dbCommand,"?Notes",message.Notes);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        message.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<Message>  messageList)
      {
        Insert(messageList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Message Set "
      
        + " EmployeeId = ?EmployeeId, "
      
        + " MessageTypeId = ?MessageTypeId, "
      
        + " VisitId = ?VisitId, "
      
        + " Notes = ?Notes "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(Message message, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", message.ID);
      
        Database.PutParameter(dbCommand,"?EmployeeId", message.EmployeeId);
      
        Database.PutParameter(dbCommand,"?MessageTypeId", message.MessageTypeId);
      
        Database.PutParameter(dbCommand,"?VisitId", message.VisitId);
      
        Database.PutParameter(dbCommand,"?Notes", message.Notes);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Message message)
      {
        Update(message, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " EmployeeId, "
      
        + " MessageTypeId, "
      
        + " VisitId, "
      
        + " Notes "
      

      + " From Message "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static Message FindByPrimaryKey(
      int iD, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", iD);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Message not found, search by primary key");

      }

      public static Message FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Message message, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",message.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Message message)
      {
      return Exists(message, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Message limit 1";

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

      public static Message Load(IDataReader dataReader, int offset)
      {
      Message message = new Message();

      message.ID = dataReader.GetInt32(0 + offset);
          message.EmployeeId = dataReader.GetInt32(1 + offset);
          message.MessageTypeId = dataReader.GetInt32(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            message.VisitId = dataReader.GetInt32(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            message.Notes = dataReader.GetString(4 + offset);
          

      return message;
      }

      public static Message Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Message "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(Message message, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", message.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Message message)
      {
        Delete(message, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Message ";

      public static void Clear(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll, connection))
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

      
        + " ID, "
      
        + " EmployeeId, "
      
        + " MessageTypeId, "
      
        + " VisitId, "
      
        + " Notes "
      

      + " From Message ";
      public static List<Message> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Message> rv = new List<Message>();

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

      public static List<Message> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Message> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (Message obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && EmployeeId == obj.EmployeeId && MessageTypeId == obj.MessageTypeId && VisitId == obj.VisitId && Notes == obj.Notes;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<Message> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Message));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Message item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Message>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Message));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Message> itemsList
      = new List<Message>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Message)
      itemsList.Add(deserializedObject as Message);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_employeeId;
      
        protected int m_messageTypeId;
      
        protected int? m_visitId;
      
        protected String m_notes;
      
      #endregion

      #region Constructors
      public Message(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public Message(
        int 
          iD,int 
          employeeId,int 
          messageTypeId,int? 
          visitId,String 
          notes
        ) : this()
        {
        
          m_iD = iD;
        
          m_employeeId = employeeId;
        
          m_messageTypeId = messageTypeId;
        
          m_visitId = visitId;
        
          m_notes = notes;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int EmployeeId
        {
        get { return m_employeeId;}
        set { m_employeeId = value; }
        }
      
        [XmlElement]
        public int MessageTypeId
        {
        get { return m_messageTypeId;}
        set { m_messageTypeId = value; }
        }
      
        [XmlElement]
        public int? VisitId
        {
        get { return m_visitId;}
        set { m_visitId = value; }
        }
      
        [XmlElement]
        public String Notes
        {
        get { return m_notes;}
        set { m_notes = value; }
        }
      

      public static int FieldsCount
      {
      get { return 5; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    