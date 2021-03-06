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
  import flexTense.core.*;
  import AerSysCo.Server.*
  import mx.rpc.soap.WebService;
  import mx.rpc.AbstractOperation;
  import flash.events.EventDispatcher;
  import mx.rpc.events.ResultEvent;
  import mx.rpc.events.FaultEvent;
  public class GetOrderPDFMethod extends EventDispatcher
  {
    private var ws:WebService;
    private var operation:AbstractOperation;

    public function GetOrderPDFMethod(webService:WebService)
    {
        this.ws = webService;
        this.operation = ws.getOperation(WebServiceMethodName.GetOrderPDF);
    }
    private function faultHandler(fault:FaultEvent):void
    {
       var e:BaseFaultEvent = new BaseFaultEvent(fault.fault);
       this.dispatchEvent(e);

    }

    private function resultHandler(result:ResultEvent):void
    {
       var e:GetOrderPDFResultEvent = new GetOrderPDFResultEvent(result.result);
       this.dispatchEvent(e);

    }

    public function addResponder(listener:Function):void
    {
       this.addEventListener(ResultEvent.RESULT,listener);
    }

    public function addFaulter(listener:Function):void
    {
       this.addEventListener(FaultEvent.FAULT,listener);
    }

    public function send(context:Context,orderId:int):void
    {
       this.operation.addEventListener(ResultEvent.RESULT,resultHandler)
       this.operation.addEventListener(FaultEvent.FAULT,faultHandler); 
       var sendArray:Array = getSendArray(context,orderId);
       this.operation.send.apply(null,sendArray);
    }

    private function getSendArray(...args:Array):Array
    {
       return args;
    }


  }

}
