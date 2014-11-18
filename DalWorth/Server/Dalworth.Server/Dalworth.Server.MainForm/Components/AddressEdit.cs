using System;
using System.Collections.Generic;
using System.ComponentModel;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.MapscoLookup;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.Components
{
    public partial class AddressEdit : BaseControl
    {
        #region Address

        private Address m_address;
        
        [Browsable(false)]
        public Address Address
        {
            get
            {
                LoadDataFromUI();
                return m_address;
            }
            
            set
            {
                m_address = value;
                LoadDataToUI();
            }
        }

        #endregion

        #region IsValidationEnabled

        private bool m_isValidationDisabled;
        public bool IsValidationDisabled
        {
            get { return m_isValidationDisabled; }
            set { m_isValidationDisabled = value; }
        }

        #endregion

        #region ZipUI

        private string ZipUI
        {
            get
            {
                if (m_txtZip.EditValue != null)
                    return m_txtZip.EditValue.ToString().Replace("_", string.Empty);
                return string.Empty;
            }
        }

        #endregion


        #region HasErrors
        
        public bool HasErrors
        {
            get { return m_errorProvider.HasErrors; }
        }

        #endregion

        #region Constructor

        public AddressEdit()
        {
            InitializeComponent();

            m_btnMapscoLookup.Click += OnMapscoLookupClick;

            m_txtZip.Validating += OnZipValidating;
            m_txtBlock.Validating += OnBlockValidating;
            m_txtStreet.Validating += OnStreetValidating;
            m_txtCity.Validating += OnCityValidating;

            m_txtBlock.Leave += OnBlockLeave;
            m_txtStreet.Leave += OnStreetLeave;
            m_txtCity.Leave += OnCityLeave;
            m_txtZip.Leave += OnZipLeave;
            m_txtMapsco.Leave += OnMapscoLeave;

            m_txtZip.TextChanged += MapscoSpecificFieldChanged;
            m_txtStreet.TextChanged += MapscoSpecificFieldChanged;
            m_txtCity.TextChanged += MapscoSpecificFieldChanged;
        }

        #endregion

        #region OnZipLeave

        private string m_previousZip;

        private void OnZipLeave(object sender, EventArgs e)
        {
            if (ZipUI == string.Empty)
                return;

            if (ZipUI == m_previousZip)
                return;

            List<string> suitableCities = MapscoAddress.FindCities(ZipUI);

            bool isSuitableCityEntered = false;
            foreach (string city in suitableCities)
            {
                if (city.ToLower() == m_txtCity.Text.ToLower())
                {
                    isSuitableCityEntered = true;
                    break;
                }                    
            }

            if (!isSuitableCityEntered)
            {
                if (suitableCities.Count > 0)
                {
                    m_txtCity.Text = suitableCities[0];
                    m_errorProvider.SetError(m_txtCity, string.Empty);

                } else
                    m_txtCity.Text = string.Empty;
            }

            Area area = Zip.FindArea(ZipUI);
            if (area == null)
            {
                m_lblArea.Text = "[Unknown]";
                m_address.AreaId = null;
            }                
            else
            {
                m_lblArea.Text = area.Name;
                m_address.AreaId = area.ID;
            }

            OnAddressFieldLeave(null, null);
            m_previousZip = ZipUI;
        }

        #endregion

        #region OnCityLeave

        private string m_previousCity;
        private void OnCityLeave(object sender, EventArgs e)
        {
            if (ZipUI == string.Empty && m_txtBlock.Text != string.Empty
                    && m_txtStreet.Text != string.Empty
                    && m_txtCity.Text != string.Empty)
            {
                Address tempAddress = new Address();
                tempAddress.Block = m_txtBlock.Text;
                tempAddress.Street = m_txtStreet.Text;
                tempAddress.City = m_txtCity.Text;
                tempAddress.State = m_cmbState.Text;

                string zip = MapscoAddress.FindZip(tempAddress);
                if (zip != string.Empty)
                {
                    m_txtZip.Text = zip;
                    OnZipLeave(null, null);
                }                    
            }

            if (m_txtCity.Text == m_previousCity)
                return;

            OnAddressFieldLeave(null, null);
            m_previousZip = ZipUI;
            m_previousCity = m_txtCity.Text;
        }

        #endregion


        #region EnableDisableMapscoLookup

        private void MapscoSpecificFieldChanged(object sender, EventArgs e)
        {
            EnableDisableMapscoLookup();
        }

        private void EnableDisableMapscoLookup()
        {
            if (ZipUI != string.Empty
                && m_txtStreet.Text != string.Empty
                && m_txtCity.Text != string.Empty)
            {
                m_btnMapscoLookup.Enabled = true;
            } else
                m_btnMapscoLookup.Enabled = false;            
        }

        #endregion

        #region MapscoLookup

        public void MapscoLookup()
        {
            if (ZipUI == string.Empty
                || m_txtStreet.Text == string.Empty
                || m_txtCity.Text == string.Empty)
            {
                return;
            }

            LoadDataFromUI();

            using (MapscoLookupController controller
                = Controller.Prepare<MapscoLookupController>(m_address))
            {
                controller.Execute(false);
                if (controller.ResultAddress != null)
                {
                    m_txtZip.Text = controller.ResultAddress.Zip;
                    m_cmbPrefix.Text = controller.ResultAddress.Prefix;
                    m_txtStreet.Text = controller.ResultAddress.Street;
                    m_cmbSuffux.Text = controller.ResultAddress.Suffix;
                    m_txtMapsco.Text = controller.ResultAddress.MapscoFull;
                    m_txtCity.Text = controller.ResultAddress.City;
                    m_cmbState.Text = controller.ResultAddress.State;
                }
                    
            }            
        }

        #endregion

        #region OnMapscoLookupClick

        private void OnMapscoLookupClick(object sender, EventArgs e)
        {
            MapscoLookup();
        }

        #endregion



        #region OnAddressFieldLeave

        private string m_previousBlock;
        private void OnBlockLeave(object sender, EventArgs e)
        {
            if (m_txtBlock.Text == m_previousBlock)
                return;

            OnAddressFieldLeave(null, null);
            m_previousBlock = m_txtBlock.Text;
        }

        private string m_previousStreet;
        private void OnStreetLeave(object sender, EventArgs e)
        {
            if (m_txtStreet.Text == m_previousStreet)
                return;

            OnAddressFieldLeave(null, null);
            m_previousStreet = m_txtStreet.Text;
        }

        private void OnMapscoLeave(object sender, EventArgs e)
        {
            m_previousBlock = m_txtBlock.Text;
            m_previousStreet = m_txtStreet.Text;
            m_previousZip = ZipUI;
            m_previousCity = m_txtCity.Text;
        }

        private void OnAddressFieldLeave(object sender, EventArgs e)
        {
            LoadDataFromUI();
            MapscoAddress.AssignMapsco(m_address);

            if (m_address == null)
                return;

            m_txtMapsco.Text = m_address.Map;
        }

        #endregion

        #region Controls Validation

        private void OnZipValidating(object sender, CancelEventArgs e)
        {
            if (m_isValidationDisabled)
            {
                m_errorProvider.SetError(m_txtZip, string.Empty);
                return;
            }

            if (ZipUI == string.Empty)
                m_errorProvider.SetError(m_txtZip, "Please enter Zip code");
            else
                m_errorProvider.SetError(m_txtZip, string.Empty);
        }

        private void OnBlockValidating(object sender, CancelEventArgs e)
        {
            if (m_isValidationDisabled)
            {
                m_errorProvider.SetError(m_txtBlock, string.Empty);
                return;
            }

            if (m_txtBlock.Text == string.Empty)
                m_errorProvider.SetError(m_txtBlock, "Please enter Block");
            else
                m_errorProvider.SetError(m_txtBlock, string.Empty);
        }

        private void OnStreetValidating(object sender, CancelEventArgs e)
        {
            if (m_isValidationDisabled)
            {
                m_errorProvider.SetError(m_txtStreet, string.Empty);
                return;
            }

            if (m_txtStreet.Text == string.Empty)
                m_errorProvider.SetError(m_txtStreet, "Please enter Street");
            else
                m_errorProvider.SetError(m_txtStreet, string.Empty);
        }

        private void OnCityValidating(object sender, CancelEventArgs e)
        {
            if (m_isValidationDisabled)
            {
                m_errorProvider.SetError(m_txtCity, string.Empty);
                return;
            }                

            if (m_txtCity.Text == string.Empty)
                m_errorProvider.SetError(m_txtCity, "Please enter City");
            else
                m_errorProvider.SetError(m_txtCity, string.Empty);
        }

        #endregion

        #region LoadDataFromUI

        private void LoadDataFromUI()
        {
            if (m_address == null)
                return;

            m_address.Block = m_txtBlock.Text;
            m_address.Prefix = m_cmbPrefix.Text;
            m_address.Street = m_txtStreet.Text;
            m_address.Suffix = m_cmbSuffux.Text;
            m_address.Unit = m_txtUnit.Text;
            m_address.Address2 = m_txtAddress2.Text;
            m_address.City = m_txtCity.Text;
            m_address.State = m_cmbState.Text;
            m_address.Zip = ZipUI == string.Empty ? (int?)null : int.Parse(ZipUI);

            string page = m_txtMapsco.Text;
            if (m_txtMapsco.Text != string.Empty &&  char.IsLetter(m_txtMapsco.Text[m_txtMapsco.Text.Length - 1]))
            {
                m_address.MapLetter = m_txtMapsco.Text[m_txtMapsco.Text.Length - 1].ToString();
                page = m_txtMapsco.Text.Substring(0, m_txtMapsco.Text.Length - 1);
            } else
                m_address.MapLetter = string.Empty;

            m_address.MapPage = page;                
        }

        #endregion

        #region LoadDataToUI

        private void LoadDataToUI()
        {
            if (m_address == null)
                return;

            m_txtBlock.Text = m_address.Block;
            m_cmbPrefix.Text = m_address.Prefix;
            m_txtStreet.Text = m_address.Street;
            m_cmbSuffux.Text = m_address.Suffix;
            m_txtUnit.Text = m_address.Unit;
            m_txtAddress2.Text = m_address.Address2;
            m_txtCity.Text = m_address.City;
            m_cmbState.Text = m_address.State;
            m_txtZip.Text = m_address.Zip.ToString();
            m_txtMapsco.Text = m_address.Map;

            if (m_address.AreaId == null)
                m_lblArea.Text = "[Unknown]";
            else
                m_lblArea.Text = Area.FindByPrimaryKey(m_address.AreaId.Value).Name;

            if (m_address.ID == 0)
            {
                OnAddressFieldLeave(null, null);
                OnZipLeave(null, null);
            }

            m_txtBlock.Focus();
        }

        #endregion
    }
}