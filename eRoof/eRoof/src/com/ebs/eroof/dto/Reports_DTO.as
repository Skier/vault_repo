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
	import com.ebs.eroof.business.fieldmaps.Reports_FieldMap;
	import mx.collections.ArrayCollection;

	[Bindable]
	public class Reports_DTO extends KingussieDTOBase implements IValueObject, IKingussieDTO
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		// Record Owner Dirty Bit
		public function get IDNIsRecordOwnerDirty():Boolean				{return _fields["_isRecordOwnerDirty"];}


		// Current value getters
		public function get rid():String								{return _fields["_rid"];}
		public function get SeqAndName():String							{return _fields["_SeqAndName"];}
		public function get SeqAndName_isDirty():Boolean				{return _fields["_SeqAndName_isDirty"];}
		public function get RelatedTable():Number						{return _fields["_RelatedTable"];}
		public function get RelatedTable_isDirty():Boolean				{return _fields["_RelatedTable_isDirty"];}
		public function get ReportName():String							{return _fields["_ReportName"];}
		public function get ReportName_isDirty():Boolean				{return _fields["_ReportName_isDirty"];}
		public function get Filter():String								{return _fields["_Filter"];}
		public function get Filter_isDirty():Boolean					{return _fields["_Filter_isDirty"];}
		public function get Sort():String								{return _fields["_Sort"];}
		public function get Sort_isDirty():Boolean						{return _fields["_Sort_isDirty"];}
		public function get IsSummary():Boolean							{return _fields["_IsSummary"];}
		public function get IsSummary_isDirty():Boolean					{return _fields["_IsSummary_isDirty"];}
		public function get IsSuppEdit():Boolean						{return _fields["_IsSuppEdit"];}
		public function get IsSuppEdit_isDirty():Boolean				{return _fields["_IsSuppEdit_isDirty"];}
		public function get IsSuppView():Boolean						{return _fields["_IsSuppView"];}
		public function get IsSuppView_isDirty():Boolean				{return _fields["_IsSuppView_isDirty"];}
		public function get IsGridEdit():Boolean						{return _fields["_IsGridEdit"];}
		public function get IsGridEdit_isDirty():Boolean				{return _fields["_IsGridEdit_isDirty"];}
		public function get Table():String								{return _fields["_Table"];}
		public function get Table_isDirty():Boolean						{return _fields["_Table_isDirty"];}
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
		public function set RelatedTable(val:Number):void				{_fields["_RelatedTable"] = val; _fields["_RelatedTable_isDirty"] = true;}
		public function set ReportName(val:String):void					{_fields["_ReportName"] = val; _fields["_ReportName_isDirty"] = true;}
		public function set Filter(val:String):void						{_fields["_Filter"] = val; _fields["_Filter_isDirty"] = true;}
		public function set Sort(val:String):void						{_fields["_Sort"] = val; _fields["_Sort_isDirty"] = true;}
		public function set IsSummary(val:Boolean):void					{_fields["_IsSummary"] = val; _fields["_IsSummary_isDirty"] = true;}
		public function set IsSuppEdit(val:Boolean):void				{_fields["_IsSuppEdit"] = val; _fields["_IsSuppEdit_isDirty"] = true;}
		public function set IsSuppView(val:Boolean):void				{_fields["_IsSuppView"] = val; _fields["_IsSuppView_isDirty"] = true;}
		public function set IsGridEdit(val:Boolean):void				{_fields["_IsGridEdit"] = val; _fields["_IsGridEdit_isDirty"] = true;}

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
		private function set IDNSeqAndName(val:String):void				{_fields["_SeqAndName"] = val;}
		private function set IDNRelatedTable(val:String):void			{_fields["_RelatedTable"] = Number(val);}
		private function set IDNReportName(val:String):void				{_fields["_ReportName"] = val;}
		private function set IDNFilter(val:String):void					{_fields["_Filter"] = val;}
		private function set IDNSort(val:String):void					{_fields["_Sort"] = val;}
		private function set IDNIsSummary(val:String):void				{_fields["_IsSummary"] = Boolean(Number(val));}
		private function set IDNIsSuppEdit(val:String):void				{_fields["_IsSuppEdit"] = Boolean(Number(val));}
		private function set IDNIsSuppView(val:String):void				{_fields["_IsSuppView"] = Boolean(Number(val));}
		private function set IDNIsGridEdit(val:String):void				{_fields["_IsGridEdit"] = Boolean(Number(val));}
		private function set IDNTable(val:String):void					{_fields["_Table"] = val;}
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
		public function getInfoObj():IKingussieInfo						{return Reports_Info.getInstance();}
		public function getFieldMapObj():IQuickBaseFieldMap				{return new Reports_FieldMap();}
		public function getFieldMapClass():Class						{return Reports_FieldMap;}
		public function get dtoClass():Class							{return Reports_DTO;}

		// MetaData Information Objects getters
		public function get SeqAndName_Info():TextField					{return Reports_Info.getInstance().SeqAndName_Info;}
		public function get RelatedTable_Info():NumberField				{return Reports_Info.getInstance().RelatedTable_Info;}
		public function get ReportName_Info():TextField					{return Reports_Info.getInstance().ReportName_Info;}
		public function get Filter_Info():TextField						{return Reports_Info.getInstance().Filter_Info;}
		public function get Sort_Info():TextField						{return Reports_Info.getInstance().Sort_Info;}
		public function get IsSummary_Info():BooleanField				{return Reports_Info.getInstance().IsSummary_Info;}
		public function get IsSuppEdit_Info():BooleanField				{return Reports_Info.getInstance().IsSuppEdit_Info;}
		public function get IsSuppView_Info():BooleanField				{return Reports_Info.getInstance().IsSuppView_Info;}
		public function get IsGridEdit_Info():BooleanField				{return Reports_Info.getInstance().IsGridEdit_Info;}
		public function get Table_Info():TextField						{return Reports_Info.getInstance().Table_Info;}
		public function get DateCreated_Info():TimeStampField			{return Reports_Info.getInstance().DateCreated_Info;}
		public function get DateModified_Info():TimeStampField			{return Reports_Info.getInstance().DateModified_Info;}
		public function get RecordOwner_Info():UserIdField				{return Reports_Info.getInstance().RecordOwner_Info;}
		public function get LastModifiedBy_Info():UserIdField			{return Reports_Info.getInstance().LastModifiedBy_Info;}

	}
}
