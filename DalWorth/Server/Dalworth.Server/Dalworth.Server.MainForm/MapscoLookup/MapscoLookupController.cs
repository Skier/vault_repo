using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.AddressEdit;
using Dalworth.Server.MainForm.CustomerEdit;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using BaseControl=DevExpress.XtraEditors.BaseControl;

namespace Dalworth.Server.MainForm.MapscoLookup
{
    public class MapscoLookupController : Controller<MapscoLookupModel, MapscoLookupView>
    {
        #region ResultAddress

        private MapscoAddress m_resultAddress;
        public MapscoAddress ResultAddress
        {
            get { return m_resultAddress; }
        }

        #endregion

        #region SelectedMapsco

        private MapscoAddress SelectedMapsco
        {
            get
            {
                if (View.m_gridViewMapsco.FocusedRowHandle >= 0)
                    return (MapscoAddress)View.m_gridViewMapsco.GetRow(
                        View.m_gridViewMapsco.FocusedRowHandle);
                return null;
            }
        }

        #endregion        

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.InitialAddress = (Address) data[0];
                
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {            
            View.m_gridViewMapsco.KeyPress += OnGridMapscoKeyPress;
            View.m_gridViewMapsco.DoubleClick += OnGridMapscoDoubleClick;

            View.m_txtStreet.KeyDown += OnStreetKeyDown;            
            View.m_btnClose.Click += OnCloseClick;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_txtStreet.Text = Model.InitialAddress.Street;
            View.m_txtStreet.Select(View.m_txtStreet.Text.Length, 0);
            View.m_txtStreet.TextChanged += OnStreetChanged;

            View.m_gridMapsco.DataSource = Model.MapscoAddresses;
            if (Model.MapscoAddresses.Count > 0)
                View.m_gridMapsco.Select();
            else
                View.m_txtStreet.Select();
        }

        #endregion

        #region OnGridMapscoDoubleClick

        private void OnGridMapscoDoubleClick(object sender, EventArgs e)
        {
            if (SelectedMapsco == null)
                return;

            m_resultAddress = SelectedMapsco;
            View.Destroy();
        }

        #endregion

        #region OnGridMapscoKeyPress

        private void OnGridMapscoKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                OnGridMapscoDoubleClick(null, null);
        }

        #endregion

        #region OnStreetKeyDown

        private void OnStreetKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Lookup();
        }

        #endregion

        #region OnStreetChanged

        private void OnStreetChanged(object sender, EventArgs e)
        {
            Lookup();
        }

        #endregion

        #region Lookup

        private void Lookup()
        {
            if (View.m_txtStreet.Text.Length >= 2)
            {
                Model.Lookup(View.m_txtStreet.Text);
                View.m_gridMapsco.DataSource = Model.MapscoAddresses;                
            }
        }

        #endregion


        #region OnCloseClick

        private void OnCloseClick(object sender, EventArgs e)
        {
            m_resultAddress = null;
            View.Destroy();
        }

        #endregion
    }
}
