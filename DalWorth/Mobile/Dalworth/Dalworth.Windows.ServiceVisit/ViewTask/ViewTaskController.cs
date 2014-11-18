using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Dalworth.Controls;
using Dalworth.Data;
using Dalworth.Domain;
using Dalworth.Domain.SyncService;
using Dalworth.SDK;
using Dalworth.Windows.ServiceVisit.ItemEdit;
using Dalworth.Windows.ServiceVisit.NoGo;
using Dalworth.Windows.ServiceVisit.SubmitEtc;
using Dalworth.Windows.ServiceVisit.VisitReceipt;
using Microsoft.WindowsMobile.Telephony;
using Item=Dalworth.Domain.SyncService.Item;
using WorkTransaction=Dalworth.Domain.WorkTransaction;

namespace Dalworth.Windows.ServiceVisit.ViewTask
{
    public class ViewTaskController : SingleFormController<ViewTaskModel, ViewTaskView>
    {
        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.Task = (TaskPackage)data[0];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_tblRugs.RowChanged += new RowHandler(OnTableRowChanged);
            View.m_tblRugs.SelectionChanged += new SelectionHandler(OnTableSelectionChanged);

            View.m_menuAddRug.Click += OnAddRugClick;
            View.m_menuEditRug.Click += OnEditRugClick;
            View.m_menuDeleteRug.Click += OnDeleteRugClick;
            View.m_menuViewRug.Click += OnViewRugClick;
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            IsRightActionExist = true;
            IsLeftActionExist = true;
            RightActionName = "Menu";
            LeftActionName = "Close";

            if (Model.IsRugPickup)
            {
                View.Text = "View Task - Rug Pickup";
                View.m_tblRugs.AddColumn(new TableColumn(0, 20, new SelectionRenderer()));
                View.m_tblRugs.AddColumn(new TableColumn(1));
                View.m_tblRugs.AddColumn(new TableColumn(2, 60));
                View.m_tblRugs.MultipleSelection = true;
            }
            else
            {
                View.Text = "View Task - Rug Delivery";
                View.m_tblRugs.AddColumn(new TableColumn(0));
                View.m_tblRugs.AddColumn(new TableColumn(1, 60));
                View.m_tblRugs.MultipleSelection = false;
            }

            View.m_tblRugs.BindModel(Model);

            CheckAllRows();

            UpdateMenus();
            UpdateRugsTotal();
            View.m_tblRugs.Focus();
        }

        #endregion

        #region OnSave

        protected override bool OnSave()
        {
            return false;
        }

        #endregion

        #region OnLeftAction

        public override void OnLeftAction()
        {
            View.Destroy();
        }

        #endregion

        #region CheckAllRows

        private void CheckAllRows()
        {
            List<int> selectionIndexes = new List<int>();

            for (int i = 0; i < Model.Task.Items.Length; i++)
                selectionIndexes.Add(i);

            View.m_tblRugs.Select(selectionIndexes);
            if (View.m_tblRugs.Model.GetRowCount() > 0)
            {
                if (Model.IsRugPickup)
                    View.m_tblRugs.Select(0, 1);
                else
                    View.m_tblRugs.Select(0, 0);
            }
        }

        #endregion

        #region OnTableSelectionChanged

        private void OnTableSelectionChanged()
        {
            UpdateRugsTotal();
        }

        #endregion

        #region OnTableRowChanged

        private void OnTableRowChanged(int rowIndex)
        {
            UpdateMenus();
        }

        #endregion

        #region UpdateRugsTotal

        private decimal GetRugsTotal(bool isIncludeTax)
        {
            decimal rugsTotal = decimal.Zero;

            for (int i = 0; i < Model.Task.Items.Length; i++)
            {
                if ((Model.IsRugPickup && View.m_tblRugs.IsRowSelected(i)) || !Model.IsRugPickup)
                {
                    if (isIncludeTax)
                        rugsTotal += Model.Task.Items[i].TotalCost;
                    else
                        rugsTotal += Model.Task.Items[i].SubTotalCost;
                }                
            }

            return rugsTotal;
        }

        private void UpdateRugsTotal()
        {
            decimal subTotal = GetRugsTotal(false);
            decimal total = GetRugsTotal(true);

            View.m_lblTaskSubTotal.Text = subTotal.ToString("C");
            View.m_lblTaskTax.Text = (total - subTotal).ToString("C");
            View.m_lblTaskTotal.Text = total.ToString("C");
        }

        #endregion

        #region UpdateMenus

        private void UpdateMenus()
        {
            if (Model.IsUnknownTaskType)
            {
                View.m_menuAddRug.Enabled = false;
                View.m_menuEditRug.Enabled = false;
                View.m_menuDeleteRug.Enabled = false;
                View.m_menuViewRug.Enabled = false;
                return;
            }

            if (View.m_tblRugs.Model.GetRowCount() > 0 && View.m_tblRugs.CurrentRowIndex >= 0)
            {
                if (Model.IsRugPickup)
                {
                    View.m_menuAddRug.Enabled = true;
                    View.m_menuEditRug.Enabled = true;
                    View.m_menuDeleteRug.Enabled = true;
                    View.m_menuViewRug.Enabled = true;
                }
                else
                {
                    View.m_menuAddRug.Enabled = false;
                    View.m_menuEditRug.Enabled = false;
                    View.m_menuDeleteRug.Enabled = false;
                    View.m_menuViewRug.Enabled = true;
                }
            }
            else
            {
                if (Model.IsRugPickup)
                {
                    View.m_menuAddRug.Enabled = true;
                    View.m_menuEditRug.Enabled = false;
                    View.m_menuViewRug.Enabled = false;
                    View.m_menuDeleteRug.Enabled = false;
                }
                else
                {
                    View.m_menuAddRug.Enabled = false;
                    View.m_menuEditRug.Enabled = false;
                    View.m_menuViewRug.Enabled = false;
                    View.m_menuDeleteRug.Enabled = false;
                }
            }
        }

        #endregion

        #region OnAddRugClick

        private void OnAddRugClick(object sender, EventArgs e)
        {
            ItemEditController controller = Prepare<ItemEditController>(Model.Task, RugAction.Add, -1);
            controller.Closed += OnAddRugClosed;
            controller.Execute();
        }

        private void OnAddRugClosed(SingleFormController controller)
        {
            ItemEditController editController = (ItemEditController)controller;
            if (!editController.IsCancelled)
            {                
                if (Model.Task.Items.Length == 1)
                    View.m_tblRugs.BindModel(Model);

                List<int> selectionIndexes = new List<int>();
                selectionIndexes.Add(Model.Task.Items.Length - 1);
                View.m_tblRugs.Select(selectionIndexes);

                View.m_tblRugs.Select(Model.Task.Items.Length, 1);                
                UpdateRugsTotal();
            }

            View.m_tblRugs.Focus();
            UpdateMenus();
        }

        #endregion

        #region OnEditRugClick

        private void OnEditRugClick(object sender, EventArgs e)
        {
            if (View.m_tblRugs.CurrentRowIndex >= 0)
            {
                ItemEditController controller =
                    Prepare<ItemEditController>(Model.Task, RugAction.Edit, View.m_tblRugs.CurrentRowIndex);
                controller.Closed += OnEditRugClosed;
                controller.Execute();
            }
        }

        private void OnEditRugClosed(SingleFormController controller)
        {
            ItemEditController editController = (ItemEditController)controller;
            if (!editController.IsCancelled)
            {
                View.m_tblRugs.Update();                
                UpdateRugsTotal();
            }

            View.m_tblRugs.Focus();
            UpdateMenus();
        }

        #endregion

        #region OnDeleteRugClick

        private void OnDeleteRugClick(object sender, EventArgs e)
        {
            if (View.m_tblRugs.CurrentRowIndex >= 0)
            {
                if (MessageBox.Show("Do you want to delete this Rug?", "Confirmation",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    return;
                }

                List<Item> itemsList = new List<Item>(Model.Task.Items);
                itemsList.RemoveAt(View.m_tblRugs.CurrentRowIndex);
                Model.Task.Items = itemsList.ToArray();

                if (Model.Task.Items.Length > 0)
                    View.m_tblRugs.Select(0, 1);
                View.m_tblRugs.Update();
                View.m_tblRugs.Focus();
                UpdateRugsTotal();
                UpdateMenus();
            }
        }

        #endregion

        #region OnViewRugClick

        private void OnViewRugClick(object sender, EventArgs e)
        {
            if (View.m_tblRugs.CurrentRowIndex >= 0)
            {
                ItemEditController controller =
                    Prepare<ItemEditController>(Model.Task, RugAction.View, View.m_tblRugs.CurrentRowIndex);
                controller.Closed += OnViewRugClosed;
                controller.Execute();
            }
        }

        private void OnViewRugClosed(SingleFormController controller)
        {
            View.m_tblRugs.Focus();
        }

        #endregion
    }
}
