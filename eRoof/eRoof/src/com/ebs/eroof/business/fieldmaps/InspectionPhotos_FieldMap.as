package com.ebs.eroof.business.fieldmaps
{
	import com.quickbase.idn.business.fieldmaps.QuickBaseFieldMap;
	import com.quickbase.idn.business.fieldmaps.IQuickBaseFieldMap;

	public class InspectionPhotos_FieldMap extends QuickBaseFieldMap implements IQuickBaseFieldMap
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		public function InspectionPhotos_FieldMap()
		{
			super();
		}

		protected override function loadFids():void
		{
			columnFids.rid = 3;
			columnFids.RelatedInspection = 10;
			columnFids.Client = 11;
			columnFids.Facility = 12;
			columnFids.Roof = 13;
			columnFids.Photo = 9;
			columnFids.Description = 8;
			columnFids.Date = 6;
			columnFids.AllowedClientUser = 19;
			columnFids.AllowedSectionUser = 20;
			columnFids.AllowedFacilityUser = 21;
			columnFids.PhotoThumbnail = 22;
			columnFids.AllowedUser = 23;
			columnFids.IRPPhotoSetID = 24;
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
