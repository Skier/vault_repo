using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using QuickBooksAgent.Windows.UI.Controls;

namespace QuickBooksAgent.Windows.UI.Banking.CreditCardCharges
{        
    public partial class CreditCardView : BaseControl
    {
        internal MenuItem m_menuAddExpence = new MenuItem();
        internal MenuItem m_menuEditExpence = new MenuItem();
        internal MenuItem m_menuDeleteExpence = new MenuItem();
        
        
        public CreditCardView()
        {
            InitializeComponent();

            MenuItems.Add(m_menuAddExpence);
            MenuItems.Add(m_menuEditExpence);
            MenuItems.Add(m_menuDeleteExpence);

            m_menuEditExpence.Enabled = false;
            m_menuDeleteExpence.Enabled = false;
            m_menuAddExpence.Enabled = false;                   
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Enter Credit Card Charges - Q-Agent";

            m_menuAddExpence.Text = "Add Expence Line";
            m_menuEditExpence.Text = "Edit Expence Line";
            m_menuDeleteExpence.Text = "Delete Expence Line";
        }

        protected override void OnInit()
        {
            base.OnInit();

            //General
            Joystick.Add(m_cmbCreditCard, m_tabs, m_cmbTransactionType, m_tabs, m_cmbTransactionType);
            Joystick.Add(m_cmbTransactionType, m_cmbCreditCard, m_cmbPayeeType, m_cmbCreditCard, m_cmbPayee);
            Joystick.Add(m_cmbPayeeType, m_cmbTransactionType, m_cmbPayee, m_cmbTransactionType, m_txtRefNumber);
            Joystick.Add(m_cmbPayee, m_cmbPayeeType, m_txtRefNumber, m_cmbTransactionType, m_txtRefNumber);
            Joystick.Add(m_txtRefNumber, m_cmbPayee, m_dtpDate, m_cmbPayee, m_dtpDate);
            Joystick.Add(m_dtpDate, m_txtRefNumber, m_curAmount, m_txtRefNumber, m_curAmount);
            Joystick.Add(m_curAmount, m_dtpDate, m_tabs, m_dtpDate, m_tabs);

            //Additional
            Joystick.Add(m_txtNotes, m_tabs, m_tabs, m_tabs, m_tabs);

            //Expences            
            Joystick.Add(m_table, m_tabs, m_tabs, m_tabs, m_tabs);

            //Tabs
            Joystick.Add(m_tabs, 0, m_curAmount, m_cmbCreditCard);
            Joystick.Add(m_tabs, 1, m_txtNotes, m_txtNotes);
            Joystick.Add(m_tabs, 2, m_table, m_table);
        }        
    }

    public class ExpenceLineTableCellRenderer : DefaultTableCellRenderer
    {
        public override DrawControl getTableCellRendererComponent(
            Table table, object value, bool isSelected,
            bool hasFocus, int row, int column)
        {
            DrawControl drawControl = base.getTableCellRendererComponent(
                table, value, isSelected, hasFocus, row, column);

            if (column == 1)
                drawControl.StringFormat.Alignment = StringAlignment.Far;
            else
                drawControl.StringFormat.Alignment = StringAlignment.Center;

            return drawControl;
        }
    }

    public class ExpenceLineTableHeaderRenderer : DefaultTableHeaderRenderer
    {
        public override DrawControl getTableCellRendererComponent(Table table, object value, bool isSelected,
                                                                  bool hasFocus, int row, int column)
        {
            DrawControl drawControl
                = base.getTableCellRendererComponent(table, value,
                    isSelected, hasFocus, row, column);

            if (column == 1)
                drawControl.StringFormat.Alignment = StringAlignment.Far;
            else
                drawControl.StringFormat.Alignment = StringAlignment.Center;

            return drawControl;
        }
    }    
    
}
