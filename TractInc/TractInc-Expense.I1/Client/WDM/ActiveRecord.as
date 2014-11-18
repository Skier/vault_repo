package WDM
{
	import mx.rpc.remoting.RemoteObject;
	import mx.rpc.AsyncToken;
	import mx.rpc.events.ResultEvent;
	import flash.utils.Proxy;
	import flash.utils.flash_proxy;
	import mx.controls.Alert;
	import mx.events.FlexEvent;
	import mx.rpc.AbstractOperation;
	import mx.utils.ObjectProxy;
	import mx.utils.ObjectUtil;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.binding.utils.ChangeWatcher;
	import flash.events.Event;

	public class ActiveRecord extends Object
	{
		public var IsReadOnly:Boolean;
		
		private var m_lock:Boolean;
		
		[Bindable]
		public var IsDirty:Boolean;
		
		public function ActiveRecord()
		{
	
		}
	
		public function get IsLocked():Boolean
		{
			return m_lock;
		}	
		
		internal function lock():void
		{
			m_lock = true;
		}
		
		internal function unlock():void
		{
			m_lock = false;
		}
		
		protected virtual function get dataMapper():DataMapper
		{
			throw new Error("Not Implemented");
		}
		
		public function save(responder:Responder = null):AsyncToken
		{
			return dataMapper.save(this,responder);
		}
		
		public function remove(responder:Responder = null):AsyncToken
		{
			return dataMapper.remove(this,responder);
		}

		public virtual function applyFields(object:Object):void {}
		
		protected function LinstenChanges():void
		{
			var objClassInfo:Object = ObjectUtil.getClassInfo(this);

			for each (var prop:QName in objClassInfo.properties)
			{
				if(prop.localName != "IsDirty")
				{
					ChangeWatcher.watch(this,prop.localName,OnChange);
				}
			}
		}
		
		protected virtual function OnChange(event:PropertyChangeEvent):void
		{
			if(event.kind == "update")
				IsDirty = true;
				
			trace(event.newValue);
		}

	}
}