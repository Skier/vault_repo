package App.Entity
{
	
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.AssetAssignmentDataObject")]
    public class AssetAssignmentDataObject
    {

        public var AssetAssignmentId:int;

        public var AFE:String;

        public var SubAFE:String;

        public var AssetId:int;

        public var Deleted:Boolean;

        public var Rates:Array;
        
        public var IsClientActive:Boolean;

        public var ratesHash:Array;
        
        public var afe:AFEDataObject;
        
        public var project:ProjectDataObject;
        
        public var AFEStatus:String;

        public var ProjectStatus:String;
        
        public var ClientId:int;
        
        public var ClientName:String;
        
    	public function isEditable():Boolean {
        	return IsClientActive
				&& (AFEDataObject.AFE_STATUS_ISSUED == afe.AFEStatus
					|| AFEDataObject.AFE_STATUS_UNLOCKED == afe.AFEStatus)
				&& (ProjectDataObject.SUBAFE_STATUS_ISSUED == project.SubAFEStatus
					|| ProjectDataObject.SUBAFE_STATUS_UNLOCKED == project.SubAFEStatus)
				&& !Deleted;
        }
        	
    }

}
