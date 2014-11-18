
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


      public partial class hdeflood
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into hdeflood ( " +
      
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
      
        " src_flood, " +
      
        " d_of_flood, " +
      
        " rooms, " +
      
        " ordered_by, " +
      
        " i_agncy, " +
      
        " i_agncy_ph, " +
      
        " i_agent, " +
      
        " i_carrier, " +
      
        " i_adj, " +
      
        " i_adj_ph, " +
      
        " referred, " +
      
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
      
        " callorigin, " +
      
        " phase, " +
      
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

      public static void Insert(hdeflood hdeflood)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@ticket_num", hdeflood.ticket_num);
      
        Database.PutParameter(dbCommand,"@cust_id", hdeflood.cust_id);
      
        Database.PutParameter(dbCommand,"@rejected", hdeflood.rejected);
      
        Database.PutParameter(dbCommand,"@trans_num", hdeflood.trans_num);
      
        Database.PutParameter(dbCommand,"@csr_id", hdeflood.csr_id);
      
        Database.PutParameter(dbCommand,"@ad_source", hdeflood.ad_source);
      
        Database.PutParameter(dbCommand,"@alt_addr", hdeflood.alt_addr);
      
        Database.PutParameter(dbCommand,"@tech_refer", hdeflood.tech_refer);
      
        Database.PutParameter(dbCommand,"@d_1st_call", hdeflood.d_1st_call);
      
        Database.PutParameter(dbCommand,"@t_1st_call", hdeflood.t_1st_call);
      
        Database.PutParameter(dbCommand,"@d_schedule", hdeflood.d_schedule);
      
        Database.PutParameter(dbCommand,"@t_schedule", hdeflood.t_schedule);
      
        Database.PutParameter(dbCommand,"@d_dispatch", hdeflood.d_dispatch);
      
        Database.PutParameter(dbCommand,"@t_dispatch", hdeflood.t_dispatch);
      
        Database.PutParameter(dbCommand,"@d_complete", hdeflood.d_complete);
      
        Database.PutParameter(dbCommand,"@t_complete", hdeflood.t_complete);
      
        Database.PutParameter(dbCommand,"@tran_type", hdeflood.tran_type);
      
        Database.PutParameter(dbCommand,"@tran_stat", hdeflood.tran_stat);
      
        Database.PutParameter(dbCommand,"@comp_type", hdeflood.comp_type);
      
        Database.PutParameter(dbCommand,"@canc_type", hdeflood.canc_type);
      
        Database.PutParameter(dbCommand,"@mop", hdeflood.mop);
      
        Database.PutParameter(dbCommand,"@amount", hdeflood.amount);
      
        Database.PutParameter(dbCommand,"@reschd_num", hdeflood.reschd_num);
      
        Database.PutParameter(dbCommand,"@company", hdeflood.company);
      
        Database.PutParameter(dbCommand,"@tech_id", hdeflood.tech_id);
      
        Database.PutParameter(dbCommand,"@b_person", hdeflood.b_person);
      
        Database.PutParameter(dbCommand,"@l_person", hdeflood.l_person);
      
        Database.PutParameter(dbCommand,"@c_person", hdeflood.c_person);
      
        Database.PutParameter(dbCommand,"@note", hdeflood.note);
      
        Database.PutParameter(dbCommand,"@special1", hdeflood.special1);
      
        Database.PutParameter(dbCommand,"@src_flood", hdeflood.src_flood);
      
        Database.PutParameter(dbCommand,"@d_of_flood", hdeflood.d_of_flood);
      
        Database.PutParameter(dbCommand,"@rooms", hdeflood.rooms);
      
        Database.PutParameter(dbCommand,"@ordered_by", hdeflood.ordered_by);
      
        Database.PutParameter(dbCommand,"@i_agncy", hdeflood.i_agncy);
      
        Database.PutParameter(dbCommand,"@i_agncy_ph", hdeflood.i_agncy_ph);
      
        Database.PutParameter(dbCommand,"@i_agent", hdeflood.i_agent);
      
        Database.PutParameter(dbCommand,"@i_carrier", hdeflood.i_carrier);
      
        Database.PutParameter(dbCommand,"@i_adj", hdeflood.i_adj);
      
        Database.PutParameter(dbCommand,"@i_adj_ph", hdeflood.i_adj_ph);
      
        Database.PutParameter(dbCommand,"@referred", hdeflood.referred);
      
        Database.PutParameter(dbCommand,"@grade", hdeflood.grade);
      
        Database.PutParameter(dbCommand,"@tax_perc", hdeflood.tax_perc);
      
        Database.PutParameter(dbCommand,"@spec_id", hdeflood.spec_id);
      
        Database.PutParameter(dbCommand,"@sd_name", hdeflood.sd_name);
      
        Database.PutParameter(dbCommand,"@sd_amt", hdeflood.sd_amt);
      
        Database.PutParameter(dbCommand,"@min_prc", hdeflood.min_prc);
      
        Database.PutParameter(dbCommand,"@mp_used", hdeflood.mp_used);
      
        Database.PutParameter(dbCommand,"@btid", hdeflood.btid);
      
        Database.PutParameter(dbCommand,"@companyid", hdeflood.companyid);
      
        Database.PutParameter(dbCommand,"@comp_refer", hdeflood.comp_refer);
      
        Database.PutParameter(dbCommand,"@callorigin", hdeflood.callorigin);
      
        Database.PutParameter(dbCommand,"@phase", hdeflood.phase);
      
        Database.PutParameter(dbCommand,"@repeat", hdeflood.repeat);
      
        Database.PutParameter(dbCommand,"@cc_num", hdeflood.cc_num);
      
        Database.PutParameter(dbCommand,"@cc_expdate", hdeflood.cc_expdate);
      
        Database.PutParameter(dbCommand,"@dl_num", hdeflood.dl_num);
      
        Database.PutParameter(dbCommand,"@dl_dob", hdeflood.dl_dob);
      
        Database.PutParameter(dbCommand,"@auth_code", hdeflood.auth_code);
      
        Database.PutParameter(dbCommand,"@t_arrival", hdeflood.t_arrival);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<hdeflood>  hdefloodList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(hdeflood hdeflood in  hdefloodList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ticket_num", hdeflood.ticket_num);
      
        Database.PutParameter(dbCommand,"@cust_id", hdeflood.cust_id);
      
        Database.PutParameter(dbCommand,"@rejected", hdeflood.rejected);
      
        Database.PutParameter(dbCommand,"@trans_num", hdeflood.trans_num);
      
        Database.PutParameter(dbCommand,"@csr_id", hdeflood.csr_id);
      
        Database.PutParameter(dbCommand,"@ad_source", hdeflood.ad_source);
      
        Database.PutParameter(dbCommand,"@alt_addr", hdeflood.alt_addr);
      
        Database.PutParameter(dbCommand,"@tech_refer", hdeflood.tech_refer);
      
        Database.PutParameter(dbCommand,"@d_1st_call", hdeflood.d_1st_call);
      
        Database.PutParameter(dbCommand,"@t_1st_call", hdeflood.t_1st_call);
      
        Database.PutParameter(dbCommand,"@d_schedule", hdeflood.d_schedule);
      
        Database.PutParameter(dbCommand,"@t_schedule", hdeflood.t_schedule);
      
        Database.PutParameter(dbCommand,"@d_dispatch", hdeflood.d_dispatch);
      
        Database.PutParameter(dbCommand,"@t_dispatch", hdeflood.t_dispatch);
      
        Database.PutParameter(dbCommand,"@d_complete", hdeflood.d_complete);
      
        Database.PutParameter(dbCommand,"@t_complete", hdeflood.t_complete);
      
        Database.PutParameter(dbCommand,"@tran_type", hdeflood.tran_type);
      
        Database.PutParameter(dbCommand,"@tran_stat", hdeflood.tran_stat);
      
        Database.PutParameter(dbCommand,"@comp_type", hdeflood.comp_type);
      
        Database.PutParameter(dbCommand,"@canc_type", hdeflood.canc_type);
      
        Database.PutParameter(dbCommand,"@mop", hdeflood.mop);
      
        Database.PutParameter(dbCommand,"@amount", hdeflood.amount);
      
        Database.PutParameter(dbCommand,"@reschd_num", hdeflood.reschd_num);
      
        Database.PutParameter(dbCommand,"@company", hdeflood.company);
      
        Database.PutParameter(dbCommand,"@tech_id", hdeflood.tech_id);
      
        Database.PutParameter(dbCommand,"@b_person", hdeflood.b_person);
      
        Database.PutParameter(dbCommand,"@l_person", hdeflood.l_person);
      
        Database.PutParameter(dbCommand,"@c_person", hdeflood.c_person);
      
        Database.PutParameter(dbCommand,"@note", hdeflood.note);
      
        Database.PutParameter(dbCommand,"@special1", hdeflood.special1);
      
        Database.PutParameter(dbCommand,"@src_flood", hdeflood.src_flood);
      
        Database.PutParameter(dbCommand,"@d_of_flood", hdeflood.d_of_flood);
      
        Database.PutParameter(dbCommand,"@rooms", hdeflood.rooms);
      
        Database.PutParameter(dbCommand,"@ordered_by", hdeflood.ordered_by);
      
        Database.PutParameter(dbCommand,"@i_agncy", hdeflood.i_agncy);
      
        Database.PutParameter(dbCommand,"@i_agncy_ph", hdeflood.i_agncy_ph);
      
        Database.PutParameter(dbCommand,"@i_agent", hdeflood.i_agent);
      
        Database.PutParameter(dbCommand,"@i_carrier", hdeflood.i_carrier);
      
        Database.PutParameter(dbCommand,"@i_adj", hdeflood.i_adj);
      
        Database.PutParameter(dbCommand,"@i_adj_ph", hdeflood.i_adj_ph);
      
        Database.PutParameter(dbCommand,"@referred", hdeflood.referred);
      
        Database.PutParameter(dbCommand,"@grade", hdeflood.grade);
      
        Database.PutParameter(dbCommand,"@tax_perc", hdeflood.tax_perc);
      
        Database.PutParameter(dbCommand,"@spec_id", hdeflood.spec_id);
      
        Database.PutParameter(dbCommand,"@sd_name", hdeflood.sd_name);
      
        Database.PutParameter(dbCommand,"@sd_amt", hdeflood.sd_amt);
      
        Database.PutParameter(dbCommand,"@min_prc", hdeflood.min_prc);
      
        Database.PutParameter(dbCommand,"@mp_used", hdeflood.mp_used);
      
        Database.PutParameter(dbCommand,"@btid", hdeflood.btid);
      
        Database.PutParameter(dbCommand,"@companyid", hdeflood.companyid);
      
        Database.PutParameter(dbCommand,"@comp_refer", hdeflood.comp_refer);
      
        Database.PutParameter(dbCommand,"@callorigin", hdeflood.callorigin);
      
        Database.PutParameter(dbCommand,"@phase", hdeflood.phase);
      
        Database.PutParameter(dbCommand,"@repeat", hdeflood.repeat);
      
        Database.PutParameter(dbCommand,"@cc_num", hdeflood.cc_num);
      
        Database.PutParameter(dbCommand,"@cc_expdate", hdeflood.cc_expdate);
      
        Database.PutParameter(dbCommand,"@dl_num", hdeflood.dl_num);
      
        Database.PutParameter(dbCommand,"@dl_dob", hdeflood.dl_dob);
      
        Database.PutParameter(dbCommand,"@auth_code", hdeflood.auth_code);
      
        Database.PutParameter(dbCommand,"@t_arrival", hdeflood.t_arrival);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ticket_num",hdeflood.ticket_num);
      
        Database.UpdateParameter(dbCommand,"@cust_id",hdeflood.cust_id);
      
        Database.UpdateParameter(dbCommand,"@rejected",hdeflood.rejected);
      
        Database.UpdateParameter(dbCommand,"@trans_num",hdeflood.trans_num);
      
        Database.UpdateParameter(dbCommand,"@csr_id",hdeflood.csr_id);
      
        Database.UpdateParameter(dbCommand,"@ad_source",hdeflood.ad_source);
      
        Database.UpdateParameter(dbCommand,"@alt_addr",hdeflood.alt_addr);
      
        Database.UpdateParameter(dbCommand,"@tech_refer",hdeflood.tech_refer);
      
        Database.UpdateParameter(dbCommand,"@d_1st_call",hdeflood.d_1st_call);
      
        Database.UpdateParameter(dbCommand,"@t_1st_call",hdeflood.t_1st_call);
      
        Database.UpdateParameter(dbCommand,"@d_schedule",hdeflood.d_schedule);
      
        Database.UpdateParameter(dbCommand,"@t_schedule",hdeflood.t_schedule);
      
        Database.UpdateParameter(dbCommand,"@d_dispatch",hdeflood.d_dispatch);
      
        Database.UpdateParameter(dbCommand,"@t_dispatch",hdeflood.t_dispatch);
      
        Database.UpdateParameter(dbCommand,"@d_complete",hdeflood.d_complete);
      
        Database.UpdateParameter(dbCommand,"@t_complete",hdeflood.t_complete);
      
        Database.UpdateParameter(dbCommand,"@tran_type",hdeflood.tran_type);
      
        Database.UpdateParameter(dbCommand,"@tran_stat",hdeflood.tran_stat);
      
        Database.UpdateParameter(dbCommand,"@comp_type",hdeflood.comp_type);
      
        Database.UpdateParameter(dbCommand,"@canc_type",hdeflood.canc_type);
      
        Database.UpdateParameter(dbCommand,"@mop",hdeflood.mop);
      
        Database.UpdateParameter(dbCommand,"@amount",hdeflood.amount);
      
        Database.UpdateParameter(dbCommand,"@reschd_num",hdeflood.reschd_num);
      
        Database.UpdateParameter(dbCommand,"@company",hdeflood.company);
      
        Database.UpdateParameter(dbCommand,"@tech_id",hdeflood.tech_id);
      
        Database.UpdateParameter(dbCommand,"@b_person",hdeflood.b_person);
      
        Database.UpdateParameter(dbCommand,"@l_person",hdeflood.l_person);
      
        Database.UpdateParameter(dbCommand,"@c_person",hdeflood.c_person);
      
        Database.UpdateParameter(dbCommand,"@note",hdeflood.note);
      
        Database.UpdateParameter(dbCommand,"@special1",hdeflood.special1);
      
        Database.UpdateParameter(dbCommand,"@src_flood",hdeflood.src_flood);
      
        Database.UpdateParameter(dbCommand,"@d_of_flood",hdeflood.d_of_flood);
      
        Database.UpdateParameter(dbCommand,"@rooms",hdeflood.rooms);
      
        Database.UpdateParameter(dbCommand,"@ordered_by",hdeflood.ordered_by);
      
        Database.UpdateParameter(dbCommand,"@i_agncy",hdeflood.i_agncy);
      
        Database.UpdateParameter(dbCommand,"@i_agncy_ph",hdeflood.i_agncy_ph);
      
        Database.UpdateParameter(dbCommand,"@i_agent",hdeflood.i_agent);
      
        Database.UpdateParameter(dbCommand,"@i_carrier",hdeflood.i_carrier);
      
        Database.UpdateParameter(dbCommand,"@i_adj",hdeflood.i_adj);
      
        Database.UpdateParameter(dbCommand,"@i_adj_ph",hdeflood.i_adj_ph);
      
        Database.UpdateParameter(dbCommand,"@referred",hdeflood.referred);
      
        Database.UpdateParameter(dbCommand,"@grade",hdeflood.grade);
      
        Database.UpdateParameter(dbCommand,"@tax_perc",hdeflood.tax_perc);
      
        Database.UpdateParameter(dbCommand,"@spec_id",hdeflood.spec_id);
      
        Database.UpdateParameter(dbCommand,"@sd_name",hdeflood.sd_name);
      
        Database.UpdateParameter(dbCommand,"@sd_amt",hdeflood.sd_amt);
      
        Database.UpdateParameter(dbCommand,"@min_prc",hdeflood.min_prc);
      
        Database.UpdateParameter(dbCommand,"@mp_used",hdeflood.mp_used);
      
        Database.UpdateParameter(dbCommand,"@btid",hdeflood.btid);
      
        Database.UpdateParameter(dbCommand,"@companyid",hdeflood.companyid);
      
        Database.UpdateParameter(dbCommand,"@comp_refer",hdeflood.comp_refer);
      
        Database.UpdateParameter(dbCommand,"@callorigin",hdeflood.callorigin);
      
        Database.UpdateParameter(dbCommand,"@phase",hdeflood.phase);
      
        Database.UpdateParameter(dbCommand,"@repeat",hdeflood.repeat);
      
        Database.UpdateParameter(dbCommand,"@cc_num",hdeflood.cc_num);
      
        Database.UpdateParameter(dbCommand,"@cc_expdate",hdeflood.cc_expdate);
      
        Database.UpdateParameter(dbCommand,"@dl_num",hdeflood.dl_num);
      
        Database.UpdateParameter(dbCommand,"@dl_dob",hdeflood.dl_dob);
      
        Database.UpdateParameter(dbCommand,"@auth_code",hdeflood.auth_code);
      
        Database.UpdateParameter(dbCommand,"@t_arrival",hdeflood.t_arrival);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update hdeflood Set "
      
        + " hdeflood.cust_id = ? , "
      
        + " hdeflood.rejected = ? , "
      
        + " hdeflood.trans_num = ? , "
      
        + " hdeflood.csr_id = ? , "
      
        + " hdeflood.ad_source = ? , "
      
        + " hdeflood.alt_addr = ? , "
      
        + " hdeflood.tech_refer = ? , "
      
        + " hdeflood.d_1st_call = ? , "
      
        + " hdeflood.t_1st_call = ? , "
      
        + " hdeflood.d_schedule = ? , "
      
        + " hdeflood.t_schedule = ? , "
      
        + " hdeflood.d_dispatch = ? , "
      
        + " hdeflood.t_dispatch = ? , "
      
        + " hdeflood.d_complete = ? , "
      
        + " hdeflood.t_complete = ? , "
      
        + " hdeflood.tran_type = ? , "
      
        + " hdeflood.tran_stat = ? , "
      
        + " hdeflood.comp_type = ? , "
      
        + " hdeflood.canc_type = ? , "
      
        + " hdeflood.mop = ? , "
      
        + " hdeflood.amount = ? , "
      
        + " hdeflood.reschd_num = ? , "
      
        + " hdeflood.company = ? , "
      
        + " hdeflood.tech_id = ? , "
      
        + " hdeflood.b_person = ? , "
      
        + " hdeflood.l_person = ? , "
      
        + " hdeflood.c_person = ? , "
      
        + " hdeflood.note = ? , "
      
        + " hdeflood.special1 = ? , "
      
        + " hdeflood.src_flood = ? , "
      
        + " hdeflood.d_of_flood = ? , "
      
        + " hdeflood.rooms = ? , "
      
        + " hdeflood.ordered_by = ? , "
      
        + " hdeflood.i_agncy = ? , "
      
        + " hdeflood.i_agncy_ph = ? , "
      
        + " hdeflood.i_agent = ? , "
      
        + " hdeflood.i_carrier = ? , "
      
        + " hdeflood.i_adj = ? , "
      
        + " hdeflood.i_adj_ph = ? , "
      
        + " hdeflood.referred = ? , "
      
        + " hdeflood.grade = ? , "
      
        + " hdeflood.tax_perc = ? , "
      
        + " hdeflood.spec_id = ? , "
      
        + " hdeflood.sd_name = ? , "
      
        + " hdeflood.sd_amt = ? , "
      
        + " hdeflood.min_prc = ? , "
      
        + " hdeflood.mp_used = ? , "
      
        + " hdeflood.btid = ? , "
      
        + " hdeflood.companyid = ? , "
      
        + " hdeflood.comp_refer = ? , "
      
        + " hdeflood.callorigin = ? , "
      
        + " hdeflood.phase = ? , "
      
        + " hdeflood.repeat = ? , "
      
        + " hdeflood.cc_num = ? , "
      
        + " hdeflood.cc_expdate = ? , "
      
        + " hdeflood.dl_num = ? , "
      
        + " hdeflood.dl_dob = ? , "
      
        + " hdeflood.auth_code = ? , "
      
        + " hdeflood.t_arrival = ?  "
      
        + " Where "
        
          + " hdeflood.ticket_num = ?  "
        
      ;

      public static void Update(hdeflood hdeflood)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@cust_id", hdeflood.cust_id);
      
        Database.PutParameter(dbCommand,"@rejected", hdeflood.rejected);
      
        Database.PutParameter(dbCommand,"@trans_num", hdeflood.trans_num);
      
        Database.PutParameter(dbCommand,"@csr_id", hdeflood.csr_id);
      
        Database.PutParameter(dbCommand,"@ad_source", hdeflood.ad_source);
      
        Database.PutParameter(dbCommand,"@alt_addr", hdeflood.alt_addr);
      
        Database.PutParameter(dbCommand,"@tech_refer", hdeflood.tech_refer);
      
        Database.PutParameter(dbCommand,"@d_1st_call", hdeflood.d_1st_call);
      
        Database.PutParameter(dbCommand,"@t_1st_call", hdeflood.t_1st_call);
      
        Database.PutParameter(dbCommand,"@d_schedule", hdeflood.d_schedule);
      
        Database.PutParameter(dbCommand,"@t_schedule", hdeflood.t_schedule);
      
        Database.PutParameter(dbCommand,"@d_dispatch", hdeflood.d_dispatch);
      
        Database.PutParameter(dbCommand,"@t_dispatch", hdeflood.t_dispatch);
      
        Database.PutParameter(dbCommand,"@d_complete", hdeflood.d_complete);
      
        Database.PutParameter(dbCommand,"@t_complete", hdeflood.t_complete);
      
        Database.PutParameter(dbCommand,"@tran_type", hdeflood.tran_type);
      
        Database.PutParameter(dbCommand,"@tran_stat", hdeflood.tran_stat);
      
        Database.PutParameter(dbCommand,"@comp_type", hdeflood.comp_type);
      
        Database.PutParameter(dbCommand,"@canc_type", hdeflood.canc_type);
      
        Database.PutParameter(dbCommand,"@mop", hdeflood.mop);
      
        Database.PutParameter(dbCommand,"@amount", hdeflood.amount);
      
        Database.PutParameter(dbCommand,"@reschd_num", hdeflood.reschd_num);
      
        Database.PutParameter(dbCommand,"@company", hdeflood.company);
      
        Database.PutParameter(dbCommand,"@tech_id", hdeflood.tech_id);
      
        Database.PutParameter(dbCommand,"@b_person", hdeflood.b_person);
      
        Database.PutParameter(dbCommand,"@l_person", hdeflood.l_person);
      
        Database.PutParameter(dbCommand,"@c_person", hdeflood.c_person);
      
        Database.PutParameter(dbCommand,"@note", hdeflood.note);
      
        Database.PutParameter(dbCommand,"@special1", hdeflood.special1);
      
        Database.PutParameter(dbCommand,"@src_flood", hdeflood.src_flood);
      
        Database.PutParameter(dbCommand,"@d_of_flood", hdeflood.d_of_flood);
      
        Database.PutParameter(dbCommand,"@rooms", hdeflood.rooms);
      
        Database.PutParameter(dbCommand,"@ordered_by", hdeflood.ordered_by);
      
        Database.PutParameter(dbCommand,"@i_agncy", hdeflood.i_agncy);
      
        Database.PutParameter(dbCommand,"@i_agncy_ph", hdeflood.i_agncy_ph);
      
        Database.PutParameter(dbCommand,"@i_agent", hdeflood.i_agent);
      
        Database.PutParameter(dbCommand,"@i_carrier", hdeflood.i_carrier);
      
        Database.PutParameter(dbCommand,"@i_adj", hdeflood.i_adj);
      
        Database.PutParameter(dbCommand,"@i_adj_ph", hdeflood.i_adj_ph);
      
        Database.PutParameter(dbCommand,"@referred", hdeflood.referred);
      
        Database.PutParameter(dbCommand,"@grade", hdeflood.grade);
      
        Database.PutParameter(dbCommand,"@tax_perc", hdeflood.tax_perc);
      
        Database.PutParameter(dbCommand,"@spec_id", hdeflood.spec_id);
      
        Database.PutParameter(dbCommand,"@sd_name", hdeflood.sd_name);
      
        Database.PutParameter(dbCommand,"@sd_amt", hdeflood.sd_amt);
      
        Database.PutParameter(dbCommand,"@min_prc", hdeflood.min_prc);
      
        Database.PutParameter(dbCommand,"@mp_used", hdeflood.mp_used);
      
        Database.PutParameter(dbCommand,"@btid", hdeflood.btid);
      
        Database.PutParameter(dbCommand,"@companyid", hdeflood.companyid);
      
        Database.PutParameter(dbCommand,"@comp_refer", hdeflood.comp_refer);
      
        Database.PutParameter(dbCommand,"@callorigin", hdeflood.callorigin);
      
        Database.PutParameter(dbCommand,"@phase", hdeflood.phase);
      
        Database.PutParameter(dbCommand,"@repeat", hdeflood.repeat);
      
        Database.PutParameter(dbCommand,"@cc_num", hdeflood.cc_num);
      
        Database.PutParameter(dbCommand,"@cc_expdate", hdeflood.cc_expdate);
      
        Database.PutParameter(dbCommand,"@dl_num", hdeflood.dl_num);
      
        Database.PutParameter(dbCommand,"@dl_dob", hdeflood.dl_dob);
      
        Database.PutParameter(dbCommand,"@auth_code", hdeflood.auth_code);
      
        Database.PutParameter(dbCommand,"@t_arrival", hdeflood.t_arrival);
      
        Database.PutParameter(dbCommand,"@ticket_num", hdeflood.ticket_num);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " hdeflood.ticket_num, "
      
        + " hdeflood.cust_id, "
      
        + " hdeflood.rejected, "
      
        + " hdeflood.trans_num, "
      
        + " hdeflood.csr_id, "
      
        + " hdeflood.ad_source, "
      
        + " hdeflood.alt_addr, "
      
        + " hdeflood.tech_refer, "
      
        + " hdeflood.d_1st_call, "
      
        + " hdeflood.t_1st_call, "
      
        + " hdeflood.d_schedule, "
      
        + " hdeflood.t_schedule, "
      
        + " hdeflood.d_dispatch, "
      
        + " hdeflood.t_dispatch, "
      
        + " hdeflood.d_complete, "
      
        + " hdeflood.t_complete, "
      
        + " hdeflood.tran_type, "
      
        + " hdeflood.tran_stat, "
      
        + " hdeflood.comp_type, "
      
        + " hdeflood.canc_type, "
      
        + " hdeflood.mop, "
      
        + " hdeflood.amount, "
      
        + " hdeflood.reschd_num, "
      
        + " hdeflood.company, "
      
        + " hdeflood.tech_id, "
      
        + " hdeflood.b_person, "
      
        + " hdeflood.l_person, "
      
        + " hdeflood.c_person, "
      
        + " hdeflood.note, "
      
        + " hdeflood.special1, "
      
        + " hdeflood.src_flood, "
      
        + " hdeflood.d_of_flood, "
      
        + " hdeflood.rooms, "
      
        + " hdeflood.ordered_by, "
      
        + " hdeflood.i_agncy, "
      
        + " hdeflood.i_agncy_ph, "
      
        + " hdeflood.i_agent, "
      
        + " hdeflood.i_carrier, "
      
        + " hdeflood.i_adj, "
      
        + " hdeflood.i_adj_ph, "
      
        + " hdeflood.referred, "
      
        + " hdeflood.grade, "
      
        + " hdeflood.tax_perc, "
      
        + " hdeflood.spec_id, "
      
        + " hdeflood.sd_name, "
      
        + " hdeflood.sd_amt, "
      
        + " hdeflood.min_prc, "
      
        + " hdeflood.mp_used, "
      
        + " hdeflood.btid, "
      
        + " hdeflood.companyid, "
      
        + " hdeflood.comp_refer, "
      
        + " hdeflood.callorigin, "
      
        + " hdeflood.phase, "
      
        + " hdeflood.repeat, "
      
        + " hdeflood.cc_num, "
      
        + " hdeflood.cc_expdate, "
      
        + " hdeflood.dl_num, "
      
        + " hdeflood.dl_dob, "
      
        + " hdeflood.auth_code, "
      
        + " hdeflood.t_arrival "
      

      + " From hdeflood "

      
        + " Where "
        
          + " hdeflood.ticket_num = ?  "
        
      ;

      public static hdeflood FindByPrimaryKey(
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
      throw new DataNotFoundException("hdeflood not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(hdeflood hdeflood)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@ticket_num",hdeflood.ticket_num);
      

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
      String sql = "select 1 from hdeflood";

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

      public static hdeflood Load(IDataReader dataReader)
      {
      hdeflood hdeflood = new hdeflood();

      hdeflood.ticket_num = dataReader.GetString(0);
          hdeflood.cust_id = dataReader.GetString(1);
          hdeflood.rejected = dataReader.GetBoolean(2);
          hdeflood.trans_num = dataReader.GetString(3);
          hdeflood.csr_id = dataReader.GetString(4);
          hdeflood.ad_source = dataReader.GetString(5);
          hdeflood.alt_addr = dataReader.GetBoolean(6);
          hdeflood.tech_refer = dataReader.GetString(7);
          hdeflood.d_1st_call = dataReader.GetDateTime(8);
          hdeflood.t_1st_call = dataReader.GetString(9);
          hdeflood.d_schedule = dataReader.GetDateTime(10);
          hdeflood.t_schedule = dataReader.GetString(11);
          hdeflood.d_dispatch = dataReader.GetDateTime(12);
          hdeflood.t_dispatch = dataReader.GetString(13);
          hdeflood.d_complete = dataReader.GetDateTime(14);
          hdeflood.t_complete = dataReader.GetString(15);
          hdeflood.tran_type = dataReader.GetInt32(16);
          hdeflood.tran_stat = dataReader.GetInt32(17);
          hdeflood.comp_type = dataReader.GetInt32(18);
          hdeflood.canc_type = dataReader.GetInt32(19);
          hdeflood.mop = dataReader.GetInt32(20);
          hdeflood.amount = dataReader.GetFloat(21);
          hdeflood.reschd_num = dataReader.GetInt32(22);
          hdeflood.company = dataReader.GetString(23);
          hdeflood.tech_id = dataReader.GetString(24);
          hdeflood.b_person = dataReader.GetString(25);
          hdeflood.l_person = dataReader.GetString(26);
          hdeflood.c_person = dataReader.GetString(27);
          hdeflood.note = dataReader.GetString(28);
          hdeflood.special1 = dataReader.GetString(29);
          hdeflood.src_flood = dataReader.GetString(30);
          hdeflood.d_of_flood = dataReader.GetDateTime(31);
          hdeflood.rooms = dataReader.GetString(32);
          hdeflood.ordered_by = dataReader.GetString(33);
          hdeflood.i_agncy = dataReader.GetString(34);
          hdeflood.i_agncy_ph = dataReader.GetString(35);
          hdeflood.i_agent = dataReader.GetString(36);
          hdeflood.i_carrier = dataReader.GetString(37);
          hdeflood.i_adj = dataReader.GetString(38);
          hdeflood.i_adj_ph = dataReader.GetString(39);
          hdeflood.referred = dataReader.GetBoolean(40);
          hdeflood.grade = dataReader.GetInt32(41);
          hdeflood.tax_perc = dataReader.GetFloat(42);
          hdeflood.spec_id = dataReader.GetInt32(43);
          hdeflood.sd_name = dataReader.GetString(44);
          hdeflood.sd_amt = dataReader.GetFloat(45);
          hdeflood.min_prc = dataReader.GetFloat(46);
          hdeflood.mp_used = dataReader.GetBoolean(47);
          hdeflood.btid = dataReader.GetInt32(48);
          hdeflood.companyid = dataReader.GetInt32(49);
          hdeflood.comp_refer = dataReader.GetInt32(50);
          hdeflood.callorigin = dataReader.GetString(51);
          hdeflood.phase = dataReader.GetInt32(52);
          hdeflood.repeat = dataReader.GetBoolean(53);
          hdeflood.cc_num = dataReader.GetString(54);
          hdeflood.cc_expdate = dataReader.GetString(55);
          hdeflood.dl_num = dataReader.GetString(56);
          hdeflood.dl_dob = dataReader.GetDateTime(57);
          hdeflood.auth_code = dataReader.GetString(58);
          hdeflood.t_arrival = dataReader.GetString(59);
          

      return hdeflood;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [hdeflood] "

      
        + " Where "
        
          + " ticket_num = ?  "
        
      ;
      public static void Delete(hdeflood hdeflood)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      
        Database.PutParameter(dbCommand,"@ticket_num", hdeflood.ticket_num);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [hdeflood] ";

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

      
        + " hdeflood.ticket_num, "
      
        + " hdeflood.cust_id, "
      
        + " hdeflood.rejected, "
      
        + " hdeflood.trans_num, "
      
        + " hdeflood.csr_id, "
      
        + " hdeflood.ad_source, "
      
        + " hdeflood.alt_addr, "
      
        + " hdeflood.tech_refer, "
      
        + " hdeflood.d_1st_call, "
      
        + " hdeflood.t_1st_call, "
      
        + " hdeflood.d_schedule, "
      
        + " hdeflood.t_schedule, "
      
        + " hdeflood.d_dispatch, "
      
        + " hdeflood.t_dispatch, "
      
        + " hdeflood.d_complete, "
      
        + " hdeflood.t_complete, "
      
        + " hdeflood.tran_type, "
      
        + " hdeflood.tran_stat, "
      
        + " hdeflood.comp_type, "
      
        + " hdeflood.canc_type, "
      
        + " hdeflood.mop, "
      
        + " hdeflood.amount, "
      
        + " hdeflood.reschd_num, "
      
        + " hdeflood.company, "
      
        + " hdeflood.tech_id, "
      
        + " hdeflood.b_person, "
      
        + " hdeflood.l_person, "
      
        + " hdeflood.c_person, "
      
        + " hdeflood.note, "
      
        + " hdeflood.special1, "
      
        + " hdeflood.src_flood, "
      
        + " hdeflood.d_of_flood, "
      
        + " hdeflood.rooms, "
      
        + " hdeflood.ordered_by, "
      
        + " hdeflood.i_agncy, "
      
        + " hdeflood.i_agncy_ph, "
      
        + " hdeflood.i_agent, "
      
        + " hdeflood.i_carrier, "
      
        + " hdeflood.i_adj, "
      
        + " hdeflood.i_adj_ph, "
      
        + " hdeflood.referred, "
      
        + " hdeflood.grade, "
      
        + " hdeflood.tax_perc, "
      
        + " hdeflood.spec_id, "
      
        + " hdeflood.sd_name, "
      
        + " hdeflood.sd_amt, "
      
        + " hdeflood.min_prc, "
      
        + " hdeflood.mp_used, "
      
        + " hdeflood.btid, "
      
        + " hdeflood.companyid, "
      
        + " hdeflood.comp_refer, "
      
        + " hdeflood.callorigin, "
      
        + " hdeflood.phase, "
      
        + " hdeflood.repeat, "
      
        + " hdeflood.cc_num, "
      
        + " hdeflood.cc_expdate, "
      
        + " hdeflood.dl_num, "
      
        + " hdeflood.dl_dob, "
      
        + " hdeflood.auth_code, "
      
        + " hdeflood.t_arrival "
      

      + " From hdeflood ";
      public static List<hdeflood> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<hdeflood> rv = new List<hdeflood>();

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
      List<hdeflood> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<hdeflood> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(hdeflood));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(hdeflood item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<hdeflood>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(hdeflood));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<hdeflood> itemsList
      = new List<hdeflood>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is hdeflood)
      itemsList.Add(deserializedObject as hdeflood);
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
      
        protected int m_tran_type;
      
        protected int m_tran_stat;
      
        protected int m_comp_type;
      
        protected int m_canc_type;
      
        protected int m_mop;
      
        protected float m_amount;
      
        protected int m_reschd_num;
      
        protected String m_company;
      
        protected String m_tech_id;
      
        protected String m_b_person;
      
        protected String m_l_person;
      
        protected String m_c_person;
      
        protected String m_note;
      
        protected String m_special1;
      
        protected String m_src_flood;
      
        protected DateTime m_d_of_flood;
      
        protected String m_rooms;
      
        protected String m_ordered_by;
      
        protected String m_i_agncy;
      
        protected String m_i_agncy_ph;
      
        protected String m_i_agent;
      
        protected String m_i_carrier;
      
        protected String m_i_adj;
      
        protected String m_i_adj_ph;
      
        protected bool m_referred;
      
        protected int m_grade;
      
        protected float m_tax_perc;
      
        protected int m_spec_id;
      
        protected String m_sd_name;
      
        protected float m_sd_amt;
      
        protected float m_min_prc;
      
        protected bool m_mp_used;
      
        protected int m_btid;
      
        protected int m_companyid;
      
        protected int m_comp_refer;
      
        protected String m_callorigin;
      
        protected int m_phase;
      
        protected bool m_repeat;
      
        protected String m_cc_num;
      
        protected String m_cc_expdate;
      
        protected String m_dl_num;
      
        protected DateTime m_dl_dob;
      
        protected String m_auth_code;
      
        protected String m_t_arrival;
      
      #endregion

      #region Constructors
      public hdeflood(
      String 
          ticket_num
      )
      {
      
        m_ticket_num = ticket_num;
      
      }

      


        public hdeflood(
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
          t_complete,int 
          tran_type,int 
          tran_stat,int 
          comp_type,int 
          canc_type,int 
          mop,float 
          amount,int 
          reschd_num,String 
          company,String 
          tech_id,String 
          b_person,String 
          l_person,String 
          c_person,String 
          note,String 
          special1,String 
          src_flood,DateTime 
          d_of_flood,String 
          rooms,String 
          ordered_by,String 
          i_agncy,String 
          i_agncy_ph,String 
          i_agent,String 
          i_carrier,String 
          i_adj,String 
          i_adj_ph,bool 
          referred,int 
          grade,float 
          tax_perc,int 
          spec_id,String 
          sd_name,float 
          sd_amt,float 
          min_prc,bool 
          mp_used,int 
          btid,int 
          companyid,int 
          comp_refer,String 
          callorigin,int 
          phase,bool 
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
        
          m_src_flood = src_flood;
        
          m_d_of_flood = d_of_flood;
        
          m_rooms = rooms;
        
          m_ordered_by = ordered_by;
        
          m_i_agncy = i_agncy;
        
          m_i_agncy_ph = i_agncy_ph;
        
          m_i_agent = i_agent;
        
          m_i_carrier = i_carrier;
        
          m_i_adj = i_adj;
        
          m_i_adj_ph = i_adj_ph;
        
          m_referred = referred;
        
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
        
          m_callorigin = callorigin;
        
          m_phase = phase;
        
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
        public int tran_type
        {
        get { return m_tran_type;}
        set { m_tran_type = value; }
        }
      
        [XmlElement]
        public int tran_stat
        {
        get { return m_tran_stat;}
        set { m_tran_stat = value; }
        }
      
        [XmlElement]
        public int comp_type
        {
        get { return m_comp_type;}
        set { m_comp_type = value; }
        }
      
        [XmlElement]
        public int canc_type
        {
        get { return m_canc_type;}
        set { m_canc_type = value; }
        }
      
        [XmlElement]
        public int mop
        {
        get { return m_mop;}
        set { m_mop = value; }
        }
      
        [XmlElement]
        public float amount
        {
        get { return m_amount;}
        set { m_amount = value; }
        }
      
        [XmlElement]
        public int reschd_num
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
        public String src_flood
        {
        get { return m_src_flood;}
        set { m_src_flood = value; }
        }
      
        [XmlElement]
        public DateTime d_of_flood
        {
        get { return m_d_of_flood;}
        set { m_d_of_flood = value; }
        }
      
        [XmlElement]
        public String rooms
        {
        get { return m_rooms;}
        set { m_rooms = value; }
        }
      
        [XmlElement]
        public String ordered_by
        {
        get { return m_ordered_by;}
        set { m_ordered_by = value; }
        }
      
        [XmlElement]
        public String i_agncy
        {
        get { return m_i_agncy;}
        set { m_i_agncy = value; }
        }
      
        [XmlElement]
        public String i_agncy_ph
        {
        get { return m_i_agncy_ph;}
        set { m_i_agncy_ph = value; }
        }
      
        [XmlElement]
        public String i_agent
        {
        get { return m_i_agent;}
        set { m_i_agent = value; }
        }
      
        [XmlElement]
        public String i_carrier
        {
        get { return m_i_carrier;}
        set { m_i_carrier = value; }
        }
      
        [XmlElement]
        public String i_adj
        {
        get { return m_i_adj;}
        set { m_i_adj = value; }
        }
      
        [XmlElement]
        public String i_adj_ph
        {
        get { return m_i_adj_ph;}
        set { m_i_adj_ph = value; }
        }
      
        [XmlElement]
        public bool referred
        {
        get { return m_referred;}
        set { m_referred = value; }
        }
      
        [XmlElement]
        public int grade
        {
        get { return m_grade;}
        set { m_grade = value; }
        }
      
        [XmlElement]
        public float tax_perc
        {
        get { return m_tax_perc;}
        set { m_tax_perc = value; }
        }
      
        [XmlElement]
        public int spec_id
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
        public float sd_amt
        {
        get { return m_sd_amt;}
        set { m_sd_amt = value; }
        }
      
        [XmlElement]
        public float min_prc
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
        public int btid
        {
        get { return m_btid;}
        set { m_btid = value; }
        }
      
        [XmlElement]
        public int companyid
        {
        get { return m_companyid;}
        set { m_companyid = value; }
        }
      
        [XmlElement]
        public int comp_refer
        {
        get { return m_comp_refer;}
        set { m_comp_refer = value; }
        }
      
        [XmlElement]
        public String callorigin
        {
        get { return m_callorigin;}
        set { m_callorigin = value; }
        }
      
        [XmlElement]
        public int phase
        {
        get { return m_phase;}
        set { m_phase = value; }
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

    