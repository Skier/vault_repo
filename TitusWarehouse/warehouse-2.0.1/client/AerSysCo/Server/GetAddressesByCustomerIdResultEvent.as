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

  public class GetAddressesByCustomerIdResultEvent extends Event
  {
   private var _result:ShippingAddressListResult;

   public function GetAddressesByCustomerIdResultEvent(result:Object)
   {
     if(null != result) 
     {
         super(ResultEvent.RESULT,result);
         var obj:Object = ResultConverter.convertObject(result,new ShippingAddressListResult());
         this._result = ShippingAddressListResult(obj);

     }
   }
    public function get result():ShippingAddressListResult
    {
        return this._result;
    }

  }

}
