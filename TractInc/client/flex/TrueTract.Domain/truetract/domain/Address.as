package truetract.domain
{
import mx.collections.ArrayCollection;
import mx.collections.Sort;
import mx.collections.SortField;

import truetract.domain.mementos.AddressMemento;
    
[Bindable]
[RemoteClass(alias="TractInc.TrueTract.Entity.AddressInfo")]
public class Address implements IMemento
{
    public var AddressId:int;
    public var Address1:String = "";
    public var Address2:String = "";
    public var City:String = "";
    public var State:Number;
    public var Zip:String = "";

    public function getRegion():String
    {
        return City + " " + StateName + ", " + Zip;
    }

    public function set StateName(value:String):void {}
    public function get StateName():String
    {
        return DictionaryRegistry.getInstance().getState(State).@Name;
    }

    public function getMemento():Object
    {
        var memento:AddressMemento = new AddressMemento();

        memento.addressId = AddressId;
        memento.address1 = Address1;
        memento.address2 = Address2;
        memento.city = City;
        memento.state = State;
        memento.zip = Zip;

        return memento;
    }

    public function setMemento(value:Object):void
    {
        var memento:AddressMemento = AddressMemento(value);

        AddressId = memento.addressId;
        Address1 = memento.address1;
        Address2 = memento.address2;
        City = memento.city;
        State = memento.state;
        Zip = memento.zip;
    }    
}
}