package Domain
{
	import mx.collections.ArrayCollection;

	[Bindable]
	public class UsStates
	{
		private static var items:ArrayCollection = new ArrayCollection();
		
		public static function GetItems():ArrayCollection{
			items.addItem(new String(" "));
			items.addItem(new String("Alabama"));
			items.addItem(new String("Alaska"));
			items.addItem(new String("Arizona"));
			items.addItem(new String("Arkansas"));
			items.addItem(new String("California"));
			items.addItem(new String("Colorado"));
			items.addItem(new String("Connecticut"));
			items.addItem(new String("Delaware"));
			items.addItem(new String("Florida"));
			items.addItem(new String("Georgia"));
			items.addItem(new String("Hawaii"));
			items.addItem(new String("Idaho"));
			items.addItem(new String("Illinois"));
			items.addItem(new String("Indiana"));
			items.addItem(new String("Iowa"));
			items.addItem(new String("Kansas"));
			items.addItem(new String("Kentucky"));
			items.addItem(new String("Louisiana"));
			items.addItem(new String("Maine"));
			items.addItem(new String("Maryland"));
			items.addItem(new String("Massachusetts"));
			items.addItem(new String("Michigan"));
			items.addItem(new String("Minnesota"));
			items.addItem(new String("Mississippi"));
			items.addItem(new String("Missouri"));
			items.addItem(new String("Montana"));
			items.addItem(new String("Nebraska"));
			items.addItem(new String("Nevada"));
			items.addItem(new String("New Hampshire"));
			items.addItem(new String("New Jersey"));
			items.addItem(new String("New Mexico"));
			items.addItem(new String("New York"));
			items.addItem(new String("North Carolina"));
			items.addItem(new String("North Dakota"));
			items.addItem(new String("Ohio"));
			items.addItem(new String("Oklahoma"));
			items.addItem(new String("Oregon"));
			items.addItem(new String("Pennsylvania"));
			items.addItem(new String("Rhode Island"));
			items.addItem(new String("South Carolina"));
			items.addItem(new String("South Dakota"));
			items.addItem(new String("Tennessee"));
			items.addItem(new String("Texas"));
			items.addItem(new String("Utah"));
			items.addItem(new String("Vermont"));
			items.addItem(new String("Virginia"));
			items.addItem(new String("Washington"));
			items.addItem(new String("West Virginia"));
			items.addItem(new String("Wisconsin"));
			items.addItem(new String("Wyoming"));
	
			return items;
		}
	}
}