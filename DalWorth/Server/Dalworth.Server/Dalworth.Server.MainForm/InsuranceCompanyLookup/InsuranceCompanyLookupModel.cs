using System;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.CustomerLookup
{
    public class InsuranceCompanyLookupModel : IModel
    {
        #region Customers

        private BindingList<InsuranceCompanyAndAddress> m_insuranceCompanies;
        public BindingList<InsuranceCompanyAndAddress> InsuranceCompanies
        {
            get { return m_insuranceCompanies; }
            set { m_insuranceCompanies = value; }
        }

        #endregion        

        #region Init

        public void Init()
        {
            Refresh(string.Empty, string.Empty, string.Empty);
        }

        #endregion

        #region Refresh

        public void Refresh(string name, string mapsco, string address)
        {
            m_insuranceCompanies = new BindingList<InsuranceCompanyAndAddress>(
                InsuranceCompany.FindInsuranceCompanyAndAddresses(name, mapsco, address));
        }

        #endregion

        #region CreateNewCompany

        public void CreateNewCompany(InsuranceCompanyAndAddress company)
        {
            Address.Insert(company.Address);
            company.InsuranceCompany.AddressId = company.Address.ID;
            InsuranceCompany.Insert(company.InsuranceCompany);
        }

        #endregion
    }
}
