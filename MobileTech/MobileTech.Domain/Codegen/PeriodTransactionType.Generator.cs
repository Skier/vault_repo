
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class PeriodTransactionType
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into PeriodTransactionType ( " +
      
        " PeriodTransactionTypeId, " +
      
        " Name " +
      
      ") Values (" +
      
        " @PeriodTransactionTypeId, " +
      
        " @Name " +
      
      ")";

      public static void Insert(PeriodTransactionType periodTransactionType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@PeriodTransactionTypeId", periodTransactionType.PeriodTransactionTypeId);
      
        Database.PutParameter(dbCommand,"@Name", periodTransactionType.Name);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<PeriodTransactionType>  periodTransactionTypeList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(PeriodTransactionType periodTransactionType in  periodTransactionTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@PeriodTransactionTypeId", periodTransactionType.PeriodTransactionTypeId);
      
        Database.PutParameter(dbCommand,"@Name", periodTransactionType.Name);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@PeriodTransactionTypeId",periodTransactionType.PeriodTransactionTypeId);
      
        Database.UpdateParameter(dbCommand,"@Name",periodTransactionType.Name);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update PeriodTransactionType Set "
      
        + " Name = @Name "
      
        + " Where "
        
          + " PeriodTransactionTypeId = @PeriodTransactionTypeId "
        
      ;

      public static void Update(PeriodTransactionType periodTransactionType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@PeriodTransactionTypeId", periodTransactionType.PeriodTransactionTypeId);
      
        Database.PutParameter(dbCommand,"@Name", periodTransactionType.Name);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " PeriodTransactionTypeId, "
      
        + " Name "
      

      + " From PeriodTransactionType "

      
        + " Where "
        
          + " PeriodTransactionTypeId = @PeriodTransactionTypeId "
        
      ;

      public static PeriodTransactionType FindByPrimaryKey(
      int periodTransactionTypeId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@PeriodTransactionTypeId", periodTransactionTypeId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("PeriodTransactionType not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(PeriodTransactionType periodTransactionType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@PeriodTransactionTypeId",periodTransactionType.PeriodTransactionTypeId);
      

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
      String sql = "select 1 from PeriodTransactionType";

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

      public static PeriodTransactionType Load(IDataReader dataReader)
      {
      PeriodTransactionType periodTransactionType = new PeriodTransactionType();

      periodTransactionType.PeriodTransactionTypeId = dataReader.GetInt16(0);
          periodTransactionType.Name = dataReader.GetString(1);
          

      return periodTransactionType;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From PeriodTransactionType "

      
        + " Where "
        
          + " PeriodTransactionTypeId = @PeriodTransactionTypeId "
        
      ;
      public static void Delete(PeriodTransactionType periodTransactionType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@PeriodTransactionTypeId", periodTransactionType.PeriodTransactionTypeId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From PeriodTransactionType ";

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

      
        + " PeriodTransactionTypeId, "
      
        + " Name "
      

      + " From PeriodTransactionType ";
      public static List<PeriodTransactionType> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<PeriodTransactionType> rv = new List<PeriodTransactionType>();

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
        List<PeriodTransactionType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<PeriodTransactionType> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(PeriodTransactionType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(PeriodTransactionType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<PeriodTransactionType>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(PeriodTransactionType));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<PeriodTransactionType> itemsList
      = new List<PeriodTransactionType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is PeriodTransactionType)
        itemsList.Add(deserializedObject as PeriodTransactionType);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected int m_periodTransactionTypeId;
        
          protected String m_name;
        
        #endregion
        
        #region Constructors
        public PeriodTransactionType(
        int 
          periodTransactionTypeId
         )
        {
        
          m_periodTransactionTypeId = periodTransactionTypeId;
        
        }
        
        


        public PeriodTransactionType(
        int 
          periodTransactionTypeId,String 
          name
        )
        {
        
          m_periodTransactionTypeId = periodTransactionTypeId;
        
          m_name = name;
        
          }


        
      #endregion

      
        [XmlElement]
        public int PeriodTransactionTypeId
        {
          get { return m_periodTransactionTypeId;}
          set { m_periodTransactionTypeId = value; }
        }
      
        [XmlElement]
        public String Name
        {
          get { return m_name;}
          set { m_name = value; }
        }
      
      }
      #endregion
      }

    