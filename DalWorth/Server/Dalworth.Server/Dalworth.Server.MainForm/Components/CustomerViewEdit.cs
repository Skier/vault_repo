using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.CustomerEdit;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.Components
{
    public partial class CustomerViewEdit : BaseControl
    {
        #region Modified event

        public delegate void ModifiedHandler(Customer customer, Address address);
        public event ModifiedHandler Modified;

        #endregion

        #region Customer

        private Customer m_customer;
        public virtual Customer Customer
        {
            get { return m_customer; }
            set
            {
                m_customer = value;
                UpdateLabels();                
            }
        }

        #endregion

        #region Address

        private Address m_address;
        public virtual Address Address
        {
            get { return m_address; }
            set { m_address = value; }
        }

        #endregion        

        #region Constructor

        public CustomerViewEdit()
        {
            InitializeComponent();
            m_btnCustomerEdit.Click += OnCustomerEditClick;
        }

        #endregion

        #region Enabled

        public new bool Enabled
        {
            get { return m_btnCustomerEdit.Enabled; }
            set { m_btnCustomerEdit.Enabled = value; }
        }

        #endregion

        #region EmailVisible

        public bool EmailVisible
        {
            get { return m_txtEmail.Visible; }
            set 
            { 
                 m_txtEmail.Visible = value;
                 m_lblEmail.Visible = value;
            }
        }

        #endregion



        #region UpdateLabels

        private void UpdateLabels()
        {
            if (m_customer != null)
            {
                m_lblCustomerName.Text = m_customer.DisplayName;
                m_lblHomePhone.Text = m_customer.Phone1Formatted;
                m_lblWorkPhone.Text = m_customer.Phone2Formatted;
                m_txtEmail.Text = m_customer.Email;

                m_btnCustomerEdit.Enabled = true;
            }
            else
            {
                m_lblCustomerName.Text = string.Empty;
                m_lblHomePhone.Text = string.Empty;
                m_lblWorkPhone.Text = string.Empty;

                m_btnCustomerEdit.Enabled = false;
            }            
        }

        #endregion

        #region OnCustomerEditClick

        private void OnCustomerEditClick(object sender, EventArgs e)
        {
            CustomerEditController controller = Controller.Prepare<CustomerEditController>(
                new CustomerAndAddress(m_customer, m_address));
            controller.Execute(false);

            if (!controller.IsCancelled)
            {
                UpdateLabels();
                if (Modified != null)
                    Modified.Invoke(m_customer, m_address);
            }
        }

        #endregion

        #region OnCustomerEditClick

        private void OnEmailLeave(object sender, EventArgs e)
        {
            if (m_txtEmail.Text.Trim() != String.Empty)
            {
                string newEmail = m_txtEmail.Text.ToUpper().Trim();

                if (!newEmail.Equals(m_customer.Email))
                {
                    m_customer.Email = m_txtEmail.Text.ToUpper().Trim();
                    if (Modified != null)
                        Modified.Invoke(m_customer, m_address);
                }
            }
        }

        #endregion
    }
}
