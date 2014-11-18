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
	import com.ebs.eroof.business.fieldmaps.Details_FieldMap;
	import mx.collections.ArrayCollection;

	[Bindable]
	public class Details_DTO extends KingussieDTOBase implements IValueObject, IKingussieDTO
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
		public function get DetailType():String							{return _fields["_DetailType"];}
		public function get Description():String						{return _fields["_Description"];}
		public function get FlashingMembrane():String					{return _fields["_FlashingMembrane"];}
		public function get FlashingMetal():String						{return _fields["_FlashingMetal"];}
		public function get RelatedSection():Number						{return _fields["_RelatedSection"];}
		public function get RelatedSection_isDirty():Boolean			{return _fields["_RelatedSection_isDirty"];}
		public function get AddPhoto():String							{return _fields["_AddPhoto"];}
		public function get AddPhoto_isDirty():Boolean					{return _fields["_AddPhoto_isDirty"];}
		public function get Photos():String								{return _fields["_Photos"];}
		public function get Photos_isDirty():Boolean					{return _fields["_Photos_isDirty"];}
		public function get Photo():QuickBaseFileDTO {
			if(_fields["_Photo"] == null) {
				_fields["_Photo"] = new QuickBaseFileDTO();
			}
			return _fields["_Photo"];
		}
		public function get PhotoName():String							{return _fields["_PhotoName"];}
		public function get PhotoName_isDirty():Boolean					{return _fields["_PhotoName_isDirty"];}
		public function get Condition():String							{return _fields["_Condition"];}
		public function get Condition_isDirty():Boolean					{return _fields["_Condition_isDirty"];}
		public function get AllowedSectionUser():Number					{return _fields["_AllowedSectionUser"];}
		public function get AllowedSectionUser_isDirty():Boolean		{return _fields["_AllowedSectionUser_isDirty"];}
		public function get AllowedFacilityUser():Number				{return _fields["_AllowedFacilityUser"];}
		public function get AllowedFacilityUser_isDirty():Boolean		{return _fields["_AllowedFacilityUser_isDirty"];}
		public function get AllowedClientUser():Number					{return _fields["_AllowedClientUser"];}
		public function get AllowedClientUser_isDirty():Boolean			{return _fields["_AllowedClientUser_isDirty"];}
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
		public function get DetailTypeChoices():ArrayCollection			{return DetailType_Info.choiceArray;}
		public function get DescriptionChoices():ArrayCollection		{return Description_Info.choiceArray;}
		public function get FlashingMembraneChoices():ArrayCollection	{return FlashingMembrane_Info.choiceArray;}
		public function get FlashingMetalChoices():ArrayCollection		{return FlashingMetal_Info.choiceArray;}

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
		public function set DetailType(val:String):void
		{
			if (DetailType_Info.validate(val)) {
				_fields["_DetailType"] = val;
				_fields["_DetailType_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + DetailType_Info.label + " value requested: " + val);
			}
		}

		public function set Description(val:String):void
		{
			if (Description_Info.validate(val)) {
				_fields["_Description"] = val;
				_fields["_Description_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + Description_Info.label + " value requested: " + val);
			}
		}

		public function set FlashingMembrane(val:String):void
		{
			if (FlashingMembrane_Info.validate(val)) {
				_fields["_FlashingMembrane"] = val;
				_fields["_FlashingMembrane_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + FlashingMembrane_Info.label + " value requested: " + val);
			}
		}

		public function set FlashingMetal(val:String):void
		{
			if (FlashingMetal_Info.validate(val)) {
				_fields["_FlashingMetal"] = val;
				_fields["_FlashingMetal_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + FlashingMetal_Info.label + " value requested: " + val);
			}
		}

		public function set RelatedSection(val:Number):void				{_fields["_RelatedSection"] = val; _fields["_RelatedSection_isDirty"] = true;}
		public function set PhotoName(val:String):void					{_fields["_PhotoName"] = val; _fields["_PhotoName_isDirty"] = true;}
		public function set Condition(val:String):void					{_fields["_Condition"] = val; _fields["_Condition_isDirty"] = true;}

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
		private function set IDNDetailType(val:String):void				{_fields["_DetailType"] = val;}
		private function set IDNDescription(val:String):void			{_fields["_Description"] = val;}
		private function set IDNFlashingMembrane(val:String):void		{_fields["_FlashingMembrane"] = val;}
		private function set IDNFlashingMetal(val:String):void			{_fields["_FlashingMetal"] = val;}
		private function set IDNRelatedSection(val:String):void			{_fields["_RelatedSection"] = Number(val);}
		private function set IDNAddPhoto(val:String):void				{_fields["_AddPhoto"] = val;}
		private function set IDNPhotos(val:String):void					{_fields["_Photos"] = val;}
		private function set IDNPhoto(val:String):void {
			if(_fields["_Photo"] == null) {
				_fields["_Photo"] = new QuickBaseFileDTO();
			}
			_fields["_Photo"].url = val;
		}
		private function set IDNPhotoName(val:String):void				{_fields["_PhotoName"] = val;}
		private function set IDNCondition(val:String):void				{_fields["_Condition"] = val;}
		private function set IDNAllowedSectionUser(val:String):void		{_fields["_AllowedSectionUser"] = Number(val);}
		private function set IDNAllowedFacilityUser(val:String):void	{_fields["_AllowedFacilityUser"] = Number(val);}
		private function set IDNAllowedClientUser(val:String):void		{_fields["_AllowedClientUser"] = Number(val);}
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
		public function getInfoObj():IKingussieInfo						{return Details_Info.getInstance();}
		public function getFieldMapObj():IQuickBaseFieldMap				{return new Details_FieldMap();}
		public function getFieldMapClass():Class						{return Details_FieldMap;}
		public function get dtoClass():Class							{return Details_DTO;}

		// MetaData Information Objects getters
		public function get Client_Info():TextField						{return Details_Info.getInstance().Client_Info;}
		public function get Facility_Info():TextField					{return Details_Info.getInstance().Facility_Info;}
		public function get Roof_Info():TextField						{return Details_Info.getInstance().Roof_Info;}
		public function get DetailType_Info():ChoiceField				{return Details_Info.getInstance().DetailType_Info;}
		public function get Description_Info():ChoiceField				{return Details_Info.getInstance().Description_Info;}
		public function get FlashingMembrane_Info():ChoiceField			{return Details_Info.getInstance().FlashingMembrane_Info;}
		public function get FlashingMetal_Info():ChoiceField			{return Details_Info.getInstance().FlashingMetal_Info;}
		public function get RelatedSection_Info():NumberField			{return Details_Info.getInstance().RelatedSection_Info;}
		public function get AddPhoto_Info():URLField					{return Details_Info.getInstance().AddPhoto_Info;}
		public function get Photos_Info():DbLinkField					{return Details_Info.getInstance().Photos_Info;}
		public function get Photo_Info():FileField						{return Details_Info.getInstance().Photo_Info;}
		public function get PhotoName_Info():TextField					{return Details_Info.getInstance().PhotoName_Info;}
		public function get Condition_Info():TextField					{return Details_Info.getInstance().Condition_Info;}
		public function get AllowedSectionUser_Info():NumberField		{return Details_Info.getInstance().AllowedSectionUser_Info;}
		public function get AllowedFacilityUser_Info():NumberField		{return Details_Info.getInstance().AllowedFacilityUser_Info;}
		public function get AllowedClientUser_Info():NumberField		{return Details_Info.getInstance().AllowedClientUser_Info;}
		public function get AllowedUser_Info():NumberField				{return Details_Info.getInstance().AllowedUser_Info;}
		public function get DateCreated_Info():TimeStampField			{return Details_Info.getInstance().DateCreated_Info;}
		public function get DateModified_Info():TimeStampField			{return Details_Info.getInstance().DateModified_Info;}
		public function get RecordOwner_Info():UserIdField				{return Details_Info.getInstance().RecordOwner_Info;}
		public function get LastModifiedBy_Info():UserIdField			{return Details_Info.getInstance().LastModifiedBy_Info;}

	}
}
