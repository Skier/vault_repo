
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


      public partial class WorkEquipment : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into WorkEquipment ( " +
      
        " WorkId, " +
      
        " EquipmentTypeId, " +
      
        " Quantity " +
      
      ") Values (" +
      
        " ?WorkId, " +
      
        " ?EquipmentTypeId, " +
      
        " ?Quantity " +
      
      ")";

      public static void Insert(WorkEquipment workEquipment, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkId", workEquipment.WorkId);
      
        Database.PutParameter(dbCommand,"?EquipmentTypeId", workEquipment.EquipmentTypeId);
      
        Database.PutParameter(dbCommand,"?Quantity", workEquipment.Quantity);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        workEquipment.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(WorkEquipment workEquipment)
      {
        Insert(workEquipment, null);
      }


      public static void Insert(List<WorkEquipment>  workEquipmentList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(WorkEquipment workEquipment in  workEquipmentList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?WorkId", workEquipment.WorkId);
      
        Database.PutParameter(dbCommand,"?EquipmentTypeId", workEquipment.EquipmentTypeId);
      
        Database.PutParameter(dbCommand,"?Quantity", workEquipment.Quantity);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?WorkId",workEquipment.WorkId);
      
        Database.UpdateParameter(dbCommand,"?EquipmentTypeId",workEquipment.EquipmentTypeId);
      
        Database.UpdateParameter(dbCommand,"?Quantity",workEquipment.Quantity);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        workEquipment.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<WorkEquipment>  workEquipmentList)
      {
        Insert(workEquipmentList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update WorkEquipment Set "
      
        + " WorkId = ?WorkId, "
      
        + " EquipmentTypeId = ?EquipmentTypeId, "
      
        + " Quantity = ?Quantity "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(WorkEquipment workEquipment, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", workEquipment.ID);
      
        Database.PutParameter(dbCommand,"?WorkId", workEquipment.WorkId);
      
        Database.PutParameter(dbCommand,"?EquipmentTypeId", workEquipment.EquipmentTypeId);
      
        Database.PutParameter(dbCommand,"?Quantity", workEquipment.Quantity);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WorkEquipment workEquipment)
      {
        Update(workEquipment, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " WorkId, "
      
        + " EquipmentTypeId, "
      
        + " Quantity "
      

      + " From WorkEquipment "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static WorkEquipment FindByPrimaryKey(
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
      throw new DataNotFoundException("WorkEquipment not found, search by primary key");

      }

      public static WorkEquipment FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(WorkEquipment workEquipment, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",workEquipment.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WorkEquipment workEquipment)
      {
      return Exists(workEquipment, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from WorkEquipment limit 1";

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

      public static WorkEquipment Load(IDataReader dataReader, int offset)
      {
      WorkEquipment workEquipment = new WorkEquipment();

      workEquipment.ID = dataReader.GetInt32(0 + offset);
          workEquipment.WorkId = dataReader.GetInt32(1 + offset);
          workEquipment.EquipmentTypeId = dataReader.GetInt32(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            workEquipment.Quantity = dataReader.GetInt32(3 + offset);
          

      return workEquipment;
      }

      public static WorkEquipment Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From WorkEquipment "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(WorkEquipment workEquipment, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", workEquipment.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WorkEquipment workEquipment)
      {
        Delete(workEquipment, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From WorkEquipment ";

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
      
        + " WorkId, "
      
        + " EquipmentTypeId, "
      
        + " Quantity "
      

      + " From WorkEquipment ";
      public static List<WorkEquipment> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<WorkEquipment> rv = new List<WorkEquipment>();

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

      public static List<WorkEquipment> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WorkEquipment> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (WorkEquipment obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && WorkId == obj.WorkId && EquipmentTypeId == obj.EquipmentTypeId && Quantity == obj.Quantity;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<WorkEquipment> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkEquipment));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WorkEquipment item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WorkEquipment>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkEquipment));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WorkEquipment> itemsList
      = new List<WorkEquipment>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WorkEquipment)
      itemsList.Add(deserializedObject as WorkEquipment);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_workId;
      
        protected int m_equipmentTypeId;
      
        protected int? m_quantity;
      
      #endregion

      #region Constructors
      public WorkEquipment(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public WorkEquipment(
        int 
          iD,int 
          workId,int 
          equipmentTypeId,int? 
          quantity
        ) : this()
        {
        
          m_iD = iD;
        
          m_workId = workId;
        
          m_equipmentTypeId = equipmentTypeId;
        
          m_quantity = quantity;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int WorkId
        {
        get { return m_workId;}
        set { m_workId = value; }
        }
      
        [XmlElement]
        public int EquipmentTypeId
        {
        get { return m_equipmentTypeId;}
        set { m_equipmentTypeId = value; }
        }
      
        [XmlElement]
        public int? Quantity
        {
        get { return m_quantity;}
        set { m_quantity = value; }
        }
      

      public static int FieldsCount
      {
      get { return 4; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    