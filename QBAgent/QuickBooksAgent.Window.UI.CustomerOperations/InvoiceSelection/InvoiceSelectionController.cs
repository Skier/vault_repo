using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.WindowsMobile.PocketOutlook;
using QuickBooksAgent.Data;
using QuickBooksAgent.Windows.UI.Controls;
using QuickBooksAgent.Windows.UI.CustomerOperations.Invoice;
using QuickBooksAgent.Domain;
using WinToolZone.CSLMail;

namespace QuickBooksAgent.Windows.UI.CustomerOperations.InvoiceSelection
{
    public class InvoiceSelectionController : SingleFormController<InvoiceSelectionModel, InvoiceSelectionView>
    {
        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_table.Enter += new CellValueHandler(OnTableEnter);
            View.m_menuDelete.Click += new EventHandler(OnDeleteClick);
            View.m_menuSelect.Click += new EventHandler(OnSelectClick);
            View.m_menuCreate.Click += new EventHandler(OnCreateClick);
            View.m_table.RowChanged += new RowHandler(OnRowChanged);            
        }

        #endregion

        #region OnCreateClick

        void OnCreateClick(object sender, EventArgs e)
        {
            InvoiceController invoiceController
                = SingleFormController.Prepare<InvoiceController>(
                Model.Customer);

            invoiceController.Model.InvoiceAffected +=
                new InvoiceAffectHandler(InvoiceAffectHandler);

             invoiceController.Execute();

             OnDefaultActionSettingChanged();
         }

        #endregion

        #region InvoiceAffectHandler

        void InvoiceAffectHandler(QuickBooksAgent.Domain.Invoice invoice)
        {
            int CurrentIndex = Model.Update(invoice);

            View.m_table.Select(CurrentIndex);

            View.m_table.Focus();
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            if (data != null || data[0] is Customer)            
                Model.Init((Customer)data[0]);
            else
                throw new QuickBooksAgentException("Invalid parameter");            
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            base.OnViewLoad();

            View.m_table.AddColumn(new TableColumn(0, 0, new InvoiceTableCellRenderer(),
                null, new InvoiceTableHeaderCellRenderer()));
            View.m_table.AddColumn(new TableColumn(1, 0, new InvoiceTableCellRenderer(),
                null, new InvoiceTableHeaderCellRenderer()));

            View.m_table.BindModel(Model);

            if (View.m_table.Model.GetRowCount() > 0)
            {
                View.m_table.Select(0);
            } 
        }

        #endregion 

        #region OnRowChanged

        void OnRowChanged(int rowIndex)
        {
            SelectInvoice();
                            

            View.m_menuSelect.Enabled = (Model.GetRowCount() > 0) && (View.m_table.CurrentRowIndex >= 0);
            View.m_menuDelete.Enabled = View.m_menuSelect.Enabled && Model.Current.EntityState == EntityState.Created;
            View.m_menuSend.Enabled = View.m_menuSelect.Enabled && Model.ModifiedOrOriginalCustomer.Email != string.Empty;

            if (View.m_menuSend.Enabled)
            {
                View.m_menuSend.MenuItems.Clear();
                MenuItem sendInvoice = new MenuItem();                
                sendInvoice.Text = Model.ModifiedOrOriginalCustomer.Email ?? string.Empty;                                    
                sendInvoice.Click += new EventHandler(OnSendInvoiceClick);                
                View.m_menuSend.MenuItems.Add(sendInvoice);

                Regex regex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");

                if (sendInvoice.Text == null || sendInvoice.Text == string.Empty)
                {
                    sendInvoice.Enabled = false;
                } else if (!regex.IsMatch(sendInvoice.Text))
                {
                    sendInvoice.Enabled = false;
                }                                        
            }

            View.m_menuSelect.Text = (Model.Current.EntityState == EntityState.Created) ? "Edit" : "View";
            DefaultActionName = View.m_menuSelect.Text;
        }

        #endregion

        #region OnSendInvoiceClick

        private void OnSendInvoiceClick(object sender, EventArgs e)
        {
            string emailConfigurationError = Configuration.App.CheckEmailSettings();
            if (emailConfigurationError != string.Empty)
            {
                MessageBox.Show(emailConfigurationError, "Email Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                return;
            }

            string subject = "Invoice from " + Model.Company.CompanyName;
            
            FileInfo messageFile = new FileInfo(Host.GetPath("invoice.htm"));
            StreamWriter writer = messageFile.CreateText();
            writer.Write(Model.GetInvoiceEmailAttachment());
            writer.Close();
            
                        
            if (Configuration.App.EmailSettingsType == 1) //Pocket Outlook
            {
                try
                {
                    EmailMessage message = new EmailMessage();
                    message.BodyText = Model.GetInvoiceEmailBody();
                    message.Subject = subject;                    
                    message.Attachments.Add(new Attachment(Host.GetPath("invoice.htm")));
                    message.To.Add(new Recipient(Model.ModifiedOrOriginalCustomer.Email));                    
                    message.Send(Configuration.App.OutlookAccount);                                                            
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Email error occurred: " + ex.Message, "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    return;
                }

                MessageBox.Show("Invoice has been sent to your Outlook message queue and will be sent to the Customer next time Outlook sends messages (depends on your Outlooks account settings).", "Message Scheduled", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            } else
            {
             
                SMTP smtp = new SMTP();
                smtp.From = Configuration.App.EmailFrom;
                smtp.To = Model.ModifiedOrOriginalCustomer.Email;
                smtp.Subject = subject;
                smtp.SMTPServer = Configuration.App.SmtpServer;
                smtp.SMTPPort = (short) Configuration.App.SmtpPort.Value;
                smtp.MailType = SMTP.MailEncodingType.PLAINTEXT;
                smtp.Message = Model.GetInvoiceEmailBody();
                smtp.AddAttachment(Host.GetPath("invoice.htm"));

                Cursor.Current = Cursors.WaitCursor;
                if (!smtp.SendMail())
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Cannot send email: " + smtp.ErrorDescription, "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                } else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Invoice has been successfully sent", "Invoice sent", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                }
            }
        }

        #endregion

        #region OnSelectClick

        void OnSelectClick(object sender, EventArgs e)
        {
            ManageInvoice();
        }

        #endregion 

        #region OnDeleteClick

        void OnDeleteClick(object sender, EventArgs e)
        {
            if (Model.Current == null)
            {
                MessageDialog.Show(MessageDialogType.Information,
                    "Please select invoice");

                return;
            }

            if (MessageDialog.Show(MessageDialogType.Question,
                "Do you really want to deletet this invoice?") == 
                    System.Windows.Forms.DialogResult.Yes)
                try
                {
                    using (WaitCursor waitCursor = new WaitCursor())
                    {
                        Model.DeleteInvoice();
                    }

                    if (Model.GetRowCount() == 0) View.Destroy();
                }
                catch (Exception ex)
                {
                    MessageDialog.Show(ex);
                }    
        }

        #endregion 

        #region OnTableEnter

        private void OnTableEnter(TableCell cell)
        {
            SelectInvoice();

            ManageInvoice();
        }

        #endregion

        #region SelectInvoice

        private void SelectInvoice()
        {
            if (View.m_table.CurrentRowIndex < 0)
            {
                Model.Current = null;
            }
            else
            {
                Model.Current = (QuickBooksAgent.Domain.Invoice)Model.GetObjectAt(
                    View.m_table.CurrentRowIndex, 0); ;
            }
        }

        #endregion 

        #region ManageInvoice

        protected void ManageInvoice()
        {
            if (Model.Current == null)
            {
                MessageDialog.Show(MessageDialogType.Information, "Please select invoice");
                return;
            }

            QuickBooksAgent.Domain.Invoice invoice =
                (QuickBooksAgent.Domain.Invoice)Model.GetObjectAt(View.m_table.CurrentRowIndex, 0);

            InvoiceController invoiceController =
                SingleFormController.Prepare<InvoiceController>(
                    Model.Customer,
                        invoice);

            invoiceController.Model.InvoiceAffected +=
                new InvoiceAffectHandler(InvoiceAffectHandler);

            invoiceController.Execute();
        }

        #endregion

        #region IsDefaultActionExist

        public override bool IsDefaultActionExist
        {
            get
            {
                return Model.GetRowCount() > 0;
            }
        }

        #endregion        

        #region OnDefaultAction

        public override void OnDefaultAction()
        {
            base.OnDefaultAction();

            ManageInvoice();
        }

        #endregion

        #region Renderer Classes

        private class InvoiceTableCellRenderer : DefaultTableCellRenderer
        {
            public override DrawControl getTableCellRendererComponent(Table table, object value, bool isSelected, bool hasFocus, int row, int column)
            {
                DrawControl drawControl =
                    base.getTableCellRendererComponent(
                    table,
                    value,
                    isSelected,
                    hasFocus,
                    row,
                    column);

                drawControl.StringFormat.Alignment =
                    (column == 0) ? System.Drawing.StringAlignment.Near :
                        System.Drawing.StringAlignment.Center;

                return drawControl;
            }
        }

        private class InvoiceTableHeaderCellRenderer : DefaultTableCellRenderer
        {
            public override DrawControl getTableCellRendererComponent(Table table, object value, bool isSelected, bool hasFocus, int row, int column)
            {
                DrawControl drawControl =
                    base.getTableCellRendererComponent(
                    table,
                    value,
                    isSelected,
                    hasFocus,
                    row,
                    column);

                drawControl.BackColor = System.Drawing.Color.LightGray;

                drawControl.StringFormat.Alignment =
                    (column == 0) ? System.Drawing.StringAlignment.Near :
                        System.Drawing.StringAlignment.Center;

                return drawControl;
            }
        }

        #endregion
    }
}

