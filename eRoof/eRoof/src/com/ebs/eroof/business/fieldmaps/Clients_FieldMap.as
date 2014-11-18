package com.ebs.eroof.business.fieldmaps
{
	import com.quickbase.idn.business.fieldmaps.QuickBaseFieldMap;
	import com.quickbase.idn.business.fieldmaps.IQuickBaseFieldMap;

	public class Clients_FieldMap extends QuickBaseFieldMap implements IQuickBaseFieldMap
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		public function Clients_FieldMap()
		{
			super();
		}

		protected override function loadFids():void
		{
			columnFids.ClientName = 7;
			columnFids.Facilities = 25;
			columnFids.Address = 9;
			columnFids.City = 11;
			columnFids.FacilitiesCount = 30;
			columnFids.SectionsCount = 31;
			columnFids.TotalSqFt = 32;
			columnFids.TotalValue = 33;
			columnFids.Province = 12;
			columnFids.rid = 3;
			columnFids.RelatedSegment = 39;
			columnFids.Segment = 40;
			columnFids.AddDocument = 50;
			columnFids.Documents = 49;
			columnFids.AddFacility = 26;
			columnFids.Category = 44;
			columnFids.BriefName = 8;
			columnFids.Country = 42;
			columnFids.PostalCode = 13;
			columnFids.PrimaryContact = 14;
			columnFids.Position = 15;
			columnFids.Phone = 16;
			columnFids.Cell = 17;
			columnFids.Fax = 18;
			columnFids.EMail = 19;
			columnFids.AdditionalContacts = 20;
			columnFids.FiscalYearEnd = 43;
			columnFids.BudgetDeadline = 21;
			columnFids.BudgetNotes = 22;
			columnFids.ClientStandards = 23;
			columnFids.Notes = 24;
			columnFids.ClientUsers = 51;
			columnFids.AddClientUser = 52;
			columnFids.AllowedClientUser = 53;
			columnFids.AllowedUser = 55;
			columnFids.Test = 56;
			columnFids.Photo = 67;
			columnFids.PhotoThumbnail = 68;
			columnFids.QBMap = 72;
			columnFids.MapLatLong = 76;
			columnFids.MapZoom = 77;
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
