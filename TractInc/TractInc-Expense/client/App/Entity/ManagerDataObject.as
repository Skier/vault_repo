package App.Entity
{
	
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.ManagerDataObject")]
	public class ManagerDataObject
	{
		
        public var Bills:Array;
        
        public var Assets:Array;

        public var Clients:Array;
        
        public var Assignments:Array;

	}
	
}
