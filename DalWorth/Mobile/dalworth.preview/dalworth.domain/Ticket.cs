using System;
using System.Collections.Generic;
using System.Text;

namespace dalworth.domain
{
    public class Ticket
    {
        public Ticket(int number, string customerName, int rugsCount)
        {
            m_number = number;
            m_customerName = customerName;
            m_rugsCount = rugsCount;
            
            m_rugs = new List<Rug>();
        }

        public Ticket(int number, string customerName, DateTime date, string map, string address, string phone1, string phone2, string jobType, string notes, decimal amountCollected, TicketStatus status)
        {
            m_number = number;
            m_customerName = customerName;
            m_date = date;
            m_map = map;
            m_address = address;
            m_phone1 = phone1;
            m_phone2 = phone2;
            m_jobType = jobType;
            m_notes = notes;
            m_amountCollected = amountCollected;
            m_status = status;

            m_rugs = new List<Rug>();
        }

        public Ticket(int number, string customerName, DateTime date, string map, string address, string phone1, string phone2, string jobType, string notes, decimal amountCollected, TicketStatus status, string notes2, string firstName, string lastName, string city, string state, string zip)
        {
            m_number = number;
            m_customerName = customerName;
            m_date = date;
            m_map = map;
            m_address = address;
            m_phone1 = phone1;
            m_phone2 = phone2;
            m_jobType = jobType;
            m_notes = notes;
            m_amountCollected = amountCollected;
            m_status = status;
            m_notes2 = notes2;
            m_firstName = firstName;
            m_lastName = lastName;
            m_city = city;
            m_state = state;
            m_zip = zip;

            m_rugs = new List<Rug>();
        }

        private List<Rug> m_rugs;
        public List<Rug> Rugs
        {
            get { return m_rugs; }
            set { m_rugs = value; }
        }

        private int m_number;
        private string m_customerName;
        private int m_rugsCount;
        private bool m_isRugsLoaded;

        public int Number
        {
            get { return m_number; }
            set { m_number = value; }
        }

        public string CustomerName
        {
            get { return m_customerName; }
            set { m_customerName = value; }
        }

        public int RugsCount
        {
            get { return m_rugsCount; }
            set { m_rugsCount = value; }
        }

        public bool IsRugsLoaded
        {
            get { return m_isRugsLoaded; }
            set { m_isRugsLoaded = value; }
        }

        private DateTime m_date;
        private string m_map;
        private string m_address;
        private string m_phone1;
        private string m_phone2;
        private string m_jobType;
        private string m_notes;
        private decimal m_amountCollected;

        public DateTime Date
        {
            get { return m_date; }
            set { m_date = value; }
        }

        public string Map
        {
            get { return m_map; }
            set { m_map = value; }
        }

        public string Address
        {
            get { return m_address; }
            set { m_address = value; }
        }

        public string Phone1
        {
            get { return m_phone1; }
            set { m_phone1 = value; }
        }

        public string Phone2
        {
            get { return m_phone2; }
            set { m_phone2 = value; }
        }

        public string JobType
        {
            get { return m_jobType; }
            set { m_jobType = value; }
        }

        public string Notes
        {
            get { return m_notes; }
            set { m_notes = value; }
        }

        public decimal AmountCollected
        {
            get { return m_amountCollected; }
            set { m_amountCollected = value; }
        }

        private TicketStatus m_status;
        public TicketStatus Status
        {
            get { return m_status; }
            set { m_status = value; }
        }

        private string m_notes2;
        public string Notes2
        {
            get { return m_notes2; }
            set { m_notes2 = value; }
        }

        private string m_firstName;
        private string m_lastName;
        private string m_city;
        private string m_state;
        private string m_zip;

        public string FirstName
        {
            get { return m_firstName; }
            set { m_firstName = value; }
        }

        public string LastName
        {
            get { return m_lastName; }
            set { m_lastName = value; }
        }

        public string City
        {
            get { return m_city; }
            set { m_city = value; }
        }

        public string State
        {
            get { return m_state; }
            set { m_state = value; }
        }

        public string Zip
        {
            get { return m_zip; }
            set { m_zip = value; }
        }
    }
}
