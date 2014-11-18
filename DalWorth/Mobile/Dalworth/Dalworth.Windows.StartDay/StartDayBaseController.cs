using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Domain;
using Dalworth.SDK;
using Dalworth.Windows.StartDay.EnterEquipment;
using Dalworth.Windows.StartDay.LoadEquipment;
using Dalworth.Windows.StartDay.LoadItems;
using Dalworth.Windows.StartDay.Login;
using Dalworth.Windows.StartDay.Message;
using Dalworth.Windows.StartDay.VanCheck;
using Dalworth.Windows.StartDay.WorkSummary;

namespace Dalworth.Windows.StartDay
{
    public enum WizardPointEnum
    {
        Login,
        WorkSummary,
        Message,
        LoadEquipment,
        LoadItems,
        EnterEquipment,
        VanCheck
    }

    public class StartDayBaseController<TModel, TView> : SingleFormController<TModel, TView>
        where TView : BaseControl, new()
        where TModel : StartDayBaseModel, new()
    {
        private WizardPointEnum m_wizardPoint;

        public StartDayBaseController()
        {
            if (this is LoginController)
                m_wizardPoint = WizardPointEnum.Login;
            else if (this is WorkSummaryController)
                m_wizardPoint = WizardPointEnum.WorkSummary;
            else if (this is MessageController)
                m_wizardPoint = WizardPointEnum.Message;
            else if (this is LoadEquipmentController)
                m_wizardPoint = WizardPointEnum.LoadEquipment;
            else if (this is LoadItemsController)
                m_wizardPoint = WizardPointEnum.LoadItems;
            else if (this is EnterEquipmentController)
                m_wizardPoint = WizardPointEnum.EnterEquipment;
            else if (this is VanCheckController)
                m_wizardPoint = WizardPointEnum.VanCheck;
        }

        #region Next

        public void Next()
        {
            if (m_wizardPoint == WizardPointEnum.Login)
            {                
                WorkSummaryController workSummaryController
                    = Prepare<WorkSummaryController>(Model.StartDayModel);
                workSummaryController.Execute();
            }
            else if (m_wizardPoint == WizardPointEnum.WorkSummary)
            {
                if (Model.StartDayModel.Work.StartMessage == null
                    || Model.StartDayModel.Work.StartMessage == string.Empty)
                {
                    m_wizardPoint = WizardPointEnum.Message;
                    Next();
                } else
                {
                    MessageController messageController
                        = Prepare<MessageController>(Model.StartDayModel);
                    messageController.Execute();                    
                }
            }
            else if (m_wizardPoint == WizardPointEnum.Message)
            {
                if (WorkEquipment.FindBy(Model.StartDayModel.Work).Count > 0
                    || (Model.StartDayModel.Work.EquipmentNotes != null
                        && Model.StartDayModel.Work.EquipmentNotes != string.Empty))
                {
                    LoadEquipmentController loadEquipmentController =
                        Prepare<LoadEquipmentController>(Model.StartDayModel);
                    loadEquipmentController.Execute();
                }
                else
                {
                    m_wizardPoint = WizardPointEnum.LoadEquipment;
                    Next();
                }                
            }
            else if (m_wizardPoint == WizardPointEnum.LoadEquipment)
            {
                if (Visit.GetItemDeliveryInformation(Model.StartDayModel.Work).Count > 0)
                {
                    LoadItemsController loadItemsController
                        = Prepare<LoadItemsController>(Model.StartDayModel);
                    loadItemsController.Execute();                    
                } else
                {
                    m_wizardPoint = WizardPointEnum.LoadItems;
                    Next();
                }
            }
            else if (m_wizardPoint == WizardPointEnum.LoadItems)
            {
                if (WorkEquipment.FindBy(Model.StartDayModel.Work).Count > 0)
                {
                    EnterEquipmentController enterEquipmentController
                        = Prepare<EnterEquipmentController>(Model.StartDayModel);
                    enterEquipmentController.Execute();                                
                } else
                {                    
                    m_wizardPoint = WizardPointEnum.EnterEquipment;
                    Next();
                }
            }
            else if (m_wizardPoint == WizardPointEnum.EnterEquipment)
            {
                VanCheckController vanCheckController
                    = Prepare<VanCheckController>(Model.StartDayModel,
                        WorkEquipment.FindBy(Model.StartDayModel.Work).Count);
                vanCheckController.Execute();                                    
            }
        }

        #endregion
    }
}
