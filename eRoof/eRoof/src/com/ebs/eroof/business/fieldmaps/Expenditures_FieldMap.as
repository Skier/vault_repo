package com.ebs.eroof.business.fieldmaps
{
	import com.quickbase.idn.business.fieldmaps.QuickBaseFieldMap;
	import com.quickbase.idn.business.fieldmaps.IQuickBaseFieldMap;

	public class Expenditures_FieldMap extends QuickBaseFieldMap implements IQuickBaseFieldMap
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		public function Expenditures_FieldMap()
		{
			super();
		}

		protected override function loadFids():void
		{
			columnFids.Client = 13;
			columnFids.Facility = 14;
			columnFids.Roof = 15;
			columnFids.ExpeditureDate = 27;
			columnFids.TypeOfWork = 34;
			columnFids.Status = 35;
			columnFids.Amount = 32;
			columnFids.rid = 3;
			columnFids.RelatedSection = 12;
			columnFids.AddDocuments = 42;
			columnFids.Documents = 41;
			columnFids.BudgetYear = 40;
			columnFids.DateSort = 43;
			columnFids.Contractor = 29;
			columnFids.ActionItem = 33;
			columnFids.Allocation = 39;
			columnFids.Urgency = 36;
			columnFids.Description = 31;
			columnFids.IsCompleted = 16;
			columnFids.CompletionDate = 17;
			columnFids.IsInvoiced = 18;
			columnFids.InvoiceDate = 19;
			columnFids.IsApproved = 20;
			columnFids.ApprovalDate = 21;
			columnFids.IsPaid = 22;
			columnFids.PaymentDate = 23;
			columnFids.InitDate = 28;
			columnFids.InitBy = 30;
			columnFids.Notes = 37;
			columnFids.AllowedClientUser = 44;
			columnFids.AllowedFacilityUser = 45;
			columnFids.AllowedSectionUser = 46;
			columnFids.PhotoThumbnail = 48;
			columnFids.Test = 49;
			columnFids.AllowedUser = 50;
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
