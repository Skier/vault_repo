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
	import com.ebs.eroof.business.fieldmaps.Consultant_FieldMap;
	import mx.collections.ArrayCollection;

	[Bindable]
	public class Consultant_DTO extends KingussieDTOBase implements IValueObject, IKingussieDTO
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		// Record Owner Dirty Bit
		public function get IDNIsRecordOwnerDirty():Boolean				{return _fields["_isRecordOwnerDirty"];}


		// Current value getters
		public function get rid():String								{return _fields["_rid"];}
		public function get CompanyName():String						{return _fields["_CompanyName"];}
		public function get CompanyName_isDirty():Boolean				{return _fields["_CompanyName_isDirty"];}
		public function get Contact():String							{return _fields["_Contact"];}
		public function get Contact_isDirty():Boolean					{return _fields["_Contact_isDirty"];}
		public function get Address():String							{return _fields["_Address"];}
		public function get Address_isDirty():Boolean					{return _fields["_Address_isDirty"];}
		public function get ReportBanner():QuickBaseFileDTO {
			if(_fields["_ReportBanner"] == null) {
				_fields["_ReportBanner"] = new QuickBaseFileDTO();
			}
			return _fields["_ReportBanner"];
		}
		public function get MapLatLong():String							{return _fields["_MapLatLong"];}
		public function get MapLatLong_isDirty():Boolean				{return _fields["_MapLatLong_isDirty"];}
		public function get MapZoom():Number							{return _fields["_MapZoom"];}
		public function get MapZoom_isDirty():Boolean					{return _fields["_MapZoom_isDirty"];}
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
		public function set CompanyName(val:String):void				{_fields["_CompanyName"] = val; _fields["_CompanyName_isDirty"] = true;}
		public function set Contact(val:String):void					{_fields["_Contact"] = val; _fields["_Contact_isDirty"] = true;}
		public function set Address(val:String):void					{_fields["_Address"] = val; _fields["_Address_isDirty"] = true;}
		public function set MapLatLong(val:String):void					{_fields["_MapLatLong"] = val; _fields["_MapLatLong_isDirty"] = true;}
		public function set MapZoom(val:Number):void					{_fields["_MapZoom"] = val; _fields["_MapZoom_isDirty"] = true;}

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
		private function set IDNCompanyName(val:String):void			{_fields["_CompanyName"] = val;}
		private function set IDNContact(val:String):void				{_fields["_Contact"] = val;}
		private function set IDNAddress(val:String):void				{_fields["_Address"] = val;}
		private function set IDNReportBanner(val:String):void {
			if(_fields["_ReportBanner"] == null) {
				_fields["_ReportBanner"] = new QuickBaseFileDTO();
			}
			_fields["_ReportBanner"].url = val;
		}
		private function set IDNMapLatLong(val:String):void				{_fields["_MapLatLong"] = val;}
		private function set IDNMapZoom(val:String):void				{_fields["_MapZoom"] = Number(val);}
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
		public function getInfoObj():IKingussieInfo						{return Consultant_Info.getInstance();}
		public function getFieldMapObj():IQuickBaseFieldMap				{return new Consultant_FieldMap();}
		public function getFieldMapClass():Class						{return Consultant_FieldMap;}
		public function get dtoClass():Class							{return Consultant_DTO;}

		// MetaData Information Objects getters
		public function get CompanyName_Info():TextField				{return Consultant_Info.getInstance().CompanyName_Info;}
		public function get Contact_Info():TextField					{return Consultant_Info.getInstance().Contact_Info;}
		public function get Address_Info():TextField					{return Consultant_Info.getInstance().Address_Info;}
		public function get ReportBanner_Info():FileField				{return Consultant_Info.getInstance().ReportBanner_Info;}
		public function get MapLatLong_Info():TextField					{return Consultant_Info.getInstance().MapLatLong_Info;}
		public function get MapZoom_Info():NumberField					{return Consultant_Info.getInstance().MapZoom_Info;}
		public function get DateCreated_Info():TimeStampField			{return Consultant_Info.getInstance().DateCreated_Info;}
		public function get DateModified_Info():TimeStampField			{return Consultant_Info.getInstance().DateModified_Info;}
		public function get RecordOwner_Info():UserIdField				{return Consultant_Info.getInstance().RecordOwner_Info;}
		public function get LastModifiedBy_Info():UserIdField			{return Consultant_Info.getInstance().LastModifiedBy_Info;}

	}
}
