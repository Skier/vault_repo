
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class RouteSchedule
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into RouteSchedule ( " +
      
        " LocationId, " +
      
        " RouteScheduleId, " +
      
        " DayOfWeekNumber, " +
      
        " CustomerId, " +
      
        " RouteNumber, " +
      
        " Frequency, " +
      
        " Sequence, " +
      
        " StartDate, " +
      
        " EndDate " +
      
      ") Values (" +
      
        " @LocationId, " +
      
        " @RouteScheduleId, " +
      
        " @DayOfWeekNumber, " +
      
        " @CustomerId, " +
      
        " @RouteNumber, " +
      
        " @Frequency, " +
      
        " @Sequence, " +
      
        " @StartDate, " +
      
        " @EndDate " +
      
      ")";

      public static void Insert(RouteSchedule routeSchedule)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", routeSchedule.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteScheduleId", routeSchedule.RouteScheduleId);
      
        Database.PutParameter(dbCommand,"@DayOfWeekNumber", routeSchedule.DayOfWeekNumber);
      
        Database.PutParameter(dbCommand,"@CustomerId", routeSchedule.CustomerId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", routeSchedule.RouteNumber);
      
        Database.PutParameter(dbCommand,"@Frequency", routeSchedule.Frequency);
      
        Database.PutParameter(dbCommand,"@Sequence", routeSchedule.Sequence);
      
        Database.PutParameter(dbCommand,"@StartDate", routeSchedule.StartDate);
      
        Database.PutParameter(dbCommand,"@EndDate", routeSchedule.EndDate);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<RouteSchedule>  routeScheduleList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(RouteSchedule routeSchedule in  routeScheduleList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@LocationId", routeSchedule.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteScheduleId", routeSchedule.RouteScheduleId);
      
        Database.PutParameter(dbCommand,"@DayOfWeekNumber", routeSchedule.DayOfWeekNumber);
      
        Database.PutParameter(dbCommand,"@CustomerId", routeSchedule.CustomerId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", routeSchedule.RouteNumber);
      
        Database.PutParameter(dbCommand,"@Frequency", routeSchedule.Frequency);
      
        Database.PutParameter(dbCommand,"@Sequence", routeSchedule.Sequence);
      
        Database.PutParameter(dbCommand,"@StartDate", routeSchedule.StartDate);
      
        Database.PutParameter(dbCommand,"@EndDate", routeSchedule.EndDate);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@LocationId",routeSchedule.LocationId);
      
        Database.UpdateParameter(dbCommand,"@RouteScheduleId",routeSchedule.RouteScheduleId);
      
        Database.UpdateParameter(dbCommand,"@DayOfWeekNumber",routeSchedule.DayOfWeekNumber);
      
        Database.UpdateParameter(dbCommand,"@CustomerId",routeSchedule.CustomerId);
      
        Database.UpdateParameter(dbCommand,"@RouteNumber",routeSchedule.RouteNumber);
      
        Database.UpdateParameter(dbCommand,"@Frequency",routeSchedule.Frequency);
      
        Database.UpdateParameter(dbCommand,"@Sequence",routeSchedule.Sequence);
      
        Database.UpdateParameter(dbCommand,"@StartDate",routeSchedule.StartDate);
      
        Database.UpdateParameter(dbCommand,"@EndDate",routeSchedule.EndDate);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update RouteSchedule Set "
      
        + " DayOfWeekNumber = @DayOfWeekNumber, "
      
        + " CustomerId = @CustomerId, "
      
        + " RouteNumber = @RouteNumber, "
      
        + " Frequency = @Frequency, "
      
        + " Sequence = @Sequence, "
      
        + " StartDate = @StartDate, "
      
        + " EndDate = @EndDate "
      
        + " Where "
        
          + " LocationId = @LocationId and  "
        
          + " RouteScheduleId = @RouteScheduleId "
        
      ;

      public static void Update(RouteSchedule routeSchedule)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", routeSchedule.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteScheduleId", routeSchedule.RouteScheduleId);
      
        Database.PutParameter(dbCommand,"@DayOfWeekNumber", routeSchedule.DayOfWeekNumber);
      
        Database.PutParameter(dbCommand,"@CustomerId", routeSchedule.CustomerId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", routeSchedule.RouteNumber);
      
        Database.PutParameter(dbCommand,"@Frequency", routeSchedule.Frequency);
      
        Database.PutParameter(dbCommand,"@Sequence", routeSchedule.Sequence);
      
        Database.PutParameter(dbCommand,"@StartDate", routeSchedule.StartDate);
      
        Database.PutParameter(dbCommand,"@EndDate", routeSchedule.EndDate);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " LocationId, "
      
        + " RouteScheduleId, "
      
        + " DayOfWeekNumber, "
      
        + " CustomerId, "
      
        + " RouteNumber, "
      
        + " Frequency, "
      
        + " Sequence, "
      
        + " StartDate, "
      
        + " EndDate "
      

      + " From RouteSchedule "

      
        + " Where "
        
          + " LocationId = @LocationId and  "
        
          + " RouteScheduleId = @RouteScheduleId "
        
      ;

      public static RouteSchedule FindByPrimaryKey(
      int locationId,int routeScheduleId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", locationId);
      
        Database.PutParameter(dbCommand,"@RouteScheduleId", routeScheduleId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("RouteSchedule not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(RouteSchedule routeSchedule)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@LocationId",routeSchedule.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteScheduleId",routeSchedule.RouteScheduleId);
      

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
      String sql = "select 1 from RouteSchedule";

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

      public static RouteSchedule Load(IDataReader dataReader)
      {
      RouteSchedule routeSchedule = new RouteSchedule();

      routeSchedule.LocationId = dataReader.GetInt32(0);
          routeSchedule.RouteScheduleId = dataReader.GetInt32(1);
          routeSchedule.DayOfWeekNumber = dataReader.GetByte(2);
          routeSchedule.CustomerId = dataReader.GetInt32(3);
          routeSchedule.RouteNumber = dataReader.GetInt32(4);
          routeSchedule.Frequency = dataReader.GetInt32(5);
          routeSchedule.Sequence = dataReader.GetInt32(6);
          routeSchedule.StartDate = dataReader.GetDateTime(7);
          
            if(!dataReader.IsDBNull(8))
            routeSchedule.EndDate = dataReader.GetDateTime(8);
          

      return routeSchedule;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From RouteSchedule "

      
        + " Where "
        
          + " LocationId = @LocationId and  "
        
          + " RouteScheduleId = @RouteScheduleId "
        
      ;
      public static void Delete(RouteSchedule routeSchedule)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@LocationId", routeSchedule.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteScheduleId", routeSchedule.RouteScheduleId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From RouteSchedule ";

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

      
        + " LocationId, "
      
        + " RouteScheduleId, "
      
        + " DayOfWeekNumber, "
      
        + " CustomerId, "
      
        + " RouteNumber, "
      
        + " Frequency, "
      
        + " Sequence, "
      
        + " StartDate, "
      
        + " EndDate "
      

      + " From RouteSchedule ";
      public static List<RouteSchedule> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<RouteSchedule> rv = new List<RouteSchedule>();

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
        List<RouteSchedule> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<RouteSchedule> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(RouteSchedule));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(RouteSchedule item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<RouteSchedule>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(RouteSchedule));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<RouteSchedule> itemsList
      = new List<RouteSchedule>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is RouteSchedule)
        itemsList.Add(deserializedObject as RouteSchedule);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected int m_locationId;
        
          protected int m_routeScheduleId;
        
          protected byte m_dayOfWeekNumber;
        
          protected int m_customerId;
        
          protected int m_routeNumber;
        
          protected int m_frequency;
        
          protected int m_sequence;
        
          protected DateTime m_startDate;
        
          protected DateTime? m_endDate;
        
        #endregion
        
        #region Constructors
        public RouteSchedule(
        int 
          locationId,int 
          routeScheduleId
         )
        {
        
          m_locationId = locationId;
        
          m_routeScheduleId = routeScheduleId;
        
        }
        
        


        public RouteSchedule(
        int 
          locationId,int 
          routeScheduleId,byte 
          dayOfWeekNumber,int 
          customerId,int 
          routeNumber,int 
          frequency,int 
          sequence,DateTime 
          startDate,DateTime? 
          endDate
        )
        {
        
          m_locationId = locationId;
        
          m_routeScheduleId = routeScheduleId;
        
          m_dayOfWeekNumber = dayOfWeekNumber;
        
          m_customerId = customerId;
        
          m_routeNumber = routeNumber;
        
          m_frequency = frequency;
        
          m_sequence = sequence;
        
          m_startDate = startDate;
        
          m_endDate = endDate;
        
          }


        
      #endregion

      
        [XmlElement]
        public int LocationId
        {
          get { return m_locationId;}
          set { m_locationId = value; }
        }
      
        [XmlElement]
        public int RouteScheduleId
        {
          get { return m_routeScheduleId;}
          set { m_routeScheduleId = value; }
        }
      
        [XmlElement]
        public byte DayOfWeekNumber
        {
          get { return m_dayOfWeekNumber;}
          set { m_dayOfWeekNumber = value; }
        }
      
        [XmlElement]
        public int CustomerId
        {
          get { return m_customerId;}
          set { m_customerId = value; }
        }
      
        [XmlElement]
        public int RouteNumber
        {
          get { return m_routeNumber;}
          set { m_routeNumber = value; }
        }
      
        [XmlElement]
        public int Frequency
        {
          get { return m_frequency;}
          set { m_frequency = value; }
        }
      
        [XmlElement]
        public int Sequence
        {
          get { return m_sequence;}
          set { m_sequence = value; }
        }
      
        [XmlElement]
        public DateTime StartDate
        {
          get { return m_startDate;}
          set { m_startDate = value; }
        }
      
        [XmlElement]
        public DateTime? EndDate
        {
          get { return m_endDate;}
          set { m_endDate = value; }
        }
      
      }
      #endregion
      }

    