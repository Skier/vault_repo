using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using SmartSchedule.Domain;
using SmartSchedule.Win32.Authenticate;
using SmartSchedule.Windows;
using Point = System.Drawing.Point;

namespace SmartSchedule.Win32.Controls
{
    public partial class BaseForm : Form
    {

        #region Events
        public event System.ComponentModel.CancelEventHandler Cancel;
        #endregion

        #region Fields

        private Image m_imageLocked;
        private Image m_imageUnlocked;
        private Dictionary<Control, bool> m_controlInitialStateMap;

        #region IsSelfClose

        protected bool m_selfClose; 
        public bool IsSelfClose
        {
            get
            {
                return m_selfClose;
            }
        }

        #endregion

        #region DialogResult

        DialogResult m_dialogResult;
        public new DialogResult DialogResult
        {
            get { return m_dialogResult; }
            set { m_dialogResult = value; }
        }

        #endregion

        #region MinRequiredUserRole

        private UserRoleEnum m_minRequiredUserRole;
        public UserRoleEnum MinRequiredUserRole
        {
            get { return m_minRequiredUserRole; }
            set
            {
                m_minRequiredUserRole = value;
                m_btnLock.Visible = m_minRequiredUserRole != UserRoleEnum.Anonymous;
                LockUnlockControls();
            }
        }

        #endregion

        #region AlwaysAllowedControls

        private List<Control> m_alwaysAllowedControls;
        public List<Control> AlwaysAllowedControls
        {
            get { return m_alwaysAllowedControls; }
        }

        #endregion

        #endregion

        #region Constructor

        public BaseForm()
        {            
            m_alwaysAllowedControls = new List<Control>();
            m_controlInitialStateMap = new Dictionary<Control, bool>();
            User.CurrentUserChanged += OnCurrentUserChanged;
            InitializeComponent();

            ComponentResourceManager resources = new ComponentResourceManager(typeof(BaseForm));
            m_imageLocked = ((Image)(resources.GetObject("Locked")));
            m_imageUnlocked = ((Image)(resources.GetObject("Unlocked")));

            MinRequiredUserRole = UserRoleEnum.Anonymous;
            KeyPreview = true;            
        }

        #endregion

        #region ApplyUIResources
        protected virtual void ApplyUIResources(CultureInfo cultureInfo)
        { }
        #endregion

        #region OnKeyPress

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (e.KeyChar == (char)Keys.Escape)
            {
                if (!OnCancel())
                {
                    m_selfClose = true;

                    WinAPI.CloseWindow(this);
                }
            }
        }

        #endregion

        #region OnClosing

        protected override void OnClosing(CancelEventArgs e)
        {
            bool cancel = !m_selfClose;

            if (cancel)
                cancel = OnCancel();

            e.Cancel = cancel;

            if (!e.Cancel)
            {
                base.OnClosing(e);
            }
        }

        #endregion

        #region OnClosed

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Host.Instance.CultureChange -= new CultureChangeHandler(OnCultureChanged);

            CleanUp();
        }

        #endregion

        #region OnLoad

        protected override void OnLoad(EventArgs e)
        {
            Host.Instance.CultureChange += new CultureChangeHandler(OnCultureChanged);
            m_btnLock.MouseClick += OnLockClick;

            ApplyUIResources(Host.Instance.Culture);
            m_btnLock.Location = new Point(2, Height - m_btnLock.Height - 31);
            Controls.SetChildIndex(m_btnLock, 0);

            base.OnLoad(e);            
        }

        #endregion        

        #region LockUnlockControls

        private void LockUnlockControls()
        {
            bool isEnable = (int)User.CurrentRole >= (int)m_minRequiredUserRole;
            m_btnLock.Image = isEnable ? m_imageUnlocked : m_imageLocked;
            if (!isEnable && m_controlInitialStateMap.Count > 0)
                return;
            LockUnlockControls(Controls, isEnable);
        }

        private void LockUnlockControls(Control.ControlCollection controlCollection, bool isEnable)
        {
            foreach (var item in controlCollection)
            {
                if (!(item is Control))
                    continue;

                Control control = item as Control;                

                if (m_alwaysAllowedControls.Contains(control))
                    continue;                

                if (control.Controls.Count > 0)
                    LockUnlockControls(control.Controls, isEnable);

                if (control is Grid)
                {
                    Grid grid = (control as Grid);

                    if (isEnable)
                    {
                        if (m_controlInitialStateMap.ContainsKey(grid))
                            grid.IsReadOnly = !m_controlInitialStateMap[grid];
                    } 
                    else
                    {
                        m_controlInitialStateMap.Add(control, !grid.IsReadOnly);
                        grid.IsReadOnly = true;
                    }                    
                }                    
                else if (control is BaseEdit)
                {
                    BaseEdit baseEdit = (control as BaseEdit);

                    if (isEnable)
                    {
                        if (m_controlInitialStateMap.ContainsKey(baseEdit))
                            baseEdit.Properties.ReadOnly = !m_controlInitialStateMap[baseEdit];
                    }
                    else
                    {
                        m_controlInitialStateMap.Add(control, !baseEdit.Properties.ReadOnly);
                        baseEdit.Properties.ReadOnly = true;
                    }
                }                    
                else if (control is SimpleButton)
                {
                    SimpleButton simpleButton = (control as SimpleButton);

                    if (isEnable)
                    {
                        if (m_controlInitialStateMap.ContainsKey(simpleButton))
                            simpleButton.Enabled = m_controlInitialStateMap[simpleButton];
                    }
                    else
                    {
                        m_controlInitialStateMap.Add(control, simpleButton.Enabled);
                        simpleButton.Enabled = false;
                    }
                }
            }
        }

        #endregion

        #region OnCurrentUserChanged

        private void OnCurrentUserChanged(User newUser)
        {
            LockUnlockControls();
        }

        #endregion

        #region OnLockClick

        private void OnLockClick(object sender, MouseEventArgs mouseEventArgs)
        {   
            if (mouseEventArgs.Button != MouseButtons.Left)
                return;

            if (m_btnLock.Image == m_imageUnlocked)
                return;
            
            if (AuthenticateController.IsAccessAllowed(MinRequiredUserRole))
            {
                m_btnLock.Image = m_imageUnlocked;
                LockUnlockControls();
            }
        }

        #endregion

        #region OnCultureChanged

        void OnCultureChanged(CultureInfo cultureInfo)
        {
            ApplyUIResources(cultureInfo);

            Invalidate();
        }

        #endregion

        #region OnCancel

        protected virtual bool OnCancel()
        {
            CancelEventArgs args = new CancelEventArgs(false);

            if (Cancel != null)
                Cancel.Invoke(this, args);

            return args.Cancel;
        }

        #endregion
        
        #region Destroy

        public void Destroy()
        {
            m_selfClose = true;

            WinAPI.CloseWindow(this);
        }

        #endregion

        #region OnEnterSuppress
        protected void OnEnterSuppress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                e.Handled = true;
        }
        #endregion

        #region CleanUp

        protected virtual void CleanUp()
        {

        }

        #endregion

        #region ShowDialog

        public new DialogResult ShowDialog()
        {
            base.ShowDialog();

            return m_dialogResult;
        }

        #endregion


        #region OnPaint

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
        }

        #endregion
    }
}