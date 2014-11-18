package com.ebs.eroof.business.fieldmaps
{
	import com.quickbase.idn.business.fieldmaps.QuickBaseFieldMap;
	import com.quickbase.idn.business.fieldmaps.IQuickBaseFieldMap;

	public class Layers_FieldMap extends QuickBaseFieldMap implements IQuickBaseFieldMap
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		public function Layers_FieldMap()
		{
			super();
		}

		protected override function loadFids():void
		{
			columnFids.Client = 11;
			columnFids.Facility = 12;
			columnFids.Roof = 13;
			columnFids.LayerNumber = 6;
			columnFids.LayerType = 7;
			columnFids.Description = 8;
			columnFids.Attachment = 9;
			columnFids.rid = 3;
			columnFids.RelatedSection = 10;
			columnFids.AllowedSectionUser = 18;
			columnFids.AllowedClientUser = 19;
			columnFids.AllowedFacilityUser = 20;
			columnFids.AllowedUser = 21;
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
