package com.ebs.eroof.business.fieldmaps
{
	import com.quickbase.idn.business.fieldmaps.QuickBaseFieldMap;
	import com.quickbase.idn.business.fieldmaps.IQuickBaseFieldMap;

	public class Fields_FieldMap extends QuickBaseFieldMap implements IQuickBaseFieldMap
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		public function Fields_FieldMap()
		{
			super();
		}

		protected override function loadFids():void
		{
			columnFids.rid = 3;
			columnFids.RelatedTable = 6;
			columnFids.Table = 7;
			columnFids.SeqAndName = 39;
			columnFids.DBID = 45;
			columnFids.Seq = 40;
			columnFids.Seq2 = 54;
			columnFids.Label = 38;
			columnFids.Type = 8;
			columnFids.FieldID = 46;
			columnFids.Set = 44;
			columnFids.IsReqd = 9;
			columnFids.IsWrap = 10;
			columnFids.Col = 11;
			columnFids.Default = 12;
			columnFids.IsUnique = 13;
			columnFids.T = 14;
			columnFids.Lines = 15;
			columnFids.Len = 16;
			columnFids.IsApp = 17;
			columnFids.TxtWidth = 18;
			columnFids.Entry = 19;
			columnFids.IsMult = 20;
			columnFids.IsNew = 21;
			columnFids.IsSort = 22;
			columnFids.IsHTML = 23;
			columnFids.N = 24;
			columnFids.Units = 25;
			columnFids.NumWidth = 26;
			columnFids.TreatAs = 27;
			columnFids.Dec = 28;
			columnFids.IsTotals = 29;
			columnFids.IsAvg = 30;
			columnFids.D = 31;
			columnFids.IsAlpha = 32;
			columnFids.IsSmartDate = 33;
			columnFids.DateType = 34;
			columnFids.Text = 35;
			columnFids.IsJPG = 36;
			columnFids.Revs = 37;
			columnFids.test = 47;
			columnFids.IsUpdateable = 48;
			columnFids.IsText = 49;
			columnFids.IsNumeric = 50;
			columnFids.IsDate = 51;
			columnFids.Eqn = 55;
			columnFids.IsConsultant = 62;
			columnFids.IsClient = 63;
			columnFids.IsContractor = 64;
			columnFids.IsInspector = 65;
			columnFids.IRPSource = 66;
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
