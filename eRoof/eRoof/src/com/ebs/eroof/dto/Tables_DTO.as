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
	import com.ebs.eroof.business.fieldmaps.Tables_FieldMap;
	import mx.collections.ArrayCollection;

	[Bindable]
	public class Tables_DTO extends KingussieDTOBase implements IValueObject, IKingussieDTO
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		// Record Owner Dirty Bit
		public function get IDNIsRecordOwnerDirty():Boolean				{return _fields["_isRecordOwnerDirty"];}


		// Current value getters
		public function get rid():String								{return _fields["_rid"];}
		public function get Seq():Number								{return _fields["_Seq"];}
		public function get Seq_isDirty():Boolean						{return _fields["_Seq_isDirty"];}
		public function get Name():String								{return _fields["_Name"];}
		public function get Name_isDirty():Boolean						{return _fields["_Name_isDirty"];}
		public function get Fields():String								{return _fields["_Fields"];}
		public function get Fields_isDirty():Boolean					{return _fields["_Fields_isDirty"];}
		public function get AddField():String							{return _fields["_AddField"];}
		public function get AddField_isDirty():Boolean					{return _fields["_AddField_isDirty"];}
		public function get SeqAndName():String							{return _fields["_SeqAndName"];}
		public function get SeqAndName_isDirty():Boolean				{return _fields["_SeqAndName_isDirty"];}
		public function get RecordPicker():String						{return _fields["_RecordPicker"];}
		public function get RecordPicker_isDirty():Boolean				{return _fields["_RecordPicker_isDirty"];}
		public function get IsHide():Boolean							{return _fields["_IsHide"];}
		public function get IsHide_isDirty():Boolean					{return _fields["_IsHide_isDirty"];}
		public function get DBIDName():String							{return _fields["_DBIDName"];}
		public function get DBIDName_isDirty():Boolean					{return _fields["_DBIDName_isDirty"];}
		public function get DBID():String								{return _fields["_DBID"];}
		public function get DBID_isDirty():Boolean						{return _fields["_DBID_isDirty"];}
		public function get Reports():String							{return _fields["_Reports"];}
		public function get Reports_isDirty():Boolean					{return _fields["_Reports_isDirty"];}
		public function get AddReport():String							{return _fields["_AddReport"];}
		public function get AddReport_isDirty():Boolean					{return _fields["_AddReport_isDirty"];}
		public function get IsConsultant():Boolean						{return _fields["_IsConsultant"];}
		public function get IsConsultant_isDirty():Boolean				{return _fields["_IsConsultant_isDirty"];}
		public function get IsClient():Boolean							{return _fields["_IsClient"];}
		public function get IsClient_isDirty():Boolean					{return _fields["_IsClient_isDirty"];}
		public function get IsContractor():Boolean						{return _fields["_IsContractor"];}
		public function get IsContractor_isDirty():Boolean				{return _fields["_IsContractor_isDirty"];}
		public function get IsInspector():Boolean						{return _fields["_IsInspector"];}
		public function get IsInspector_isDirty():Boolean				{return _fields["_IsInspector_isDirty"];}
		public function get IRPSource():String							{return _fields["_IRPSource"];}
		public function get IRPSource_isDirty():Boolean					{return _fields["_IRPSource_isDirty"];}
		public function get IsParent():Boolean							{return _fields["_IsParent"];}
		public function get IsParent_isDirty():Boolean					{return _fields["_IsParent_isDirty"];}
		public function get IRPConvert():Boolean						{return _fields["_IRPConvert"];}
		public function get IRPConvert_isDirty():Boolean				{return _fields["_IRPConvert_isDirty"];}
		public function get KeyField():String							{return _fields["_KeyField"];}
		public function get KeyField_isDirty():Boolean					{return _fields["_KeyField_isDirty"];}
		public function get IsChild():Boolean							{return _fields["_IsChild"];}
		public function get IsChild_isDirty():Boolean					{return _fields["_IsChild_isDirty"];}
		public function get ParentTableName():String					{return _fields["_ParentTableName"];}
		public function get ParentTableName_isDirty():Boolean			{return _fields["_ParentTableName_isDirty"];}
		public function get ParentKeyField():String						{return _fields["_ParentKeyField"];}
		public function get ParentKeyField_isDirty():Boolean			{return _fields["_ParentKeyField_isDirty"];}
		public function get OldParentKeyField():String					{return _fields["_OldParentKeyField"];}
		public function get OldParentKeyField_isDirty():Boolean			{return _fields["_OldParentKeyField_isDirty"];}
		public function get IRPConvert2():Boolean						{return _fields["_IRPConvert2"];}
		public function get IRPConvert2_isDirty():Boolean				{return _fields["_IRPConvert2_isDirty"];}
		public function get ClientList():String							{return _fields["_ClientList"];}
		public function get ClientList_isDirty():Boolean				{return _fields["_ClientList_isDirty"];}
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
		public function set Seq(val:Number):void						{_fields["_Seq"] = val; _fields["_Seq_isDirty"] = true;}
		public function set Name(val:String):void						{_fields["_Name"] = val; _fields["_Name_isDirty"] = true;}
		public function set RecordPicker(val:String):void				{_fields["_RecordPicker"] = val; _fields["_RecordPicker_isDirty"] = true;}
		public function set IsHide(val:Boolean):void					{_fields["_IsHide"] = val; _fields["_IsHide_isDirty"] = true;}
		public function set DBIDName(val:String):void					{_fields["_DBIDName"] = val; _fields["_DBIDName_isDirty"] = true;}
		public function set IsConsultant(val:Boolean):void				{_fields["_IsConsultant"] = val; _fields["_IsConsultant_isDirty"] = true;}
		public function set IsClient(val:Boolean):void					{_fields["_IsClient"] = val; _fields["_IsClient_isDirty"] = true;}
		public function set IsContractor(val:Boolean):void				{_fields["_IsContractor"] = val; _fields["_IsContractor_isDirty"] = true;}
		public function set IsInspector(val:Boolean):void				{_fields["_IsInspector"] = val; _fields["_IsInspector_isDirty"] = true;}
		public function set IRPSource(val:String):void					{_fields["_IRPSource"] = val; _fields["_IRPSource_isDirty"] = true;}
		public function set IsParent(val:Boolean):void					{_fields["_IsParent"] = val; _fields["_IsParent_isDirty"] = true;}
		public function set IRPConvert(val:Boolean):void				{_fields["_IRPConvert"] = val; _fields["_IRPConvert_isDirty"] = true;}
		public function set KeyField(val:String):void					{_fields["_KeyField"] = val; _fields["_KeyField_isDirty"] = true;}
		public function set IsChild(val:Boolean):void					{_fields["_IsChild"] = val; _fields["_IsChild_isDirty"] = true;}
		public function set ParentTableName(val:String):void			{_fields["_ParentTableName"] = val; _fields["_ParentTableName_isDirty"] = true;}
		public function set ParentKeyField(val:String):void				{_fields["_ParentKeyField"] = val; _fields["_ParentKeyField_isDirty"] = true;}
		public function set OldParentKeyField(val:String):void			{_fields["_OldParentKeyField"] = val; _fields["_OldParentKeyField_isDirty"] = true;}
		public function set IRPConvert2(val:Boolean):void				{_fields["_IRPConvert2"] = val; _fields["_IRPConvert2_isDirty"] = true;}

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
		private function set IDNSeq(val:String):void					{_fields["_Seq"] = Number(val);}
		private function set IDNName(val:String):void					{_fields["_Name"] = val;}
		private function set IDNFields(val:String):void					{_fields["_Fields"] = val;}
		private function set IDNAddField(val:String):void				{_fields["_AddField"] = val;}
		private function set IDNSeqAndName(val:String):void				{_fields["_SeqAndName"] = val;}
		private function set IDNRecordPicker(val:String):void			{_fields["_RecordPicker"] = val;}
		private function set IDNIsHide(val:String):void					{_fields["_IsHide"] = Boolean(Number(val));}
		private function set IDNDBIDName(val:String):void				{_fields["_DBIDName"] = val;}
		private function set IDNDBID(val:String):void					{_fields["_DBID"] = val;}
		private function set IDNReports(val:String):void				{_fields["_Reports"] = val;}
		private function set IDNAddReport(val:String):void				{_fields["_AddReport"] = val;}
		private function set IDNIsConsultant(val:String):void			{_fields["_IsConsultant"] = Boolean(Number(val));}
		private function set IDNIsClient(val:String):void				{_fields["_IsClient"] = Boolean(Number(val));}
		private function set IDNIsContractor(val:String):void			{_fields["_IsContractor"] = Boolean(Number(val));}
		private function set IDNIsInspector(val:String):void			{_fields["_IsInspector"] = Boolean(Number(val));}
		private function set IDNIRPSource(val:String):void				{_fields["_IRPSource"] = val;}
		private function set IDNIsParent(val:String):void				{_fields["_IsParent"] = Boolean(Number(val));}
		private function set IDNIRPConvert(val:String):void				{_fields["_IRPConvert"] = Boolean(Number(val));}
		private function set IDNKeyField(val:String):void				{_fields["_KeyField"] = val;}
		private function set IDNIsChild(val:String):void				{_fields["_IsChild"] = Boolean(Number(val));}
		private function set IDNParentTableName(val:String):void		{_fields["_ParentTableName"] = val;}
		private function set IDNParentKeyField(val:String):void			{_fields["_ParentKeyField"] = val;}
		private function set IDNOldParentKeyField(val:String):void		{_fields["_OldParentKeyField"] = val;}
		private function set IDNIRPConvert2(val:String):void			{_fields["_IRPConvert2"] = Boolean(Number(val));}
		private function set IDNClientList(val:String):void				{_fields["_ClientList"] = val;}
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
		public function getInfoObj():IKingussieInfo						{return Tables_Info.getInstance();}
		public function getFieldMapObj():IQuickBaseFieldMap				{return new Tables_FieldMap();}
		public function getFieldMapClass():Class						{return Tables_FieldMap;}
		public function get dtoClass():Class							{return Tables_DTO;}

		// MetaData Information Objects getters
		public function get Seq_Info():NumberField						{return Tables_Info.getInstance().Seq_Info;}
		public function get Name_Info():TextField						{return Tables_Info.getInstance().Name_Info;}
		public function get Fields_Info():DbLinkField					{return Tables_Info.getInstance().Fields_Info;}
		public function get AddField_Info():URLField					{return Tables_Info.getInstance().AddField_Info;}
		public function get SeqAndName_Info():TextField					{return Tables_Info.getInstance().SeqAndName_Info;}
		public function get RecordPicker_Info():TextField				{return Tables_Info.getInstance().RecordPicker_Info;}
		public function get IsHide_Info():BooleanField					{return Tables_Info.getInstance().IsHide_Info;}
		public function get DBIDName_Info():TextField					{return Tables_Info.getInstance().DBIDName_Info;}
		public function get DBID_Info():TextField						{return Tables_Info.getInstance().DBID_Info;}
		public function get Reports_Info():DbLinkField					{return Tables_Info.getInstance().Reports_Info;}
		public function get AddReport_Info():URLField					{return Tables_Info.getInstance().AddReport_Info;}
		public function get IsConsultant_Info():BooleanField			{return Tables_Info.getInstance().IsConsultant_Info;}
		public function get IsClient_Info():BooleanField				{return Tables_Info.getInstance().IsClient_Info;}
		public function get IsContractor_Info():BooleanField			{return Tables_Info.getInstance().IsContractor_Info;}
		public function get IsInspector_Info():BooleanField				{return Tables_Info.getInstance().IsInspector_Info;}
		public function get IRPSource_Info():TextField					{return Tables_Info.getInstance().IRPSource_Info;}
		public function get IsParent_Info():BooleanField				{return Tables_Info.getInstance().IsParent_Info;}
		public function get IRPConvert_Info():BooleanField				{return Tables_Info.getInstance().IRPConvert_Info;}
		public function get KeyField_Info():TextField					{return Tables_Info.getInstance().KeyField_Info;}
		public function get IsChild_Info():BooleanField					{return Tables_Info.getInstance().IsChild_Info;}
		public function get ParentTableName_Info():TextField			{return Tables_Info.getInstance().ParentTableName_Info;}
		public function get ParentKeyField_Info():TextField				{return Tables_Info.getInstance().ParentKeyField_Info;}
		public function get OldParentKeyField_Info():TextField			{return Tables_Info.getInstance().OldParentKeyField_Info;}
		public function get IRPConvert2_Info():BooleanField				{return Tables_Info.getInstance().IRPConvert2_Info;}
		public function get ClientList_Info():URLField					{return Tables_Info.getInstance().ClientList_Info;}
		public function get DateCreated_Info():TimeStampField			{return Tables_Info.getInstance().DateCreated_Info;}
		public function get DateModified_Info():TimeStampField			{return Tables_Info.getInstance().DateModified_Info;}
		public function get RecordOwner_Info():UserIdField				{return Tables_Info.getInstance().RecordOwner_Info;}
		public function get LastModifiedBy_Info():UserIdField			{return Tables_Info.getInstance().LastModifiedBy_Info;}

	}
}
