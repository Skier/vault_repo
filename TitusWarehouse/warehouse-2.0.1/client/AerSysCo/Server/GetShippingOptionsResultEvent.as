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
  import flash.events.Event;
  import mx.rpc.events.ResultEvent;
  import flash.utils.*;
  import mx.collections.ArrayCollection;
  import flexTense.core.*;

  public class GetShippingOptionsResultEvent extends Event
  {
   private var _result:ShipmentShippingOptionsResult;

   public function GetShippingOptionsResultEvent(result:Object)
   {
     if(null != result) 
     {
         super(ResultEvent.RESULT,result);
         var obj:Object = ResultConverter.convertObject(result,new ShipmentShippingOptionsResult());
         this._result = ShipmentShippingOptionsResult(obj);

     }
   }
    public function get result():ShipmentShippingOptionsResult
    {
        return this._result;
    }

  }

}