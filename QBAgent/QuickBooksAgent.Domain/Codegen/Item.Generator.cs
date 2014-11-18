
    using System;
    using System.Data;
    using System.Collections.Generic;
    using QuickBooksAgent.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace QuickBooksAgent.Domain
      {


      public partial class Item
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Item] ( " +
      
        " ItemId, " +
        " QuickBooksListId, " +
        " EditSequence, " +
        " Name, " +
        " SalesPrice " +
        ") Values (" +
      
        " @ItemId, " +
        " @QuickBooksListId, " +
        " @EditSequence, " +
        " @Name, " +
        " @SalesPrice " +
      ")";

      public static void Insert(Item item)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
              Database.PutParameter(dbCommand,"@ItemId", item.ItemId);            
          
              Database.PutParameter(dbCommand,"@QuickBooksListId", item.QuickBooksListId);            
          
              Database.PutParameter(dbCommand,"@EditSequence", item.EditSequence);            
          
              Database.PutParameter(dbCommand,"@Name", item.Name);            
          
              Database.PutParameter(dbCommand,"@SalesPrice", item.SalesPrice);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<Item>  itemList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(Item item in  itemList)
      {
      if(!parametersAdded)
      {
      
            Database.PutParameter(dbCommand,"@ItemId", item.ItemId);
          
            Database.PutParameter(dbCommand,"@QuickBooksListId", item.QuickBooksListId);
          
            Database.PutParameter(dbCommand,"@EditSequence", item.EditSequence);
          
            Database.PutParameter(dbCommand,"@Name", item.Name);
          
            Database.PutParameter(dbCommand,"@SalesPrice", item.SalesPrice);
          
      parametersAdded = true;
      }
      else
      {

      
            Database.UpdateParameter(dbCommand,"@ItemId",item.ItemId);
          
            Database.UpdateParameter(dbCommand,"@QuickBooksListId",item.QuickBooksListId);
          
            Database.UpdateParameter(dbCommand,"@EditSequence",item.EditSequence);
          
            Database.UpdateParameter(dbCommand,"@Name",item.Name);
          
            Database.UpdateParameter(dbCommand,"@SalesPrice",item.SalesPrice);
          
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Item] Set "
      
        + " QuickBooksListId = @QuickBooksListId, "
        + " EditSequence = @EditSequence, "
        + " Name = @Name, "
        + " SalesPrice = @SalesPrice "
        + " Where "
        
          + " ItemId = @ItemId "
        
      ;

      public static void Update(Item item)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
            Database.PutParameter(dbCommand,"@ItemId", item.ItemId);            
          
            Database.PutParameter(dbCommand,"@QuickBooksListId", item.QuickBooksListId);            
          
            Database.PutParameter(dbCommand,"@EditSequence", item.EditSequence);            
          
            Database.PutParameter(dbCommand,"@Name", item.Name);            
          
            Database.PutParameter(dbCommand,"@SalesPrice", item.SalesPrice);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "
      
        + " ItemId, "
        + " QuickBooksListId, "
        + " EditSequence, "
        + " Name, "
        + " SalesPrice "
        + " From [Item] "
      
        + " Where "
        
        + " ItemId = @ItemId "
        
      ;

      public static Item FindByPrimaryKey(
      int itemId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ItemId", itemId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Item not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(Item item)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ItemId",item.ItemId);
      

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
      String sql = "select 1 from [Item]";

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

      public static Item Load(IDataReader dataReader)
      {
      Item item = new Item();

      item.ItemId = dataReader.GetInt32(0);
          item.QuickBooksListId = dataReader.GetInt32(1);
          item.EditSequence = dataReader.GetInt32(2);
          item.Name = dataReader.GetString(3);
          item.SalesPrice = dataReader.GetDecimal(4);
          

      return item;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Item] "

      
        + " Where "
        
          + " ItemId = @ItemId "
        
      ;
      public static void Delete(Item item)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@ItemId", item.ItemId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Item] ";

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
      
        + " ItemId, "
        + " QuickBooksListId, "
        + " EditSequence, "
        + " Name, "
        + " SalesPrice "
        + " From [Item] ";
      public static List<Item> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<Item> rv = new List<Item>();

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
      List<Item> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Item> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Item));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Item item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Item>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Item));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Item> itemsList
      = new List<Item>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Item)
      itemsList.Add(deserializedObject as Item);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      


		#region Fields
		
		#region ItemId
        protected int m_itemId;

			[XmlAttribute]
			public int ItemId
			{
			get { return m_itemId;}
			set { m_itemId = value; }
			}
		#endregion
		
		#region QuickBooksListId
        protected int m_quickBooksListId;

			[XmlAttribute]
			public int QuickBooksListId
			{
			get { return m_quickBooksListId;}
			set { m_quickBooksListId = value; }
			}
		#endregion
		
		#region EditSequence
        protected int m_editSequence;

			[XmlAttribute]
			public int EditSequence
			{
			get { return m_editSequence;}
			set { m_editSequence = value; }
			}
		#endregion
		
		#region Name
        protected String m_name;

			[XmlAttribute]
			public String Name
			{
			get { return m_name;}
			set { m_name = value; }
			}
		#endregion
		
		#region SalesPrice
        protected decimal m_salesPrice;

			[XmlAttribute]
			public decimal SalesPrice
			{
			get { return m_salesPrice;}
			set { m_salesPrice = value; }
			}
		#endregion
		
		
		#endregion

      #region Constructors
      public Item(
		int itemId

		)
		{
		
			m_itemId = itemId;
		
        }

      


        public Item(
		  int itemId,int quickBooksListId,int editSequence,String name,decimal salesPrice
		  )
		  {

		  
			  m_itemId = itemId;
		  
			  m_quickBooksListId = quickBooksListId;
		  
			  m_editSequence = editSequence;
		  
			  m_name = name;
		  
			  m_salesPrice = salesPrice;
		  
		  }


	  
      #endregion

	
      }
      #endregion
      }

    