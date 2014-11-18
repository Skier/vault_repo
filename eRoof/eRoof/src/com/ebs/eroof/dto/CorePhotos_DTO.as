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
	import com.ebs.eroof.business.fieldmaps.CorePhotos_FieldMap;
	import mx.collections.ArrayCollection;

	[Bindable]
	public class CorePhotos_DTO extends KingussieDTOBase implements IValueObject, IKingussieDTO
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
		public function get Photo():QuickBaseFileDTO {
			if(_fields["_Photo"] == null) {
				_fields["_Photo"] = new QuickBaseFileDTO();
			}
			return _fields["_Photo"];
		}
		public function get Description():String						{return _fields["_Description"];}
		public function get Description_isDirty():Boolean				{return _fields["_Description_isDirty"];}
		public function get PhotoDate():Date							{return _fields["_PhotoDate"];}
		public function get PhotoDate_isDirty():Boolean					{return _fields["_PhotoDate_isDirty"];}
		public function get AllowedFacilityUser():Number				{return _fields["_AllowedFacilityUser"];}
		public function get AllowedFacilityUser_isDirty():Boolean		{return _fields["_AllowedFacilityUser_isDirty"];}
		public function get AllowedSectionUser():Number					{return _fields["_AllowedSectionUser"];}
		public function get AllowedSectionUser_isDirty():Boolean		{return _fields["_AllowedSectionUser_isDirty"];}
		public function get AllowedClientUser():Number					{return _fields["_AllowedClientUser"];}
		public function get AllowedClientUser_isDirty():Boolean			{return _fields["_AllowedClientUser_isDirty"];}
		public function get PhotoThumbnail():String						{return _fields["_PhotoThumbnail"];}
		public function get PhotoThumbnail_isDirty():Boolean			{return _fields["_PhotoThumbnail_isDirty"];}
		public function get AllowedUser():Number						{return _fields["_AllowedUser"];}
		public function get AllowedUser_isDirty():Boolean				{return _fields["_AllowedUser_isDirty"];}
		public function get IRPPhotoFileName():String					{return _fields["_IRPPhotoFileName"];}
		public function get IRPPhotoFileName_isDirty():Boolean			{return _fields["_IRPPhotoFileName_isDirty"];}
		public function get DateCreated():Date							{return _fields["_DateCreated"];}
		public function get DateCreated_isDirty():Boolean				{return _fields["_DateCreated_isDirty"];}
		public function get DateModified():Date							{return _fields["_DateModified"];}
		public function get DateModified_isDirty():Boolean				{return _fields["_DateModified_isDirty"];}
		public function get RecordOwner():QuickBaseUserDTO 				{return _fields["_RecordOwner"];}
		public function get RecordOwner_isDirty():Boolean					{return _fields["_RecordOwner_isDirty"];}
		public function get LastModifiedBy():QuickBaseUserDTO 			{return _fields["_LastModifiedBy"];}
		public function get LastModifiedBy_isDirty():Boolean				{return _fields["_LastModifiedBy_isDirty"];}

		// Choice getters

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
		public function set Description(val:String):void				{_fields["_Description"] = val; _fields["_Description_isDirty"] = true;}
		public function set PhotoDate(val:Date):void					{_fields["_PhotoDate"] = val; _fields["_PhotoDate_isDirty"] = true;}
		public function set IRPPhotoFileName(val:String):void			{_fields["_IRPPhotoFileName"] = val; _fields["_IRPPhotoFileName_isDirty"] = true;}

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
		private function set IDNPhoto(val:String):void {
			if(_fields["_Photo"] == null) {
				_fields["_Photo"] = new QuickBaseFileDTO();
			}
			_fields["_Photo"].url = val;
		}
		private function set IDNDescription(val:String):void			{_fields["_Description"] = val;}
		private function set IDNPhotoDate(val:String):void				{_fields["_PhotoDate"] = new Date(Number(val));}
		private function set IDNAllowedFacilityUser(val:String):void	{_fields["_AllowedFacilityUser"] = Number(val);}
		private function set IDNAllowedSectionUser(val:String):void		{_fields["_AllowedSectionUser"] = Number(val);}
		private function set IDNAllowedClientUser(val:String):void		{_fields["_AllowedClientUser"] = Number(val);}
		private function set IDNPhotoThumbnail(val:String):void			{_fields["_PhotoThumbnail"] = val;}
		private function set IDNAllowedUser(val:String):void			{_fields["_AllowedUser"] = Number(val);}
		private function set IDNIRPPhotoFileName(val:String):void		{_fields["_IRPPhotoFileName"] = val;}
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
		public function getInfoObj():IKingussieInfo						{return CorePhotos_Info.getInstance();}
		public function getFieldMapObj():IQuickBaseFieldMap				{return new CorePhotos_FieldMap();}
		public function getFieldMapClass():Class						{return CorePhotos_FieldMap;}
		public function get dtoClass():Class							{return CorePhotos_DTO;}

		// MetaData Information Objects getters
		public function get RelatedSection_Info():NumberField			{return CorePhotos_Info.getInstance().RelatedSection_Info;}
		public function get Client_Info():TextField						{return CorePhotos_Info.getInstance().Client_Info;}
		public function get Facility_Info():TextField					{return CorePhotos_Info.getInstance().Facility_Info;}
		public function get Roof_Info():TextField						{return CorePhotos_Info.getInstance().Roof_Info;}
		public function get Photo_Info():FileField						{return CorePhotos_Info.getInstance().Photo_Info;}
		public function get Description_Info():TextField				{return CorePhotos_Info.getInstance().Description_Info;}
		public function get PhotoDate_Info():DateField					{return CorePhotos_Info.getInstance().PhotoDate_Info;}
		public function get AllowedFacilityUser_Info():NumberField		{return CorePhotos_Info.getInstance().AllowedFacilityUser_Info;}
		public function get AllowedSectionUser_Info():NumberField		{return CorePhotos_Info.getInstance().AllowedSectionUser_Info;}
		public function get AllowedClientUser_Info():NumberField		{return CorePhotos_Info.getInstance().AllowedClientUser_Info;}
		public function get PhotoThumbnail_Info():TextField				{return CorePhotos_Info.getInstance().PhotoThumbnail_Info;}
		public function get AllowedUser_Info():NumberField				{return CorePhotos_Info.getInstance().AllowedUser_Info;}
		public function get IRPPhotoFileName_Info():TextField			{return CorePhotos_Info.getInstance().IRPPhotoFileName_Info;}
		public function get DateCreated_Info():TimeStampField			{return CorePhotos_Info.getInstance().DateCreated_Info;}
		public function get DateModified_Info():TimeStampField			{return CorePhotos_Info.getInstance().DateModified_Info;}
		public function get RecordOwner_Info():UserIdField				{return CorePhotos_Info.getInstance().RecordOwner_Info;}
		public function get LastModifiedBy_Info():UserIdField			{return CorePhotos_Info.getInstance().LastModifiedBy_Info;}

	}
}
