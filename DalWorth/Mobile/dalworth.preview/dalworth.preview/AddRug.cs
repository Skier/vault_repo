using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using dalworth.domain;
using hobson.controls;

namespace dalworth.preview
{
    public partial class AddRug : BaseForm
    {
        public AddRug()
        {
            InitializeComponent();
        }

        private RugAction m_rugAction;
        public RugAction RugAction
        {
            get { return m_rugAction; }
            set { m_rugAction = value; }
        }

        private Rug m_temporaryRug;

        private Rug m_inputRug;
        public Rug InputRug
        {
            get { return m_inputRug; }
            set { m_inputRug = value; }
        }
                
        private Rug m_outputRug;
        public Rug OutputRug
        {
            get { return m_outputRug; }
            set { m_outputRug = value; }
        }

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
            set { m_isCancelled = value; }
        }

        private decimal m_jobTotalWithoutCurrentRug;

        protected override void OnFormLoad(EventArgs e)
        {
            m_isCancelled = true;

            foreach (Rug rug in Model.CurrentTicket.Rugs)
            {
                if (rug != m_inputRug)
                    m_jobTotalWithoutCurrentRug += rug.TotalWithTaxCost;
            }            
            
            if (m_rugAction == RugAction.Add)
            {
                Text = "0231 Add Rug";                
                m_temporaryRug = new Rug(RugShape.Rectangle, 0, 0, 0, true, false, false, false, false, 0);
                LoadRug();
            }                
            else if (m_rugAction == RugAction.Edit)
            {
                Text = "0231 Edit Rug";
                m_temporaryRug = new Rug(m_inputRug);
                LoadRug();
            }                
            else if (m_rugAction == RugAction.View)
            {
                Text = "0231 View Rug";
                m_temporaryRug = new Rug(m_inputRug);
                LoadRug();
                m_cmbShape.Enabled = false;                
                m_txtDiameter.Enabled = false;                
                m_txtWidth.Enabled = false;                
                m_txtHeight.Enabled = false;                
                m_chkProtector.Enabled = false;                
                m_chkPadding.Enabled = false;                
                m_chkMothRepel.Enabled = false;                
                m_chkRap.Enabled = false;                
                m_curOther.Enabled = false;                
            }
                        
            m_cmbShape.Focus();
        }

        protected override void OnJoystickInit()
        {
            Joystick.Add(m_cmbShape, m_curOther, m_txtDiameter, m_curOther, m_txtDiameter);
            Joystick.Add(m_txtDiameter, m_cmbShape, m_txtWidth, m_cmbShape, m_txtWidth);
            Joystick.Add(m_txtWidth, m_txtDiameter, m_txtHeight, m_txtDiameter, m_chkProtector);
            Joystick.Add(m_txtHeight, m_txtWidth, m_chkProtector, m_cmbShape, m_chkProtector);
            Joystick.Add(m_chkProtector, m_txtHeight, m_chkPadding, m_txtWidth, m_chkPadding);
            Joystick.Add(m_chkPadding, m_chkProtector, m_chkMothRepel, m_chkProtector, m_chkMothRepel);
            Joystick.Add(m_chkMothRepel, m_chkPadding, m_chkRap, m_chkPadding, m_chkRap);
            Joystick.Add(m_chkRap, m_chkMothRepel, m_curOther, m_chkMothRepel, m_curOther);
            Joystick.Add(m_curOther, m_chkRap, m_cmbShape, m_chkRap, m_cmbShape);
        }

        private void OnShapeChanged(object sender, EventArgs e)
        {
            if (m_cmbShape.SelectedIndex == 0)
            {
                m_lblDimenstion.Visible = true;
                m_txtHeight.Visible = true;
                m_lblMultiply.Visible = true;
                m_txtWidth.Visible = true;

                m_lblDiameter.Visible = false;
                m_txtDiameter.Visible = false;

                m_temporaryRug.Shape = RugShape.Rectangle;
            } else
            {
                m_lblDimenstion.Visible = false;
                m_txtHeight.Visible = false;
                m_lblMultiply.Visible = false;
                m_txtWidth.Visible = false;

                m_lblDiameter.Visible = true;
                m_txtDiameter.Visible = true;

                m_temporaryRug.Shape = RugShape.Round;
            }
            
            UpdateLabels();                        
        }
                
        private void LoadRug()
        {
            if (m_temporaryRug.Shape == RugShape.Rectangle)
                m_cmbShape.SelectedIndex = 0;
            else
                m_cmbShape.SelectedIndex = 1;

            if (m_temporaryRug.Height != decimal.Zero)
                m_txtHeight.Text = m_temporaryRug.Height.ToString("0.00");
            if (m_temporaryRug.Width != decimal.Zero)
                m_txtWidth.Text = m_temporaryRug.Width.ToString("0.00");
            if (m_temporaryRug.Diameter != decimal.Zero)
                m_txtDiameter.Text = m_temporaryRug.Diameter.ToString("0.00");

            m_chkProtector.Checked = m_temporaryRug.IsProtectorApplied;
            m_chkPadding.Checked = m_temporaryRug.IsPaddingApplied;
            m_chkMothRepel.Checked = m_temporaryRug.IsMothRepelApplied;
            m_chkRap.Checked = m_temporaryRug.IsRapApplied;
            m_curOther.Value = m_temporaryRug.OtherCost;

            UpdateLabels();
        }

        private void UpdateLabels()
        {
            m_lblSquareFootage.Text = m_temporaryRug.SquareFootage.ToString("0.00") + " SF";
            m_lblCleanCost.Text = m_temporaryRug.CleanCost.ToString("C");
            m_lblProtectorCost.Text = m_temporaryRug.ProtectorCost.ToString("C");
            m_lblPaddingCost.Text = m_temporaryRug.PaddingCost.ToString("C");
            m_lblMothRepelCost.Text = m_temporaryRug.MothRepelCost.ToString("C");
            m_lblRapCost.Text = m_temporaryRug.RapCost.ToString("C");
            m_lblTotalCost.Text = m_temporaryRug.TotalCost.ToString("C");
            m_lblTaxAmount.Text = m_temporaryRug.TaxAmount.ToString("C");
            m_lblTotal.Text = m_temporaryRug.TotalWithTaxCost.ToString("C");
            m_lblJobTotal.Text = (m_temporaryRug.TotalWithTaxCost + m_jobTotalWithoutCurrentRug).ToString("C");
        }
        
        private void OnCancelClick(object sender, EventArgs e)
        {
            Close();
        }

        private void OnDoneClick(object sender, EventArgs e)
        {                        
            if (m_temporaryRug.Shape == RugShape.Round)
            {
                if (m_temporaryRug.Diameter == decimal.Zero)
                {
                    MessageBox.Show("Please enter Diameter");
                    m_txtDiameter.SelectAll();
                    m_txtDiameter.Focus();
                    return;
                }
            } else
            {
                if (m_temporaryRug.Width == decimal.Zero)
                {
                    MessageBox.Show("Please enter Width");
                    m_txtWidth.SelectAll();
                    m_txtWidth.Focus();
                    return;
                } else if (m_temporaryRug.Height == decimal.Zero)
                {
                    MessageBox.Show("Please enter Height");
                    m_txtHeight.SelectAll();
                    m_txtHeight.Focus();
                    return;
                } 
                
            }

            m_isCancelled = false;
            m_outputRug = new Rug(m_temporaryRug);
            Close();
        }

        private void OnDiameterChanged(object sender, EventArgs e)
        {
            if (m_txtDiameter.Text.Length != 0)
            {
                try
                {
                    decimal.Parse(m_txtDiameter.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Please enter correct Diameter");
                    m_txtDiameter.SelectAll();
                    m_txtDiameter.Focus();
                    return;
                }
                m_temporaryRug.Diameter = decimal.Parse(m_txtDiameter.Text);
            } else
            {
                m_temporaryRug.Diameter = decimal.Zero;
            }
                        
            UpdateLabels();
        }

        private void OnHeightChanged(object sender, EventArgs e)
        {
            if (m_txtHeight.Text.Length != 0)
            {
                try
                {
                    decimal.Parse(m_txtHeight.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Please enter correct Height");
                    m_txtHeight.SelectAll();
                    m_txtHeight.Focus();
                    return;
                }
                m_temporaryRug.Height = decimal.Parse(m_txtHeight.Text);
            }
            else
            {
                m_temporaryRug.Height = decimal.Zero;
            }

            UpdateLabels();
        }

        private void OnWidthChanged(object sender, EventArgs e)
        {
            if (m_txtWidth.Text.Length != 0)
            {
                try
                {
                    decimal.Parse(m_txtWidth.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Please enter correct Width");
                    m_txtWidth.SelectAll();
                    m_txtWidth.Focus();
                    return;
                }
                m_temporaryRug.Width = decimal.Parse(m_txtWidth.Text);
            }
            else
            {
                m_temporaryRug.Width = decimal.Zero;
            }

            UpdateLabels();
        }

        private void OnProtectorChanged(object sender, EventArgs e)
        {
            m_temporaryRug.IsProtectorApplied = m_chkProtector.Checked;
            UpdateLabels();            
        }

        private void OnPaddingChanged(object sender, EventArgs e)
        {
            m_temporaryRug.IsPaddingApplied = m_chkPadding.Checked;
            UpdateLabels();            
        }

        private void OnMothRepelChanged(object sender, EventArgs e)
        {
            m_temporaryRug.IsMothRepelApplied = m_chkMothRepel.Checked;
            UpdateLabels();            
        }

        private void OnRapChanged(object sender, EventArgs e)
        {
            m_temporaryRug.IsRapApplied = m_chkRap.Checked;
            UpdateLabels();            
        }

        private void OnOtherAmountChanged(object sender, EventArgs e)
        {
            m_temporaryRug.OtherCost = m_curOther.Value.Value;
            UpdateLabels();
        }
    }
    
    public enum RugAction
    {
        Add,
        Edit,
        View
    }
}