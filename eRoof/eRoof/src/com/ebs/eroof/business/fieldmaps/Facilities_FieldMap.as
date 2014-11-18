package com.ebs.eroof.business.fieldmaps
{
	import com.quickbase.idn.business.fieldmaps.QuickBaseFieldMap;
	import com.quickbase.idn.business.fieldmaps.IQuickBaseFieldMap;

	public class Facilities_FieldMap extends QuickBaseFieldMap implements IQuickBaseFieldMap
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		public function Facilities_FieldMap()
		{
			super();
		}

		protected override function loadFids():void
		{
			columnFids.AddressMap = 79;
			columnFids.SiteMap = 80;
			columnFids.Client = 30;
			columnFids.FacilityName = 6;
			columnFids.Address = 9;
			columnFids.City = 11;
			columnFids.SectionsCount = 33;
			columnFids.TotalSqFt = 34;
			columnFids.TotalValue = 35;
			columnFids.Sections = 31;
			columnFids.rid = 3;
			columnFids.RelatedClient = 29;
			columnFids.AddSection = 32;
			columnFids.AddDocument = 57;
			columnFids.Documents = 56;
			columnFids.BriefName = 7;
			columnFids.Photo = 8;
			columnFids.PhotoName = 45;
			columnFids.Province = 12;
			columnFids.Country = 13;
			columnFids.PostalCode = 14;
			columnFids.TypeOfBuilding = 15;
			columnFids.Neighbourhood = 16;
			columnFids.PrimaryContact = 17;
			columnFids.Position = 19;
			columnFids.Phone = 20;
			columnFids.Cell = 21;
			columnFids.Fax = 22;
			columnFids.EMail = 23;
			columnFids.AdditionalContacts = 24;
			columnFids.FiscalYearEnd = 53;
			columnFids.BudgetDeadline = 25;
			columnFids.BudgetNotes = 26;
			columnFids.FacilityStandards = 27;
			columnFids.Leaking = 47;
			columnFids.Keyplan = 46;
			columnFids.KeyplanName = 60;
			columnFids.X1 = 48;
			columnFids.Y1 = 49;
			columnFids.X2 = 50;
			columnFids.Y2 = 51;
			columnFids.Drawing = 61;
			columnFids.DrawingName = 52;
			columnFids.Notes = 28;
			columnFids.FacilityUsers = 63;
			columnFids.AddFacilityUser = 64;
			columnFids.PhotoThumbnail = 65;
			columnFids.KeyplanThumbnail = 66;
			columnFids.AllowedFacilityUser = 68;
			columnFids.AllowedClientUser = 69;
			columnFids.AllowedUser = 71;
			columnFids.OldFacilityid = 72;
			columnFids.MapLatLong = 73;
			columnFids.MapZoom = 75;
			columnFids.SitePlanZoom = 78;
			columnFids.DrawingThumbnail = 84;
			columnFids.AddressLat = 85;
			columnFids.AddressLong = 86;
			columnFids.IRPCoverPagePhotoFileName = 87;
			columnFids.IRPKeyPlanFileName = 88;
			columnFids.IRPDrawingFileName = 89;
			columnFids.QBMapsUrl = 90;
			columnFids.QBMapsMarker = 91;
			columnFids.QBAddress = 93;
			columnFids.SitePlan = 94;
			columnFids.Map2 = 95;
			columnFids.SitePlanLatLong = 96;
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
