using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using Dalworth.ConnectionMonitor;
using Dalworth.Controls;
using Dalworth.GPS;
using Dalworth.SDK;
using Microsoft.WindowsMobile.Telephony;
using OpenNETCF.Windows.Forms;

namespace Dalworth.Windows
{
    public class MainFormController
    {
        private EventHandler m_gpsDataUpdateHandler;
        private GpsPosition m_gpsPosition = null;
        private Gps m_gps;
        private bool m_isFormDisposed;
        private bool m_isApplicationShouldBeClosed = false;

        public delegate void GpsPositionChandedHandler(GpsPosition position);
        public event GpsPositionChandedHandler GpsPositionChanded;

        static MainFormController s_instance;
        Stack<SingleFormController> m_controllers = new Stack<SingleFormController>();

        #region Properties

        #region Form

        public MainForm Form
        {
            get
            {
                return MainForm.Instance;
            }
        }

        #endregion

        #region Instance

        public static MainFormController Instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = new MainFormController();

                return s_instance;
            }
        }

        #endregion

        #region IsLeftActionEnabled

        private bool IsLeftActionEnabled
        {
            set
            {
                Form.m_leftAction.Enabled = value;
            }
        }

        #endregion

        #region IsRightActionEnabled

        private bool IsRightActionEnabled
        {
            set
            {
                Form.m_rightAction.Enabled = value;
            }
        }

        #endregion

        #endregion

        #region MainFormController

        private MainFormController()
        {   
            Form.Closing += new CancelEventHandler(OnClosing);
            Form.m_leftAction.Click += new EventHandler(OnLeftActionClick);
            Form.m_rightAction.Click += new EventHandler(OnRightActionClick);
            Form.Resize += new EventHandler(OnFormResize);
            Form.Load += OnFormLoad;
            Form.m_pnlExit.Click += OnExitClick;
            Form.m_messageWindow.HardwareButtonKeyPress += OnHardwareButtonKeyPress;
            Form.Closed += OnFormClosed;

            Form.m_timerClock.Tick += OnTimerClockTick;
            Form.m_timerBatteryUpdate.Tick += OnTimerBatteryUpdateTick;
            Form.m_timerStayAlive.Tick += OnTimerStayAliveTick;
            OnTimerBatteryUpdateTick(null, EventArgs.Empty);
            OnTimerClockTick(null, EventArgs.Empty);

            m_gps = new Gps();
            ConnectionMonitor.ConnectionMonitor monitor = ConnectionMonitorFactory.Create();                        
            monitor.Connections[0].StateChanged += OnConnectionStateChanged;
            monitor.Connections[1].StateChanged += OnConnectionStateChanged;
            monitor.Connections[2].StateChanged += OnConnectionStateChanged;            
            OnConnectionStateChanged(null, null);
        }

        #endregion

        #region OnConnectionStateChanged

        private void OnConnectionStateChanged(object sender, StateChangedEventArgs e)
        {
            if (ConnectionMonitorFactory.Instance.IsConnected)
            {
                Form.m_pictureConnectionFound.Visible = true;
                Form.m_pictureConnectionLost.Visible = false;
            } else
            {
                Form.m_pictureConnectionFound.Visible = false;
                Form.m_pictureConnectionLost.Visible = true;                
            }
        }

        #endregion

        #region OnFormLoad

        private void OnFormLoad(object sender, EventArgs e)
        {
            FullScreenEngine.InitFullScreen(Form);
            FullScreenEngine.Activate();

            OnTimerStayAliveTick(null, EventArgs.Empty);
            m_gpsDataUpdateHandler = new EventHandler(GpsDataUpdate);
            m_gps.LocationChanged += OnGpsLocationChanged;

            if (!m_gps.Opened)
            {
                m_gps.Open();
            }
        }

        #endregion

        #region OnTimerBatteryUpdateTick

        private void OnTimerBatteryUpdateTick(object sender, EventArgs e)
        {
            Form.m_batteryLife.UpdateBatteryLife();
            if (Form.m_batteryLife.PowerStatus.BatteryLifePercent > 60)
            {
                Form.m_batteryLife.BorderColor = Color.Green;
                Form.m_batteryLife.PercentageBarColor = Color.Green;
            }                
            else if (Form.m_batteryLife.PowerStatus.BatteryLifePercent > 30)
            {
                Form.m_batteryLife.BorderColor = Color.Gold;
                Form.m_batteryLife.PercentageBarColor = Color.Gold;
            }                
            else
            {
                Form.m_batteryLife.BorderColor = Color.Red;
                Form.m_batteryLife.PercentageBarColor = Color.Red;
            }                
        }

        #endregion

        #region OnTimerStayAliveTick

        private static void OnTimerStayAliveTick(object sender, EventArgs e)
        {
            WinAPI.SystemIdleTimerReset();
        }

        #endregion

        #region OnExitClick

        private void OnExitClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Close the application?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                m_isApplicationShouldBeClosed = true;
                Form.Close();
            }
        }

        #endregion

        #region OnHardwareButtonKeyPress

        private static void OnHardwareButtonKeyPress(KeysHardware key)
        {
            if (key == KeysHardware.Hardware7)
            {
                try
                {
                    Phone phone = new Phone();
                    phone.Talk(Configuration.DispatcherPhoneNumber + "\0", false);
                }
                catch (Exception)
                {
                    MessageDialog.Show(MessageDialogType.Warning, "Cannot make a call, please check your phone settings");
                }
            }
        }

        #endregion

        #region GpsDataUpdate

        private void GpsDataUpdate(object sender, EventArgs args)
        {
            if (m_gps.Opened && m_gpsPosition != null
                && m_gpsPosition.LatitudeValid && m_gpsPosition.LongitudeValid && m_gpsPosition.TimeValid
                && m_gpsPosition.SatelliteCount >= 3)
            {
                if (Form.m_pictureGpsLost.Visible)
                {
                    Form.m_pictureGpsFound.Visible = true;
                    Form.m_pictureGpsLost.Visible = false;
                }
                
                if (GpsPositionChanded != null)
                    GpsPositionChanded.Invoke(m_gpsPosition);
            }
            else
            {
                if (Form.m_pictureGpsFound.Visible)
                {
                    Form.m_pictureGpsFound.Visible = false;
                    Form.m_pictureGpsLost.Visible = true;
                }
            }
        }

        #endregion

        #region OnGpsLocationChanged

        private void OnGpsLocationChanged(object sender, LocationChangedEventArgs args)
        {
            if (!m_isFormDisposed)
            {
                m_gpsPosition = args.Position;
                Form.Invoke(m_gpsDataUpdateHandler);                
            } 
        }

        #endregion


        #region OnTimerClockTick

        private void OnTimerClockTick(object sender, EventArgs e)
        {
            if (Form.m_lblTime.Text != DateTime.Now.ToString("h:mm tt"))
                Form.m_lblTime.Text = DateTime.Now.ToString("h:mm tt");
        }

        #endregion

        #region OnFormResize

        void OnFormResize(object sender, EventArgs e)
        {
            UpdateViewHeight();
        }

        #endregion

        #region UpdateViewHeight

        private void UpdateViewHeight()
        {
            Form.m_pnlContent.Height
                = Form.Height;
        }

        #endregion

        #region OnLeftActionClick

        void OnLeftActionClick(object sender, EventArgs e)
        {
            if (m_controllers.Count > 0 &&
                m_controllers.Peek().IsLeftActionExist)
            {
                m_controllers.Peek().OnLeftAction();
            }
        }

        #endregion

        #region OnRightActionClick

        void OnRightActionClick(object sender, EventArgs e)
        {
            if (m_controllers.Count > 0 &&
                m_controllers.Peek().IsRightActionExist)
            {
                m_controllers.Peek().OnRightAction();
            }
        }

        #endregion

        #region OnClosing

        void OnClosing(object sender, CancelEventArgs e)
        {
            if (m_controllers.Count == 0 || m_isApplicationShouldBeClosed)
                return;

            SingleFormController activeController = m_controllers.Peek();

            if (activeController.Save())
            {
                OnUnregister();
            }

            if (m_controllers.Count > 0)
                e.Cancel = true;
        }

        #endregion

        #region OnFormClosed

        private void OnFormClosed(object sender, EventArgs e)
        {
            FullScreenEngine.Dactivate();

            if (m_gps.Opened)
            {
                m_gps.Close();
            }
        }

        #endregion

        #region Register

        public static void Register(SingleFormController controller)
        {
            Instance.OnRegister(controller);
        }

        #endregion

        #region Unregister

        public static void Unregister()
        {
            Instance.OnUnregister();
        }

        #endregion

        #region OnRegister

        private void OnRegister(SingleFormController controller)
        {

            Host.Trace("MainFormController::OnRegister", String.Format(
                                                             "Registering {0} controller", controller));

            m_controllers.Push(controller);

            controller.BaseViewInstance.Dock = DockStyle.Fill;

            Form.m_pnlContent.Controls.Add(controller.BaseViewInstance);

            controller.OnViewLoad();

            OnActivate(controller);
        }

        #endregion

        #region OnUnregister

        private void OnUnregister()
        {
            SingleFormController activeController =
                m_controllers.Peek();


            Host.Trace("MainFormController::OnUnregister", String.Format(
                                                               "Unregistering {0} controller", activeController));


            Form.m_pnlContent.Controls.Remove(activeController.BaseViewInstance);

            m_controllers.Pop();

            activeController.OnClose();

            if (m_controllers.Count > 0)
                OnActivate(m_controllers.Peek());
            else
                m_isFormDisposed = true;
        }

        #endregion

        #region UpdateCaption

        public static void UpdateCaption(SingleFormController controller)
        {
            Instance.OnUpdateCaption(controller);
        }

        #endregion

        #region OnUpdateCaption

        private void OnUpdateCaption(SingleFormController controller)
        {
            Form.m_lblTitle.Text = controller.BaseViewInstance.Text;            
        }

        #endregion

        #region OnActivate

        private void OnActivate(SingleFormController controller)
        {
            Host.Trace("MainFormController::OnActivate", String.Format(
                                                             "Activating {0} controller", controller));

            Form.m_lblTitle.Text = controller.BaseViewInstance.Text;

            Form.m_leftAction.Text = string.Empty;
            Form.m_leftAction.MenuItems.Clear();
            foreach (MenuItem menuItem in controller.BaseViewInstance.MenuItemsLeft)
            {
                Form.m_leftAction.MenuItems.Add(menuItem);
            }
            Form.m_leftAction.Text = controller.LeftActionName;
            IsLeftActionEnabled = controller.IsLeftActionExist;

            Form.m_rightAction.Text = string.Empty;
            Form.m_rightAction.MenuItems.Clear();
            foreach (MenuItem menuItem in controller.BaseViewInstance.MenuItemsRight)
            {
                Form.m_rightAction.MenuItems.Add(menuItem);
            }
            Form.m_rightAction.Text = controller.RightActionName;
            IsRightActionEnabled = controller.IsRightActionExist;

            UpdateViewHeight();

            //Application.DoEvents();

            controller.BaseViewInstance.BringToFront();

            controller.OnViewActivated();
        }

        #endregion

        #region RefreshLeftAction

        internal static void RefreshLeftAction()
        {
            Instance.OnRefreshLeftAction();
        }

        #endregion

        #region RefreshRightAction

        internal static void RefreshRightAction()
        {
            Instance.OnRefreshRightAction();
        }

        #endregion

        #region OnRefreshLeftAction

        private void OnRefreshLeftAction()
        {
            Debug.Assert(m_controllers.Count > 0, "Least one controller must exists");

            SingleFormController activeController = m_controllers.Peek();

            if (activeController.IsLeftActionExist)
            {
                Form.m_leftAction.Text = activeController.LeftActionName;
                IsLeftActionEnabled = true;
            }
            else
            {
                IsLeftActionEnabled = false;
            }            
        }

        #endregion

        #region OnRefreshRightAction

        private void OnRefreshRightAction()
        {
            Debug.Assert(m_controllers.Count > 0, "Least one controller must exists");

            SingleFormController activeController = m_controllers.Peek();

            if (activeController.IsRightActionExist)
            {
                Form.m_rightAction.Text = activeController.RightActionName;
                IsRightActionEnabled = true;
            }
            else
            {
                IsRightActionEnabled = false;
            }            
        }

        #endregion
    }
}
