using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Domain;
using QuickBooksAgent.Windows.UI.Password;
using QuickBooksAgent.Windows.UI.Synchronize.Details;

namespace QuickBooksAgent.Windows.UI.Synchronize.Basic
{
    public class SynchronizeBasicController:SingleFormController<SynchronizeBasicModel,SynchronizeBasicView>
    {
        public override void OnViewLoad()
        {
            View.m_table.SetColumnWidth(0, 50);
            View.m_table.BindModel(Model);

            Model.Change += new QuickBooksAgent.Windows.UI.Controls.TableModelChangeHandler(OnModelChanged); // += new QuickBooksAgent.QBSDK.QBConnectionStateChangedHandler(OnConnectionStateChanged);
            Model.TaskChanged += new SynchronizeBasicTaskChangedHandler(OnTaskChanged);
            DefaultActionName = "Start";
            IsDefaultActionExist = true;
        }


        void OnTaskChanged(int index, int percentComplete)
        {
            View.m_table.Select(index);
            View.m_progress.Value = percentComplete;
        }

        void OnModelChanged()
        {
            View.m_table.Refresh();
        }

        #region Done

        bool m_done;
        public bool Done
        {
            get { return m_done; }
        }
        
        #endregion



        public override void OnDefaultAction()
        {
            base.OnDefaultAction();

            if (m_done)
            {
                SynchronizeDetailsController synchronizeDetailsController =
                        SingleFormController.Prepare<SynchronizeDetailsController>(Model.Synchronizers);

                synchronizeDetailsController.Execute();

                return;
            }

            if (!SessionModel.Instance.SkipPasswordRequest)
            {
                PasswordController passwordController =
                    SingleFormController.Prepare<PasswordController>();

                passwordController.Complete += new PasswordControllerCompleteHandler(OnPasswordClose);

                passwordController.Execute();
            }
            else
                Start();
        }

        private void Start()
        {
            try
            {
                IsDefaultActionExist = false;

                Model.Connection.ConnectionTicket.Ticket =
                    Crypto.Decrypt(
                        SessionModel.Instance.LoginInfo.Password,
                        Configuration.QuickBooks.ConnectionTicket);


                View.m_progress.Value = 0;

                if (Model.Start() == SyncResultEnum.LicenseFailed)                
                    View.Destroy();

                bool isDetailsExist = false;
                foreach (Synchronizer synchronizer in Model.Synchronizers)
                {
                    if (synchronizer.QBReader.ResponseStatus.Count > 0)
                    {
                        isDetailsExist = true;
                        break;
                    }                                            
                }
                
                if (isDetailsExist)
                {
                    m_done = true;
                    DefaultActionName = "Details";
                    if (Model.IsErrorsExist())
                        OnDefaultAction();                    
                } else
                {
                    m_done = false;
                    DefaultActionName = "Start";
                }
                
                
                SessionModel.Instance.SkipPasswordRequest = true;

            }
            catch (System.Net.WebException ex)
            {
                EventService.AddEvent(
                   new QuickBooksAgentException((ex.InnerException != null) ?
                       ex.InnerException.Message : ex.Message, ex));
            }
            catch (QuickBooksAgentException ex)
            {
                EventService.AddEvent(ex);
            }
            catch (Exception ex)
            {
                EventService.AddEvent(new QuickBooksAgentException("Unable to synchronize data",
                    ex));
            }
            finally
            {
                View.m_progress.Value = 100;
                IsDefaultActionExist = true;
            }
        }

        void OnPasswordClose(bool isInfoUpdated)
        {
            if (isInfoUpdated)
                Start();
        }
    }
}
