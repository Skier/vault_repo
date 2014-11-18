package com.ebs.eroof.business.fieldmaps
{
	import com.quickbase.idn.business.fieldmaps.QuickBaseFieldMap;
	import com.quickbase.idn.business.fieldmaps.IQuickBaseFieldMap;

	public class Segments_FieldMap extends QuickBaseFieldMap implements IQuickBaseFieldMap
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		public function Segments_FieldMap()
		{
			super();
		}

		protected override function loadFids():void
		{
			columnFids.rid = 3;
			columnFids.SegmentName = 6;
			columnFids.ClientsCount = 9;
			columnFids.FacilitiesCount = 10;
			columnFids.SectionsCount = 11;
			columnFids.TotalSqFt = 12;
			columnFids.TotalValue = 13;
			columnFids.DateCreated = 1;
			columnFids.DateModified = 2;
			columnFids.RecordOwner = 4;
			columnFids.LastModifiedBy = 5;

		}

		protected override function loadRequiredColumns():void
		{
			requiredColumns.addItem("rid");
			requiredColumns.addItem("SegmentName");
		}
	}
}
