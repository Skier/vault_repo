using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MobileTech.Windows.UI.Controls;
using MobileTech.ServiceLayer;
using MobileTech.Windows.UI.ItemSearch;
using MobileTech.Domain;
using System.Globalization;


namespace MobileTech.Windows.UI.Inventory.Load
{
    public partial class InventoryLoadItemView : BaseForm
    {
        #region Fields
#if WINCE
        AutoDropDown m_autoDropDown = new AutoDropDown();
#endif
        #endregion

        #region Constructor

        public InventoryLoadItemView()
        {
            InitializeComponent();

            m_table.RowChanged += new RowHandler(OnRowChanged);

#if WINCE
            m_autoDropDown.Add(m_cbModes);
#endif
        }

        #endregion

        #region ApplyUIResources

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            OnApplyUIResources(cultureInfo);
        }

        protected virtual void OnApplyUIResources(CultureInfo cultureInfo)
        {
            Resources.Culture = cultureInfo;

            m_mbSave.Text = CommonResources.BtnDone;
            m_mbAddItem.Text = CommonResources.BtnAddItem;
        }

        #endregion

        #region ChangeMode

        private void ChangeMode(InventoryLoadEditMode mode)
        {
            if (m_model != null)
            {
                m_model.Mode = mode;

                //m_mbAddItem.Enabled = mode == InventoryLoadDataMode.Adjustments;
                m_lbTruckQty.Visible = mode != InventoryLoadEditMode.TruckStock;
            }
        }

        #endregion

        #region Event Handlers

        #region OnLoad
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            m_cbModes.Items.Add(new InventoryLoadMode(InventoryLoadEditMode.TruckStock));
            m_cbModes.Items.Add(new InventoryLoadMode(InventoryLoadEditMode.CurrentLoad));
            m_cbModes.Items.Add(new InventoryLoadMode(InventoryLoadEditMode.Adjustments));
            m_cbModes.Items.Add(new InventoryLoadMode(InventoryLoadEditMode.TotalLoad));


            m_cbModes.SelectedIndex = (int)m_model.Mode;

            if (m_model.DualInventory)
            {
                m_cbStorage.Items.Add(new Storage(StorageTypeEnum.Store));
                m_cbStorage.Items.Add(new Storage(StorageTypeEnum.Bin));

                if (m_model.StorageType == StorageTypeEnum.Bin)
                    m_cbStorage.SelectedIndex = 1;
                else
                    m_cbStorage.SelectedIndex = 0;
            }
            else
            {
                m_cbModes.Width = m_cbStorage.Right - m_cbModes.Left;
                m_cbStorage.Visible = false;
            }

            m_table.BindModel(m_model);

            m_table.SetColumnWidth(1, 50);

            m_table.Focus();

            if (m_table.Model.GetRowCount() > 0)
            {
                m_table.Select(0, 1);
                m_table.BeginEdit();
            }
        }

        #endregion

        #region OnAddItemClick

        private void OnAddItemClick(object sender, EventArgs e)
        {

            m_cbModes.SelectedIndex = 2; // Adjustments

            App.Execute(CommandName.SelectItem, m_model,false);

#if WINCE
            m_table.Focus();
#else
            m_table.Select();
#endif
        }

        #endregion

        #region OnModeSelectedIndexChanged

        private void OnModeSelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeMode((m_cbModes.SelectedItem as InventoryLoadMode).Key);
        }

        #endregion

        #region OnSaveClick
        private void OnSaveClick(object sender, EventArgs e)
        {
            try
            {
                m_table.ApplyEdit();

                using (WaitCursor waitCursor = new WaitCursor())
                {
                    m_model.Save();
                }

                Destroy();
            }
            catch (Exception ex)
            {
                EventService.AddEvent(
                    new MobileTechException(Resources.ExEnableToSaveLoad, 
                    ex ) ); 
            }
        }

        #endregion

        #region OnCancel

        protected override bool OnCancel()
        {
            // insert check on exist changes

            return base.OnCancel(CommonResources.MsgAllChangesWillBeLostExit);
        }

        #endregion

        #region OnClosing

        private void OnClosing(object sender, CancelEventArgs e)
        {
            App.Execute(CommandName.InventoryLoad);
        }

        #endregion

        #region OnBackClick

        private void OnBackClick(object sender, EventArgs e)
        {
            Destroy();
        }

        #endregion

        #region OnRowChanged

        void OnRowChanged(int rowIndex)
        {
            InventoryLoadItemModel.Data data = (InventoryLoadItemModel.Data)m_table.Model.GetObjectAt(rowIndex, 0);

            m_lbItemNumber.Text = String.Format(Resources.LbItemNumber,
                data.RouteInventory.ItemNumber);

            m_lbTruckQty.Text = String.Format(Resources.LbTruckQty, 
                data.TruckQty);
        }

        #endregion

        #region OnStorageSelectedIndexChanged

        private void OnStorageSelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_model != null)
                m_model.StorageType = ((Storage)m_cbStorage.SelectedItem).Key;

        }

        #endregion

        #endregion

        #region InitModel

        InventoryLoadItemModel m_model;

        public override void InitModel()
        {
            m_model = CreateModelInstance();

            m_model.Init();
        }

        protected virtual InventoryLoadItemModel CreateModelInstance()
        {
            throw new MobileTechException("Not implemented");
        }

        #endregion

        #region Helper classes

        class InventoryLoadMode
        {
            public InventoryLoadEditMode Key;

            public InventoryLoadMode(InventoryLoadEditMode key)
            {
                Key = key;
            }

            public override string ToString()
            {
                switch (Key)
                {
                    case InventoryLoadEditMode.TruckStock:
                        return Resources.TruckStock;
                    case InventoryLoadEditMode.CurrentLoad:
                        return Resources.CurrentLoad;
                    case InventoryLoadEditMode.Adjustments:
                        return Resources.Adjustments;
                    case InventoryLoadEditMode.TotalLoad:
                        return Resources.TotalLoad;
                }

                return base.ToString();
            }
        }


        class Storage
        {

            public StorageTypeEnum Key;

            public Storage(StorageTypeEnum key)
            {
                Key = key;
            }

            public override string ToString()
            {
                if (Key == StorageTypeEnum.Bin)
                    return CommonResources.EnmStorageTypeBin;

                return CommonResources.EnmStorageTypeStore;
            }
        }

        #endregion
    }
}