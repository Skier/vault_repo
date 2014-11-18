using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using dalworth.controls;
using dalworth.domain;
using hobson.controls;
using MobileTech;
using MobileTech.Windows.UI;
using Timer=System.Threading.Timer;

namespace dalworth.preview
{
    public partial class MainMenu : BaseForm
    {
        public MainMenu()
        {
            InitializeComponent();
            Model = new Model();
            UpdateButtons();  
        }

        protected override void OnFormLoad(EventArgs e)
        {   
            BeforeMoreClick += new BeforeMoreClickHandler(OnBeforeMoreClick);
            AfterMoreClosed += new AfterMoreClosedHandler(OnAfterMoreClosed);
            
            m_btnStartDay.Picture = ImageKeys.StartDay;
            m_btnPage.Picture = ImageKeys.JobHistory;
            m_btnTools.Picture = ImageKeys.Tools;
            
            Model.Init();
            UpdateButtons();      
            
            //TODO: temp, remove me
            //m_timerIncome.Enabled = true;
        }

        private void OnBeforeMoreClick()
        {
            DeactiveteTimer();
        }
                
        private void OnAfterMoreClosed()
        {
            ActivateTimer();
        }

        private void ActivateTimer()
        {
            if (Model.AppPoint == ApplicationPoint.StartDayDone)
                m_timerIncome.Enabled = true;
        }
        
        private void DeactiveteTimer()
        {
            m_timerIncome.Enabled = false;
        }

        protected override void OnJoystickInit()
        {
            Joystick.Add(m_btnStartDay, m_btnTools, m_btnPage, m_btnTools, m_btnTools);
            Joystick.Add(m_btnPage, m_btnStartDay, m_btnTools, m_btnTools, m_btnTools);
            Joystick.Add(m_btnTools, m_btnPage, m_btnStartDay, m_btnStartDay, m_btnStartDay);
        }

        private void UpdateButtons()
        {
            if (Model.AppPoint == ApplicationPoint.StartDay)
            {
                m_btnStartDay.Enabled = true;
                m_btnPage.Enabled = false;
                m_btnStartDay.Picture = ImageKeys.StartDay;
                m_btnStartDay.Text = "Start Day";
            } else if (Model.AppPoint == ApplicationPoint.StartDayDone)
            {
                m_btnStartDay.Enabled = true;
                m_btnPage.Enabled = true;
                m_btnStartDay.Picture = ImageKeys.EndDay;
                m_btnStartDay.Text = "End Day";
            }
        }        

        private void OnStartDayClick(object sender, EventArgs e)
        {
            if (Model.AppPoint == ApplicationPoint.StartDay)
            {
                m_timerIncome.Enabled = false;
                
                Messages messages = new Messages();                
                ShowForm(messages);
                UpdateButtons();
                if (Model.AppPoint == ApplicationPoint.StartDayDone)
                {
                    ActivateTimer();
                    m_btnStartDay.Focus();
                }                
            } else if (Model.AppPoint == ApplicationPoint.StartDayDone)
            {
                DeactiveteTimer();

                EndDayDone endDayDone = new EndDayDone();
                ShowForm(endDayDone);
                Model.AppPoint = ApplicationPoint.StartDay;
                foreach (Ticket ticket in Model.Tickets)
                {
                    ticket.Status = TicketStatus.Started;
                }
                UpdateButtons();
                m_btnStartDay.Focus();                
            }
        }

        private void OnTimerIncomeTick(object sender, EventArgs e)
        {
            m_timerIncome.Enabled = false;
            Model.ResetCurrentTicket();

            Model.CurrentTicket = null;
            foreach (Ticket ticket in Model.Tickets)
            {
                if (ticket.Status == TicketStatus.Started)
                {
                    Model.CurrentTicket = ticket;
                    break;
                }                    
            }
            
            if (Model.CurrentTicket == null)
            {
                m_timerIncome.Enabled = false;
                return;
            }
            
            TicketIncome ticketIncome = new TicketIncome();
            ShowForm(ticketIncome);

            m_timerIncome.Enabled = true;
        }

        private void OnPageClick(object sender, EventArgs e)
        {
            DeactiveteTimer();
            
            JobHistory jobHistory = new JobHistory();
            ShowForm(jobHistory);

            ActivateTimer();
        }
        
        private void OnToolsClick(object sender, EventArgs e)
        {
            DeactiveteTimer();
            
            Tools tools = new Tools();
            ShowForm(tools);
            
            ActivateTimer();
        }
    }
}