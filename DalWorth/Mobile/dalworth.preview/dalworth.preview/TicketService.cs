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
using OpenNETCF.Phone;

namespace dalworth.preview
{
    public partial class TicketService : BaseForm, ITableModel
    {
        public TicketService()
        {
            InitializeComponent();
        }

        private bool m_isRugPickup;
        
        #region OnFormLoad

        protected override void OnFormLoad(EventArgs e)
        {
            if (Model.CurrentTicket.JobType == "Rug Pickup")
                m_isRugPickup = true;
            else
                m_isRugPickup = false;
            
            m_lblDate.Text = DateTime.Now.ToString("ddd, MMM dd yyyy");

            m_lblMap.Text = "MAP: " + Model.CurrentTicket.Map;
            m_lblCustomerName.Text = Model.CurrentTicket.CustomerName;
            m_txtAddress.Text = Model.CurrentTicket.Address;
            m_linkPhone1.Text = "HM " + Model.CurrentTicket.Phone1;
            m_linkPhone2.Text = "BS " + Model.CurrentTicket.Phone2;
            m_lblJobType.Text = Model.CurrentTicket.JobType;
            m_lblTicketNumber.Text = "TKT: " + Model.CurrentTicket.Number;
            m_txtNotes.Text = Model.CurrentTicket.Notes;
            m_txtMessage.Text = Model.CurrentTicket.Notes2;
            
            if (m_isRugPickup)
            {
                m_table.AddColumn(new TableColumn(0, 20, new SelectionRenderer()));
                m_table.AddColumn(new TableColumn(1));
                m_table.AddColumn(new TableColumn(2, 60));
                
            } else
            {
                tabPage4.Text = "Rug Delivery";
                m_table.AddColumn(new TableColumn(0));
                m_table.AddColumn(new TableColumn(1, 60));                
                m_table.MultipleSelection = false;
            }
                
            m_table.BindModel(this);

            //Check all rows
            List<int> selectionIndexes = new List<int>();
            for (int i = 0; i < Model.CurrentTicket.Rugs.Count; i++)
                selectionIndexes.Add(i);

            m_table.Select(selectionIndexes);
            if (m_table.Model.GetRowCount() > 0)
            {
                if (m_isRugPickup)
                    m_table.Select(0, 1);
                else
                    m_table.Select(0, 0);
            }
                
            //Check all rows
            
            OnTabChanged(null, EventArgs.Empty);
            UpdateRugsTotal();            
            
            m_table.RowChanged += new RowHandler(OnTableRowChanged);
            m_table.SelectionChanged += new SelectionHandler(OnTableSelectionChanged);
        }

        private void OnTableSelectionChanged()
        {
            UpdateRugsTotal();
        }

        #region OnTableRowChanged

        private void OnTableRowChanged(int rowIndex)
        {
            OnTabChanged(null, EventArgs.Empty);
        }

        #endregion

        #endregion

        #region UpdateRugsTotal

        private decimal GetRugsTotal(bool isIncludeTax)
        {
            decimal rugsTotal = decimal.Zero;

            for (int i = 0; i < Model.CurrentTicket.Rugs.Count; i++)
            {
                if ((m_isRugPickup && m_table.IsRowSelected(i)) || !m_isRugPickup)
                {
                    if (isIncludeTax)
                        rugsTotal += Model.CurrentTicket.Rugs[i].TotalWithTaxCost;
                    else
                        rugsTotal += Model.CurrentTicket.Rugs[i].TotalCost;
                }                    
            }

            return rugsTotal;            
        }
        
        private void UpdateRugsTotal()
        {
            decimal subTotal = GetRugsTotal(false);
            decimal total = GetRugsTotal(true);

            m_lblJobSubTotal.Text = subTotal.ToString("C");            
            m_lblJobTax.Text = (total - subTotal).ToString("C");
            m_lblJobTotal.Text = total.ToString("C");            
        }

        #endregion

        #region Joystick

        protected override void OnJoystickInit()
        {
            Joystick.Add(m_txtAddress, m_tabs, m_linkPhone1, m_tabs, m_linkPhone1);
            Joystick.Add(m_linkPhone1, m_txtAddress, m_linkPhone2, m_txtAddress, m_txtNotes);
            Joystick.Add(m_linkPhone2, m_linkPhone1, m_txtNotes, m_txtAddress, m_txtNotes);
            Joystick.Add(m_txtNotes, m_linkPhone2, m_tabs, m_linkPhone1, m_tabs);
            
            Joystick.Add(m_txtMessage, m_tabs, m_tabs, m_tabs, m_tabs);            
            Joystick.Add(m_table, m_tabs, m_tabs, m_tabs, m_tabs);
            
            Joystick.Add(m_txtUserNotes, m_tabs, m_tabs, m_tabs, m_tabs);
                        
            Joystick.Add(m_tabs, 0, m_txtNotes, m_txtAddress);
            Joystick.Add(m_tabs, 1, m_txtMessage, m_txtMessage);
            Joystick.Add(m_tabs, 2, m_table, m_table);
            Joystick.Add(m_tabs, 3, m_txtUserNotes, m_txtUserNotes);
        }

        #endregion

        #region OnClosing

        protected override void OnClosing(CancelEventArgs e)
        {
            if (Model.CurrentTicket.Status == TicketStatus.Started)
                e.Cancel = true;
            else
                e.Cancel = false;
        }

        #endregion

        #region OnSubmitETCClick

        private void OnSubmitETCClick(object sender, EventArgs e)
        {
            SubmitETC submitETC = new SubmitETC();
            ShowForm(submitETC);
        }

        #endregion

        #region OnNoGoClick

        private void OnNoGoClick(object sender, EventArgs e)
        {
            NoGo noGo = new NoGo();
            ShowForm(noGo);
            if (noGo.IsCancelService)
            {
                Model.CurrentTicket.Status = TicketStatus.Cancelled;
                Close();
            }
                
        }

        #endregion

        #region OnCompleteClick

        private void OnCompleteClick(object sender, EventArgs e)
        {
            if (m_isRugPickup)
            {
                if (MessageBox.Show("Complete visit?", "Confirmation", MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    return;
                }
                MessageBox.Show("Dispatch notified");
                Model.CurrentTicket.Status = TicketStatus.Processed;
                Close();                
            } else
            {
                if (Model.CurrentServiceSum == decimal.Zero)
                    Model.CurrentServiceSum = GetRugsTotal(false);
                                    
                TicketReceipt ticketReceipt = new TicketReceipt();
                ShowForm(ticketReceipt);
                if (!ticketReceipt.IsCancelled)
                    Close();                
            }            
        }

        #endregion

        #region OnPhoneClick

        private void OnPhone1Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Do you want to call {0} at {1}?",
                                              Model.CurrentTicket.CustomerName, Model.CurrentTicket.Phone1),
                                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }

            try
            {
                Phone.MakeCall(Model.CurrentTicket.Phone1 + "\0", false);
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot make a call, please check your phone settings", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void OnPhone2Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Do you want to call {0} at {1}?",
                                              Model.CurrentTicket.CustomerName, Model.CurrentTicket.Phone2),
                                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }

            try
            {
                Phone.MakeCall(Model.CurrentTicket.Phone2 + "\0", false);
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot make a call, please check your phone settings", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        #endregion

        #region Table

        public int GetRowCount()
        {
            return Model.CurrentTicket.Rugs.Count;
        }

        public int GetColumnCount()
        {
            if (m_isRugPickup)
                return 3;
            return 2;
        }

        public string GetColumnName(int columnIndex)
        {
            if ((columnIndex == 1 && m_isRugPickup) || (columnIndex == 0 && !m_isRugPickup))
                return "Dimension";
            else if ((columnIndex == 2 && m_isRugPickup) || (columnIndex == 1 && !m_isRugPickup))
                return "Cost";

            return string.Empty;
        }

        public Type GetColumnClass(int columnIndex)
        {
            return typeof(string);
        }

        public bool IsCellEditable(int rowIndex, int columnIndex)
        {
            return false;
        }

        public object GetValueAt(int rowIndex, int columnIndex)
        {
            if ((columnIndex == 1 && m_isRugPickup) || (columnIndex == 0 && !m_isRugPickup))
            {
                if (Model.CurrentTicket.Rugs[rowIndex].Shape == RugShape.Rectangle)
                {
                    return "Rect, " + Model.CurrentTicket.Rugs[rowIndex].Width
                           + "x" + Model.CurrentTicket.Rugs[rowIndex].Height
                           + ", " + Model.CurrentTicket.Rugs[rowIndex].SquareFootage.ToString("0.00")
                           + "SF";
                }
                else if (Model.CurrentTicket.Rugs[rowIndex].Shape == RugShape.Round)
                {
                    return "Round, D" + Model.CurrentTicket.Rugs[rowIndex].Diameter
                           + ", " + Model.CurrentTicket.Rugs[rowIndex].SquareFootage.ToString("0.00")
                           + "SF";                    
                }
                return string.Empty;
            }
            else if ((columnIndex == 2 && m_isRugPickup) || (columnIndex == 1 && !m_isRugPickup))
                return Model.CurrentTicket.Rugs[rowIndex].TotalWithTaxCost.ToString("C");

            return string.Empty;
        }

        public void SetValueAt(object aValue, int rowIndex, int columnIndex)
        {
            return;
        }

        public object GetObjectAt(int rowIndex, int columnIndex)
        {
            return null;
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

        private void OnAddRugClick(object sender, EventArgs e)
        {                        
            AddRug addRug = new AddRug();
            addRug.RugAction = RugAction.Add;
            ShowForm(addRug);
            
            if (!addRug.IsCancelled)
            {
                m_tabs.SelectedIndex = 2;                  
                Model.CurrentTicket.Rugs.Add(addRug.OutputRug);                                
                
                if (Model.CurrentTicket.Rugs.Count == 1)
                    m_table.BindModel(this);

                List<int> selectionIndexes = new List<int>();
                selectionIndexes.Add(Model.CurrentTicket.Rugs.Count - 1);
                m_table.Select(selectionIndexes);
                
                m_table.Select(Model.CurrentTicket.Rugs.Count - 1, 1);                
                m_table.Focus();
                UpdateRugsTotal();
            }
            OnTabChanged(null, EventArgs.Empty);
        }

        private void OnEditRugClick(object sender, EventArgs e)
        {
            if (m_table.CurrentRowIndex >= 0)
            {
                AddRug addRug = new AddRug();
                addRug.RugAction = RugAction.Edit;
                addRug.InputRug = Model.CurrentTicket.Rugs[m_table.CurrentRowIndex];
                ShowForm(addRug);

                if (!addRug.IsCancelled)
                {
                    Model.CurrentTicket.Rugs[m_table.CurrentRowIndex] = new Rug(addRug.OutputRug);
                    m_table.Update();                    
                    m_table.Focus();
                    UpdateRugsTotal();
                }
                OnTabChanged(null, EventArgs.Empty);
            }
        }

        private void OnDeleteRugClick(object sender, EventArgs e)
        {
            if (m_table.CurrentRowIndex >= 0)
            {
                if (MessageBox.Show("Do you want to delete this Rug?", "Confirmation", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, 
                    MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    return;
                }

                Model.CurrentTicket.Rugs.RemoveAt(m_table.CurrentRowIndex);
                if (Model.CurrentTicket.Rugs.Count > 0)
                    m_table.Select(0, 1);                
                m_table.Update();
                m_table.Focus();
                UpdateRugsTotal();
                OnTabChanged(null, EventArgs.Empty);
            }
        }

        private void OnViewRugClick(object sender, EventArgs e)
        {
            if (m_table.CurrentRowIndex >= 0)
            {
                AddRug addRug = new AddRug();
                addRug.RugAction = RugAction.View;
                addRug.InputRug = Model.CurrentTicket.Rugs[m_table.CurrentRowIndex];
                ShowForm(addRug);

                m_table.Focus();
            }
        }

        private void OnTabChanged(object sender, EventArgs e)
        {
            
            if (m_tabs.SelectedIndex == 2 && m_table.Model.GetRowCount() > 0 && m_table.CurrentRowIndex >= 0)
            {
                if (m_isRugPickup)
                {
                    m_menuAddRug.Enabled = true;
                    m_menuEditRug.Enabled = true;
                    m_menuDeleteRug.Enabled = true;
                    m_menuViewRug.Enabled = true;                    
                } else
                {
                    m_menuAddRug.Enabled = false;
                    m_menuEditRug.Enabled = false;
                    m_menuDeleteRug.Enabled = false;
                    m_menuViewRug.Enabled = true;                                        
                }
            } else 
            {
                if (m_isRugPickup)
                {
                    m_menuAddRug.Enabled = true;
                    m_menuEditRug.Enabled = false;
                    m_menuViewRug.Enabled = false;
                    m_menuDeleteRug.Enabled = false;                    
                } else
                {
                    m_menuAddRug.Enabled = false;
                    m_menuEditRug.Enabled = false;
                    m_menuViewRug.Enabled = false;
                    m_menuDeleteRug.Enabled = false;                                        
                }
            } 
        }
    }        
}