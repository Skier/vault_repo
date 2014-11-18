using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MobileTech.Domain;

using MobileTech.ServiceLayer;

namespace MobileTech.Windows.UI.StartDay
{
	public partial class StartDayView : BaseForm
    {

#if WINCE
        AutoDropDown m_autoDropDown = new AutoDropDown();
#endif

        #region Constructor

        public StartDayView()
		{
			InitializeComponent();
        }

        #endregion

        #region ApplyUIResources
        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            Resources.Culture = cultureInfo;

            Text = Resources.Title;

            m_lbLocation.Text = Resources.Location;
            m_lbRoute.Text = Resources.Route;
            m_lbSalesRep.Text = Resources.SalesRep;
            m_lbDate.Text = Resources.Date;

            m_mbAccept.Text = CommonResources.BtnDone;

        }
        #endregion

        #region Events

        #region OnLoad

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

#if WINCE
            m_autoDropDown.Add(m_cbEmployee);
#endif

            m_mbAccept.Focus();
        }

        #endregion

        #region OnClosing

        private void OnClosing(object sender, CancelEventArgs e)
		{
			App.Execute(CommandName.MainMenu);
        }

        #endregion

        #region OnAcceptClick

        private void OnAcceptClick(object sender, EventArgs e)
        {
            int m_routeOption = RouteOption.GetValue(RouteOptionEnum.Odometer, false);
            if (m_routeOption == 1 || m_routeOption == 2)
            {
                App.Execute(CommandName.Odometer, m_model, false);
            }

            if (m_routeOption == 0 
                || m_model.OdometerReading != 0)
            {
                try
                {
                    m_model.Save();

                    Destroy();
                }
                catch (Exception ex)
                {
                    EventService.AddEvent(
                        new MobileTechException(Resources.EnableToChangePeriod, 
                        ex));
                }
            }
        }

        #endregion

        #region OnBackClick

        private void OnBackClick(object sender, EventArgs e)
        {
			WinAPI.CloseWindow(this);
        }

        #endregion

        #endregion

        #region Model

        StartDayModel m_model;

        public override void BindData(Object data)
        {

            if (!(data is StartDayModel))
                throw new MobileTechInvalidModelExeption();

            m_model = (StartDayModel)data;

            m_dtpRouteDate.Value = m_model.Date;

            m_lbLocation2.Text = m_model.Location.ToString();
            m_lbRoute2.Text = m_model.Route.ToString();

            if (m_model.Employee != null)
            {
                foreach (Employee employee in m_model.EmployeeList)
                    m_cbEmployee.Items.Add(employee);

                m_cbEmployee.SelectedItem = m_model.Employee;
            }

        }

        #endregion
    }
}