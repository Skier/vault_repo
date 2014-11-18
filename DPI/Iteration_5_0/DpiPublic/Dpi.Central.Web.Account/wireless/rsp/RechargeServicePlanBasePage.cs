using Dpi.Central.Web.Controls;

namespace Dpi.Central.Web.Account.Wireless.Processes.Rsp
{
    public enum RspProcess 
    {
        RechargeSamePlan,
        RechargeDifferentPlan
    }

    public class RechargeServicePlanBasePage : BaseAccountPage
    {
        #region Constants

        public const string SK_RSP_PROCESS = "RspProcess";

        private const string SK_MODEL = "RspModel";

        #endregion

        #region Override Methods

        protected override void InitLayout()
        {
            base.InitLayout();

            ProcessMap processMap = new ProcessMap();

            processMap.CssClass = "process_map";
            processMap.CssClassPrevious = "previous_step";
            processMap.CssClassCurrent = "current_step";
            processMap.CssClassNext = "next_step";
            processMap.Provider = ProcessMapProvider;

            Form.Controls.AddAt(2, processMap);
        }

        protected override void AddErrorControlToPage() 
        {
            Form.Controls.AddAt(3, ErrorControl);
        }

        #endregion

        #region Properties

        internal RechargeServicePlanModel Model
        {
            get
            {
                if (Session[SK_MODEL] == null) {
                    Session[SK_MODEL] = new RechargeServicePlanModel(Map, GetAccountNumber());
                }

                return (RechargeServicePlanModel) Session[SK_MODEL];
            }
        }

        private RechargeServicePlanProcessMapProvider ProcessMapProvider
        {
            get
            {
                if (Application["RspProcessMapProvider"] == null) {
                    Application["RspProcessMapProvider"] = new RechargeServicePlanProcessMapProvider();
                }

                return (RechargeServicePlanProcessMapProvider) Application["RspProcessMapProvider"];
            }
        }

        #endregion
    }
}