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
	import com.ebs.eroof.business.fieldmaps.Surveys_FieldMap;
	import mx.collections.ArrayCollection;

	[Bindable]
	public class Surveys_DTO extends KingussieDTOBase implements IValueObject, IKingussieDTO
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
		public function get Date():String								{return _fields["_Date"];}
		public function get Date_isDirty():Boolean						{return _fields["_Date_isDirty"];}
		public function get Type():String								{return _fields["_Type"];}
		public function get MembraneCondition():String					{return _fields["_MembraneCondition"];}
		public function get InsulationCondition():String				{return _fields["_InsulationCondition"];}
		public function get RelatedSection():Number						{return _fields["_RelatedSection"];}
		public function get RelatedSection_isDirty():Boolean			{return _fields["_RelatedSection_isDirty"];}
		public function get AddPhoto():String							{return _fields["_AddPhoto"];}
		public function get AddPhoto_isDirty():Boolean					{return _fields["_AddPhoto_isDirty"];}
		public function get Photos():String								{return _fields["_Photos"];}
		public function get Photos_isDirty():Boolean					{return _fields["_Photos_isDirty"];}
		public function get DateSort():Date								{return _fields["_DateSort"];}
		public function get DateSort_isDirty():Boolean					{return _fields["_DateSort_isDirty"];}
		public function get InfraredPhotoID():String					{return _fields["_InfraredPhotoID"];}
		public function get InfraredPhotoID_isDirty():Boolean			{return _fields["_InfraredPhotoID_isDirty"];}
		public function get MatchingPhotoID():String					{return _fields["_MatchingPhotoID"];}
		public function get MatchingPhotoID_isDirty():Boolean			{return _fields["_MatchingPhotoID_isDirty"];}
		public function get Details():String							{return _fields["_Details"];}
		public function get Details_isDirty():Boolean					{return _fields["_Details_isDirty"];}
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
		public function get TypeChoices():ArrayCollection				{return Type_Info.choiceArray;}
		public function get MembraneConditionChoices():ArrayCollection	{return MembraneCondition_Info.choiceArray;}
		public function get InsulationConditionChoices():ArrayCollection{return InsulationCondition_Info.choiceArray;}

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
		public function set Date(val:String):void						{_fields["_Date"] = val; _fields["_Date_isDirty"] = true;}
		public function set Type(val:String):void
		{
			if (Type_Info.validate(val)) {
				_fields["_Type"] = val;
				_fields["_Type_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + Type_Info.label + " value requested: " + val);
			}
		}

		public function set MembraneCondition(val:String):void
		{
			if (MembraneCondition_Info.validate(val)) {
				_fields["_MembraneCondition"] = val;
				_fields["_MembraneCondition_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + MembraneCondition_Info.label + " value requested: " + val);
			}
		}

		public function set InsulationCondition(val:String):void
		{
			if (InsulationCondition_Info.validate(val)) {
				_fields["_InsulationCondition"] = val;
				_fields["_InsulationCondition_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + InsulationCondition_Info.label + " value requested: " + val);
			}
		}

		public function set RelatedSection(val:Number):void				{_fields["_RelatedSection"] = val; _fields["_RelatedSection_isDirty"] = true;}
		public function set DateSort(val:Date):void						{_fields["_DateSort"] = val; _fields["_DateSort_isDirty"] = true;}
		public function set InfraredPhotoID(val:String):void			{_fields["_InfraredPhotoID"] = val; _fields["_InfraredPhotoID_isDirty"] = true;}
		public function set MatchingPhotoID(val:String):void			{_fields["_MatchingPhotoID"] = val; _fields["_MatchingPhotoID_isDirty"] = true;}
		public function set Details(val:String):void					{_fields["_Details"] = val; _fields["_Details_isDirty"] = true;}

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
		private function set IDNDate(val:String):void					{_fields["_Date"] = val;}
		private function set IDNType(val:String):void					{_fields["_Type"] = val;}
		private function set IDNMembraneCondition(val:String):void		{_fields["_MembraneCondition"] = val;}
		private function set IDNInsulationCondition(val:String):void	{_fields["_InsulationCondition"] = val;}
		private function set IDNRelatedSection(val:String):void			{_fields["_RelatedSection"] = Number(val);}
		private function set IDNAddPhoto(val:String):void				{_fields["_AddPhoto"] = val;}
		private function set IDNPhotos(val:String):void					{_fields["_Photos"] = val;}
		private function set IDNDateSort(val:String):void				{_fields["_DateSort"] = new Date(Number(val));}
		private function set IDNInfraredPhotoID(val:String):void		{_fields["_InfraredPhotoID"] = val;}
		private function set IDNMatchingPhotoID(val:String):void		{_fields["_MatchingPhotoID"] = val;}
		private function set IDNDetails(val:String):void				{_fields["_Details"] = val;}
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
		public function getInfoObj():IKingussieInfo						{return Surveys_Info.getInstance();}
		public function getFieldMapObj():IQuickBaseFieldMap				{return new Surveys_FieldMap();}
		public function getFieldMapClass():Class						{return Surveys_FieldMap;}
		public function get dtoClass():Class							{return Surveys_DTO;}

		// MetaData Information Objects getters
		public function get Client_Info():TextField						{return Surveys_Info.getInstance().Client_Info;}
		public function get Facility_Info():TextField					{return Surveys_Info.getInstance().Facility_Info;}
		public function get Roof_Info():TextField						{return Surveys_Info.getInstance().Roof_Info;}
		public function get Date_Info():TextField						{return Surveys_Info.getInstance().Date_Info;}
		public function get Type_Info():ChoiceField						{return Surveys_Info.getInstance().Type_Info;}
		public function get MembraneCondition_Info():ChoiceField		{return Surveys_Info.getInstance().MembraneCondition_Info;}
		public function get InsulationCondition_Info():ChoiceField		{return Surveys_Info.getInstance().InsulationCondition_Info;}
		public function get RelatedSection_Info():NumberField			{return Surveys_Info.getInstance().RelatedSection_Info;}
		public function get AddPhoto_Info():URLField					{return Surveys_Info.getInstance().AddPhoto_Info;}
		public function get Photos_Info():DbLinkField					{return Surveys_Info.getInstance().Photos_Info;}
		public function get DateSort_Info():DateField					{return Surveys_Info.getInstance().DateSort_Info;}
		public function get InfraredPhotoID_Info():TextField			{return Surveys_Info.getInstance().InfraredPhotoID_Info;}
		public function get MatchingPhotoID_Info():TextField			{return Surveys_Info.getInstance().MatchingPhotoID_Info;}
		public function get Details_Info():TextField					{return Surveys_Info.getInstance().Details_Info;}
		public function get AllowedClientUser_Info():NumberField		{return Surveys_Info.getInstance().AllowedClientUser_Info;}
		public function get AllowedFacilityUser_Info():NumberField		{return Surveys_Info.getInstance().AllowedFacilityUser_Info;}
		public function get AllowedSectionUser_Info():NumberField		{return Surveys_Info.getInstance().AllowedSectionUser_Info;}
		public function get AllowedUser_Info():NumberField				{return Surveys_Info.getInstance().AllowedUser_Info;}
		public function get DateCreated_Info():TimeStampField			{return Surveys_Info.getInstance().DateCreated_Info;}
		public function get DateModified_Info():TimeStampField			{return Surveys_Info.getInstance().DateModified_Info;}
		public function get RecordOwner_Info():UserIdField				{return Surveys_Info.getInstance().RecordOwner_Info;}
		public function get LastModifiedBy_Info():UserIdField			{return Surveys_Info.getInstance().LastModifiedBy_Info;}

	}
}
