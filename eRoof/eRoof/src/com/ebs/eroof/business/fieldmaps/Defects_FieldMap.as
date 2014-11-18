package com.ebs.eroof.business.fieldmaps
{
	import com.quickbase.idn.business.fieldmaps.QuickBaseFieldMap;
	import com.quickbase.idn.business.fieldmaps.IQuickBaseFieldMap;

	public class Defects_FieldMap extends QuickBaseFieldMap implements IQuickBaseFieldMap
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		public function Defects_FieldMap()
		{
			super();
		}

		protected override function loadFids():void
		{
			columnFids.Client = 12;
			columnFids.Facility = 13;
			columnFids.Roof = 14;
			columnFids.Type = 7;
			columnFids.Description = 9;
			columnFids.Severity = 8;
			columnFids.rid = 3;
			columnFids.RelatedSection = 11;
			columnFids.AddPhoto = 22;
			columnFids.Photos = 21;
			columnFids.Photo = 6;
			columnFids.PhotoName = 19;
			columnFids.AllowedClientUser = 24;
			columnFids.AllowedFacilityUser = 25;
			columnFids.AllowedSectionUser = 26;
			columnFids.AllowedUser = 27;
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
