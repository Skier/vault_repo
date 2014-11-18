package com.ebs.eroof.business.fieldmaps
{
	import com.quickbase.idn.business.fieldmaps.QuickBaseFieldMap;
	import com.quickbase.idn.business.fieldmaps.IQuickBaseFieldMap;

	public class Histories_FieldMap extends QuickBaseFieldMap implements IQuickBaseFieldMap
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		public function Histories_FieldMap()
		{
			super();
		}

		protected override function loadFids():void
		{
			columnFids.DateCompleted = 6;
			columnFids.TypeOfWork = 7;
			columnFids.Company = 8;
			columnFids.Allocation = 9;
			columnFids.Status = 10;
			columnFids.ActualCost = 11;
			columnFids.Details = 12;
			columnFids.RelatedSection = 14;
			columnFids.Client = 15;
			columnFids.Facility = 16;
			columnFids.Roof = 17;
			columnFids.Photo = 18;
			columnFids.Report = 19;
			columnFids.Thumbnail = 20;
			columnFids.Annotation = 21;
			columnFids.AllowedClientUser = 22;
			columnFids.AllowedContractorUser = 23;
			columnFids.AllowedInspectorUser = 24;
			columnFids.DateCreated = 1;
			columnFids.DateModified = 2;
			columnFids.rid = 3;
			columnFids.RecordOwner = 4;
			columnFids.LastModifiedBy = 5;

		}

		protected override function loadRequiredColumns():void
		{
			requiredColumns.addItem("rid");
		}
	}
}
