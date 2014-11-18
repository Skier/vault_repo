using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MobileTech.Data;
using MobileTech.Domain;
using System.Globalization;

namespace MobileTech.Windows.UI.ItemSearch
{
    public partial class ItemSearchView : BaseForm
    {
        #region Fields

        ItemSearchModel m_model;
#if WINCE
        Microsoft.WindowsCE.Forms.InputPanel m_sip;
#endif
        #endregion

        #region Constructor

        public ItemSearchView()
        {
            InitializeComponent();
#if WINCE
            WinAPI.SIPAllowSuggestions(false);

            m_sip = new Microsoft.WindowsCE.Forms.InputPanel();

            m_sip.EnabledChanged += new System.EventHandler(this.OnSipEnabledChanged);

            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnClosing);

            this.m_txtSearch.GotFocus += new System.EventHandler(this.OnSearchFocus);

#endif

            m_model = new ItemSearchModel();

            m_table.BindModel(m_model);

            m_table.SetColumnWidth(0, 50);

            m_model.CategoryChanged += new CategoryChangedHandler(OnCategoryChanged);


        }
        #endregion

        #region ApplyUIResources
        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            Resources.Culture = cultureInfo;

            m_mbSelect.Text = CommonResources.BtnSelect;

            m_rbSearchByName.Text = Resources.SearchName;
            m_rbSearchByNumber.Text = Resources.SearchNumber;
            Text = Resources.Title9000Search;
        }
        #endregion

        #region SelectCategory

        private void SelectCategory()
        {
            CategorySearchView categorySearch = new CategorySearchView(m_model);
            categorySearch.ShowDialog();
        }

        #endregion

        #region Search

        void Search()
        {
            int selectIndex = m_model.Search(m_txtSearch.Text);

            if (selectIndex != -1)
                m_table.Select(selectIndex);
        }

        #endregion

        #region Event Handlers

        #region OnLoad

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            int itemCategoryCount = m_model.GetItemType() == ItemTypeEnum.Equipment ?
                ItemCategory.EquipmentCategoryCount : ItemCategory.ProductCategoryCount;

            if (itemCategoryCount == 1)
            {
                m_model.Category = ItemCategory.Find(ItemFieldName.Name,
                    m_model.GetItemType())[0];
            }
            else
            {
                SelectCategory();
            }

            if (m_model.Category == null)
            {
                Destroy();
                return;
            }

            m_table.Select(0);

            m_rbSearchByName.Checked = m_model.Mode == ItemFieldName.Name;
            m_rbSearchByNumber.Checked = !m_rbSearchByName.Checked;

#if WINCE
            m_txtSearch.Focus();
#else
            m_txtSearch.Select();
#endif

        }

        #endregion

        #region OnCategoryChanged

        void OnCategoryChanged()
        {
            m_linkCategory.Text = m_model.Category.Name;

            Search();

            m_table.Select(0);
        }

        #endregion

        #region OnSearchKeyDown

        private void OnSearchKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Return)
            {
                m_table.Focus();
            }
            else if (e.KeyData == Keys.Up)
            {
                m_table.Focus();
                m_table.MoveBack();
            }
            else if (e.KeyData == Keys.Down)
            {
                m_table.Focus();
                m_table.MoveNext();

            }
            else if (e.KeyData == Keys.PageUp)
            {
                m_table.Focus();
                m_table.MoveBackPage();
            }
            else if (e.KeyData == Keys.PageDown)
            {
                m_table.Focus();
                m_table.MoveNextPage();
            }
        }

        #endregion

        #region OnRowChanged

        private void OnRowChanged(int rowIndex)
        {
            if (rowIndex >= 0)
                m_model.SelectedItem = (Item)m_model.GetObjectAt(rowIndex, 0);
        }

        #endregion

        #region OnSelectClick

        private void OnSelectClick(object sender, EventArgs e)
        {
            OnSelect();
        }

        #endregion

        #region OnSelect

        private void OnSelect()
        {
            if (m_model.SelectedItem != null)
            {
                m_model.Listener.SetItem(m_model.SelectedItem);

                Destroy();
            }
            else
                MessageDialog.Show(MessageDialogType.Information,
                    Resources.MsgPleaseSelectItem);
        }

        #endregion

        #region OnModeChange

        private void OnModeChange(object sender, EventArgs e)
        {

            ItemFieldName mode = m_rbSearchByName.Checked ? ItemFieldName.Name :
                ItemFieldName.Number;

            if (mode != m_model.Mode)
            {
                m_model.Mode = mode;

                Search(); 
            }

#if WINCE
            m_txtSearch.Focus();
#else
            m_txtSearch.Select();
#endif


        }

        #endregion

        #region OnTextChanged

        private void OnTextChanged(object sender, EventArgs e)
        {
            Search();
        }

        #endregion

#if WINCE

        #region OnSipEnabledChanged
        private void OnSipEnabledChanged(object sender, EventArgs e)
        {
            if (m_sip.Enabled)
            {
                m_table.Height = Height - m_table.Top - m_sip.Bounds.Height - 24;
            }
            else
                m_table.Height = Height - m_table.Top;
        }
        #endregion

        #region OnSearchFocus

        private void OnSearchFocus(object sender, EventArgs e)
        {
            m_sip.Enabled = m_model.Mode == ItemFieldName.Name;

        }

        #endregion

        #region OnClosing
        private void OnClosing(object sender, CancelEventArgs e)
        {
            m_sip.Enabled = false;

            WinAPI.SIPAllowSuggestions(true);
        }
        #endregion

#endif

        #region OnTableEnter

        private void OnTableEnter(MobileTech.Windows.UI.Controls.TableCell cell)
        {
            OnSelect();
        }

        #endregion

        #region OnEnterPress

        private void OnEnterPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
            }
        }

        #endregion

        #region OnCategoryClick

        private void OnCategoryClick(object sender, EventArgs e)
        {
            SelectCategory();
        }

        #endregion

        #endregion

        #region BindData

        public override void BindData(Object data)
        {
            if (data is IItemSearchListener)
                m_model.Listener = (IItemSearchListener)data;
            else if (data is ItemSearchModel)
                m_model = (ItemSearchModel)data;
            else
                throw new MobileTechInvalidModelExeption();
        }

        #endregion

    }
}