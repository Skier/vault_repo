
    using System;
    using System.Data;
    using System.Collections.Generic;
    using Dalworth.Data;
    using Dalworth.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Dalworth.Domain
      {


      public partial class ItemShape : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [ItemShape] ( " +
      
        " ID, " +
      
        " Shape, " +
      
        " Description " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @Shape, " +
      
        " @Description " +
      
      ")";

      public static void Insert(ItemShape itemShape, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", itemShape.ID);
      
        Database.PutParameter(dbCommand,"@Shape", itemShape.Shape);
      
        Database.PutParameter(dbCommand,"@Description", itemShape.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(ItemShape itemShape)
      {
        Insert(itemShape, null);
      }

      public static void Insert(List<ItemShape>  itemShapeList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(ItemShape itemShape in  itemShapeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", itemShape.ID);
      
        Database.PutParameter(dbCommand,"@Shape", itemShape.Shape);
      
        Database.PutParameter(dbCommand,"@Description", itemShape.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",itemShape.ID);
      
        Database.UpdateParameter(dbCommand,"@Shape",itemShape.Shape);
      
        Database.UpdateParameter(dbCommand,"@Description",itemShape.Description);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<ItemShape>  itemShapeList)
      {
      Insert(itemShapeList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [ItemShape] Set "
      
        + " Shape = @Shape, "
      
        + " Description = @Description "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(ItemShape itemShape, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", itemShape.ID);
      
        Database.PutParameter(dbCommand,"@Shape", itemShape.Shape);
      
        Database.PutParameter(dbCommand,"@Description", itemShape.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(ItemShape itemShape)
      {
      Update(itemShape, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Shape, "
      
        + " Description "
      

      + " From [ItemShape] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static ItemShape FindByPrimaryKey(
      int iD, IDbTransaction transaction
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", iD);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("ItemShape not found, search by primary key");

      }

      public static ItemShape FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(ItemShape itemShape, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",itemShape.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(ItemShape itemShape)
      {
      return Exists(itemShape, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from ItemShape";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql, transaction))
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

      public static ItemShape Load(IDataReader dataReader)
      {
      ItemShape itemShape = new ItemShape();

      itemShape.ID = dataReader.GetInt32(0);
          itemShape.Shape = dataReader.GetString(1);
          
            if(!dataReader.IsDBNull(2))
            itemShape.Description = dataReader.GetString(2);
          

      return itemShape;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [ItemShape] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(ItemShape itemShape, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", itemShape.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(ItemShape itemShape)
      {
      Delete(itemShape, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [ItemShape] ";

      public static void Clear(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll, transaction))
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
      
        + " Shape, "
      
        + " Description "
      

      + " From [ItemShape] ";
      public static List<ItemShape> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<ItemShape> rv = new List<ItemShape>();

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

      public static List<ItemShape> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<ItemShape> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<ItemShape> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ItemShape));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ItemShape item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ItemShape>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ItemShape));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ItemShape> itemsList
      = new List<ItemShape>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ItemShape)
      itemsList.Add(deserializedObject as ItemShape);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_shape;
      
        protected String m_description;
      
      #endregion

      #region Constructors
      public ItemShape(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public ItemShape(
        int 
          iD,String 
          shape,String 
          description
        )
        {
        
          m_iD = iD;
        
          m_shape = shape;
        
          m_description = description;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public String Shape
        {
        get { return m_shape;}
        set { m_shape = value; }
        }
      
        [XmlElement]
        public String Description
        {
        get { return m_description;}
        set { m_description = value; }
        }
      
      }
      #endregion
      }

    