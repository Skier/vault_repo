using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MobileTech.Domain;
using MobileTech.ServiceLayer;
using MobileTech.Data;

namespace MobileTech.Windows.UI.TComm.Emulator
{
    public partial class TCommEmulatorView : BaseForm
    {
        public TCommEmulatorView()
        {
            InitializeComponent();
        }

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            TCommEmulatorResources.Culture = cultureInfo;

            m_btBegin.Text = TCommEmulatorResources.Start;
            m_btDone.Text = CommonResources.BtnDone;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            App.Execute(CommandName.MainMenu);
        }

        private void OnDoneClick(object sender, EventArgs e)
        {
            try
            {
                using (WaitCursor waitCursor = new WaitCursor())
                {
                    Route.ChangeStatus(RouteStatusEnum.IDL_TCOM_DONE);
                }

                Destroy();
            }
            catch (Exception ex)
            {
                EventService.AddEvent(
                   new MobileTechException(TCommEmulatorResources.EnableToChangeRouteStatus,
                   ex));
            }
        }

        private void OnBeginClick(object sender, EventArgs e)
        {
            Import import = new Import(Host.GetPath("Database\\Import"));
            import.Clear = true;

            import.Messages += new TaskMessageEvent(OnTaskMessages);
            import.Progress += new TaskProgressEvent(OnTaskProgress);

            try
            {
                using (WaitCursor cursor = new WaitCursor())
                {
#if WINCE
                    if (!Database.IsDatabaseExist())
                        Database.CreateDatabase();
#else
                    if(!Database.IsDatabaseExist() || 
                    !Database.IsSchemaExist())
                        Database.CreateDatabase();
#endif

                }

                import.Execute();

                Route.Reset();
                Session.Reset();
                RouteInventory.Reset();


                Item.UpdateIndex();

            }
            catch (Exception ex)
            {
                EventService.AddEvent(
                    new MobileTechException(TCommEmulatorResources.ErrorWithImportingData,
                    ex));
            }
        }

        void OnTaskProgress(int percentComplete)
        {
            m_progress.Value = percentComplete;
            
        }

        void OnTaskMessages(string message)
        {
            m_lbProgressMessage.Text = message;
            m_lbProgressMessage.Refresh();
        }
    }
}