using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Server.Data;

namespace Dalworth.Server.Servman.Domain.intermediate
{
    public enum OrderTypeEnum
    {
        Deflood,
        Monitoring, //Inventory pickup
        RugPickup,
        RugDelivery
    }    
    
    public enum OrderStatusEnum
    {
        Pending = 1,
        Assigned = 2,
        Dispatched = 3,
        CallbackNeeded = 4,
        Completed = 5,
        InAccounting = 6,
        AccountingClosed = 7,
        Cancelled,
    }

    public class Order
    {
        #region Customer

        private Customer m_customer;
        public Customer Customer
        {
            get { return m_customer; }
            set { m_customer = value; }
        }

        #endregion

        #region AlternativeAddress

        private Address m_alternativeAddress;
        public Address AlternativeAddress
        {
            get { return m_alternativeAddress; }
            set { m_alternativeAddress = value; }
        }

        #endregion

        #region DefloodInfo

        private Deflood m_defloodInfo;
        public Deflood DefloodInfo
        {
            get { return m_defloodInfo; }
            set { m_defloodInfo = value; }
        }

        #endregion

        #region OrderType

        private OrderTypeEnum m_orderType;
        public OrderTypeEnum OrderType
        {
            get { return m_orderType; }
            set { m_orderType = value; }
        }

        #endregion

        #region OrderStatus

        private OrderStatusEnum m_orderStatus;
        public OrderStatusEnum OrderStatus
        {
            get { return m_orderStatus; }
            set { m_orderStatus = value; }
        }

        #endregion


        #region TicketNumber

        private string m_ticketNumber;
        //ID
        public string TicketNumber
        {
            get { return (m_ticketNumber ?? string.Empty).ToUpper(); }
            set { m_ticketNumber = value; }
        }

        #endregion

        #region ContactName

        private string m_contactName;
        public string ContactName
        {
            get { return (m_contactName ?? string.Empty).ToUpper(); }
            set { m_contactName = value; }
        }

        #endregion

        #region ServiceDate

        private DateTime m_serviceDate;
        public DateTime ServiceDate
        {
            get { return m_serviceDate == DateTime.MinValue ? Utils.SERVMAN_NULL_DATE : m_serviceDate; }
            set { m_serviceDate = value; }
        }

        #endregion

        #region TimeSchedule

        private string m_timeSchedule;
        public string TimeSchedule
        {
            get { return (m_timeSchedule ?? string.Empty).ToUpper(); }
            set { m_timeSchedule = value; }
        }

        #endregion

        #region TechnicianId

        private string m_technicianId;
        public string TechnicianId
        {
            get
            {
                if (OrderType == OrderTypeEnum.RugDelivery && m_technicianId != null && m_technicianId != string.Empty)
                    return "519"; //Shane Hobbs
                else
                    return m_technicianId ?? string.Empty;
            }
            set { m_technicianId = value; }
        }

        #endregion

        #region TruckId

        private string m_truckId;
        public string TruckId
        {
            get
            {
                return m_truckId ?? string.Empty;
            }
            set { m_truckId = value; }
        }

        #endregion

        #region ClosedAmount

        private decimal m_closedAmount;
        public decimal ClosedAmount
        {
            get { return m_closedAmount; }
            set { m_closedAmount = value; }
        }

        #endregion       

        #region AdvertisingSourceId

        private string m_advertisingSourceId;
        public string AdvertisingSourceId
        {
            get { return (m_advertisingSourceId ?? string.Empty).ToUpper(); }
            set { m_advertisingSourceId = value; }
        }

        #endregion

        #region AdvertisingTechnicianReferenceId

        private string m_advertisingTechnicianReferenceId;
        public string AdvertisingTechnicianReferenceId
        {
            get { return (m_advertisingTechnicianReferenceId ?? string.Empty).ToUpper(); }
            set { m_advertisingTechnicianReferenceId = value; }
        }

        #endregion

        #region DateTimeFirstCall

        private DateTime m_dateTimeFirstCall;
        public DateTime DateTimeFirstCall
        {
            get { return m_dateTimeFirstCall == DateTime.MinValue ? Utils.SERVMAN_NULL_DATE : m_dateTimeFirstCall; }
            set { m_dateTimeFirstCall = value; }
        }

        #endregion

        #region DateSchedule

        private DateTime m_dateSchedule;
        public DateTime DateSchedule
        {
            get { return m_dateSchedule == DateTime.MinValue ? Utils.SERVMAN_NULL_DATE : m_dateSchedule; }
            set { m_dateSchedule = value; }
        }

        #endregion

        #region DateTimeDispatch

        private DateTime m_dateTimeDispatch;
        public DateTime DateTimeDispatch
        {
            get 
            {
                if (IsRugPickupCompletion)
                    return Utils.SERVMAN_NULL_DATE;
                return m_dateTimeDispatch == DateTime.MinValue ? Utils.SERVMAN_NULL_DATE : m_dateTimeDispatch; 
            }
            set { m_dateTimeDispatch = value; }
        }

        #endregion

        #region DateTimeCompleted

        private DateTime m_dateTimeCompleted;
        public DateTime DateTimeCompleted
        {
            get { return m_dateTimeCompleted == DateTime.MinValue ? Utils.SERVMAN_NULL_DATE : m_dateTimeCompleted; }
            set { m_dateTimeCompleted = value; }
        }

        #endregion

        #region DateTimeArrived

        private DateTime m_dateTimeArrived;
        public DateTime DateTimeArrived
        {
            get { return m_dateTimeArrived == DateTime.MinValue ? Utils.SERVMAN_NULL_DATE : m_dateTimeArrived; }
            set { m_dateTimeArrived = value; }
        }

        #endregion

        #region PrintedNote

        private string m_printedNote;
        public string PrintedNote
        {
            get { return (m_printedNote ?? string.Empty).ToUpper(); }
            set { m_printedNote = value; }
        }

        #endregion

        #region TimeEstimateComplete

        private DateTime m_timeEstimateComplete;
        public DateTime TimeEstimateComplete
        {
            get { return m_timeEstimateComplete == DateTime.MinValue ? Utils.SERVMAN_NULL_DATE : m_timeEstimateComplete; }
            set { m_timeEstimateComplete = value; }
        }

        #endregion

        #region IsConfirmed

        private bool m_isConfirmed;
        public bool IsConfirmed
        {
            get { return m_isConfirmed; }
            set { m_isConfirmed = value; }
        }

        #endregion

        #region DispatchNote

        private string m_dispatchNote;
        public string DispatchNote
        {
            get { return (m_dispatchNote ?? string.Empty).ToUpper(); }
            set { m_dispatchNote = value; }
        }

        #endregion

        #region PerformedByUser

        private string m_performedByUser;
        public string PerformedByUser
        {
            get { return (m_performedByUser ?? string.Empty).ToUpper(); }
            set { m_performedByUser = value; }
        }

        #endregion

        #region BookedByText

        private string m_bookedByText;
        public string BookedByText
        {
            get { return (m_bookedByText ?? string.Empty).ToUpper(); }
            set { m_bookedByText = value; }
        }

        #endregion

        #region OriginateTicketNumber

        private string m_originateTicketNumber;
        public string OriginateTicketNumber
        {
            get { return (m_originateTicketNumber ?? string.Empty).ToUpper(); }
            set { m_originateTicketNumber = value; }
        }

        #endregion


        #region CurrentAddress

        public Address CurrentAddress
        {
            get
            {
                if (AlternativeAddress == null)
                    return Customer.Address;
                return AlternativeAddress;
            }
        }

        #endregion

        #region IsRugPickupCompletion

        public bool IsRugPickupCompletion
        {
            get
            {
                return OrderType == OrderTypeEnum.RugPickup && OrderStatus == OrderStatusEnum.Completed;
            }
        }

        #endregion

        #region GetTimeString

        private string GetTimeString(DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue || dateTime == Utils.SERVMAN_NULL_DATE)
                return string.Empty;
            return dateTime.Hour.ToString("00") + dateTime.Minute.ToString("00");
        }

        #endregion



        #region Export

        public OrderExport Export()
        {
            OrderExport orderExport = new OrderExport();
            orderExport.H_order = GetHOrder();
            orderExport.M_alt_ad = GetAltAddress();
            orderExport.Hdeflood = GetHDeflood();
            orderExport.Ddeflood = GetDDeflood();
            orderExport.Disp_que = GetDispQue();
            return orderExport;
        }

        #endregion

        #region GetHOrder

        private h_order GetHOrder()
        {
            h_order h_order = new h_order();

            h_order.ticket_num = TicketNumber;
            h_order.cust_id = Customer.ID;
            h_order.alt_addr = AlternativeAddress != null;
            h_order.contact = ContactName;
            if (IsRugPickupCompletion)
                ServiceDate = ServiceDate.AddYears(1);
            h_order.date = ServiceDate;                

            if (OrderStatus == OrderStatusEnum.Completed && !IsRugPickupCompletion)
                h_order.time = GetTimeString(DateTimeCompleted);
            else
                h_order.time = TimeSchedule;

            h_order.page = CurrentAddress.Page;
            h_order.grid = CurrentAddress.Grid;
            h_order.area_id = CurrentAddress.AreaId;
            h_order.serv_type = 4;
            h_order.tran_type = 1;            
            h_order.amount = decimal.ToSingle(ClosedAmount);
            h_order.tech_id = TechnicianId;
            h_order.closer_id = string.Empty;
            h_order.recve_amt = 0;
            h_order.pr_date = Utils.SERVMAN_NULL_DATE;
            h_order.zip = CurrentAddress.Zip;
            h_order.bookby = BookedByText;

            if (TechnicianId != string.Empty)
            {
                techmast tech = techmast.FindByPrimaryKey(TechnicianId);
                h_order.company = tech.cont_id;
            } else 
                h_order.company = string.Empty; 


            
            h_order.companyid = 2;
            h_order.cleancnum = string.Empty;

            try
            {
                h_order.mapbook = zip_data.FindByPrimaryKey(CurrentAddress.Zip).mapbook;
            }
            catch (DataNotFoundException)
            {
                h_order.mapbook = string.Empty;
            }


            if (OrderStatus == OrderStatusEnum.Cancelled)
            {
                h_order.tran_stat = (int)OrderStatusEnum.Completed;
                h_order.comp_type = 3;
            }
            else
            {
                if (IsRugPickupCompletion)
                    h_order.tran_stat = 1;
                else
                    h_order.tran_stat = (int)OrderStatus;

                if (OrderStatus == OrderStatusEnum.Completed && !IsRugPickupCompletion)
                    h_order.comp_type = 2;
                else
                    h_order.comp_type = 1;
            }

            return h_order;            
        }

        #endregion

        #region GetAltAddress

        private m_alt_ad GetAltAddress()
        {
            if (AlternativeAddress == null)
                return null;

            m_alt_ad altAddress = new m_alt_ad();
            altAddress.ticket_num = TicketNumber;
            altAddress.block = AlternativeAddress.Block;
            altAddress.prefix = AlternativeAddress.Prefix;
            altAddress.street = AlternativeAddress.Street;
            altAddress.suffix = AlternativeAddress.Suffux;
            altAddress.unit = AlternativeAddress.Unit;
            altAddress.address2 = AlternativeAddress.Address2;
            altAddress.city = AlternativeAddress.City;
            altAddress.state = AlternativeAddress.State;
            altAddress.zip = AlternativeAddress.Zip;
            altAddress.grid = AlternativeAddress.Page + AlternativeAddress.Grid;
            altAddress.area_id = AlternativeAddress.AreaId;
            return altAddress;            
        }

        #endregion

        #region GetHDeflood

        private hdeflood GetHDeflood()
        {
            hdeflood hdeflood = new hdeflood();
            hdeflood.ticket_num = TicketNumber;
            hdeflood.cust_id = Customer.ID;
            hdeflood.rejected = false; ///??????
            hdeflood.trans_num = OriginateTicketNumber;
            hdeflood.csr_id = "555";
            hdeflood.ad_source = AdvertisingSourceId;
            hdeflood.alt_addr = AlternativeAddress != null;
            hdeflood.tech_refer = AdvertisingTechnicianReferenceId;
            hdeflood.d_1st_call = DateTimeFirstCall.Date;
            hdeflood.t_1st_call = GetTimeString(DateTimeFirstCall);
            if (IsRugPickupCompletion)
                DateSchedule = DateSchedule.AddYears(1);
            hdeflood.d_schedule = DateSchedule;
            hdeflood.t_schedule = TimeSchedule;
            hdeflood.d_dispatch = DateTimeDispatch.Date;
            hdeflood.t_dispatch = GetTimeString(DateTimeDispatch);
            hdeflood.d_complete = DateTimeCompleted.Date;
            hdeflood.t_complete = GetTimeString(DateTimeCompleted);
            hdeflood.mop = 0;
            hdeflood.amount = decimal.ToSingle(ClosedAmount);            
            hdeflood.tech_id = TechnicianId;
            hdeflood.b_person = "555";
            hdeflood.l_person = "555";
            hdeflood.c_person = string.Empty;
            hdeflood.note = PrintedNote;

            if (OrderType == OrderTypeEnum.RugPickup || OrderType == OrderTypeEnum.RugDelivery)
                hdeflood.special1 = "OR";
            else
                hdeflood.special1 = string.Empty;

            if (DefloodInfo == null)
                DefloodInfo = new Deflood();

            hdeflood.src_flood = DefloodInfo.SourceOfFlood;
            hdeflood.d_of_flood = DefloodInfo.DateOfFlood;
            hdeflood.rooms = DefloodInfo.AffectedRooms;
            hdeflood.ordered_by = DefloodInfo.OrderedBy;
            hdeflood.i_agncy = DefloodInfo.InsuranceAgency;
            hdeflood.i_agncy_ph = DefloodInfo.InsuranceAgenyPhone;
            hdeflood.i_agent = DefloodInfo.InsuranceAgent;
            hdeflood.i_carrier = DefloodInfo.InsuranceCarrier;
            hdeflood.i_adj = DefloodInfo.InsuranceAdjustor;
            hdeflood.i_adj_ph = DefloodInfo.InsuranceAdjustorPhone;
            hdeflood.tax_perc = 8.25f;
            hdeflood.sd_name = string.Empty;
            hdeflood.callorigin = string.Empty;
            hdeflood.cc_num = string.Empty;
            hdeflood.cc_expdate = string.Empty;
            hdeflood.dl_num = string.Empty;
            hdeflood.dl_dob = Utils.SERVMAN_NULL_DATE;
            hdeflood.auth_code = string.Empty;
            hdeflood.companyid = 2;
            hdeflood.repeat = false;
            hdeflood.t_arrival = GetTimeString(DateTimeArrived);

            h_order h_order = GetHOrder();
            hdeflood.company = h_order.company;
            hdeflood.tran_type = h_order.tran_type;
            hdeflood.tran_stat = h_order.tran_stat;
            hdeflood.comp_type = h_order.comp_type;
            if (OrderStatus == OrderStatusEnum.Cancelled)
            {
                hdeflood.canc_type = 3; ///?????????     
                hdeflood.c_person = PerformedByUser;
            }
            else
                hdeflood.canc_type = 1;

            return hdeflood;
        }

        #endregion

        #region GetDDeflood

        private ddeflood GetDDeflood()
        {
            ddeflood ddeflood = new ddeflood();
            ddeflood.ticket_num = TicketNumber;
            ddeflood.note = DispatchNote;
            ddeflood.orig_book = true;
            ddeflood.apply2mp = true;
            ddeflood.schddisc = true;

            if (OrderType == OrderTypeEnum.Deflood)
            {
                ddeflood.serv_type = "01";
            }
            else if (OrderType == OrderTypeEnum.Monitoring)
            {
                ddeflood.serv_type = "15";
                if (ddeflood.note == null || ddeflood.note == string.Empty)
                    ddeflood.note = "INVENTORY PICK UP";
                ddeflood.orig_book = false;
                ddeflood.apply2mp = false;
                ddeflood.schddisc = false;
            }
            else if (OrderType == OrderTypeEnum.RugPickup || OrderType == OrderTypeEnum.RugDelivery)
            {
                ddeflood.serv_type = "17";
            }

            ddeflood.item_num = 1;
            ddeflood.amount = decimal.ToSingle(ClosedAmount); ///Fix this            
            ddeflood.commission = 1;
            ddeflood.enter_by = "B";
            ddeflood.prc_type = string.Empty;
            ddeflood.user_adj = string.Empty;


            return ddeflood;
        }

        #endregion

        #region GetDispQue

        private disp_que GetDispQue()
        {
            if (OrderStatus == OrderStatusEnum.Pending && ServiceDate.Date != DateTime.Now.Date)
                return null;

            if (OrderStatus == OrderStatusEnum.Cancelled)
                return null;

            if (IsRugPickupCompletion)
                return null;
            
            disp_que disp_que = new disp_que();
            disp_que.ticket_num = TicketNumber;
            disp_que.customer = Customer.Name;
            disp_que.d_dispatch = DateSchedule.Date;
            disp_que.t_dispatch = GetTimeString(DateTimeDispatch);
            disp_que.t_estcomp = GetTimeString(TimeEstimateComplete);
            disp_que.t_complete = GetTimeString(DateTimeCompleted);
            disp_que.tech_id = TechnicianId == string.Empty ? "167" : TechnicianId;
            disp_que.serv_type = 4;
            disp_que.amount = decimal.ToSingle(ClosedAmount);
            disp_que.comp_type = GetHOrder().comp_type;
            disp_que.phone = Customer.HomePhone;
            disp_que.arival = GetTimeString(DateTimeArrived);
            disp_que.span = TimeSchedule;
            disp_que.time_stat = IsConfirmed ? "OK" : string.Empty;
            disp_que.note = string.Empty;
            disp_que.order = "000 ";
            disp_que.grid = CurrentAddress.Page + CurrentAddress.Grid;
            disp_que.order = string.Empty;
            disp_que.auto_time = string.Empty;


            return disp_que;
        }

        #endregion

    }
}
