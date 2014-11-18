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
	import com.ebs.eroof.business.fieldmaps.Facilities_FieldMap;
	import mx.collections.ArrayCollection;

	[Bindable]
	public class Facilities_DTO extends KingussieDTOBase implements IValueObject, IKingussieDTO
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		// Record Owner Dirty Bit
		public function get IDNIsRecordOwnerDirty():Boolean				{return _fields["_isRecordOwnerDirty"];}


		// Current value getters
		public function get rid():String								{return _fields["_rid"];}
		public function get AddressMap():String							{return _fields["_AddressMap"];}
		public function get AddressMap_isDirty():Boolean				{return _fields["_AddressMap_isDirty"];}
		public function get SiteMap():String							{return _fields["_SiteMap"];}
		public function get SiteMap_isDirty():Boolean					{return _fields["_SiteMap_isDirty"];}
		public function get Client():String								{return _fields["_Client"];}
		public function get Client_isDirty():Boolean					{return _fields["_Client_isDirty"];}
		public function get FacilityName():String						{return _fields["_FacilityName"];}
		public function get FacilityName_isDirty():Boolean				{return _fields["_FacilityName_isDirty"];}
		public function get Address():String							{return _fields["_Address"];}
		public function get Address_isDirty():Boolean					{return _fields["_Address_isDirty"];}
		public function get City():String								{return _fields["_City"];}
		public function get City_isDirty():Boolean						{return _fields["_City_isDirty"];}
		public function get SectionsCount():Number						{return _fields["_SectionsCount"];}
		public function get SectionsCount_isDirty():Boolean				{return _fields["_SectionsCount_isDirty"];}
		public function get TotalSqFt():Number							{return _fields["_TotalSqFt"];}
		public function get TotalSqFt_isDirty():Boolean					{return _fields["_TotalSqFt_isDirty"];}
		public function get TotalValue():Number							{return _fields["_TotalValue"];}
		public function get TotalValue_isDirty():Boolean				{return _fields["_TotalValue_isDirty"];}
		public function get Sections():String							{return _fields["_Sections"];}
		public function get Sections_isDirty():Boolean					{return _fields["_Sections_isDirty"];}
		public function get RelatedClient():Number						{return _fields["_RelatedClient"];}
		public function get RelatedClient_isDirty():Boolean				{return _fields["_RelatedClient_isDirty"];}
		public function get AddSection():String							{return _fields["_AddSection"];}
		public function get AddSection_isDirty():Boolean				{return _fields["_AddSection_isDirty"];}
		public function get AddDocument():String						{return _fields["_AddDocument"];}
		public function get AddDocument_isDirty():Boolean				{return _fields["_AddDocument_isDirty"];}
		public function get Documents():String							{return _fields["_Documents"];}
		public function get Documents_isDirty():Boolean					{return _fields["_Documents_isDirty"];}
		public function get BriefName():String							{return _fields["_BriefName"];}
		public function get BriefName_isDirty():Boolean					{return _fields["_BriefName_isDirty"];}
		public function get Photo():QuickBaseFileDTO {
			if(_fields["_Photo"] == null) {
				_fields["_Photo"] = new QuickBaseFileDTO();
			}
			return _fields["_Photo"];
		}
		public function get PhotoName():String							{return _fields["_PhotoName"];}
		public function get PhotoName_isDirty():Boolean					{return _fields["_PhotoName_isDirty"];}
		public function get Province():String							{return _fields["_Province"];}
		public function get Country():String							{return _fields["_Country"];}
		public function get PostalCode():String							{return _fields["_PostalCode"];}
		public function get PostalCode_isDirty():Boolean				{return _fields["_PostalCode_isDirty"];}
		public function get TypeOfBuilding():String						{return _fields["_TypeOfBuilding"];}
		public function get Neighbourhood():String						{return _fields["_Neighbourhood"];}
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
		public function get FacilityStandards():String					{return _fields["_FacilityStandards"];}
		public function get FacilityStandards_isDirty():Boolean			{return _fields["_FacilityStandards_isDirty"];}
		public function get Leaking():Boolean							{return _fields["_Leaking"];}
		public function get Leaking_isDirty():Boolean					{return _fields["_Leaking_isDirty"];}
		public function get Keyplan():QuickBaseFileDTO {
			if(_fields["_Keyplan"] == null) {
				_fields["_Keyplan"] = new QuickBaseFileDTO();
			}
			return _fields["_Keyplan"];
		}
		public function get KeyplanName():String						{return _fields["_KeyplanName"];}
		public function get KeyplanName_isDirty():Boolean				{return _fields["_KeyplanName_isDirty"];}
		public function get X1():Number									{return _fields["_X1"];}
		public function get X1_isDirty():Boolean						{return _fields["_X1_isDirty"];}
		public function get Y1():Number									{return _fields["_Y1"];}
		public function get Y1_isDirty():Boolean						{return _fields["_Y1_isDirty"];}
		public function get X2():Number									{return _fields["_X2"];}
		public function get X2_isDirty():Boolean						{return _fields["_X2_isDirty"];}
		public function get Y2():Number									{return _fields["_Y2"];}
		public function get Y2_isDirty():Boolean						{return _fields["_Y2_isDirty"];}
		public function get Drawing():QuickBaseFileDTO {
			if(_fields["_Drawing"] == null) {
				_fields["_Drawing"] = new QuickBaseFileDTO();
			}
			return _fields["_Drawing"];
		}
		public function get DrawingName():String						{return _fields["_DrawingName"];}
		public function get DrawingName_isDirty():Boolean				{return _fields["_DrawingName_isDirty"];}
		public function get Notes():String								{return _fields["_Notes"];}
		public function get Notes_isDirty():Boolean						{return _fields["_Notes_isDirty"];}
		public function get PhotoThumbnail():String						{return _fields["_PhotoThumbnail"];}
		public function get PhotoThumbnail_isDirty():Boolean			{return _fields["_PhotoThumbnail_isDirty"];}
		public function get KeyplanThumbnail():String					{return _fields["_KeyplanThumbnail"];}
		public function get KeyplanThumbnail_isDirty():Boolean			{return _fields["_KeyplanThumbnail_isDirty"];}
		public function get AllowedClientUser():Number					{return _fields["_AllowedClientUser"];}
		public function get AllowedClientUser_isDirty():Boolean			{return _fields["_AllowedClientUser_isDirty"];}
		public function get AllowedUser():Number						{return _fields["_AllowedUser"];}
		public function get AllowedUser_isDirty():Boolean				{return _fields["_AllowedUser_isDirty"];}
		public function get OldFacilityid():Number						{return _fields["_OldFacilityid"];}
		public function get OldFacilityid_isDirty():Boolean				{return _fields["_OldFacilityid_isDirty"];}
		public function get AddressLatlong():String						{return _fields["_AddressLatlong"];}
		public function get AddressLatlong_isDirty():Boolean			{return _fields["_AddressLatlong_isDirty"];}
		public function get AddressZoom():Number						{return _fields["_AddressZoom"];}
		public function get AddressZoom_isDirty():Boolean				{return _fields["_AddressZoom_isDirty"];}
		public function get SitePlanZoom():Number						{return _fields["_SitePlanZoom"];}
		public function get SitePlanZoom_isDirty():Boolean				{return _fields["_SitePlanZoom_isDirty"];}
		public function get DrawingThumbnail():String					{return _fields["_DrawingThumbnail"];}
		public function get DrawingThumbnail_isDirty():Boolean			{return _fields["_DrawingThumbnail_isDirty"];}
		public function get AddressLat():Number							{return _fields["_AddressLat"];}
		public function get AddressLat_isDirty():Boolean				{return _fields["_AddressLat_isDirty"];}
		public function get AddressLong():Number						{return _fields["_AddressLong"];}
		public function get AddressLong_isDirty():Boolean				{return _fields["_AddressLong_isDirty"];}
		public function get IRPCoverPagePhotoFileName():String			{return _fields["_IRPCoverPagePhotoFileName"];}
		public function get IRPCoverPagePhotoFileName_isDirty():Boolean	{return _fields["_IRPCoverPagePhotoFileName_isDirty"];}
		public function get IRPKeyPlanFileName():String					{return _fields["_IRPKeyPlanFileName"];}
		public function get IRPKeyPlanFileName_isDirty():Boolean		{return _fields["_IRPKeyPlanFileName_isDirty"];}
		public function get IRPDrawingFileName():String					{return _fields["_IRPDrawingFileName"];}
		public function get IRPDrawingFileName_isDirty():Boolean		{return _fields["_IRPDrawingFileName_isDirty"];}
		public function get QBMapsUrl():String							{return _fields["_QBMapsUrl"];}
		public function get QBMapsUrl_isDirty():Boolean					{return _fields["_QBMapsUrl_isDirty"];}
		public function get QBMapsMarker():String						{return _fields["_QBMapsMarker"];}
		public function get QBMapsMarker_isDirty():Boolean				{return _fields["_QBMapsMarker_isDirty"];}
		public function get QBAddress():String							{return _fields["_QBAddress"];}
		public function get QBAddress_isDirty():Boolean					{return _fields["_QBAddress_isDirty"];}
		public function get SitePlan():String							{return _fields["_SitePlan"];}
		public function get SitePlan_isDirty():Boolean					{return _fields["_SitePlan_isDirty"];}
		public function get Map2():String								{return _fields["_Map2"];}
		public function get Map2_isDirty():Boolean						{return _fields["_Map2_isDirty"];}
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
		public function get TypeOfBuildingChoices():ArrayCollection		{return TypeOfBuilding_Info.choiceArray;}
		public function get NeighbourhoodChoices():ArrayCollection		{return Neighbourhood_Info.choiceArray;}

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
		public function set FacilityName(val:String):void				{_fields["_FacilityName"] = val; _fields["_FacilityName_isDirty"] = true;}
		public function set Address(val:String):void					{_fields["_Address"] = val; _fields["_Address_isDirty"] = true;}
		public function set City(val:String):void						{_fields["_City"] = val; _fields["_City_isDirty"] = true;}
		public function set RelatedClient(val:Number):void				{_fields["_RelatedClient"] = val; _fields["_RelatedClient_isDirty"] = true;}
		public function set BriefName(val:String):void					{_fields["_BriefName"] = val; _fields["_BriefName_isDirty"] = true;}
		public function set PhotoName(val:String):void					{_fields["_PhotoName"] = val; _fields["_PhotoName_isDirty"] = true;}
		public function set Province(val:String):void
		{
			if (Province_Info.validate(val)) {
				_fields["_Province"] = val;
				_fields["_Province_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + Province_Info.label + " value requested: " + val);
			}
		}

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
		public function set TypeOfBuilding(val:String):void
		{
			if (TypeOfBuilding_Info.validate(val)) {
				_fields["_TypeOfBuilding"] = val;
				_fields["_TypeOfBuilding_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + TypeOfBuilding_Info.label + " value requested: " + val);
			}
		}

		public function set Neighbourhood(val:String):void
		{
			if (Neighbourhood_Info.validate(val)) {
				_fields["_Neighbourhood"] = val;
				_fields["_Neighbourhood_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + Neighbourhood_Info.label + " value requested: " + val);
			}
		}

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
		public function set FacilityStandards(val:String):void			{_fields["_FacilityStandards"] = val; _fields["_FacilityStandards_isDirty"] = true;}
		public function set Leaking(val:Boolean):void					{_fields["_Leaking"] = val; _fields["_Leaking_isDirty"] = true;}
		public function set KeyplanName(val:String):void				{_fields["_KeyplanName"] = val; _fields["_KeyplanName_isDirty"] = true;}
		public function set X1(val:Number):void							{_fields["_X1"] = val; _fields["_X1_isDirty"] = true;}
		public function set Y1(val:Number):void							{_fields["_Y1"] = val; _fields["_Y1_isDirty"] = true;}
		public function set X2(val:Number):void							{_fields["_X2"] = val; _fields["_X2_isDirty"] = true;}
		public function set Y2(val:Number):void							{_fields["_Y2"] = val; _fields["_Y2_isDirty"] = true;}
		public function set DrawingName(val:String):void				{_fields["_DrawingName"] = val; _fields["_DrawingName_isDirty"] = true;}
		public function set Notes(val:String):void						{_fields["_Notes"] = val; _fields["_Notes_isDirty"] = true;}
		public function set OldFacilityid(val:Number):void				{_fields["_OldFacilityid"] = val; _fields["_OldFacilityid_isDirty"] = true;}
		public function set AddressLatlong(val:String):void				{_fields["_AddressLatlong"] = val; _fields["_AddressLatlong_isDirty"] = true;}
		public function set AddressZoom(val:Number):void				{_fields["_AddressZoom"] = val; _fields["_AddressZoom_isDirty"] = true;}
		public function set SitePlanZoom(val:Number):void				{_fields["_SitePlanZoom"] = val; _fields["_SitePlanZoom_isDirty"] = true;}
		public function set AddressLat(val:Number):void					{_fields["_AddressLat"] = val; _fields["_AddressLat_isDirty"] = true;}
		public function set AddressLong(val:Number):void				{_fields["_AddressLong"] = val; _fields["_AddressLong_isDirty"] = true;}
		public function set IRPCoverPagePhotoFileName(val:String):void	{_fields["_IRPCoverPagePhotoFileName"] = val; _fields["_IRPCoverPagePhotoFileName_isDirty"] = true;}
		public function set IRPKeyPlanFileName(val:String):void			{_fields["_IRPKeyPlanFileName"] = val; _fields["_IRPKeyPlanFileName_isDirty"] = true;}
		public function set IRPDrawingFileName(val:String):void			{_fields["_IRPDrawingFileName"] = val; _fields["_IRPDrawingFileName_isDirty"] = true;}
		public function set QBAddress(val:String):void					{_fields["_QBAddress"] = val; _fields["_QBAddress_isDirty"] = true;}

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
		private function set IDNAddressMap(val:String):void				{_fields["_AddressMap"] = val;}
		private function set IDNSiteMap(val:String):void				{_fields["_SiteMap"] = val;}
		private function set IDNClient(val:String):void					{_fields["_Client"] = val;}
		private function set IDNFacilityName(val:String):void			{_fields["_FacilityName"] = val;}
		private function set IDNAddress(val:String):void				{_fields["_Address"] = val;}
		private function set IDNCity(val:String):void					{_fields["_City"] = val;}
		private function set IDNSectionsCount(val:String):void			{_fields["_SectionsCount"] = Number(val);}
		private function set IDNTotalSqFt(val:String):void				{_fields["_TotalSqFt"] = Number(val);}
		private function set IDNTotalValue(val:String):void				{_fields["_TotalValue"] = Number(val);}
		private function set IDNSections(val:String):void				{_fields["_Sections"] = val;}
		private function set IDNRelatedClient(val:String):void			{_fields["_RelatedClient"] = Number(val);}
		private function set IDNAddSection(val:String):void				{_fields["_AddSection"] = val;}
		private function set IDNAddDocument(val:String):void			{_fields["_AddDocument"] = val;}
		private function set IDNDocuments(val:String):void				{_fields["_Documents"] = val;}
		private function set IDNBriefName(val:String):void				{_fields["_BriefName"] = val;}
		private function set IDNPhoto(val:String):void {
			if(_fields["_Photo"] == null) {
				_fields["_Photo"] = new QuickBaseFileDTO();
			}
			_fields["_Photo"].url = val;
		}
		private function set IDNPhotoName(val:String):void				{_fields["_PhotoName"] = val;}
		private function set IDNProvince(val:String):void				{_fields["_Province"] = val;}
		private function set IDNCountry(val:String):void				{_fields["_Country"] = val;}
		private function set IDNPostalCode(val:String):void				{_fields["_PostalCode"] = val;}
		private function set IDNTypeOfBuilding(val:String):void			{_fields["_TypeOfBuilding"] = val;}
		private function set IDNNeighbourhood(val:String):void			{_fields["_Neighbourhood"] = val;}
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
		private function set IDNFacilityStandards(val:String):void		{_fields["_FacilityStandards"] = val;}
		private function set IDNLeaking(val:String):void				{_fields["_Leaking"] = Boolean(Number(val));}
		private function set IDNKeyplan(val:String):void {
			if(_fields["_Keyplan"] == null) {
				_fields["_Keyplan"] = new QuickBaseFileDTO();
			}
			_fields["_Keyplan"].url = val;
		}
		private function set IDNKeyplanName(val:String):void			{_fields["_KeyplanName"] = val;}
		private function set IDNX1(val:String):void						{_fields["_X1"] = Number(val);}
		private function set IDNY1(val:String):void						{_fields["_Y1"] = Number(val);}
		private function set IDNX2(val:String):void						{_fields["_X2"] = Number(val);}
		private function set IDNY2(val:String):void						{_fields["_Y2"] = Number(val);}
		private function set IDNDrawing(val:String):void {
			if(_fields["_Drawing"] == null) {
				_fields["_Drawing"] = new QuickBaseFileDTO();
			}
			_fields["_Drawing"].url = val;
		}
		private function set IDNDrawingName(val:String):void			{_fields["_DrawingName"] = val;}
		private function set IDNNotes(val:String):void					{_fields["_Notes"] = val;}
		private function set IDNPhotoThumbnail(val:String):void			{_fields["_PhotoThumbnail"] = val;}
		private function set IDNKeyplanThumbnail(val:String):void		{_fields["_KeyplanThumbnail"] = val;}
		private function set IDNAllowedClientUser(val:String):void		{_fields["_AllowedClientUser"] = Number(val);}
		private function set IDNAllowedUser(val:String):void			{_fields["_AllowedUser"] = Number(val);}
		private function set IDNOldFacilityid(val:String):void			{_fields["_OldFacilityid"] = Number(val);}
		private function set IDNAddressLatlong(val:String):void			{_fields["_AddressLatlong"] = val;}
		private function set IDNAddressZoom(val:String):void			{_fields["_AddressZoom"] = Number(val);}
		private function set IDNSitePlanZoom(val:String):void			{_fields["_SitePlanZoom"] = Number(val);}
		private function set IDNDrawingThumbnail(val:String):void		{_fields["_DrawingThumbnail"] = val;}
		private function set IDNAddressLat(val:String):void				{_fields["_AddressLat"] = Number(val);}
		private function set IDNAddressLong(val:String):void			{_fields["_AddressLong"] = Number(val);}
		private function set IDNIRPCoverPagePhotoFileName(val:String):void{_fields["_IRPCoverPagePhotoFileName"] = val;}
		private function set IDNIRPKeyPlanFileName(val:String):void		{_fields["_IRPKeyPlanFileName"] = val;}
		private function set IDNIRPDrawingFileName(val:String):void		{_fields["_IRPDrawingFileName"] = val;}
		private function set IDNQBMapsUrl(val:String):void				{_fields["_QBMapsUrl"] = val;}
		private function set IDNQBMapsMarker(val:String):void			{_fields["_QBMapsMarker"] = val;}
		private function set IDNQBAddress(val:String):void				{_fields["_QBAddress"] = val;}
		private function set IDNSitePlan(val:String):void				{_fields["_SitePlan"] = val;}
		private function set IDNMap2(val:String):void					{_fields["_Map2"] = val;}
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
		public function getInfoObj():IKingussieInfo						{return Facilities_Info.getInstance();}
		public function getFieldMapObj():IQuickBaseFieldMap				{return new Facilities_FieldMap();}
		public function getFieldMapClass():Class						{return Facilities_FieldMap;}
		public function get dtoClass():Class							{return Facilities_DTO;}

		// MetaData Information Objects getters
		public function get AddressMap_Info():URLField					{return Facilities_Info.getInstance().AddressMap_Info;}
		public function get SiteMap_Info():URLField						{return Facilities_Info.getInstance().SiteMap_Info;}
		public function get Client_Info():TextField						{return Facilities_Info.getInstance().Client_Info;}
		public function get FacilityName_Info():TextField				{return Facilities_Info.getInstance().FacilityName_Info;}
		public function get Address_Info():TextField					{return Facilities_Info.getInstance().Address_Info;}
		public function get City_Info():TextField						{return Facilities_Info.getInstance().City_Info;}
		public function get SectionsCount_Info():NumberField			{return Facilities_Info.getInstance().SectionsCount_Info;}
		public function get TotalSqFt_Info():NumberField				{return Facilities_Info.getInstance().TotalSqFt_Info;}
		public function get TotalValue_Info():CurrencyField				{return Facilities_Info.getInstance().TotalValue_Info;}
		public function get Sections_Info():URLField					{return Facilities_Info.getInstance().Sections_Info;}
		public function get RelatedClient_Info():NumberField			{return Facilities_Info.getInstance().RelatedClient_Info;}
		public function get AddSection_Info():URLField					{return Facilities_Info.getInstance().AddSection_Info;}
		public function get AddDocument_Info():URLField					{return Facilities_Info.getInstance().AddDocument_Info;}
		public function get Documents_Info():DbLinkField				{return Facilities_Info.getInstance().Documents_Info;}
		public function get BriefName_Info():TextField					{return Facilities_Info.getInstance().BriefName_Info;}
		public function get Photo_Info():FileField						{return Facilities_Info.getInstance().Photo_Info;}
		public function get PhotoName_Info():TextField					{return Facilities_Info.getInstance().PhotoName_Info;}
		public function get Province_Info():ChoiceField					{return Facilities_Info.getInstance().Province_Info;}
		public function get Country_Info():ChoiceField					{return Facilities_Info.getInstance().Country_Info;}
		public function get PostalCode_Info():TextField					{return Facilities_Info.getInstance().PostalCode_Info;}
		public function get TypeOfBuilding_Info():ChoiceField			{return Facilities_Info.getInstance().TypeOfBuilding_Info;}
		public function get Neighbourhood_Info():ChoiceField			{return Facilities_Info.getInstance().Neighbourhood_Info;}
		public function get PrimaryContact_Info():TextField				{return Facilities_Info.getInstance().PrimaryContact_Info;}
		public function get Position_Info():TextField					{return Facilities_Info.getInstance().Position_Info;}
		public function get Phone_Info():PhoneField						{return Facilities_Info.getInstance().Phone_Info;}
		public function get Cell_Info():PhoneField						{return Facilities_Info.getInstance().Cell_Info;}
		public function get Fax_Info():PhoneField						{return Facilities_Info.getInstance().Fax_Info;}
		public function get EMail_Info():EmailField						{return Facilities_Info.getInstance().EMail_Info;}
		public function get AdditionalContacts_Info():TextField			{return Facilities_Info.getInstance().AdditionalContacts_Info;}
		public function get FiscalYearEnd_Info():TextField				{return Facilities_Info.getInstance().FiscalYearEnd_Info;}
		public function get BudgetDeadline_Info():TextField				{return Facilities_Info.getInstance().BudgetDeadline_Info;}
		public function get BudgetNotes_Info():TextField				{return Facilities_Info.getInstance().BudgetNotes_Info;}
		public function get FacilityStandards_Info():TextField			{return Facilities_Info.getInstance().FacilityStandards_Info;}
		public function get Leaking_Info():BooleanField					{return Facilities_Info.getInstance().Leaking_Info;}
		public function get Keyplan_Info():FileField					{return Facilities_Info.getInstance().Keyplan_Info;}
		public function get KeyplanName_Info():TextField				{return Facilities_Info.getInstance().KeyplanName_Info;}
		public function get X1_Info():NumberField						{return Facilities_Info.getInstance().X1_Info;}
		public function get Y1_Info():NumberField						{return Facilities_Info.getInstance().Y1_Info;}
		public function get X2_Info():NumberField						{return Facilities_Info.getInstance().X2_Info;}
		public function get Y2_Info():NumberField						{return Facilities_Info.getInstance().Y2_Info;}
		public function get Drawing_Info():FileField					{return Facilities_Info.getInstance().Drawing_Info;}
		public function get DrawingName_Info():TextField				{return Facilities_Info.getInstance().DrawingName_Info;}
		public function get Notes_Info():TextField						{return Facilities_Info.getInstance().Notes_Info;}
		public function get PhotoThumbnail_Info():TextField				{return Facilities_Info.getInstance().PhotoThumbnail_Info;}
		public function get KeyplanThumbnail_Info():TextField			{return Facilities_Info.getInstance().KeyplanThumbnail_Info;}
		public function get AllowedClientUser_Info():NumberField		{return Facilities_Info.getInstance().AllowedClientUser_Info;}
		public function get AllowedUser_Info():NumberField				{return Facilities_Info.getInstance().AllowedUser_Info;}
		public function get OldFacilityid_Info():NumberField			{return Facilities_Info.getInstance().OldFacilityid_Info;}
		public function get AddressLatlong_Info():TextField				{return Facilities_Info.getInstance().AddressLatlong_Info;}
		public function get AddressZoom_Info():NumberField				{return Facilities_Info.getInstance().AddressZoom_Info;}
		public function get SitePlanZoom_Info():NumberField				{return Facilities_Info.getInstance().SitePlanZoom_Info;}
		public function get DrawingThumbnail_Info():TextField			{return Facilities_Info.getInstance().DrawingThumbnail_Info;}
		public function get AddressLat_Info():NumberField				{return Facilities_Info.getInstance().AddressLat_Info;}
		public function get AddressLong_Info():NumberField				{return Facilities_Info.getInstance().AddressLong_Info;}
		public function get IRPCoverPagePhotoFileName_Info():TextField	{return Facilities_Info.getInstance().IRPCoverPagePhotoFileName_Info;}
		public function get IRPKeyPlanFileName_Info():TextField			{return Facilities_Info.getInstance().IRPKeyPlanFileName_Info;}
		public function get IRPDrawingFileName_Info():TextField			{return Facilities_Info.getInstance().IRPDrawingFileName_Info;}
		public function get QBMapsUrl_Info():TextField					{return Facilities_Info.getInstance().QBMapsUrl_Info;}
		public function get QBMapsMarker_Info():TextField				{return Facilities_Info.getInstance().QBMapsMarker_Info;}
		public function get QBAddress_Info():TextField					{return Facilities_Info.getInstance().QBAddress_Info;}
		public function get SitePlan_Info():URLField					{return Facilities_Info.getInstance().SitePlan_Info;}
		public function get Map2_Info():TextField						{return Facilities_Info.getInstance().Map2_Info;}
		public function get DateCreated_Info():TimeStampField			{return Facilities_Info.getInstance().DateCreated_Info;}
		public function get DateModified_Info():TimeStampField			{return Facilities_Info.getInstance().DateModified_Info;}
		public function get RecordOwner_Info():UserIdField				{return Facilities_Info.getInstance().RecordOwner_Info;}
		public function get LastModifiedBy_Info():UserIdField			{return Facilities_Info.getInstance().LastModifiedBy_Info;}

	}
}
