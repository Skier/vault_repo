namespace TractInc.ScopeScetch.Entity
{
    public class TractCall
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

        public TractCall() {
        }

        public TractCall(int tractId, string callType, int callOrder, string callDBValue, bool createdByMouse) {
            TractId = tractId;
            CallType = callType;
            CallOrder = callOrder;
            CallDBValue = callDBValue;
            CreatedByMouse = createdByMouse;
        }

        #endregion

        #region Methods

        public TractCall copy()
        {
            TractCall callCopy = new TractCall();
            
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
