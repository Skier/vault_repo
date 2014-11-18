using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MobileTech.Domain;
using MobileTech.Windows.UI.Controls;


namespace MobileTech.Windows.UI.CustomerOperations.CustomerSelection
{
	public partial class CustomerSelectionView : BaseForm
    {
        #region Constructor

        public CustomerSelectionView()
		{
			InitializeComponent();

            m_table.AddColumn(new TableColumn(0,50,new StatusRenderer()));
		}

        #endregion

        #region ApplyUIResources

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            Resources.Culture = cultureInfo;

            Text = Resources.Title3010;

            m_mbSelect.Text = CommonResources.BtnSelect;
        }

        #endregion

        #region Event Handlers

        #region OnLoad
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            m_table.BindModel(m_model);

            SelectCurrentCustomer();
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
            if (m_table.CurrentRowIndex < 0)
            {
                MessageDialog.Show(MessageDialogType.Information,
                    Resources.PleaseSelectCustomer);

                return;
            }

            RouteScheduleQueue routeScheduleQueue = (RouteScheduleQueue)m_table.Model.GetObjectAt(
                m_table.CurrentRowIndex, 0);

            App.Execute(CommandName.CustomerOperations, 
                new CustomerOperationsCommonData(routeScheduleQueue));
   
            Destroy();
        }

        #endregion

        #region OnExitClick

        private void OnExitClick(object sender, EventArgs e)
        {
            Destroy();
        }

        #endregion

        #region OnCancel

        protected override bool OnCancel()
        {
            App.Execute(CommandName.MainMenu);

            return false;
        }

        #endregion

        #region OnTableEnter

        private void OnTableEnter(TableCell cell)
        {
            OnSelect();
        }

        #endregion

        #endregion

        #region SelectCurrentCustomer

        private void SelectCurrentCustomer()
        {
            if (m_table.Model == null)
                return;

            for (int i = 0; i < m_table.Model.GetRowCount(); i++)
            {

                RouteScheduleQueue item =
                    (RouteScheduleQueue)m_table.Model.GetObjectAt(i, 0);

                if (!item.IsServiced)
                {
                    m_table.Select(i);

                    break;
                }
            }

        }

        #endregion

        #region InitModel

        CustomerSelectionModel m_model;

        public override void InitModel()
        {
            m_model = new CustomerSelectionModel();

            m_model.Init();
        }

        #endregion

        #region Custom renderers

        private class StatusRenderer : ImageTableCellRenderer
        {
            Image m_imgServiced, m_imgInit;

            public StatusRenderer()
            {
                m_imgServiced = Resources.CustomerStatusServiced;
                m_imgInit = Resources.CustomerStatusInit;

                DrawText = false;
            }


            public override DrawControl getTableCellRendererComponent(Table table, object value, bool isSelected, bool hasFocus, int row, int column)
            {
                base.getTableCellRendererComponent(table, value, isSelected, hasFocus, row, column);

                RouteScheduleQueue customerStatus = (RouteScheduleQueue)table.Model.GetObjectAt(row, column);

                if (customerStatus.IsServiced)
                    Picture = m_imgServiced;
                else
                    Picture = m_imgInit;

                return this;
            }
        }

        #endregion
    }
}