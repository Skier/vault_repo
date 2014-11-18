using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MobileTech.Domain;

namespace MobileTech.Windows.UI.Inventory.Unload
{
    public partial class InventoryUnloadItemView : BaseForm
    {
        #region Constructor

        public InventoryUnloadItemView()
        {
            InitializeComponent();
        }

        #endregion

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            OnApplyUIResources(cultureInfo);

        }

        protected virtual void OnApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            UnloadCommonResources.Culture = cultureInfo;

            Text = UnloadCommonResources.Title;

            m_mbAddItem.Text = CommonResources.BtnAddItem;
            m_mbSave.Text = CommonResources.BtnDone;
        }

        #region Event handlers

        #region OnLoad

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if(m_model.IsModeAllow(UnloadEditMode.Unload))
                m_cbMode.Items.Add(new Mode(UnloadEditMode.Unload));

            if(m_model.IsModeAllow(UnloadEditMode.Truck))
                m_cbMode.Items.Add(new Mode(UnloadEditMode.Truck));

            if(m_model.IsModeAllow(UnloadEditMode.Damage))
                m_cbMode.Items.Add(new Mode(UnloadEditMode.Damage));


            if(m_model.Mode == UnloadEditMode.Unload)
                m_cbMode.SelectedIndex = 0;
            else if (m_model.Mode == UnloadEditMode.Truck)
                m_cbMode.SelectedIndex = 1;
            else if (m_model.Mode == UnloadEditMode.Damage)
                m_cbMode.SelectedIndex = 2;

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
                m_cbMode.Width = m_cbStorage.Right - m_cbMode.Left;
                m_cbStorage.Visible = false;
            }

            m_table.BindModel(m_model);

            m_table.SetColumnWidth(1, 50);
        }

        #endregion

        #region OnSaveClick

        private void OnSaveClick(object sender, EventArgs e)
        {
            if(m_model != null)
                m_model.Save();

            App.Execute(CommandName.InventoryUnload);

            Destroy();
        }
        #endregion

        #region OnTableRowChanged

        private void OnTableRowChanged(int rowIndex)
        {
            m_lbItemNumber.Text = ((RouteInventory)m_table.Model.GetObjectAt(rowIndex, 0)).ItemNumber;
        }

        #endregion

        #region OnModeSelectedIndexChanged

        private void OnModeSelectedIndexChanged(object sender, EventArgs e)
        {
            if(m_model != null)
                m_model.Mode = ((Mode)m_cbMode.SelectedItem).Key;
        }

        #endregion

        #region OnStorageSelectedIndexChanged

        private void OnStorageSelectedIndexChanged(object sender, EventArgs e)
        {
            if(m_model != null)
                m_model.StorageType = ((Storage)m_cbStorage.SelectedItem).Key;
        }

        #endregion

        #region OnCancel

        protected override bool OnCancel()
        {
            bool cancel = base.OnCancel(CommonResources.MsgAllChangesWillBeLostExit);

            if(!cancel)
                App.Execute(CommandName.InventoryUnload);

            return cancel;
        }

        #endregion

        #region OnAddItemClick
        private void OnAddItemClick(object sender, EventArgs e)
        {
            App.Execute(CommandName.SelectItem, m_model, false);

#if WINCE
            m_table.Focus();
#else
            m_table.Select();
#endif
        }
        #endregion

        #region OnItemAffected

        void OnItemAffected(int index)
        {
            m_table.Select(index);
        }

        #endregion

        #endregion

        #region InitModel

        protected InventoryUnloadItemModel m_model;

        public override void InitModel()
        {
            m_model = CreateModelInstance();

            m_model.Init();

            m_model.ItemAffected += new SelectIndexEvent(OnItemAffected);
        }


        protected virtual InventoryUnloadItemModel CreateModelInstance()
        {
            throw new Exception("CreateModelInstance must be override");
        }

        #endregion

        #region CleanUp

        protected override void CleanUp()
        {
            if (m_model != null)
            {
                m_model.CleanUp();
                m_model = null;
            }
        }

        #endregion

        #region Helper classes

        class Mode
        {
            public UnloadEditMode Key;

            public Mode(UnloadEditMode key)
            {
                Key = key;
            }

            public override string ToString()
            {
                if (Key == UnloadEditMode.Damage)
                    return UnloadCommonResources.TruckDamaged;
                else if(Key == UnloadEditMode.Truck)
                    return UnloadCommonResources.TruckEnding;

                return UnloadCommonResources.Unload;
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