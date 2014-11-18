using System;
using System.Windows.Forms;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.AddressEdit
{
    class AddressEditController : Controller<AddressEditModel, AddressEditView>
    {
        #region Address

        private Address m_address;
        public Address Address
        {
            get { return m_address; }
            set
            {
                m_address = value;
            }
        }

        #endregion

        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            if (data != null && data.Length > 0)
                m_address = (Address)data[0];
            else 
            {
                m_address = new Address();
                m_address.State = "TX";
            }
            
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnCancel.Click += OnCancelClick;
            View.m_btnOk.Click += OnOkClick;
            View.KeyDown += OnKeyDown;
        }

        void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) {
                OnCancelClick(sender, EventArgs.Empty);
            } else if (e.KeyCode == Keys.Enter) {
                OnOkClick(sender, EventArgs.Empty);
            }
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            if (m_address.ID == 0)
                View.Text = "Add Address";
            else
                View.Text = "Edit Address";

            View.m_ctrlAddressEdit.Address = m_address;
        }

        #endregion

        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {
            View.ValidateChildren();
            
            if (View.m_ctrlAddressEdit.HasErrors)
                return;

            m_isCancelled = false;

            m_address = View.m_ctrlAddressEdit.Address;
            
            View.Destroy();
        }

        #endregion

        #region OnCancelClick

        private void OnCancelClick(object sender, EventArgs e)
        {
            m_isCancelled = true;
            View.Destroy();
        }

        #endregion
    }
}
