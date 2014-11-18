package Domain
{
	import mx.collections.ArrayCollection;
	
	public class County
	{
		private static var items:ArrayCollection = new ArrayCollection();
		
		public static function GetItems():ArrayCollection{
			items.addItem(new String(" "));
			items.addItem(new String("County1"));
			items.addItem(new String("County2"));
			items.addItem(new String("County3"));
			items.addItem(new String("County4"));
			items.addItem(new String("County5"));
			items.addItem(new String("County6"));
			items.addItem(new String("County7"));
			items.addItem(new String("County8"));
			items.addItem(new String("County9"));

			return items;
		}
	}
}