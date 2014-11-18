using System;

namespace AerSysCo.Entity
{
    public class OrderFilter
    {
        public string createdBy;
        public int statusId;
        public string poNumber;
        public string poNumberStrong;
        public string confirmNumber;
        public int quantity;
        public DateTime fromDate;
        public DateTime toDate;
        public int customerId;
    }
}
