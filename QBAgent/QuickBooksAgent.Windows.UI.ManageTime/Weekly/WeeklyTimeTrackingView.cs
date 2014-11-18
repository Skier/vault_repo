using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using QuickBooksAgent.Windows.UI.Controls;

namespace QuickBooksAgent.Windows.UI.ManageTime.Weekly
{
    public partial class WeeklyTimeTrackingView : BaseControl
    {
        internal MenuItem m_menuAdd = new MenuItem();
        internal MenuItem m_menuCopy = new MenuItem();
        internal MenuItem m_menuDelete = new MenuItem();
        internal MenuItem m_menuEdit = new MenuItem();        
//        internal MenuItem m_menuUndo = new MenuItem();        
        
        public WeeklyTimeTrackingView()
        {
            InitializeComponent();                        
            m_table.AddColumn(new TableColumn(0, 0, 
                new WeeklyTimeSheetTableCellRenderer(), 
                new DefaultTableCellEditor(), 
                new WeeklyTimeSheetTableHeaderRenderer()));
            
            m_table.AddColumn(new TableColumn(1, 0, 
                new WeeklyTimeSheetTableCellRenderer(), 
                new DefaultTableCellEditor(), 
                new WeeklyTimeSheetTableHeaderRenderer()));
            
            m_table.AddColumn(new TableColumn(2, 0,
                new WeeklyTimeSheetTableCellRenderer(),
                new DefaultTableCellEditor(),
                new WeeklyTimeSheetTableHeaderRenderer()));
            
            m_table.AddColumn(new TableColumn(3, 0,
                new WeeklyTimeSheetTableCellRenderer(),
                new DefaultTableCellEditor(),
                new WeeklyTimeSheetTableHeaderRenderer()));

            MenuItems.Add(m_menuAdd);
            MenuItems.Add(m_menuCopy);
            MenuItems.Add(m_menuDelete);
            MenuItems.Add(m_menuEdit);
            //MenuItems.Add(m_menuUndo);

            m_menuDelete.Enabled = false;
            m_menuEdit.Enabled = false;
            //m_menuUndo.Enabled = false;
            
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);
            Text = "Weekly Time Sheet - Q-Agent";

            m_menuAdd.Text = "Add";
            m_menuCopy.Text = "Copy";
            m_menuDelete.Text = "Delete";
            m_menuEdit.Text = "Edit";            
            //m_menuUndo.Text = "Undo";            
        }

        protected override void OnInit()
        {
            base.OnInit();

            Joystick.Add(m_cmbPersonType, m_table, m_cmbPerson, m_table, m_btnPrevWeek);
            Joystick.Add(m_cmbPerson, m_cmbPersonType, m_btnPrevWeek, m_table, m_btnWeek);            
            Joystick.Add(m_table, m_btnNextWeek, m_cmbPersonType, m_btnWeek, m_cmbPersonType);
            
            Joystick.Add(m_btnWeek, m_btnPrevWeek, m_btnNextWeek, m_cmbPerson, m_table);
            Joystick.Add(m_btnPrevWeek, m_cmbPerson, m_btnWeek, m_cmbPersonType, m_table);
            Joystick.Add(m_btnNextWeek, m_btnWeek, m_table, m_cmbPerson, m_table);                                    
        }
    }
    
    public class WeeklyTimeSheetTableCellRenderer : DefaultTableCellRenderer
    {
        public override DrawControl getTableCellRendererComponent(
            Table table, object value, bool isSelected,
            bool hasFocus, int row, int column)
        {
            DrawControl drawControl = base.getTableCellRendererComponent(
                table, value, isSelected, hasFocus, row, column);            

            if (drawControl.BackColor == Color.Linen)
                drawControl.BackColor = Color.White;

            if (column == 2 || column == 3)
                drawControl.StringFormat.Alignment = StringAlignment.Far;
            else 
                drawControl.StringFormat.Alignment = StringAlignment.Near;

            TimeTrackingTableElement tableElement
                = (TimeTrackingTableElement)table.Model.GetObjectAt(row, 0);            
                        
            if (tableElement.IsDayOfWeek)
            {
                drawControl.Font = new Font("Tahoma", 10F, FontStyle.Bold);
                
                if (!hasFocus)
                    drawControl.BackColor = Color.FromArgb(254, 249, 193);

                if (isSelected && !hasFocus)
                    drawControl.BackColor = Color.FromArgb(192, 192, 255);
                    
            }                            
                
            return drawControl;
        }
    }
    
    public class WeeklyTimeSheetTableHeaderRenderer : DefaultTableHeaderRenderer
    {
        public override DrawControl getTableCellRendererComponent(Table table, object value, bool isSelected,
                                                                  bool hasFocus, int row, int column)
        {
            DrawControl drawControl 
                = base.getTableCellRendererComponent(table, value, 
                    isSelected, hasFocus, row, column);

            if (column == 2 || column == 3)
                drawControl.StringFormat.Alignment = StringAlignment.Far;
            else if (column == 1)
                drawControl.StringFormat.Alignment = StringAlignment.Center;
            else
                drawControl.StringFormat.Alignment = StringAlignment.Near;

            return drawControl;
        }
    }
}
