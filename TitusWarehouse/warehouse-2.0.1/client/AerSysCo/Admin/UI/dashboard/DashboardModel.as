package AerSysCo.Admin.UI.dashboard
{
	import mx.collections.ArrayCollection;
	import mx.collections.ListCollectionView;
	import AerSysCo.UI.Models.CustomerUI;
	import AerSysCo.UI.Models.ASCUserUI;
	import AerSysCo.UI.Models.BrandUI;
	
	[Bindable]
	public class DashboardModel
	{
		public var currentUser:ASCUserUI;
		public var customers:ArrayCollection;
		public var customersFiltered:ListCollectionView;
		
		public var brandList:ArrayCollection;

		public var searchString:String;
		public var currentBrand:BrandUI;

		public var loading:Boolean = false;
		
		public function DashboardModel() 
		{
			customers = new ArrayCollection();
			customersFiltered = new ListCollectionView(customers);
			brandList = new ArrayCollection();
		}
		
	}
}