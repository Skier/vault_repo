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

  public class RemoveAllShoppingCartDetailsResultEvent extends Event
  {
   private var _result:VersionResult;

   public function RemoveAllShoppingCartDetailsResultEvent(result:Object)
   {
     if(null != result) 
     {
         super(ResultEvent.RESULT,result);
         var obj:Object = ResultConverter.convertObject(result,new VersionResult());
         this._result = VersionResult(obj);

     }
   }
    public function get result():VersionResult
    {
        return this._result;
    }

  }

}
