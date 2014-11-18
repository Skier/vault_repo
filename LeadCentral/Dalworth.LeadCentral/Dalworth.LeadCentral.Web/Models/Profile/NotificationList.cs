using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Web.Models.Profile
{
    public class NotificationList
    {
        public List<Notification> Notifications { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public void Load(IDbConnection connection)
        {
            var filter = new NotificationFilter {CreatedFrom = DateFrom, CreatedTo = DateTo};
            Notifications = NotificationService.Find(filter, connection);
        }
    }
}