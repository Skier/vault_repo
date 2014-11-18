package App.Entity
{
	
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.AssetTypeDataObject")]
	public class AssetTypeDataObject
	{
		
		public static const ASSET_TYPE_LANDMAN:String = 'LANDMAN';

		public var Type:String;
		
	}
	
}
