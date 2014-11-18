using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Dalworth.Controls;
using Dalworth.Controls.Menu;
using Dalworth.Domain;
using Dalworth.Windows.StartDay.VanCheck;

namespace Dalworth.Windows.StartDay.EnterEquipment
{
    public class EnterEquipmentController : StartDayBaseController<EnterEquipmentModel, EnterEquipmentView>, ITableModel
    {
        private Dictionary<int, Dictionary<int, List<string>>> m_serialNumbers;
        private int m_currentEquipmentType;
        private int COLUMNS_COUNT = 3;

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.StartDayModel = (StartDayModel)data[0];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btn0.MouseDown += new MouseEventHandler(OnKeyboardClick);
            View.m_btn1.MouseDown += new MouseEventHandler(OnKeyboardClick);
            View.m_btn2.MouseDown += new MouseEventHandler(OnKeyboardClick);
            View.m_btn3.MouseDown += new MouseEventHandler(OnKeyboardClick);
            View.m_btn4.MouseDown += new MouseEventHandler(OnKeyboardClick);
            View.m_btn5.MouseDown += new MouseEventHandler(OnKeyboardClick);
            View.m_btn6.MouseDown += new MouseEventHandler(OnKeyboardClick);
            View.m_btn7.MouseDown += new MouseEventHandler(OnKeyboardClick);
            View.m_btn8.MouseDown += new MouseEventHandler(OnKeyboardClick);
            View.m_btn9.MouseDown += new MouseEventHandler(OnKeyboardClick);
            View.m_btnEnter.MouseDown += new MouseEventHandler(OnKeyboardClick);
            View.m_btnLeft.MouseDown += new MouseEventHandler(OnKeyboardClick);

            View.m_btn0.Picture = ImageKeys.Key0;
            View.m_btn1.Picture = ImageKeys.Key1;
            View.m_btn2.Picture = ImageKeys.Key2;
            View.m_btn3.Picture = ImageKeys.Key3;
            View.m_btn4.Picture = ImageKeys.Key4;
            View.m_btn5.Picture = ImageKeys.Key5;
            View.m_btn6.Picture = ImageKeys.Key6;
            View.m_btn7.Picture = ImageKeys.Key7;
            View.m_btn8.Picture = ImageKeys.Key8;
            View.m_btn9.Picture = ImageKeys.Key9;
            View.m_btnLeft.Picture = ImageKeys.KeyLeft;
            View.m_btnEnter.Picture = ImageKeys.KeyEnter;

            View.m_menuBack.Click += OnBackClick;
            View.m_menuCancel.Click += OnCancelClick;            
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            m_serialNumbers = new Dictionary<int, Dictionary<int, List<string>>>();

            int equipmentType = 0;

            foreach (WorkEquipment equipment in Model.WorkEquipment)
            {
                Dictionary<int, List<string>> currentGrid = new Dictionary<int, List<string>>();
                int rowsCount = equipment.Quantity.Value / COLUMNS_COUNT;
                if (equipment.Quantity.Value % COLUMNS_COUNT != 0)
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
                View.m_table.AddColumn(new TableColumn(i, 0, new CellRenderer(), cellEditor));
            }

            View.m_table.BindModel(this);

            UpdateStatus();
        }

        #endregion

        #region OnCellEditorEnterPress

        private void OnCellEditorEnterPress()
        {
            EditNextCell();
        }

        #endregion

        #region OnCellEditorTextChanged

        private void OnCellEditorTextChanged(object sender, EventArgs e)
        {
            TextBoxBase textBox = (TextBoxBase)sender;
            if (textBox.TextLength == 4)
            {
                View.m_table.ApplyEdit();
                EditNextCell();
            }
            else if (textBox.TextLength > 4)
                textBox.Text = textBox.Text.Substring(0, 4);
        }

        #endregion

        #region OnViewActivated

        public override void OnViewActivated()
        {            
            View.m_table.Focus();
            View.m_table.Select(0, 0);
            View.m_table.BeginEdit();                  

            if (Model.StartDayModel.IsStartDayDone() || Model.StartDayModel.IsStartDayCancelled)
                View.Destroy();
        }

        #endregion

        #region OnEquipmentTypeChanged

        private void OnEquipmentTypeChanged()
        {            
            View.m_lblEquipment.Text = Model.WorkEquipment[m_currentEquipmentType].EquipmentType.ToString();
            View.Text =
                string.Format("Van Checklist - step {0}/{1}", m_currentEquipmentType + 1,
                  Model.WorkEquipment.Count + 1);            
            
            MainFormController.UpdateCaption(this);            

            View.m_table.BindModel(this);
            UpdateStatus();
            View.m_table.Focus();
            View.m_table.Select(0);
        }

        #endregion


        #region Back Next

        public override bool IsLeftActionExist
        {
            get { return true; }
        }

        public override string LeftActionName
        {
            get { return "Menu"; }
        }

        public override string RightActionName
        {
            get { return "Next"; }
        }

        public override void OnRightAction()
        {
            OnNextClick();
        }

        private void OnBackClick(object sender, EventArgs e)
        {
            View.m_table.ApplyEdit();

            if (m_currentEquipmentType == 0)
            {
                View.Destroy();
            }
            else
            {
                m_currentEquipmentType--;
                OnEquipmentTypeChanged();
            }            
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            if (MessageDialog.Show(MessageDialogType.Question, "Do you realy want to cancel Start Day wizard?")
                == DialogResult.Yes)
            {
                Model.StartDayModel.IsStartDayCancelled = true;
                View.Destroy();
            }
        }

        #endregion




        #region OnNextClick

        private void OnNextClick()
        {
            View.m_table.ApplyEdit();

            Hashtable hashtable = new Hashtable();
            List<string> invalidEquipmentNumbers = new List<string>();
            foreach (List<string> list in m_serialNumbers[m_currentEquipmentType].Values)
            {
                foreach (string s in list)
                {
                    if (s == string.Empty)
                        continue;

                    try
                    {
                        hashtable.Add(s, s);

                        if (!Model.IsEquipmentExist(Model.WorkEquipment[m_currentEquipmentType].EquipmentType, s))
                            invalidEquipmentNumbers.Add(s);

                    }
                    catch (ArgumentException)
                    {
                        MessageDialog.Show(MessageDialogType.Information,
                            "There are equal equipment numbers. All equipment numbers should be different.");
                        return;
                    }
                }
            }

            if (invalidEquipmentNumbers.Count > 0)
            {
                string invalidEquipment = string.Empty;
                invalidEquipment += invalidEquipmentNumbers[0];

                for (int i = 1; i < invalidEquipmentNumbers.Count; i++)
                {
                    invalidEquipment += ", " + invalidEquipmentNumbers[i];
                }

                MessageDialog.Show(MessageDialogType.Information,
                    string.Format("You have entered invalid equipment number(s): {0}. Please make corrections.", invalidEquipment));
                return;
            }

            if (m_currentEquipmentType >= Model.WorkEquipment.Count - 1)
            {
                Model.SaveCapturedEquipment(m_serialNumbers);
                Next();
            }
            else
            {
                m_currentEquipmentType++;
                OnEquipmentTypeChanged();
                if (m_serialNumbers[m_currentEquipmentType][0][0] == string.Empty)
                    View.m_table.BeginEdit();
            }            
        }

        #endregion

        #region GetSerialsCount

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

        #endregion

        #region UpdateStatus

        private void UpdateStatus()
        {
            int filledCount = GetSerialsCount();
            View.m_lblCount.Text = filledCount + "/" + Model.WorkEquipment[m_currentEquipmentType].Quantity;

            if (filledCount == Model.WorkEquipment[m_currentEquipmentType].Quantity)
                IsRightActionExist = true;                
            else
                IsRightActionExist = false;
        }

        #endregion

        #region KeyBoard

        private void OnKeyboardClick(object sender, MouseEventArgs e)
        {
            MenuButton button = (MenuButton)sender;

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
            }
            else if (button.Name == "m_btnLeft")
            {
                WinAPI.SendKey('\b');
            }
        }

        private void EditNextCell()
        {
            CellCoordinate nextCell = new CellCoordinate();

            if (View.m_table.CurrentColumnIndex == COLUMNS_COUNT - 1) //Last column
            {
                if (View.m_table.CurrentRowIndex != View.m_table.Model.GetRowCount() - 1)
                {
                    nextCell.RowIndex = View.m_table.CurrentRowIndex + 1;
                    nextCell.ColumnIndex = 0;
                }
            }
            else
            {
                nextCell.RowIndex = View.m_table.CurrentRowIndex;
                nextCell.ColumnIndex = View.m_table.CurrentColumnIndex + 1;
            }

            if (!nextCell.IsInited() || (nextCell.IsInited()
                && !View.m_table.Model.IsCellEditable(nextCell.RowIndex, nextCell.ColumnIndex)))
            {
                View.m_table.ApplyEdit();
                View.m_table.Focus();
            }
            else
            {
                View.m_table.ApplyEdit();
                View.m_table.Select(nextCell.RowIndex, nextCell.ColumnIndex);
                View.m_table.Focus();
                View.m_table.BeginEdit();
            }
        }

        #endregion

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
                <= Model.WorkEquipment[m_currentEquipmentType].Quantity)
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
            m_serialNumbers[m_currentEquipmentType][rowIndex][columnIndex] = (string)aValue;
            UpdateStatus();
        }

        public object GetObjectAt(int rowIndex, int columnIndex)
        {
            return m_serialNumbers[m_currentEquipmentType][rowIndex];
        }

        public event TableModelChangeHandler Change;

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
}
