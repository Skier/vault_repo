
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class Counter
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Counter ( " +
      
        " CounterName, " +
      
        " Val " +
      
      ") Values (" +
      
        " @CounterName, " +
      
        " @Val " +
      
      ")";

      public static void Insert(Counter counter)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@CounterName", counter.CounterName);
      
        Database.PutParameter(dbCommand,"@Val", counter.Val);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<Counter>  counterList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(Counter counter in  counterList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@CounterName", counter.CounterName);
      
        Database.PutParameter(dbCommand,"@Val", counter.Val);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@CounterName",counter.CounterName);
      
        Database.UpdateParameter(dbCommand,"@Val",counter.Val);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update Counter Set "
      
        + " Val = @Val "
      
        + " Where "
        
          + " CounterName = @CounterName "
        
      ;

      public static void Update(Counter counter)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@CounterName", counter.CounterName);
      
        Database.PutParameter(dbCommand,"@Val", counter.Val);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " CounterName, "
      
        + " Val "
      

      + " From Counter "

      
        + " Where "
        
          + " CounterName = @CounterName "
        
      ;

      public static Counter FindByPrimaryKey(
      String counterName
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CounterName", counterName);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Counter not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(Counter counter)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CounterName",counter.CounterName);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData()
      {
      String sql = "select 1 from Counter";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      #endregion

      #region Load

      public static Counter Load(IDataReader dataReader)
      {
      Counter counter = new Counter();

      counter.CounterName = dataReader.GetString(0);
          counter.Val = dataReader.GetInt32(1);
          

      return counter;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Counter "

      
        + " Where "
        
          + " CounterName = @CounterName "
        
      ;
      public static void Delete(Counter counter)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@CounterName", counter.CounterName);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From Counter ";

      public static void Clear()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "

      
        + " CounterName, "
      
        + " Val "
      

      + " From Counter ";
      public static List<Counter> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
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
         )
        {
        
          m_counterName = counterName;
        
        }
        
        


        public Counter(
        String 
          counterName,int 
          val
        )
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
      
      }
      #endregion
      }

    