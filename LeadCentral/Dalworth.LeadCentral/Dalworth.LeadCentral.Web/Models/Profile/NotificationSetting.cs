using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Web.Models.Profile
{
    public class NotificationSetting
    {
        public List<NotificationType> NotificationSettings { get; set; }
        public NotificationSetting(IDbConnection connection)
        {
            NotificationSettings = NotificationService.GetTypes(connection);
        }

        public void Update(string[] settings, IDbConnection connection)
        {
            var list = new List<string>(settings);

            foreach (var type in NotificationSettings)
            {
                type.SendToAdmin = list.Contains(string.Format("{0}.01", type.Id));
                type.SendToSalesRep = list.Contains(string.Format("{0}.02", type.Id));
                type.SendToStaff = list.Contains(string.Format("{0}.03", type.Id));
                type.SendToAccountant = list.Contains(string.Format("{0}.04", type.Id));
                type.SendToPartner = list.Contains(string.Format("{0}.05", type.Id));
                type.SendToPartnerUsers = list.Contains(string.Format("{0}.06", type.Id));
            }

            NotificationService.UpdateSettings(NotificationSettings, connection);
        }
    }
}