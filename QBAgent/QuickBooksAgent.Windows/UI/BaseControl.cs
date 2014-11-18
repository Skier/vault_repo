using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Collections;

namespace QuickBooksAgent.Windows.UI
{
    public class BaseControl : UserControl
    {
        List<MenuItem> m_menuItems = new List<MenuItem>();
        Joystick m_joystick = new Joystick();

        public Joystick Joystick
        {
            get { return m_joystick; }
        }     

        public List<MenuItem> MenuItems
        {
            get { return m_menuItems; }
        }


        public BaseControl()
        {        
            
        }

        internal void Init()
        {
            OnInit();
        }

        protected virtual void OnInit()
        {
//            List<IJoystickElement> joystickElements = new List<IJoystickElement>();
//                
//            foreach (Control control in Controls)
//            {
//                if (control is IJoystickElement)
//                    joystickElements.Add((IJoystickElement)control);
//            }
//
//            m_joystick = new Joystick(joystickElements);
        }

        protected virtual void ApplyUIResources(CultureInfo cultureInfo)
        { }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            ApplyUIResources(Host.Instance.Culture);
        }

        public void Destroy()
        {
            MainFormController.Unregister();
        }
    }
}
