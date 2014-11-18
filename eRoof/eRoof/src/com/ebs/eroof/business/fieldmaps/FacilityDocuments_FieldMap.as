package com.ebs.eroof.business.fieldmaps
{
	import com.quickbase.idn.business.fieldmaps.QuickBaseFieldMap;
	import com.quickbase.idn.business.fieldmaps.IQuickBaseFieldMap;

	public class FacilityDocuments_FieldMap extends QuickBaseFieldMap implements IQuickBaseFieldMap
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		public function FacilityDocuments_FieldMap()
		{
			super();
		}

		protected override function loadFids():void
		{
			columnFids.rid = 3;
			columnFids.RelatedFacility = 12;
			columnFids.Client = 14;
			columnFids.Facility = 15;
			columnFids.Document = 18;
			columnFids.Title = 6;
			columnFids.Description = 7;
			columnFids.Date = 8;
			columnFids.AllowedClientUser = 19;
			columnFids.AllowedFacilityUser = 20;
			columnFids.AllowedUser = 21;
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
