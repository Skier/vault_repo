package com.ebs.eroof.business.fieldmaps
{
	import com.quickbase.idn.business.fieldmaps.QuickBaseFieldMap;
	import com.quickbase.idn.business.fieldmaps.IQuickBaseFieldMap;

	public class Warranties_FieldMap extends QuickBaseFieldMap implements IQuickBaseFieldMap
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		public function Warranties_FieldMap()
		{
			super();
		}

		protected override function loadFids():void
		{
			columnFids.rid = 3;
			columnFids.RelatedSection = 12;
			columnFids.Client = 13;
			columnFids.Facility = 14;
			columnFids.Roof = 15;
			columnFids.DateI = 6;
			columnFids.IssueDate = 7;
			columnFids.DateE = 11;
			columnFids.ExpiryDate = 21;
			columnFids.Type = 8;
			columnFids.IssuedBy = 10;
			columnFids.Notes = 23;
			columnFids.Duration = 22;
			columnFids.Documents = 25;
			columnFids.AddDocument = 26;
			columnFids.AllowedClientUser = 27;
			columnFids.AllowedFacilityUser = 28;
			columnFids.AllowedSectionUser = 29;
			columnFids.AllowedUser = 30;
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
