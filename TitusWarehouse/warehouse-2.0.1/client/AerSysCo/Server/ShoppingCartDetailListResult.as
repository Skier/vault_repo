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

  public class ShoppingCartDetailListResult
  {
    private var resultField:AerSysCo.Server.RequestResult;
    private var detailsField:AerSysCo.Server.ShoppingCartDetailCollection;
    private var versionField:int = 0;

    public function ShoppingCartDetailListResult()
    {
      this.resultField = new AerSysCo.Server.RequestResult();
      this.detailsField = new AerSysCo.Server.ShoppingCartDetailCollection();

    }

    public function get result():AerSysCo.Server.RequestResult
    {
        return this.resultField;
    }
    public function set result(value:AerSysCo.Server.RequestResult):void
    {
       this.resultField = value;
    }
    public function get details():AerSysCo.Server.ShoppingCartDetailCollection
    {
        return this.detailsField;
    }
    public function set details(value:AerSysCo.Server.ShoppingCartDetailCollection):void
    {
       this.detailsField = value;
    }
    public function get version():int
    {
        return this.versionField;
    }
    public function set version(value:int):void
    {
       this.versionField = value;
    }

  }
}