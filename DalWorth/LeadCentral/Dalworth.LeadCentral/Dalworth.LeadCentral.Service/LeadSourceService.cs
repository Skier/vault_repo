using System.Collections.Generic;
using System.Data;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.SDK;

namespace Dalworth.LeadCentral.Service
{
    public class LeadSourceService
    {
        public static LeadSource Save(string ticket, LeadSource leadSource)
        {
            var context = ContextHelper.GetCurrentQbContext(ticket);
            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            if (leadSource.RelatedUser != null)
            {
                leadSource.RelatedUser = UserService.Save(ticket, leadSource.RelatedUser);
                leadSource.UserId = leadSource.RelatedUser.Id;
            }

            if (leadSource.OwnedByUserId == 0) leadSource.OwnedByUserId = null;
            if (leadSource.UserId == 0) leadSource.UserId = null;

            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                leadSource = LeadSource.Save(leadSource, connection);

                if (leadSource.UserId != null)
                    leadSource.RelatedUser = User.FindByPrimaryKey(leadSource.UserId.Value, connection);
            }
            return leadSource;
        }

        public static List<LeadSource> GetAll(string ticket)
        {
            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);
            
            List<LeadSource> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = LeadSource.Find(connection);

                foreach (var bp in result)
                {
                    if (bp.UserId != null)
                        bp.RelatedUser = User.FindByPrimaryKey(bp.UserId.Value, connection);
                }
            }
            return result;
        }

        public static LeadSource GetByUserId(string ticket, int userId)
        {
            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            LeadSource result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = LeadSource.GetByUserId(userId, connection);
                
                if (result.UserId != null)
                    result.RelatedUser = User.FindByPrimaryKey(result.UserId.Value, connection);
            }
            return result;
        }


        public static List<LeadSource> GetByTrackingPhoneId(string ticket, int phoneId)
        {
            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            List<LeadSource> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = LeadSource.GetByTrackingPhoneId(phoneId, connection);

                foreach (var leadSource in result)
                {
                    if (leadSource.UserId != null)
                        leadSource.RelatedUser = User.FindByPrimaryKey(leadSource.UserId.Value, connection);
                }
            }
            return result;
        }

        public static List<LeadSource> GetByPhoneNumber(ServmanCustomer servmanCustomer, string phoneNo)
        {
            List<LeadSource> result;
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = LeadSource.GetBySimplePhoneNumber(StringUtil.ExtractLastSevenDigits(phoneNo), connection);

                foreach (var leadSource in result)
                {
                    if (leadSource.UserId != null)
                        leadSource.RelatedUser = User.FindByPrimaryKey(leadSource.UserId.Value, connection);
                }
            }
            return result;
        }

        public static List<LeadSource> GetByTrackingPhoneNumber(ServmanCustomer servmanCustomer, int phoneId)
        {
            List<LeadSource> result;
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = LeadSource.GetByTrackingPhoneId(phoneId, connection);

                foreach (var leadSource in result)
                {
                    if (leadSource.UserId != null)
                        leadSource.RelatedUser = User.FindByPrimaryKey(leadSource.UserId.Value, connection);
                }
            }
            return result;
        }

        public static LeadSource SaveWithPhones(string ticket, LeadSource leadSource, LeadSourcePhone[] leadSourcePhones, TrackingPhone[] trackingPhones)
        {
            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            var source = Save(ticket, leadSource);
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                List<LeadSourcePhone> phones = LeadSourcePhone.GetByLeadSourceId(source.Id, connection);

                foreach(var phone in phones)
                {
                    if (PhoneExistsInCollection(phone, leadSourcePhones)) 
                        continue;

                    phone.IsRemoved = true;
                    LeadSourcePhone.Save(phone, connection);
                }

                foreach (var phone in leadSourcePhones)
                {
                    phone.LeadSourceId = source.Id;
                    phone.SimplePhoneNumber = StringUtil.ExtractLastSevenDigits(phone.PhoneNumber);
                    LeadSourcePhone.Save(phone, connection);
                }

                LeadSourceTrackingPhone.RemoveByLeadSourceId(source.Id, connection);
                if (leadSource.IsActive)
                {
                    var leadSourceTrackingPhones = new List<LeadSourceTrackingPhone>();
                    foreach (var trackingPhone in trackingPhones)
                    {
                        var leadSourceTrackingPhone = new LeadSourceTrackingPhone
                        {
                            LeadSourceId = leadSource.Id,
                            TrackingPhoneId = trackingPhone.Id
                        };
                        leadSourceTrackingPhones.Add(leadSourceTrackingPhone);
                    }

                    LeadSourceTrackingPhone.Insert(leadSourceTrackingPhones, connection);
                }
            }

            return source;
        }

        private static bool PhoneExistsInCollection(LeadSourcePhone phone, IEnumerable<LeadSourcePhone> collection)
        {
            foreach (var p in collection)
            {
                if (p.Id == phone.Id)
                    return true;
            }
            return false;
        }

        public static TrackingPhone AddTrackingPhone(string ticket, LeadSource leadSource, TrackingPhone phone)
        {
            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                var leadSourceTrackingPhone = new LeadSourceTrackingPhone
                    {
                        LeadSourceId = leadSource.Id,
                        TrackingPhoneId = phone.Id
                    };

                LeadSourceTrackingPhone.Insert(leadSourceTrackingPhone, connection);
            }

            return PhoneService.Save(ticket, phone);
        }

        public static TrackingPhone RemoveTrackingPhone(string ticket, LeadSource leadSource, TrackingPhone phone)
        {
            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                var leadSourceTrackingPhone = LeadSourceTrackingPhone.FindByPrimaryKey(leadSource.Id, phone.Id, connection);
                LeadSourceTrackingPhone.Delete(leadSourceTrackingPhone, connection);
            }

            return PhoneService.Save(ticket, phone);
        }

    }
}
