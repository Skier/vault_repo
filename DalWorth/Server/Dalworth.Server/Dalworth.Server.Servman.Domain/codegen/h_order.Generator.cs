
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


      public partial class h_order
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into h_order ( " +
      
        " ticket_num, " +
      
        " cust_id, " +
      
        " alt_addr, " +
      
        " contact, " +
      
        " date, " +
      
        " time, " +
      
        " page, " +
      
        " grid, " +
      
        " area_id, " +
      
        " serv_type, " +
      
        " tran_type, " +
      
        " comp_type, " +
      
        " company, " +
      
        " tech_id, " +
      
        " amount, " +
      
        " tran_stat, " +
      
        " closer_id, " +
      
        " recve_amt, " +
      
        " pr_date, " +
      
        " zip, " +
      
        " bookby, " +
      
        " mapbook, " +
      
        " remcall, " +
      
        " companyid, " +
      
        " order_conf, " +
      
        " canc_conf, " +
      
        " rschd_conf, " +
      
        " done_conf, " +
      
        " qt_reqsted, " +
      
        " qt_done, " +
      
        " qt_convreq, " +
      
        " cleancnum, " +
      
        " survey_rem, " +
      
        " e_offer, " +
      
        " reminder, " +
      
        " exp_cred, " +
      
        " done_eref " +
      
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

      public static void Insert(h_order h_order)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@ticket_num", h_order.ticket_num);
      
        Database.PutParameter(dbCommand,"@cust_id", h_order.cust_id);
      
        Database.PutParameter(dbCommand,"@alt_addr", h_order.alt_addr);
      
        Database.PutParameter(dbCommand,"@contact", h_order.contact);
      
        Database.PutParameter(dbCommand,"@date", h_order.date);
      
        Database.PutParameter(dbCommand,"@time", h_order.time);
      
        Database.PutParameter(dbCommand,"@page", h_order.page);
      
        Database.PutParameter(dbCommand,"@grid", h_order.grid);
      
        Database.PutParameter(dbCommand,"@area_id", h_order.area_id);
      
        Database.PutParameter(dbCommand,"@serv_type", h_order.serv_type);
      
        Database.PutParameter(dbCommand,"@tran_type", h_order.tran_type);
      
        Database.PutParameter(dbCommand,"@comp_type", h_order.comp_type);
      
        Database.PutParameter(dbCommand,"@company", h_order.company);
      
        Database.PutParameter(dbCommand,"@tech_id", h_order.tech_id);
      
        Database.PutParameter(dbCommand,"@amount", h_order.amount);
      
        Database.PutParameter(dbCommand,"@tran_stat", h_order.tran_stat);
      
        Database.PutParameter(dbCommand,"@closer_id", h_order.closer_id);
      
        Database.PutParameter(dbCommand,"@recve_amt", h_order.recve_amt);
      
        Database.PutParameter(dbCommand,"@pr_date", h_order.pr_date);
      
        Database.PutParameter(dbCommand,"@zip", h_order.zip);
      
        Database.PutParameter(dbCommand,"@bookby", h_order.bookby);
      
        Database.PutParameter(dbCommand,"@mapbook", h_order.mapbook);
      
        Database.PutParameter(dbCommand,"@remcall", h_order.remcall);
      
        Database.PutParameter(dbCommand,"@companyid", h_order.companyid);
      
        Database.PutParameter(dbCommand,"@order_conf", h_order.order_conf);
      
        Database.PutParameter(dbCommand,"@canc_conf", h_order.canc_conf);
      
        Database.PutParameter(dbCommand,"@rschd_conf", h_order.rschd_conf);
      
        Database.PutParameter(dbCommand,"@done_conf", h_order.done_conf);
      
        Database.PutParameter(dbCommand,"@qt_reqsted", h_order.qt_reqsted);
      
        Database.PutParameter(dbCommand,"@qt_done", h_order.qt_done);
      
        Database.PutParameter(dbCommand,"@qt_convreq", h_order.qt_convreq);
      
        Database.PutParameter(dbCommand,"@cleancnum", h_order.cleancnum);
      
        Database.PutParameter(dbCommand,"@survey_rem", h_order.survey_rem);
      
        Database.PutParameter(dbCommand,"@e_offer", h_order.e_offer);
      
        Database.PutParameter(dbCommand,"@reminder", h_order.reminder);
      
        Database.PutParameter(dbCommand,"@exp_cred", h_order.exp_cred);
      
        Database.PutParameter(dbCommand,"@done_eref", h_order.done_eref);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<h_order>  h_orderList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(h_order h_order in  h_orderList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ticket_num", h_order.ticket_num);
      
        Database.PutParameter(dbCommand,"@cust_id", h_order.cust_id);
      
        Database.PutParameter(dbCommand,"@alt_addr", h_order.alt_addr);
      
        Database.PutParameter(dbCommand,"@contact", h_order.contact);
      
        Database.PutParameter(dbCommand,"@date", h_order.date);
      
        Database.PutParameter(dbCommand,"@time", h_order.time);
      
        Database.PutParameter(dbCommand,"@page", h_order.page);
      
        Database.PutParameter(dbCommand,"@grid", h_order.grid);
      
        Database.PutParameter(dbCommand,"@area_id", h_order.area_id);
      
        Database.PutParameter(dbCommand,"@serv_type", h_order.serv_type);
      
        Database.PutParameter(dbCommand,"@tran_type", h_order.tran_type);
      
        Database.PutParameter(dbCommand,"@comp_type", h_order.comp_type);
      
        Database.PutParameter(dbCommand,"@company", h_order.company);
      
        Database.PutParameter(dbCommand,"@tech_id", h_order.tech_id);
      
        Database.PutParameter(dbCommand,"@amount", h_order.amount);
      
        Database.PutParameter(dbCommand,"@tran_stat", h_order.tran_stat);
      
        Database.PutParameter(dbCommand,"@closer_id", h_order.closer_id);
      
        Database.PutParameter(dbCommand,"@recve_amt", h_order.recve_amt);
      
        Database.PutParameter(dbCommand,"@pr_date", h_order.pr_date);
      
        Database.PutParameter(dbCommand,"@zip", h_order.zip);
      
        Database.PutParameter(dbCommand,"@bookby", h_order.bookby);
      
        Database.PutParameter(dbCommand,"@mapbook", h_order.mapbook);
      
        Database.PutParameter(dbCommand,"@remcall", h_order.remcall);
      
        Database.PutParameter(dbCommand,"@companyid", h_order.companyid);
      
        Database.PutParameter(dbCommand,"@order_conf", h_order.order_conf);
      
        Database.PutParameter(dbCommand,"@canc_conf", h_order.canc_conf);
      
        Database.PutParameter(dbCommand,"@rschd_conf", h_order.rschd_conf);
      
        Database.PutParameter(dbCommand,"@done_conf", h_order.done_conf);
      
        Database.PutParameter(dbCommand,"@qt_reqsted", h_order.qt_reqsted);
      
        Database.PutParameter(dbCommand,"@qt_done", h_order.qt_done);
      
        Database.PutParameter(dbCommand,"@qt_convreq", h_order.qt_convreq);
      
        Database.PutParameter(dbCommand,"@cleancnum", h_order.cleancnum);
      
        Database.PutParameter(dbCommand,"@survey_rem", h_order.survey_rem);
      
        Database.PutParameter(dbCommand,"@e_offer", h_order.e_offer);
      
        Database.PutParameter(dbCommand,"@reminder", h_order.reminder);
      
        Database.PutParameter(dbCommand,"@exp_cred", h_order.exp_cred);
      
        Database.PutParameter(dbCommand,"@done_eref", h_order.done_eref);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ticket_num",h_order.ticket_num);
      
        Database.UpdateParameter(dbCommand,"@cust_id",h_order.cust_id);
      
        Database.UpdateParameter(dbCommand,"@alt_addr",h_order.alt_addr);
      
        Database.UpdateParameter(dbCommand,"@contact",h_order.contact);
      
        Database.UpdateParameter(dbCommand,"@date",h_order.date);
      
        Database.UpdateParameter(dbCommand,"@time",h_order.time);
      
        Database.UpdateParameter(dbCommand,"@page",h_order.page);
      
        Database.UpdateParameter(dbCommand,"@grid",h_order.grid);
      
        Database.UpdateParameter(dbCommand,"@area_id",h_order.area_id);
      
        Database.UpdateParameter(dbCommand,"@serv_type",h_order.serv_type);
      
        Database.UpdateParameter(dbCommand,"@tran_type",h_order.tran_type);
      
        Database.UpdateParameter(dbCommand,"@comp_type",h_order.comp_type);
      
        Database.UpdateParameter(dbCommand,"@company",h_order.company);
      
        Database.UpdateParameter(dbCommand,"@tech_id",h_order.tech_id);
      
        Database.UpdateParameter(dbCommand,"@amount",h_order.amount);
      
        Database.UpdateParameter(dbCommand,"@tran_stat",h_order.tran_stat);
      
        Database.UpdateParameter(dbCommand,"@closer_id",h_order.closer_id);
      
        Database.UpdateParameter(dbCommand,"@recve_amt",h_order.recve_amt);
      
        Database.UpdateParameter(dbCommand,"@pr_date",h_order.pr_date);
      
        Database.UpdateParameter(dbCommand,"@zip",h_order.zip);
      
        Database.UpdateParameter(dbCommand,"@bookby",h_order.bookby);
      
        Database.UpdateParameter(dbCommand,"@mapbook",h_order.mapbook);
      
        Database.UpdateParameter(dbCommand,"@remcall",h_order.remcall);
      
        Database.UpdateParameter(dbCommand,"@companyid",h_order.companyid);
      
        Database.UpdateParameter(dbCommand,"@order_conf",h_order.order_conf);
      
        Database.UpdateParameter(dbCommand,"@canc_conf",h_order.canc_conf);
      
        Database.UpdateParameter(dbCommand,"@rschd_conf",h_order.rschd_conf);
      
        Database.UpdateParameter(dbCommand,"@done_conf",h_order.done_conf);
      
        Database.UpdateParameter(dbCommand,"@qt_reqsted",h_order.qt_reqsted);
      
        Database.UpdateParameter(dbCommand,"@qt_done",h_order.qt_done);
      
        Database.UpdateParameter(dbCommand,"@qt_convreq",h_order.qt_convreq);
      
        Database.UpdateParameter(dbCommand,"@cleancnum",h_order.cleancnum);
      
        Database.UpdateParameter(dbCommand,"@survey_rem",h_order.survey_rem);
      
        Database.UpdateParameter(dbCommand,"@e_offer",h_order.e_offer);
      
        Database.UpdateParameter(dbCommand,"@reminder",h_order.reminder);
      
        Database.UpdateParameter(dbCommand,"@exp_cred",h_order.exp_cred);
      
        Database.UpdateParameter(dbCommand,"@done_eref",h_order.done_eref);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update h_order Set "
      
        + " h_order.cust_id = ? , "
      
        + " h_order.alt_addr = ? , "
      
        + " h_order.contact = ? , "
      
        + " h_order.date = ? , "
      
        + " h_order.time = ? , "
      
        + " h_order.page = ? , "
      
        + " h_order.grid = ? , "
      
        + " h_order.area_id = ? , "
      
        + " h_order.serv_type = ? , "
      
        + " h_order.tran_type = ? , "
      
        + " h_order.comp_type = ? , "
      
        + " h_order.company = ? , "
      
        + " h_order.tech_id = ? , "
      
        + " h_order.amount = ? , "
      
        + " h_order.tran_stat = ? , "
      
        + " h_order.closer_id = ? , "
      
        + " h_order.recve_amt = ? , "
      
        + " h_order.pr_date = ? , "
      
        + " h_order.zip = ? , "
      
        + " h_order.bookby = ? , "
      
        + " h_order.mapbook = ? , "
      
        + " h_order.remcall = ? , "
      
        + " h_order.companyid = ? , "
      
        + " h_order.order_conf = ? , "
      
        + " h_order.canc_conf = ? , "
      
        + " h_order.rschd_conf = ? , "
      
        + " h_order.done_conf = ? , "
      
        + " h_order.qt_reqsted = ? , "
      
        + " h_order.qt_done = ? , "
      
        + " h_order.qt_convreq = ? , "
      
        + " h_order.cleancnum = ? , "
      
        + " h_order.survey_rem = ? , "
      
        + " h_order.e_offer = ? , "
      
        + " h_order.reminder = ? , "
      
        + " h_order.exp_cred = ? , "
      
        + " h_order.done_eref = ?  "
      
        + " Where "
        
          + " h_order.ticket_num = ?  "
        
      ;

      public static void Update(h_order h_order)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@cust_id", h_order.cust_id);
      
        Database.PutParameter(dbCommand,"@alt_addr", h_order.alt_addr);
      
        Database.PutParameter(dbCommand,"@contact", h_order.contact);
      
        Database.PutParameter(dbCommand,"@date", h_order.date);
      
        Database.PutParameter(dbCommand,"@time", h_order.time);
      
        Database.PutParameter(dbCommand,"@page", h_order.page);
      
        Database.PutParameter(dbCommand,"@grid", h_order.grid);
      
        Database.PutParameter(dbCommand,"@area_id", h_order.area_id);
      
        Database.PutParameter(dbCommand,"@serv_type", h_order.serv_type);
      
        Database.PutParameter(dbCommand,"@tran_type", h_order.tran_type);
      
        Database.PutParameter(dbCommand,"@comp_type", h_order.comp_type);
      
        Database.PutParameter(dbCommand,"@company", h_order.company);
      
        Database.PutParameter(dbCommand,"@tech_id", h_order.tech_id);
      
        Database.PutParameter(dbCommand,"@amount", h_order.amount);
      
        Database.PutParameter(dbCommand,"@tran_stat", h_order.tran_stat);
      
        Database.PutParameter(dbCommand,"@closer_id", h_order.closer_id);
      
        Database.PutParameter(dbCommand,"@recve_amt", h_order.recve_amt);
      
        Database.PutParameter(dbCommand,"@pr_date", h_order.pr_date);
      
        Database.PutParameter(dbCommand,"@zip", h_order.zip);
      
        Database.PutParameter(dbCommand,"@bookby", h_order.bookby);
      
        Database.PutParameter(dbCommand,"@mapbook", h_order.mapbook);
      
        Database.PutParameter(dbCommand,"@remcall", h_order.remcall);
      
        Database.PutParameter(dbCommand,"@companyid", h_order.companyid);
      
        Database.PutParameter(dbCommand,"@order_conf", h_order.order_conf);
      
        Database.PutParameter(dbCommand,"@canc_conf", h_order.canc_conf);
      
        Database.PutParameter(dbCommand,"@rschd_conf", h_order.rschd_conf);
      
        Database.PutParameter(dbCommand,"@done_conf", h_order.done_conf);
      
        Database.PutParameter(dbCommand,"@qt_reqsted", h_order.qt_reqsted);
      
        Database.PutParameter(dbCommand,"@qt_done", h_order.qt_done);
      
        Database.PutParameter(dbCommand,"@qt_convreq", h_order.qt_convreq);
      
        Database.PutParameter(dbCommand,"@cleancnum", h_order.cleancnum);
      
        Database.PutParameter(dbCommand,"@survey_rem", h_order.survey_rem);
      
        Database.PutParameter(dbCommand,"@e_offer", h_order.e_offer);
      
        Database.PutParameter(dbCommand,"@reminder", h_order.reminder);
      
        Database.PutParameter(dbCommand,"@exp_cred", h_order.exp_cred);
      
        Database.PutParameter(dbCommand,"@done_eref", h_order.done_eref);
      
        Database.PutParameter(dbCommand,"@ticket_num", h_order.ticket_num);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " h_order.ticket_num, "
      
        + " h_order.cust_id, "
      
        + " h_order.alt_addr, "
      
        + " h_order.contact, "
      
        + " h_order.date, "
      
        + " h_order.time, "
      
        + " h_order.page, "
      
        + " h_order.grid, "
      
        + " h_order.area_id, "
      
        + " h_order.serv_type, "
      
        + " h_order.tran_type, "
      
        + " h_order.comp_type, "
      
        + " h_order.company, "
      
        + " h_order.tech_id, "
      
        + " h_order.amount, "
      
        + " h_order.tran_stat, "
      
        + " h_order.closer_id, "
      
        + " h_order.recve_amt, "
      
        + " h_order.pr_date, "
      
        + " h_order.zip, "
      
        + " h_order.bookby, "
      
        + " h_order.mapbook, "
      
        + " h_order.remcall, "
      
        + " h_order.companyid, "
      
        + " h_order.order_conf, "
      
        + " h_order.canc_conf, "
      
        + " h_order.rschd_conf, "
      
        + " h_order.done_conf, "
      
        + " h_order.qt_reqsted, "
      
        + " h_order.qt_done, "
      
        + " h_order.qt_convreq, "
      
        + " h_order.cleancnum, "
      
        + " h_order.survey_rem, "
      
        + " h_order.e_offer, "
      
        + " h_order.reminder, "
      
        + " h_order.exp_cred, "
      
        + " h_order.done_eref "
      

      + " From h_order "

      
        + " Where "
        
          + " h_order.ticket_num = ?  "
        
      ;

      public static h_order FindByPrimaryKey(
      String ticket_num
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@ticket_num", ticket_num);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("h_order not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(h_order h_order)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@ticket_num",h_order.ticket_num);
      

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
      String sql = "select 1 from h_order";

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

      public static h_order Load(IDataReader dataReader)
      {
      h_order h_order = new h_order();

      h_order.ticket_num = dataReader.GetString(0);
          h_order.cust_id = dataReader.GetString(1);
          h_order.alt_addr = dataReader.GetBoolean(2);
          h_order.contact = dataReader.GetString(3);
          h_order.date = dataReader.GetDateTime(4);
          h_order.time = dataReader.GetString(5);
          h_order.page = dataReader.GetString(6);
          h_order.grid = dataReader.GetString(7);
          h_order.area_id = dataReader.GetString(8);
          h_order.serv_type = dataReader.GetInt32(9);
          h_order.tran_type = dataReader.GetInt32(10);
          h_order.comp_type = dataReader.GetInt32(11);
          h_order.company = dataReader.GetString(12);
          h_order.tech_id = dataReader.GetString(13);
          h_order.amount = dataReader.GetFloat(14);
          h_order.tran_stat = dataReader.GetInt32(15);
          h_order.closer_id = dataReader.GetString(16);
          h_order.recve_amt = dataReader.GetFloat(17);
          h_order.pr_date = dataReader.GetDateTime(18);
          h_order.zip = dataReader.GetString(19);
          h_order.bookby = dataReader.GetString(20);
          h_order.mapbook = dataReader.GetString(21);
          h_order.remcall = dataReader.GetInt32(22);
          h_order.companyid = dataReader.GetInt32(23);
          h_order.order_conf = dataReader.GetBoolean(24);
          h_order.canc_conf = dataReader.GetBoolean(25);
          h_order.rschd_conf = dataReader.GetInt32(26);
          h_order.done_conf = dataReader.GetBoolean(27);
          h_order.qt_reqsted = dataReader.GetBoolean(28);
          h_order.qt_done = dataReader.GetBoolean(29);
          h_order.qt_convreq = dataReader.GetBoolean(30);
          h_order.cleancnum = dataReader.GetString(31);
          h_order.survey_rem = dataReader.GetBoolean(32);
          h_order.e_offer = dataReader.GetBoolean(33);
          h_order.reminder = dataReader.GetBoolean(34);
          h_order.exp_cred = dataReader.GetBoolean(35);
          h_order.done_eref = dataReader.GetBoolean(36);
          

      return h_order;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [h_order] "

      
        + " Where "
        
          + " ticket_num = ?  "
        
      ;
      public static void Delete(h_order h_order)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      
        Database.PutParameter(dbCommand,"@ticket_num", h_order.ticket_num);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [h_order] ";

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

      
        + " h_order.ticket_num, "
      
        + " h_order.cust_id, "
      
        + " h_order.alt_addr, "
      
        + " h_order.contact, "
      
        + " h_order.date, "
      
        + " h_order.time, "
      
        + " h_order.page, "
      
        + " h_order.grid, "
      
        + " h_order.area_id, "
      
        + " h_order.serv_type, "
      
        + " h_order.tran_type, "
      
        + " h_order.comp_type, "
      
        + " h_order.company, "
      
        + " h_order.tech_id, "
      
        + " h_order.amount, "
      
        + " h_order.tran_stat, "
      
        + " h_order.closer_id, "
      
        + " h_order.recve_amt, "
      
        + " h_order.pr_date, "
      
        + " h_order.zip, "
      
        + " h_order.bookby, "
      
        + " h_order.mapbook, "
      
        + " h_order.remcall, "
      
        + " h_order.companyid, "
      
        + " h_order.order_conf, "
      
        + " h_order.canc_conf, "
      
        + " h_order.rschd_conf, "
      
        + " h_order.done_conf, "
      
        + " h_order.qt_reqsted, "
      
        + " h_order.qt_done, "
      
        + " h_order.qt_convreq, "
      
        + " h_order.cleancnum, "
      
        + " h_order.survey_rem, "
      
        + " h_order.e_offer, "
      
        + " h_order.reminder, "
      
        + " h_order.exp_cred, "
      
        + " h_order.done_eref "
      

      + " From h_order ";
      public static List<h_order> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<h_order> rv = new List<h_order>();

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
      List<h_order> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<h_order> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(h_order));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(h_order item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<h_order>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(h_order));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<h_order> itemsList
      = new List<h_order>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is h_order)
      itemsList.Add(deserializedObject as h_order);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_ticket_num;
      
        protected String m_cust_id;
      
        protected bool m_alt_addr;
      
        protected String m_contact;
      
        protected DateTime m_date;
      
        protected String m_time;
      
        protected String m_page;
      
        protected String m_grid;
      
        protected String m_area_id;
      
        protected int m_serv_type;
      
        protected int m_tran_type;
      
        protected int m_comp_type;
      
        protected String m_company;
      
        protected String m_tech_id;
      
        protected float m_amount;
      
        protected int m_tran_stat;
      
        protected String m_closer_id;
      
        protected float m_recve_amt;
      
        protected DateTime m_pr_date;
      
        protected String m_zip;
      
        protected String m_bookby;
      
        protected String m_mapbook;
      
        protected int m_remcall;
      
        protected int m_companyid;
      
        protected bool m_order_conf;
      
        protected bool m_canc_conf;
      
        protected int m_rschd_conf;
      
        protected bool m_done_conf;
      
        protected bool m_qt_reqsted;
      
        protected bool m_qt_done;
      
        protected bool m_qt_convreq;
      
        protected String m_cleancnum;
      
        protected bool m_survey_rem;
      
        protected bool m_e_offer;
      
        protected bool m_reminder;
      
        protected bool m_exp_cred;
      
        protected bool m_done_eref;
      
      #endregion

      #region Constructors
      public h_order(
      String 
          ticket_num
      )
      {
      
        m_ticket_num = ticket_num;
      
      }

      


        public h_order(
        String 
          ticket_num,String 
          cust_id,bool 
          alt_addr,String 
          contact,DateTime 
          date,String 
          time,String 
          page,String 
          grid,String 
          area_id,int 
          serv_type,int 
          tran_type,int 
          comp_type,String 
          company,String 
          tech_id,float 
          amount,int 
          tran_stat,String 
          closer_id,float 
          recve_amt,DateTime 
          pr_date,String 
          zip,String 
          bookby,String 
          mapbook,int 
          remcall,int 
          companyid,bool 
          order_conf,bool 
          canc_conf,int 
          rschd_conf,bool 
          done_conf,bool 
          qt_reqsted,bool 
          qt_done,bool 
          qt_convreq,String 
          cleancnum,bool 
          survey_rem,bool 
          e_offer,bool 
          reminder,bool 
          exp_cred,bool 
          done_eref
        )
        {
        
          m_ticket_num = ticket_num;
        
          m_cust_id = cust_id;
        
          m_alt_addr = alt_addr;
        
          m_contact = contact;
        
          m_date = date;
        
          m_time = time;
        
          m_page = page;
        
          m_grid = grid;
        
          m_area_id = area_id;
        
          m_serv_type = serv_type;
        
          m_tran_type = tran_type;
        
          m_comp_type = comp_type;
        
          m_company = company;
        
          m_tech_id = tech_id;
        
          m_amount = amount;
        
          m_tran_stat = tran_stat;
        
          m_closer_id = closer_id;
        
          m_recve_amt = recve_amt;
        
          m_pr_date = pr_date;
        
          m_zip = zip;
        
          m_bookby = bookby;
        
          m_mapbook = mapbook;
        
          m_remcall = remcall;
        
          m_companyid = companyid;
        
          m_order_conf = order_conf;
        
          m_canc_conf = canc_conf;
        
          m_rschd_conf = rschd_conf;
        
          m_done_conf = done_conf;
        
          m_qt_reqsted = qt_reqsted;
        
          m_qt_done = qt_done;
        
          m_qt_convreq = qt_convreq;
        
          m_cleancnum = cleancnum;
        
          m_survey_rem = survey_rem;
        
          m_e_offer = e_offer;
        
          m_reminder = reminder;
        
          m_exp_cred = exp_cred;
        
          m_done_eref = done_eref;
        
        }


      
      #endregion

      
        [XmlElement]
        public String ticket_num
        {
        get { return m_ticket_num;}
        set { m_ticket_num = value; }
        }
      
        [XmlElement]
        public String cust_id
        {
        get { return m_cust_id;}
        set { m_cust_id = value; }
        }
      
        [XmlElement]
        public bool alt_addr
        {
        get { return m_alt_addr;}
        set { m_alt_addr = value; }
        }
      
        [XmlElement]
        public String contact
        {
        get { return m_contact;}
        set { m_contact = value; }
        }
      
        [XmlElement]
        public DateTime date
        {
        get { return m_date;}
        set { m_date = value; }
        }
      
        [XmlElement]
        public String time
        {
        get { return m_time;}
        set { m_time = value; }
        }
      
        [XmlElement]
        public String page
        {
        get { return m_page;}
        set { m_page = value; }
        }
      
        [XmlElement]
        public String grid
        {
        get { return m_grid;}
        set { m_grid = value; }
        }
      
        [XmlElement]
        public String area_id
        {
        get { return m_area_id;}
        set { m_area_id = value; }
        }
      
        [XmlElement]
        public int serv_type
        {
        get { return m_serv_type;}
        set { m_serv_type = value; }
        }
      
        [XmlElement]
        public int tran_type
        {
        get { return m_tran_type;}
        set { m_tran_type = value; }
        }
      
        [XmlElement]
        public int comp_type
        {
        get { return m_comp_type;}
        set { m_comp_type = value; }
        }
      
        [XmlElement]
        public String company
        {
        get { return m_company;}
        set { m_company = value; }
        }
      
        [XmlElement]
        public String tech_id
        {
        get { return m_tech_id;}
        set { m_tech_id = value; }
        }
      
        [XmlElement]
        public float amount
        {
        get { return m_amount;}
        set { m_amount = value; }
        }
      
        [XmlElement]
        public int tran_stat
        {
        get { return m_tran_stat;}
        set { m_tran_stat = value; }
        }
      
        [XmlElement]
        public String closer_id
        {
        get { return m_closer_id;}
        set { m_closer_id = value; }
        }
      
        [XmlElement]
        public float recve_amt
        {
        get { return m_recve_amt;}
        set { m_recve_amt = value; }
        }
      
        [XmlElement]
        public DateTime pr_date
        {
        get { return m_pr_date;}
        set { m_pr_date = value; }
        }
      
        [XmlElement]
        public String zip
        {
        get { return m_zip;}
        set { m_zip = value; }
        }
      
        [XmlElement]
        public String bookby
        {
        get { return m_bookby;}
        set { m_bookby = value; }
        }
      
        [XmlElement]
        public String mapbook
        {
        get { return m_mapbook;}
        set { m_mapbook = value; }
        }
      
        [XmlElement]
        public int remcall
        {
        get { return m_remcall;}
        set { m_remcall = value; }
        }
      
        [XmlElement]
        public int companyid
        {
        get { return m_companyid;}
        set { m_companyid = value; }
        }
      
        [XmlElement]
        public bool order_conf
        {
        get { return m_order_conf;}
        set { m_order_conf = value; }
        }
      
        [XmlElement]
        public bool canc_conf
        {
        get { return m_canc_conf;}
        set { m_canc_conf = value; }
        }
      
        [XmlElement]
        public int rschd_conf
        {
        get { return m_rschd_conf;}
        set { m_rschd_conf = value; }
        }
      
        [XmlElement]
        public bool done_conf
        {
        get { return m_done_conf;}
        set { m_done_conf = value; }
        }
      
        [XmlElement]
        public bool qt_reqsted
        {
        get { return m_qt_reqsted;}
        set { m_qt_reqsted = value; }
        }
      
        [XmlElement]
        public bool qt_done
        {
        get { return m_qt_done;}
        set { m_qt_done = value; }
        }
      
        [XmlElement]
        public bool qt_convreq
        {
        get { return m_qt_convreq;}
        set { m_qt_convreq = value; }
        }
      
        [XmlElement]
        public String cleancnum
        {
        get { return m_cleancnum;}
        set { m_cleancnum = value; }
        }
      
        [XmlElement]
        public bool survey_rem
        {
        get { return m_survey_rem;}
        set { m_survey_rem = value; }
        }
      
        [XmlElement]
        public bool e_offer
        {
        get { return m_e_offer;}
        set { m_e_offer = value; }
        }
      
        [XmlElement]
        public bool reminder
        {
        get { return m_reminder;}
        set { m_reminder = value; }
        }
      
        [XmlElement]
        public bool exp_cred
        {
        get { return m_exp_cred;}
        set { m_exp_cred = value; }
        }
      
        [XmlElement]
        public bool done_eref
        {
        get { return m_done_eref;}
        set { m_done_eref = value; }
        }
      
      }
      #endregion
      }

    