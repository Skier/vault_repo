using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using MobileTech.Domain;

using MobileTech.ServiceLayer;

namespace MobileTech.Windows.UI.EndDay
{
	public partial class EndDayView : BaseForm
    {
        #region Constructor

        public EndDayView()
		{
			InitializeComponent();
		}

        #endregion

        #region ApplyUIResources

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            Resources.Culture = cultureInfo;

            Text = Resources.Title;

            m_mbEndDay.Text = Resources.EndDayDone;
        }

        #endregion

        #region Event Handlers

        #region OnLoad

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            int m_routeOption = RouteOption.GetValue(RouteOptionEnum.Odometer, false);
            if (m_routeOption == 1 || m_routeOption == 2)
            {
                App.Execute(CommandName.Odometer, m_model, false);
            }

            if ((m_routeOption == 1 || m_routeOption == 2) && m_model.OdometerReading == 0)
            {
                Destroy();
            }

        }

        #endregion

        #region OnClosing

        private void OnClosing(object sender, CancelEventArgs e)
		{
			App.Execute(CommandName.MainMenu);
        }

        #endregion

        #region OnEndDayClick

        private void OnEndDayClick(object sender, EventArgs e)
        {
            try
            {
                using (WaitCursor cursor = new WaitCursor())
                {
                    m_model.Save();
                }

                Destroy();
            }
            catch (MobileTechException ex)
            {
                EventService.AddEvent(ex);
            }
            catch (Exception ex)
            {
                EventService.AddEvent(new MobileTechException(ex));
            }
        }

        #endregion

        #region OnExitClick

        private void OnExitClick(object sender, EventArgs e)
        {
            Destroy();
        }

        #endregion

        #endregion

        #region IView Members

        EndDayModel m_model;

        public override void BindData(Object data)
        {

            m_model = (EndDayModel)data;
        }

        #endregion
    }
}