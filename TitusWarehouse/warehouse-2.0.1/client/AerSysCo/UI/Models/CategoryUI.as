package AerSysCo.UI.Models
{
	import mx.collections.ArrayCollection;
	import AerSysCo.Server.Category;
	import AerSysCo.Server.CatalogPackage;
	import AerSysCo.Server.Model;
	
	[Bindable]
	public class CategoryUI
	{
	    public var categoryId:int;
	    public var brandId:int;
	    public var parentCategoryId:int;
	    public var name:String;
	    
	    public var children:ArrayCollection = new ArrayCollection();

		public function populateFromCategory(value:Category, cascade:Boolean = false):void 
		{
			this.categoryId = value.CategoryId;
			this.brandId = value.BrandId;
			this.parentCategoryId = value.ParentCategoryId;
			this.name = value.Name;
		}
		
		public function toCategory():Category 
		{
			var result:Category = new Category();
			
			result.CategoryId = this.categoryId;
			result.BrandId = this.brandId;
			result.ParentCategoryId = this.parentCategoryId;
			result.Name = this.name;
	    	result.dateCreated = new Date();
	    	result.lastUpdateDate = new Date();

			return result;
		}
		
		public static function createFromCatalogPackage(cp:CatalogPackage):CategoryUI 
		{
			var root:CategoryUI = new CategoryUI();
			root.name = "Categories";
			root.categoryId = 0;
			root.parentCategoryId = 0;
			populateCategoryUI(root, cp);
			
			return root;
		}
		
		private static function populateCategoryUI(cUI:CategoryUI, cp:CatalogPackage):void 
		{
			for each (var c:Category in cp.categoryList.toArray()) 
			{
				if (c.ParentCategoryId == cUI.categoryId) 
				{
					var child:CategoryUI = new CategoryUI();
					child.populateFromCategory(c);
					cUI.children.addItem(child);
					populateCategoryUI(child, cp);
				}
			}

			populateModelUI(cUI, cp);
		}
		
		private static function populateModelUI(cUI:CategoryUI, cp:CatalogPackage):void 
		{
			for each (var m:Model in cp.modelList.toArray()) 
			{
				if (m.categoryId == cUI.categoryId) 
				{
					var child:ModelUI = new ModelUI();
					child.populateFromModel(m);
					cUI.children.addItem(child);
				}
			}
		}

	}
}