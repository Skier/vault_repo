
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


      public partial class WorkTransactionVanCheck : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into WorkTransactionVanCheck ( " +
      
        " WorkTransactionId, " +
      
        " OilChecked, " +
      
        " UnitClean, " +
      
        " VanClean, " +
      
        " SuppliesStocked, " +
      
        " OdometerReading, " +
      
        " HobbsReading, " +
      
        " SpecialNeeds " +
      
      ") Values (" +
      
        " ?WorkTransactionId, " +
      
        " ?OilChecked, " +
      
        " ?UnitClean, " +
      
        " ?VanClean, " +
      
        " ?SuppliesStocked, " +
      
        " ?OdometerReading, " +
      
        " ?HobbsReading, " +
      
        " ?SpecialNeeds " +
      
      ")";

      public static void Insert(WorkTransactionVanCheck workTransactionVanCheck, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionVanCheck.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?OilChecked", workTransactionVanCheck.OilChecked);
      
        Database.PutParameter(dbCommand,"?UnitClean", workTransactionVanCheck.UnitClean);
      
        Database.PutParameter(dbCommand,"?VanClean", workTransactionVanCheck.VanClean);
      
        Database.PutParameter(dbCommand,"?SuppliesStocked", workTransactionVanCheck.SuppliesStocked);
      
        Database.PutParameter(dbCommand,"?OdometerReading", workTransactionVanCheck.OdometerReading);
      
        Database.PutParameter(dbCommand,"?HobbsReading", workTransactionVanCheck.HobbsReading);
      
        Database.PutParameter(dbCommand,"?SpecialNeeds", workTransactionVanCheck.SpecialNeeds);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(WorkTransactionVanCheck workTransactionVanCheck)
      {
        Insert(workTransactionVanCheck, null);
      }


      public static void Insert(List<WorkTransactionVanCheck>  workTransactionVanCheckList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(WorkTransactionVanCheck workTransactionVanCheck in  workTransactionVanCheckList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionVanCheck.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?OilChecked", workTransactionVanCheck.OilChecked);
      
        Database.PutParameter(dbCommand,"?UnitClean", workTransactionVanCheck.UnitClean);
      
        Database.PutParameter(dbCommand,"?VanClean", workTransactionVanCheck.VanClean);
      
        Database.PutParameter(dbCommand,"?SuppliesStocked", workTransactionVanCheck.SuppliesStocked);
      
        Database.PutParameter(dbCommand,"?OdometerReading", workTransactionVanCheck.OdometerReading);
      
        Database.PutParameter(dbCommand,"?HobbsReading", workTransactionVanCheck.HobbsReading);
      
        Database.PutParameter(dbCommand,"?SpecialNeeds", workTransactionVanCheck.SpecialNeeds);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?WorkTransactionId",workTransactionVanCheck.WorkTransactionId);
      
        Database.UpdateParameter(dbCommand,"?OilChecked",workTransactionVanCheck.OilChecked);
      
        Database.UpdateParameter(dbCommand,"?UnitClean",workTransactionVanCheck.UnitClean);
      
        Database.UpdateParameter(dbCommand,"?VanClean",workTransactionVanCheck.VanClean);
      
        Database.UpdateParameter(dbCommand,"?SuppliesStocked",workTransactionVanCheck.SuppliesStocked);
      
        Database.UpdateParameter(dbCommand,"?OdometerReading",workTransactionVanCheck.OdometerReading);
      
        Database.UpdateParameter(dbCommand,"?HobbsReading",workTransactionVanCheck.HobbsReading);
      
        Database.UpdateParameter(dbCommand,"?SpecialNeeds",workTransactionVanCheck.SpecialNeeds);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<WorkTransactionVanCheck>  workTransactionVanCheckList)
      {
        Insert(workTransactionVanCheckList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update WorkTransactionVanCheck Set "
      
        + " OilChecked = ?OilChecked, "
      
        + " UnitClean = ?UnitClean, "
      
        + " VanClean = ?VanClean, "
      
        + " SuppliesStocked = ?SuppliesStocked, "
      
        + " OdometerReading = ?OdometerReading, "
      
        + " HobbsReading = ?HobbsReading, "
      
        + " SpecialNeeds = ?SpecialNeeds "
      
        + " Where "
        
          + " WorkTransactionId = ?WorkTransactionId "
        
      ;

      public static void Update(WorkTransactionVanCheck workTransactionVanCheck, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionVanCheck.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?OilChecked", workTransactionVanCheck.OilChecked);
      
        Database.PutParameter(dbCommand,"?UnitClean", workTransactionVanCheck.UnitClean);
      
        Database.PutParameter(dbCommand,"?VanClean", workTransactionVanCheck.VanClean);
      
        Database.PutParameter(dbCommand,"?SuppliesStocked", workTransactionVanCheck.SuppliesStocked);
      
        Database.PutParameter(dbCommand,"?OdometerReading", workTransactionVanCheck.OdometerReading);
      
        Database.PutParameter(dbCommand,"?HobbsReading", workTransactionVanCheck.HobbsReading);
      
        Database.PutParameter(dbCommand,"?SpecialNeeds", workTransactionVanCheck.SpecialNeeds);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WorkTransactionVanCheck workTransactionVanCheck)
      {
        Update(workTransactionVanCheck, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " WorkTransactionId, "
      
        + " OilChecked, "
      
        + " UnitClean, "
      
        + " VanClean, "
      
        + " SuppliesStocked, "
      
        + " OdometerReading, "
      
        + " HobbsReading, "
      
        + " SpecialNeeds "
      

      + " From WorkTransactionVanCheck "

      
        + " Where "
        
          + " WorkTransactionId = ?WorkTransactionId "
        
      ;

      public static WorkTransactionVanCheck FindByPrimaryKey(
      int workTransactionId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("WorkTransactionVanCheck not found, search by primary key");

      }

      public static WorkTransactionVanCheck FindByPrimaryKey(
      int workTransactionId
      )
      {
      return FindByPrimaryKey(
      workTransactionId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(WorkTransactionVanCheck workTransactionVanCheck, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId",workTransactionVanCheck.WorkTransactionId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WorkTransactionVanCheck workTransactionVanCheck)
      {
      return Exists(workTransactionVanCheck, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from WorkTransactionVanCheck limit 1";

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

      public static WorkTransactionVanCheck Load(IDataReader dataReader, int offset)
      {
      WorkTransactionVanCheck workTransactionVanCheck = new WorkTransactionVanCheck();

      workTransactionVanCheck.WorkTransactionId = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            workTransactionVanCheck.OilChecked = dataReader.GetBoolean(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            workTransactionVanCheck.UnitClean = dataReader.GetBoolean(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            workTransactionVanCheck.VanClean = dataReader.GetBoolean(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            workTransactionVanCheck.SuppliesStocked = dataReader.GetBoolean(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            workTransactionVanCheck.OdometerReading = dataReader.GetDecimal(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            workTransactionVanCheck.HobbsReading = dataReader.GetDecimal(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            workTransactionVanCheck.SpecialNeeds = dataReader.GetString(7 + offset);
          

      return workTransactionVanCheck;
      }

      public static WorkTransactionVanCheck Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From WorkTransactionVanCheck "

      
        + " Where "
        
          + " WorkTransactionId = ?WorkTransactionId "
        
      ;
      public static void Delete(WorkTransactionVanCheck workTransactionVanCheck, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionVanCheck.WorkTransactionId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WorkTransactionVanCheck workTransactionVanCheck)
      {
        Delete(workTransactionVanCheck, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From WorkTransactionVanCheck ";

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

      
        + " WorkTransactionId, "
      
        + " OilChecked, "
      
        + " UnitClean, "
      
        + " VanClean, "
      
        + " SuppliesStocked, "
      
        + " OdometerReading, "
      
        + " HobbsReading, "
      
        + " SpecialNeeds "
      

      + " From WorkTransactionVanCheck ";
      public static List<WorkTransactionVanCheck> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<WorkTransactionVanCheck> rv = new List<WorkTransactionVanCheck>();

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

      public static List<WorkTransactionVanCheck> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WorkTransactionVanCheck> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (WorkTransactionVanCheck obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return WorkTransactionId == obj.WorkTransactionId && OilChecked == obj.OilChecked && UnitClean == obj.UnitClean && VanClean == obj.VanClean && SuppliesStocked == obj.SuppliesStocked && OdometerReading == obj.OdometerReading && HobbsReading == obj.HobbsReading && SpecialNeeds == obj.SpecialNeeds;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<WorkTransactionVanCheck> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionVanCheck));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WorkTransactionVanCheck item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WorkTransactionVanCheck>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionVanCheck));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WorkTransactionVanCheck> itemsList
      = new List<WorkTransactionVanCheck>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WorkTransactionVanCheck)
      itemsList.Add(deserializedObject as WorkTransactionVanCheck);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_workTransactionId;
      
        protected bool m_oilChecked;
      
        protected bool m_unitClean;
      
        protected bool m_vanClean;
      
        protected bool m_suppliesStocked;
      
        protected decimal m_odometerReading;
      
        protected decimal m_hobbsReading;
      
        protected String m_specialNeeds;
      
      #endregion

      #region Constructors
      public WorkTransactionVanCheck(
      int 
          workTransactionId
      ) : this()
      {
      
        m_workTransactionId = workTransactionId;
      
      }

      


        public WorkTransactionVanCheck(
        int 
          workTransactionId,bool 
          oilChecked,bool 
          unitClean,bool 
          vanClean,bool 
          suppliesStocked,decimal 
          odometerReading,decimal 
          hobbsReading,String 
          specialNeeds
        ) : this()
        {
        
          m_workTransactionId = workTransactionId;
        
          m_oilChecked = oilChecked;
        
          m_unitClean = unitClean;
        
          m_vanClean = vanClean;
        
          m_suppliesStocked = suppliesStocked;
        
          m_odometerReading = odometerReading;
        
          m_hobbsReading = hobbsReading;
        
          m_specialNeeds = specialNeeds;
        
        }


      
      #endregion

      
        [XmlElement]
        public int WorkTransactionId
        {
        get { return m_workTransactionId;}
        set { m_workTransactionId = value; }
        }
      
        [XmlElement]
        public bool OilChecked
        {
        get { return m_oilChecked;}
        set { m_oilChecked = value; }
        }
      
        [XmlElement]
        public bool UnitClean
        {
        get { return m_unitClean;}
        set { m_unitClean = value; }
        }
      
        [XmlElement]
        public bool VanClean
        {
        get { return m_vanClean;}
        set { m_vanClean = value; }
        }
      
        [XmlElement]
        public bool SuppliesStocked
        {
        get { return m_suppliesStocked;}
        set { m_suppliesStocked = value; }
        }
      
        [XmlElement]
        public decimal OdometerReading
        {
        get { return m_odometerReading;}
        set { m_odometerReading = value; }
        }
      
        [XmlElement]
        public decimal HobbsReading
        {
        get { return m_hobbsReading;}
        set { m_hobbsReading = value; }
        }
      
        [XmlElement]
        public String SpecialNeeds
        {
        get { return m_specialNeeds;}
        set { m_specialNeeds = value; }
        }
      

      public static int FieldsCount
      {
      get { return 8; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    