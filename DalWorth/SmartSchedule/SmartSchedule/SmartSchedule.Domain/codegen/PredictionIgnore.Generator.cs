
    using System;
    using System.Data;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using SmartSchedule.Data;
    using SmartSchedule.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace SmartSchedule.Domain
      {

      [DataContract]
      public partial class PredictionIgnore : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into PredictionIgnore ( " +
      
        " IgnoreDate " +
      
      ") Values (" +
      
        " ?IgnoreDate " +
      
      ")";

      public static void Insert(PredictionIgnore predictionIgnore, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?IgnoreDate", predictionIgnore.IgnoreDate);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(PredictionIgnore predictionIgnore)
      {
        Insert(predictionIgnore, null);
      }


      public static void Insert(List<PredictionIgnore>  predictionIgnoreList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(PredictionIgnore predictionIgnore in  predictionIgnoreList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?IgnoreDate", predictionIgnore.IgnoreDate);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?IgnoreDate",predictionIgnore.IgnoreDate);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<PredictionIgnore>  predictionIgnoreList)
      {
        Insert(predictionIgnoreList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update PredictionIgnore Set "
      
        + " Where "
        
          + " IgnoreDate = ?IgnoreDate "
        
      ;

      public static void Update(PredictionIgnore predictionIgnore, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?IgnoreDate", predictionIgnore.IgnoreDate);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(PredictionIgnore predictionIgnore)
      {
        Update(predictionIgnore, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " IgnoreDate "
      

      + " From PredictionIgnore "

      
        + " Where "
        
          + " IgnoreDate = ?IgnoreDate "
        
      ;

      public static PredictionIgnore FindByPrimaryKey(
      DateTime ignoreDate, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?IgnoreDate", ignoreDate);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("PredictionIgnore not found, search by primary key");

      }

      public static PredictionIgnore FindByPrimaryKey(
      DateTime ignoreDate
      )
      {
      return FindByPrimaryKey(
      ignoreDate, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(PredictionIgnore predictionIgnore, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?IgnoreDate",predictionIgnore.IgnoreDate);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(PredictionIgnore predictionIgnore)
      {
      return Exists(predictionIgnore, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from PredictionIgnore limit 1";

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

      public static PredictionIgnore Load(IDataReader dataReader, int offset)
      {
      PredictionIgnore predictionIgnore = new PredictionIgnore();

      predictionIgnore.IgnoreDate = dataReader.GetDateTime(0 + offset);
          

      return predictionIgnore;
      }

      public static PredictionIgnore Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From PredictionIgnore "

      
        + " Where "
        
          + " IgnoreDate = ?IgnoreDate "
        
      ;
      public static void Delete(PredictionIgnore predictionIgnore, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?IgnoreDate", predictionIgnore.IgnoreDate);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(PredictionIgnore predictionIgnore)
      {
        Delete(predictionIgnore, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From PredictionIgnore ";

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

      
        + " IgnoreDate "
      

      + " From PredictionIgnore ";
      public static List<PredictionIgnore> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<PredictionIgnore> rv = new List<PredictionIgnore>();

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

      public static List<PredictionIgnore> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<PredictionIgnore> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<PredictionIgnore> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(PredictionIgnore));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(PredictionIgnore item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<PredictionIgnore>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(PredictionIgnore));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<PredictionIgnore> itemsList
      = new List<PredictionIgnore>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is PredictionIgnore)
      itemsList.Add(deserializedObject as PredictionIgnore);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected DateTime m_ignoreDate;
      
      #endregion

      #region Constructors
      public PredictionIgnore(
      DateTime 
          ignoreDate
      ) : this()
      {
      
        m_ignoreDate = ignoreDate;
      
      }

      
      #endregion

      
        [DataMember]
        public DateTime IgnoreDate
        {
        get { return m_ignoreDate;}
        set { m_ignoreDate = value; }
        }
      

      public static int FieldsCount
      {
      get { return 1; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    