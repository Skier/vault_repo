using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Dalworth.Controls;
using Dalworth.Windows.ServiceVisit.Resources;

namespace Dalworth.Windows.ServiceVisit.ServiceVisit
{
    public partial class ServiceVisitView : BaseControl
    {
        internal MenuItem m_menuAddTask;
        internal MenuItem m_menuAddTaskRugPickup;
        internal MenuItem m_menuViewTask;
        internal MenuItem m_menuDeleteTask;
        internal MenuItem m_menuSeparator1;
        internal MenuItem m_menuSubmitETC;
        internal MenuItem m_menuNoGo;
        internal MenuItem m_menuSeparator2;
        internal MenuItem m_menuComplete;
        internal MenuItem m_menuCompleteByDispatch;

        public ServiceVisitView()
        {
            InitializeComponent();

            m_menuAddTask = new MenuItem();
            m_menuAddTaskRugPickup = new MenuItem();
            m_menuViewTask = new MenuItem();
            m_menuDeleteTask = new MenuItem();
            m_menuSeparator1 = new MenuItem();
            m_menuSubmitETC = new MenuItem();
            m_menuNoGo = new MenuItem();
            m_menuSeparator2 = new MenuItem();
            m_menuComplete = new MenuItem();
            m_menuCompleteByDispatch = new MenuItem();


            m_menuAddTask.Text = "Add Task";
            m_menuAddTaskRugPickup.Text = "Rug Pickup";
            m_menuViewTask.Text = "View Task";
            m_menuDeleteTask.Text = "Delete Task";
            m_menuSeparator1.Text = "-";
            m_menuSubmitETC.Text = "Submit ETC";
            m_menuNoGo.Text = "No Go";
            m_menuSeparator2.Text = "-";
            m_menuComplete.Text = "Complete";
            m_menuCompleteByDispatch.Text = "Complete by Dispatch";

            MenuItemsRight.Add(m_menuAddTask);
            m_menuAddTask.MenuItems.Add(m_menuAddTaskRugPickup);
            MenuItemsRight.Add(m_menuViewTask);
            MenuItemsRight.Add(m_menuDeleteTask);
            MenuItemsRight.Add(m_menuSeparator1);
            MenuItemsRight.Add(m_menuSubmitETC);
            MenuItemsRight.Add(m_menuNoGo);
            MenuItemsRight.Add(m_menuSeparator2);
            MenuItemsRight.Add(m_menuComplete);
            MenuItemsRight.Add(m_menuCompleteByDispatch);
        }


        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Service Visit";
        }


        protected override void OnInit()
        {
            Joystick.Add(m_txtAddress, m_tabs, m_linkPhone1, m_tabs, m_linkPhone1);
            Joystick.Add(m_linkPhone1, m_txtAddress, m_linkPhone2, m_txtAddress, m_txtNotes);
            Joystick.Add(m_linkPhone2, m_linkPhone1, m_txtNotes, m_txtAddress, m_txtNotes);
            Joystick.Add(m_txtNotes, m_linkPhone2, m_tabs, m_linkPhone1, m_tabs);

            Joystick.Add(m_txtMessage, m_tabs, m_tabs, m_tabs, m_tabs);
            Joystick.Add(m_tblTasks, m_tabs, m_tabs, m_tabs, m_tabs);

            Joystick.Add(m_txtUserNotes, m_tabs, m_tabs, m_tabs, m_tabs);

            Joystick.Add(m_tabs, 0, m_txtNotes, m_txtAddress);
            Joystick.Add(m_tabs, 1, m_txtMessage, m_txtMessage);
            Joystick.Add(m_tabs, 2, m_tblTasks, m_tblTasks);
            Joystick.Add(m_tabs, 3, m_txtUserNotes, m_txtUserNotes);            
        }
    }      
}
