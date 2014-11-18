using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using dalworth.domain;
using hobson.controls;
using MobileTech.Windows.UI.Controls;

namespace dalworth.preview
{
    public partial class LoadRugs : BaseForm, ITableModel
    {
        public LoadRugs()
        {
            InitializeComponent();
        }

        protected override void OnFormLoad(EventArgs e)
        {
            m_table.SelectionChanged += new SelectionHandler(OnTableSelectionChanged);
            
            m_table.AddColumn(new TableColumn(0, 20, new SelectionRenderer()));
            m_table.AddColumn(new TableColumn(1, 40));
            m_table.AddColumn(new TableColumn(2, 0));
            m_table.AddColumn(new TableColumn(3, 30));
            m_table.BindModel(this);
            UpdateNextStatus();

            m_table.Focus();
            m_table.Select(0, 1);            
        }

        private void OnTableSelectionChanged()
        {
            UpdateNextStatus();
        }

        private void OnBackClick(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateNextStatus()
        {
            for (int i = 0; i < Model.StartDayTickets.Count; i++)
            {
                if (!m_table.IsRowSelected(i))
                {
                    m_menuNext.Enabled = false;
                    return;
                }                    
            }

            m_menuNext.Enabled = true;            
        }

        private void OnNextClick(object sender, EventArgs e)
        {
            VanCheck vanCheck = new VanCheck();
            ShowForm(vanCheck);

            if (Model.AppPoint == ApplicationPoint.StartDayDone)
                Close();
        }

        #region ITable Model

        public int GetRowCount()
        {
            return Model.StartDayTickets.Count;
        }

        public int GetColumnCount()
        {
            return 4;
        }

        public string GetColumnName(int columnIndex)
        {
            if (columnIndex == 1)
                return "Ticket";
            else if (columnIndex == 2)
                return "Customer";
            else if (columnIndex == 3)
                return "Rugs";
            return string.Empty;
        }

        public Type GetColumnClass(int columnIndex)
        {
            if (columnIndex == 0)
                return typeof(Image);
            else
                return typeof (string);
        }

        public bool IsCellEditable(int rowIndex, int columnIndex)
        {
            return false;
        }

        public object GetValueAt(int rowIndex, int columnIndex)
        {
            if (columnIndex == 1)
                return Model.StartDayTickets[rowIndex].Number;
            else if (columnIndex == 2)
                return Model.StartDayTickets[rowIndex].CustomerName;
            else if (columnIndex == 3)
                return Model.StartDayTickets[rowIndex].RugsCount;
                
            return string.Empty;
        }

        public void SetValueAt(object aValue, int rowIndex, int columnIndex)
        {
            return;
        }

        public object GetObjectAt(int rowIndex, int columnIndex)
        {
            return Model.StartDayTickets[rowIndex];
        }

        public event TableModelChangeHandler Change;

        #endregion

        #region SelectionRenderer

        private class SelectionRenderer : ImageTableCellRenderer
        {
            #region Constructor

            public SelectionRenderer()
            {
                DrawText = false;
            }

            #endregion

            #region DrawControl

            public override DrawControl getTableCellRendererComponent(Table table, object value, bool isSelected, bool hasFocus, int row, int column)
            {
                base.getTableCellRendererComponent(table, value, isSelected, hasFocus, row, column);

                if (isSelected)
                    Picture = Resource.Selected;
                else
                    Picture = Resource.Unselected;

                return this;
            }

            #endregion
        }

        #endregion        
    }
}