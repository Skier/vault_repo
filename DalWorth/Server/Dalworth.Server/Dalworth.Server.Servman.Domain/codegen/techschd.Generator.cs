
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


      public partial class techschd
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into techschd ( " +
      
        " tech_id, " +
      
        " date, " +
      
        " promise, " +
      
        " done, " +
      
        " dwc_fault, " +
      
        " amt_day, " +
      
        " num_pjob, " +
      
        " hours, " +
      
        " amt_os, " +
      
        " num_rej, " +
      
        " num_gjob, " +
      
        " cum_grade, " +
      
        " num_refer, " +
      
        " truck_num, " +
      
        " hold, " +
      
        " note, " +
      
        " disp_id, " +
      
        " odometer, " +
      
        " truck_id " +
      
      ") Values (" +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ? " +
      
      ")";

      public static void Insert(techschd techschd)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@tech_id", techschd.tech_id);
      
        Database.PutParameter(dbCommand,"@date", techschd.date);
      
        Database.PutParameter(dbCommand,"@promise", techschd.promise);
      
        Database.PutParameter(dbCommand,"@done", techschd.done);
      
        Database.PutParameter(dbCommand,"@dwc_fault", techschd.dwc_fault);
      
        Database.PutParameter(dbCommand,"@amt_day", techschd.amt_day);
      
        Database.PutParameter(dbCommand,"@num_pjob", techschd.num_pjob);
      
        Database.PutParameter(dbCommand,"@hours", techschd.hours);
      
        Database.PutParameter(dbCommand,"@amt_os", techschd.amt_os);
      
        Database.PutParameter(dbCommand,"@num_rej", techschd.num_rej);
      
        Database.PutParameter(dbCommand,"@num_gjob", techschd.num_gjob);
      
        Database.PutParameter(dbCommand,"@cum_grade", techschd.cum_grade);
      
        Database.PutParameter(dbCommand,"@num_refer", techschd.num_refer);
      
        Database.PutParameter(dbCommand,"@truck_num", techschd.truck_num);
      
        Database.PutParameter(dbCommand,"@hold", techschd.hold);
      
        Database.PutParameter(dbCommand,"@note", techschd.note);
      
        Database.PutParameter(dbCommand,"@disp_id", techschd.disp_id);
      
        Database.PutParameter(dbCommand,"@odometer", techschd.odometer);
      
        Database.PutParameter(dbCommand,"@truck_id", techschd.truck_id);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<techschd>  techschdList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(techschd techschd in  techschdList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@tech_id", techschd.tech_id);
      
        Database.PutParameter(dbCommand,"@date", techschd.date);
      
        Database.PutParameter(dbCommand,"@promise", techschd.promise);
      
        Database.PutParameter(dbCommand,"@done", techschd.done);
      
        Database.PutParameter(dbCommand,"@dwc_fault", techschd.dwc_fault);
      
        Database.PutParameter(dbCommand,"@amt_day", techschd.amt_day);
      
        Database.PutParameter(dbCommand,"@num_pjob", techschd.num_pjob);
      
        Database.PutParameter(dbCommand,"@hours", techschd.hours);
      
        Database.PutParameter(dbCommand,"@amt_os", techschd.amt_os);
      
        Database.PutParameter(dbCommand,"@num_rej", techschd.num_rej);
      
        Database.PutParameter(dbCommand,"@num_gjob", techschd.num_gjob);
      
        Database.PutParameter(dbCommand,"@cum_grade", techschd.cum_grade);
      
        Database.PutParameter(dbCommand,"@num_refer", techschd.num_refer);
      
        Database.PutParameter(dbCommand,"@truck_num", techschd.truck_num);
      
        Database.PutParameter(dbCommand,"@hold", techschd.hold);
      
        Database.PutParameter(dbCommand,"@note", techschd.note);
      
        Database.PutParameter(dbCommand,"@disp_id", techschd.disp_id);
      
        Database.PutParameter(dbCommand,"@odometer", techschd.odometer);
      
        Database.PutParameter(dbCommand,"@truck_id", techschd.truck_id);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@tech_id",techschd.tech_id);
      
        Database.UpdateParameter(dbCommand,"@date",techschd.date);
      
        Database.UpdateParameter(dbCommand,"@promise",techschd.promise);
      
        Database.UpdateParameter(dbCommand,"@done",techschd.done);
      
        Database.UpdateParameter(dbCommand,"@dwc_fault",techschd.dwc_fault);
      
        Database.UpdateParameter(dbCommand,"@amt_day",techschd.amt_day);
      
        Database.UpdateParameter(dbCommand,"@num_pjob",techschd.num_pjob);
      
        Database.UpdateParameter(dbCommand,"@hours",techschd.hours);
      
        Database.UpdateParameter(dbCommand,"@amt_os",techschd.amt_os);
      
        Database.UpdateParameter(dbCommand,"@num_rej",techschd.num_rej);
      
        Database.UpdateParameter(dbCommand,"@num_gjob",techschd.num_gjob);
      
        Database.UpdateParameter(dbCommand,"@cum_grade",techschd.cum_grade);
      
        Database.UpdateParameter(dbCommand,"@num_refer",techschd.num_refer);
      
        Database.UpdateParameter(dbCommand,"@truck_num",techschd.truck_num);
      
        Database.UpdateParameter(dbCommand,"@hold",techschd.hold);
      
        Database.UpdateParameter(dbCommand,"@note",techschd.note);
      
        Database.UpdateParameter(dbCommand,"@disp_id",techschd.disp_id);
      
        Database.UpdateParameter(dbCommand,"@odometer",techschd.odometer);
      
        Database.UpdateParameter(dbCommand,"@truck_id",techschd.truck_id);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update techschd Set "
      
        + " techschd.promise = ? , "
      
        + " techschd.done = ? , "
      
        + " techschd.dwc_fault = ? , "
      
        + " techschd.amt_day = ? , "
      
        + " techschd.num_pjob = ? , "
      
        + " techschd.hours = ? , "
      
        + " techschd.amt_os = ? , "
      
        + " techschd.num_rej = ? , "
      
        + " techschd.num_gjob = ? , "
      
        + " techschd.cum_grade = ? , "
      
        + " techschd.num_refer = ? , "
      
        + " techschd.truck_num = ? , "
      
        + " techschd.hold = ? , "
      
        + " techschd.note = ? , "
      
        + " techschd.disp_id = ? , "
      
        + " techschd.odometer = ? , "
      
        + " techschd.truck_id = ?  "
      
        + " Where "
        
          + " techschd.tech_id = ?  and  "
        
          + " techschd.date = ?  "
        
      ;

      public static void Update(techschd techschd)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@promise", techschd.promise);
      
        Database.PutParameter(dbCommand,"@done", techschd.done);
      
        Database.PutParameter(dbCommand,"@dwc_fault", techschd.dwc_fault);
      
        Database.PutParameter(dbCommand,"@amt_day", techschd.amt_day);
      
        Database.PutParameter(dbCommand,"@num_pjob", techschd.num_pjob);
      
        Database.PutParameter(dbCommand,"@hours", techschd.hours);
      
        Database.PutParameter(dbCommand,"@amt_os", techschd.amt_os);
      
        Database.PutParameter(dbCommand,"@num_rej", techschd.num_rej);
      
        Database.PutParameter(dbCommand,"@num_gjob", techschd.num_gjob);
      
        Database.PutParameter(dbCommand,"@cum_grade", techschd.cum_grade);
      
        Database.PutParameter(dbCommand,"@num_refer", techschd.num_refer);
      
        Database.PutParameter(dbCommand,"@truck_num", techschd.truck_num);
      
        Database.PutParameter(dbCommand,"@hold", techschd.hold);
      
        Database.PutParameter(dbCommand,"@note", techschd.note);
      
        Database.PutParameter(dbCommand,"@disp_id", techschd.disp_id);
      
        Database.PutParameter(dbCommand,"@odometer", techschd.odometer);
      
        Database.PutParameter(dbCommand,"@truck_id", techschd.truck_id);
      
        Database.PutParameter(dbCommand,"@tech_id", techschd.tech_id);
      
        Database.PutParameter(dbCommand,"@date", techschd.date);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " techschd.tech_id, "
      
        + " techschd.date, "
      
        + " techschd.promise, "
      
        + " techschd.done, "
      
        + " techschd.dwc_fault, "
      
        + " techschd.amt_day, "
      
        + " techschd.num_pjob, "
      
        + " techschd.hours, "
      
        + " techschd.amt_os, "
      
        + " techschd.num_rej, "
      
        + " techschd.num_gjob, "
      
        + " techschd.cum_grade, "
      
        + " techschd.num_refer, "
      
        + " techschd.truck_num, "
      
        + " techschd.hold, "
      
        + " techschd.note, "
      
        + " techschd.disp_id, "
      
        + " techschd.odometer, "
      
        + " techschd.truck_id "
      

      + " From techschd "

      
        + " Where "
        
          + " techschd.tech_id = ?  and  "
        
          + " techschd.date = ?  "
        
      ;

      public static techschd FindByPrimaryKey(
      String tech_id,DateTime date
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@tech_id", tech_id);
      
        Database.PutParameter(dbCommand,"@date", date);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("techschd not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(techschd techschd)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@tech_id",techschd.tech_id);
      
        Database.PutParameter(dbCommand,"@date",techschd.date);
      

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
      String sql = "select 1 from techschd";

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

      public static techschd Load(IDataReader dataReader)
      {
      techschd techschd = new techschd();

      techschd.tech_id = dataReader.GetString(0);
          techschd.date = dataReader.GetDateTime(1);
          techschd.promise = dataReader.GetInt32(2);
          techschd.done = dataReader.GetInt32(3);
          techschd.dwc_fault = dataReader.GetInt32(4);
          techschd.amt_day = dataReader.GetFloat(5);
          techschd.num_pjob = dataReader.GetInt32(6);
          techschd.hours = dataReader.GetFloat(7);
          techschd.amt_os = dataReader.GetFloat(8);
          techschd.num_rej = dataReader.GetInt32(9);
          techschd.num_gjob = dataReader.GetInt32(10);
          techschd.cum_grade = dataReader.GetFloat(11);
          techschd.num_refer = dataReader.GetInt32(12);
          techschd.truck_num = dataReader.GetString(13);
          techschd.hold = dataReader.GetBoolean(14);
          techschd.note = dataReader.GetString(15);
          techschd.disp_id = dataReader.GetString(16);
          techschd.odometer = dataReader.GetFloat(17);
          techschd.truck_id = dataReader.GetString(18);
          

      return techschd;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [techschd] "

      
        + " Where "
        
          + " tech_id = ?  and  "
        
          + " date = ?  "
        
      ;
      public static void Delete(techschd techschd)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      
        Database.PutParameter(dbCommand,"@tech_id", techschd.tech_id);
      
        Database.PutParameter(dbCommand,"@date", techschd.date);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [techschd] ";

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

      
        + " techschd.tech_id, "
      
        + " techschd.date, "
      
        + " techschd.promise, "
      
        + " techschd.done, "
      
        + " techschd.dwc_fault, "
      
        + " techschd.amt_day, "
      
        + " techschd.num_pjob, "
      
        + " techschd.hours, "
      
        + " techschd.amt_os, "
      
        + " techschd.num_rej, "
      
        + " techschd.num_gjob, "
      
        + " techschd.cum_grade, "
      
        + " techschd.num_refer, "
      
        + " techschd.truck_num, "
      
        + " techschd.hold, "
      
        + " techschd.note, "
      
        + " techschd.disp_id, "
      
        + " techschd.odometer, "
      
        + " techschd.truck_id "
      

      + " From techschd ";
      public static List<techschd> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<techschd> rv = new List<techschd>();

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
      List<techschd> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<techschd> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(techschd));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(techschd item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<techschd>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(techschd));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<techschd> itemsList
      = new List<techschd>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is techschd)
      itemsList.Add(deserializedObject as techschd);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_tech_id;
      
        protected DateTime m_date;
      
        protected int m_promise;
      
        protected int m_done;
      
        protected int m_dwc_fault;
      
        protected float m_amt_day;
      
        protected int m_num_pjob;
      
        protected float m_hours;
      
        protected float m_amt_os;
      
        protected int m_num_rej;
      
        protected int m_num_gjob;
      
        protected float m_cum_grade;
      
        protected int m_num_refer;
      
        protected String m_truck_num;
      
        protected bool m_hold;
      
        protected String m_note;
      
        protected String m_disp_id;
      
        protected float m_odometer;
      
        protected String m_truck_id;
      
      #endregion

      #region Constructors
      public techschd(
      String 
          tech_id,DateTime 
          date
      )
      {
      
        m_tech_id = tech_id;
      
        m_date = date;
      
      }

      


        public techschd(
        String 
          tech_id,DateTime 
          date,int 
          promise,int 
          done,int 
          dwc_fault,float 
          amt_day,int 
          num_pjob,float 
          hours,float 
          amt_os,int 
          num_rej,int 
          num_gjob,float 
          cum_grade,int 
          num_refer,String 
          truck_num,bool 
          hold,String 
          note,String 
          disp_id,float 
          odometer,String 
          truck_id
        )
        {
        
          m_tech_id = tech_id;
        
          m_date = date;
        
          m_promise = promise;
        
          m_done = done;
        
          m_dwc_fault = dwc_fault;
        
          m_amt_day = amt_day;
        
          m_num_pjob = num_pjob;
        
          m_hours = hours;
        
          m_amt_os = amt_os;
        
          m_num_rej = num_rej;
        
          m_num_gjob = num_gjob;
        
          m_cum_grade = cum_grade;
        
          m_num_refer = num_refer;
        
          m_truck_num = truck_num;
        
          m_hold = hold;
        
          m_note = note;
        
          m_disp_id = disp_id;
        
          m_odometer = odometer;
        
          m_truck_id = truck_id;
        
        }


      
      #endregion

      
        [XmlElement]
        public String tech_id
        {
        get { return m_tech_id;}
        set { m_tech_id = value; }
        }
      
        [XmlElement]
        public DateTime date
        {
        get { return m_date;}
        set { m_date = value; }
        }
      
        [XmlElement]
        public int promise
        {
        get { return m_promise;}
        set { m_promise = value; }
        }
      
        [XmlElement]
        public int done
        {
        get { return m_done;}
        set { m_done = value; }
        }
      
        [XmlElement]
        public int dwc_fault
        {
        get { return m_dwc_fault;}
        set { m_dwc_fault = value; }
        }
      
        [XmlElement]
        public float amt_day
        {
        get { return m_amt_day;}
        set { m_amt_day = value; }
        }
      
        [XmlElement]
        public int num_pjob
        {
        get { return m_num_pjob;}
        set { m_num_pjob = value; }
        }
      
        [XmlElement]
        public float hours
        {
        get { return m_hours;}
        set { m_hours = value; }
        }
      
        [XmlElement]
        public float amt_os
        {
        get { return m_amt_os;}
        set { m_amt_os = value; }
        }
      
        [XmlElement]
        public int num_rej
        {
        get { return m_num_rej;}
        set { m_num_rej = value; }
        }
      
        [XmlElement]
        public int num_gjob
        {
        get { return m_num_gjob;}
        set { m_num_gjob = value; }
        }
      
        [XmlElement]
        public float cum_grade
        {
        get { return m_cum_grade;}
        set { m_cum_grade = value; }
        }
      
        [XmlElement]
        public int num_refer
        {
        get { return m_num_refer;}
        set { m_num_refer = value; }
        }
      
        [XmlElement]
        public String truck_num
        {
        get { return m_truck_num;}
        set { m_truck_num = value; }
        }
      
        [XmlElement]
        public bool hold
        {
        get { return m_hold;}
        set { m_hold = value; }
        }
      
        [XmlElement]
        public String note
        {
        get { return m_note;}
        set { m_note = value; }
        }
      
        [XmlElement]
        public String disp_id
        {
        get { return m_disp_id;}
        set { m_disp_id = value; }
        }
      
        [XmlElement]
        public float odometer
        {
        get { return m_odometer;}
        set { m_odometer = value; }
        }
      
        [XmlElement]
        public String truck_id
        {
        get { return m_truck_id;}
        set { m_truck_id = value; }
        }
      
      }
      #endregion
      }

    