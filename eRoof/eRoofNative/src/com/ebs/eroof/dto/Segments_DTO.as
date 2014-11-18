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
	import com.ebs.eroof.business.fieldmaps.Segments_FieldMap;
	import mx.collections.ArrayCollection;

	[Bindable]
	public class Segments_DTO extends KingussieDTOBase implements IValueObject, IKingussieDTO
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		// Record Owner Dirty Bit
		public function get IDNIsRecordOwnerDirty():Boolean				{return _fields["_isRecordOwnerDirty"];}


		// Current value getters
		public function get rid():String								{return _fields["_rid"];}
		public function get SegmentName():String						{return _fields["_SegmentName"];}
		public function get SegmentName_isDirty():Boolean				{return _fields["_SegmentName_isDirty"];}
		public function get ClientsCount():Number						{return _fields["_ClientsCount"];}
		public function get ClientsCount_isDirty():Boolean				{return _fields["_ClientsCount_isDirty"];}
		public function get FacilitiesCount():Number					{return _fields["_FacilitiesCount"];}
		public function get FacilitiesCount_isDirty():Boolean			{return _fields["_FacilitiesCount_isDirty"];}
		public function get SectionsCount():Number						{return _fields["_SectionsCount"];}
		public function get SectionsCount_isDirty():Boolean				{return _fields["_SectionsCount_isDirty"];}
		public function get TotalSqFt():Number							{return _fields["_TotalSqFt"];}
		public function get TotalSqFt_isDirty():Boolean					{return _fields["_TotalSqFt_isDirty"];}
		public function get TotalValue():Number							{return _fields["_TotalValue"];}
		public function get TotalValue_isDirty():Boolean				{return _fields["_TotalValue_isDirty"];}
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
		public function set SegmentName(val:String):void				{_fields["_SegmentName"] = val; _fields["_SegmentName_isDirty"] = true;}

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
		private function set IDNSegmentName(val:String):void			{_fields["_SegmentName"] = val;}
		private function set IDNClientsCount(val:String):void			{_fields["_ClientsCount"] = Number(val);}
		private function set IDNFacilitiesCount(val:String):void		{_fields["_FacilitiesCount"] = Number(val);}
		private function set IDNSectionsCount(val:String):void			{_fields["_SectionsCount"] = Number(val);}
		private function set IDNTotalSqFt(val:String):void				{_fields["_TotalSqFt"] = Number(val);}
		private function set IDNTotalValue(val:String):void				{_fields["_TotalValue"] = Number(val);}
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
		public function getInfoObj():IKingussieInfo						{return Segments_Info.getInstance();}
		public function getFieldMapObj():IQuickBaseFieldMap				{return new Segments_FieldMap();}
		public function getFieldMapClass():Class						{return Segments_FieldMap;}
		public function get dtoClass():Class							{return Segments_DTO;}

		// MetaData Information Objects getters
		public function get SegmentName_Info():TextField				{return Segments_Info.getInstance().SegmentName_Info;}
		public function get ClientsCount_Info():NumberField				{return Segments_Info.getInstance().ClientsCount_Info;}
		public function get FacilitiesCount_Info():NumberField			{return Segments_Info.getInstance().FacilitiesCount_Info;}
		public function get SectionsCount_Info():NumberField			{return Segments_Info.getInstance().SectionsCount_Info;}
		public function get TotalSqFt_Info():NumberField				{return Segments_Info.getInstance().TotalSqFt_Info;}
		public function get TotalValue_Info():CurrencyField				{return Segments_Info.getInstance().TotalValue_Info;}
		public function get DateCreated_Info():TimeStampField			{return Segments_Info.getInstance().DateCreated_Info;}
		public function get DateModified_Info():TimeStampField			{return Segments_Info.getInstance().DateModified_Info;}
		public function get RecordOwner_Info():UserIdField				{return Segments_Info.getInstance().RecordOwner_Info;}
		public function get LastModifiedBy_Info():UserIdField			{return Segments_Info.getInstance().LastModifiedBy_Info;}

	}
}
