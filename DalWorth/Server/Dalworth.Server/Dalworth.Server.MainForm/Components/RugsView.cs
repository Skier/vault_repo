using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Domain;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using BaseControl=Dalworth.Server.Windows.BaseControl;

namespace Dalworth.Server.MainForm.Components
{
    public delegate void ItemsChangedHandler();

    public partial class RugsView : BaseControl
    {
        public event ItemsChangedHandler ItemsChanged;
        private ItemRecalcWrapper m_currentItem;

        private bool m_isItemsPopulating = false;

        #region IsEditable

        private bool m_isEditable;
        public bool IsEditable
        {
            get { return m_isEditable; }
            set
            {
                m_isEditable = value;
                m_gridRugs.UseEmbeddedNavigator = m_isEditable;
                ChangeMode();
            }
        }

        #endregion

        #region Items

        private BindingList<Item> m_items;
        public BindingList<Item> Items
        {
            get { return m_items; }
            set
            {
                if (DesignMode)
                    return;

                m_isItemsPopulating = true;

                m_items = value;                                
                if (m_items != null && m_items.Count > 0)
                    m_txtCleaningRate.EditValue = m_items[0].CleaningRate;
                else
                    m_txtCleaningRate.EditValue = Item.GetDefaultCleaningRate(IsPartOfFlood);                

                m_gridRugs.DataSource = Items;
                UpdateRugDetails();

                m_isItemsPopulating = false;
            }
        }

        #endregion        

        #region IsPartOfFlood

        private bool m_isPartOfFlood;
        public bool IsPartOfFlood
        {
            get { return m_isPartOfFlood; }
            set { m_isPartOfFlood = value; }
        }

        #endregion

        #region Constructor

        public RugsView()
        {
            InitializeComponent();

            m_gridRugsView.FocusedRowChanged += OnRugsFocusedRowChanged;
            m_cmbShape.SelectedIndexChanged += OnShapeChanged;
            m_txtHeight.EditValueChanged += OnHeightChanged;
            m_txtWidth.EditValueChanged += OnWidthChanged;
            m_txtDiameter.EditValueChanged += OnDiameterChanged;
            m_chkProtector.CheckedChanged += OnProtectorChanged;
            m_chkPadding.CheckedChanged += OnPaddingChanged;
            m_chkMothRepel.CheckedChanged += OnMothRepelChanged;
            m_chkRap.CheckedChanged += OnRapChanged;
            m_txtOtherCost.EditValueChanged += OnOtherCostChanged;


            m_gridRugsView.RowCountChanged += OnRowCountChanged;            
            m_gridRugs.KeyDown += OnGridRugsKeyDown;
            m_gridRugs.EmbeddedNavigator.ButtonClick += OnEmbeddedNavigatorButtonClick;

            m_cmbShape.KeyDown += AddRemoveRowHandler;
            m_txtWidth.KeyDown += AddRemoveRowHandler;
            m_txtHeight.KeyDown += AddRemoveRowHandler;
            m_txtDiameter.KeyDown += AddRemoveRowHandler;
            m_chkProtector.KeyDown += AddRemoveRowHandler;
            m_chkPadding.KeyDown += AddRemoveRowHandler;
            m_chkMothRepel.KeyDown += AddRemoveRowHandler;
            m_chkRap.KeyDown += AddRemoveRowHandler;
            m_txtOtherCost.KeyDown += AddRemoveRowHandler;            

            m_txtCleaningRate.TextChanged += OnCleaningRateChanged;
        }

        #endregion        

        #region OnCleaningRateChanged

        private void OnCleaningRateChanged(object sender, EventArgs e)
        {
            if (!m_isItemsPopulating && m_items != null)
            {
                foreach (Item item in m_items)
                {
                    ItemRecalcWrapper wrapper = new ItemRecalcWrapper(item);
                    wrapper.CleaningRate = (decimal) m_txtCleaningRate.EditValue;
                }
                                    
                m_items.ResetBindings();
                UpdateRugDetails();

                if (ItemsChanged != null)
                    ItemsChanged.Invoke();
            }
        }

        #endregion


        #region SetFocusToRugsGrid

        public void SetFocusToRugsGrid()
        {
            m_gridRugsView.Focus();
        }

        #endregion

        #region AddRemoveRowHandler

        private void AddRemoveRowHandler(object sender, KeyEventArgs e)
        {
            if (e.Alt && (e.KeyCode == Keys.Insert || e.KeyCode == Keys.Delete))
            {
                OnGridRugsKeyDown(null, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            if (e.Alt && e.KeyCode == Keys.Up)
            {
                if (m_gridRugsView.FocusedRowHandle > 0)
                {
                    m_gridRugsView.FocusedRowHandle--;
                    FocusSizeField();                        
                }                    
                
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            if (e.Alt && e.KeyCode == Keys.Down)
            {                
                m_gridRugsView.FocusedRowHandle++;
                FocusSizeField();

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        #endregion

        #region OnGridRugsKeyDown

        private void OnGridRugsKeyDown(object sender, KeyEventArgs e)
        {
            if (IsEditable && e.Alt)
            {
                if (e.KeyCode == Keys.Insert)
                {
                    AddNewRow();

                    e.Handled = true;
                    e.SuppressKeyPress = true;

                }
                else if (e.KeyCode == Keys.Delete)
                {
                    m_gridRugs.EmbeddedNavigator.Buttons.Remove.DoClick();

                    e.Handled = true;
                    e.SuppressKeyPress = true;                    
                }                
            }

        }

        #endregion

        #region AddNewRow

        private void AddNewRow()
        {
            m_gridRugsView.AddNewRow();
            m_gridRugsView.UpdateCurrentRow();
            m_txtWidth.Focus();
            m_txtWidth.SelectAll();            
        }

        private void FocusSizeField()
        {
            if (m_cmbShape.SelectedIndex == 0)
            {
                m_txtWidth.Focus();
                m_txtWidth.SelectAll();                            
            } else
            {
                m_txtDiameter.Focus();
                m_txtDiameter.SelectAll();                                            
            }
            
        }

        #endregion

        #region OnEmbeddedNavigatorButtonClick

        private void OnEmbeddedNavigatorButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == NavigatorButtonType.Append)
            {
                AddNewRow();
                e.Handled = true;
            }
        }

        #endregion

        #region OnRowCountChanged

        private void OnRowCountChanged(object sender, EventArgs e)
        {
            UpdateRugDetails();            
        }

        #endregion

        #region ChangeMode

        private void ChangeMode()
        {
            m_lblShape.Visible = m_isEditable;
            m_cmbShape.Visible = m_isEditable;
            m_lblRug.Visible = !m_isEditable;
            m_lblDimension.Visible = m_isEditable;
            m_txtWidth.Visible = m_isEditable;
            m_txtDiameter.Visible = m_isEditable;
            m_txtHeight.Visible = m_isEditable;
            m_lblX.Visible = m_isEditable;
            m_chkProtector.Visible = m_isEditable;
            m_chkPadding.Visible = m_isEditable;
            m_chkMothRepel.Visible = m_isEditable;
            m_chkRap.Visible = m_isEditable;
            m_txtOtherCost.Visible = m_isEditable;

            m_txtCleaningRate.Properties.ReadOnly = !m_isEditable;

            SwitchItemShape();
        }

        #endregion        

        #region UpdateRugDetails

        private void UpdateRugDetails()
        {
            int[] selectedRows = m_gridRugsView.GetSelectedRows();
            if (selectedRows == null || selectedRows.Length == 0 || Items.Count == 0)
            {
                SetEditControlsEnability(false);
                m_currentItem = null;
                PopulateCurrentItemOnUI();                
            }
            else
            {
                SetEditControlsEnability(true);
                Item item = (Item)m_gridRugsView.GetRow(selectedRows[0]);
                if (item == null)
                    m_currentItem = null;
                else
                {
                    ItemRecalcWrapper wrapper = new ItemRecalcWrapper(item);
                    wrapper.CleaningRate = (decimal) m_txtCleaningRate.EditValue;
                    m_currentItem = wrapper;
                }
                    
                PopulateCurrentItemOnUI();
            }
        }

        #endregion

        #region SetEditControlsEnability

        private void SetEditControlsEnability(bool enability)
        {
            m_cmbShape.Enabled = enability;
            m_txtDiameter.Enabled = enability;
            m_txtHeight.Enabled = enability;
            m_txtWidth.Enabled = enability;
            m_chkMothRepel.Enabled = enability;
            m_chkPadding.Enabled = enability;
            m_chkProtector.Enabled = enability;
            m_chkRap.Enabled = enability;
            m_txtOtherCost.Enabled = enability;            
        }

        #endregion

        #region OnRugsFocusedRowChanged

        private void OnRugsFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            UpdateRugDetails();            
        }

        #endregion

        #region User changes handlers

        private void OnOtherCostChanged(object sender, EventArgs e)
        {
            m_currentItem.OtherCost = (decimal)m_txtOtherCost.EditValue;
            PopulateCurrentItemOnUI();
        }

        private void OnRapChanged(object sender, EventArgs e)
        {
            m_currentItem.IsRapApplied = m_chkRap.Checked;
            PopulateCurrentItemOnUI();
        }

        private void OnMothRepelChanged(object sender, EventArgs e)
        {
            m_currentItem.IsMothRepelApplied = m_chkMothRepel.Checked;
            PopulateCurrentItemOnUI();
        }

        private void OnPaddingChanged(object sender, EventArgs e)
        {
            m_currentItem.IsPaddingApplied = m_chkPadding.Checked;
            PopulateCurrentItemOnUI();
        }

        private void OnProtectorChanged(object sender, EventArgs e)
        {
            m_currentItem.IsProtectorApplied = m_chkProtector.Checked;
            PopulateCurrentItemOnUI();
        }

        private void OnDiameterChanged(object sender, EventArgs e)
        {
            if (m_txtDiameter.EditValue.ToString() == string.Empty)
                m_currentItem.Diameter = 0;
            else
                m_currentItem.Diameter = decimal.Parse(m_txtDiameter.EditValue.ToString());
            PopulateCurrentItemOnUI();
        }

        private void OnWidthChanged(object sender, EventArgs e)
        {
            if (m_txtWidth.EditValue.ToString() == string.Empty)
                m_currentItem.Width = 0;
            else
                m_currentItem.Width = decimal.Parse(m_txtWidth.EditValue.ToString());
            PopulateCurrentItemOnUI();
        }

        private void OnHeightChanged(object sender, EventArgs e)
        {
            if (m_txtHeight.EditValue.ToString() == string.Empty)
                m_currentItem.Height = 0;
            else
                m_currentItem.Height = decimal.Parse(m_txtHeight.EditValue.ToString());
            PopulateCurrentItemOnUI();
        }

        private void OnShapeChanged(object sender, EventArgs e)
        {
            m_currentItem.ItemShape = (ItemShapeEnum)(m_cmbShape.SelectedIndex + 1);
            PopulateCurrentItemOnUI();
        }

        #endregion

        #region PopulateCurrentItemOnUI

        private bool m_isValuesPopulating;

        private void PopulateCurrentItemOnUI()
        {
            if (m_isValuesPopulating)
                return;

            m_isValuesPopulating = true;
            if (m_currentItem == null)
            {
                Item emptyItem = new Item();
                emptyItem.ItemShape = ItemShapeEnum.Rectangle;
                emptyItem.CleaningRate = (decimal) m_txtCleaningRate.EditValue;
                m_currentItem = new ItemRecalcWrapper(emptyItem);
                m_lblRug.Text = "NA";
            } else
                m_lblRug.Text = m_currentItem.ItemShortSpec;

            if (m_currentItem.NestedItem.ID == 0 && m_currentItem.ItemShape == null) //Newly added item
                m_currentItem.ItemShape = ItemShapeEnum.Rectangle;

            if (m_cmbShape.Visible)
               SwitchItemShape();
            
            m_lblCleanCost.Text = m_currentItem.CleanCost.ToString("C");
            m_lblProtectorCost.Text = m_currentItem.ProtectorCost.ToString("C");
            m_lblPaddingCost.Text = m_currentItem.PaddingCost.ToString("C");
            m_lblMothRepelCost.Text = m_currentItem.MothRepelCost.ToString("C");
            m_lblRapCost.Text = m_currentItem.RapCost.ToString("C");
            m_lblOtherCost.Text = m_currentItem.OtherCost.ToString("C");
            m_lblRugServiceCost.Text = m_currentItem.SubTotalCost.ToString("C");

            m_cmbShape.SelectedIndex = (int)m_currentItem.ItemShape.Value - 1;
            m_txtWidth.Text = Utils.RemoveTrailingZeros(m_currentItem.Width);
            m_txtHeight.EditValue = Utils.RemoveTrailingZeros(m_currentItem.Height);
            m_txtDiameter.EditValue = Utils.RemoveTrailingZeros(m_currentItem.Diameter);
            m_chkProtector.Checked = m_currentItem.IsProtectorApplied;
            m_chkPadding.Checked = m_currentItem.IsPaddingApplied;
            m_chkMothRepel.Checked = m_currentItem.IsMothRepelApplied;
            m_chkRap.Checked = m_currentItem.IsRapApplied;
            m_txtOtherCost.EditValue = m_currentItem.OtherCost;

            if (Items != null && Items.Count > 0)
            {
                Items.ResetItem(Items.IndexOf(m_currentItem.NestedItem));  
                if (ItemsChanged != null)
                    ItemsChanged.Invoke();
            }
                

            m_isValuesPopulating = false;
        }

        #endregion

        #region SwitchItemShape

        private void SwitchItemShape()
        {
            if (!IsEditable)
                return;

            if (m_currentItem != null)
            {
                if (m_currentItem.ItemShape == ItemShapeEnum.Rectangle)
                {
                    m_lblDimension.Text = "D&imension";
                    m_txtHeight.Visible = true;
                    m_txtWidth.Visible = true;
                    m_txtDiameter.Visible = false;
                    m_lblX.Visible = true;
                }
                else
                {
                    m_lblDimension.Text = "D&iameter";
                    m_txtHeight.Visible = false;
                    m_txtWidth.Visible = false;
                    m_txtDiameter.Visible = true;
                    m_lblX.Visible = false;
                }                   
            }
        }

        #endregion

    }
}
