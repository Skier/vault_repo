using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Collections;
using Dalworth.Controls;

namespace Dalworth.Windows
{
    public class BaseControl : UserControl
    {
        List<MenuItem> m_menuItemsRight = new List<MenuItem>();
        List<MenuItem> m_menuItemsLeft = new List<MenuItem>();
        Joystick m_joystick = new Joystick();

        public Joystick Joystick
        {
            get { return m_joystick; }
        }     

        public List<MenuItem> MenuItemsRight
        {
            get { return m_menuItemsRight; }
        }

        public List<MenuItem> MenuItemsLeft
        {
            get { return m_menuItemsLeft; }
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
