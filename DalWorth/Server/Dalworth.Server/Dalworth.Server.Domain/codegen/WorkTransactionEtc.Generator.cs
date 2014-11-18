
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


      public partial class WorkTransactionEtc : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into WorkTransactionEtc ( " +
      
        " WorkTransactionId, " +
      
        " SaleAmount, " +
      
        " Hours, " +
      
        " Minutes, " +
      
        " Notes " +
      
      ") Values (" +
      
        " ?WorkTransactionId, " +
      
        " ?SaleAmount, " +
      
        " ?Hours, " +
      
        " ?Minutes, " +
      
        " ?Notes " +
      
      ")";

      public static void Insert(WorkTransactionEtc workTransactionEtc, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionEtc.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?SaleAmount", workTransactionEtc.SaleAmount);
      
        Database.PutParameter(dbCommand,"?Hours", workTransactionEtc.Hours);
      
        Database.PutParameter(dbCommand,"?Minutes", workTransactionEtc.Minutes);
      
        Database.PutParameter(dbCommand,"?Notes", workTransactionEtc.Notes);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(WorkTransactionEtc workTransactionEtc)
      {
        Insert(workTransactionEtc, null);
      }


      public static void Insert(List<WorkTransactionEtc>  workTransactionEtcList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(WorkTransactionEtc workTransactionEtc in  workTransactionEtcList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionEtc.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?SaleAmount", workTransactionEtc.SaleAmount);
      
        Database.PutParameter(dbCommand,"?Hours", workTransactionEtc.Hours);
      
        Database.PutParameter(dbCommand,"?Minutes", workTransactionEtc.Minutes);
      
        Database.PutParameter(dbCommand,"?Notes", workTransactionEtc.Notes);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?WorkTransactionId",workTransactionEtc.WorkTransactionId);
      
        Database.UpdateParameter(dbCommand,"?SaleAmount",workTransactionEtc.SaleAmount);
      
        Database.UpdateParameter(dbCommand,"?Hours",workTransactionEtc.Hours);
      
        Database.UpdateParameter(dbCommand,"?Minutes",workTransactionEtc.Minutes);
      
        Database.UpdateParameter(dbCommand,"?Notes",workTransactionEtc.Notes);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<WorkTransactionEtc>  workTransactionEtcList)
      {
        Insert(workTransactionEtcList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update WorkTransactionEtc Set "
      
        + " SaleAmount = ?SaleAmount, "
      
        + " Hours = ?Hours, "
      
        + " Minutes = ?Minutes, "
      
        + " Notes = ?Notes "
      
        + " Where "
        
          + " WorkTransactionId = ?WorkTransactionId "
        
      ;

      public static void Update(WorkTransactionEtc workTransactionEtc, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionEtc.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?SaleAmount", workTransactionEtc.SaleAmount);
      
        Database.PutParameter(dbCommand,"?Hours", workTransactionEtc.Hours);
      
        Database.PutParameter(dbCommand,"?Minutes", workTransactionEtc.Minutes);
      
        Database.PutParameter(dbCommand,"?Notes", workTransactionEtc.Notes);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WorkTransactionEtc workTransactionEtc)
      {
        Update(workTransactionEtc, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " WorkTransactionId, "
      
        + " SaleAmount, "
      
        + " Hours, "
      
        + " Minutes, "
      
        + " Notes "
      

      + " From WorkTransactionEtc "

      
        + " Where "
        
          + " WorkTransactionId = ?WorkTransactionId "
        
      ;

      public static WorkTransactionEtc FindByPrimaryKey(
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
      throw new DataNotFoundException("WorkTransactionEtc not found, search by primary key");

      }

      public static WorkTransactionEtc FindByPrimaryKey(
      int workTransactionId
      )
      {
      return FindByPrimaryKey(
      workTransactionId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(WorkTransactionEtc workTransactionEtc, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId",workTransactionEtc.WorkTransactionId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WorkTransactionEtc workTransactionEtc)
      {
      return Exists(workTransactionEtc, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from WorkTransactionEtc limit 1";

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

      public static WorkTransactionEtc Load(IDataReader dataReader, int offset)
      {
      WorkTransactionEtc workTransactionEtc = new WorkTransactionEtc();

      workTransactionEtc.WorkTransactionId = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            workTransactionEtc.SaleAmount = dataReader.GetDecimal(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            workTransactionEtc.Hours = dataReader.GetInt32(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            workTransactionEtc.Minutes = dataReader.GetInt32(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            workTransactionEtc.Notes = dataReader.GetString(4 + offset);
          

      return workTransactionEtc;
      }

      public static WorkTransactionEtc Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From WorkTransactionEtc "

      
        + " Where "
        
          + " WorkTransactionId = ?WorkTransactionId "
        
      ;
      public static void Delete(WorkTransactionEtc workTransactionEtc, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionEtc.WorkTransactionId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WorkTransactionEtc workTransactionEtc)
      {
        Delete(workTransactionEtc, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From WorkTransactionEtc ";

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
      
        + " SaleAmount, "
      
        + " Hours, "
      
        + " Minutes, "
      
        + " Notes "
      

      + " From WorkTransactionEtc ";
      public static List<WorkTransactionEtc> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<WorkTransactionEtc> rv = new List<WorkTransactionEtc>();

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

      public static List<WorkTransactionEtc> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WorkTransactionEtc> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (WorkTransactionEtc obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return WorkTransactionId == obj.WorkTransactionId && SaleAmount == obj.SaleAmount && Hours == obj.Hours && Minutes == obj.Minutes && Notes == obj.Notes;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<WorkTransactionEtc> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionEtc));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WorkTransactionEtc item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WorkTransactionEtc>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionEtc));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WorkTransactionEtc> itemsList
      = new List<WorkTransactionEtc>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WorkTransactionEtc)
      itemsList.Add(deserializedObject as WorkTransactionEtc);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_workTransactionId;
      
        protected decimal m_saleAmount;
      
        protected int? m_hours;
      
        protected int? m_minutes;
      
        protected String m_notes;
      
      #endregion

      #region Constructors
      public WorkTransactionEtc(
      int 
          workTransactionId
      ) : this()
      {
      
        m_workTransactionId = workTransactionId;
      
      }

      


        public WorkTransactionEtc(
        int 
          workTransactionId,decimal 
          saleAmount,int? 
          hours,int? 
          minutes,String 
          notes
        ) : this()
        {
        
          m_workTransactionId = workTransactionId;
        
          m_saleAmount = saleAmount;
        
          m_hours = hours;
        
          m_minutes = minutes;
        
          m_notes = notes;
        
        }


      
      #endregion

      
        [XmlElement]
        public int WorkTransactionId
        {
        get { return m_workTransactionId;}
        set { m_workTransactionId = value; }
        }
      
        [XmlElement]
        public decimal SaleAmount
        {
        get { return m_saleAmount;}
        set { m_saleAmount = value; }
        }
      
        [XmlElement]
        public int? Hours
        {
        get { return m_hours;}
        set { m_hours = value; }
        }
      
        [XmlElement]
        public int? Minutes
        {
        get { return m_minutes;}
        set { m_minutes = value; }
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

    