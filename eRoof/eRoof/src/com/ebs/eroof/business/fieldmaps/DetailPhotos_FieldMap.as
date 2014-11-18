package com.ebs.eroof.business.fieldmaps
{
	import com.quickbase.idn.business.fieldmaps.QuickBaseFieldMap;
	import com.quickbase.idn.business.fieldmaps.IQuickBaseFieldMap;

	public class DetailPhotos_FieldMap extends QuickBaseFieldMap implements IQuickBaseFieldMap
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		public function DetailPhotos_FieldMap()
		{
			super();
		}

		protected override function loadFids():void
		{
			columnFids.Photo = 10;
			columnFids.Description = 8;
			columnFids.Date = 6;
			columnFids.rid = 3;
			columnFids.RelatedDetail = 11;
			columnFids.Client = 12;
			columnFids.Facility = 13;
			columnFids.Roof = 14;
			columnFids.AllowedClientUser = 19;
			columnFids.AllowedFacilityUser = 20;
			columnFids.AllowedSectionUser = 21;
			columnFids.PhotoThumbnail = 22;
			columnFids.AllowedUser = 24;
			columnFids.IRPPhotoFileName = 25;
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
