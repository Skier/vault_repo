package truetract.web.util
{
	import flash.events.Event;
	import flash.events.EventDispatcher;
	import flash.events.TimerEvent;
	import flash.net.URLLoader;
	import flash.net.URLRequest;
	import flash.net.navigateToURL;
	import flash.utils.ByteArray;
	import flash.utils.Timer;
	
	import mx.controls.Alert;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.mxml.RemoteObject;
	
	import truetract.plotter.domain.Tract;
	
	public class TractConverter extends EventDispatcher
	{
		private var service:RemoteObject;

		public function TractConverter()
		{
			service = new RemoteObject("GenericDestination");
			service.showBusyCursor = true;
			service.source = "TractInc.TrueTract.TractConverter";
			service.ConvertToPdf.addEventListener(ResultEvent.RESULT, service_onResultHandler);
			service.ConvertToPdf.addEventListener(FaultEvent.FAULT, service_onFaultHandler);
		}

        public function convertToPdf(tract:Tract, tractImage:ByteArray, scaleBarImage:ByteArray):void
        {
            service.ConvertToPdf(tract.ToTractWO(), tractImage, scaleBarImage);
        }
        
        private function service_onResultHandler(event:ResultEvent):void
        {
            var pdfUrl:String = event.result.toString();

            navigateToURL(new URLRequest(pdfUrl), "_blank");
        }	
        	
		private function service_onFaultHandler(event:FaultEvent):void
		{
		    Alert.show(event.fault.faultString);
	 	}
		
	}
}