using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using hobson.controls;
using MobileTech.Windows.UI.Controls;

namespace dalworth.preview
{
    public partial class JobHistory : BaseForm, ITableModel
    {
        public JobHistory()
        {
            InitializeComponent();
            m_table.Enter += new CellValueHandler(OnEnter);
        }

        private void OnEnter(TableCell cell)
        {
            OnDetailsClick(null, EventArgs.Empty);
        }

        protected override void OnFormLoad(EventArgs e)
        {
            m_table.AddColumn(new TableColumn(0));
            m_table.AddColumn(new TableColumn(1, 60));
            m_table.BindModel(this);
            m_table.Select(0);                        
        }

        #region Table

        public int GetRowCount()
        {
            return Model.ServicedTickets.Count;
        }

        public int GetColumnCount()
        {
            return 2;
        }

        public string GetColumnName(int columnIndex)
        {
            if (columnIndex == 0)
                return "Customer";
            return "Date";
        }

        public Type GetColumnClass(int columnIndex)
        {
            return typeof (string);
        }

        public bool IsCellEditable(int rowIndex, int columnIndex)
        {
            return false;
        }

        public object GetValueAt(int rowIndex, int columnIndex)
        {
            if (columnIndex == 0)
                return Model.ServicedTickets[rowIndex].CustomerName;
            return Model.ServicedTickets[rowIndex].Date.ToString("d");
        }

        public void SetValueAt(object aValue, int rowIndex, int columnIndex)
        {
            return;
        }

        public object GetObjectAt(int rowIndex, int columnIndex)
        {
            return Model.ServicedTickets[rowIndex];
        }

        public event TableModelChangeHandler Change;

        #endregion                

        private void OnCancelClick(object sender, EventArgs e)
        {
            Close();
        }

        private void OnDetailsClick(object sender, EventArgs e)
        {
            if (m_table.CurrentRowIndex >=0)
            {
                Model.CurrentServicedTicket = Model.ServicedTickets[m_table.CurrentRowIndex];

                JobDetails jobDetails = new JobDetails();
                ShowForm(jobDetails);
            }
            
        }
    }
}