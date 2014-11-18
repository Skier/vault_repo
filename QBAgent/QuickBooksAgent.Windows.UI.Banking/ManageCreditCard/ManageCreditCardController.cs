using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using QuickBooksAgent.Domain;
using QuickBooksAgent.Windows.UI.Banking.CreditCardCharges;
using QuickBooksAgent.Windows.UI.Controls;

namespace QuickBooksAgent.Windows.UI.Banking.ManageCreditCard
{
    public class ManageCreditCardController : SingleFormController<ManageCreditCardModel, ManageCreditCardView>
    {
        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.CurrentAccount = (Account)data[0];

            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_table.RowChanged += new RowHandler(OnTableRowChanged);
            View.m_table.Enter += new CellValueHandler(OnTableEnter);

            View.m_menuAddCard.Click += new EventHandler(OnAddCardClick);
            View.m_menuViewEditCard.Click += new EventHandler(OnViewEditClick);
            View.m_menuDeleteCard.Click += new EventHandler(OnDeleteClick);
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            base.OnViewLoad();

            View.m_lblAccount.Text = Model.CurrentAccount.Name;

            View.m_table.BindModel(Model);
            View.m_table.AddColumn(new TableColumn(0, 0, new CardTableCellRenderer(),
                                                   null, new CardTableHeaderCellRenderer()));
            View.m_table.AddColumn(new TableColumn(1, 0, new CardTableCellRenderer(),
                                                   null, new CardTableHeaderCellRenderer()));
            View.m_table.AddColumn(new TableColumn(2, 0, new CardTableCellRenderer(),
                                                   null, new CardTableHeaderCellRenderer()));

            View.m_table.GetColumn(0).Width = 10;
            View.m_table.GetColumn(2).Width = 80;

            View.m_table.Focus();
            if (Model.GetRowCount() > 0)
            {
                View.m_table.Select(0, 1);
            }

            UpdateMenuItems();
        }

        #endregion

        #region OnTableRowChanged

        private void OnTableRowChanged(int rowIndex)
        {
            UpdateMenuItems();
        }

        #endregion

        #region OnTableEnter

        private void OnTableEnter(TableCell cell)
        {
            if (View.m_menuViewEditCard.Enabled)
                OnViewEditClick(this, EventArgs.Empty);
        }

        #endregion

        #region OnDefaultAction

        public override void OnDefaultAction()
        {
            OnViewEditClick(this, EventArgs.Empty);
        }

        #endregion

        #region OnAddCardClick

        private void OnAddCardClick(object sender, EventArgs e)
        {                        
            CreditCardController creditCardController
                = SingleFormController.Prepare<CreditCardController>(
                    EnteredFromEnum.ManageCreditCards, null, false, Model.CurrentAccount);
            creditCardController.Closed += new SingleFormClosedHandler(OnAddCardClosed);
            creditCardController.Execute();
        }

        #endregion

        #region OnAddCardClosed

        private void OnAddCardClosed(SingleFormController controller)
        {
            View.m_table.Focus();

            CreditCardController creditCardController = (CreditCardController)controller;
            if (!creditCardController.IsCancelled)
            {
                Model.AddInList(creditCardController.CreatedCard);
                View.m_table.Select(0, 1);
            }

            UpdateMenuItems();
        }

        #endregion

        #region OnViewEditClick

        private void OnViewEditClick(object sender, EventArgs e)
        {
            CreditCardController creditCardController
                = SingleFormController.Prepare<CreditCardController>(
                    EnteredFromEnum.ManageCreditCards,
                    Model.GetObjectAt(View.m_table.CurrentRowIndex, 0),
                    View.m_menuViewEditCard.Text == "View" ? true : false,
                    Model.CurrentAccount);

            creditCardController.Closed += new SingleFormClosedHandler(OnViewEditCardClosed);
            creditCardController.Execute();
        }

        #endregion

        #region OnViewEditCardClosed

        private void OnViewEditCardClosed(SingleFormController controller)
        {
            CreditCardController creditCardController = (CreditCardController)controller;
            if (!creditCardController.IsCancelled)
                Model.ChangeInList(creditCardController.CreatedCard);

            View.m_table.Focus();
            UpdateMenuItems();
        }

        #endregion

        #region OnDeleteClick

        private void OnDeleteClick(object sender, EventArgs e)
        {
            if (MessageDialog.Show(MessageDialogType.Question,
                "Do you really want to delete credit card charge")
                    == DialogResult.No)
                return;

            Model.Delete((CreditCard)Model.GetObjectAt(View.m_table.CurrentRowIndex, 0));
            UpdateMenuItems();
        }

        #endregion

        #region UpdateMenuItems

        private void UpdateMenuItems()
        {
            if (View.m_table.CurrentRowIndex < 0 || Model.GetRowCount() == 0)
            {
                View.m_menuViewEditCard.Text = "View";
                DefaultActionName = View.m_menuViewEditCard.Text;
                IsDefaultActionExist = false;
                View.m_menuDeleteCard.Enabled = false;
                View.m_menuViewEditCard.Enabled = false;
                return;
            }

            CreditCard card
                = (CreditCard)Model.GetObjectAt(View.m_table.CurrentRowIndex, 0);

            if (card.EntityState == EntityState.Created)
            {
                View.m_menuViewEditCard.Text = "Edit";
                View.m_menuDeleteCard.Enabled = true;
            }
            else
            {
                View.m_menuViewEditCard.Text = "View";
                View.m_menuDeleteCard.Enabled = false;
            }

            DefaultActionName = View.m_menuViewEditCard.Text;
            View.m_menuViewEditCard.Enabled = true;
            IsDefaultActionExist = true;
        }

        #endregion                
    }
}
