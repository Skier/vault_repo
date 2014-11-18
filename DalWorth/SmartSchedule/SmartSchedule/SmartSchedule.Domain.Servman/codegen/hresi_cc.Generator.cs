
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.Odbc;
using SmartSchedule.Data;
using SmartSchedule.SDK;
using System.Xml;
using System.Xml.Serialization;
using System.Text;

      namespace SmartSchedule.Domain.Servman
      {


      public partial class hresi_cc
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into hresi_cc ( " +
      
        " ticket_num, " +
      
        " cust_id, " +
      
        " rejected, " +
      
        " trans_num, " +
      
        " csr_id, " +
      
        " ad_source, " +
      
        " alt_addr, " +
      
        " tech_refer, " +
      
        " d_1st_call, " +
      
        " t_1st_call, " +
      
        " d_schedule, " +
      
        " t_schedule, " +
      
        " d_dispatch, " +
      
        " t_dispatch, " +
      
        " d_complete, " +
      
        " t_complete, " +
      
        " tran_type, " +
      
        " tran_stat, " +
      
        " comp_type, " +
      
        " canc_type, " +
      
        " mop, " +
      
        " amount, " +
      
        " reschd_num, " +
      
        " company, " +
      
        " tech_id, " +
      
        " b_person, " +
      
        " l_person, " +
      
        " c_person, " +
      
        " note, " +
      
        " special1, " +
      
        " grade, " +
      
        " tax_perc, " +
      
        " spec_id, " +
      
        " sd_name, " +
      
        " sd_amt, " +
      
        " min_prc, " +
      
        " mp_used, " +
      
        " btid, " +
      
        " companyid, " +
      
        " comp_refer, " +
      
        " repeat, " +
      
        " cc_num, " +
      
        " cc_expdate, " +
      
        " dl_num, " +
      
        " dl_dob, " +
      
        " auth_code, " +
      
        " t_arrival " +
      
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
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ? " +
      
      ")";

      public static void Insert(hresi_cc hresi_cc)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@ticket_num", hresi_cc.ticket_num);
      
        Database.PutParameter(dbCommand,"@cust_id", hresi_cc.cust_id);
      
        Database.PutParameter(dbCommand,"@rejected", hresi_cc.rejected);
      
        Database.PutParameter(dbCommand,"@trans_num", hresi_cc.trans_num);
      
        Database.PutParameter(dbCommand,"@csr_id", hresi_cc.csr_id);
      
        Database.PutParameter(dbCommand,"@ad_source", hresi_cc.ad_source);
      
        Database.PutParameter(dbCommand,"@alt_addr", hresi_cc.alt_addr);
      
        Database.PutParameter(dbCommand,"@tech_refer", hresi_cc.tech_refer);
      
        Database.PutParameter(dbCommand,"@d_1st_call", hresi_cc.d_1st_call);
      
        Database.PutParameter(dbCommand,"@t_1st_call", hresi_cc.t_1st_call);
      
        Database.PutParameter(dbCommand,"@d_schedule", hresi_cc.d_schedule);
      
        Database.PutParameter(dbCommand,"@t_schedule", hresi_cc.t_schedule);
      
        Database.PutParameter(dbCommand,"@d_dispatch", hresi_cc.d_dispatch);
      
        Database.PutParameter(dbCommand,"@t_dispatch", hresi_cc.t_dispatch);
      
        Database.PutParameter(dbCommand,"@d_complete", hresi_cc.d_complete);
      
        Database.PutParameter(dbCommand,"@t_complete", hresi_cc.t_complete);
      
        Database.PutParameter(dbCommand,"@tran_type", hresi_cc.tran_type);
      
        Database.PutParameter(dbCommand,"@tran_stat", hresi_cc.tran_stat);
      
        Database.PutParameter(dbCommand,"@comp_type", hresi_cc.comp_type);
      
        Database.PutParameter(dbCommand,"@canc_type", hresi_cc.canc_type);
      
        Database.PutParameter(dbCommand,"@mop", hresi_cc.mop);
      
        Database.PutParameter(dbCommand,"@amount", hresi_cc.amount);
      
        Database.PutParameter(dbCommand,"@reschd_num", hresi_cc.reschd_num);
      
        Database.PutParameter(dbCommand,"@company", hresi_cc.company);
      
        Database.PutParameter(dbCommand,"@tech_id", hresi_cc.tech_id);
      
        Database.PutParameter(dbCommand,"@b_person", hresi_cc.b_person);
      
        Database.PutParameter(dbCommand,"@l_person", hresi_cc.l_person);
      
        Database.PutParameter(dbCommand,"@c_person", hresi_cc.c_person);
      
        Database.PutParameter(dbCommand,"@note", hresi_cc.note);
      
        Database.PutParameter(dbCommand,"@special1", hresi_cc.special1);
      
        Database.PutParameter(dbCommand,"@grade", hresi_cc.grade);
      
        Database.PutParameter(dbCommand,"@tax_perc", hresi_cc.tax_perc);
      
        Database.PutParameter(dbCommand,"@spec_id", hresi_cc.spec_id);
      
        Database.PutParameter(dbCommand,"@sd_name", hresi_cc.sd_name);
      
        Database.PutParameter(dbCommand,"@sd_amt", hresi_cc.sd_amt);
      
        Database.PutParameter(dbCommand,"@min_prc", hresi_cc.min_prc);
      
        Database.PutParameter(dbCommand,"@mp_used", hresi_cc.mp_used);
      
        Database.PutParameter(dbCommand,"@btid", hresi_cc.btid);
      
        Database.PutParameter(dbCommand,"@companyid", hresi_cc.companyid);
      
        Database.PutParameter(dbCommand,"@comp_refer", hresi_cc.comp_refer);
      
        Database.PutParameter(dbCommand,"@repeat", hresi_cc.repeat);
      
        Database.PutParameter(dbCommand,"@cc_num", hresi_cc.cc_num);
      
        Database.PutParameter(dbCommand,"@cc_expdate", hresi_cc.cc_expdate);
      
        Database.PutParameter(dbCommand,"@dl_num", hresi_cc.dl_num);
      
        Database.PutParameter(dbCommand,"@dl_dob", hresi_cc.dl_dob);
      
        Database.PutParameter(dbCommand,"@auth_code", hresi_cc.auth_code);
      
        Database.PutParameter(dbCommand,"@t_arrival", hresi_cc.t_arrival);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<hresi_cc>  hresi_ccList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(hresi_cc hresi_cc in  hresi_ccList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ticket_num", hresi_cc.ticket_num);
      
        Database.PutParameter(dbCommand,"@cust_id", hresi_cc.cust_id);
      
        Database.PutParameter(dbCommand,"@rejected", hresi_cc.rejected);
      
        Database.PutParameter(dbCommand,"@trans_num", hresi_cc.trans_num);
      
        Database.PutParameter(dbCommand,"@csr_id", hresi_cc.csr_id);
      
        Database.PutParameter(dbCommand,"@ad_source", hresi_cc.ad_source);
      
        Database.PutParameter(dbCommand,"@alt_addr", hresi_cc.alt_addr);
      
        Database.PutParameter(dbCommand,"@tech_refer", hresi_cc.tech_refer);
      
        Database.PutParameter(dbCommand,"@d_1st_call", hresi_cc.d_1st_call);
      
        Database.PutParameter(dbCommand,"@t_1st_call", hresi_cc.t_1st_call);
      
        Database.PutParameter(dbCommand,"@d_schedule", hresi_cc.d_schedule);
      
        Database.PutParameter(dbCommand,"@t_schedule", hresi_cc.t_schedule);
      
        Database.PutParameter(dbCommand,"@d_dispatch", hresi_cc.d_dispatch);
      
        Database.PutParameter(dbCommand,"@t_dispatch", hresi_cc.t_dispatch);
      
        Database.PutParameter(dbCommand,"@d_complete", hresi_cc.d_complete);
      
        Database.PutParameter(dbCommand,"@t_complete", hresi_cc.t_complete);
      
        Database.PutParameter(dbCommand,"@tran_type", hresi_cc.tran_type);
      
        Database.PutParameter(dbCommand,"@tran_stat", hresi_cc.tran_stat);
      
        Database.PutParameter(dbCommand,"@comp_type", hresi_cc.comp_type);
      
        Database.PutParameter(dbCommand,"@canc_type", hresi_cc.canc_type);
      
        Database.PutParameter(dbCommand,"@mop", hresi_cc.mop);
      
        Database.PutParameter(dbCommand,"@amount", hresi_cc.amount);
      
        Database.PutParameter(dbCommand,"@reschd_num", hresi_cc.reschd_num);
      
        Database.PutParameter(dbCommand,"@company", hresi_cc.company);
      
        Database.PutParameter(dbCommand,"@tech_id", hresi_cc.tech_id);
      
        Database.PutParameter(dbCommand,"@b_person", hresi_cc.b_person);
      
        Database.PutParameter(dbCommand,"@l_person", hresi_cc.l_person);
      
        Database.PutParameter(dbCommand,"@c_person", hresi_cc.c_person);
      
        Database.PutParameter(dbCommand,"@note", hresi_cc.note);
      
        Database.PutParameter(dbCommand,"@special1", hresi_cc.special1);
      
        Database.PutParameter(dbCommand,"@grade", hresi_cc.grade);
      
        Database.PutParameter(dbCommand,"@tax_perc", hresi_cc.tax_perc);
      
        Database.PutParameter(dbCommand,"@spec_id", hresi_cc.spec_id);
      
        Database.PutParameter(dbCommand,"@sd_name", hresi_cc.sd_name);
      
        Database.PutParameter(dbCommand,"@sd_amt", hresi_cc.sd_amt);
      
        Database.PutParameter(dbCommand,"@min_prc", hresi_cc.min_prc);
      
        Database.PutParameter(dbCommand,"@mp_used", hresi_cc.mp_used);
      
        Database.PutParameter(dbCommand,"@btid", hresi_cc.btid);
      
        Database.PutParameter(dbCommand,"@companyid", hresi_cc.companyid);
      
        Database.PutParameter(dbCommand,"@comp_refer", hresi_cc.comp_refer);
      
        Database.PutParameter(dbCommand,"@repeat", hresi_cc.repeat);
      
        Database.PutParameter(dbCommand,"@cc_num", hresi_cc.cc_num);
      
        Database.PutParameter(dbCommand,"@cc_expdate", hresi_cc.cc_expdate);
      
        Database.PutParameter(dbCommand,"@dl_num", hresi_cc.dl_num);
      
        Database.PutParameter(dbCommand,"@dl_dob", hresi_cc.dl_dob);
      
        Database.PutParameter(dbCommand,"@auth_code", hresi_cc.auth_code);
      
        Database.PutParameter(dbCommand,"@t_arrival", hresi_cc.t_arrival);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ticket_num",hresi_cc.ticket_num);
      
        Database.UpdateParameter(dbCommand,"@cust_id",hresi_cc.cust_id);
      
        Database.UpdateParameter(dbCommand,"@rejected",hresi_cc.rejected);
      
        Database.UpdateParameter(dbCommand,"@trans_num",hresi_cc.trans_num);
      
        Database.UpdateParameter(dbCommand,"@csr_id",hresi_cc.csr_id);
      
        Database.UpdateParameter(dbCommand,"@ad_source",hresi_cc.ad_source);
      
        Database.UpdateParameter(dbCommand,"@alt_addr",hresi_cc.alt_addr);
      
        Database.UpdateParameter(dbCommand,"@tech_refer",hresi_cc.tech_refer);
      
        Database.UpdateParameter(dbCommand,"@d_1st_call",hresi_cc.d_1st_call);
      
        Database.UpdateParameter(dbCommand,"@t_1st_call",hresi_cc.t_1st_call);
      
        Database.UpdateParameter(dbCommand,"@d_schedule",hresi_cc.d_schedule);
      
        Database.UpdateParameter(dbCommand,"@t_schedule",hresi_cc.t_schedule);
      
        Database.UpdateParameter(dbCommand,"@d_dispatch",hresi_cc.d_dispatch);
      
        Database.UpdateParameter(dbCommand,"@t_dispatch",hresi_cc.t_dispatch);
      
        Database.UpdateParameter(dbCommand,"@d_complete",hresi_cc.d_complete);
      
        Database.UpdateParameter(dbCommand,"@t_complete",hresi_cc.t_complete);
      
        Database.UpdateParameter(dbCommand,"@tran_type",hresi_cc.tran_type);
      
        Database.UpdateParameter(dbCommand,"@tran_stat",hresi_cc.tran_stat);
      
        Database.UpdateParameter(dbCommand,"@comp_type",hresi_cc.comp_type);
      
        Database.UpdateParameter(dbCommand,"@canc_type",hresi_cc.canc_type);
      
        Database.UpdateParameter(dbCommand,"@mop",hresi_cc.mop);
      
        Database.UpdateParameter(dbCommand,"@amount",hresi_cc.amount);
      
        Database.UpdateParameter(dbCommand,"@reschd_num",hresi_cc.reschd_num);
      
        Database.UpdateParameter(dbCommand,"@company",hresi_cc.company);
      
        Database.UpdateParameter(dbCommand,"@tech_id",hresi_cc.tech_id);
      
        Database.UpdateParameter(dbCommand,"@b_person",hresi_cc.b_person);
      
        Database.UpdateParameter(dbCommand,"@l_person",hresi_cc.l_person);
      
        Database.UpdateParameter(dbCommand,"@c_person",hresi_cc.c_person);
      
        Database.UpdateParameter(dbCommand,"@note",hresi_cc.note);
      
        Database.UpdateParameter(dbCommand,"@special1",hresi_cc.special1);
      
        Database.UpdateParameter(dbCommand,"@grade",hresi_cc.grade);
      
        Database.UpdateParameter(dbCommand,"@tax_perc",hresi_cc.tax_perc);
      
        Database.UpdateParameter(dbCommand,"@spec_id",hresi_cc.spec_id);
      
        Database.UpdateParameter(dbCommand,"@sd_name",hresi_cc.sd_name);
      
        Database.UpdateParameter(dbCommand,"@sd_amt",hresi_cc.sd_amt);
      
        Database.UpdateParameter(dbCommand,"@min_prc",hresi_cc.min_prc);
      
        Database.UpdateParameter(dbCommand,"@mp_used",hresi_cc.mp_used);
      
        Database.UpdateParameter(dbCommand,"@btid",hresi_cc.btid);
      
        Database.UpdateParameter(dbCommand,"@companyid",hresi_cc.companyid);
      
        Database.UpdateParameter(dbCommand,"@comp_refer",hresi_cc.comp_refer);
      
        Database.UpdateParameter(dbCommand,"@repeat",hresi_cc.repeat);
      
        Database.UpdateParameter(dbCommand,"@cc_num",hresi_cc.cc_num);
      
        Database.UpdateParameter(dbCommand,"@cc_expdate",hresi_cc.cc_expdate);
      
        Database.UpdateParameter(dbCommand,"@dl_num",hresi_cc.dl_num);
      
        Database.UpdateParameter(dbCommand,"@dl_dob",hresi_cc.dl_dob);
      
        Database.UpdateParameter(dbCommand,"@auth_code",hresi_cc.auth_code);
      
        Database.UpdateParameter(dbCommand,"@t_arrival",hresi_cc.t_arrival);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update hresi_cc Set "
      
        + " hresi_cc.cust_id = ? , "
      
        + " hresi_cc.rejected = ? , "
      
        + " hresi_cc.trans_num = ? , "
      
        + " hresi_cc.csr_id = ? , "
      
        + " hresi_cc.ad_source = ? , "
      
        + " hresi_cc.alt_addr = ? , "
      
        + " hresi_cc.tech_refer = ? , "
      
        + " hresi_cc.d_1st_call = ? , "
      
        + " hresi_cc.t_1st_call = ? , "
      
        + " hresi_cc.d_schedule = ? , "
      
        + " hresi_cc.t_schedule = ? , "
      
        + " hresi_cc.d_dispatch = ? , "
      
        + " hresi_cc.t_dispatch = ? , "
      
        + " hresi_cc.d_complete = ? , "
      
        + " hresi_cc.t_complete = ? , "
      
        + " hresi_cc.tran_type = ? , "
      
        + " hresi_cc.tran_stat = ? , "
      
        + " hresi_cc.comp_type = ? , "
      
        + " hresi_cc.canc_type = ? , "
      
        + " hresi_cc.mop = ? , "
      
        + " hresi_cc.amount = ? , "
      
        + " hresi_cc.reschd_num = ? , "
      
        + " hresi_cc.company = ? , "
      
        + " hresi_cc.tech_id = ? , "
      
        + " hresi_cc.b_person = ? , "
      
        + " hresi_cc.l_person = ? , "
      
        + " hresi_cc.c_person = ? , "
      
        + " hresi_cc.note = ? , "
      
        + " hresi_cc.special1 = ? , "
      
        + " hresi_cc.grade = ? , "
      
        + " hresi_cc.tax_perc = ? , "
      
        + " hresi_cc.spec_id = ? , "
      
        + " hresi_cc.sd_name = ? , "
      
        + " hresi_cc.sd_amt = ? , "
      
        + " hresi_cc.min_prc = ? , "
      
        + " hresi_cc.mp_used = ? , "
      
        + " hresi_cc.btid = ? , "
      
        + " hresi_cc.companyid = ? , "
      
        + " hresi_cc.comp_refer = ? , "
      
        + " hresi_cc.repeat = ? , "
      
        + " hresi_cc.cc_num = ? , "
      
        + " hresi_cc.cc_expdate = ? , "
      
        + " hresi_cc.dl_num = ? , "
      
        + " hresi_cc.dl_dob = ? , "
      
        + " hresi_cc.auth_code = ? , "
      
        + " hresi_cc.t_arrival = ?  "
      
        + " Where "
        
          + " hresi_cc.ticket_num = ?  "
        
      ;

      public static void Update(hresi_cc hresi_cc)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@cust_id", hresi_cc.cust_id);
      
        Database.PutParameter(dbCommand,"@rejected", hresi_cc.rejected);
      
        Database.PutParameter(dbCommand,"@trans_num", hresi_cc.trans_num);
      
        Database.PutParameter(dbCommand,"@csr_id", hresi_cc.csr_id);
      
        Database.PutParameter(dbCommand,"@ad_source", hresi_cc.ad_source);
      
        Database.PutParameter(dbCommand,"@alt_addr", hresi_cc.alt_addr);
      
        Database.PutParameter(dbCommand,"@tech_refer", hresi_cc.tech_refer);
      
        Database.PutParameter(dbCommand,"@d_1st_call", hresi_cc.d_1st_call);
      
        Database.PutParameter(dbCommand,"@t_1st_call", hresi_cc.t_1st_call);
      
        Database.PutParameter(dbCommand,"@d_schedule", hresi_cc.d_schedule);
      
        Database.PutParameter(dbCommand,"@t_schedule", hresi_cc.t_schedule);
      
        Database.PutParameter(dbCommand,"@d_dispatch", hresi_cc.d_dispatch);
      
        Database.PutParameter(dbCommand,"@t_dispatch", hresi_cc.t_dispatch);
      
        Database.PutParameter(dbCommand,"@d_complete", hresi_cc.d_complete);
      
        Database.PutParameter(dbCommand,"@t_complete", hresi_cc.t_complete);
      
        Database.PutParameter(dbCommand,"@tran_type", hresi_cc.tran_type);
      
        Database.PutParameter(dbCommand,"@tran_stat", hresi_cc.tran_stat);
      
        Database.PutParameter(dbCommand,"@comp_type", hresi_cc.comp_type);
      
        Database.PutParameter(dbCommand,"@canc_type", hresi_cc.canc_type);
      
        Database.PutParameter(dbCommand,"@mop", hresi_cc.mop);
      
        Database.PutParameter(dbCommand,"@amount", hresi_cc.amount);
      
        Database.PutParameter(dbCommand,"@reschd_num", hresi_cc.reschd_num);
      
        Database.PutParameter(dbCommand,"@company", hresi_cc.company);
      
        Database.PutParameter(dbCommand,"@tech_id", hresi_cc.tech_id);
      
        Database.PutParameter(dbCommand,"@b_person", hresi_cc.b_person);
      
        Database.PutParameter(dbCommand,"@l_person", hresi_cc.l_person);
      
        Database.PutParameter(dbCommand,"@c_person", hresi_cc.c_person);
      
        Database.PutParameter(dbCommand,"@note", hresi_cc.note);
      
        Database.PutParameter(dbCommand,"@special1", hresi_cc.special1);
      
        Database.PutParameter(dbCommand,"@grade", hresi_cc.grade);
      
        Database.PutParameter(dbCommand,"@tax_perc", hresi_cc.tax_perc);
      
        Database.PutParameter(dbCommand,"@spec_id", hresi_cc.spec_id);
      
        Database.PutParameter(dbCommand,"@sd_name", hresi_cc.sd_name);
      
        Database.PutParameter(dbCommand,"@sd_amt", hresi_cc.sd_amt);
      
        Database.PutParameter(dbCommand,"@min_prc", hresi_cc.min_prc);
      
        Database.PutParameter(dbCommand,"@mp_used", hresi_cc.mp_used);
      
        Database.PutParameter(dbCommand,"@btid", hresi_cc.btid);
      
        Database.PutParameter(dbCommand,"@companyid", hresi_cc.companyid);
      
        Database.PutParameter(dbCommand,"@comp_refer", hresi_cc.comp_refer);
      
        Database.PutParameter(dbCommand,"@repeat", hresi_cc.repeat);
      
        Database.PutParameter(dbCommand,"@cc_num", hresi_cc.cc_num);
      
        Database.PutParameter(dbCommand,"@cc_expdate", hresi_cc.cc_expdate);
      
        Database.PutParameter(dbCommand,"@dl_num", hresi_cc.dl_num);
      
        Database.PutParameter(dbCommand,"@dl_dob", hresi_cc.dl_dob);
      
        Database.PutParameter(dbCommand,"@auth_code", hresi_cc.auth_code);
      
        Database.PutParameter(dbCommand,"@t_arrival", hresi_cc.t_arrival);
      
        Database.PutParameter(dbCommand,"@ticket_num", hresi_cc.ticket_num);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " hresi_cc.ticket_num, "
      
        + " hresi_cc.cust_id, "
      
        + " hresi_cc.rejected, "
      
        + " hresi_cc.trans_num, "
      
        + " hresi_cc.csr_id, "
      
        + " hresi_cc.ad_source, "
      
        + " hresi_cc.alt_addr, "
      
        + " hresi_cc.tech_refer, "
      
        + " hresi_cc.d_1st_call, "
      
        + " hresi_cc.t_1st_call, "
      
        + " hresi_cc.d_schedule, "
      
        + " hresi_cc.t_schedule, "
      
        + " hresi_cc.d_dispatch, "
      
        + " hresi_cc.t_dispatch, "
      
        + " hresi_cc.d_complete, "
      
        + " hresi_cc.t_complete, "
      
        + " hresi_cc.tran_type, "
      
        + " hresi_cc.tran_stat, "
      
        + " hresi_cc.comp_type, "
      
        + " hresi_cc.canc_type, "
      
        + " hresi_cc.mop, "
      
        + " hresi_cc.amount, "
      
        + " hresi_cc.reschd_num, "
      
        + " hresi_cc.company, "
      
        + " hresi_cc.tech_id, "
      
        + " hresi_cc.b_person, "
      
        + " hresi_cc.l_person, "
      
        + " hresi_cc.c_person, "
      
        + " hresi_cc.note, "
      
        + " hresi_cc.special1, "
      
        + " hresi_cc.grade, "
      
        + " hresi_cc.tax_perc, "
      
        + " hresi_cc.spec_id, "
      
        + " hresi_cc.sd_name, "
      
        + " hresi_cc.sd_amt, "
      
        + " hresi_cc.min_prc, "
      
        + " hresi_cc.mp_used, "
      
        + " hresi_cc.btid, "
      
        + " hresi_cc.companyid, "
      
        + " hresi_cc.comp_refer, "
      
        + " hresi_cc.repeat, "
      
        + " hresi_cc.cc_num, "
      
        + " hresi_cc.cc_expdate, "
      
        + " hresi_cc.dl_num, "
      
        + " hresi_cc.dl_dob, "
      
        + " hresi_cc.auth_code, "
      
        + " hresi_cc.t_arrival "
      

      + " From hresi_cc "

      
        + " Where "
        
          + " hresi_cc.ticket_num = ?  "
        
      ;

      public static hresi_cc FindByPrimaryKey(
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
      throw new DataNotFoundException("hresi_cc not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(hresi_cc hresi_cc)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@ticket_num",hresi_cc.ticket_num);
      

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
      String sql = "select 1 from hresi_cc";

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

      public static hresi_cc Load(IDataReader dataReader)
      {
      hresi_cc hresi_cc = new hresi_cc();

      hresi_cc.ticket_num = dataReader.GetString(0);
          hresi_cc.cust_id = dataReader.GetString(1);
          hresi_cc.rejected = dataReader.GetBoolean(2);
          hresi_cc.trans_num = dataReader.GetString(3);
          hresi_cc.csr_id = dataReader.GetString(4);
          hresi_cc.ad_source = dataReader.GetString(5);
          hresi_cc.alt_addr = dataReader.GetBoolean(6);
          hresi_cc.tech_refer = dataReader.GetString(7);
          hresi_cc.d_1st_call = dataReader.GetDateTime(8);
          hresi_cc.t_1st_call = dataReader.GetString(9);
          hresi_cc.d_schedule = dataReader.GetDateTime(10);
          hresi_cc.t_schedule = dataReader.GetString(11);
          hresi_cc.d_dispatch = dataReader.GetDateTime(12);
          hresi_cc.t_dispatch = dataReader.GetString(13);
          hresi_cc.d_complete = dataReader.GetDateTime(14);
          hresi_cc.t_complete = dataReader.GetString(15);
          hresi_cc.tran_type = dataReader.GetDecimal(16);
          hresi_cc.tran_stat = dataReader.GetDecimal(17);
          hresi_cc.comp_type = dataReader.GetDecimal(18);
          hresi_cc.canc_type = dataReader.GetDecimal(19);
          hresi_cc.mop = dataReader.GetDecimal(20);
          hresi_cc.amount = dataReader.GetDecimal(21);
          hresi_cc.reschd_num = dataReader.GetDecimal(22);
          hresi_cc.company = dataReader.GetString(23);
          hresi_cc.tech_id = dataReader.GetString(24);
          hresi_cc.b_person = dataReader.GetString(25);
          hresi_cc.l_person = dataReader.GetString(26);
          hresi_cc.c_person = dataReader.GetString(27);
          hresi_cc.note = dataReader.GetString(28);
          hresi_cc.special1 = dataReader.GetString(29);
          hresi_cc.grade = dataReader.GetDecimal(30);
          hresi_cc.tax_perc = dataReader.GetDecimal(31);
          hresi_cc.spec_id = dataReader.GetDecimal(32);
          hresi_cc.sd_name = dataReader.GetString(33);
          hresi_cc.sd_amt = dataReader.GetDecimal(34);
          hresi_cc.min_prc = dataReader.GetDecimal(35);
          hresi_cc.mp_used = dataReader.GetBoolean(36);
          hresi_cc.btid = dataReader.GetDecimal(37);
          hresi_cc.companyid = dataReader.GetDecimal(38);
          hresi_cc.comp_refer = dataReader.GetDecimal(39);
          hresi_cc.repeat = dataReader.GetBoolean(40);
          hresi_cc.cc_num = dataReader.GetString(41);
          hresi_cc.cc_expdate = dataReader.GetString(42);
          hresi_cc.dl_num = dataReader.GetString(43);
          hresi_cc.dl_dob = dataReader.GetDateTime(44);
          hresi_cc.auth_code = dataReader.GetString(45);
          hresi_cc.t_arrival = dataReader.GetString(46);
          

      return hresi_cc;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [hresi_cc] "

      
        + " Where "
        
          + " ticket_num = ?  "
        
      ;
      public static void Delete(hresi_cc hresi_cc)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      
        Database.PutParameter(dbCommand,"@ticket_num", hresi_cc.ticket_num);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [hresi_cc] ";

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

      
        + " hresi_cc.ticket_num, "
      
        + " hresi_cc.cust_id, "
      
        + " hresi_cc.rejected, "
      
        + " hresi_cc.trans_num, "
      
        + " hresi_cc.csr_id, "
      
        + " hresi_cc.ad_source, "
      
        + " hresi_cc.alt_addr, "
      
        + " hresi_cc.tech_refer, "
      
        + " hresi_cc.d_1st_call, "
      
        + " hresi_cc.t_1st_call, "
      
        + " hresi_cc.d_schedule, "
      
        + " hresi_cc.t_schedule, "
      
        + " hresi_cc.d_dispatch, "
      
        + " hresi_cc.t_dispatch, "
      
        + " hresi_cc.d_complete, "
      
        + " hresi_cc.t_complete, "
      
        + " hresi_cc.tran_type, "
      
        + " hresi_cc.tran_stat, "
      
        + " hresi_cc.comp_type, "
      
        + " hresi_cc.canc_type, "
      
        + " hresi_cc.mop, "
      
        + " hresi_cc.amount, "
      
        + " hresi_cc.reschd_num, "
      
        + " hresi_cc.company, "
      
        + " hresi_cc.tech_id, "
      
        + " hresi_cc.b_person, "
      
        + " hresi_cc.l_person, "
      
        + " hresi_cc.c_person, "
      
        + " hresi_cc.note, "
      
        + " hresi_cc.special1, "
      
        + " hresi_cc.grade, "
      
        + " hresi_cc.tax_perc, "
      
        + " hresi_cc.spec_id, "
      
        + " hresi_cc.sd_name, "
      
        + " hresi_cc.sd_amt, "
      
        + " hresi_cc.min_prc, "
      
        + " hresi_cc.mp_used, "
      
        + " hresi_cc.btid, "
      
        + " hresi_cc.companyid, "
      
        + " hresi_cc.comp_refer, "
      
        + " hresi_cc.repeat, "
      
        + " hresi_cc.cc_num, "
      
        + " hresi_cc.cc_expdate, "
      
        + " hresi_cc.dl_num, "
      
        + " hresi_cc.dl_dob, "
      
        + " hresi_cc.auth_code, "
      
        + " hresi_cc.t_arrival "
      

      + " From hresi_cc ";
      public static List<hresi_cc> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<hresi_cc> rv = new List<hresi_cc>();

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
      List<hresi_cc> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<hresi_cc> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(hresi_cc));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(hresi_cc item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<hresi_cc>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(hresi_cc));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<hresi_cc> itemsList
      = new List<hresi_cc>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is hresi_cc)
      itemsList.Add(deserializedObject as hresi_cc);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_ticket_num;
      
        protected String m_cust_id;
      
        protected bool m_rejected;
      
        protected String m_trans_num;
      
        protected String m_csr_id;
      
        protected String m_ad_source;
      
        protected bool m_alt_addr;
      
        protected String m_tech_refer;
      
        protected DateTime m_d_1st_call;
      
        protected String m_t_1st_call;
      
        protected DateTime m_d_schedule;
      
        protected String m_t_schedule;
      
        protected DateTime m_d_dispatch;
      
        protected String m_t_dispatch;
      
        protected DateTime m_d_complete;
      
        protected String m_t_complete;
      
        protected decimal m_tran_type;
      
        protected decimal m_tran_stat;
      
        protected decimal m_comp_type;
      
        protected decimal m_canc_type;
      
        protected decimal m_mop;
      
        protected decimal m_amount;
      
        protected decimal m_reschd_num;
      
        protected String m_company;
      
        protected String m_tech_id;
      
        protected String m_b_person;
      
        protected String m_l_person;
      
        protected String m_c_person;
      
        protected String m_note;
      
        protected String m_special1;
      
        protected decimal m_grade;
      
        protected decimal m_tax_perc;
      
        protected decimal m_spec_id;
      
        protected String m_sd_name;
      
        protected decimal m_sd_amt;
      
        protected decimal m_min_prc;
      
        protected bool m_mp_used;
      
        protected decimal m_btid;
      
        protected decimal m_companyid;
      
        protected decimal m_comp_refer;
      
        protected bool m_repeat;
      
        protected String m_cc_num;
      
        protected String m_cc_expdate;
      
        protected String m_dl_num;
      
        protected DateTime m_dl_dob;
      
        protected String m_auth_code;
      
        protected String m_t_arrival;
      
      #endregion

      #region Constructors
      public hresi_cc(
      String 
          ticket_num
      )
      {
      
        m_ticket_num = ticket_num;
      
      }

      


        public hresi_cc(
        String 
          ticket_num,String 
          cust_id,bool 
          rejected,String 
          trans_num,String 
          csr_id,String 
          ad_source,bool 
          alt_addr,String 
          tech_refer,DateTime 
          d_1st_call,String 
          t_1st_call,DateTime 
          d_schedule,String 
          t_schedule,DateTime 
          d_dispatch,String 
          t_dispatch,DateTime 
          d_complete,String 
          t_complete,decimal 
          tran_type,decimal 
          tran_stat,decimal 
          comp_type,decimal 
          canc_type,decimal 
          mop,decimal 
          amount,decimal 
          reschd_num,String 
          company,String 
          tech_id,String 
          b_person,String 
          l_person,String 
          c_person,String 
          note,String 
          special1,decimal 
          grade,decimal 
          tax_perc,decimal 
          spec_id,String 
          sd_name,decimal 
          sd_amt,decimal 
          min_prc,bool 
          mp_used,decimal 
          btid,decimal 
          companyid,decimal 
          comp_refer,bool 
          repeat,String 
          cc_num,String 
          cc_expdate,String 
          dl_num,DateTime 
          dl_dob,String 
          auth_code,String 
          t_arrival
        )
        {
        
          m_ticket_num = ticket_num;
        
          m_cust_id = cust_id;
        
          m_rejected = rejected;
        
          m_trans_num = trans_num;
        
          m_csr_id = csr_id;
        
          m_ad_source = ad_source;
        
          m_alt_addr = alt_addr;
        
          m_tech_refer = tech_refer;
        
          m_d_1st_call = d_1st_call;
        
          m_t_1st_call = t_1st_call;
        
          m_d_schedule = d_schedule;
        
          m_t_schedule = t_schedule;
        
          m_d_dispatch = d_dispatch;
        
          m_t_dispatch = t_dispatch;
        
          m_d_complete = d_complete;
        
          m_t_complete = t_complete;
        
          m_tran_type = tran_type;
        
          m_tran_stat = tran_stat;
        
          m_comp_type = comp_type;
        
          m_canc_type = canc_type;
        
          m_mop = mop;
        
          m_amount = amount;
        
          m_reschd_num = reschd_num;
        
          m_company = company;
        
          m_tech_id = tech_id;
        
          m_b_person = b_person;
        
          m_l_person = l_person;
        
          m_c_person = c_person;
        
          m_note = note;
        
          m_special1 = special1;
        
          m_grade = grade;
        
          m_tax_perc = tax_perc;
        
          m_spec_id = spec_id;
        
          m_sd_name = sd_name;
        
          m_sd_amt = sd_amt;
        
          m_min_prc = min_prc;
        
          m_mp_used = mp_used;
        
          m_btid = btid;
        
          m_companyid = companyid;
        
          m_comp_refer = comp_refer;
        
          m_repeat = repeat;
        
          m_cc_num = cc_num;
        
          m_cc_expdate = cc_expdate;
        
          m_dl_num = dl_num;
        
          m_dl_dob = dl_dob;
        
          m_auth_code = auth_code;
        
          m_t_arrival = t_arrival;
        
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
        public bool rejected
        {
        get { return m_rejected;}
        set { m_rejected = value; }
        }
      
        [XmlElement]
        public String trans_num
        {
        get { return m_trans_num;}
        set { m_trans_num = value; }
        }
      
        [XmlElement]
        public String csr_id
        {
        get { return m_csr_id;}
        set { m_csr_id = value; }
        }
      
        [XmlElement]
        public String ad_source
        {
        get { return m_ad_source;}
        set { m_ad_source = value; }
        }
      
        [XmlElement]
        public bool alt_addr
        {
        get { return m_alt_addr;}
        set { m_alt_addr = value; }
        }
      
        [XmlElement]
        public String tech_refer
        {
        get { return m_tech_refer;}
        set { m_tech_refer = value; }
        }
      
        [XmlElement]
        public DateTime d_1st_call
        {
        get { return m_d_1st_call;}
        set { m_d_1st_call = value; }
        }
      
        [XmlElement]
        public String t_1st_call
        {
        get { return m_t_1st_call;}
        set { m_t_1st_call = value; }
        }
      
        [XmlElement]
        public DateTime d_schedule
        {
        get { return m_d_schedule;}
        set { m_d_schedule = value; }
        }
      
        [XmlElement]
        public String t_schedule
        {
        get { return m_t_schedule;}
        set { m_t_schedule = value; }
        }
      
        [XmlElement]
        public DateTime d_dispatch
        {
        get { return m_d_dispatch;}
        set { m_d_dispatch = value; }
        }
      
        [XmlElement]
        public String t_dispatch
        {
        get { return m_t_dispatch;}
        set { m_t_dispatch = value; }
        }
      
        [XmlElement]
        public DateTime d_complete
        {
        get { return m_d_complete;}
        set { m_d_complete = value; }
        }
      
        [XmlElement]
        public String t_complete
        {
        get { return m_t_complete;}
        set { m_t_complete = value; }
        }
      
        [XmlElement]
        public decimal tran_type
        {
        get { return m_tran_type;}
        set { m_tran_type = value; }
        }
      
        [XmlElement]
        public decimal tran_stat
        {
        get { return m_tran_stat;}
        set { m_tran_stat = value; }
        }
      
        [XmlElement]
        public decimal comp_type
        {
        get { return m_comp_type;}
        set { m_comp_type = value; }
        }
      
        [XmlElement]
        public decimal canc_type
        {
        get { return m_canc_type;}
        set { m_canc_type = value; }
        }
      
        [XmlElement]
        public decimal mop
        {
        get { return m_mop;}
        set { m_mop = value; }
        }
      
        [XmlElement]
        public decimal amount
        {
        get { return m_amount;}
        set { m_amount = value; }
        }
      
        [XmlElement]
        public decimal reschd_num
        {
        get { return m_reschd_num;}
        set { m_reschd_num = value; }
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
        public String b_person
        {
        get { return m_b_person;}
        set { m_b_person = value; }
        }
      
        [XmlElement]
        public String l_person
        {
        get { return m_l_person;}
        set { m_l_person = value; }
        }
      
        [XmlElement]
        public String c_person
        {
        get { return m_c_person;}
        set { m_c_person = value; }
        }
      
        [XmlElement]
        public String note
        {
        get { return m_note;}
        set { m_note = value; }
        }
      
        [XmlElement]
        public String special1
        {
        get { return m_special1;}
        set { m_special1 = value; }
        }
      
        [XmlElement]
        public decimal grade
        {
        get { return m_grade;}
        set { m_grade = value; }
        }
      
        [XmlElement]
        public decimal tax_perc
        {
        get { return m_tax_perc;}
        set { m_tax_perc = value; }
        }
      
        [XmlElement]
        public decimal spec_id
        {
        get { return m_spec_id;}
        set { m_spec_id = value; }
        }
      
        [XmlElement]
        public String sd_name
        {
        get { return m_sd_name;}
        set { m_sd_name = value; }
        }
      
        [XmlElement]
        public decimal sd_amt
        {
        get { return m_sd_amt;}
        set { m_sd_amt = value; }
        }
      
        [XmlElement]
        public decimal min_prc
        {
        get { return m_min_prc;}
        set { m_min_prc = value; }
        }
      
        [XmlElement]
        public bool mp_used
        {
        get { return m_mp_used;}
        set { m_mp_used = value; }
        }
      
        [XmlElement]
        public decimal btid
        {
        get { return m_btid;}
        set { m_btid = value; }
        }
      
        [XmlElement]
        public decimal companyid
        {
        get { return m_companyid;}
        set { m_companyid = value; }
        }
      
        [XmlElement]
        public decimal comp_refer
        {
        get { return m_comp_refer;}
        set { m_comp_refer = value; }
        }
      
        [XmlElement]
        public bool repeat
        {
        get { return m_repeat;}
        set { m_repeat = value; }
        }
      
        [XmlElement]
        public String cc_num
        {
        get { return m_cc_num;}
        set { m_cc_num = value; }
        }
      
        [XmlElement]
        public String cc_expdate
        {
        get { return m_cc_expdate;}
        set { m_cc_expdate = value; }
        }
      
        [XmlElement]
        public String dl_num
        {
        get { return m_dl_num;}
        set { m_dl_num = value; }
        }
      
        [XmlElement]
        public DateTime dl_dob
        {
        get { return m_dl_dob;}
        set { m_dl_dob = value; }
        }
      
        [XmlElement]
        public String auth_code
        {
        get { return m_auth_code;}
        set { m_auth_code = value; }
        }
      
        [XmlElement]
        public String t_arrival
        {
        get { return m_t_arrival;}
        set { m_t_arrival = value; }
        }
      
      }
      #endregion
      }

    