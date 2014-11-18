using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Server.Data;
using Dalworth.Server.Servman.Domain;
using Dalworth.Server.Servman.Domain.intermediate;

namespace Dalworth.Server.Domain.ServmanSync
{
    public class ServmanExportModel
    {
        #region InsertCustomer

        public static string InsertCustomer(Servman.Domain.intermediate.Customer customer)
        {
            custmast custmast = customer.Export(null);
            custmast.cust_id = CustomerCounter.FindNextCustomerIdAndIncrement();
            custmast.Insert(custmast);
            return custmast.cust_id;
        }

        #endregion

        #region UpdateCustomer

        public static void UpdateCustomer(Servman.Domain.intermediate.Customer customer, custmast servmanCustomer)
        {
            custmast custmast = customer.Export(servmanCustomer);
            custmast.Update(custmast);
        }

        #endregion

        #region InsertOrder

        public static string InsertOrder(Servman.Domain.intermediate.Order order)
        {
            OrderExport orderExport = order.Export();

            orderExport.H_order.cust_id = order.Customer.ID;
            orderExport.H_order.ticket_num = TicketCounter.FindNextOrderNumberAndIncrement();
            h_order.Insert(orderExport.H_order);            
            order.TicketNumber = orderExport.H_order.ticket_num;

            if (orderExport.M_alt_ad != null)
            {
                orderExport.M_alt_ad.ticket_num = order.TicketNumber;
                m_alt_ad.Insert(orderExport.M_alt_ad);
            }

            orderExport.Hdeflood.ticket_num = order.TicketNumber;
            orderExport.Hdeflood.cust_id = order.Customer.ID;
            orderExport.Hdeflood.callorigin = "R";
            hdeflood.Insert(orderExport.Hdeflood);

            orderExport.Ddeflood.ticket_num = order.TicketNumber;
            ddeflood.Insert(orderExport.Ddeflood);

            //no idea about techschd

            if (orderExport.Disp_que != null)
            {
                orderExport.Disp_que.ticket_num = order.TicketNumber;
                disp_que.Insert(orderExport.Disp_que);                    
            }

            if (order.TechnicianId != string.Empty)
                UpdateTechnicianSchedule(order.TechnicianId, order.DateSchedule, order.TruckId);

            return order.TicketNumber;
        }

        #endregion

        #region UpdateOrder

        public static void UpdateOrder(Servman.Domain.intermediate.Order order)
        {
            OrderExport orderExport = order.Export();

            h_order originalOrder = null;
            try
            {
                originalOrder = h_order.FindByPrimaryKey(order.TicketNumber);
                if (order.OrderStatus != OrderStatusEnum.Completed)
                    orderExport.H_order.time = originalOrder.time; //keep original order time on update
            }
            catch (DataNotFoundException) { }
            h_order.Update(orderExport.H_order);


            //Alternative address
            m_alt_ad existingAltAddress = null;
            try
            {
                existingAltAddress = m_alt_ad.FindByPrimaryKey(order.TicketNumber);
            }
            catch (DataNotFoundException){}

            if (orderExport.M_alt_ad == null)
            {
                if (existingAltAddress != null)
                    m_alt_ad.Delete(existingAltAddress);
            } else
            {
                if (existingAltAddress == null)
                    m_alt_ad.Insert(orderExport.M_alt_ad);
                else
                    m_alt_ad.Update(orderExport.M_alt_ad);                
            }

            //hdeflood
            hdeflood existingHDeflood = null;
            try
            {
                existingHDeflood = hdeflood.FindByPrimaryKey(order.TicketNumber);
                orderExport.Hdeflood.csr_id = existingHDeflood.csr_id;
                orderExport.Hdeflood.b_person = existingHDeflood.b_person;
                orderExport.Hdeflood.t_schedule = existingHDeflood.t_schedule;
                orderExport.Hdeflood.callorigin = existingHDeflood.callorigin;
            }
            catch (DataNotFoundException) {}

            if (existingHDeflood != null && existingHDeflood.d_schedule.Date != order.DateSchedule.Date)
                orderExport.Hdeflood.reschd_num = existingHDeflood.reschd_num + 1;            
            hdeflood.Update(orderExport.Hdeflood);


            //ddeflood
            ddeflood.Update(orderExport.Ddeflood);

            

            //disp_que
            disp_que existingQue = null;
            try
            {
                existingQue = disp_que.FindByPrimaryKey(order.TicketNumber);
                string oldTruckId = string.Empty;
                try
                {
                    Host.Trace("techschd.FindByPrimaryKey", existingQue.tech_id + ", " + existingQue.d_dispatch);
                    techschd oldSchedule = techschd.FindByPrimaryKey(existingQue.tech_id, existingQue.d_dispatch);
                    oldTruckId = oldSchedule.truck_id;
                }
                catch (DataNotFoundException){}

                UpdateTechnicianSchedule(existingQue.tech_id, existingQue.d_dispatch, oldTruckId);
            }
            catch (DataNotFoundException) { }

            if (orderExport.Disp_que == null)
            {
                if (existingQue != null)
                    disp_que.Delete(existingQue);
            }
            else
            {
                if (existingQue == null)
                    disp_que.Insert(orderExport.Disp_que);
                else
                    disp_que.Update(orderExport.Disp_que);
            }

            UpdateTechnicianSchedule(order.TechnicianId, order.DateSchedule, order.TruckId);
        }

        #endregion

        #region UpdateTechnicianSchedule

        private static void UpdateTechnicianSchedule(string techId, DateTime date, string truckId)
        {
            try
            {
                Host.Trace("techschd.FindByPrimaryKey", techId + ", " + date.Date);
                techschd existingSchedule = techschd.FindByPrimaryKey(techId, date.Date);

                if (truckId == string.Empty)
                    techschd.Delete(existingSchedule);
                else
                {
                    existingSchedule.truck_id = truckId;
                    UpdateTechnicianScheduleCalcFields(existingSchedule);
                    techschd.Update(existingSchedule);
                }
            }
            catch (DataNotFoundException)
            {
                if (truckId != string.Empty)
                {
                    techschd newSchedule = new techschd();
                    newSchedule.tech_id = techId;
                    newSchedule.date = date.Date;
                    newSchedule.truck_id = truckId;
                    newSchedule.truck_num = string.Empty;
                    newSchedule.note = string.Empty;
                    newSchedule.disp_id = string.Empty;
                    UpdateTechnicianScheduleCalcFields(newSchedule);
                    techschd.Insert(newSchedule);
                }
            }
        }

        private static void UpdateTechnicianScheduleCalcFields(techschd schedule)
        {
            List<disp_que> ques = disp_que.FindByTechAndDate(schedule.tech_id, schedule.date);

            schedule.amt_day = 0;
            schedule.done = 0;

            foreach (disp_que que in ques)
            {
                if (que.comp_type == 2) //Normally completed
                {
                    schedule.amt_day += que.amount;
                    schedule.done += 1;
                }
            }
        }

        #endregion

        #region DeleteOrder

        public static void DeleteOrder(string ticketNumber)
        {
            h_order.Delete(new h_order(ticketNumber));
            hdeflood.Delete(new hdeflood(ticketNumber));
            ddeflood.DeleteByTicket(ticketNumber);
            disp_que.Delete(new disp_que(ticketNumber));
        }

        #endregion
    }
}
