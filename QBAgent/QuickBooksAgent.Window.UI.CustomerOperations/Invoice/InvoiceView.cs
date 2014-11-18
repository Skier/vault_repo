using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QuickBooksAgent.Windows.UI.Controls;

namespace QuickBooksAgent.Windows.UI.CustomerOperations.Invoice
{
    public partial class InvoiceView : BaseControl
    {
        internal MenuItem m_menuAddNewCharge = new MenuItem();
        internal MenuItem m_menuDeleteNewCharge = new MenuItem();
        internal MenuItem m_menuEditNewCharge = new MenuItem();                
        
        public InvoiceView()
        {
            InitializeComponent();

            MenuItems.Add(m_menuAddNewCharge);
            MenuItems.Add(m_menuDeleteNewCharge);            
            MenuItems.Add(m_menuEditNewCharge);  
            
            m_menuDeleteNewCharge.Enabled = false;
            m_menuEditNewCharge.Enabled = false;

            m_table.AddColumn(new TableColumn(0, 0, new NewChargesCellRenderer(), 
                new DefaultTableCellEditor(), new NewChargesHeaderRenderer()));
            m_table.AddColumn(new TableColumn(1, 60, new NewChargesCellRenderer(),
                new DefaultTableCellEditor(), new NewChargesHeaderRenderer()));
        }

        protected override void OnInit()
        {
            base.OnInit();

            // Info Page
            Joystick.Add(m_dtpInvoiceDate, m_tabs, m_cmbTerms, m_tabs, m_cmbTerms);
            Joystick.Add(m_cmbTerms, m_dtpInvoiceDate, m_dtpDueDate, m_dtpInvoiceDate, m_dtpDueDate);
            Joystick.Add(m_dtpDueDate, m_cmbTerms, m_dtpShipDate, m_cmbTerms, m_dtpShipDate);
            Joystick.Add(m_dtpShipDate, m_dtpDueDate, m_btnClearShipDate, m_dtpDueDate, m_txtMemo);
            Joystick.Add(m_btnClearShipDate, m_dtpShipDate, m_txtMemo, m_dtpDueDate, m_txtMemo);
            Joystick.Add(m_txtMemo, m_dtpShipDate, m_chkDelivery, m_dtpShipDate, m_chkDelivery);
            Joystick.Add(m_chkDelivery, m_txtMemo, m_tabs, m_txtMemo, m_tabs);
            
            //New Charges
            Joystick.Add(m_table, m_tabs, m_tabs, m_tabs, m_tabs);
            
            //BillTo
            Joystick.Add(m_txtBillToStreet, m_tabs, m_txtBillToCity, m_tabs, m_txtBillToCity);
            Joystick.Add(m_txtBillToCity, m_txtBillToStreet, m_txtBillToZip, m_txtBillToStreet, m_txtBillToZip);
            Joystick.Add(m_txtBillToZip, m_txtBillToCity, m_txtBillToState, m_txtBillToCity, m_txtBillToCountry);
            Joystick.Add(m_txtBillToState, m_txtBillToZip, m_txtBillToCountry, m_txtBillToCity, m_txtBillToCountry);
            Joystick.Add(m_txtBillToCountry, m_txtBillToState, m_tabs, m_txtBillToZip, m_tabs);
            
            //ShipTo
            Joystick.Add(m_txtShipToStreet, m_tabs, m_txtShipToCity, m_tabs, m_txtShipToCity);
            Joystick.Add(m_txtShipToCity, m_txtShipToStreet, m_txtShipToZip, m_txtShipToStreet, m_txtShipToZip);
            Joystick.Add(m_txtShipToZip, m_txtShipToCity, m_txtShipToState, m_txtShipToCity, m_txtShipToCountry);
            Joystick.Add(m_txtShipToState, m_txtShipToZip, m_txtShipToCountry, m_txtShipToCity, m_txtShipToCountry);
            Joystick.Add(m_txtShipToCountry, m_txtShipToState, m_tabs, m_txtShipToZip, m_tabs);

            //Totals            
            Joystick.Add(m_txtDiscountPercent, m_tabs, m_curDiscountAmount, m_tabs, m_chkIsCustomerTaxable);
            Joystick.Add(m_curDiscountAmount, m_txtDiscountPercent, m_chkIsCustomerTaxable, m_tabs, m_chkIsCustomerTaxable);
            Joystick.Add(m_chkIsCustomerTaxable, m_curDiscountAmount, m_cmbCalcTaxSubtotal, m_txtDiscountPercent, m_cmbCalcTaxSubtotal);
            Joystick.Add(m_cmbCalcTaxSubtotal, m_chkIsCustomerTaxable, m_txtTaxPercent, m_chkIsCustomerTaxable, m_curTaxAmount);
            Joystick.Add(m_txtTaxPercent, m_cmbCalcTaxSubtotal, m_curTaxAmount, m_cmbCalcTaxSubtotal, m_curShipping);
            Joystick.Add(m_curTaxAmount, m_txtTaxPercent, m_curShipping, m_cmbCalcTaxSubtotal, m_curShipping);
            Joystick.Add(m_curShipping, m_curTaxAmount, m_tabs, m_curTaxAmount, m_tabs);
            
            //Account
            Joystick.Add(m_cmbDiscountAccount, m_tabs, m_cmbTaxAccount, m_tabs, m_cmbTaxAccount);
            Joystick.Add(m_cmbTaxAccount, m_cmbDiscountAccount, m_cmbShippingAccount, m_cmbDiscountAccount, m_cmbShippingAccount);
            Joystick.Add(m_cmbShippingAccount, m_cmbTaxAccount, m_tabs, m_cmbTaxAccount, m_tabs);
            

            //Tabs
            Joystick.Add(m_tabs, 0, m_chkDelivery, m_dtpInvoiceDate);
            Joystick.Add(m_tabs, 1, m_table, m_table);
            Joystick.Add(m_tabs, 2, m_txtBillToCountry, m_txtBillToStreet);
            Joystick.Add(m_tabs, 3, m_txtShipToCountry, m_txtShipToStreet);
            Joystick.Add(m_tabs, 4, m_curShipping, m_curDiscountAmount);
            Joystick.Add(m_tabs, 5, m_cmbShippingAccount, m_cmbDiscountAccount);
        }

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Invoice - Q-Agent";

            m_menuAddNewCharge.Text = "Add Charge";
            m_menuDeleteNewCharge.Text = "Delete Charge";
            m_menuEditNewCharge.Text = "Edit Charge";
        }        

        #region NewChargesCellRenderer

        public class NewChargesCellRenderer : DefaultTableCellRenderer
        {
            public override DrawControl getTableCellRendererComponent(
                Table table, object value, bool isSelected,
                bool hasFocus, int row, int column)
            {
                DrawControl drawControl = base.getTableCellRendererComponent(
                    table, value, isSelected, hasFocus, row, column);

                if (column == 1)
                {
                    drawControl.StringFormat.Alignment = StringAlignment.Far;
                    drawControl.StringFormat.LineAlignment = StringAlignment.Center;
                } else
                {
                    drawControl.StringFormat.Alignment = StringAlignment.Near;
                    drawControl.StringFormat.LineAlignment = StringAlignment.Center;                    
                }
                    
                               
                return drawControl;
            }
        }

        public class NewChargesHeaderRenderer : DefaultTableHeaderRenderer
        {
            public override DrawControl getTableCellRendererComponent(Table table, object value, bool isSelected,
                                                                      bool hasFocus, int row, int column)
            {
                DrawControl drawControl
                    = base.getTableCellRendererComponent(table, value,
                        isSelected, hasFocus, row, column);

                if (column == 1)
                {
                    drawControl.StringFormat.Alignment = StringAlignment.Center;
                    drawControl.StringFormat.LineAlignment = StringAlignment.Center;
                }
                else
                {
                    drawControl.StringFormat.Alignment = StringAlignment.Center;
                    drawControl.StringFormat.LineAlignment = StringAlignment.Center;
                }

                return drawControl;
            }
        }    
        

        #endregion

    }
}