//*******************************************************************
//  <auto-generated>
//       FleXtense : www.flextense.net
//       Version:1.0 Beta Release
//
//
//       Changes to this file may cause incorrect behavior and will be lost if
//       the code is regenerated.
//  <auto-generated>
//*******************************************************************

 package AerSysCo.Server
{

  public class Address
  {
    private var nameField:String="";
    private var address1Field:String="";
    private var address2Field:String="";
    private var cityField:String="";
    private var stateField:String="";
    private var zipField:String="";
    private var countryField:String="";

    public function Address()
    {

    }

    public function get name():String
    {
        return this.nameField;
    }
    public function set name(value:String):void
    {
       this.nameField = value;
    }
    public function get address1():String
    {
        return this.address1Field;
    }
    public function set address1(value:String):void
    {
       this.address1Field = value;
    }
    public function get address2():String
    {
        return this.address2Field;
    }
    public function set address2(value:String):void
    {
       this.address2Field = value;
    }
    public function get city():String
    {
        return this.cityField;
    }
    public function set city(value:String):void
    {
       this.cityField = value;
    }
    public function get state():String
    {
        return this.stateField;
    }
    public function set state(value:String):void
    {
       this.stateField = value;
    }
    public function get zip():String
    {
        return this.zipField;
    }
    public function set zip(value:String):void
    {
       this.zipField = value;
    }
    public function get country():String
    {
        return this.countryField;
    }
    public function set country(value:String):void
    {
       this.countryField = value;
    }

  }
}