package com.ebs.eroof.business.fieldmaps
{
	import com.quickbase.idn.business.fieldmaps.QuickBaseFieldMap;
	import com.quickbase.idn.business.fieldmaps.IQuickBaseFieldMap;

	public class Reports_FieldMap extends QuickBaseFieldMap implements IQuickBaseFieldMap
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		public function Reports_FieldMap()
		{
			super();
		}

		protected override function loadFids():void
		{
			columnFids.SeqAndName = 8;
			columnFids.RelatedTable = 6;
			columnFids.ReportName = 15;
			columnFids.Filter = 9;
			columnFids.Sort = 10;
			columnFids.IsSummary = 11;
			columnFids.IsSuppEdit = 12;
			columnFids.IsSuppView = 13;
			columnFids.IsGridEdit = 14;
			columnFids.Table = 7;
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
