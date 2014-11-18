package com.titus.catalog.model.cart
{
	import com.titus.catalog.model.Submittal;
	
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class Cart
	{
		private static var instance:Cart;

		public var items:ArrayCollection;
		
		public function Cart()
		{
			if (instance)
				throw new Error("DB Storage is singleton!");

			items = new ArrayCollection();
		}

		public static function getInstance():Cart
		{
			if (!instance)
				instance = new Cart();
	
			return instance;
		}
		
		public function get length():int 
		{
			return items.length;
		}
	
		public function addItem(item:Object):void 
		{
			if (item is Submittal) 
				Submittal(item).isInCart = true;
			
			if (!itemExists(item)) 
				items.addItem(item);
		}
	
		public function removeItem(item:Object):void 
		{
			if (itemExists(item)) 
				items.removeItemAt(items.getItemIndex(item));

			if (item is Submittal) 
				Submittal(item).isInCart = false;
		}
		
		public function clear():void 
		{
			for each (var o:Object in items) 
			{
				if (o is Submittal) 
					Submittal(o).isInCart = false;
			}
			
			items.removeAll();
		}
	
		public function itemExists(item:Object):Boolean 
		{
			for each (var o:Object in items) 
			{
				if (o is Submittal && item is Submittal) 
				{
					if (Submittal(o).FileId == Submittal(item).FileId)
					return true;
				}
			}
			
			return false;
		}
	}
}