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

  public class ShipmentShippingOptionsResult
  {
    private var resultField:AerSysCo.Server.RequestResult;
    private var optionsField:AerSysCo.Server.ShipmentShippingOptionsCollection;

    public function ShipmentShippingOptionsResult()
    {
      this.resultField = new AerSysCo.Server.RequestResult();
      this.optionsField = new AerSysCo.Server.ShipmentShippingOptionsCollection();

    }

    public function get result():AerSysCo.Server.RequestResult
    {
        return this.resultField;
    }
    public function set result(value:AerSysCo.Server.RequestResult):void
    {
       this.resultField = value;
    }
    public function get options():AerSysCo.Server.ShipmentShippingOptionsCollection
    {
        return this.optionsField;
    }
    public function set options(value:AerSysCo.Server.ShipmentShippingOptionsCollection):void
    {
       this.optionsField = value;
    }

  }
}