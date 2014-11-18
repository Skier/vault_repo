using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace QuickBooksAgent.Windows.UI
{
    public class MainFormController
    {
        private MainFormController()
        {
            Form.Closing += new System.ComponentModel.CancelEventHandler(OnClosing);
            Form.m_defaultAction.Click += new EventHandler(OnDefaultActionClick);
            Form.Resize += new EventHandler(OnFormResize);

#if !SMARTPHONE
            Form.m_sip.EnabledChanged += new System.EventHandler(this.OnSipEnabledChanged);
#endif

        }

        void OnFormResize(object sender, EventArgs e)
        {
            UpdateViewHeight();
        }

        #region OnSipEnabledChanged
        private void OnSipEnabledChanged(object sender, EventArgs e)
        {
            UpdateViewHeight();
        }
        #endregion

        private void UpdateViewHeight()
        {
#if !SMARTPHONE
            if (Form.m_sip.Enabled)
                Form.m_viewPanel.Height
                    = Form.Height - Form.m_sip.Bounds.Height;
            else
#endif
                Form.m_viewPanel.Height
                    = Form.Height;

        }

        void OnDefaultActionClick(object sender, EventArgs e)
        {
            //MessageBox.Show("OnDefaultActionClick");

            if (m_controllers.Count > 0 &&
                 m_controllers.Peek().IsDefaultActionExist)
            {
                m_controllers.Peek().OnDefaultAction();
            }
        }


        void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (m_controllers.Count == 0)
                return;

            SingleFormController activeController = m_controllers.Peek();

            if (activeController.Save())
            {
                OnUnregister();
            }

            e.Cancel = m_controllers.Count > 0;
        }

        static MainFormController s_instance;

        Stack<SingleFormController> m_controllers = new Stack<SingleFormController>();

        public MainForm Form
        {
            get
            {
                return MainForm.Instance;
            }
        }

        public static MainFormController Instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = new MainFormController();

                return s_instance;
            }
        }

        public static void Register(SingleFormController controller)
        {
            Instance.OnRegister(controller);
        }

        public static void Unregister()
        {
            Instance.OnUnregister();
        }

        private void OnRegister(SingleFormController controller)
        {

            Host.Trace("MainFormController::OnRegister", String.Format(
                    "Registering {0} controller", controller.ToString()));

            m_controllers.Push(controller);

            controller.BaseViewInstance.Dock = System.Windows.Forms.DockStyle.Fill;

            Form.m_viewPanel.Controls.Add(controller.BaseViewInstance);

            controller.OnViewLoad();

            OnActivate(controller);
        }

        private void OnUnregister()
        {
            SingleFormController activeController =
                    m_controllers.Peek();


                Host.Trace("MainFormController::OnUnregister", String.Format(
                        "Unregistering {0} controller", activeController.ToString()));


            Form.m_viewPanel.Controls.Remove(activeController.BaseViewInstance);

            m_controllers.Pop();

            activeController.OnClose();

            if (m_controllers.Count > 0)
                OnActivate(m_controllers.Peek());
        }

        private void OnActivate(SingleFormController controller)
        {


                Host.Trace("MainFormController::OnActivate", String.Format(
                        "Activating {0} controller", controller.ToString()));

            Form.Text = controller.BaseViewInstance.Text;

            Form.m_defaultAction.Text = controller.DefaultActionName;
            IsDefaultActionEnabled = controller.IsDefaultActionExist;

            Form.m_subMenu.MenuItems.Clear();

            foreach (MenuItem menuItem in controller.BaseViewInstance.MenuItems)
            {
                Form.m_subMenu.MenuItems.Add(menuItem);
            }

            if (Form.m_subMenu.MenuItems.Count > 0)
            {
                Form.m_subMenu.Enabled = true;
                Form.m_subMenu.Text = "Menu";
            }
            else
            {
                Form.m_subMenu.Text = String.Empty;
                Form.m_subMenu.Enabled = false;
            }

            UpdateViewHeight();

            //Application.DoEvents();

            controller.BaseViewInstance.BringToFront();

            controller.OnViewActivated();


        }

        internal static void RefreshDefaultAction()
        {
            Instance.OnRefreshDefaultAction();
        }

        private void OnRefreshDefaultAction()
        {
            Debug.Assert(m_controllers.Count > 0, "Least one controller must exists");

            SingleFormController activeController = m_controllers.Peek();

            if (activeController.IsDefaultActionExist)
            {
                Form.m_defaultAction.Text = activeController.DefaultActionName;
                IsDefaultActionEnabled = true;
            }
            else
            {
                IsDefaultActionEnabled = false;
            }

            //Application.DoEvents();
        }

        private bool IsDefaultActionEnabled
        {
            get { return Form.m_defaultAction.Enabled; }
            set 
            {
                Debug.WriteLineIf(value, "MainFormController::IsDefaultActionEnabled - True");
                Debug.WriteLineIf(!value, "MainFormController::IsDefaultActionEnabled - False");

                Form.m_defaultAction.Enabled = value; 
            }
        }
    }
}
