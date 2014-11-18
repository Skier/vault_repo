using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Dalworth.Controls;

namespace Dalworth.Windows
{
    public partial class BaseForm : Form
    {
        Joystick m_joystick = new Joystick();

        #region Events
        public event CancelEventHandler Cancel;
        #endregion

        #region Fields

        protected bool m_selfClose;
 
        public bool IsSelfClose
        {
            get
            {
                return m_selfClose;
            }
        }

        public Joystick Joystick
        {
            get { return m_joystick; }
        }     

        #endregion

        #region Constructor

        public BaseForm()
        {
            InitializeComponent();

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

            ApplyUIResources(Host.Instance.Culture);

            base.OnLoad(e);
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

        protected bool OnCancel(String confirmMessage)
        {
              bool cancel = MessageDialog.Show(
                MessageDialogType.Question, confirmMessage) == DialogResult.No;

              if (cancel)
                  Activate();

              return cancel;
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

        DialogResult m_dialogResult;

        public new DialogResult DialogResult
        {
            get { return m_dialogResult; }
            set { m_dialogResult = value; }
        }

        public new DialogResult ShowDialog()
        {
            base.ShowDialog();

            return m_dialogResult;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //e.Graphics.Clear(BackColor);
        }
    }
}