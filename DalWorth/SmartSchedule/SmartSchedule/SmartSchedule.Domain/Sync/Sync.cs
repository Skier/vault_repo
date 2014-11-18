using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Text;
using System.Threading;
using SmartSchedule.Data;
using SmartSchedule.Domain.Servman;
using SmartSchedule.Domain.WCF;
using SmartSchedule.SDK;

namespace SmartSchedule.Domain.Sync
{
    public class Sync
    {
        public static VisitsChangeDetail Import(BookingEngine bookingEngine, List<Order> orders)
        {
            InitTechnicians(orders);

            VisitsChangeDetail result = new VisitsChangeDetail();

            if (!ApplicationSetting.IsContainsData())
            {
                ApplicationSetting setting = new ApplicationSetting(DateTime.Now, string.Empty, DateTime.MinValue);
                ApplicationSetting.Insert(setting);
            }            

            Dictionary<string, Visit> ticketToVisitMap = new Dictionary<string, Visit>();
            foreach (Visit visit in bookingEngine.Visits)
            {
                if (!visit.IsBlockout)
                    ticketToVisitMap.Add(visit.TicketNumber, visit);
            }
                
            foreach (Visit visit in Visit.FindDelayedVisits(null))
                ticketToVisitMap.Add(visit.TicketNumber, visit);
                
            List<string> satisfyQueryTickets = new List<string>();
            List<DateTime> affectedDates = new List<DateTime>();

            foreach (Order order in orders)
            {
                satisfyQueryTickets.Add(order.TicketNumber);

                if (ticketToVisitMap.ContainsKey(order.TicketNumber))
                {
                    Visit existingVisit = ticketToVisitMap[order.TicketNumber];
                    OrderChangeTypeEnum changeType = order.GetOrderChangeType(existingVisit);

                    if (changeType == OrderChangeTypeEnum.Change)
                    {
                        existingVisit.CustomerName = order.Cutomer;
                        existingVisit.Street = order.Street;
                        existingVisit.Address2 = order.Address2;
                        existingVisit.City = order.City;
                        existingVisit.State = order.State;
                        existingVisit.CallDateTime = order.CallDateTime;

                        existingVisit.Area = order.AreaId;
                        existingVisit.ServType = order.ServType;
                        existingVisit.AdsourceAcronym = order.AdsourceAcronym;
                        existingVisit.CustomerRank = order.CustomerRank;
                        existingVisit.OriginatedTechnicianDefaultId = order.OriginatedTechnicianDefaultId;
                        existingVisit.OriginatedCompleteDate = order.OriginatedCompleteDate;
                        existingVisit.OriginatedTicketNumber = order.OriginatedTicketNumber;
                        existingVisit.CustomerExclusiveTechnicianDefaultId = order.CustomerExclusiveTechnicianDefaultId;
                        existingVisit.Note = order.Note;
                        existingVisit.ExpCred = order.ExpCred;
                        existingVisit.SpecName = order.SpecName;
                        existingVisit.SdPercent = order.SdPercent;
                        existingVisit.TaxPercent = order.TaxPercent;

                        Visit.Update(existingVisit);
                        if (bookingEngine.Visits.IndexOf(existingVisit) >= 0)
                            bookingEngine.Visits.ResetItem(bookingEngine.Visits.IndexOf(existingVisit));
                        result.AddModifiedVisit(existingVisit);
                    }
                    else if (changeType == OrderChangeTypeEnum.MustRescheduleChange)
                    {
                        order.Geocode();
                        Visit newVisit = bookingEngine.GetNewVisit(order, existingVisit);

                        newVisit.Details = new List<VisitDetail>();
                        foreach (OrderDetail detail in order.OrderDetails)
                        {
                            VisitDetail visitDetail = new VisitDetail(0, 0, detail.ServType,
                                detail.ItemNumber, detail.Note.Trim(), detail.Amount);
                            newVisit.Details.Add(visitDetail);
                        }

                        if (bookingEngine.Visits.Contains(existingVisit))
                        {
                            bookingEngine.RemoveVisit(existingVisit, false, User.Sync);
                            result.AddRemovedVisit(existingVisit);

                            VisitInsertResult insertResult = bookingEngine.InsertVisit(newVisit);
                            if (insertResult.IsInsertSucceed)
                            {
                                result.AddAddedVisit(newVisit);

                                foreach (var modifiedVisit in insertResult.ModifiedVisits)
                                    result.AddModifiedVisit(modifiedVisit);
                            }

                            insertResult.UserAction.User = User.Sync;
                            UserAction.Insert(insertResult.UserAction);
                        }
                        else
                        {
                            VisitDetail.DeleteByVisit(existingVisit);

                            newVisit.TechnicianId = null;
                            newVisit.ID = existingVisit.ID;
                            Visit.Update(newVisit);

                            foreach (VisitDetail detail in newVisit.Details)
                            {
                                detail.VisitId = newVisit.ID;
                                VisitDetail.Insert(detail);
                            }                            
                        }

                        if (!affectedDates.Contains(existingVisit.ScheduleDate))
                            affectedDates.Add(existingVisit.ScheduleDate);
                        if (!affectedDates.Contains(newVisit.ScheduleDate))
                            affectedDates.Add(newVisit.ScheduleDate);

                        ticketToVisitMap[newVisit.TicketNumber] = newVisit;
                    }
                }
                else
                {
                    order.Geocode();
                    Visit newVisit = bookingEngine.GetNewVisit(order, null);
                    VisitInsertResult insertResult = bookingEngine.InsertVisit(newVisit);
                    if (insertResult.IsInsertSucceed)
                    {
                        result.AddAddedVisit(newVisit);
                        foreach (var modifiedVisit in insertResult.ModifiedVisits)
                            result.AddModifiedVisit(modifiedVisit);
                    }

                    insertResult.UserAction.User = User.Sync;
                    UserAction.Insert(insertResult.UserAction);

                    ticketToVisitMap.Add(newVisit.TicketNumber, newVisit);

                    if (!affectedDates.Contains(newVisit.ScheduleDate))
                        affectedDates.Add(newVisit.ScheduleDate);
                }
            }

            if (ticketToVisitMap.Count != satisfyQueryTickets.Count)
            {
                List<string> existingTickets = new List<string>(ticketToVisitMap.Keys);
                foreach (string ticketNumber in existingTickets)
                {
                    if (!satisfyQueryTickets.Contains(ticketNumber)) // delete visit
                    {
                        Visit existingVisit = ticketToVisitMap[ticketNumber];

                        if (bookingEngine.Visits.Contains(existingVisit))
                        {
                            List<Visit> modifiedVisits = bookingEngine.RemoveVisit(existingVisit, false, User.Sync);

                            if (existingVisit.TechnicianId.HasValue)
                                result.AddRemovedVisit(existingVisit);

                            foreach (var modifiedVisit in modifiedVisits)
                                result.AddModifiedVisit(modifiedVisit);

                        }
                        else
                        {
                            Visit.DeleteWithDetails(existingVisit);
                            UserAction.WriteAction(User.Sync, UserActionTypeEnum.Visit, existingVisit,
                                string.Format("Visit {0} removed from bucket", existingVisit.TicketNumber));
                        }                            

                        if (!affectedDates.Contains(existingVisit.ScheduleDate))
                            affectedDates.Add(existingVisit.ScheduleDate);

                        ticketToVisitMap.Remove(ticketNumber);
                    }
                }
            }

            foreach (var affectedDate in affectedDates)
                bookingEngine.RefreshBucket(affectedDate);

            result.AffectedDates = affectedDates;
            foreach (var modifiedVisit in result.DashboardModifiedVisits)
            {
                if (!result.AffectedDates.Contains(modifiedVisit.ScheduleDate))
                    result.AffectedDates.Add(modifiedVisit.ScheduleDate);
            }

            TicketsCheck(orders);
            return result;
        }

        #region InitTechnicians

        private static void InitTechnicians(List<Order> orders)
        {
            foreach (Order order in orders)
            {
                if (!string.IsNullOrEmpty(order.ExclusiveTechnicianDefaultServmanId))
                {
                    try
                    {
                        order.ExclusiveTechnicianDefault = Technician.GetTechnician(
                            order.ExclusiveTechnicianDefaultServmanId);
                    }
                    catch (Exception)
                    {
                        Host.Trace("Sync.Import", string.Format("Exclusivity ignored. Ticket {0}, ServmanTechId {1}",
                            order.TicketNumber, order.ExclusiveTechnicianDefaultServmanId));
                    }
                }

                if (!string.IsNullOrEmpty(order.OriginatedTechnicianServmanId))
                {
                    try
                    {
                        order.OriginatedTechnicianDefaultId = Technician.GetTechnician(
                            order.OriginatedTechnicianServmanId).ID;
                    }
                    catch (Exception)
                    {
                        Host.Trace("Sync.Import", string.Format("Originated technician ignored. Ticket {0}, ServmanTechId {1}",
                            order.TicketNumber, order.OriginatedTechnicianServmanId));
                    }
                }

                if (!string.IsNullOrEmpty(order.CustomerExclusiveTechnicianServmanId))
                {
                    try
                    {
                        order.CustomerExclusiveTechnicianDefaultId = Technician.GetTechnician(
                            order.CustomerExclusiveTechnicianServmanId).ID;
                    }
                    catch (Exception)
                    {
                        Host.Trace("Sync.Import", string.Format("Customer Exclusive Technician ignored. Ticket {0}, ServmanTechId {1}",
                            order.TicketNumber, order.CustomerExclusiveTechnicianServmanId));
                    }
                }
            }            
        }

        #endregion

        #region FindImportOrders

        private const string SqlFindImportOrders =
            @"SELECT Hresi_cc.ticket_num, dresi_cc.serv_type, dresi_cc.Item_num, dresi_cc.Note, dresi_cc.Amount,
                custmast.customer, custmast.block, 
                custmast.prefix, custmast.street, custmast.suffix, custmast.city, 
                custmast.state, h_order.zip, hresi_cc.d_schedule, hresi_cc.t_schedule, 
                hresi_cc.Amount, hresi_cc.tech_refer, hresi_cc.d_1st_call, 
                hresi_cc.t_1st_call, hresi_cc.tran_type, hresi_cc.spec_id, 
                TRIM(h_order.Page) + TRIM(h_order.Grid), hresi2.tech_id, 
                custmast.home_phone, custmast.bus_phone, TRIM(custmast.Unit) + ' ' + TRIM(custmast.Address2),
                h_order.area_id, h_order.serv_type, ad_src.Acronym, custmast.Rank, hresi2.D_complete,
                hresi2.Ticket_num, contmast.Prime_tech, hresi_cc.Note, h_order.Exp_cred, spec_hd.Name,
                hresi_cc.Sd_amt, hresi_cc.Tax_perc
             FROM hresi_cc 
                INNER JOIN custmast ON  Custmast.cust_id = Hresi_cc.cust_id 
                INNER JOIN h_order ON  H_order.ticket_num = Hresi_cc.ticket_num 
                LEFT OUTER JOIN ad_src ON hresi_cc.Ad_source = ad_src.Id_code
                LEFT OUTER JOIN contmast ON Custmast.Excl_cont = contmast.Cont_id
                LEFT OUTER JOIN spec_hd ON hresi_cc.Spec_id = spec_hd.Spec_id
                LEFT OUTER JOIN h_reject ON  Hresi_cc.ticket_num = H_reject.reject_tkt 
                LEFT OUTER JOIN hresi_cc Hresi2 ON  H_reject.ticket_num = Hresi2.ticket_num
                LEFT OUTER JOIN dresi_cc ON dresi_cc.ticket_num = Hresi_cc.ticket_num                
             WHERE  
                Hresi_cc.d_schedule >= ? and Hresi_cc.d_schedule <= ? 
                AND  Hresi_cc.tran_stat = 1
                order by Hresi_cc.ticket_num";

//        private const string SqlFindImportOrders =
//            @"select * from prodquery";

        public static List<Order> FindImportOrders()
        {
            List<Order> result = new List<Order>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindImportOrders, ConnectionKeyEnum.Servman))
            {
                if (Configuration.IsRealtimeMode)
                {
                    Database.PutParameter(dbCommand, "@d_schedule_start", DateTime.Now.Date);
                    Database.PutParameter(dbCommand, "@d_schedule_end", DateTime.Now.AddMonths(4).Date);                    
                }
                else
                {
                    Database.PutParameter(dbCommand, "@d_schedule_start", new DateTime(2009, 7, 10));
                    Database.PutParameter(dbCommand, "@d_schedule_end", new DateTime(2009, 7, 18));
                }

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    Order order = null;

                    while (dataReader.Read())
                    {
                        string ticketNumber = dataReader.GetString(0);

                        if (order == null || order.TicketNumber != ticketNumber)
                        {
                            if (order != null)
                                result.Add(order);

                            order = new Order(
                                ticketNumber,
                                dataReader.GetString(5),
                                dataReader.GetString(6),
                                dataReader.GetString(7),
                                dataReader.GetString(8),
                                dataReader.GetString(9),
                                dataReader.GetString(10),
                                dataReader.GetString(11),
                                dataReader.GetString(12),
                                dataReader.GetDateTime(13),
                                dataReader.GetString(14),
                                dataReader.GetDecimal(15),
                                dataReader.GetString(16),
                                dataReader.GetDateTime(17),
                                dataReader.GetString(18),
                                dataReader.GetInt32(19),
                                dataReader.GetInt32(20),                                
                                dataReader.GetString(21),                                
                                dataReader.IsDBNull(22) ? string.Empty : dataReader.GetString(22),
                                dataReader.GetString(23),
                                dataReader.GetString(24),
                                dataReader.GetString(25),
                                dataReader.GetString(26),
                                dataReader.GetInt32(27),
                                dataReader.IsDBNull(28) ? string.Empty : dataReader.GetString(28),
                                dataReader.GetInt32(29),
                                dataReader.IsDBNull(30) ? (DateTime?)null : dataReader.GetDateTime(30),
                                dataReader.IsDBNull(31) ? string.Empty : dataReader.GetString(31),
                                dataReader.IsDBNull(32) ? string.Empty : dataReader.GetString(32),
                                dataReader.GetString(33),
                                dataReader.GetBoolean(34),
                                dataReader.IsDBNull(35) ? string.Empty : dataReader.GetString(35),
                                dataReader.GetDecimal(36),
                                dataReader.GetDecimal(37));
                            
                            order.OrderDetails = new List<OrderDetail>();
                        } 

                        if (!dataReader.IsDBNull(1))
                        {                            
                            order.OrderDetails.Add(
                                new OrderDetail(int.Parse(dataReader.GetString(1)),
                                (int)dataReader.GetDecimal(2),
                                dataReader.GetString(3),
                                dataReader.GetDecimal(4)));
                        }
                            
                    }

                    if (order != null)
                        result.Add(order);    
                }
            }

            result.Sort(delegate(Order x, Order y)
                        {
                            decimal costX = x.Cost;
                            decimal costY = y.Cost;

                            if (x.ExclusiveTechnicianDefaultId != null)
                                costX += 100000;

                            if (y.ExclusiveTechnicianDefaultId != null)
                                costY += 100000;

                            return costY.CompareTo(costX);
                        });


            return result;
            
        }

        #endregion

        #region TicketsCheck

        private static void TicketsCheck(List<Order> servmanQueriedTickets)
        {
            List<Visit> systemVisits = Visit.Find();
            if (servmanQueriedTickets.Count == systemVisits.Count)
                return;

            List<string> systemTicketNumbers = new List<string>();
            foreach (var systemVisit in systemVisits)
                systemTicketNumbers.Add(systemVisit.TicketNumber);

            bool isMissingTickets = false;
            foreach (var servmanTicket in servmanQueriedTickets)
            {
                if (!systemTicketNumbers.Contains(servmanTicket.TicketNumber))
                {
                    Host.Trace("SmartScheduleServmanSync ERROR",
                        string.Format("System should contain {0} after sync, but it doesn't", servmanTicket.TicketNumber));
                    isMissingTickets = true;
                }
            }

            if (isMissingTickets)
                EmailSender.SendErrorNotifications();
        }

        #endregion

        #region FindTicketNumbersCheck

        private const string SqlFindTicketNumbersCheck =
            @"SELECT ticket_num from hresi_cc
                WHERE  
              Hresi_cc.d_schedule >= ? and Hresi_cc.d_schedule <= ? 
              AND  Hresi_cc.tran_stat = 1";

//        private const string SqlFindTicketNumbersCheck =
//            @"SELECT ticket_num from hresiquery where d_schedule >= ? and d_schedule <= ?";

        public static List<string> FindTicketNumbersCheck()
        {
            List<string> result = new List<string>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindTicketNumbersCheck, ConnectionKeyEnum.Servman))
            {
                if (Configuration.IsRealtimeMode)
                {
                    Database.PutParameter(dbCommand, "@d_schedule_start", DateTime.Now.Date);
                    Database.PutParameter(dbCommand, "@d_schedule_end", DateTime.Now.AddMonths(4).Date);
                }
                else
                {
                    Database.PutParameter(dbCommand, "@d_schedule_start", new DateTime(2009, 7, 10));
                    Database.PutParameter(dbCommand, "@d_schedule_end", new DateTime(2009, 7, 18));
                }

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(dataReader.GetString(0));
                }
            }

            return result;
        }

        #endregion

        #region FindTechniciansWorkHoursDayPreset

        private const string SqlFindTechniciansWorkHoursDayPreset =
            @"select tech_id, Date, promise from techschd
                WHERE Date >= DATE()";

        public static Dictionary<string, Dictionary<DateTime, int>> FindTechniciansWorkHoursDayPreset()
        {
            Dictionary<string, Dictionary<DateTime, int>> result 
                = new Dictionary<string, Dictionary<DateTime, int>>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindTechniciansWorkHoursDayPreset, ConnectionKeyEnum.Servman))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        string techId = dataReader.GetString(0);
                        DateTime date = dataReader.GetDateTime(1);
                        int promise = dataReader.GetInt32(2);

                        if (!result.ContainsKey(techId))
                            result.Add(techId, new Dictionary<DateTime, int>());

                        if (!result[techId].ContainsKey(date))
                            result[techId].Add(date, promise);
                    }
                }
            }

            return result;
        }

        #endregion
    }
}
