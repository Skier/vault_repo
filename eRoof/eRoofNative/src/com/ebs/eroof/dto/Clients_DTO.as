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
	import com.ebs.eroof.business.fieldmaps.Clients_FieldMap;
	import mx.collections.ArrayCollection;

	[Bindable]
	public class Clients_DTO extends KingussieDTOBase implements IValueObject, IKingussieDTO
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		// Record Owner Dirty Bit
		public function get IDNIsRecordOwnerDirty():Boolean				{return _fields["_isRecordOwnerDirty"];}


		// Current value getters
		public function get rid():String								{return _fields["_rid"];}
		public function get ClientName():String							{return _fields["_ClientName"];}
		public function get ClientName_isDirty():Boolean				{return _fields["_ClientName_isDirty"];}
		public function get Address():String							{return _fields["_Address"];}
		public function get Address_isDirty():Boolean					{return _fields["_Address_isDirty"];}
		public function get City():String								{return _fields["_City"];}
		public function get City_isDirty():Boolean						{return _fields["_City_isDirty"];}
		public function get FacilitiesCount():Number					{return _fields["_FacilitiesCount"];}
		public function get FacilitiesCount_isDirty():Boolean			{return _fields["_FacilitiesCount_isDirty"];}
		public function get SectionsCount():Number						{return _fields["_SectionsCount"];}
		public function get SectionsCount_isDirty():Boolean				{return _fields["_SectionsCount_isDirty"];}
		public function get TotalSqFt():Number							{return _fields["_TotalSqFt"];}
		public function get TotalSqFt_isDirty():Boolean					{return _fields["_TotalSqFt_isDirty"];}
		public function get TotalValue():Number							{return _fields["_TotalValue"];}
		public function get TotalValue_isDirty():Boolean				{return _fields["_TotalValue_isDirty"];}
		public function get Province():String							{return _fields["_Province"];}
		public function get RelatedSegment():Number						{return _fields["_RelatedSegment"];}
		public function get RelatedSegment_isDirty():Boolean			{return _fields["_RelatedSegment_isDirty"];}
		public function get Segment():String							{return _fields["_Segment"];}
		public function get Segment_isDirty():Boolean					{return _fields["_Segment_isDirty"];}
		public function get AddDocument():String						{return _fields["_AddDocument"];}
		public function get AddDocument_isDirty():Boolean				{return _fields["_AddDocument_isDirty"];}
		public function get Documents():String							{return _fields["_Documents"];}
		public function get Documents_isDirty():Boolean					{return _fields["_Documents_isDirty"];}
		public function get AddFacility():String						{return _fields["_AddFacility"];}
		public function get AddFacility_isDirty():Boolean				{return _fields["_AddFacility_isDirty"];}
		public function get Category():String							{return _fields["_Category"];}
		public function get Category_isDirty():Boolean					{return _fields["_Category_isDirty"];}
		public function get BriefName():String							{return _fields["_BriefName"];}
		public function get BriefName_isDirty():Boolean					{return _fields["_BriefName_isDirty"];}
		public function get Country():String							{return _fields["_Country"];}
		public function get PostalCode():String							{return _fields["_PostalCode"];}
		public function get PostalCode_isDirty():Boolean				{return _fields["_PostalCode_isDirty"];}
		public function get PrimaryContact():String						{return _fields["_PrimaryContact"];}
		public function get PrimaryContact_isDirty():Boolean			{return _fields["_PrimaryContact_isDirty"];}
		public function get Position():String							{return _fields["_Position"];}
		public function get Position_isDirty():Boolean					{return _fields["_Position_isDirty"];}
		public function get Phone():String								{return _fields["_Phone"];}
		public function get Phone_isDirty():Boolean						{return _fields["_Phone_isDirty"];}
		public function get Cell():String								{return _fields["_Cell"];}
		public function get Cell_isDirty():Boolean						{return _fields["_Cell_isDirty"];}
		public function get Fax():String								{return _fields["_Fax"];}
		public function get Fax_isDirty():Boolean						{return _fields["_Fax_isDirty"];}
		public function get EMail():String								{return _fields["_EMail"];}
		public function get EMail_isDirty():Boolean						{return _fields["_EMail_isDirty"];}
		public function get AdditionalContacts():String					{return _fields["_AdditionalContacts"];}
		public function get AdditionalContacts_isDirty():Boolean		{return _fields["_AdditionalContacts_isDirty"];}
		public function get FiscalYearEnd():String						{return _fields["_FiscalYearEnd"];}
		public function get FiscalYearEnd_isDirty():Boolean				{return _fields["_FiscalYearEnd_isDirty"];}
		public function get BudgetDeadline():String						{return _fields["_BudgetDeadline"];}
		public function get BudgetDeadline_isDirty():Boolean			{return _fields["_BudgetDeadline_isDirty"];}
		public function get BudgetNotes():String						{return _fields["_BudgetNotes"];}
		public function get BudgetNotes_isDirty():Boolean				{return _fields["_BudgetNotes_isDirty"];}
		public function get ClientStandards():String					{return _fields["_ClientStandards"];}
		public function get ClientStandards_isDirty():Boolean			{return _fields["_ClientStandards_isDirty"];}
		public function get Notes():String								{return _fields["_Notes"];}
		public function get Notes_isDirty():Boolean						{return _fields["_Notes_isDirty"];}
		public function get Test():String								{return _fields["_Test"];}
		public function get Test_isDirty():Boolean						{return _fields["_Test_isDirty"];}
		public function get Photo():QuickBaseFileDTO {
			if(_fields["_Photo"] == null) {
				_fields["_Photo"] = new QuickBaseFileDTO();
			}
			return _fields["_Photo"];
		}
		public function get PhotoThumbnail():String						{return _fields["_PhotoThumbnail"];}
		public function get PhotoThumbnail_isDirty():Boolean			{return _fields["_PhotoThumbnail_isDirty"];}
		public function get QBMap():String								{return _fields["_QBMap"];}
		public function get QBMap_isDirty():Boolean						{return _fields["_QBMap_isDirty"];}
		public function get DateCreated():Date							{return _fields["_DateCreated"];}
		public function get DateCreated_isDirty():Boolean				{return _fields["_DateCreated_isDirty"];}
		public function get DateModified():Date							{return _fields["_DateModified"];}
		public function get DateModified_isDirty():Boolean				{return _fields["_DateModified_isDirty"];}
		public function get RecordOwner():QuickBaseUserDTO 				{return _fields["_RecordOwner"];}
		public function get RecordOwner_isDirty():Boolean					{return _fields["_RecordOwner_isDirty"];}
		public function get LastModifiedBy():QuickBaseUserDTO 			{return _fields["_LastModifiedBy"];}
		public function get LastModifiedBy_isDirty():Boolean				{return _fields["_LastModifiedBy_isDirty"];}

		// Choice getters
		public function get ProvinceChoices():ArrayCollection			{return Province_Info.choiceArray;}
		public function get CountryChoices():ArrayCollection			{return Country_Info.choiceArray;}

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
		public function set ClientName(val:String):void					{_fields["_ClientName"] = val; _fields["_ClientName_isDirty"] = true;}
		public function set Address(val:String):void					{_fields["_Address"] = val; _fields["_Address_isDirty"] = true;}
		public function set City(val:String):void						{_fields["_City"] = val; _fields["_City_isDirty"] = true;}
		public function set Province(val:String):void
		{
			if (Province_Info.validate(val)) {
				_fields["_Province"] = val;
				_fields["_Province_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + Province_Info.label + " value requested: " + val);
			}
		}

		public function set RelatedSegment(val:Number):void				{_fields["_RelatedSegment"] = val; _fields["_RelatedSegment_isDirty"] = true;}
		public function set Category(val:String):void					{_fields["_Category"] = val; _fields["_Category_isDirty"] = true;}
		public function set BriefName(val:String):void					{_fields["_BriefName"] = val; _fields["_BriefName_isDirty"] = true;}
		public function set Country(val:String):void
		{
			if (Country_Info.validate(val)) {
				_fields["_Country"] = val;
				_fields["_Country_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + Country_Info.label + " value requested: " + val);
			}
		}

		public function set PostalCode(val:String):void					{_fields["_PostalCode"] = val; _fields["_PostalCode_isDirty"] = true;}
		public function set PrimaryContact(val:String):void				{_fields["_PrimaryContact"] = val; _fields["_PrimaryContact_isDirty"] = true;}
		public function set Position(val:String):void					{_fields["_Position"] = val; _fields["_Position_isDirty"] = true;}
		public function set Phone(val:String):void						{_fields["_Phone"] = val; _fields["_Phone_isDirty"] = true;}
		public function set Cell(val:String):void						{_fields["_Cell"] = val; _fields["_Cell_isDirty"] = true;}
		public function set Fax(val:String):void						{_fields["_Fax"] = val; _fields["_Fax_isDirty"] = true;}
		public function set EMail(val:String):void						{_fields["_EMail"] = val; _fields["_EMail_isDirty"] = true;}
		public function set AdditionalContacts(val:String):void			{_fields["_AdditionalContacts"] = val; _fields["_AdditionalContacts_isDirty"] = true;}
		public function set FiscalYearEnd(val:String):void				{_fields["_FiscalYearEnd"] = val; _fields["_FiscalYearEnd_isDirty"] = true;}
		public function set BudgetDeadline(val:String):void				{_fields["_BudgetDeadline"] = val; _fields["_BudgetDeadline_isDirty"] = true;}
		public function set BudgetNotes(val:String):void				{_fields["_BudgetNotes"] = val; _fields["_BudgetNotes_isDirty"] = true;}
		public function set ClientStandards(val:String):void			{_fields["_ClientStandards"] = val; _fields["_ClientStandards_isDirty"] = true;}
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
		private function set IDNClientName(val:String):void				{_fields["_ClientName"] = val;}
		private function set IDNAddress(val:String):void				{_fields["_Address"] = val;}
		private function set IDNCity(val:String):void					{_fields["_City"] = val;}
		private function set IDNFacilitiesCount(val:String):void		{_fields["_FacilitiesCount"] = Number(val);}
		private function set IDNSectionsCount(val:String):void			{_fields["_SectionsCount"] = Number(val);}
		private function set IDNTotalSqFt(val:String):void				{_fields["_TotalSqFt"] = Number(val);}
		private function set IDNTotalValue(val:String):void				{_fields["_TotalValue"] = Number(val);}
		private function set IDNProvince(val:String):void				{_fields["_Province"] = val;}
		private function set IDNRelatedSegment(val:String):void			{_fields["_RelatedSegment"] = Number(val);}
		private function set IDNSegment(val:String):void				{_fields["_Segment"] = val;}
		private function set IDNAddDocument(val:String):void			{_fields["_AddDocument"] = val;}
		private function set IDNDocuments(val:String):void				{_fields["_Documents"] = val;}
		private function set IDNAddFacility(val:String):void			{_fields["_AddFacility"] = val;}
		private function set IDNCategory(val:String):void				{_fields["_Category"] = val;}
		private function set IDNBriefName(val:String):void				{_fields["_BriefName"] = val;}
		private function set IDNCountry(val:String):void				{_fields["_Country"] = val;}
		private function set IDNPostalCode(val:String):void				{_fields["_PostalCode"] = val;}
		private function set IDNPrimaryContact(val:String):void			{_fields["_PrimaryContact"] = val;}
		private function set IDNPosition(val:String):void				{_fields["_Position"] = val;}
		private function set IDNPhone(val:String):void					{_fields["_Phone"] = val;}
		private function set IDNCell(val:String):void					{_fields["_Cell"] = val;}
		private function set IDNFax(val:String):void					{_fields["_Fax"] = val;}
		private function set IDNEMail(val:String):void					{_fields["_EMail"] = val;}
		private function set IDNAdditionalContacts(val:String):void		{_fields["_AdditionalContacts"] = val;}
		private function set IDNFiscalYearEnd(val:String):void			{_fields["_FiscalYearEnd"] = val;}
		private function set IDNBudgetDeadline(val:String):void			{_fields["_BudgetDeadline"] = val;}
		private function set IDNBudgetNotes(val:String):void			{_fields["_BudgetNotes"] = val;}
		private function set IDNClientStandards(val:String):void		{_fields["_ClientStandards"] = val;}
		private function set IDNNotes(val:String):void					{_fields["_Notes"] = val;}
		private function set IDNTest(val:String):void					{_fields["_Test"] = val;}
		private function set IDNPhoto(val:String):void {
			if(_fields["_Photo"] == null) {
				_fields["_Photo"] = new QuickBaseFileDTO();
			}
			_fields["_Photo"].url = val;
		}
		private function set IDNPhotoThumbnail(val:String):void			{_fields["_PhotoThumbnail"] = val;}
		private function set IDNQBMap(val:String):void					{_fields["_QBMap"] = val;}
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
		public function getInfoObj():IKingussieInfo						{return Clients_Info.getInstance();}
		public function getFieldMapObj():IQuickBaseFieldMap				{return new Clients_FieldMap();}
		public function getFieldMapClass():Class						{return Clients_FieldMap;}
		public function get dtoClass():Class							{return Clients_DTO;}

		// MetaData Information Objects getters
		public function get ClientName_Info():TextField					{return Clients_Info.getInstance().ClientName_Info;}
		public function get Address_Info():TextField					{return Clients_Info.getInstance().Address_Info;}
		public function get City_Info():TextField						{return Clients_Info.getInstance().City_Info;}
		public function get FacilitiesCount_Info():NumberField			{return Clients_Info.getInstance().FacilitiesCount_Info;}
		public function get SectionsCount_Info():NumberField			{return Clients_Info.getInstance().SectionsCount_Info;}
		public function get TotalSqFt_Info():NumberField				{return Clients_Info.getInstance().TotalSqFt_Info;}
		public function get TotalValue_Info():CurrencyField				{return Clients_Info.getInstance().TotalValue_Info;}
		public function get Province_Info():ChoiceField					{return Clients_Info.getInstance().Province_Info;}
		public function get RelatedSegment_Info():NumberField			{return Clients_Info.getInstance().RelatedSegment_Info;}
		public function get Segment_Info():TextField					{return Clients_Info.getInstance().Segment_Info;}
		public function get AddDocument_Info():URLField					{return Clients_Info.getInstance().AddDocument_Info;}
		public function get Documents_Info():DbLinkField				{return Clients_Info.getInstance().Documents_Info;}
		public function get AddFacility_Info():URLField					{return Clients_Info.getInstance().AddFacility_Info;}
		public function get Category_Info():TextField					{return Clients_Info.getInstance().Category_Info;}
		public function get BriefName_Info():TextField					{return Clients_Info.getInstance().BriefName_Info;}
		public function get Country_Info():ChoiceField					{return Clients_Info.getInstance().Country_Info;}
		public function get PostalCode_Info():TextField					{return Clients_Info.getInstance().PostalCode_Info;}
		public function get PrimaryContact_Info():TextField				{return Clients_Info.getInstance().PrimaryContact_Info;}
		public function get Position_Info():TextField					{return Clients_Info.getInstance().Position_Info;}
		public function get Phone_Info():PhoneField						{return Clients_Info.getInstance().Phone_Info;}
		public function get Cell_Info():PhoneField						{return Clients_Info.getInstance().Cell_Info;}
		public function get Fax_Info():PhoneField						{return Clients_Info.getInstance().Fax_Info;}
		public function get EMail_Info():EmailField						{return Clients_Info.getInstance().EMail_Info;}
		public function get AdditionalContacts_Info():TextField			{return Clients_Info.getInstance().AdditionalContacts_Info;}
		public function get FiscalYearEnd_Info():TextField				{return Clients_Info.getInstance().FiscalYearEnd_Info;}
		public function get BudgetDeadline_Info():TextField				{return Clients_Info.getInstance().BudgetDeadline_Info;}
		public function get BudgetNotes_Info():TextField				{return Clients_Info.getInstance().BudgetNotes_Info;}
		public function get ClientStandards_Info():TextField			{return Clients_Info.getInstance().ClientStandards_Info;}
		public function get Notes_Info():TextField						{return Clients_Info.getInstance().Notes_Info;}
		public function get Test_Info():URLField						{return Clients_Info.getInstance().Test_Info;}
		public function get Photo_Info():FileField						{return Clients_Info.getInstance().Photo_Info;}
		public function get PhotoThumbnail_Info():TextField				{return Clients_Info.getInstance().PhotoThumbnail_Info;}
		public function get QBMap_Info():URLField						{return Clients_Info.getInstance().QBMap_Info;}
		public function get DateCreated_Info():TimeStampField			{return Clients_Info.getInstance().DateCreated_Info;}
		public function get DateModified_Info():TimeStampField			{return Clients_Info.getInstance().DateModified_Info;}
		public function get RecordOwner_Info():UserIdField				{return Clients_Info.getInstance().RecordOwner_Info;}
		public function get LastModifiedBy_Info():UserIdField			{return Clients_Info.getInstance().LastModifiedBy_Info;}

	}
}
