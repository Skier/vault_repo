package truetract.domain.mementos
{
	import truetract.domain.Address;
	
	public class ProjectTabContactMemento
	{
	    public var projectTabContactId:int;
	    public var projectTabId:int;

	    public var contactType:String;
	
	    public var contactName:String = "";
	    public var firstName:String = "";
	    public var middleName:String = "";
	    public var lastName:String = "";
	    public var entityRelationship:String = "";
	
	    public var phoneNumber:String = "";
	    public var email:String = "";
	
	    public var isActive:Boolean = false;
	    public var isEntity:Boolean = false;
	}
}