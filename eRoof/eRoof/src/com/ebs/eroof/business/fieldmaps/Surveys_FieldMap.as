package com.ebs.eroof.business.fieldmaps
{
	import com.quickbase.idn.business.fieldmaps.QuickBaseFieldMap;
	import com.quickbase.idn.business.fieldmaps.IQuickBaseFieldMap;

	public class Surveys_FieldMap extends QuickBaseFieldMap implements IQuickBaseFieldMap
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		public function Surveys_FieldMap()
		{
			super();
		}

		protected override function loadFids():void
		{
			columnFids.Client = 15;
			columnFids.Facility = 16;
			columnFids.Roof = 17;
			columnFids.Date = 6;
			columnFids.Type = 8;
			columnFids.MembraneCondition = 9;
			columnFids.InsulationCondition = 10;
			columnFids.rid = 3;
			columnFids.RelatedSection = 14;
			columnFids.AddPhoto = 22;
			columnFids.Photos = 21;
			columnFids.DateSort = 7;
			columnFids.InfraredPhotoID = 11;
			columnFids.MatchingPhotoID = 12;
			columnFids.Details = 13;
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
