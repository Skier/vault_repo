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
	import com.ebs.eroof.business.fieldmaps.Inspections_FieldMap;
	import mx.collections.ArrayCollection;

	[Bindable]
	public class Inspections_DTO extends KingussieDTOBase implements IValueObject, IKingussieDTO
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
		public function get InspectionDate():Date						{return _fields["_InspectionDate"];}
		public function get InspectionDate_isDirty():Boolean			{return _fields["_InspectionDate_isDirty"];}
		public function get InspectionType():String						{return _fields["_InspectionType"];}
		public function get InspectorCompany():String					{return _fields["_InspectorCompany"];}
		public function get InspectorName():String						{return _fields["_InspectorName"];}
		public function get InspectorName_isDirty():Boolean				{return _fields["_InspectorName_isDirty"];}
		public function get RelatedSection():Number						{return _fields["_RelatedSection"];}
		public function get RelatedSection_isDirty():Boolean			{return _fields["_RelatedSection_isDirty"];}
		public function get AddPhoto():String							{return _fields["_AddPhoto"];}
		public function get AddPhoto_isDirty():Boolean					{return _fields["_AddPhoto_isDirty"];}
		public function get Photos():String								{return _fields["_Photos"];}
		public function get Photos_isDirty():Boolean					{return _fields["_Photos_isDirty"];}
		public function get DateSort():String							{return _fields["_DateSort"];}
		public function get DateSort_isDirty():Boolean					{return _fields["_DateSort_isDirty"];}
		public function get Weather():String							{return _fields["_Weather"];}
		public function get Weather_isDirty():Boolean					{return _fields["_Weather_isDirty"];}
		public function get CoreTaken():Boolean							{return _fields["_CoreTaken"];}
		public function get CoreTaken_isDirty():Boolean					{return _fields["_CoreTaken_isDirty"];}
		public function get FurtherInvestigation():Boolean				{return _fields["_FurtherInvestigation"];}
		public function get FurtherInvestigation_isDirty():Boolean		{return _fields["_FurtherInvestigation_isDirty"];}
		public function get FurtherInvestigationNotes():String			{return _fields["_FurtherInvestigationNotes"];}
		public function get FurtherInvestigationNotes_isDirty():Boolean	{return _fields["_FurtherInvestigationNotes_isDirty"];}
		public function get OverallRating():String						{return _fields["_OverallRating"];}
		public function get OverallRating_isDirty():Boolean				{return _fields["_OverallRating_isDirty"];}
		public function get Assessment():String							{return _fields["_Assessment"];}
		public function get Assessment_isDirty():Boolean				{return _fields["_Assessment_isDirty"];}
		public function get PhotoSetID():String							{return _fields["_PhotoSetID"];}
		public function get PhotoSetID_isDirty():Boolean				{return _fields["_PhotoSetID_isDirty"];}
		public function get AllowedClientUser():Number					{return _fields["_AllowedClientUser"];}
		public function get AllowedClientUser_isDirty():Boolean			{return _fields["_AllowedClientUser_isDirty"];}
		public function get AllowedFacilityUser():Number				{return _fields["_AllowedFacilityUser"];}
		public function get AllowedFacilityUser_isDirty():Boolean		{return _fields["_AllowedFacilityUser_isDirty"];}
		public function get AllowedSectionUser():Number					{return _fields["_AllowedSectionUser"];}
		public function get AllowedSectionUser_isDirty():Boolean		{return _fields["_AllowedSectionUser_isDirty"];}
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
		public function get InspectionTypeChoices():ArrayCollection		{return InspectionType_Info.choiceArray;}
		public function get InspectorCompanyChoices():ArrayCollection	{return InspectorCompany_Info.choiceArray;}

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
		public function set InspectionDate(val:Date):void				{_fields["_InspectionDate"] = val; _fields["_InspectionDate_isDirty"] = true;}
		public function set InspectionType(val:String):void
		{
			if (InspectionType_Info.validate(val)) {
				_fields["_InspectionType"] = val;
				_fields["_InspectionType_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + InspectionType_Info.label + " value requested: " + val);
			}
		}

		public function set InspectorCompany(val:String):void
		{
			if (InspectorCompany_Info.validate(val)) {
				_fields["_InspectorCompany"] = val;
				_fields["_InspectorCompany_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + InspectorCompany_Info.label + " value requested: " + val);
			}
		}

		public function set InspectorName(val:String):void				{_fields["_InspectorName"] = val; _fields["_InspectorName_isDirty"] = true;}
		public function set RelatedSection(val:Number):void				{_fields["_RelatedSection"] = val; _fields["_RelatedSection_isDirty"] = true;}
		public function set DateSort(val:String):void					{_fields["_DateSort"] = val; _fields["_DateSort_isDirty"] = true;}
		public function set Weather(val:String):void					{_fields["_Weather"] = val; _fields["_Weather_isDirty"] = true;}
		public function set CoreTaken(val:Boolean):void					{_fields["_CoreTaken"] = val; _fields["_CoreTaken_isDirty"] = true;}
		public function set FurtherInvestigation(val:Boolean):void		{_fields["_FurtherInvestigation"] = val; _fields["_FurtherInvestigation_isDirty"] = true;}
		public function set FurtherInvestigationNotes(val:String):void	{_fields["_FurtherInvestigationNotes"] = val; _fields["_FurtherInvestigationNotes_isDirty"] = true;}
		public function set OverallRating(val:String):void				{_fields["_OverallRating"] = val; _fields["_OverallRating_isDirty"] = true;}
		public function set Assessment(val:String):void					{_fields["_Assessment"] = val; _fields["_Assessment_isDirty"] = true;}
		public function set PhotoSetID(val:String):void					{_fields["_PhotoSetID"] = val; _fields["_PhotoSetID_isDirty"] = true;}

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
		private function set IDNInspectionDate(val:String):void			{_fields["_InspectionDate"] = new Date(Number(val));}
		private function set IDNInspectionType(val:String):void			{_fields["_InspectionType"] = val;}
		private function set IDNInspectorCompany(val:String):void		{_fields["_InspectorCompany"] = val;}
		private function set IDNInspectorName(val:String):void			{_fields["_InspectorName"] = val;}
		private function set IDNRelatedSection(val:String):void			{_fields["_RelatedSection"] = Number(val);}
		private function set IDNAddPhoto(val:String):void				{_fields["_AddPhoto"] = val;}
		private function set IDNPhotos(val:String):void					{_fields["_Photos"] = val;}
		private function set IDNDateSort(val:String):void				{_fields["_DateSort"] = val;}
		private function set IDNWeather(val:String):void				{_fields["_Weather"] = val;}
		private function set IDNCoreTaken(val:String):void				{_fields["_CoreTaken"] = Boolean(Number(val));}
		private function set IDNFurtherInvestigation(val:String):void	{_fields["_FurtherInvestigation"] = Boolean(Number(val));}
		private function set IDNFurtherInvestigationNotes(val:String):void{_fields["_FurtherInvestigationNotes"] = val;}
		private function set IDNOverallRating(val:String):void			{_fields["_OverallRating"] = val;}
		private function set IDNAssessment(val:String):void				{_fields["_Assessment"] = val;}
		private function set IDNPhotoSetID(val:String):void				{_fields["_PhotoSetID"] = val;}
		private function set IDNAllowedClientUser(val:String):void		{_fields["_AllowedClientUser"] = Number(val);}
		private function set IDNAllowedFacilityUser(val:String):void	{_fields["_AllowedFacilityUser"] = Number(val);}
		private function set IDNAllowedSectionUser(val:String):void		{_fields["_AllowedSectionUser"] = Number(val);}
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
		public function getInfoObj():IKingussieInfo						{return Inspections_Info.getInstance();}
		public function getFieldMapObj():IQuickBaseFieldMap				{return new Inspections_FieldMap();}
		public function getFieldMapClass():Class						{return Inspections_FieldMap;}
		public function get dtoClass():Class							{return Inspections_DTO;}

		// MetaData Information Objects getters
		public function get Client_Info():TextField						{return Inspections_Info.getInstance().Client_Info;}
		public function get Facility_Info():TextField					{return Inspections_Info.getInstance().Facility_Info;}
		public function get Roof_Info():TextField						{return Inspections_Info.getInstance().Roof_Info;}
		public function get InspectionDate_Info():DateField				{return Inspections_Info.getInstance().InspectionDate_Info;}
		public function get InspectionType_Info():ChoiceField			{return Inspections_Info.getInstance().InspectionType_Info;}
		public function get InspectorCompany_Info():ChoiceField			{return Inspections_Info.getInstance().InspectorCompany_Info;}
		public function get InspectorName_Info():TextField				{return Inspections_Info.getInstance().InspectorName_Info;}
		public function get RelatedSection_Info():NumberField			{return Inspections_Info.getInstance().RelatedSection_Info;}
		public function get AddPhoto_Info():URLField					{return Inspections_Info.getInstance().AddPhoto_Info;}
		public function get Photos_Info():DbLinkField					{return Inspections_Info.getInstance().Photos_Info;}
		public function get DateSort_Info():TextField					{return Inspections_Info.getInstance().DateSort_Info;}
		public function get Weather_Info():TextField					{return Inspections_Info.getInstance().Weather_Info;}
		public function get CoreTaken_Info():BooleanField				{return Inspections_Info.getInstance().CoreTaken_Info;}
		public function get FurtherInvestigation_Info():BooleanField	{return Inspections_Info.getInstance().FurtherInvestigation_Info;}
		public function get FurtherInvestigationNotes_Info():TextField	{return Inspections_Info.getInstance().FurtherInvestigationNotes_Info;}
		public function get OverallRating_Info():TextField				{return Inspections_Info.getInstance().OverallRating_Info;}
		public function get Assessment_Info():TextField					{return Inspections_Info.getInstance().Assessment_Info;}
		public function get PhotoSetID_Info():TextField					{return Inspections_Info.getInstance().PhotoSetID_Info;}
		public function get AllowedClientUser_Info():NumberField		{return Inspections_Info.getInstance().AllowedClientUser_Info;}
		public function get AllowedFacilityUser_Info():NumberField		{return Inspections_Info.getInstance().AllowedFacilityUser_Info;}
		public function get AllowedSectionUser_Info():NumberField		{return Inspections_Info.getInstance().AllowedSectionUser_Info;}
		public function get AllowedUser_Info():NumberField				{return Inspections_Info.getInstance().AllowedUser_Info;}
		public function get DateCreated_Info():TimeStampField			{return Inspections_Info.getInstance().DateCreated_Info;}
		public function get DateModified_Info():TimeStampField			{return Inspections_Info.getInstance().DateModified_Info;}
		public function get RecordOwner_Info():UserIdField				{return Inspections_Info.getInstance().RecordOwner_Info;}
		public function get LastModifiedBy_Info():UserIdField			{return Inspections_Info.getInstance().LastModifiedBy_Info;}

	}
}
