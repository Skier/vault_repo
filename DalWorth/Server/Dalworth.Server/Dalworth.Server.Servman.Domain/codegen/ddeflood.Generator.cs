
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


      public partial class ddeflood
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ddeflood ( " +
      
        " ticket_num, " +
      
        " serv_type, " +
      
        " item_num, " +
      
        " note, " +
      
        " amount, " +
      
        " exempt, " +
      
        " infran, " +
      
        " orig_book, " +
      
        " commission, " +
      
        " enter_by, " +
      
        " auto_prc, " +
      
        " prc_type, " +
      
        " num_units, " +
      
        " unit_prc, " +
      
        " base_prc, " +
      
        " user_adj, " +
      
        " manu_type, " +
      
        " manu_amt, " +
      
        " part_units, " +
      
        " apply2mp, " +
      
        " schddisc, " +
      
        " rs_cost " +
      
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
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ? " +
      
      ")";

      public static void Insert(ddeflood ddeflood)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@ticket_num", ddeflood.ticket_num);
      
        Database.PutParameter(dbCommand,"@serv_type", ddeflood.serv_type);
      
        Database.PutParameter(dbCommand,"@item_num", ddeflood.item_num);
      
        Database.PutParameter(dbCommand,"@note", ddeflood.note);
      
        Database.PutParameter(dbCommand,"@amount", ddeflood.amount);
      
        Database.PutParameter(dbCommand,"@exempt", ddeflood.exempt);
      
        Database.PutParameter(dbCommand,"@infran", ddeflood.infran);
      
        Database.PutParameter(dbCommand,"@orig_book", ddeflood.orig_book);
      
        Database.PutParameter(dbCommand,"@commission", ddeflood.commission);
      
        Database.PutParameter(dbCommand,"@enter_by", ddeflood.enter_by);
      
        Database.PutParameter(dbCommand,"@auto_prc", ddeflood.auto_prc);
      
        Database.PutParameter(dbCommand,"@prc_type", ddeflood.prc_type);
      
        Database.PutParameter(dbCommand,"@num_units", ddeflood.num_units);
      
        Database.PutParameter(dbCommand,"@unit_prc", ddeflood.unit_prc);
      
        Database.PutParameter(dbCommand,"@base_prc", ddeflood.base_prc);
      
        Database.PutParameter(dbCommand,"@user_adj", ddeflood.user_adj);
      
        Database.PutParameter(dbCommand,"@manu_type", ddeflood.manu_type);
      
        Database.PutParameter(dbCommand,"@manu_amt", ddeflood.manu_amt);
      
        Database.PutParameter(dbCommand,"@part_units", ddeflood.part_units);
      
        Database.PutParameter(dbCommand,"@apply2mp", ddeflood.apply2mp);
      
        Database.PutParameter(dbCommand,"@schddisc", ddeflood.schddisc);
      
        Database.PutParameter(dbCommand,"@rs_cost", ddeflood.rs_cost);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<ddeflood>  ddefloodList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(ddeflood ddeflood in  ddefloodList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ticket_num", ddeflood.ticket_num);
      
        Database.PutParameter(dbCommand,"@serv_type", ddeflood.serv_type);
      
        Database.PutParameter(dbCommand,"@item_num", ddeflood.item_num);
      
        Database.PutParameter(dbCommand,"@note", ddeflood.note);
      
        Database.PutParameter(dbCommand,"@amount", ddeflood.amount);
      
        Database.PutParameter(dbCommand,"@exempt", ddeflood.exempt);
      
        Database.PutParameter(dbCommand,"@infran", ddeflood.infran);
      
        Database.PutParameter(dbCommand,"@orig_book", ddeflood.orig_book);
      
        Database.PutParameter(dbCommand,"@commission", ddeflood.commission);
      
        Database.PutParameter(dbCommand,"@enter_by", ddeflood.enter_by);
      
        Database.PutParameter(dbCommand,"@auto_prc", ddeflood.auto_prc);
      
        Database.PutParameter(dbCommand,"@prc_type", ddeflood.prc_type);
      
        Database.PutParameter(dbCommand,"@num_units", ddeflood.num_units);
      
        Database.PutParameter(dbCommand,"@unit_prc", ddeflood.unit_prc);
      
        Database.PutParameter(dbCommand,"@base_prc", ddeflood.base_prc);
      
        Database.PutParameter(dbCommand,"@user_adj", ddeflood.user_adj);
      
        Database.PutParameter(dbCommand,"@manu_type", ddeflood.manu_type);
      
        Database.PutParameter(dbCommand,"@manu_amt", ddeflood.manu_amt);
      
        Database.PutParameter(dbCommand,"@part_units", ddeflood.part_units);
      
        Database.PutParameter(dbCommand,"@apply2mp", ddeflood.apply2mp);
      
        Database.PutParameter(dbCommand,"@schddisc", ddeflood.schddisc);
      
        Database.PutParameter(dbCommand,"@rs_cost", ddeflood.rs_cost);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ticket_num",ddeflood.ticket_num);
      
        Database.UpdateParameter(dbCommand,"@serv_type",ddeflood.serv_type);
      
        Database.UpdateParameter(dbCommand,"@item_num",ddeflood.item_num);
      
        Database.UpdateParameter(dbCommand,"@note",ddeflood.note);
      
        Database.UpdateParameter(dbCommand,"@amount",ddeflood.amount);
      
        Database.UpdateParameter(dbCommand,"@exempt",ddeflood.exempt);
      
        Database.UpdateParameter(dbCommand,"@infran",ddeflood.infran);
      
        Database.UpdateParameter(dbCommand,"@orig_book",ddeflood.orig_book);
      
        Database.UpdateParameter(dbCommand,"@commission",ddeflood.commission);
      
        Database.UpdateParameter(dbCommand,"@enter_by",ddeflood.enter_by);
      
        Database.UpdateParameter(dbCommand,"@auto_prc",ddeflood.auto_prc);
      
        Database.UpdateParameter(dbCommand,"@prc_type",ddeflood.prc_type);
      
        Database.UpdateParameter(dbCommand,"@num_units",ddeflood.num_units);
      
        Database.UpdateParameter(dbCommand,"@unit_prc",ddeflood.unit_prc);
      
        Database.UpdateParameter(dbCommand,"@base_prc",ddeflood.base_prc);
      
        Database.UpdateParameter(dbCommand,"@user_adj",ddeflood.user_adj);
      
        Database.UpdateParameter(dbCommand,"@manu_type",ddeflood.manu_type);
      
        Database.UpdateParameter(dbCommand,"@manu_amt",ddeflood.manu_amt);
      
        Database.UpdateParameter(dbCommand,"@part_units",ddeflood.part_units);
      
        Database.UpdateParameter(dbCommand,"@apply2mp",ddeflood.apply2mp);
      
        Database.UpdateParameter(dbCommand,"@schddisc",ddeflood.schddisc);
      
        Database.UpdateParameter(dbCommand,"@rs_cost",ddeflood.rs_cost);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update ddeflood Set "
      
        + " ddeflood.serv_type = ? , "
      
        + " ddeflood.note = ? , "
      
        + " ddeflood.amount = ? , "
      
        + " ddeflood.exempt = ? , "
      
        + " ddeflood.infran = ? , "
      
        + " ddeflood.orig_book = ? , "
      
        + " ddeflood.commission = ? , "
      
        + " ddeflood.enter_by = ? , "
      
        + " ddeflood.auto_prc = ? , "
      
        + " ddeflood.prc_type = ? , "
      
        + " ddeflood.num_units = ? , "
      
        + " ddeflood.unit_prc = ? , "
      
        + " ddeflood.base_prc = ? , "
      
        + " ddeflood.user_adj = ? , "
      
        + " ddeflood.manu_type = ? , "
      
        + " ddeflood.manu_amt = ? , "
      
        + " ddeflood.part_units = ? , "
      
        + " ddeflood.apply2mp = ? , "
      
        + " ddeflood.schddisc = ? , "
      
        + " ddeflood.rs_cost = ?  "
      
        + " Where "
        
          + " ddeflood.ticket_num = ?  and  "
        
          + " ddeflood.item_num = ?  "
        
      ;

      public static void Update(ddeflood ddeflood)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      
//        Database.PutParameter(dbCommand,"@ticket_num", ddeflood.ticket_num);
      
        Database.PutParameter(dbCommand,"@serv_type", ddeflood.serv_type);
      
//        Database.PutParameter(dbCommand,"@item_num", ddeflood.item_num);
      
        Database.PutParameter(dbCommand,"@note", ddeflood.note);
      
        Database.PutParameter(dbCommand,"@amount", ddeflood.amount);
      
        Database.PutParameter(dbCommand,"@exempt", ddeflood.exempt);
      
        Database.PutParameter(dbCommand,"@infran", ddeflood.infran);
      
        Database.PutParameter(dbCommand,"@orig_book", ddeflood.orig_book);
      
        Database.PutParameter(dbCommand,"@commission", ddeflood.commission);
      
        Database.PutParameter(dbCommand,"@enter_by", ddeflood.enter_by);
      
        Database.PutParameter(dbCommand,"@auto_prc", ddeflood.auto_prc);
      
        Database.PutParameter(dbCommand,"@prc_type", ddeflood.prc_type);
      
        Database.PutParameter(dbCommand,"@num_units", ddeflood.num_units);
      
        Database.PutParameter(dbCommand,"@unit_prc", ddeflood.unit_prc);
      
        Database.PutParameter(dbCommand,"@base_prc", ddeflood.base_prc);
      
        Database.PutParameter(dbCommand,"@user_adj", ddeflood.user_adj);
      
        Database.PutParameter(dbCommand,"@manu_type", ddeflood.manu_type);
      
        Database.PutParameter(dbCommand,"@manu_amt", ddeflood.manu_amt);
      
        Database.PutParameter(dbCommand,"@part_units", ddeflood.part_units);
      
        Database.PutParameter(dbCommand,"@apply2mp", ddeflood.apply2mp);
      
        Database.PutParameter(dbCommand,"@schddisc", ddeflood.schddisc);
      
        Database.PutParameter(dbCommand,"@rs_cost", ddeflood.rs_cost);
      
        Database.PutParameter(dbCommand,"@ticket_num", ddeflood.ticket_num);
      
        Database.PutParameter(dbCommand,"@item_num", ddeflood.item_num);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ddeflood.ticket_num, "
      
        + " ddeflood.serv_type, "
      
        + " ddeflood.item_num, "
      
        + " ddeflood.note, "
      
        + " ddeflood.amount, "
      
        + " ddeflood.exempt, "
      
        + " ddeflood.infran, "
      
        + " ddeflood.orig_book, "
      
        + " ddeflood.commission, "
      
        + " ddeflood.enter_by, "
      
        + " ddeflood.auto_prc, "
      
        + " ddeflood.prc_type, "
      
        + " ddeflood.num_units, "
      
        + " ddeflood.unit_prc, "
      
        + " ddeflood.base_prc, "
      
        + " ddeflood.user_adj, "
      
        + " ddeflood.manu_type, "
      
        + " ddeflood.manu_amt, "
      
        + " ddeflood.part_units, "
      
        + " ddeflood.apply2mp, "
      
        + " ddeflood.schddisc, "
      
        + " ddeflood.rs_cost "
      

      + " From ddeflood "

      
        + " Where "
        
          + " ddeflood.ticket_num = ?  and  "
        
          + " ddeflood.item_num = ?  "
        
      ;

      public static ddeflood FindByPrimaryKey(
      String ticket_num,int item_num
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@ticket_num", ticket_num);
      
        Database.PutParameter(dbCommand,"@item_num", item_num);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("ddeflood not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(ddeflood ddeflood)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@ticket_num",ddeflood.ticket_num);
      
        Database.PutParameter(dbCommand,"@item_num",ddeflood.item_num);
      

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
      String sql = "select 1 from ddeflood";

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

      public static ddeflood Load(IDataReader dataReader)
      {
      ddeflood ddeflood = new ddeflood();

      ddeflood.ticket_num = dataReader.GetString(0);
          ddeflood.serv_type = dataReader.GetString(1);
          ddeflood.item_num = dataReader.GetInt32(2);
          ddeflood.note = dataReader.GetString(3);
          ddeflood.amount = dataReader.GetFloat(4);
          ddeflood.exempt = dataReader.GetBoolean(5);
          ddeflood.infran = dataReader.GetBoolean(6);
          ddeflood.orig_book = dataReader.GetBoolean(7);
          ddeflood.commission = dataReader.GetInt32(8);
          ddeflood.enter_by = dataReader.GetString(9);
          ddeflood.auto_prc = dataReader.GetBoolean(10);
          ddeflood.prc_type = dataReader.GetString(11);
          ddeflood.num_units = dataReader.GetFloat(12);
          ddeflood.unit_prc = dataReader.GetFloat(13);
          ddeflood.base_prc = dataReader.GetFloat(14);
          ddeflood.user_adj = dataReader.GetString(15);
          ddeflood.manu_type = dataReader.GetInt32(16);
          ddeflood.manu_amt = dataReader.GetFloat(17);
          ddeflood.part_units = dataReader.GetFloat(18);
          ddeflood.apply2mp = dataReader.GetBoolean(19);
          ddeflood.schddisc = dataReader.GetBoolean(20);
          ddeflood.rs_cost = dataReader.GetFloat(21);
          

      return ddeflood;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [ddeflood] "

      
        + " Where "
        
          + " ticket_num = ?  and  "
        
          + " item_num = ?  "
        
      ;
      public static void Delete(ddeflood ddeflood)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      
        Database.PutParameter(dbCommand,"@ticket_num", ddeflood.ticket_num);
      
        Database.PutParameter(dbCommand,"@item_num", ddeflood.item_num);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [ddeflood] ";

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

      
        + " ddeflood.ticket_num, "
      
        + " ddeflood.serv_type, "
      
        + " ddeflood.item_num, "
      
        + " ddeflood.note, "
      
        + " ddeflood.amount, "
      
        + " ddeflood.exempt, "
      
        + " ddeflood.infran, "
      
        + " ddeflood.orig_book, "
      
        + " ddeflood.commission, "
      
        + " ddeflood.enter_by, "
      
        + " ddeflood.auto_prc, "
      
        + " ddeflood.prc_type, "
      
        + " ddeflood.num_units, "
      
        + " ddeflood.unit_prc, "
      
        + " ddeflood.base_prc, "
      
        + " ddeflood.user_adj, "
      
        + " ddeflood.manu_type, "
      
        + " ddeflood.manu_amt, "
      
        + " ddeflood.part_units, "
      
        + " ddeflood.apply2mp, "
      
        + " ddeflood.schddisc, "
      
        + " ddeflood.rs_cost "
      

      + " From ddeflood ";
      public static List<ddeflood> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<ddeflood> rv = new List<ddeflood>();

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
      List<ddeflood> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<ddeflood> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ddeflood));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ddeflood item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ddeflood>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ddeflood));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ddeflood> itemsList
      = new List<ddeflood>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ddeflood)
      itemsList.Add(deserializedObject as ddeflood);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_ticket_num;
      
        protected String m_serv_type;
      
        protected int m_item_num;
      
        protected String m_note;
      
        protected float m_amount;
      
        protected bool m_exempt;
      
        protected bool m_infran;
      
        protected bool m_orig_book;
      
        protected int m_commission;
      
        protected String m_enter_by;
      
        protected bool m_auto_prc;
      
        protected String m_prc_type;
      
        protected float m_num_units;
      
        protected float m_unit_prc;
      
        protected float m_base_prc;
      
        protected String m_user_adj;
      
        protected int m_manu_type;
      
        protected float m_manu_amt;
      
        protected float m_part_units;
      
        protected bool m_apply2mp;
      
        protected bool m_schddisc;
      
        protected float m_rs_cost;
      
      #endregion

      #region Constructors
      public ddeflood(
      String 
          ticket_num,int 
          item_num
      )
      {
      
        m_ticket_num = ticket_num;
      
        m_item_num = item_num;
      
      }

      


        public ddeflood(
        String 
          ticket_num,String 
          serv_type,int 
          item_num,String 
          note,float 
          amount,bool 
          exempt,bool 
          infran,bool 
          orig_book,int 
          commission,String 
          enter_by,bool 
          auto_prc,String 
          prc_type,float 
          num_units,float 
          unit_prc,float 
          base_prc,String 
          user_adj,int 
          manu_type,float 
          manu_amt,float 
          part_units,bool 
          apply2mp,bool 
          schddisc,float 
          rs_cost
        )
        {
        
          m_ticket_num = ticket_num;
        
          m_serv_type = serv_type;
        
          m_item_num = item_num;
        
          m_note = note;
        
          m_amount = amount;
        
          m_exempt = exempt;
        
          m_infran = infran;
        
          m_orig_book = orig_book;
        
          m_commission = commission;
        
          m_enter_by = enter_by;
        
          m_auto_prc = auto_prc;
        
          m_prc_type = prc_type;
        
          m_num_units = num_units;
        
          m_unit_prc = unit_prc;
        
          m_base_prc = base_prc;
        
          m_user_adj = user_adj;
        
          m_manu_type = manu_type;
        
          m_manu_amt = manu_amt;
        
          m_part_units = part_units;
        
          m_apply2mp = apply2mp;
        
          m_schddisc = schddisc;
        
          m_rs_cost = rs_cost;
        
        }


      
      #endregion

      
        [XmlElement]
        public String ticket_num
        {
        get { return m_ticket_num;}
        set { m_ticket_num = value; }
        }
      
        [XmlElement]
        public String serv_type
        {
        get { return m_serv_type;}
        set { m_serv_type = value; }
        }
      
        [XmlElement]
        public int item_num
        {
        get { return m_item_num;}
        set { m_item_num = value; }
        }
      
        [XmlElement]
        public String note
        {
        get { return m_note;}
        set { m_note = value; }
        }
      
        [XmlElement]
        public float amount
        {
        get { return m_amount;}
        set { m_amount = value; }
        }
      
        [XmlElement]
        public bool exempt
        {
        get { return m_exempt;}
        set { m_exempt = value; }
        }
      
        [XmlElement]
        public bool infran
        {
        get { return m_infran;}
        set { m_infran = value; }
        }
      
        [XmlElement]
        public bool orig_book
        {
        get { return m_orig_book;}
        set { m_orig_book = value; }
        }
      
        [XmlElement]
        public int commission
        {
        get { return m_commission;}
        set { m_commission = value; }
        }
      
        [XmlElement]
        public String enter_by
        {
        get { return m_enter_by;}
        set { m_enter_by = value; }
        }
      
        [XmlElement]
        public bool auto_prc
        {
        get { return m_auto_prc;}
        set { m_auto_prc = value; }
        }
      
        [XmlElement]
        public String prc_type
        {
        get { return m_prc_type;}
        set { m_prc_type = value; }
        }
      
        [XmlElement]
        public float num_units
        {
        get { return m_num_units;}
        set { m_num_units = value; }
        }
      
        [XmlElement]
        public float unit_prc
        {
        get { return m_unit_prc;}
        set { m_unit_prc = value; }
        }
      
        [XmlElement]
        public float base_prc
        {
        get { return m_base_prc;}
        set { m_base_prc = value; }
        }
      
        [XmlElement]
        public String user_adj
        {
        get { return m_user_adj;}
        set { m_user_adj = value; }
        }
      
        [XmlElement]
        public int manu_type
        {
        get { return m_manu_type;}
        set { m_manu_type = value; }
        }
      
        [XmlElement]
        public float manu_amt
        {
        get { return m_manu_amt;}
        set { m_manu_amt = value; }
        }
      
        [XmlElement]
        public float part_units
        {
        get { return m_part_units;}
        set { m_part_units = value; }
        }
      
        [XmlElement]
        public bool apply2mp
        {
        get { return m_apply2mp;}
        set { m_apply2mp = value; }
        }
      
        [XmlElement]
        public bool schddisc
        {
        get { return m_schddisc;}
        set { m_schddisc = value; }
        }
      
        [XmlElement]
        public float rs_cost
        {
        get { return m_rs_cost;}
        set { m_rs_cost = value; }
        }
      
      }
      #endregion
      }

    