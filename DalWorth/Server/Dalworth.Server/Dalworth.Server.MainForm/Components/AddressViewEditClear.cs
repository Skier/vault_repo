using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.AddressEdit;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.Components
{
    public partial class AddressViewEditClear : BaseControl
    {
        #region Modified event

        public delegate void ModifiedHandler(Address baseAddress, Address currentAddress, bool isBaseAddressActive);
        public event ModifiedHandler Modified;

        #endregion

        #region Customer

        private Customer m_customer;
        public Customer Customer
        {
            set { m_customer = value; }
        }

        #endregion

        #region BaseAddress

        private Address m_baseAddress;
        public Address BaseAddress
        {
            get { return m_baseAddress; }
            set
            {
                m_baseAddress = value;
                LoadDataToUI();
            }
        }

        #endregion

        #region CurrentAddress

        private Address m_currentAddress;
        public Address CurrentAddress
        {
            get { return m_currentAddress; }
            set
            {
                m_currentAddress = value;
                LoadDataToUI();
            }
        }

        #endregion

        #region IsBaseAddressActive

        private bool m_isBaseAddressActive;
        public bool IsBaseAddressActive
        {
            get { return m_isBaseAddressActive; }
            set
            {
                m_isBaseAddressActive = value;
                LoadDataToUI();
            }
        }

        #endregion

        #region BaseAddressName

        private string m_baseAddressName;
        public string BaseAddressName
        {
            get { return m_baseAddressName; }
            set { m_baseAddressName = value; }
        }

        #endregion

        #region Enabled

        public new bool Enabled
        {
            get { return m_btnAddressEdit.Enabled; }
            set { m_btnAddressEdit.Enabled = value; }
        }

        #endregion

        #region Caption

        private string m_caption;
        public string Caption
        {
            get { return m_caption; }
            set { 
                m_caption = value;
                m_group.Text = m_caption;
            }
        }

        #endregion

        #region Constructor

        public AddressViewEditClear()
        {
            InitializeComponent();
            m_btnAddressEdit.Click += OnAddressEditClick;
            m_btnAddressClear.Click += OnAddressClearClick;
        }

        #endregion        

        #region LoadDataToUI

        private void LoadDataToUI()
        {
            if (m_baseAddress == null || m_currentAddress == null)
            {
                m_lblAddress.Text = string.Empty;
                m_lblCityStateZip.Text = string.Empty;
                m_lblMapsco.Text = string.Empty;
                m_btnAddressEdit.Enabled = false;
                return;
            }            

            Address address = m_isBaseAddressActive ? m_baseAddress : m_currentAddress;
            m_lblAddress.Text = address.AddressFirstLine;
            m_lblCityStateZip.Text = address.AddressSecondLine;
            m_lblMapsco.Text = address.Map;
            m_btnAddressEdit.Enabled = true;
        }

        #endregion

        #region OnAddressEditClick

        private void OnAddressEditClick(object sender, EventArgs e)
        {
            if (m_customer == null)
                throw new DalworthException("Customer is not initialized yet.");
            
            AddressSelectController controller = Controller.Prepare<AddressSelectController>(
                m_baseAddress, m_currentAddress, m_baseAddressName, m_isBaseAddressActive, m_caption, m_customer);
            controller.Execute(false);
            
            if (!controller.IsCancelled)
            {
                m_baseAddress = controller.BaseAddress;
                m_currentAddress = controller.CurrentAddress;
                m_isBaseAddressActive = controller.IsBaseAddressActive;

                LoadDataToUI();
                if (Modified != null)
                    Modified.Invoke(m_baseAddress,
                                    m_currentAddress, m_isBaseAddressActive);                
            }            
        }

        #endregion

        #region OnAddressClearClick

        private void OnAddressClearClick(object sender, EventArgs e)
        {
            m_baseAddress = null;
            m_currentAddress = null;
            m_baseAddressName = null;
            m_isBaseAddressActive = false;
            m_caption = null;
            m_customer = null;
        }

        #endregion
    }
}
