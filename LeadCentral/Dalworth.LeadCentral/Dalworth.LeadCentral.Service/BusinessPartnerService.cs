using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service
{
    public class BusinessPartnerService
    {
        #region Find

        public static List<BusinessPartner> Find(IDbConnection connection)
        {
            var result = BusinessPartner.FindOrderByPartnerName(connection);

            foreach (var partner in result)
            {
                UpdateSalesRep(partner, connection);
            }

            return result;
        }

        #endregion

        #region FindActive

        public static List<BusinessPartner> FindActive(IDbConnection connection)
        {
            var result = BusinessPartner.FindActive(connection);

            foreach (var partner in result)
            {
                UpdateSalesRep(partner, connection);
            }

            return result;
        }

        #endregion

        #region FindByDatePeriod

        public static List<BusinessPartner> FindByDatePeriod(DateTime? dateFrom, DateTime? dateTo, IDbConnection connection)
        {
            var result = BusinessPartner.GetAllByPeriod(dateFrom, dateTo, connection);

            foreach (var partner in result)
            {
                UpdateSalesRep(partner, connection);
            }

            return result;
        }

        #endregion

        #region FindById

        public static BusinessPartner FindById(int id, IDbConnection connection)
        {
            var result = BusinessPartner.FindByPrimaryKey(id, connection);
            UpdateSalesRep(result, connection);
            result.PhoneNumbers = PartnerPhoneNumber.GetByPartnerId(id, connection);
            return result;
        }

        #endregion

        #region Save

        public static void Save(BusinessPartner partner, IDbConnection connection)
        {
            BusinessPartner.Save(partner, connection);
        }

        #endregion 

        #region FindByCallerNumber

        public static BusinessPartner FindByCallerNumber(string fromPhone, IDbConnection connection)
        {
            var partners = BusinessPartner.GetByCallerNumber(fromPhone, connection);
            return partners.Count == 1 ? partners[0] : null;
        }

        #endregion

        #region LoadPartners

        public static List<BusinessPartner> LoadPartners(PartnerFilter filter, IDbConnection connection)
        {
            var result = BusinessPartner.LoadPartners(filter, connection);

            foreach (var partner in result)
            {
                UpdateSalesRep(partner, connection);
            }

            return result;
        }

        #endregion

        #region private 

        private static void UpdateSalesRep(BusinessPartner partner, IDbConnection connection)
        {
            if (partner.SalesRepId != null && partner.SalesRepId != 0)
                partner.RelatedSalesRep = User.FindByPrimaryKey(partner.SalesRepId.Value, connection);
        }

        #endregion 
    }
}
