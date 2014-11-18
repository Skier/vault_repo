package com.ebs.eroof.dto
{
	import com.adobe.cairngorm.vo.IValueObject;
	import com.adobe.cairngorm.CairngormError;
	import com.quickbase.idn.fieldtypes.*;
	import com.quickbase.idn.fieldtypes.bool.*;
	import com.quickbase.idn.fieldtypes.float.*;
	import com.quickbase.idn.fieldtypes.int32.*;
	import com.quickbase.idn.fieldtypes.int64.*;
	import com.quickbase.idn.fieldtypes.text.*;
    import com.quickbase.idn.business.fieldmaps.IQuickBaseFieldMap;
	import com.quickbase.idn.dto.IKingussieDTO;
	import com.quickbase.idn.dto.KingussieDTOBase;
	import com.quickbase.idn.dto.IKingussieInfo;
	import com.quickbase.idn.dto.QuickBaseUserDTO;
	import com.quickbase.idn.dto.QuickBaseFileDTO;
	import com.quickbase.idn.model.QuickBaseMSAModel;
	import com.ebs.eroof.business.fieldmaps.Expenditures_FieldMap;
	import mx.collections.ArrayCollection;

	[Bindable]
	public class Expenditures_DTO extends KingussieDTOBase implements IValueObject, IKingussieDTO
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		// Record Owner Dirty Bit
		public function get IDNIsRecordOwnerDirty():Boolean				{return _fields["_isRecordOwnerDirty"];}


		// Current value getters
		public function get rid():String								{return _fields["_rid"];}
		public function get Client():String								{return _fields["_Client"];}
		public function get Client_isDirty():Boolean					{return _fields["_Client_isDirty"];}
		public function get Facility():String							{return _fields["_Facility"];}
		public function get Facility_isDirty():Boolean					{return _fields["_Facility_isDirty"];}
		public function get Roof():String								{return _fields["_Roof"];}
		public function get Roof_isDirty():Boolean						{return _fields["_Roof_isDirty"];}
		public function get ExpeditureDate():String						{return _fields["_ExpeditureDate"];}
		public function get ExpeditureDate_isDirty():Boolean			{return _fields["_ExpeditureDate_isDirty"];}
		public function get TypeOfWork():String							{return _fields["_TypeOfWork"];}
		public function get Status():String								{return _fields["_Status"];}
		public function get Amount():Number								{return _fields["_Amount"];}
		public function get Amount_isDirty():Boolean					{return _fields["_Amount_isDirty"];}
		public function get RelatedSection():Number						{return _fields["_RelatedSection"];}
		public function get RelatedSection_isDirty():Boolean			{return _fields["_RelatedSection_isDirty"];}
		public function get AddDocuments():String						{return _fields["_AddDocuments"];}
		public function get AddDocuments_isDirty():Boolean				{return _fields["_AddDocuments_isDirty"];}
		public function get Documents():String							{return _fields["_Documents"];}
		public function get Documents_isDirty():Boolean					{return _fields["_Documents_isDirty"];}
		public function get BudgetYear():Number							{return _fields["_BudgetYear"];}
		public function get BudgetYear_isDirty():Boolean				{return _fields["_BudgetYear_isDirty"];}
		public function get DateSort():Date								{return _fields["_DateSort"];}
		public function get DateSort_isDirty():Boolean					{return _fields["_DateSort_isDirty"];}
		public function get Contractor():String							{return _fields["_Contractor"];}
		public function get Contractor_isDirty():Boolean				{return _fields["_Contractor_isDirty"];}
		public function get ActionItem():String							{return _fields["_ActionItem"];}
		public function get ActionItem_isDirty():Boolean				{return _fields["_ActionItem_isDirty"];}
		public function get Allocation():String							{return _fields["_Allocation"];}
		public function get Urgency():String							{return _fields["_Urgency"];}
		public function get Description():String						{return _fields["_Description"];}
		public function get Description_isDirty():Boolean				{return _fields["_Description_isDirty"];}
		public function get IsCompleted():Boolean						{return _fields["_IsCompleted"];}
		public function get IsCompleted_isDirty():Boolean				{return _fields["_IsCompleted_isDirty"];}
		public function get CompletionDate():Date						{return _fields["_CompletionDate"];}
		public function get CompletionDate_isDirty():Boolean			{return _fields["_CompletionDate_isDirty"];}
		public function get IsInvoiced():Boolean						{return _fields["_IsInvoiced"];}
		public function get IsInvoiced_isDirty():Boolean				{return _fields["_IsInvoiced_isDirty"];}
		public function get InvoiceDate():Date							{return _fields["_InvoiceDate"];}
		public function get InvoiceDate_isDirty():Boolean				{return _fields["_InvoiceDate_isDirty"];}
		public function get IsApproved():Boolean						{return _fields["_IsApproved"];}
		public function get IsApproved_isDirty():Boolean				{return _fields["_IsApproved_isDirty"];}
		public function get ApprovalDate():Date							{return _fields["_ApprovalDate"];}
		public function get ApprovalDate_isDirty():Boolean				{return _fields["_ApprovalDate_isDirty"];}
		public function get IsPaid():Boolean							{return _fields["_IsPaid"];}
		public function get IsPaid_isDirty():Boolean					{return _fields["_IsPaid_isDirty"];}
		public function get PaymentDate():Date							{return _fields["_PaymentDate"];}
		public function get PaymentDate_isDirty():Boolean				{return _fields["_PaymentDate_isDirty"];}
		public function get InitDate():String							{return _fields["_InitDate"];}
		public function get InitDate_isDirty():Boolean					{return _fields["_InitDate_isDirty"];}
		public function get InitBy():String								{return _fields["_InitBy"];}
		public function get InitBy_isDirty():Boolean					{return _fields["_InitBy_isDirty"];}
		public function get Notes():String								{return _fields["_Notes"];}
		public function get Notes_isDirty():Boolean						{return _fields["_Notes_isDirty"];}
		public function get AllowedClientUser():Number					{return _fields["_AllowedClientUser"];}
		public function get AllowedClientUser_isDirty():Boolean			{return _fields["_AllowedClientUser_isDirty"];}
		public function get AllowedFacilityUser():Number				{return _fields["_AllowedFacilityUser"];}
		public function get AllowedFacilityUser_isDirty():Boolean		{return _fields["_AllowedFacilityUser_isDirty"];}
		public function get AllowedSectionUser():Number					{return _fields["_AllowedSectionUser"];}
		public function get AllowedSectionUser_isDirty():Boolean		{return _fields["_AllowedSectionUser_isDirty"];}
		public function get PhotoThumbnail():String						{return _fields["_PhotoThumbnail"];}
		public function get PhotoThumbnail_isDirty():Boolean			{return _fields["_PhotoThumbnail_isDirty"];}
		public function get Test():String								{return _fields["_Test"];}
		public function get Test_isDirty():Boolean						{return _fields["_Test_isDirty"];}
		public function get AllowedUser():Number						{return _fields["_AllowedUser"];}
		public function get AllowedUser_isDirty():Boolean				{return _fields["_AllowedUser_isDirty"];}
		public function get DateCreated():Date							{return _fields["_DateCreated"];}
		public function get DateCreated_isDirty():Boolean				{return _fields["_DateCreated_isDirty"];}
		public function get DateModified():Date							{return _fields["_DateModified"];}
		public function get DateModified_isDirty():Boolean				{return _fields["_DateModified_isDirty"];}
		public function get RecordOwner():QuickBaseUserDTO 				{return _fields["_RecordOwner"];}
		public function get RecordOwner_isDirty():Boolean					{return _fields["_RecordOwner_isDirty"];}
		public function get LastModifiedBy():QuickBaseUserDTO 			{return _fields["_LastModifiedBy"];}
		public function get LastModifiedBy_isDirty():Boolean				{return _fields["_LastModifiedBy_isDirty"];}

		// Choice getters
		public function get TypeOfWorkChoices():ArrayCollection			{return TypeOfWork_Info.choiceArray;}
		public function get StatusChoices():ArrayCollection				{return Status_Info.choiceArray;}
		public function get AllocationChoices():ArrayCollection			{return Allocation_Info.choiceArray;}
		public function get UrgencyChoices():ArrayCollection			{return Urgency_Info.choiceArray;}

		// Current value setters
		public function set RecordOwner(val:QuickBaseUserDTO):void
		{
			if (val == null) return;
			if (_fields["_RecordOwner"] == val)  return;
			var model:QuickBaseMSAModel = QuickBaseMSAModel.getInstance();
			if (model.currentUser == null || !model.currentUser.isAdministrator()) {
				throw new Error("Cannot set record owner if not in the Adminstrator role");
			}
			_fields["_RecordOwner"] = val;
			_fields["_isRecordOwnerDirty"] = true;
		}
		public function set ExpeditureDate(val:String):void				{_fields["_ExpeditureDate"] = val; _fields["_ExpeditureDate_isDirty"] = true;}
		public function set TypeOfWork(val:String):void
		{
			if (TypeOfWork_Info.validate(val)) {
				_fields["_TypeOfWork"] = val;
				_fields["_TypeOfWork_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + TypeOfWork_Info.label + " value requested: " + val);
			}
		}

		public function set Status(val:String):void
		{
			if (Status_Info.validate(val)) {
				_fields["_Status"] = val;
				_fields["_Status_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + Status_Info.label + " value requested: " + val);
			}
		}

		public function set Amount(val:Number):void						{_fields["_Amount"] = val; _fields["_Amount_isDirty"] = true;}
		public function set RelatedSection(val:Number):void				{_fields["_RelatedSection"] = val; _fields["_RelatedSection_isDirty"] = true;}
		public function set BudgetYear(val:Number):void					{_fields["_BudgetYear"] = val; _fields["_BudgetYear_isDirty"] = true;}
		public function set DateSort(val:Date):void						{_fields["_DateSort"] = val; _fields["_DateSort_isDirty"] = true;}
		public function set Contractor(val:String):void					{_fields["_Contractor"] = val; _fields["_Contractor_isDirty"] = true;}
		public function set ActionItem(val:String):void					{_fields["_ActionItem"] = val; _fields["_ActionItem_isDirty"] = true;}
		public function set Allocation(val:String):void
		{
			if (Allocation_Info.validate(val)) {
				_fields["_Allocation"] = val;
				_fields["_Allocation_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + Allocation_Info.label + " value requested: " + val);
			}
		}

		public function set Urgency(val:String):void
		{
			if (Urgency_Info.validate(val)) {
				_fields["_Urgency"] = val;
				_fields["_Urgency_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + Urgency_Info.label + " value requested: " + val);
			}
		}

		public function set Description(val:String):void				{_fields["_Description"] = val; _fields["_Description_isDirty"] = true;}
		public function set IsCompleted(val:Boolean):void				{_fields["_IsCompleted"] = val; _fields["_IsCompleted_isDirty"] = true;}
		public function set CompletionDate(val:Date):void				{_fields["_CompletionDate"] = val; _fields["_CompletionDate_isDirty"] = true;}
		public function set IsInvoiced(val:Boolean):void				{_fields["_IsInvoiced"] = val; _fields["_IsInvoiced_isDirty"] = true;}
		public function set InvoiceDate(val:Date):void					{_fields["_InvoiceDate"] = val; _fields["_InvoiceDate_isDirty"] = true;}
		public function set IsApproved(val:Boolean):void				{_fields["_IsApproved"] = val; _fields["_IsApproved_isDirty"] = true;}
		public function set ApprovalDate(val:Date):void					{_fields["_ApprovalDate"] = val; _fields["_ApprovalDate_isDirty"] = true;}
		public function set IsPaid(val:Boolean):void					{_fields["_IsPaid"] = val; _fields["_IsPaid_isDirty"] = true;}
		public function set PaymentDate(val:Date):void					{_fields["_PaymentDate"] = val; _fields["_PaymentDate_isDirty"] = true;}
		public function set InitDate(val:String):void					{_fields["_InitDate"] = val; _fields["_InitDate_isDirty"] = true;}
		public function set InitBy(val:String):void						{_fields["_InitBy"] = val; _fields["_InitBy_isDirty"] = true;}
		public function set Notes(val:String):void						{_fields["_Notes"] = val; _fields["_Notes_isDirty"] = true;}

		// Framework value setters
		/**
		 *
		 * @private
		 *
		 */
		public override function IDNSet(fieldName:String, val:String):void
		{
			var oldval:Object = _fields["_"+fieldName];
			this["IDN"+fieldName] = val;
			this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,fieldName,oldval,_fields["_"+fieldName]));
		}

		private function set IDNrid(val:String):void					{_fields["_rid"] = val;}
		private function set IDNClient(val:String):void					{_fields["_Client"] = val;}
		private function set IDNFacility(val:String):void				{_fields["_Facility"] = val;}
		private function set IDNRoof(val:String):void					{_fields["_Roof"] = val;}
		private function set IDNExpeditureDate(val:String):void			{_fields["_ExpeditureDate"] = val;}
		private function set IDNTypeOfWork(val:String):void				{_fields["_TypeOfWork"] = val;}
		private function set IDNStatus(val:String):void					{_fields["_Status"] = val;}
		private function set IDNAmount(val:String):void					{_fields["_Amount"] = Number(val);}
		private function set IDNRelatedSection(val:String):void			{_fields["_RelatedSection"] = Number(val);}
		private function set IDNAddDocuments(val:String):void			{_fields["_AddDocuments"] = val;}
		private function set IDNDocuments(val:String):void				{_fields["_Documents"] = val;}
		private function set IDNBudgetYear(val:String):void				{_fields["_BudgetYear"] = Number(val);}
		private function set IDNDateSort(val:String):void				{_fields["_DateSort"] = new Date(Number(val));}
		private function set IDNContractor(val:String):void				{_fields["_Contractor"] = val;}
		private function set IDNActionItem(val:String):void				{_fields["_ActionItem"] = val;}
		private function set IDNAllocation(val:String):void				{_fields["_Allocation"] = val;}
		private function set IDNUrgency(val:String):void				{_fields["_Urgency"] = val;}
		private function set IDNDescription(val:String):void			{_fields["_Description"] = val;}
		private function set IDNIsCompleted(val:String):void			{_fields["_IsCompleted"] = Boolean(Number(val));}
		private function set IDNCompletionDate(val:String):void			{_fields["_CompletionDate"] = new Date(Number(val));}
		private function set IDNIsInvoiced(val:String):void				{_fields["_IsInvoiced"] = Boolean(Number(val));}
		private function set IDNInvoiceDate(val:String):void			{_fields["_InvoiceDate"] = new Date(Number(val));}
		private function set IDNIsApproved(val:String):void				{_fields["_IsApproved"] = Boolean(Number(val));}
		private function set IDNApprovalDate(val:String):void			{_fields["_ApprovalDate"] = new Date(Number(val));}
		private function set IDNIsPaid(val:String):void					{_fields["_IsPaid"] = Boolean(Number(val));}
		private function set IDNPaymentDate(val:String):void			{_fields["_PaymentDate"] = new Date(Number(val));}
		private function set IDNInitDate(val:String):void				{_fields["_InitDate"] = val;}
		private function set IDNInitBy(val:String):void					{_fields["_InitBy"] = val;}
		private function set IDNNotes(val:String):void					{_fields["_Notes"] = val;}
		private function set IDNAllowedClientUser(val:String):void		{_fields["_AllowedClientUser"] = Number(val);}
		private function set IDNAllowedFacilityUser(val:String):void	{_fields["_AllowedFacilityUser"] = Number(val);}
		private function set IDNAllowedSectionUser(val:String):void		{_fields["_AllowedSectionUser"] = Number(val);}
		private function set IDNPhotoThumbnail(val:String):void			{_fields["_PhotoThumbnail"] = val;}
		private function set IDNTest(val:String):void					{_fields["_Test"] = val;}
		private function set IDNAllowedUser(val:String):void			{_fields["_AllowedUser"] = Number(val);}
		private function set IDNDateCreated(val:String):void			{_fields["_DateCreated"] = new Date(Number(val));}
		private function set IDNDateModified(val:String):void			{_fields["_DateModified"] = new Date(Number(val));}
		private function set IDNRecordOwner(val:String):void
		{
			if (val == null) return;
			var model:QuickBaseMSAModel = QuickBaseMSAModel.getInstance();
			_fields["_RecordOwner"] = model.appUserList.findUserID(val);
		}
		private function set IDNLastModifiedBy(val:String):void
		{
			if (val == null) return;
			var model:QuickBaseMSAModel = QuickBaseMSAModel.getInstance();
			_fields["_LastModifiedBy"] = model.appUserList.findUserID(val);
		}

		// Object getters
		public function getInfoObj():IKingussieInfo						{return Expenditures_Info.getInstance();}
		public function getFieldMapObj():IQuickBaseFieldMap				{return new Expenditures_FieldMap();}
		public function getFieldMapClass():Class						{return Expenditures_FieldMap;}
		public function get dtoClass():Class							{return Expenditures_DTO;}

		// MetaData Information Objects getters
		public function get Client_Info():TextField						{return Expenditures_Info.getInstance().Client_Info;}
		public function get Facility_Info():TextField					{return Expenditures_Info.getInstance().Facility_Info;}
		public function get Roof_Info():TextField						{return Expenditures_Info.getInstance().Roof_Info;}
		public function get ExpeditureDate_Info():TextField				{return Expenditures_Info.getInstance().ExpeditureDate_Info;}
		public function get TypeOfWork_Info():ChoiceField				{return Expenditures_Info.getInstance().TypeOfWork_Info;}
		public function get Status_Info():ChoiceField					{return Expenditures_Info.getInstance().Status_Info;}
		public function get Amount_Info():CurrencyField					{return Expenditures_Info.getInstance().Amount_Info;}
		public function get RelatedSection_Info():NumberField			{return Expenditures_Info.getInstance().RelatedSection_Info;}
		public function get AddDocuments_Info():URLField				{return Expenditures_Info.getInstance().AddDocuments_Info;}
		public function get Documents_Info():DbLinkField				{return Expenditures_Info.getInstance().Documents_Info;}
		public function get BudgetYear_Info():NumberField				{return Expenditures_Info.getInstance().BudgetYear_Info;}
		public function get DateSort_Info():DateField					{return Expenditures_Info.getInstance().DateSort_Info;}
		public function get Contractor_Info():TextField					{return Expenditures_Info.getInstance().Contractor_Info;}
		public function get ActionItem_Info():TextField					{return Expenditures_Info.getInstance().ActionItem_Info;}
		public function get Allocation_Info():ChoiceField				{return Expenditures_Info.getInstance().Allocation_Info;}
		public function get Urgency_Info():ChoiceField					{return Expenditures_Info.getInstance().Urgency_Info;}
		public function get Description_Info():TextField				{return Expenditures_Info.getInstance().Description_Info;}
		public function get IsCompleted_Info():BooleanField				{return Expenditures_Info.getInstance().IsCompleted_Info;}
		public function get CompletionDate_Info():DateField				{return Expenditures_Info.getInstance().CompletionDate_Info;}
		public function get IsInvoiced_Info():BooleanField				{return Expenditures_Info.getInstance().IsInvoiced_Info;}
		public function get InvoiceDate_Info():DateField				{return Expenditures_Info.getInstance().InvoiceDate_Info;}
		public function get IsApproved_Info():BooleanField				{return Expenditures_Info.getInstance().IsApproved_Info;}
		public function get ApprovalDate_Info():DateField				{return Expenditures_Info.getInstance().ApprovalDate_Info;}
		public function get IsPaid_Info():BooleanField					{return Expenditures_Info.getInstance().IsPaid_Info;}
		public function get PaymentDate_Info():DateField				{return Expenditures_Info.getInstance().PaymentDate_Info;}
		public function get InitDate_Info():TextField					{return Expenditures_Info.getInstance().InitDate_Info;}
		public function get InitBy_Info():TextField						{return Expenditures_Info.getInstance().InitBy_Info;}
		public function get Notes_Info():TextField						{return Expenditures_Info.getInstance().Notes_Info;}
		public function get AllowedClientUser_Info():NumberField		{return Expenditures_Info.getInstance().AllowedClientUser_Info;}
		public function get AllowedFacilityUser_Info():NumberField		{return Expenditures_Info.getInstance().AllowedFacilityUser_Info;}
		public function get AllowedSectionUser_Info():NumberField		{return Expenditures_Info.getInstance().AllowedSectionUser_Info;}
		public function get PhotoThumbnail_Info():TextField				{return Expenditures_Info.getInstance().PhotoThumbnail_Info;}
		public function get Test_Info():TextField						{return Expenditures_Info.getInstance().Test_Info;}
		public function get AllowedUser_Info():NumberField				{return Expenditures_Info.getInstance().AllowedUser_Info;}
		public function get DateCreated_Info():TimeStampField			{return Expenditures_Info.getInstance().DateCreated_Info;}
		public function get DateModified_Info():TimeStampField			{return Expenditures_Info.getInstance().DateModified_Info;}
		public function get RecordOwner_Info():UserIdField				{return Expenditures_Info.getInstance().RecordOwner_Info;}
		public function get LastModifiedBy_Info():UserIdField			{return Expenditures_Info.getInstance().LastModifiedBy_Info;}

	}
}
