
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


      public partial class Counter : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Counter ( " +
      
        " CounterName, " +
      
        " Val " +
      
      ") Values (" +
      
        " ?CounterName, " +
      
        " ?Val " +
      
      ")";

      public static void Insert(Counter counter, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?CounterName", counter.CounterName);
      
        Database.PutParameter(dbCommand,"?Val", counter.Val);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(Counter counter)
      {
        Insert(counter, null);
      }


      public static void Insert(List<Counter>  counterList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Counter counter in  counterList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?CounterName", counter.CounterName);
      
        Database.PutParameter(dbCommand,"?Val", counter.Val);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?CounterName",counter.CounterName);
      
        Database.UpdateParameter(dbCommand,"?Val",counter.Val);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<Counter>  counterList)
      {
        Insert(counterList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Counter Set "
      
        + " Val = ?Val "
      
        + " Where "
        
          + " CounterName = ?CounterName "
        
      ;

      public static void Update(Counter counter, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?CounterName", counter.CounterName);
      
        Database.PutParameter(dbCommand,"?Val", counter.Val);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Counter counter)
      {
        Update(counter, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " CounterName, "
      
        + " Val "
      

      + " From Counter "

      
        + " Where "
        
          + " CounterName = ?CounterName "
        
      ;

      public static Counter FindByPrimaryKey(
      String counterName, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?CounterName", counterName);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Counter not found, search by primary key");

      }

      public static Counter FindByPrimaryKey(
      String counterName
      )
      {
      return FindByPrimaryKey(
      counterName, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Counter counter, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?CounterName",counter.CounterName);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Counter counter)
      {
      return Exists(counter, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Counter limit 1";

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

      public static Counter Load(IDataReader dataReader, int offset)
      {
      Counter counter = new Counter();

      counter.CounterName = dataReader.GetString(0 + offset);
          counter.Val = dataReader.GetInt32(1 + offset);
          

      return counter;
      }

      public static Counter Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Counter "

      
        + " Where "
        
          + " CounterName = ?CounterName "
        
      ;
      public static void Delete(Counter counter, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?CounterName", counter.CounterName);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Counter counter)
      {
        Delete(counter, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Counter ";

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

      
        + " CounterName, "
      
        + " Val "
      

      + " From Counter ";
      public static List<Counter> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Counter> rv = new List<Counter>();

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

      public static List<Counter> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Counter> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (Counter obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return CounterName == obj.CounterName && Val == obj.Val;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<Counter> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Counter));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Counter item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Counter>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Counter));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Counter> itemsList
      = new List<Counter>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Counter)
      itemsList.Add(deserializedObject as Counter);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_counterName;
      
        protected int m_val;
      
      #endregion

      #region Constructors
      public Counter(
      String 
          counterName
      ) : this()
      {
      
        m_counterName = counterName;
      
      }

      


        public Counter(
        String 
          counterName,int 
          val
        ) : this()
        {
        
          m_counterName = counterName;
        
          m_val = val;
        
        }


      
      #endregion

      
        [XmlElement]
        public String CounterName
        {
        get { return m_counterName;}
        set { m_counterName = value; }
        }
      
        [XmlElement]
        public int Val
        {
        get { return m_val;}
        set { m_val = value; }
        }
      

      public static int FieldsCount
      {
      get { return 2; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    