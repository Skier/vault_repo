package com.ebs.eroof.business.fieldmaps
{
	import com.quickbase.idn.business.fieldmaps.QuickBaseFieldMap;
	import com.quickbase.idn.business.fieldmaps.IQuickBaseFieldMap;

	public class Consultant_FieldMap extends QuickBaseFieldMap implements IQuickBaseFieldMap
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		public function Consultant_FieldMap()
		{
			super();
		}

		protected override function loadFids():void
		{
			columnFids.CompanyName = 6;
			columnFids.Contact = 7;
			columnFids.Address = 8;
			columnFids.ReportBanner = 9;
			columnFids.MapLatLong = 10;
			columnFids.MapZoom = 11;
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
