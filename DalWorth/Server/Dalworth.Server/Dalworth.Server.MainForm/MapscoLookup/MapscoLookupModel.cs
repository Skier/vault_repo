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

namespace Dalworth.Server.MainForm.MapscoLookup
{
    public class MapscoLookupModel : IModel
    {
        #region InitialAddress

        private Address m_initialAddress;
        public Address InitialAddress
        {
            get { return m_initialAddress; }
            set { m_initialAddress = value; }
        }

        #endregion

        #region MapscoAddresses

        private BindingList<MapscoAddress> m_mapscoAddresses;
        public BindingList<MapscoAddress> MapscoAddresses
        {
            get { return m_mapscoAddresses; }
        }

        #endregion

        #region Init

        public void Init()
        {
            m_mapscoAddresses = new BindingList<MapscoAddress>(
                MapscoAddress.FindPossibleMapsco(m_initialAddress));
        }

        #endregion

        #region Lookup

        public void Lookup(string street)
        {
            Address modifiedAddress = (Address) m_initialAddress.Clone();
            modifiedAddress.Street = street;

            m_mapscoAddresses = new BindingList<MapscoAddress>(
                MapscoAddress.FindPossibleMapscoByStreet(modifiedAddress));            
        }

        #endregion
    }
}
