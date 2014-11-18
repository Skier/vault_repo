package
{

    import mx.collections.ArrayCollection;
    import App.Entity.CrewChiefDataObject;
    
    [Bindable]
    public class InvoiceModel
    {

        public static const WORK_ROLE_CREWCHIEF:int = 0;
        public static const WORK_ROLE_MANAGER:int = 1;

        public var workRole:int = 0;
        
        public var caption:String;
        
        public var data:CrewChiefDataObject;
        
    }

}
