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

  public class ShipmentShippingOptions
  {
    private var shoppingCartShipmentIdField:int = 0;
    private var liftGatePriceField:Number = 0;
    private var optionsField:AerSysCo.Server.ShippingOptionCollection;

    public function ShipmentShippingOptions()
    {
      this.optionsField = new AerSysCo.Server.ShippingOptionCollection();

    }

    public function get shoppingCartShipmentId():int
    {
        return this.shoppingCartShipmentIdField;
    }
    public function set shoppingCartShipmentId(value:int):void
    {
       this.shoppingCartShipmentIdField = value;
    }
    public function get liftGatePrice():Number
    {
        return this.liftGatePriceField;
    }
    public function set liftGatePrice(value:Number):void
    {
       this.liftGatePriceField = value;
    }
    public function get options():AerSysCo.Server.ShippingOptionCollection
    {
        return this.optionsField;
    }
    public function set options(value:AerSysCo.Server.ShippingOptionCollection):void
    {
       this.optionsField = value;
    }

  }
}