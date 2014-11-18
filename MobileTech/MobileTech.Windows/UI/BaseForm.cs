using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace MobileTech.Windows.UI
{
    public partial class BaseForm : Form,IView
    {
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

#endif
        }

        #endregion

        #region ApplyUIResources
        protected virtual void ApplyUIResources(CultureInfo cultureInfo)
        { }
        #endregion

        #region OnKeyDown

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyData == Keys.Escape)
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
            base.OnLoad(e);

#if WIN32
            ApplyWin32Style(this);
#endif
            Host.Instance.CultureChange += new CultureChangeHandler(OnCultureChanged);

            ApplyUIResources(Host.Instance.Culture);
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
            return false;
        }

        protected bool OnCancel(String confirmMessage)
        {
              bool cancel = MessageBox.Show(
                confirmMessage, "RouteNet", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1) == DialogResult.No;


              if (cancel)
                  Activate();

              return cancel;
          }

        #endregion

        #region Destroy

        protected void Destroy()
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

        #region IView Members

        public virtual void BindData(Object data)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        IApplication m_app;
        public IApplication App
        {
            get
            {
                return m_app;
            }
            set
            {
                m_app = value;
            }
        }

        public virtual void InitModel()
        {

        }

        #endregion

        #region CleanUp

        protected virtual void CleanUp()
        {

        }

        #endregion
    }
}