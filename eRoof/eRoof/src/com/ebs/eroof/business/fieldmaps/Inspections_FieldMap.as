package com.ebs.eroof.business.fieldmaps
{
	import com.quickbase.idn.business.fieldmaps.QuickBaseFieldMap;
	import com.quickbase.idn.business.fieldmaps.IQuickBaseFieldMap;

	public class Inspections_FieldMap extends QuickBaseFieldMap implements IQuickBaseFieldMap
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		public function Inspections_FieldMap()
		{
			super();
		}

		protected override function loadFids():void
		{
			columnFids.Client = 12;
			columnFids.Facility = 13;
			columnFids.Roof = 14;
			columnFids.InspectionDate = 6;
			columnFids.InspectionType = 7;
			columnFids.InspectorCompany = 8;
			columnFids.InspectorName = 9;
			columnFids.rid = 3;
			columnFids.RelatedSection = 11;
			columnFids.AddPhoto = 27;
			columnFids.Photos = 26;
			columnFids.DateSort = 18;
			columnFids.Weather = 19;
			columnFids.CoreTaken = 20;
			columnFids.FurtherInvestigation = 21;
			columnFids.FurtherInvestigationNotes = 22;
			columnFids.OverallRating = 23;
			columnFids.Assessment = 10;
			columnFids.PhotoSetID = 24;
			columnFids.AllowedClientUser = 29;
			columnFids.AllowedFacilityUser = 30;
			columnFids.AllowedSectionUser = 31;
			columnFids.AllowedUser = 32;
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
