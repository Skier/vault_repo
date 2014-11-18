package com.ebs.eroof.business.fieldmaps
{
	import com.quickbase.idn.business.fieldmaps.QuickBaseFieldMap;
	import com.quickbase.idn.business.fieldmaps.IQuickBaseFieldMap;

	public class Details_FieldMap extends QuickBaseFieldMap implements IQuickBaseFieldMap
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		public function Details_FieldMap()
		{
			super();
		}

		protected override function loadFids():void
		{
			columnFids.Client = 13;
			columnFids.Facility = 14;
			columnFids.Roof = 15;
			columnFids.DetailType = 7;
			columnFids.Description = 8;
			columnFids.FlashingMembrane = 9;
			columnFids.FlashingMetal = 10;
			columnFids.rid = 3;
			columnFids.RelatedSection = 12;
			columnFids.AddPhoto = 25;
			columnFids.Photos = 24;
			columnFids.Photo = 6;
			columnFids.PhotoName = 20;
			columnFids.Condition = 19;
			columnFids.AllowedSectionUser = 29;
			columnFids.AllowedFacilityUser = 30;
			columnFids.AllowedClientUser = 31;
			columnFids.AllowedUser = 32;
			columnFids.DateCreated = 1;
			columnFids.DateModified = 2;
			columnFids.RecordOwner = 4;
			columnFids.LastModifiedBy = 5;

		}

		protected override function loadRequiredColumns():void
		{
			requiredColumns.addItem("rid");
			requiredColumns.addItem("DetailType");
		}
	}
}
