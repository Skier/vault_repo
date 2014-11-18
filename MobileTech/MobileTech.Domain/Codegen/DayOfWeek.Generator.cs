
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class DayOfWeek
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into DayOfWeek ( " +
      
        " Number " +
      
      ") Values (" +
      
        " @Number " +
      
      ")";

      public static void Insert(DayOfWeek dayOfWeek)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@Number", dayOfWeek.Number);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<DayOfWeek>  dayOfWeekList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(DayOfWeek dayOfWeek in  dayOfWeekList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@Number", dayOfWeek.Number);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@Number",dayOfWeek.Number);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update DayOfWeek Set "
      
        + " Where "
        
          + " Number = @Number "
        
      ;

      public static void Update(DayOfWeek dayOfWeek)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@Number", dayOfWeek.Number);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " Number "
      

      + " From DayOfWeek "

      
        + " Where "
        
          + " Number = @Number "
        
      ;

      public static DayOfWeek FindByPrimaryKey(
      byte number
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@Number", number);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("DayOfWeek not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(DayOfWeek dayOfWeek)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@Number",dayOfWeek.Number);
      

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
      String sql = "select 1 from DayOfWeek";

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

      public static DayOfWeek Load(IDataReader dataReader)
      {
      DayOfWeek dayOfWeek = new DayOfWeek();

      dayOfWeek.Number = dataReader.GetByte(0);
          

      return dayOfWeek;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From DayOfWeek "

      
        + " Where "
        
          + " Number = @Number "
        
      ;
      public static void Delete(DayOfWeek dayOfWeek)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@Number", dayOfWeek.Number);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From DayOfWeek ";

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

      
        + " Number "
      

      + " From DayOfWeek ";
      public static List<DayOfWeek> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<DayOfWeek> rv = new List<DayOfWeek>();

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
        List<DayOfWeek> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<DayOfWeek> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(DayOfWeek));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(DayOfWeek item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<DayOfWeek>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(DayOfWeek));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<DayOfWeek> itemsList
      = new List<DayOfWeek>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is DayOfWeek)
        itemsList.Add(deserializedObject as DayOfWeek);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected byte m_number;
        
        #endregion
        
        #region Constructors
        public DayOfWeek(
        byte 
          number
         )
        {
        
          m_number = number;
        
        }
        
        
      #endregion

      
        [XmlElement]
        public byte Number
        {
          get { return m_number;}
          set { m_number = value; }
        }
      
      }
      #endregion
      }

    