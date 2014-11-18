package com.ebs.eroof.business.fieldmaps
{
	import com.quickbase.idn.business.fieldmaps.QuickBaseFieldMap;
	import com.quickbase.idn.business.fieldmaps.IQuickBaseFieldMap;

	public class Tables_FieldMap extends QuickBaseFieldMap implements IQuickBaseFieldMap
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		public function Tables_FieldMap()
		{
			super();
		}

		protected override function loadFids():void
		{
			columnFids.Seq = 6;
			columnFids.Name = 7;
			columnFids.Fields = 8;
			columnFids.AddField = 9;
			columnFids.SeqAndName = 10;
			columnFids.RecordPicker = 13;
			columnFids.IsHide = 14;
			columnFids.DBIDName = 15;
			columnFids.DBID = 11;
			columnFids.Reports = 16;
			columnFids.AddReport = 17;
			columnFids.IsConsultant = 18;
			columnFids.IsClient = 19;
			columnFids.IsContractor = 20;
			columnFids.IsInspector = 21;
			columnFids.IRPSource = 22;
			columnFids.IsParent = 23;
			columnFids.IRPConvert = 25;
			columnFids.KeyField = 26;
			columnFids.IsChild = 27;
			columnFids.ParentTableName = 28;
			columnFids.ParentKeyField = 29;
			columnFids.OldParentKeyField = 30;
			columnFids.IRPConvert2 = 31;
			columnFids.ClientList = 32;
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
