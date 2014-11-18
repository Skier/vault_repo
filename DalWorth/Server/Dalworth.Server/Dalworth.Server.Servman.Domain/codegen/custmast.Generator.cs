
    using System;
    using System.Data;
    using System.Collections.Generic;
    using System.Data.Odbc;
    using Dalworth.Server.Data;
    using Dalworth.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Dalworth.Server.Servman.Domain
      {


      public partial class custmast
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into custmast ( " +
      
        " cust_id, " +
      
        " customer, " +
      
        " block, " +
      
        " prefix, " +
      
        " street, " +
      
        " suffix, " +
      
        " unit, " +
      
        " address2, " +
      
        " city, " +
      
        " state, " +
      
        " zip, " +
      
        " home_phone, " +
      
        " bus_phone, " +
      
        " grid, " +
      
        " area_id, " +
      
        " cust_type, " +
      
        " cust_stat, " +
      
        " l_contact, " +
      
        " l_service, " +
      
        " l_kic_mail, " +
      
        " l_addr_chg, " +
      
        " addr_ver, " +
      
        " commission, " +
      
        " custkey_1, " +
      
        " emailaddr, " +
      
        " webvisit, " +
      
        " cleancnum, " +
      
        " no_email, " +
      
        " text_email, " +
      
        " zip4, " +
      
        " ncoa_date, " +
      
        " bad_mail, " +
      
        " notcuraddr, " +
      
        " rev_total, " +
      
        " job_count, " +
      
        " dt_lastjob, " +
      
        " rfm, " +
      
        " rank, " +
      
        " excl_cont, " +
      
        " last_eref " +
      
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
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ? " +
      
      ")";

      public static void Insert(custmast custmast)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@cust_id", custmast.cust_id);
      
        Database.PutParameter(dbCommand,"@customer", custmast.customer);
      
        Database.PutParameter(dbCommand,"@block", custmast.block);
      
        Database.PutParameter(dbCommand,"@prefix", custmast.prefix);
      
        Database.PutParameter(dbCommand,"@street", custmast.street);
      
        Database.PutParameter(dbCommand,"@suffix", custmast.suffix);
      
        Database.PutParameter(dbCommand,"@unit", custmast.unit);
      
        Database.PutParameter(dbCommand,"@address2", custmast.address2);
      
        Database.PutParameter(dbCommand,"@city", custmast.city);
      
        Database.PutParameter(dbCommand,"@state", custmast.state);
      
        Database.PutParameter(dbCommand,"@zip", custmast.zip);
      
        Database.PutParameter(dbCommand,"@home_phone", custmast.home_phone);
      
        Database.PutParameter(dbCommand,"@bus_phone", custmast.bus_phone);
      
        Database.PutParameter(dbCommand,"@grid", custmast.grid);
      
        Database.PutParameter(dbCommand,"@area_id", custmast.area_id);
      
        Database.PutParameter(dbCommand,"@cust_type", custmast.cust_type);
      
        Database.PutParameter(dbCommand,"@cust_stat", custmast.cust_stat);
      
        Database.PutParameter(dbCommand,"@l_contact", custmast.l_contact);
      
        Database.PutParameter(dbCommand,"@l_service", custmast.l_service);
      
        Database.PutParameter(dbCommand,"@l_kic_mail", custmast.l_kic_mail);
      
        Database.PutParameter(dbCommand,"@l_addr_chg", custmast.l_addr_chg);
      
        Database.PutParameter(dbCommand,"@addr_ver", custmast.addr_ver);
      
        Database.PutParameter(dbCommand,"@commission", custmast.commission);
      
        Database.PutParameter(dbCommand,"@custkey_1", custmast.custkey_1);
      
        Database.PutParameter(dbCommand,"@emailaddr", custmast.emailaddr);
      
        Database.PutParameter(dbCommand,"@webvisit", custmast.webvisit);
      
        Database.PutParameter(dbCommand,"@cleancnum", custmast.cleancnum);
      
        Database.PutParameter(dbCommand,"@no_email", custmast.no_email);
      
        Database.PutParameter(dbCommand,"@text_email", custmast.text_email);
      
        Database.PutParameter(dbCommand,"@zip4", custmast.zip4);
      
        Database.PutParameter(dbCommand,"@ncoa_date", custmast.ncoa_date);
      
        Database.PutParameter(dbCommand,"@bad_mail", custmast.bad_mail);
      
        Database.PutParameter(dbCommand,"@notcuraddr", custmast.notcuraddr);
      
        Database.PutParameter(dbCommand,"@rev_total", custmast.rev_total);
      
        Database.PutParameter(dbCommand,"@job_count", custmast.job_count);
      
        Database.PutParameter(dbCommand,"@dt_lastjob", custmast.dt_lastjob);
      
        Database.PutParameter(dbCommand,"@rfm", custmast.rfm);
      
        Database.PutParameter(dbCommand,"@rank", custmast.rank);
      
        Database.PutParameter(dbCommand,"@excl_cont", custmast.excl_cont);
      
        Database.PutParameter(dbCommand,"@last_eref", custmast.last_eref);

          try
          {
              dbCommand.ExecuteNonQuery();
          }
          catch (Exception ex)
          {
              if (ex.Message.Contains("Feature is not available"))
              {
                  try
                  {
                      FindByPrimaryKey(custmast.cust_id);
                  }
                  catch (DataNotFoundException)
                  {
                      throw ex;
                  }
              } else
                  throw;
          }
      

      }
      }

      public static void Insert(List<custmast>  custmastList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(custmast custmast in  custmastList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@cust_id", custmast.cust_id);
      
        Database.PutParameter(dbCommand,"@customer", custmast.customer);
      
        Database.PutParameter(dbCommand,"@block", custmast.block);
      
        Database.PutParameter(dbCommand,"@prefix", custmast.prefix);
      
        Database.PutParameter(dbCommand,"@street", custmast.street);
      
        Database.PutParameter(dbCommand,"@suffix", custmast.suffix);
      
        Database.PutParameter(dbCommand,"@unit", custmast.unit);
      
        Database.PutParameter(dbCommand,"@address2", custmast.address2);
      
        Database.PutParameter(dbCommand,"@city", custmast.city);
      
        Database.PutParameter(dbCommand,"@state", custmast.state);
      
        Database.PutParameter(dbCommand,"@zip", custmast.zip);
      
        Database.PutParameter(dbCommand,"@home_phone", custmast.home_phone);
      
        Database.PutParameter(dbCommand,"@bus_phone", custmast.bus_phone);
      
        Database.PutParameter(dbCommand,"@grid", custmast.grid);
      
        Database.PutParameter(dbCommand,"@area_id", custmast.area_id);
      
        Database.PutParameter(dbCommand,"@cust_type", custmast.cust_type);
      
        Database.PutParameter(dbCommand,"@cust_stat", custmast.cust_stat);
      
        Database.PutParameter(dbCommand,"@l_contact", custmast.l_contact);
      
        Database.PutParameter(dbCommand,"@l_service", custmast.l_service);
      
        Database.PutParameter(dbCommand,"@l_kic_mail", custmast.l_kic_mail);
      
        Database.PutParameter(dbCommand,"@l_addr_chg", custmast.l_addr_chg);
      
        Database.PutParameter(dbCommand,"@addr_ver", custmast.addr_ver);
      
        Database.PutParameter(dbCommand,"@commission", custmast.commission);
      
        Database.PutParameter(dbCommand,"@custkey_1", custmast.custkey_1);
      
        Database.PutParameter(dbCommand,"@emailaddr", custmast.emailaddr);
      
        Database.PutParameter(dbCommand,"@webvisit", custmast.webvisit);
      
        Database.PutParameter(dbCommand,"@cleancnum", custmast.cleancnum);
      
        Database.PutParameter(dbCommand,"@no_email", custmast.no_email);
      
        Database.PutParameter(dbCommand,"@text_email", custmast.text_email);
      
        Database.PutParameter(dbCommand,"@zip4", custmast.zip4);
      
        Database.PutParameter(dbCommand,"@ncoa_date", custmast.ncoa_date);
      
        Database.PutParameter(dbCommand,"@bad_mail", custmast.bad_mail);
      
        Database.PutParameter(dbCommand,"@notcuraddr", custmast.notcuraddr);
      
        Database.PutParameter(dbCommand,"@rev_total", custmast.rev_total);
      
        Database.PutParameter(dbCommand,"@job_count", custmast.job_count);
      
        Database.PutParameter(dbCommand,"@dt_lastjob", custmast.dt_lastjob);
      
        Database.PutParameter(dbCommand,"@rfm", custmast.rfm);
      
        Database.PutParameter(dbCommand,"@rank", custmast.rank);
      
        Database.PutParameter(dbCommand,"@excl_cont", custmast.excl_cont);
      
        Database.PutParameter(dbCommand,"@last_eref", custmast.last_eref);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@cust_id",custmast.cust_id);
      
        Database.UpdateParameter(dbCommand,"@customer",custmast.customer);
      
        Database.UpdateParameter(dbCommand,"@block",custmast.block);
      
        Database.UpdateParameter(dbCommand,"@prefix",custmast.prefix);
      
        Database.UpdateParameter(dbCommand,"@street",custmast.street);
      
        Database.UpdateParameter(dbCommand,"@suffix",custmast.suffix);
      
        Database.UpdateParameter(dbCommand,"@unit",custmast.unit);
      
        Database.UpdateParameter(dbCommand,"@address2",custmast.address2);
      
        Database.UpdateParameter(dbCommand,"@city",custmast.city);
      
        Database.UpdateParameter(dbCommand,"@state",custmast.state);
      
        Database.UpdateParameter(dbCommand,"@zip",custmast.zip);
      
        Database.UpdateParameter(dbCommand,"@home_phone",custmast.home_phone);
      
        Database.UpdateParameter(dbCommand,"@bus_phone",custmast.bus_phone);
      
        Database.UpdateParameter(dbCommand,"@grid",custmast.grid);
      
        Database.UpdateParameter(dbCommand,"@area_id",custmast.area_id);
      
        Database.UpdateParameter(dbCommand,"@cust_type",custmast.cust_type);
      
        Database.UpdateParameter(dbCommand,"@cust_stat",custmast.cust_stat);
      
        Database.UpdateParameter(dbCommand,"@l_contact",custmast.l_contact);
      
        Database.UpdateParameter(dbCommand,"@l_service",custmast.l_service);
      
        Database.UpdateParameter(dbCommand,"@l_kic_mail",custmast.l_kic_mail);
      
        Database.UpdateParameter(dbCommand,"@l_addr_chg",custmast.l_addr_chg);
      
        Database.UpdateParameter(dbCommand,"@addr_ver",custmast.addr_ver);
      
        Database.UpdateParameter(dbCommand,"@commission",custmast.commission);
      
        Database.UpdateParameter(dbCommand,"@custkey_1",custmast.custkey_1);
      
        Database.UpdateParameter(dbCommand,"@emailaddr",custmast.emailaddr);
      
        Database.UpdateParameter(dbCommand,"@webvisit",custmast.webvisit);
      
        Database.UpdateParameter(dbCommand,"@cleancnum",custmast.cleancnum);
      
        Database.UpdateParameter(dbCommand,"@no_email",custmast.no_email);
      
        Database.UpdateParameter(dbCommand,"@text_email",custmast.text_email);
      
        Database.UpdateParameter(dbCommand,"@zip4",custmast.zip4);
      
        Database.UpdateParameter(dbCommand,"@ncoa_date",custmast.ncoa_date);
      
        Database.UpdateParameter(dbCommand,"@bad_mail",custmast.bad_mail);
      
        Database.UpdateParameter(dbCommand,"@notcuraddr",custmast.notcuraddr);
      
        Database.UpdateParameter(dbCommand,"@rev_total",custmast.rev_total);
      
        Database.UpdateParameter(dbCommand,"@job_count",custmast.job_count);
      
        Database.UpdateParameter(dbCommand,"@dt_lastjob",custmast.dt_lastjob);
      
        Database.UpdateParameter(dbCommand,"@rfm",custmast.rfm);
      
        Database.UpdateParameter(dbCommand,"@rank",custmast.rank);
      
        Database.UpdateParameter(dbCommand,"@excl_cont",custmast.excl_cont);
      
        Database.UpdateParameter(dbCommand,"@last_eref",custmast.last_eref);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update custmast Set "
      
        + " custmast.customer = ? , "
      
        + " custmast.block = ? , "
      
        + " custmast.prefix = ? , "
      
        + " custmast.street = ? , "
      
        + " custmast.suffix = ? , "
      
        + " custmast.unit = ? , "
      
        + " custmast.address2 = ? , "
      
        + " custmast.city = ? , "
      
        + " custmast.state = ? , "
      
        + " custmast.zip = ? , "
      
        + " custmast.home_phone = ? , "
      
        + " custmast.bus_phone = ? , "
      
        + " custmast.grid = ? , "
      
        + " custmast.area_id = ? , "
      
        + " custmast.cust_type = ? , "
      
        + " custmast.cust_stat = ? , "
      
        + " custmast.l_contact = ? , "
      
        + " custmast.l_service = ? , "
      
        + " custmast.l_kic_mail = ? , "
      
        + " custmast.l_addr_chg = ? , "
      
        + " custmast.addr_ver = ? , "
      
        + " custmast.commission = ? , "
      
        + " custmast.custkey_1 = ? , "
      
        + " custmast.emailaddr = ? , "
      
        + " custmast.webvisit = ? , "
      
        + " custmast.cleancnum = ? , "
      
        + " custmast.no_email = ? , "
      
        + " custmast.text_email = ? , "
      
        + " custmast.zip4 = ? , "
      
        + " custmast.ncoa_date = ? , "
      
        + " custmast.bad_mail = ? , "
      
        + " custmast.notcuraddr = ? , "
      
        + " custmast.rev_total = ? , "
      
        + " custmast.job_count = ? , "
      
        + " custmast.dt_lastjob = ? , "
      
        + " custmast.rfm = ? , "
      
        + " custmast.rank = ? , "
      
        + " custmast.excl_cont = ? , "
      
        + " custmast.last_eref = ?  "
      
        + " Where "

          + " custmast.cust_id = ?  "
        
      ;

      public static void Update(custmast custmast)
      {
          using (IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@customer", custmast.customer);
      
        Database.PutParameter(dbCommand,"@block", custmast.block);
      
        Database.PutParameter(dbCommand,"@prefix", custmast.prefix);
      
        Database.PutParameter(dbCommand,"@street", custmast.street);
      
        Database.PutParameter(dbCommand,"@suffix", custmast.suffix);
      
        Database.PutParameter(dbCommand,"@unit", custmast.unit);
      
        Database.PutParameter(dbCommand,"@address2", custmast.address2);
      
        Database.PutParameter(dbCommand,"@city", custmast.city);
      
        Database.PutParameter(dbCommand,"@state", custmast.state);
      
        Database.PutParameter(dbCommand,"@zip", custmast.zip);
      
        Database.PutParameter(dbCommand,"@home_phone", custmast.home_phone);
      
        Database.PutParameter(dbCommand,"@bus_phone", custmast.bus_phone);
      
        Database.PutParameter(dbCommand,"@grid", custmast.grid);
      
        Database.PutParameter(dbCommand,"@area_id", custmast.area_id);
      
        Database.PutParameter(dbCommand,"@cust_type", custmast.cust_type);
      
        Database.PutParameter(dbCommand,"@cust_stat", custmast.cust_stat);
      
        Database.PutParameter(dbCommand,"@l_contact", custmast.l_contact);
      
        Database.PutParameter(dbCommand,"@l_service", custmast.l_service);
      
        Database.PutParameter(dbCommand,"@l_kic_mail", custmast.l_kic_mail);
      
        Database.PutParameter(dbCommand,"@l_addr_chg", custmast.l_addr_chg);
      
        Database.PutParameter(dbCommand,"@addr_ver", custmast.addr_ver);
      
        Database.PutParameter(dbCommand,"@commission", custmast.commission);
      
        Database.PutParameter(dbCommand,"@custkey_1", custmast.custkey_1);
      
        Database.PutParameter(dbCommand,"@emailaddr", custmast.emailaddr);
      
        Database.PutParameter(dbCommand,"@webvisit", custmast.webvisit);
      
        Database.PutParameter(dbCommand,"@cleancnum", custmast.cleancnum);
      
        Database.PutParameter(dbCommand,"@no_email", custmast.no_email);
      
        Database.PutParameter(dbCommand,"@text_email", custmast.text_email);
      
        Database.PutParameter(dbCommand,"@zip4", custmast.zip4);
      
        Database.PutParameter(dbCommand,"@ncoa_date", custmast.ncoa_date);
      
        Database.PutParameter(dbCommand,"@bad_mail", custmast.bad_mail);
      
        Database.PutParameter(dbCommand,"@notcuraddr", custmast.notcuraddr);
      
        Database.PutParameter(dbCommand,"@rev_total", custmast.rev_total);
      
        Database.PutParameter(dbCommand,"@job_count", custmast.job_count);
      
        Database.PutParameter(dbCommand,"@dt_lastjob", custmast.dt_lastjob);
      
        Database.PutParameter(dbCommand,"@rfm", custmast.rfm);
      
        Database.PutParameter(dbCommand,"@rank", custmast.rank);
      
        Database.PutParameter(dbCommand,"@excl_cont", custmast.excl_cont);
      
        Database.PutParameter(dbCommand,"@last_eref", custmast.last_eref);
      
        Database.PutParameter(dbCommand,"@cust_id", custmast.cust_id);


          try
          {
              dbCommand.ExecuteNonQuery();
          }
          catch (Exception ex)
          {
              if (!ex.Message.Contains("Feature is not available"))
                  throw;
          }
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " custmast.cust_id, "
      
        + " custmast.customer, "
      
        + " custmast.block, "
      
        + " custmast.prefix, "
      
        + " custmast.street, "
      
        + " custmast.suffix, "
      
        + " custmast.unit, "
      
        + " custmast.address2, "
      
        + " custmast.city, "
      
        + " custmast.state, "
      
        + " custmast.zip, "
      
        + " custmast.home_phone, "
      
        + " custmast.bus_phone, "
      
        + " custmast.grid, "
      
        + " custmast.area_id, "
      
        + " custmast.cust_type, "
      
        + " custmast.cust_stat, "
      
        + " custmast.l_contact, "
      
        + " custmast.l_service, "
      
        + " custmast.l_kic_mail, "
      
        + " custmast.l_addr_chg, "
      
        + " custmast.addr_ver, "
      
        + " custmast.commission, "
      
        + " custmast.custkey_1, "
      
        + " custmast.emailaddr, "
      
        + " custmast.webvisit, "
      
        + " custmast.cleancnum, "
      
        + " custmast.no_email, "
      
        + " custmast.text_email, "
      
        + " custmast.zip4, "
      
        + " custmast.ncoa_date, "
      
        + " custmast.bad_mail, "
      
        + " custmast.notcuraddr, "
      
        + " custmast.rev_total, "
      
        + " custmast.job_count, "
      
        + " custmast.dt_lastjob, "
      
        + " custmast.rfm, "
      
        + " custmast.rank, "
      
        + " custmast.excl_cont, "
      
        + " custmast.last_eref "
      

      + " From custmast "

      
        + " Where "
        
          + " custmast.cust_id = ?  "
        
      ;

      public static custmast FindByPrimaryKey(
      String cust_id
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@cust_id", cust_id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("custmast not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(custmast custmast)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@cust_id",custmast.cust_id);
      

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
      String sql = "select 1 from custmast";

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

      public static custmast Load(IDataReader dataReader)
      {
      custmast custmast = new custmast();

      custmast.cust_id = dataReader.GetString(0);
          custmast.customer = dataReader.GetString(1);
          custmast.block = dataReader.GetString(2);
          custmast.prefix = dataReader.GetString(3);
          custmast.street = dataReader.GetString(4);
          custmast.suffix = dataReader.GetString(5);
          custmast.unit = dataReader.GetString(6);
          custmast.address2 = dataReader.GetString(7);
          custmast.city = dataReader.GetString(8);
          custmast.state = dataReader.GetString(9);
          custmast.zip = dataReader.GetString(10);
          custmast.home_phone = dataReader.GetString(11);
          custmast.bus_phone = dataReader.GetString(12);
          custmast.grid = dataReader.GetString(13);
          custmast.area_id = dataReader.GetString(14);
          custmast.cust_type = dataReader.GetString(15);
          custmast.cust_stat = dataReader.GetString(16);
          custmast.l_contact = dataReader.GetDateTime(17);
          custmast.l_service = dataReader.GetDateTime(18);
          custmast.l_kic_mail = dataReader.GetDateTime(19);
          custmast.l_addr_chg = dataReader.GetDateTime(20);
          custmast.addr_ver = dataReader.GetBoolean(21);
          custmast.commission = dataReader.GetFloat(22);
          custmast.custkey_1 = dataReader.GetString(23);
          custmast.emailaddr = dataReader.GetString(24);
          custmast.webvisit = dataReader.GetBoolean(25);
          custmast.cleancnum = dataReader.GetString(26);
          custmast.no_email = dataReader.GetBoolean(27);
          custmast.text_email = dataReader.GetBoolean(28);
          custmast.zip4 = dataReader.GetString(29);
          custmast.ncoa_date = dataReader.GetDateTime(30);
          custmast.bad_mail = dataReader.GetBoolean(31);
          custmast.notcuraddr = dataReader.GetBoolean(32);
          custmast.rev_total = dataReader.GetFloat(33);
          custmast.job_count = dataReader.GetInt32(34);
          custmast.dt_lastjob = dataReader.GetDateTime(35);
          custmast.rfm = dataReader.GetString(36);
          custmast.rank = dataReader.GetInt32(37);
          custmast.excl_cont = dataReader.GetString(38);
          custmast.last_eref = dataReader.GetDateTime(39);
          

      return custmast;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [custmast] "

      
        + " Where "
        
          + " cust_id = ?  "
        
      ;
      public static void Delete(custmast custmast)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      
        Database.PutParameter(dbCommand,"@cust_id", custmast.cust_id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [custmast] ";

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

      
        + " custmast.cust_id, "
      
        + " custmast.customer, "
      
        + " custmast.block, "
      
        + " custmast.prefix, "
      
        + " custmast.street, "
      
        + " custmast.suffix, "
      
        + " custmast.unit, "
      
        + " custmast.address2, "
      
        + " custmast.city, "
      
        + " custmast.state, "
      
        + " custmast.zip, "
      
        + " custmast.home_phone, "
      
        + " custmast.bus_phone, "
      
        + " custmast.grid, "
      
        + " custmast.area_id, "
      
        + " custmast.cust_type, "
      
        + " custmast.cust_stat, "
      
        + " custmast.l_contact, "
      
        + " custmast.l_service, "
      
        + " custmast.l_kic_mail, "
      
        + " custmast.l_addr_chg, "
      
        + " custmast.addr_ver, "
      
        + " custmast.commission, "
      
        + " custmast.custkey_1, "
      
        + " custmast.emailaddr, "
      
        + " custmast.webvisit, "
      
        + " custmast.cleancnum, "
      
        + " custmast.no_email, "
      
        + " custmast.text_email, "
      
        + " custmast.zip4, "
      
        + " custmast.ncoa_date, "
      
        + " custmast.bad_mail, "
      
        + " custmast.notcuraddr, "
      
        + " custmast.rev_total, "
      
        + " custmast.job_count, "
      
        + " custmast.dt_lastjob, "
      
        + " custmast.rfm, "
      
        + " custmast.rank, "
      
        + " custmast.excl_cont, "
      
        + " custmast.last_eref "
      

      + " From custmast ";
      public static List<custmast> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<custmast> rv = new List<custmast>();

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
      List<custmast> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<custmast> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(custmast));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(custmast item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<custmast>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(custmast));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<custmast> itemsList
      = new List<custmast>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is custmast)
      itemsList.Add(deserializedObject as custmast);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_cust_id;
      
        protected String m_customer;
      
        protected String m_block;
      
        protected String m_prefix;
      
        protected String m_street;
      
        protected String m_suffix;
      
        protected String m_unit;
      
        protected String m_address2;
      
        protected String m_city;
      
        protected String m_state;
      
        protected String m_zip;
      
        protected String m_home_phone;
      
        protected String m_bus_phone;
      
        protected String m_grid;
      
        protected String m_area_id;
      
        protected String m_cust_type;
      
        protected String m_cust_stat;
      
        protected DateTime m_l_contact;
      
        protected DateTime m_l_service;
      
        protected DateTime m_l_kic_mail;
      
        protected DateTime m_l_addr_chg;
      
        protected bool m_addr_ver;
      
        protected float m_commission;
      
        protected String m_custkey_1;
      
        protected String m_emailaddr;
      
        protected bool m_webvisit;
      
        protected String m_cleancnum;
      
        protected bool m_no_email;
      
        protected bool m_text_email;
      
        protected String m_zip4;
      
        protected DateTime m_ncoa_date;
      
        protected bool m_bad_mail;
      
        protected bool m_notcuraddr;
      
        protected float m_rev_total;
      
        protected int m_job_count;
      
        protected DateTime m_dt_lastjob;
      
        protected String m_rfm;
      
        protected int m_rank;
      
        protected String m_excl_cont;
      
        protected DateTime m_last_eref;
      
      #endregion

      #region Constructors
      public custmast(
      String 
          cust_id
      )
      {
      
        m_cust_id = cust_id;
      
      }

      


        public custmast(
        String 
          cust_id,String 
          customer,String 
          block,String 
          prefix,String 
          street,String 
          suffix,String 
          unit,String 
          address2,String 
          city,String 
          state,String 
          zip,String 
          home_phone,String 
          bus_phone,String 
          grid,String 
          area_id,String 
          cust_type,String 
          cust_stat,DateTime 
          l_contact,DateTime 
          l_service,DateTime 
          l_kic_mail,DateTime 
          l_addr_chg,bool 
          addr_ver,float 
          commission,String 
          custkey_1,String 
          emailaddr,bool 
          webvisit,String 
          cleancnum,bool 
          no_email,bool 
          text_email,String 
          zip4,DateTime 
          ncoa_date,bool 
          bad_mail,bool 
          notcuraddr,float 
          rev_total,int 
          job_count,DateTime 
          dt_lastjob,String 
          rfm,int 
          rank,String 
          excl_cont,DateTime 
          last_eref
        )
        {
        
          m_cust_id = cust_id;
        
          m_customer = customer;
        
          m_block = block;
        
          m_prefix = prefix;
        
          m_street = street;
        
          m_suffix = suffix;
        
          m_unit = unit;
        
          m_address2 = address2;
        
          m_city = city;
        
          m_state = state;
        
          m_zip = zip;
        
          m_home_phone = home_phone;
        
          m_bus_phone = bus_phone;
        
          m_grid = grid;
        
          m_area_id = area_id;
        
          m_cust_type = cust_type;
        
          m_cust_stat = cust_stat;
        
          m_l_contact = l_contact;
        
          m_l_service = l_service;
        
          m_l_kic_mail = l_kic_mail;
        
          m_l_addr_chg = l_addr_chg;
        
          m_addr_ver = addr_ver;
        
          m_commission = commission;
        
          m_custkey_1 = custkey_1;
        
          m_emailaddr = emailaddr;
        
          m_webvisit = webvisit;
        
          m_cleancnum = cleancnum;
        
          m_no_email = no_email;
        
          m_text_email = text_email;
        
          m_zip4 = zip4;
        
          m_ncoa_date = ncoa_date;
        
          m_bad_mail = bad_mail;
        
          m_notcuraddr = notcuraddr;
        
          m_rev_total = rev_total;
        
          m_job_count = job_count;
        
          m_dt_lastjob = dt_lastjob;
        
          m_rfm = rfm;
        
          m_rank = rank;
        
          m_excl_cont = excl_cont;
        
          m_last_eref = last_eref;
        
        }


      
      #endregion

      
        [XmlElement]
        public String cust_id
        {
        get { return m_cust_id;}
        set { m_cust_id = value; }
        }
      
        [XmlElement]
        public String customer
        {
        get { return m_customer;}
        set { m_customer = value; }
        }
      
        [XmlElement]
        public String block
        {
        get { return m_block;}
        set { m_block = value; }
        }
      
        [XmlElement]
        public String prefix
        {
        get { return m_prefix;}
        set { m_prefix = value; }
        }
      
        [XmlElement]
        public String street
        {
        get { return m_street;}
        set { m_street = value; }
        }
      
        [XmlElement]
        public String suffix
        {
        get { return m_suffix;}
        set { m_suffix = value; }
        }
      
        [XmlElement]
        public String unit
        {
        get { return m_unit;}
        set { m_unit = value; }
        }
      
        [XmlElement]
        public String address2
        {
        get { return m_address2;}
        set { m_address2 = value; }
        }
      
        [XmlElement]
        public String city
        {
        get { return m_city;}
        set { m_city = value; }
        }
      
        [XmlElement]
        public String state
        {
        get { return m_state;}
        set { m_state = value; }
        }
      
        [XmlElement]
        public String zip
        {
        get { return m_zip;}
        set { m_zip = value; }
        }
      
        [XmlElement]
        public String home_phone
        {
        get { return m_home_phone;}
        set { m_home_phone = value; }
        }
      
        [XmlElement]
        public String bus_phone
        {
        get { return m_bus_phone;}
        set { m_bus_phone = value; }
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
        public String cust_type
        {
        get { return m_cust_type;}
        set { m_cust_type = value; }
        }
      
        [XmlElement]
        public String cust_stat
        {
        get { return m_cust_stat;}
        set { m_cust_stat = value; }
        }
      
        [XmlElement]
        public DateTime l_contact
        {
        get { return m_l_contact;}
        set { m_l_contact = value; }
        }
      
        [XmlElement]
        public DateTime l_service
        {
        get { return m_l_service;}
        set { m_l_service = value; }
        }
      
        [XmlElement]
        public DateTime l_kic_mail
        {
        get { return m_l_kic_mail;}
        set { m_l_kic_mail = value; }
        }
      
        [XmlElement]
        public DateTime l_addr_chg
        {
        get { return m_l_addr_chg;}
        set { m_l_addr_chg = value; }
        }
      
        [XmlElement]
        public bool addr_ver
        {
        get { return m_addr_ver;}
        set { m_addr_ver = value; }
        }
      
        [XmlElement]
        public float commission
        {
        get { return m_commission;}
        set { m_commission = value; }
        }
      
        [XmlElement]
        public String custkey_1
        {
        get { return m_custkey_1;}
        set { m_custkey_1 = value; }
        }
      
        [XmlElement]
        public String emailaddr
        {
        get { return m_emailaddr;}
        set { m_emailaddr = value; }
        }
      
        [XmlElement]
        public bool webvisit
        {
        get { return m_webvisit;}
        set { m_webvisit = value; }
        }
      
        [XmlElement]
        public String cleancnum
        {
        get { return m_cleancnum;}
        set { m_cleancnum = value; }
        }
      
        [XmlElement]
        public bool no_email
        {
        get { return m_no_email;}
        set { m_no_email = value; }
        }
      
        [XmlElement]
        public bool text_email
        {
        get { return m_text_email;}
        set { m_text_email = value; }
        }
      
        [XmlElement]
        public String zip4
        {
        get { return m_zip4;}
        set { m_zip4 = value; }
        }
      
        [XmlElement]
        public DateTime ncoa_date
        {
        get { return m_ncoa_date;}
        set { m_ncoa_date = value; }
        }
      
        [XmlElement]
        public bool bad_mail
        {
        get { return m_bad_mail;}
        set { m_bad_mail = value; }
        }
      
        [XmlElement]
        public bool notcuraddr
        {
        get { return m_notcuraddr;}
        set { m_notcuraddr = value; }
        }
      
        [XmlElement]
        public float rev_total
        {
        get { return m_rev_total;}
        set { m_rev_total = value; }
        }
      
        [XmlElement]
        public int job_count
        {
        get { return m_job_count;}
        set { m_job_count = value; }
        }
      
        [XmlElement]
        public DateTime dt_lastjob
        {
        get { return m_dt_lastjob;}
        set { m_dt_lastjob = value; }
        }
      
        [XmlElement]
        public String rfm
        {
        get { return m_rfm;}
        set { m_rfm = value; }
        }
      
        [XmlElement]
        public int rank
        {
        get { return m_rank;}
        set { m_rank = value; }
        }
      
        [XmlElement]
        public String excl_cont
        {
        get { return m_excl_cont;}
        set { m_excl_cont = value; }
        }
      
        [XmlElement]
        public DateTime last_eref
        {
        get { return m_last_eref;}
        set { m_last_eref = value; }
        }
      
      }
      #endregion
      }

    