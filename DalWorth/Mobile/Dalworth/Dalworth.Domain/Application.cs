using System;
  
namespace Dalworth.Domain
{
    public partial class Application
    {
        public const decimal TAX_PERCENT = 0.05M;

        public Application(){ }

        #region ApplicationState

        public ApplicationStateEnum ApplicationState
        {
            get { return (ApplicationStateEnum)m_applicationStateId; }
            set { m_applicationStateId = (int)value; }
        }

        #endregion        
    }
}
      