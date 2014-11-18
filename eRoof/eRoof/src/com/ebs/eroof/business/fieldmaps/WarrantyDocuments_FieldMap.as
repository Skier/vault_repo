package com.ebs.eroof.business.fieldmaps
{
	import com.quickbase.idn.business.fieldmaps.QuickBaseFieldMap;
	import com.quickbase.idn.business.fieldmaps.IQuickBaseFieldMap;

	public class WarrantyDocuments_FieldMap extends QuickBaseFieldMap implements IQuickBaseFieldMap
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		public function WarrantyDocuments_FieldMap()
		{
			super();
		}

		protected override function loadFids():void
		{
			columnFids.rid = 3;
			columnFids.RelatedWarranty = 12;
			columnFids.Client = 13;
			columnFids.Facility = 14;
			columnFids.Roof = 15;
			columnFids.Title = 6;
			columnFids.Description = 7;
			columnFids.Date = 8;
			columnFids.Document = 19;
			columnFids.AllowedClientUser = 20;
			columnFids.AllowedFacilityUser = 21;
			columnFids.AllowedSectionUser = 22;
			columnFids.AllowedUser = 23;
			columnFids.DateCreated = 1;
			columnFids.DateModified = 2;
			columnFids.RecordOwner = 4;
			columnFids.LastModifiedBy = 5;

		}

		protected override function loadRequiredColumns():void
		{
			requiredColumns.addItem("rid");
		}
	}
}
