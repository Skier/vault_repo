using System;
using System.Collections.Generic;
using System.Text;

namespace dalworth.domain
{
    public enum ApplicationPoint
    {
        StartDay,
        StartDayDone
    }
    
    public enum TicketStatus
    {
        Started,
        Cancelled,
        Declined,
        Processed
    }
        
            
    public class Model
    {
        public Model()
        {
            m_appPoint = ApplicationPoint.StartDay;
        }

        public void Init()
        {            
            m_appPoint = ApplicationPoint.StartDay;   
            
            m_messages = new List<Message>();
            m_messages.Add(new Message("subject 1", "body 1"));
            m_messages.Add(new Message("subject 2", "body 2"));
            m_messages.Add(new Message("subject 3", "body 3"));
            m_messages.Add(new Message("subject 4", "body 4"));
            m_messages.Add(new Message("subject 5", "body 5"));
            
            m_startDayTickets = new List<Ticket>();
            m_startDayTickets.Add(new Ticket(1000, "Rob Love", 2));
            m_startDayTickets.Add(new Ticket(1001, "Robert Sage", 5));
            m_startDayTickets.Add(new Ticket(1002, "Anne Loomis", 3));
            
            m_equipmentRequests = new List<EquipmentRequest>();            
            m_equipmentRequests.Add(new EquipmentRequest("Fan(s)", 4));
            m_equipmentRequests.Add(new EquipmentRequest("Dehumidifier(s)", 2));
            m_equipmentRequests.Add(new EquipmentRequest("Air Scrubber(s)", 3));
            
            m_servicedTickets = new List<Ticket>();
            m_servicedTickets.Add(new Ticket(1010, "Cheknis, Benjamin", DateTime.Now, "31B", "431 Wyatt Ave\r\nEast Bayshore, CA, 98727", "(415) 555-4356", "(415) 734-8723", "Dehumidification", "", 314, TicketStatus.Processed));
            m_servicedTickets.Add(new Ticket(1011, "Leon, Richard", DateTime.Now, "18G", "610 N. Mission Ave\r\nMiddlefield, CA, 98731", "(415) 392-5628", "(415) 819-5722", "Deflood", "", 312, TicketStatus.Processed));
            m_servicedTickets.Add(new Ticket(1012, "Price,  Gwen", DateTime.Now, "12Q", "8292 Orange Blossom Dr\r\nBayshore, CA, 94326", "(415) 023-3456", "(415) 024-6732", "Rug Delivery", "", 815, TicketStatus.Processed));
            m_servicedTickets.Add(new Ticket(1013, "Stinson, Tracy", DateTime.Now, "35M", "3219 Lisa Lane\r\nBayshore, CA, 94326", "(415) 492-7821", "(415) 205-2873", "Deflood", "", 298, TicketStatus.Processed));
            
            m_tickets = new List<Ticket>();
            
            //TODO: uncomment this
            m_tickets.Add(new Ticket(1001, "Cheknis, Benjamin", DateTime.Now, "31B", "431 Wyatt Ave\r\nEast Bayshore, CA, 98727", "(415) 555-4356", "(415) 734-8723", "Rug Pickup", "Pick up 2 rugs. Cust would like 3-5 time frame. Please call in AM to confirm.", 0, TicketStatus.Started, "Customer is not home. Key is under mat. Be aware of a dog.",
                "Benjamin", "Cheknis", "East Bayshore", "CA", "98727"));
            m_tickets.Add(new Ticket(1002, "Leon, Richard", DateTime.Now, "18G", "610 N. Mission Ave\r\nMiddlefield, CA, 98731", "(415) 392-5628", "(415) 819-5722", "Rug Pickup", "Pick up 3 rugs. Cust would like 10-12 time frame. Please call in to confirm.", 0, TicketStatus.Started, "Customer is not home. Key is under mat. Be aware of a dog.",
                "Richard", "Leon", "Middlefield", "CA", "98731"));
            //TODO: uncomment this

            m_tickets.Add(new Ticket(1003, "Price,  Gwen", DateTime.Now, "12Q", "8292 Orange Blossom Dr\r\nBayshore, CA, 94326", "(415) 023-3456", "(415) 024-6732", "Rug Delivery", "Deliver 2 rugs. Cust would like 3-5 time frame. Please call in AM to confirm.", 0, TicketStatus.Started, "Customer is not home. Key is under mat. Be aware of a dog.",
                              "Gwen", "Price", "Bayshore", "CA", "94326"));
            m_tickets.Add(new Ticket(1004, "Stinson, Tracy", DateTime.Now, "35M", "3219 Lisa Lane\r\nBayshore, CA, 94326", "(415) 492-7821", "(415) 205-2873", "Rug Delivery", "Deliver 3 rugs. Cust would like 10-12 time frame. Please call in to confirm.", 0, TicketStatus.Started, "Customer is not home. Key is under mat. Be aware of a dog.",
                              "Tracy", "Stinson", "Bayshore", "CA", "94326"));

            //TODO: remove me
//            m_tickets[0].Rugs.Add(new Rug(RugShape.Rectangle, 2, 5, 0, true, true, false, false, true, 0));
//            m_tickets[0].Rugs.Add(new Rug(RugShape.Rectangle, 4, 7, 0, true, true, false, true, false, 12));
//            m_tickets[0].Rugs.Add(new Rug(RugShape.Round, 0, 0, 12, true, false, true, true, true, 0));
//
//            m_tickets[1].Rugs.Add(new Rug(RugShape.Rectangle, 3, 3, 0, true, false, true, false, true, 0));
//            m_tickets[1].Rugs.Add(new Rug(RugShape.Rectangle, 2, 4, 0, true, true, false, true, false, 10));
            //TODO: remove me
            
            //TODO: uncomment this
            m_tickets[2].Rugs.Add(new Rug(RugShape.Rectangle, 2, 5, 0, true, true, false, false, true, 0));
            m_tickets[2].Rugs.Add(new Rug(RugShape.Rectangle, 4, 7, 0, true, true, false, true, false, 12));
            m_tickets[2].Rugs.Add(new Rug(RugShape.Round, 0, 0, 12, true, false, true, true, true, 0));

            m_tickets[3].Rugs.Add(new Rug(RugShape.Rectangle, 3, 3, 0, true, false, true, false, true, 0));
            m_tickets[3].Rugs.Add(new Rug(RugShape.Rectangle, 2, 4, 0, true, true, false, true, false, 10));
            //TODO: uncomment this
        }

        private ApplicationPoint m_appPoint;
        public ApplicationPoint AppPoint
        {
            get { return m_appPoint; }
            set { m_appPoint = value; }
        }

        private List<Message> m_messages;
        public List<Message> Messages
        {
            get { return m_messages; }
        }

        private List<int> m_selectedMessages;
        public List<int> SelectedMessages
        {
            get { return m_selectedMessages; }
            set { m_selectedMessages = value; }
        }

        private List<Ticket> m_startDayTickets;
        public List<Ticket> StartDayTickets
        {
            get { return m_startDayTickets; }
            set { m_startDayTickets = value; }
        }

        private List<EquipmentRequest> m_equipmentRequests;
        public List<EquipmentRequest> EquipmentRequests
        {
            get { return m_equipmentRequests; }
            set { m_equipmentRequests = value; }
        }

        private decimal m_currentServiceSum;
        public decimal CurrentServiceSum
        {
            get { return m_currentServiceSum; }
            set { m_currentServiceSum = value; }
        }

        private decimal m_currentTotalSum;
        public decimal CurrentTotalSum
        {
            get { return m_currentTotalSum; }
            set { m_currentTotalSum = value; }
        }
        
        private string m_currentNumber;
        public string CurrentNumber
        {
            get { return m_currentNumber; }
            set { m_currentNumber = value; }
        }
        
        private decimal m_currentAmountReceived;
        public decimal CurrentAmountReceived
        {
            get { return m_currentAmountReceived; }
            set { m_currentAmountReceived = value; }
        }

        private string m_currentEtcNote;
        public string CurrentEtcNote
        {
            get { return m_currentEtcNote; }
            set { m_currentEtcNote = value; }
        }

        private DateTime m_currentETC;
        public DateTime CurrentETC
        {
            get { return m_currentETC; }
            set { m_currentETC = value; }
        }

        private List<Ticket> m_servicedTickets;
        public List<Ticket> ServicedTickets
        {
            get { return m_servicedTickets; }
            set { m_servicedTickets = value; }
        }
        
        private Ticket m_currentServicedTicket;
        public Ticket CurrentServicedTicket
        {
            get { return m_currentServicedTicket; }
            set { m_currentServicedTicket = value; }
        }

        private List<Ticket> m_tickets;
        public List<Ticket> Tickets
        {
            get { return m_tickets; }
            set { m_tickets = value; }
        }

        private Ticket m_currentTicket;
        public Ticket CurrentTicket
        {
            get { return m_currentTicket; }
            set { m_currentTicket = value; }
        }

        public void ResetCurrentTicket()
        {
            m_currentNumber = string.Empty;
            m_currentServiceSum = decimal.Zero;            
            m_currentTotalSum = decimal.Zero;
            m_currentETC = DateTime.MinValue;
            m_currentAmountReceived = decimal.Zero;
            m_currentEtcNote = string.Empty;
        }
            
    }
}
