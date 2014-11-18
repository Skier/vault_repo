package UI
{
    import Domain.Account;
    import Domain.MailBoxStatus;
    import mx.collections.ArrayCollection;
    import mx.formatters.DateFormatter;

    [Bindable]
    public class AppModel
    {
        public var account:Account;
		public var workflowState:int;
        public var inbox:ArrayCollection;
        public var contactList:ArrayCollection;
        
        public var mailBoxStatus:MailBoxStatus;

        public var history:ArrayCollection;
        
		public static const WORKFLOWSTATE_LOGIN_VIEWING:int = 0;
		public static const WORKFLOWSTATE_INBOX_VIEWING:int = 1;
		public static const WORKFLOWSTATE_COMPOSING:int = 2;

        private var m_df:DateFormatter;
        
        public function AppModel():void
        {
            m_df = new DateFormatter();
            m_df.formatString = "YYYY.MM.DD, HH:NN:SS ";
        }
        
        public function Reset():void 
        {
            account = null;
            mailBoxStatus = null;
            inbox = new ArrayCollection();
            contactList = new ArrayCollection();
            history = new ArrayCollection();
        }
        
        public function AddHistoryEvent(s:String):void
        {
            history.addItem(m_df.format(new Date()) + ": " + s);
        }
    }
}