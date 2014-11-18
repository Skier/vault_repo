package truetract.domain
{
import mx.collections.ArrayCollection;
import mx.collections.Sort;
import mx.collections.SortField;

import truetract.domain.mementos.ProjectTabContactMemento;
    
[Bindable]
[RemoteClass(alias="TractInc.TrueTract.Entity.ProjectTabContactInfo")]
public class ProjectTabContact implements IMemento
{
	public static const CONTACT_TYPE_SURFACE_AND_MINERAL_OWNER:String = "SurfaceAndMineralOwner";
	public static const CONTACT_TYPE_SURFACE_OWNER:String = "SurfaceOwner";
	public static const CONTACT_TYPE_MINERAL_OWNER:String = "MineralOwner";
	public static const CONTACT_TYPE_SURFACE_LEASEE:String = "SurfaceLeasee";
	public static const CONTACT_TYPE_OTHER:String = "Other";

    public var ProjectTabContactId:int;
    public var ProjectTabId:int;

    public var ContactType:String;

    public var ContactName:String;
    public var FirstName:String;
    public var MiddleName:String;
    public var LastName:String;
    public var EntityRelationship:String;

    public var PhoneNumber:String = "";
    public var Email:String = "";
    public var IsActive:Boolean = false;
    public var IsEntity:Boolean = false;

    public var PhysicalAddress:Address = new Address();
    public var MailingAddress:Address = new Address();

    public var ProjectTabRef:ProjectTab;

	public function get Type():String 
	{
		if (DictionaryRegistry.getInstance().getProjectTabContactType(ContactType).length() == 0) 
		{
			return ContactType;
		} else 
		{
			return DictionaryRegistry.getInstance().getProjectTabContactType(ContactType).@Name;
		}
	}

	public function get Name():String 
	{
		var result:String;
		
		if (IsEntity) 
		{
			result = ContactName + " (" + EntityRelationship + ")";
		}
		else 
		{
			result = FirstName + " " + LastName;
		}
		
		return result;
	}

    public function getMemento():Object
    {
        var memento:ProjectTabContactMemento = new ProjectTabContactMemento();

        memento.projectTabContactId = ProjectTabContactId;
        memento.projectTabId = ProjectTabId;
        memento.contactType = ContactType;
        memento.contactName = ContactName;
        memento.firstName = FirstName;
        memento.middleName = MiddleName;
        memento.lastName = LastName;
        memento.entityRelationship = EntityRelationship;
        memento.phoneNumber = PhoneNumber;
        memento.email = Email;
        memento.isActive = IsActive;
        memento.isEntity = IsEntity;

        return memento;
    }

    public function setMemento(value:Object):void
    {
        var memento:ProjectTabContactMemento = ProjectTabContactMemento(value);

        ProjectTabContactId = memento.projectTabContactId;
        ProjectTabId = memento.projectTabId;
        ContactType = memento.contactType;
        ContactName = memento.contactName;
        FirstName = memento.firstName;
        MiddleName = memento.middleName;
        LastName = memento.lastName;
        EntityRelationship = memento.entityRelationship;
        PhoneNumber = memento.phoneNumber;
        Email = memento.email;
        IsActive = memento.isActive;
        IsEntity = memento.isEntity;
    }    
}
}