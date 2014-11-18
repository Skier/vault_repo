package com.dalworth.servman.service.registry
{
	import com.dalworth.servman.domain.IDomainEntity;
	
	import mx.collections.ArrayCollection;
	
	public class BaseRegistry
	{
		public static const KEY_FIELD_NAME:String = "Id";
		
		private var _collection:Array;
		private var externalCollection:ArrayCollection;
		
		public function BaseRegistry()
		{
			_collection = new Array();
			externalCollection = new ArrayCollection();
		}
		
		public function get keyField():String
		{
			return KEY_FIELD_NAME;
		}
		
		public function getLocal(id:Object):Object
		{
			return _collection[id];
		}
		
		public function storeLocal(value:Object):void
		{
			var keyValue:Object = value[KEY_FIELD_NAME];
			var existingObject:Object = _collection[keyValue];
			if (existingObject != null)
			{
				updateLocal(existingObject as IDomainEntity, value);
			} else 
			{
				_collection[keyValue] = value;
			}
			
			updateExternalCollection();
		}
		
		public function removeLocal(value:Object):void
		{
			var keyValue:Object = value[KEY_FIELD_NAME];
			var existingObject:Object = _collection[keyValue];
			if (existingObject != null)
			{
				_collection[keyValue] = null;
			}
			
			updateExternalCollection();
		}
		
		public function getAll():ArrayCollection
		{
			return externalCollection;
		}
		
		protected function updateLocal(targetObj:IDomainEntity, sourceObj:Object):void 
		{
			targetObj.applyFields(sourceObj);
		}
		
		private function updateExternalCollection():void 
		{
			var source:Array = new Array();
			for each(var obj:Object in _collection)
			{
				if (obj != null)
					source.push(obj);
			}
			externalCollection.source = source;
			
		}
		
		public function forceUpdate(source:Array):void 
		{
			_collection = new Array();
			
			if (source != null) 
			{
				for each(var obj:Object in source)
				{
					var keyValue:Object = obj[KEY_FIELD_NAME];
					_collection[keyValue] = obj;
				}
			}
			
			updateExternalCollection();
		}
	}
}