package com.ebs.eroof.business.fieldmaps
{
	import com.quickbase.idn.business.fieldmaps.QuickBaseFieldMap;
	import com.quickbase.idn.business.fieldmaps.IQuickBaseFieldMap;

	public class Sections_FieldMap extends QuickBaseFieldMap implements IQuickBaseFieldMap
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		public function Sections_FieldMap()
		{
			super();
		}

		protected override function loadFids():void
		{
			columnFids.Client = 30;
			columnFids.Facility = 31;
			columnFids.Designation = 6;
			columnFids.RoofName = 7;
			columnFids.RoofSystem = 9;
			columnFids.Age = 10;
			columnFids.ConditionIndex = 19;
			columnFids.SqFt = 11;
			columnFids.EstCost = 13;
			columnFids.rid = 3;
			columnFids.RelatedFacility = 29;
			columnFids.AddLayer = 33;
			columnFids.Layers = 32;
			columnFids.AddCorePhoto = 68;
			columnFids.CorePhotos = 67;
			columnFids.AddDetail = 35;
			columnFids.Details = 34;
			columnFids.AddDefect = 37;
			columnFids.Defects = 36;
			columnFids.AddInspection = 45;
			columnFids.Inspections = 44;
			columnFids.AddSurvey = 70;
			columnFids.Surveys = 69;
			columnFids.AddExpenditure = 39;
			columnFids.Expenditures = 38;
			columnFids.AddWarranty = 43;
			columnFids.Warranties = 42;
			columnFids.Photo = 8;
			columnFids.PhotoName = 56;
			columnFids.YearInstalled = 14;
			columnFids.YearInstalledSource = 15;
			columnFids.EstCostPerSqFt = 12;
			columnFids.Height = 16;
			columnFids.Slope = 17;
			columnFids.InteriorSensitivity = 18;
			columnFids.SensitivityDetails = 58;
			columnFids.ConditionDetails = 59;
			columnFids.Restorable = 20;
			columnFids.Drainage = 21;
			columnFids.CurrentlyLeaking = 22;
			columnFids.HistoryOfLeaking = 23;
			columnFids.DrainageDetails = 24;
			columnFids.LeakDetails = 57;
			columnFids.X1 = 61;
			columnFids.Y1 = 62;
			columnFids.X2 = 63;
			columnFids.Y2 = 64;
			columnFids.OverallCoreCondition = 60;
			columnFids.Notes = 25;
			columnFids.SectionUsers = 74;
			columnFids.AddSectionUser = 75;
			columnFids.AllowedSectionUser = 77;
			columnFids.AllowedClientUser = 78;
			columnFids.AllowedFacilityUser = 79;
			columnFids.AllowedUser = 80;
			columnFids.OldRoofsectionid = 81;
			columnFids.SectionLat = 82;
			columnFids.SectionLong = 83;
			columnFids.PhotoThumbnail = 84;
			columnFids.QBMap = 85;
			columnFids.QBMapsMarker = 86;
			columnFids.QBMapsUrl = 87;
			columnFids.QBAddress = 88;
			columnFids.CreateReport = 90;
			columnFids.Report = 91;
			columnFids.DateCreated = 1;
			columnFids.DateModified = 2;
			columnFids.RecordOwner = 4;
			columnFids.LastModifiedBy = 5;

		}

		protected override function loadRequiredColumns():void
		{
			requiredColumns.addItem("rid");
			requiredColumns.addItem("RoofName");
		}
	}
}
