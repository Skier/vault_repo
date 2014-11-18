
    using System;
    using System.Data;
    using System.Collections.Generic;
    using Dalworth.Server.Data;
    using Dalworth.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Dalworth.Server.Servman.Domain
      {


      public partial class TicketCounter
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ticket ( " +
      
        " ticket " +
      
      ") Values (" +
      
        " ? " +
      
      ")";

      public static void Insert(TicketCounter ticketCounter)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@ticket", ticketCounter.ticket);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<TicketCounter>  ticketCounterList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(TicketCounter ticketCounter in  ticketCounterList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ticket", ticketCounter.ticket);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ticket",ticketCounter.ticket);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


          private const String SqlUpdate = "Update ticket Set "
      
        + " Where "

          + " ticket.ticket = ?  "
        
      ;

      public static void Update(TicketCounter ticketCounter)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@ticket", ticketCounter.ticket);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "


        + " ticket.ticket "


      + " From ticket "

      
        + " Where "

          + " ticket.ticket = ?  "
        
      ;

      public static TicketCounter FindByPrimaryKey(
      String ticket
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@ticket", ticket);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("ticket not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(TicketCounter ticketCounter)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@ticket",ticketCounter.ticket);
      

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
          String sql = "select 1 from ticket";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql, ConnectionKeyEnum.Servman))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      #endregion

      #region Load

      public static TicketCounter Load(IDataReader dataReader)
      {
      TicketCounter ticketCounter = new TicketCounter();

      ticketCounter.ticket = dataReader.GetString(0);
          

      return ticketCounter;
      }

      #endregion

      #region Delete
          private const String SqlDelete = "Delete From [ticket] "

      
        + " Where "
        
          + " ticket = ?  "
        
      ;
      public static void Delete(TicketCounter ticketCounter)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      
        Database.PutParameter(dbCommand,"@ticket", ticketCounter.ticket);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

          private const String SqlDeleteAll = "Delete From [ticket] ";

      public static void Clear()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll, ConnectionKeyEnum.Servman))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "


        + " ticket.ticket "


      + " From ticket ";
      public static List<TicketCounter> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<TicketCounter> rv = new List<TicketCounter>();

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
      List<TicketCounter> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<TicketCounter> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TicketCounter));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TicketCounter item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TicketCounter>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TicketCounter));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TicketCounter> itemsList
      = new List<TicketCounter>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TicketCounter)
      itemsList.Add(deserializedObject as TicketCounter);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_ticket;
      
      #endregion

      #region Constructors
      public TicketCounter(
      String 
          ticket
      )
      {
      
        m_ticket = ticket;
      
      }

      
      #endregion

      
        [XmlElement]
        public String ticket
        {
        get { return m_ticket;}
        set { m_ticket = value; }
        }
      
      }
      #endregion
      }

    