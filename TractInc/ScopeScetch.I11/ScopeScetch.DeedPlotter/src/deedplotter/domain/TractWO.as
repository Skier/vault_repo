package src.deedplotter.domain
{
	import mx.collections.ArrayCollection;
	
	[RemoteClass(alias="TractInc.ScopeScetch.Entity.Tract")]
	public class TractWO
	{
		
        public var Uid:String;

        public var TractId:int;
        public var Easting:Number;
        public var Northing:Number;
        public var Description:String;
        public var CreatedBy:int;
        public var IsDeleted:Boolean;
        public var DocId:int;
        public var CalledAC:Number;
        public var UnitId:int;

        public var UnitName:String;

        public var Calls:Array = new Array();
        public var TextObjects:Array = new Array();
	
	    public var ParentDocument:DocumentWO;

		public function ToTract():Tract
		{
			var tract:Tract = new Tract();
			tract.Uid = Uid;
			tract.TractId = TractId;
			tract.Easting = Easting;
			tract.Northing = Northing;
			tract.Description = Description;
			tract.CreatedBy = CreatedBy;
			tract.IsDeleted = IsDeleted;
			tract.DocId = DocId;
			tract.CalledAC = CalledAC;
			tract.UnitId = UnitId;
			
			tract.Calls.source = Calls;
			tract.TextObjects.source = TextObjects;
			
			if (ParentDocument)
			{
			    tract.ParentDocument = ParentDocument.ToDocument();
			}

			return tract;
		}
		
	}
}