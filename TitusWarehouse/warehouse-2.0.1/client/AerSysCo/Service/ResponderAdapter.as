package AerSysCo.Service
{
    import flash.events.Event;
    import mx.rpc.Fault;
    import mx.rpc.Responder;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import flexTense.core.BaseFaultEvent;
    import flash.events.EventDispatcher;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    

    
    public class ResponderAdapter
    {
        private var responder:Responder = null;
        private var method:String = null;

        public function ResponderAdapter(r:Responder, m:String):void {
            responder = r;
            method = m;
        }

        public function onResult(e:Event):void {
            var re:ResultEvent = new ResultEvent(ResultEvent.RESULT, false, true, Object(e).result);
            responder.result(re);
            (e.target as EventDispatcher).removeEventListener(ResultEvent.RESULT, this.onResult);
        }

        public function onFailure(ev:Event):void {
        	var e:BaseFaultEvent = BaseFaultEvent(ev);
            var f:Fault = new Fault(e.fault.faultCode, method+":"+e.fault.faultString, e.fault.faultDetail);
            var fe:FaultEvent = new FaultEvent(FaultEvent.FAULT, false, true, f);
            responder.fault(fe);
            (e.target as EventDispatcher).removeEventListener(FaultEvent.FAULT, this.onFailure);
        }

    }
}