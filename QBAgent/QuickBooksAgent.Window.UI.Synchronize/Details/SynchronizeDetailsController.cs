using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.QBSDK;
using System.Diagnostics;
using QuickBooksAgent.Windows.UI.Synchronize.Basic;
using QuickBooksAgent.Domain;
using QuickBooksAgent.Windows.UI.DatabaseManager.Explorer;

namespace QuickBooksAgent.Windows.UI.Synchronize.Details
{
    public class SynchronizeDetailsController:SingleFormController<SynchronizeDetailsModel,SynchronizeDetailsView>
    {
        protected override void OnModelInitialize(object[] data)
        {
            Debug.Assert(data != null
            && data.Length == 1, "One parameter required");

            Debug.Assert(data[0] is List<Synchronizer> , "First parameter must be List<Synchronizer>");

            Model.Init(data[0] as List<Synchronizer>);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            View.m_table.BindModel(Model);

            InitDefaultAction("Info", true);
        }

        public override void OnViewLoad()
        {
            base.OnViewLoad();

            View.m_table.Focus();
            View.m_table.Select(0);
        }

        public override void OnDefaultAction()
        {
            base.OnDefaultAction();

            if (Model.GetRowCount() > 0 && View.m_table.CurrentRowIndex >= 0)
            {
                SynchronizeDetailsModel.Detail detail = Model.GetObject(View.m_table.CurrentRowIndex, 0);

                if (detail.Status.IsError)
                {   
                    string objectName = string.Empty;
                    if (detail.Status.DomainObjects.Count > 0)
                        objectName = detail.Status.DomainObjects[0].ToString();
                    
                    if (detail.Status.IsVersionConflictError)
                    {                        
                        if (objectName == string.Empty)
                        {
                            MessageDialog.Show(MessageDialogType.Information,
                                string.Format("Object was changed by another user. Undo your changes and sync again to get latest version of this object. Then you can make your changes.\n\n{0}", detail.Status.Message));                                                        
                        } else
                        {
                            MessageDialog.Show(MessageDialogType.Information,
                                string.Format("Object \"{0}\" was changed by another user. Undo your changes and sync again to get latest version of this object. Then you can make your changes.\n\n{1}", objectName, detail.Status.Message));                            
                        }
                    } else
                    {
                        if (objectName == string.Empty)
                        {
                            MessageDialog.Show(MessageDialogType.Information, detail.Status.Message);                            
                        } else
                        {
                            MessageDialog.Show(MessageDialogType.Information,
                                string.Format("Error while processing \"{0}\".\n{1}", objectName, detail.Status.Message));                            
                        }                        
                    }
                }
                else if (detail.Status.CommandType == QBCommandTypeEnum.Add)
                    MessageDialog.Show(MessageDialogType.Information,
                        String.Format("Successfully added object: {0}",
                        detail.Status.DomainObjects[0].ToString()));
                else if (detail.Status.CommandType == QBCommandTypeEnum.Update)
                    MessageDialog.Show(MessageDialogType.Information,
                        String.Format("Successfully updated object: {0}",
                        detail.Status.DomainObjects[0].ToString()));
                else if (detail.Status.CommandType == QBCommandTypeEnum.Query)
                {
                    if (detail.Status.DomainObjects.Count > 0)
                    {

                        ExplorerController explorerController =
                            SingleFormController.Prepare<ExplorerController>(detail.Status.DomainObjects);
                        explorerController.Closed += new SingleFormClosedHandler(OnExplorerClosed);
                        explorerController.Execute();

                    }
                    else
                        MessageDialog.Show(MessageDialogType.Information,
                            "Server didn't return any new or updated item");
                }                
            }

        }

        void OnExplorerClosed(SingleFormController controller)
        {
            View.m_table.Focus();
        }
    }
}
