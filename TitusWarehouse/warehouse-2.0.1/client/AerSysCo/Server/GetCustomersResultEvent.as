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

  public class GetCustomersResultEvent extends Event
  {
   private var _result:CustomerListResult;

   public function GetCustomersResultEvent(result:Object)
   {
     if(null != result) 
     {
         super(ResultEvent.RESULT,result);
         var obj:Object = ResultConverter.convertObject(result,new CustomerListResult());
         this._result = CustomerListResult(obj);

     }
   }
    public function get result():CustomerListResult
    {
        return this._result;
    }

  }

}