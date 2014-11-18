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
	import com.ebs.eroof.business.fieldmaps.Warranties_FieldMap;
	import mx.collections.ArrayCollection;

	[Bindable]
	public class Warranties_DTO extends KingussieDTOBase implements IValueObject, IKingussieDTO
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		// Record Owner Dirty Bit
		public function get IDNIsRecordOwnerDirty():Boolean				{return _fields["_isRecordOwnerDirty"];}


		// Current value getters
		public function get rid():String								{return _fields["_rid"];}
		public function get RelatedSection():Number						{return _fields["_RelatedSection"];}
		public function get RelatedSection_isDirty():Boolean			{return _fields["_RelatedSection_isDirty"];}
		public function get Client():String								{return _fields["_Client"];}
		public function get Client_isDirty():Boolean					{return _fields["_Client_isDirty"];}
		public function get Facility():String							{return _fields["_Facility"];}
		public function get Facility_isDirty():Boolean					{return _fields["_Facility_isDirty"];}
		public function get Roof():String								{return _fields["_Roof"];}
		public function get Roof_isDirty():Boolean						{return _fields["_Roof_isDirty"];}
		public function get DateI():String								{return _fields["_DateI"];}
		public function get DateI_isDirty():Boolean						{return _fields["_DateI_isDirty"];}
		public function get IssueDate():Date							{return _fields["_IssueDate"];}
		public function get IssueDate_isDirty():Boolean					{return _fields["_IssueDate_isDirty"];}
		public function get DateE():String								{return _fields["_DateE"];}
		public function get DateE_isDirty():Boolean						{return _fields["_DateE_isDirty"];}
		public function get ExpiryDate():Date							{return _fields["_ExpiryDate"];}
		public function get ExpiryDate_isDirty():Boolean				{return _fields["_ExpiryDate_isDirty"];}
		public function get Type():String								{return _fields["_Type"];}
		public function get IssuedBy():String							{return _fields["_IssuedBy"];}
		public function get IssuedBy_isDirty():Boolean					{return _fields["_IssuedBy_isDirty"];}
		public function get Notes():String								{return _fields["_Notes"];}
		public function get Notes_isDirty():Boolean						{return _fields["_Notes_isDirty"];}
		public function get Duration():Number							{return _fields["_Duration"];}
		public function get Duration_isDirty():Boolean					{return _fields["_Duration_isDirty"];}
		public function get Documents():String							{return _fields["_Documents"];}
		public function get Documents_isDirty():Boolean					{return _fields["_Documents_isDirty"];}
		public function get AddDocument():String						{return _fields["_AddDocument"];}
		public function get AddDocument_isDirty():Boolean				{return _fields["_AddDocument_isDirty"];}
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
		public function set RelatedSection(val:Number):void				{_fields["_RelatedSection"] = val; _fields["_RelatedSection_isDirty"] = true;}
		public function set DateI(val:String):void						{_fields["_DateI"] = val; _fields["_DateI_isDirty"] = true;}
		public function set IssueDate(val:Date):void					{_fields["_IssueDate"] = val; _fields["_IssueDate_isDirty"] = true;}
		public function set DateE(val:String):void						{_fields["_DateE"] = val; _fields["_DateE_isDirty"] = true;}
		public function set ExpiryDate(val:Date):void					{_fields["_ExpiryDate"] = val; _fields["_ExpiryDate_isDirty"] = true;}
		public function set Type(val:String):void
		{
			if (Type_Info.validate(val)) {
				_fields["_Type"] = val;
				_fields["_Type_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + Type_Info.label + " value requested: " + val);
			}
		}

		public function set IssuedBy(val:String):void					{_fields["_IssuedBy"] = val; _fields["_IssuedBy_isDirty"] = true;}
		public function set Notes(val:String):void						{_fields["_Notes"] = val; _fields["_Notes_isDirty"] = true;}
		public function set Duration(val:Number):void					{_fields["_Duration"] = val; _fields["_Duration_isDirty"] = true;}

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
		private function set IDNRelatedSection(val:String):void			{_fields["_RelatedSection"] = Number(val);}
		private function set IDNClient(val:String):void					{_fields["_Client"] = val;}
		private function set IDNFacility(val:String):void				{_fields["_Facility"] = val;}
		private function set IDNRoof(val:String):void					{_fields["_Roof"] = val;}
		private function set IDNDateI(val:String):void					{_fields["_DateI"] = val;}
		private function set IDNIssueDate(val:String):void				{_fields["_IssueDate"] = new Date(Number(val));}
		private function set IDNDateE(val:String):void					{_fields["_DateE"] = val;}
		private function set IDNExpiryDate(val:String):void				{_fields["_ExpiryDate"] = new Date(Number(val));}
		private function set IDNType(val:String):void					{_fields["_Type"] = val;}
		private function set IDNIssuedBy(val:String):void				{_fields["_IssuedBy"] = val;}
		private function set IDNNotes(val:String):void					{_fields["_Notes"] = val;}
		private function set IDNDuration(val:String):void				{_fields["_Duration"] = Number(val);}
		private function set IDNDocuments(val:String):void				{_fields["_Documents"] = val;}
		private function set IDNAddDocument(val:String):void			{_fields["_AddDocument"] = val;}
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
		public function getInfoObj():IKingussieInfo						{return Warranties_Info.getInstance();}
		public function getFieldMapObj():IQuickBaseFieldMap				{return new Warranties_FieldMap();}
		public function getFieldMapClass():Class						{return Warranties_FieldMap;}
		public function get dtoClass():Class							{return Warranties_DTO;}

		// MetaData Information Objects getters
		public function get RelatedSection_Info():NumberField			{return Warranties_Info.getInstance().RelatedSection_Info;}
		public function get Client_Info():TextField						{return Warranties_Info.getInstance().Client_Info;}
		public function get Facility_Info():TextField					{return Warranties_Info.getInstance().Facility_Info;}
		public function get Roof_Info():TextField						{return Warranties_Info.getInstance().Roof_Info;}
		public function get DateI_Info():TextField						{return Warranties_Info.getInstance().DateI_Info;}
		public function get IssueDate_Info():DateField					{return Warranties_Info.getInstance().IssueDate_Info;}
		public function get DateE_Info():TextField						{return Warranties_Info.getInstance().DateE_Info;}
		public function get ExpiryDate_Info():DateField					{return Warranties_Info.getInstance().ExpiryDate_Info;}
		public function get Type_Info():ChoiceField						{return Warranties_Info.getInstance().Type_Info;}
		public function get IssuedBy_Info():TextField					{return Warranties_Info.getInstance().IssuedBy_Info;}
		public function get Notes_Info():TextField						{return Warranties_Info.getInstance().Notes_Info;}
		public function get Duration_Info():DurationField				{return Warranties_Info.getInstance().Duration_Info;}
		public function get Documents_Info():DbLinkField				{return Warranties_Info.getInstance().Documents_Info;}
		public function get AddDocument_Info():URLField					{return Warranties_Info.getInstance().AddDocument_Info;}
		public function get AllowedClientUser_Info():NumberField		{return Warranties_Info.getInstance().AllowedClientUser_Info;}
		public function get AllowedFacilityUser_Info():NumberField		{return Warranties_Info.getInstance().AllowedFacilityUser_Info;}
		public function get AllowedSectionUser_Info():NumberField		{return Warranties_Info.getInstance().AllowedSectionUser_Info;}
		public function get AllowedUser_Info():NumberField				{return Warranties_Info.getInstance().AllowedUser_Info;}
		public function get DateCreated_Info():TimeStampField			{return Warranties_Info.getInstance().DateCreated_Info;}
		public function get DateModified_Info():TimeStampField			{return Warranties_Info.getInstance().DateModified_Info;}
		public function get RecordOwner_Info():UserIdField				{return Warranties_Info.getInstance().RecordOwner_Info;}
		public function get LastModifiedBy_Info():UserIdField			{return Warranties_Info.getInstance().LastModifiedBy_Info;}

	}
}
