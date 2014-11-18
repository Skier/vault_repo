using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace SmartSchedule.Windows
{


    public partial class BaseForm : Form
    {

        #region Events
        public event System.ComponentModel.CancelEventHandler Cancel;
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

        #endregion

        #region Constructor

        public BaseForm()
        {
            InitializeComponent();

            this.KeyPreview = true;

#if WIN32
            MaximizeBox = false;
            BackColor = Color.White;


            StartPosition = FormStartPosition.CenterParent;
            SizeGripStyle = SizeGripStyle.Hide;
            FormBorderStyle = FormBorderStyle.FixedDialog;
#endif
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

#if WIN32
            ApplyWin32Style(this);
#endif
            Host.Instance.CultureChange += new CultureChangeHandler(OnCultureChanged);

            ApplyUIResources(Host.Instance.Culture);

#if WIN32
            CenterToParent();
#endif
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

        #region ApplyWin32Style

#if WIN32
        public static void ApplyWin32Style(Form form)
        {
            foreach (Control control in form.Controls)
            {
                if (control is ComboBox)
                {
                    ComboBox comboBox = control as ComboBox;

                    comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    comboBox.FlatStyle = FlatStyle.System;
                }
            }

            form.BackColor = Color.White;
            form.StartPosition = FormStartPosition.CenterParent;
        }
#endif
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