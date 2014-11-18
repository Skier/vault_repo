using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using dalworth.domain;
using dalworth.preview;
using QuickBooksAgent.Windows.UI;

namespace hobson.controls
{
    public class BaseForm : Form
    {
        private Model m_model;
        public Model Model
        {
            get { return m_model; }
            set { m_model = value; }
        }
        
        private bool m_isMoreMenuAvailable = true;
        public bool IsMoreMenuAvailable
        {
            get { return m_isMoreMenuAvailable; }
            set { m_isMoreMenuAvailable = value; }
        }

        Joystick m_joystick = new Joystick();

        public Joystick Joystick
        {
            get { return m_joystick; }
        }     
        
        public void Init(BaseForm form)
        {
            m_model = form.Model;
        }

        protected virtual void OnFormLoad(EventArgs e) {}
        protected virtual void OnJoystickInit() {}

        protected override sealed void OnLoad(EventArgs e)
        {
            OnJoystickInit();
            OnFormLoad(e);            
            
            if (m_isMoreMenuAvailable)
            {
                foreach (MenuItem menuItem in Menu.MenuItems)
                {
                    if (menuItem.Text == "Menu")
                    {
                        if (menuItem.MenuItems.Count > 0)
                        {
                            MenuItem separator = new MenuItem();
                            separator.Text = "-";
                            menuItem.MenuItems.Add(separator);                            
                        }
                        
                        MenuItem more = new MenuItem();
                        more.Text = "More";
                        menuItem.MenuItems.Add(more);
                        more.Click += new EventHandler(OnMoreClick);
                        break;
                    }
                }                
            }            
        }

        protected void ShowForm(BaseForm form)
        {
            form.Init(this);
            form.ShowDialog();
            Activate();
        }
        
        public delegate void BeforeMoreClickHandler();
        public event BeforeMoreClickHandler BeforeMoreClick;

        public delegate void AfterMoreClosedHandler();
        public event AfterMoreClosedHandler AfterMoreClosed;
        
        private void OnMoreClick(object sender, EventArgs e)
        {
            if (BeforeMoreClick != null)
                BeforeMoreClick.Invoke();
            
            More more = new More();
            ShowForm(more);

            if (AfterMoreClosed != null)
                AfterMoreClosed.Invoke();            
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BaseForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Name = "BaseForm";
            this.ResumeLayout(false);

        }
    }
}
