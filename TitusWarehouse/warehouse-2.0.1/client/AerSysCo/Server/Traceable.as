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

  public class Traceable
  {
    private var createdByUserField:String="";
    private var lastUpdateDateField:Date;
    private var dateCreatedField:Date;

    public function Traceable()
    {

    }

    public function get createdByUser():String
    {
        return this.createdByUserField;
    }
    public function set createdByUser(value:String):void
    {
       this.createdByUserField = value;
    }
    public function get lastUpdateDate():Date
    {
        return this.lastUpdateDateField;
    }
    public function set lastUpdateDate(value:Date):void
    {
       this.lastUpdateDateField = value;
    }
    public function get dateCreated():Date
    {
        return this.dateCreatedField;
    }
    public function set dateCreated(value:Date):void
    {
       this.dateCreatedField = value;
    }

  }
}