using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using dalworth.domain;
using hobson.controls;
using MobileTech.Windows.UI;
using MobileTech.Windows.UI.Controls;
using QuickBooksAgent.Windows.UI;

namespace dalworth.preview
{    
    public partial class VanCheck : BaseForm, ITableModel
    {
        private Dictionary<int, Dictionary<int, List<string>>> m_serialNumbers;
        private int m_currentEquipmentType;
        private int COLUMNS_COUNT = 3;
        
        public VanCheck()
        {
            InitializeComponent();
            
            m_btn0.MouseDown += new MouseEventHandler(OnKeyboardClick);
            m_btn1.MouseDown += new MouseEventHandler(OnKeyboardClick);
            m_btn2.MouseDown += new MouseEventHandler(OnKeyboardClick);
            m_btn3.MouseDown += new MouseEventHandler(OnKeyboardClick);
            m_btn4.MouseDown += new MouseEventHandler(OnKeyboardClick);
            m_btn5.MouseDown += new MouseEventHandler(OnKeyboardClick);
            m_btn6.MouseDown += new MouseEventHandler(OnKeyboardClick);
            m_btn7.MouseDown += new MouseEventHandler(OnKeyboardClick);
            m_btn8.MouseDown += new MouseEventHandler(OnKeyboardClick);
            m_btn9.MouseDown += new MouseEventHandler(OnKeyboardClick);
            m_btnEnter.MouseDown += new MouseEventHandler(OnKeyboardClick);
            m_btnLeft.MouseDown += new MouseEventHandler(OnKeyboardClick);
            
            m_btn0.Picture = ImageKeys.Key0;
            m_btn1.Picture = ImageKeys.Key1;
            m_btn2.Picture = ImageKeys.Key2;
            m_btn3.Picture = ImageKeys.Key3;
            m_btn4.Picture = ImageKeys.Key4;
            m_btn5.Picture = ImageKeys.Key5;
            m_btn6.Picture = ImageKeys.Key6;
            m_btn7.Picture = ImageKeys.Key7;
            m_btn8.Picture = ImageKeys.Key8;
            m_btn9.Picture = ImageKeys.Key9;
            m_btnLeft.Picture = ImageKeys.KeyLeft;
            m_btnEnter.Picture = ImageKeys.KeyEnter;              
        }

        protected override void OnFormLoad(EventArgs e)
        {
            m_serialNumbers = new Dictionary<int, Dictionary<int, List<string>>>();

            int equipmentType = 0;
            
            foreach (EquipmentRequest request in Model.EquipmentRequests)
            {
                Dictionary<int, List<string>> currentGrid = new Dictionary<int, List<string>>();
                int rowsCount = request.Count/COLUMNS_COUNT;
                if (request.Count % COLUMNS_COUNT != 0)
                    rowsCount++;                
                
                for (int i = 0; i < rowsCount; i++)
                {
                    List<string> values = new List<string>();
                    for (int j = 0; j < COLUMNS_COUNT; j++)
                        values.Add(string.Empty);

                    currentGrid.Add(i, values);
                }
                                                
                m_serialNumbers.Add(equipmentType, currentGrid);
                equipmentType++;
            }

            m_currentEquipmentType = 0;
            OnEquipmentTypeChanged();
            
            for (int i = 0; i < COLUMNS_COUNT; i++)
            {
                CellEditor cellEditor = new CellEditor();
                cellEditor.TextChanged += new EventHandler(OnCellEditorTextChanged);                
                cellEditor.OnEnterPress += new CellEditor.OnEnterPressHandler(OnCellEditorEnterPress);
                m_table.AddColumn(new TableColumn(i, 0, new CellRenderer(), cellEditor));
            }
                
            m_table.BindModel(this);

            UpdateStatus();

            m_table.Focus();
            m_table.Select(0);
            m_table.BeginEdit();            
        }

        private void OnCellEditorEnterPress()
        {
            EditNextCell();
        }

        private void OnCellEditorTextChanged(object sender, EventArgs e)
        {
            TextBoxBase textBox = (TextBoxBase)sender;
            if (textBox.TextLength == 4)
            {
                m_table.ApplyEdit();
                EditNextCell();
            } else if (textBox.TextLength > 4)
                textBox.Text = textBox.Text.Substring(0, 4);                
        }

        protected override void OnActivated(EventArgs e)
        {
            m_table.Focus();
        }

        private void OnEquipmentTypeChanged()
        {
            m_lblEquipment.Text = Model.EquipmentRequests[m_currentEquipmentType].Name;
            Text =
                string.Format("0140 Van Checklist - step {0}/{1}", m_currentEquipmentType + 1,
                              Model.EquipmentRequests.Count + 1);
            m_table.BindModel(this);
            UpdateStatus();
            m_table.Focus();
            m_table.Select(0);
        }

        private void OnBackClick(object sender, EventArgs e)
        {
            m_table.ApplyEdit();
            
            if (m_currentEquipmentType == 0)
            {
                Close();
            }
            else
            {
                m_currentEquipmentType--;
                OnEquipmentTypeChanged();
            }
        }

        private void OnNextClick(object sender, EventArgs e)
        {
            m_table.ApplyEdit();
            
            if (m_currentEquipmentType >= Model.EquipmentRequests.Count - 1)
            {
                Finish finish = new Finish();
                ShowForm(finish);

                if (Model.AppPoint == ApplicationPoint.StartDayDone)
                    Close();
            } else
            {
                m_currentEquipmentType++;
                OnEquipmentTypeChanged();  
                if (m_serialNumbers[m_currentEquipmentType][0][0] == string.Empty)
                    m_table.BeginEdit();
            }
        }
        
        private int GetSerialsCount()
        {
            int result = 0;

            foreach (List<string> list in m_serialNumbers[m_currentEquipmentType].Values)
            {
                foreach (string value in list)
                {
                    if (value != string.Empty)
                        result++;
                }
            }
            return result;
        }
        
        private void UpdateStatus()
        {
            int filledCount = GetSerialsCount();
            m_lblCount.Text = filledCount + "/" + Model.EquipmentRequests[m_currentEquipmentType].Count;

            if (filledCount == Model.EquipmentRequests[m_currentEquipmentType].Count)
                m_menuNext.Enabled = true;
            else
                m_menuNext.Enabled = false;
        }

        #region ITable Model

        public int GetRowCount()
        {
            return m_serialNumbers[m_currentEquipmentType].Keys.Count;
        }

        public int GetColumnCount()
        {
            return COLUMNS_COUNT;
        }

        public string GetColumnName(int columnIndex)
        {
            return string.Empty;
        }

        public Type GetColumnClass(int columnIndex)
        {
            return string.Empty.GetType();
        }

        public bool IsCellEditable(int rowIndex, int columnIndex)
        {
            if ((rowIndex * COLUMNS_COUNT) + columnIndex + 1 
                <= Model.EquipmentRequests[m_currentEquipmentType].Count)
            {
                return true;
            }
            return false;            
        }

        public object GetValueAt(int rowIndex, int columnIndex)
        {
            return m_serialNumbers[m_currentEquipmentType][rowIndex][columnIndex];
        }

        public void SetValueAt(object aValue, int rowIndex, int columnIndex)
        {
            m_serialNumbers[m_currentEquipmentType][rowIndex][columnIndex] = (string) aValue;
            UpdateStatus();
        }

        public object GetObjectAt(int rowIndex, int columnIndex)
        {
            return m_serialNumbers[m_currentEquipmentType][rowIndex];
        }

        public event TableModelChangeHandler Change;

        #endregion

        #region KeyBoard

        private void OnKeyboardClick(object sender, MouseEventArgs e)
        {
            MenuButton button = (MenuButton) sender;
            
            if (button.Name == "m_btn0")
                WinAPI.SendKey('0');
            else if (button.Name == "m_btn1")
                WinAPI.SendKey('1');
            else if (button.Name == "m_btn2")
                WinAPI.SendKey('2');
            else if (button.Name == "m_btn3")
                WinAPI.SendKey('3');
            else if (button.Name == "m_btn4")
                WinAPI.SendKey('4');
            else if (button.Name == "m_btn5")
                WinAPI.SendKey('5');
            else if (button.Name == "m_btn6")
                WinAPI.SendKey('6');
            else if (button.Name == "m_btn7")
                WinAPI.SendKey('7');
            else if (button.Name == "m_btn8")
                WinAPI.SendKey('8');
            else if (button.Name == "m_btn9")
                WinAPI.SendKey('9');
            else if (button.Name == "m_btnEnter")
            {
                EditNextCell();                 
            } else if (button.Name == "m_btnLeft")
            {
                WinAPI.SendKey('\b');                
            }                
        }
        
        private void EditNextCell()
        {
            CellCoordinate nextCell = new CellCoordinate();
            
            if (m_table.CurrentColumnIndex == COLUMNS_COUNT - 1) //Last column
            {
                if (m_table.CurrentRowIndex != m_table.Model.GetRowCount() - 1)
                {
                    nextCell.RowIndex = m_table.CurrentRowIndex + 1;
                    nextCell.ColumnIndex = 0;                    
                }
            }
            else
            {
                nextCell.RowIndex = m_table.CurrentRowIndex;
                nextCell.ColumnIndex = m_table.CurrentColumnIndex + 1;                                
            }

            if (!nextCell.IsInited() || (nextCell.IsInited() 
                && !m_table.Model.IsCellEditable(nextCell.RowIndex, nextCell.ColumnIndex)))
            {
                m_table.ApplyEdit();
                m_table.Focus();
            } else
            {
                m_table.ApplyEdit();
                m_table.Select(nextCell.RowIndex, nextCell.ColumnIndex);
                m_table.Focus();
                m_table.BeginEdit();                
            }                        
        }

        #endregion
    }
    
    #region CellCoordinate

    class CellCoordinate
    {
        private int m_rowIndex;
        private int m_columnIndex;

        public CellCoordinate()
        {
            m_rowIndex = -1;
            m_columnIndex = -1;
        }

        public bool IsInited()
        {
            if (m_rowIndex != -1 && m_columnIndex != -1)
                return true;
            return false;
        }

        public void Reset()
        {
            m_rowIndex = -1;
            m_columnIndex = -1;
        }

        public int RowIndex
        {
            get { return m_rowIndex; }
            set { m_rowIndex = value; }
        }

        public int ColumnIndex
        {
            get { return m_columnIndex; }
            set { m_columnIndex = value; }
        }
    }

    #endregion

    #region CellRenderer

    class CellRenderer : DefaultTableCellRenderer
    {
        public override DrawControl getTableCellRendererComponent(Table table, object value, bool isSelected,
                                                                  bool hasFocus, int row, int column)
        {
            DrawControl control = base.getTableCellRendererComponent(table, value, isSelected, hasFocus, row, column);
            
            if (!table.Model.IsCellEditable(row, column))
            {
                control.BackColor = hasFocus ? Color.Black : Color.LightGray;
            }
                
            return control;            
        }
    }

    #endregion

    #region CellEditor

    class CellEditor : DefaultTableCellEditor
    {
        public delegate void OnEnterPressHandler();
        public event OnEnterPressHandler OnEnterPress;
        
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                if (OnEnterPress != null)
                    OnEnterPress.Invoke();
            } else                 
                base.OnKeyPress(e);
        }
    }

    #endregion
    
}