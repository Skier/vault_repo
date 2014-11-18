package WDM
{
	import mx.rpc.AsyncToken;
	import mx.rpc.Responder;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.events.FaultEvent;
	
	public class DatabaseAsyncToken extends AsyncToken
	{
		private var m_activeRecord:ActiveRecord;
		
		public function DatabaseAsyncToken(asyncToken:AsyncToken,responder:Responder = null,activeRecord:ActiveRecord = null)
		{
			super(asyncToken.message);
			
			m_activeRecord = activeRecord;
			
			asyncToken.addResponder(new Responder(OnResult,OnFault));
			
			if(m_activeRecord != null)
				m_activeRecord.lock();
				
			if(responder != null)
				addResponder(responder);
		}
		
		private function OnResult(resultEvent:ResultEvent):void
		{
			if(m_activeRecord != null)
				m_activeRecord.unlock();
			
			if((resultEvent.result as ActiveRecord) != null)
			{
				trace("active record received");
				trace(resultEvent.result);
				
				var activeRecord:ActiveRecord = resultEvent.result as ActiveRecord;

				if(m_activeRecord != null)
				{
					trace("Old active record exists");
					trace(m_activeRecord);
									
					m_activeRecord.applyFields( activeRecord );
					
					m_activeRecord.IsDirty = false;
					
					for each(var responder:Responder in responders)
						responder.result(m_activeRecord);
				}
				else
				{
					
					trace("Just new active record exists");
					
					activeRecord.IsDirty = false;
					
					for each(var responder4:Responder in responders)
						responder4.result(activeRecord);					
				}
					
			}
			else if(resultEvent.result is Array)
			{
				for each(var item:Object in resultEvent.result as Array)
				{
					if(item is ActiveRecord)
						ActiveRecord(item).IsDirty = false;
				}
				
				for each(var responder2:Responder in responders)
					responder2.result(resultEvent.result as Array);
			}
			else
			{
				trace("Unknown result");
				trace(resultEvent.result);
						
				for each(var responder3:Responder in responders)
					responder3.result(resultEvent);	
			}
		}
		
	
		private function OnFault(faultEvent:FaultEvent):void
		{
			if(m_activeRecord != null)
				m_activeRecord.unlock();
			
			for each(var responder:Responder in responders)
				responder.fault(faultEvent);
		}
	}
}