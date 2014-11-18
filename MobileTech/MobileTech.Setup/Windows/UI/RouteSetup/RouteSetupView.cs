using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MobileTech.Domain;
using MobileTech.ServiceLayer;

namespace MobileTech.Windows.UI.RouteSetup
{
    public partial class RouteSetupView : BaseForm
    {
        public RouteSetupView()
        {
            InitializeComponent();
        }


        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            Resources.Culture = cultureInfo;


        }

        private void OnAcceptClick(object sender, EventArgs e)
        {
            if (!m_txtLocation.IsValid)
            {
                MessageBox.Show(Resources.MsgLocationEntryNotValid);
                m_txtLocation.Focus();
                return;
            }
            if (!m_txtRoute.IsValid)
            {
                MessageBox.Show(Resources.MsgRouteEntryNotValid);
                m_txtRoute.Focus();
                return;
            }
            if (m_txtLocation.zValue == "0")
            {
                MessageBox.Show(Resources.MsgLocationCanNotBe0);
                m_txtLocation.Focus();
                return;
            }
            if (m_txtRoute.zValue == "0")
            {
                MessageBox.Show(Resources.MsgRouteCanNotBe0);
                m_txtRoute.Focus();
                return;
            } 
            try
            {
                Configuration.Location = Convert.ToInt32(m_txtLocation.zValue);
                Configuration.Route = Convert.ToInt32(m_txtRoute.zValue);
            }
            catch
            {
            }
            
            Destroy();
        }

        private void m_mbBack_Click(object sender, EventArgs e)
        {
            Destroy();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            try
            {
                m_txtLocation.zValue = Configuration.Location.ToString();
                m_txtRoute.zValue = Configuration.Route.ToString();
            }
            catch
            {

            }
        }

        protected override bool OnCancel()
        {
            if (ChangesMade())
            {
                return base.OnCancel(CommonResources.MsgAllChangesWillBeLostExit);
            }
            else
            {
                return false;
            }
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            //App.Execute(CommandName.SetupMenu);
        }

        private void control_KeyPress(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Control objTemp = new Control();
                objTemp = (Control)sender;

                switch (objTemp.Name)
                {
                    case "m_txtLocation":
                        {
                            m_txtRoute.Focus();
                            break;
                        }
                    case "m_txtRoute":
                        {
                            m_mbAccept.Focus();
                            break;
                        }
                    case "m_mbBack":
                        {
                            m_txtLocation.Focus();
                            break;
                        }
                    case "m_mbAccept":
                        {
                            m_txtLocation.Focus();
                            break;
                        }

                }
#if !WINCE
                e.SuppressKeyPress = true;
#endif
            }
        }
        private bool ChangesMade()
        {
            if ((Configuration.Location != Convert.ToInt32(m_txtLocation.zValue)) ||
                (Configuration.Route != Convert.ToInt32(m_txtRoute.zValue)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #region IView Members
        RouteSetupModel m_model;
        public override void BindData(Object data)
        {
            if (!(data is RouteSetupModel))
                throw new MobileTechInvalidModelExeption();

            m_model = (RouteSetupModel)data;
        }

        #endregion
    }
}