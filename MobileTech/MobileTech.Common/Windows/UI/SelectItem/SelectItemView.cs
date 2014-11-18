using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MobileTech.Data;
using System.Diagnostics;
using System.Globalization;

namespace MobileTech.Windows.UI.SelectItem
{
    public partial class SelectItemView : BaseForm
    {
        #region Fields

        SelectItemModel m_model;

        bool m_validateItemNumberRequired = true;

        #endregion

        #region Constructor

        public SelectItemView()
        {
            InitializeComponent();

            BindData(new SelectItemModel());
        }

        #endregion

        #region ApplyUIResources
        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            Resources.Culture = cultureInfo;

            m_lbAllowance.Text = Resources.Allowance;
            m_lbItemNumber.Text = Resources.ItemNumber;
            m_lbQuantity.Text = CommonResources.DcQuantity;
            m_lbTruckStock.Text = Resources.TruckStock;
            m_mbDone.Text = CommonResources.BtnDone;
            m_mbFind.Text = CommonResources.BtnFind;

            Text = Resources.Title;
        }
        #endregion

        #region IView

        public override void BindData(Object data)
        {
            if (data is ISelectItemListener)
                m_model.Listener = (ISelectItemListener)data;
            else if (data is SelectItemModel)
                m_model = (SelectItemModel)data;
            else
                throw new MobileTechInvalidModelExeption();
        }

        #endregion

        #region ApplyQuantity

        private bool ApplyQuantity()
        {
            try
            {
                m_model.Quantity = int.Parse(m_txQuantity.Text);
#if WINCE
                m_txItemNumber.Focus();
#else
                m_txItemNumber.Select();
#endif
            }
            catch (InvalidCastException)
            {

                MessageBox.Show(Resources.InvalidQuantityValue);
                return false;
            }

            m_model.Enter();

            return true;
        }

        #endregion

        #region RefreshUI

        private void RefreshUI()
        {
            if (m_model.SelectedItem != null)
            {
                m_txItemNumber.Text = m_model.SelectedItem.ItemNumber;
                m_txItemName.Text = m_model.SelectedItem.Name;
                m_txQuantity.Text = m_model.Quantity.ToString();
                m_txQuantity.Enabled = true;
                m_txQuantity.SelectAll();
            }
            else
            {
                m_txItemNumber.Text = String.Empty;
                m_txItemName.Text = String.Empty;
                m_txQuantity.Text = "0";

            }

        }

        #endregion

        #region SaveCheck

        private bool SaveCheck()
        {
            try
            {
                if (int.Parse(m_txQuantity.Text) != m_model.Quantity)
                {
                    if (MessageDialog.Show(MessageDialogType.Question,
                        CommonResources.MsgDoYouWantDiscardChanges) == DialogResult.No)
                        return false;
                }
            }
            catch (Exception)
            {
                // Do not anything
            }

            return true;
        }

        #endregion

        #region Event Handlers

        #region OnEnter

        private void OnEnter()
        {
            if (String.Empty.Equals(m_txItemNumber.Text))
                OnFind();
            else if (m_model.SelectedItem == null ||
                !m_model.SelectedItem.ItemNumber.Equals(m_txItemNumber.Text))
            {
                m_validateItemNumberRequired = false;

                try
                {
                    m_model.Search(m_txItemNumber.Text);

#if WINCE
                    m_txQuantity.Focus();
#else
                    m_txQuantity.Select();
#endif
                }
                catch (DataNotFoundException)
                {

                    MessageDialog.Show(MessageDialogType.Information,
                        Resources.ItemNotFound);
#if WINCE
                    m_txItemNumber.Focus();
#else
                    m_txItemNumber.Select();
#endif
                    m_txItemNumber.SelectAll();

                    return;
                }
                catch (MobileTechInvalidItemTypeException ex)
                {
                    MessageDialog.Show(MessageDialogType.Information,
                         String.Format(Resources.EnteredItemNumberIsButOnlyIsAllowed,
                        ex.PassedType.ToString(),
                        ex.AllowedType.ToString()));

#if WINCE
                    m_txItemNumber.Focus();
#else
                    m_txItemNumber.Select();
#endif
                    m_txItemNumber.SelectAll();

                    return;
                }
            }
            else
            {
                if (ApplyQuantity())
                    RefreshUI();
                else
                    return;
            }


            RefreshUI();
        }

        #endregion

        #region OnExitClick

        private void OnExitClick(object sender, EventArgs e)
        {
            if (m_model.SelectedItem != null)
            {
                if (!ApplyQuantity())
                    return;
                else
                    RefreshUI();
            }

            if (!SaveCheck())
                return;

            Destroy();
        }

        #endregion

        #region OnFindClick

        private void OnFindClick(object sender, EventArgs e)
        {
            OnFind();
        }

        #endregion

        #region OnFind

        private void OnFind()
        {
            if (!SaveCheck())
                return;

            App.Execute(CommandName.ItemSearch, m_model, false);


            if (m_model.SelectedItem != null)
            {

                RefreshUI();

                m_validateItemNumberRequired = false;

                m_txQuantity.Focus();
            }
        }

        #endregion

        #region OnItemNumberKeyDown

        private void OnItemNumberKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                OnEnter();
            }else
                m_validateItemNumberRequired = true;
        }

        #endregion

        #region OnQuantityKeyDown

        private void OnQuantityKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.Handled = true;

                OnEnter();
            }
        }

        #endregion

        #region OnQuantityFocus

        private void OnQuantityFocus(object sender, EventArgs e)
        {
            if (m_validateItemNumberRequired && 
                !String.Empty.Equals(m_txItemNumber.Text))
            {
                OnEnter();
            }

            m_txQuantity.SelectAll();

        }

        #endregion

        #region OnItemTextChanged

        private void OnItemTextChanged(object sender, EventArgs e)
        {
            m_txQuantity.Enabled = !String.Empty.Equals(m_txItemNumber.Text);
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

        #region OnCancel
        protected override bool OnCancel()
        {
            return !SaveCheck();
        }
        #endregion

        #endregion
    }
}