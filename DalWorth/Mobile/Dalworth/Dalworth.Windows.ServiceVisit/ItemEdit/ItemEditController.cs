using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Domain;
using Dalworth.Domain.SyncService;
using Dalworth.Windows.ServiceVisit.ServiceVisit;
using Item=Dalworth.Domain.SyncService.Item;
using Task=Dalworth.Domain.SyncService.Task;

namespace Dalworth.Windows.ServiceVisit.ItemEdit
{
    public class ItemEditController : SingleFormController<ItemEditModel, ItemEditView>
    {
        private const decimal PRICE_PER_SQ_FOOT_CLEAN = 3;
        private const decimal PRICE_PER_SQ_FOOT_PROTECTOR = 1.15M;
        private const decimal PRICE_PER_SQ_FOOT_PADDING = 2;
        private const decimal PRICE_PER_SQ_FOOT_MOTH_REPEL = 0.35M;
        private const decimal PRICE_RAP = 8;

        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
            set { m_isCancelled = value; }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {            
            Model.Task = (TaskPackage) data[0];
            Model.RugAction = (RugAction) data[1];
            Model.CurrentRugIndex = (int) data[2];

            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_cmbShape.SelectedIndexChanged += OnShapeChanged;
            View.m_txtDiameter.TextChanged += OnDiameterChanged;
            View.m_txtHeight.TextChanged += OnHeightChanged;
            View.m_txtWidth.TextChanged += OnWidthChanged;

            View.m_chkProtector.CheckStateChanged += OnProtectorChanged;
            View.m_chkPadding.CheckStateChanged += OnPaddingChanged;
            View.m_chkMothRepel.CheckStateChanged += OnMothRepelChanged;
            View.m_chkRap.CheckStateChanged += OnRapChanged;
            View.m_curOther.TextChanged += OnOtherAmountChanged;
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            IsLeftActionExist = true;
            IsRightActionExist = true;
            LeftActionName = "Cancel";
            RightActionName = "Done";

            if (Model.RugAction == RugAction.Add)
            {
                View.Text = "Add Rug";
                LoadRug();
            }
            else if (Model.RugAction == RugAction.Edit)
            {
                View.Text = "Edit Rug";
                LoadRug();
            }
            else if (Model.RugAction == RugAction.View)
            {
                View.Text = "View Rug";
                LoadRug();
                View.m_cmbShape.Enabled = false;
                View.m_txtDiameter.Enabled = false;
                View.m_txtWidth.Enabled = false;
                View.m_txtHeight.Enabled = false;
                View.m_chkProtector.Enabled = false;
                View.m_chkPadding.Enabled = false;
                View.m_chkMothRepel.Enabled = false;
                View.m_chkRap.Enabled = false;
                View.m_curOther.Enabled = false;
            }

            View.m_cmbShape.Focus();            
        }

        #endregion

        #region OnShapeChanged

        private void OnShapeChanged(object sender, EventArgs e)
        {
            if (View.m_cmbShape.SelectedIndex == 0)
            {
                View.m_lblDimenstion.Visible = true;
                View.m_txtHeight.Visible = true;
                View.m_lblMultiply.Visible = true;
                View.m_txtWidth.Visible = true;

                View.m_lblDiameter.Visible = false;
                View.m_txtDiameter.Visible = false;

                Model.CurrentRug.ItemShapeId = (int) ItemShapeEnum.Rectangle;
                Model.CurrentRug.ItemShapeId = (int)Domain.ItemShapeEnum.Rectangle;
            }
            else
            {
                View.m_lblDimenstion.Visible = false;
                View.m_txtHeight.Visible = false;
                View.m_lblMultiply.Visible = false;
                View.m_txtWidth.Visible = false;

                View.m_lblDiameter.Visible = true;
                View.m_txtDiameter.Visible = true;

                Model.CurrentRug.ItemShapeId = (int) ItemShapeEnum.Round;
                Model.CurrentRug.ItemShapeId = (int)Domain.ItemShapeEnum.Round;
            }

            UpdateLabels();
        }

        #endregion

        #region LoadRug

        private void LoadRug()
        {
            if (Model.CurrentRug.ItemShapeId == (int) ItemShapeEnum.Rectangle)
                View.m_cmbShape.SelectedIndex = 0;
            else
                View.m_cmbShape.SelectedIndex = 1;

            if (Model.CurrentRug.Height != decimal.Zero)
                View.m_txtHeight.Text = Model.CurrentRug.Height.ToString("0.00");
            if (Model.CurrentRug.Width != decimal.Zero)
                View.m_txtWidth.Text = Model.CurrentRug.Width.ToString("0.00");
            if (Model.CurrentRug.Diameter != decimal.Zero)
                View.m_txtDiameter.Text = Model.CurrentRug.Diameter.ToString("0.00");

            View.m_chkProtector.Checked = Model.CurrentRug.IsProtectorApplied;
            View.m_chkPadding.Checked = Model.CurrentRug.IsPaddingApplied;
            View.m_chkMothRepel.Checked = Model.CurrentRug.IsMothRepelApplied;
            View.m_chkRap.Checked = Model.CurrentRug.IsRapApplied;
            View.m_curOther.Value = Model.CurrentRug.OtherCost;

            UpdateLabels();
        }

        #endregion

        #region UpdateLabels

        private void UpdateLabels()
        {
            decimal squareFootage = decimal.Zero;

            if (Model.CurrentRug.ItemShapeId == (int) ItemShapeEnum.Rectangle)
                squareFootage = Model.CurrentRug.Height * Model.CurrentRug.Width;
            else if (Model.CurrentRug.ItemShapeId == (int) ItemShapeEnum.Round)
                squareFootage = (decimal)Math.PI * (Model.CurrentRug.Diameter * Model.CurrentRug.Diameter / 4);

            if (Model.RugAction != RugAction.View)
            {
                Model.CurrentRug.CleanCost = squareFootage * PRICE_PER_SQ_FOOT_CLEAN;
                Model.CurrentRug.ProtectorCost = Model.CurrentRug.IsProtectorApplied ? squareFootage * PRICE_PER_SQ_FOOT_PROTECTOR : decimal.Zero;
                Model.CurrentRug.PaddingCost = Model.CurrentRug.IsPaddingApplied ? squareFootage * PRICE_PER_SQ_FOOT_PADDING : decimal.Zero;
                Model.CurrentRug.MothRepelCost = Model.CurrentRug.IsMothRepelApplied ? squareFootage * PRICE_PER_SQ_FOOT_MOTH_REPEL : decimal.Zero;
                Model.CurrentRug.RapCost = Model.CurrentRug.IsRapApplied ? PRICE_RAP : decimal.Zero;
                Model.CurrentRug.SubTotalCost = Model.CurrentRug.CleanCost + Model.CurrentRug.ProtectorCost
                    + Model.CurrentRug.PaddingCost + Model.CurrentRug.MothRepelCost
                    + Model.CurrentRug.RapCost + Model.CurrentRug.OtherCost;
                Model.CurrentRug.TaxCost = Model.CurrentRug.SubTotalCost*Application.TAX_PERCENT;
                Model.CurrentRug.TotalCost = Model.CurrentRug.SubTotalCost + Model.CurrentRug.TaxCost;                
            }

            View.m_lblSquareFootage.Text = squareFootage.ToString("0.00") + " SF";
            View.m_lblCleanCost.Text = Model.CurrentRug.CleanCost.ToString("C");
            View.m_lblProtectorCost.Text = Model.CurrentRug.ProtectorCost.ToString("C");
            View.m_lblPaddingCost.Text = Model.CurrentRug.PaddingCost.ToString("C");
            View.m_lblMothRepelCost.Text = Model.CurrentRug.MothRepelCost.ToString("C");
            View.m_lblRapCost.Text = Model.CurrentRug.RapCost.ToString("C");
            View.m_lblTotalCost.Text = Model.CurrentRug.SubTotalCost.ToString("C");
            View.m_lblTaxAmount.Text = Model.CurrentRug.TaxCost.ToString("C");
            View.m_lblTotal.Text = Model.CurrentRug.TotalCost.ToString("C");
            View.m_lblTaskTotal.Text = (Model.CurrentRug.TotalCost + Model.VisitTotalWithoutCurrentRug).ToString("C");
        }

        #endregion

        #region OnSave

        protected override bool OnSave()
        {
            IsCancelled = true;
            return true;
        }

        #endregion

        #region OnLeftAction

        public override void OnLeftAction()
        {
            IsCancelled = true;
            View.Destroy();
        }

        #endregion

        #region OnRightAction

        public override void OnRightAction()
        {
            if (Model.RugAction == RugAction.View)
            {
                IsCancelled = true;
                View.Destroy();
                return;
            }
                

            if (Model.CurrentRug.ItemShapeId == (int) ItemShapeEnum.Round)
            {
                if (Model.CurrentRug.Diameter == decimal.Zero)
                {
                    MessageDialog.Show(MessageDialogType.Warning, "Please enter Diameter");
                    View.m_txtDiameter.SelectAll();
                    View.m_txtDiameter.Focus();
                    return;
                }
            }
            else
            {
                if (Model.CurrentRug.Width == decimal.Zero)
                {
                    MessageDialog.Show(MessageDialogType.Warning, "Please enter Width");
                    View.m_txtWidth.SelectAll();
                    View.m_txtWidth.Focus();
                    return;
                }
                else if (Model.CurrentRug.Height == decimal.Zero)
                {
                    MessageDialog.Show(MessageDialogType.Warning, "Please enter Height");
                    View.m_txtHeight.SelectAll();
                    View.m_txtHeight.Focus();
                    return;
                }
            }
            
            if (Model.IsRugPickup)
            {
                List<Item> items = new List<Item>(Model.Task.Items);
                if (Model.RugAction == RugAction.Add)
                    items.Add(Model.CurrentRug);                    
                else if (Model.RugAction == RugAction.Edit)
                    items[Model.CurrentRugIndex] = Model.CurrentRug;

                Model.Task.Items = items.ToArray();
                View.Destroy();
            }
            
        }

        #endregion

        #region OnDiameterChanged

        private void OnDiameterChanged(object sender, EventArgs e)
        {
            if (View.m_txtDiameter.Text.Length != 0)
            {
                try
                {
                    decimal.Parse(View.m_txtDiameter.Text);
                }
                catch (Exception)
                {
                    MessageDialog.Show(MessageDialogType.Warning, "Please enter correct Diameter");
                    View.m_txtDiameter.SelectAll();
                    View.m_txtDiameter.Focus();
                    return;
                }
                Model.CurrentRug.Diameter = decimal.Parse(View.m_txtDiameter.Text);
            }
            else
            {
                Model.CurrentRug.Diameter = decimal.Zero;
            }

            UpdateLabels();
        }

        #endregion

        #region OnHeightChanged

        private void OnHeightChanged(object sender, EventArgs e)
        {
            if (View.m_txtHeight.Text.Length != 0)
            {
                try
                {
                    decimal.Parse(View.m_txtHeight.Text);
                }
                catch (Exception)
                {
                    MessageDialog.Show(MessageDialogType.Warning, "Please enter correct Height");
                    View.m_txtHeight.SelectAll();
                    View.m_txtHeight.Focus();
                    return;
                }
                Model.CurrentRug.Height = decimal.Parse(View.m_txtHeight.Text);
            }
            else
            {
                Model.CurrentRug.Height = decimal.Zero;
            }

            UpdateLabels();
        }

        #endregion

        #region OnWidthChanged

        private void OnWidthChanged(object sender, EventArgs e)
        {
            if (View.m_txtWidth.Text.Length != 0)
            {
                try
                {
                    decimal.Parse(View.m_txtWidth.Text);
                }
                catch (Exception)
                {
                    MessageDialog.Show(MessageDialogType.Warning, "Please enter correct Width");
                    View.m_txtWidth.SelectAll();
                    View.m_txtWidth.Focus();
                    return;
                }
                Model.CurrentRug.Width = decimal.Parse(View.m_txtWidth.Text);
            }
            else
            {
                Model.CurrentRug.Width = decimal.Zero;
            }

            UpdateLabels();
        }

        #endregion

        #region OnProtectorChanged

        private void OnProtectorChanged(object sender, EventArgs e)
        {
            Model.CurrentRug.IsProtectorApplied = View.m_chkProtector.Checked;
            UpdateLabels();
        }

        #endregion

        #region OnPaddingChanged

        private void OnPaddingChanged(object sender, EventArgs e)
        {
            Model.CurrentRug.IsPaddingApplied = View.m_chkPadding.Checked;
            UpdateLabels();
        }

        #endregion

        #region OnMothRepelChanged

        private void OnMothRepelChanged(object sender, EventArgs e)
        {
            Model.CurrentRug.IsMothRepelApplied = View.m_chkMothRepel.Checked;
            UpdateLabels();
        }

        #endregion

        #region OnRapChanged

        private void OnRapChanged(object sender, EventArgs e)
        {
            Model.CurrentRug.IsRapApplied = View.m_chkRap.Checked;
            UpdateLabels();
        }

        #endregion

        #region OnOtherAmountChanged

        private void OnOtherAmountChanged(object sender, EventArgs e)
        {
            Model.CurrentRug.OtherCost = View.m_curOther.Value.Value;
            UpdateLabels();
        }

        #endregion
    }
}
