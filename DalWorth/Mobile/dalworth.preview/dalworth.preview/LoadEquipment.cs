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
    public partial class LoadEquipment : BaseForm, ITableModel
    {
        public LoadEquipment()
        {
            InitializeComponent();
        }


        protected override void OnJoystickInit()
        {
            Joystick.Add(m_table, m_txtNotes, m_txtNotes, m_txtNotes, m_txtNotes);
            Joystick.Add(m_txtNotes, m_table, m_table, m_table, m_table);
        }

        protected override void OnFormLoad(EventArgs e)
        {
            m_table.AddColumn(new TableColumn(0, 0));
            m_table.AddColumn(new TableColumn(1, 40));
            m_table.BindModel(this);

            m_table.Focus();
            m_table.Select(0, 0);
        }        
        
        private void OnBackClick(object sender, EventArgs e)
        {
            Close();
        }

        private void OnNextClick(object sender, EventArgs e)
        {
            LoadRugs loadRugs = new LoadRugs();            
            ShowForm(loadRugs);
            if (Model.AppPoint == ApplicationPoint.StartDayDone)
                Close();
        }

        #region ITable Model

        public int GetRowCount()
        {
            return Model.EquipmentRequests.Count;
        }

        public int GetColumnCount()
        {
            return 2;
        }

        public string GetColumnName(int columnIndex)
        {
            if (columnIndex == 0)
                return "Equipment";
            else if (columnIndex == 1)
                return "Count";
            return string.Empty;
        }

        public Type GetColumnClass(int columnIndex)
        {
            if (columnIndex == 0)
                return typeof(Image);
            else
                return typeof(string);
        }

        public bool IsCellEditable(int rowIndex, int columnIndex)
        {
            return false;
        }

        public object GetValueAt(int rowIndex, int columnIndex)
        {
            if (columnIndex == 0)
                return Model.EquipmentRequests[rowIndex].Name;
            else if (columnIndex == 1)
                return Model.EquipmentRequests[rowIndex].Count;

            return string.Empty;
        }

        public void SetValueAt(object aValue, int rowIndex, int columnIndex)
        {
            return;
        }

        public object GetObjectAt(int rowIndex, int columnIndex)
        {
            return Model.EquipmentRequests[rowIndex];
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