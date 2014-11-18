namespace TractInc.TrueTract.Entity
{
    public class TractCallInfo
    {
        #region Constants

        public int TractCallId;
        public int TractId;
        public string CallType;
        public int CallOrder;
        public string CallDBValue;
        public bool CreatedByMouse;

        #endregion

        #region Constructors

        public TractCallInfo() {
        }

        public TractCallInfo(int tractId, string callType, int callOrder, string callDBValue, bool createdByMouse) {
            TractId = tractId;
            CallType = callType;
            CallOrder = callOrder;
            CallDBValue = callDBValue;
            CreatedByMouse = createdByMouse;
        }

        #endregion

        #region Methods

        public TractCallInfo copy()
        {
            TractCallInfo callCopy = new TractCallInfo();
            
            callCopy.TractId = TractId;
            callCopy.CallType = CallType;
            callCopy.CallOrder = CallOrder;
            callCopy.CallDBValue = CallDBValue;
            callCopy.CreatedByMouse = CreatedByMouse;

            return callCopy;
        }

        #endregion
    }
}
