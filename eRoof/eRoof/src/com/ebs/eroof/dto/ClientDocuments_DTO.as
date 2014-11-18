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
	import com.ebs.eroof.business.fieldmaps.ClientDocuments_FieldMap;
	import mx.collections.ArrayCollection;

	[Bindable]
	public class ClientDocuments_DTO extends KingussieDTOBase implements IValueObject, IKingussieDTO
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		// Record Owner Dirty Bit
		public function get IDNIsRecordOwnerDirty():Boolean				{return _fields["_isRecordOwnerDirty"];}


		// Current value getters
		public function get rid():String								{return _fields["_rid"];}
		public function get RelatedClient():Number						{return _fields["_RelatedClient"];}
		public function get RelatedClient_isDirty():Boolean				{return _fields["_RelatedClient_isDirty"];}
		public function get Client():String								{return _fields["_Client"];}
		public function get Client_isDirty():Boolean					{return _fields["_Client_isDirty"];}
		public function get Document():QuickBaseFileDTO {
			if(_fields["_Document"] == null) {
				_fields["_Document"] = new QuickBaseFileDTO();
			}
			return _fields["_Document"];
		}
		public function get Title():String								{return _fields["_Title"];}
		public function get Title_isDirty():Boolean						{return _fields["_Title_isDirty"];}
		public function get Description():String						{return _fields["_Description"];}
		public function get Description_isDirty():Boolean				{return _fields["_Description_isDirty"];}
		public function get Date():Date									{return _fields["_Date"];}
		public function get Date_isDirty():Boolean						{return _fields["_Date_isDirty"];}
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
		public function set RelatedClient(val:Number):void				{_fields["_RelatedClient"] = val; _fields["_RelatedClient_isDirty"] = true;}
		public function set Title(val:String):void						{_fields["_Title"] = val; _fields["_Title_isDirty"] = true;}
		public function set Description(val:String):void				{_fields["_Description"] = val; _fields["_Description_isDirty"] = true;}
		public function set Date(val:Date):void							{_fields["_Date"] = val; _fields["_Date_isDirty"] = true;}

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
		private function set IDNRelatedClient(val:String):void			{_fields["_RelatedClient"] = Number(val);}
		private function set IDNClient(val:String):void					{_fields["_Client"] = val;}
		private function set IDNDocument(val:String):void {
			if(_fields["_Document"] == null) {
				_fields["_Document"] = new QuickBaseFileDTO();
			}
			_fields["_Document"].url = val;
		}
		private function set IDNTitle(val:String):void					{_fields["_Title"] = val;}
		private function set IDNDescription(val:String):void			{_fields["_Description"] = val;}
		private function set IDNDate(val:String):void					{_fields["_Date"] = new Date(Number(val));}
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
		public function getInfoObj():IKingussieInfo						{return ClientDocuments_Info.getInstance();}
		public function getFieldMapObj():IQuickBaseFieldMap				{return new ClientDocuments_FieldMap();}
		public function getFieldMapClass():Class						{return ClientDocuments_FieldMap;}
		public function get dtoClass():Class							{return ClientDocuments_DTO;}

		// MetaData Information Objects getters
		public function get RelatedClient_Info():NumberField			{return ClientDocuments_Info.getInstance().RelatedClient_Info;}
		public function get Client_Info():TextField						{return ClientDocuments_Info.getInstance().Client_Info;}
		public function get Document_Info():FileField					{return ClientDocuments_Info.getInstance().Document_Info;}
		public function get Title_Info():TextField						{return ClientDocuments_Info.getInstance().Title_Info;}
		public function get Description_Info():TextField				{return ClientDocuments_Info.getInstance().Description_Info;}
		public function get Date_Info():DateField						{return ClientDocuments_Info.getInstance().Date_Info;}
		public function get AllowedClientUser_Info():NumberField		{return ClientDocuments_Info.getInstance().AllowedClientUser_Info;}
		public function get AllowedUser_Info():NumberField				{return ClientDocuments_Info.getInstance().AllowedUser_Info;}
		public function get DateCreated_Info():TimeStampField			{return ClientDocuments_Info.getInstance().DateCreated_Info;}
		public function get DateModified_Info():TimeStampField			{return ClientDocuments_Info.getInstance().DateModified_Info;}
		public function get RecordOwner_Info():UserIdField				{return ClientDocuments_Info.getInstance().RecordOwner_Info;}
		public function get LastModifiedBy_Info():UserIdField			{return ClientDocuments_Info.getInstance().LastModifiedBy_Info;}

	}
}
