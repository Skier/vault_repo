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
	import com.ebs.eroof.business.fieldmaps.Histories_FieldMap;
	import mx.collections.ArrayCollection;

	[Bindable]
	public class Histories_DTO extends KingussieDTOBase implements IValueObject, IKingussieDTO
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		// Record Owner Dirty Bit
		public function get IDNIsRecordOwnerDirty():Boolean				{return _fields["_isRecordOwnerDirty"];}


		// Current value getters
		public function get rid():String								{return _fields["_rid"];}
		public function get DateCompleted():Date						{return _fields["_DateCompleted"];}
		public function get DateCompleted_isDirty():Boolean				{return _fields["_DateCompleted_isDirty"];}
		public function get TypeOfWork():String							{return _fields["_TypeOfWork"];}
		public function get Company():String							{return _fields["_Company"];}
		public function get Company_isDirty():Boolean					{return _fields["_Company_isDirty"];}
		public function get Allocation():String							{return _fields["_Allocation"];}
		public function get Status():String								{return _fields["_Status"];}
		public function get ActualCost():Number							{return _fields["_ActualCost"];}
		public function get ActualCost_isDirty():Boolean				{return _fields["_ActualCost_isDirty"];}
		public function get Details():String							{return _fields["_Details"];}
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
		public function get Report():QuickBaseFileDTO {
			if(_fields["_Report"] == null) {
				_fields["_Report"] = new QuickBaseFileDTO();
			}
			return _fields["_Report"];
		}
		public function get Thumbnail():String							{return _fields["_Thumbnail"];}
		public function get Thumbnail_isDirty():Boolean					{return _fields["_Thumbnail_isDirty"];}
		public function get Annotation():String							{return _fields["_Annotation"];}
		public function get Annotation_isDirty():Boolean				{return _fields["_Annotation_isDirty"];}
		public function get AllowedClientUser():QuickBaseUserDTO 		{return _fields["_AllowedClientUser"];}
		public function get AllowedClientUser_isDirty():Boolean			{return _fields["_AllowedClientUser_isDirty"];}
		public function get AllowedContractorUser():QuickBaseUserDTO 	{return _fields["_AllowedContractorUser"];}
		public function get AllowedContractorUser_isDirty():Boolean		{return _fields["_AllowedContractorUser_isDirty"];}
		public function get AllowedInspectorUser():QuickBaseUserDTO 	{return _fields["_AllowedInspectorUser"];}
		public function get AllowedInspectorUser_isDirty():Boolean		{return _fields["_AllowedInspectorUser_isDirty"];}
		public function get DateCreated():Date							{return _fields["_DateCreated"];}
		public function get DateCreated_isDirty():Boolean				{return _fields["_DateCreated_isDirty"];}
		public function get DateModified():Date							{return _fields["_DateModified"];}
		public function get DateModified_isDirty():Boolean				{return _fields["_DateModified_isDirty"];}
		public function get RecordOwner():QuickBaseUserDTO 				{return _fields["_RecordOwner"];}
		public function get RecordOwner_isDirty():Boolean					{return _fields["_RecordOwner_isDirty"];}
		public function get LastModifiedBy():QuickBaseUserDTO 			{return _fields["_LastModifiedBy"];}
		public function get LastModifiedBy_isDirty():Boolean				{return _fields["_LastModifiedBy_isDirty"];}

		// Choice getters
		public function get TypeOfWorkChoices():ArrayCollection			{return TypeOfWork_Info.choiceArray;}
		public function get AllocationChoices():ArrayCollection			{return Allocation_Info.choiceArray;}
		public function get StatusChoices():ArrayCollection				{return Status_Info.choiceArray;}
		public function get DetailsChoices():ArrayCollection			{return Details_Info.choiceArray;}

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
		public function set DateCompleted(val:Date):void				{_fields["_DateCompleted"] = val; _fields["_DateCompleted_isDirty"] = true;}
		public function set TypeOfWork(val:String):void
		{
			if (TypeOfWork_Info.validate(val)) {
				_fields["_TypeOfWork"] = val;
				_fields["_TypeOfWork_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + TypeOfWork_Info.label + " value requested: " + val);
			}
		}

		public function set Company(val:String):void					{_fields["_Company"] = val; _fields["_Company_isDirty"] = true;}
		public function set Allocation(val:String):void
		{
			if (Allocation_Info.validate(val)) {
				_fields["_Allocation"] = val;
				_fields["_Allocation_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + Allocation_Info.label + " value requested: " + val);
			}
		}

		public function set Status(val:String):void
		{
			if (Status_Info.validate(val)) {
				_fields["_Status"] = val;
				_fields["_Status_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + Status_Info.label + " value requested: " + val);
			}
		}

		public function set ActualCost(val:Number):void					{_fields["_ActualCost"] = val; _fields["_ActualCost_isDirty"] = true;}
		public function set Details(val:String):void
		{
			if (Details_Info.validate(val)) {
				_fields["_Details"] = val;
				_fields["_Details_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + Details_Info.label + " value requested: " + val);
			}
		}

		public function set RelatedSection(val:Number):void				{_fields["_RelatedSection"] = val; _fields["_RelatedSection_isDirty"] = true;}
		public function set Annotation(val:String):void					{_fields["_Annotation"] = val; _fields["_Annotation_isDirty"] = true;}

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
		private function set IDNDateCompleted(val:String):void			{_fields["_DateCompleted"] = new Date(Number(val));}
		private function set IDNTypeOfWork(val:String):void				{_fields["_TypeOfWork"] = val;}
		private function set IDNCompany(val:String):void				{_fields["_Company"] = val;}
		private function set IDNAllocation(val:String):void				{_fields["_Allocation"] = val;}
		private function set IDNStatus(val:String):void					{_fields["_Status"] = val;}
		private function set IDNActualCost(val:String):void				{_fields["_ActualCost"] = Number(val);}
		private function set IDNDetails(val:String):void				{_fields["_Details"] = val;}
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
		private function set IDNReport(val:String):void {
			if(_fields["_Report"] == null) {
				_fields["_Report"] = new QuickBaseFileDTO();
			}
			_fields["_Report"].url = val;
		}
		private function set IDNThumbnail(val:String):void				{_fields["_Thumbnail"] = val;}
		private function set IDNAnnotation(val:String):void				{_fields["_Annotation"] = val;}
		private function set IDNAllowedClientUser(val:String):void
		{
			if (val == null) return;
			var model:QuickBaseMSAModel = QuickBaseMSAModel.getInstance();
			_fields["_AllowedClientUser"] = model.appUserList.findUserID(val);
		}
		private function set IDNAllowedContractorUser(val:String):void
		{
			if (val == null) return;
			var model:QuickBaseMSAModel = QuickBaseMSAModel.getInstance();
			_fields["_AllowedContractorUser"] = model.appUserList.findUserID(val);
		}
		private function set IDNAllowedInspectorUser(val:String):void
		{
			if (val == null) return;
			var model:QuickBaseMSAModel = QuickBaseMSAModel.getInstance();
			_fields["_AllowedInspectorUser"] = model.appUserList.findUserID(val);
		}
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
		public function getInfoObj():IKingussieInfo						{return Histories_Info.getInstance();}
		public function getFieldMapObj():IQuickBaseFieldMap				{return new Histories_FieldMap();}
		public function getFieldMapClass():Class						{return Histories_FieldMap;}
		public function get dtoClass():Class							{return Histories_DTO;}

		// MetaData Information Objects getters
		public function get DateCompleted_Info():DateField				{return Histories_Info.getInstance().DateCompleted_Info;}
		public function get TypeOfWork_Info():ChoiceField				{return Histories_Info.getInstance().TypeOfWork_Info;}
		public function get Company_Info():TextField					{return Histories_Info.getInstance().Company_Info;}
		public function get Allocation_Info():ChoiceField				{return Histories_Info.getInstance().Allocation_Info;}
		public function get Status_Info():ChoiceField					{return Histories_Info.getInstance().Status_Info;}
		public function get ActualCost_Info():CurrencyField				{return Histories_Info.getInstance().ActualCost_Info;}
		public function get Details_Info():ChoiceField					{return Histories_Info.getInstance().Details_Info;}
		public function get RelatedSection_Info():NumberField			{return Histories_Info.getInstance().RelatedSection_Info;}
		public function get Client_Info():TextField						{return Histories_Info.getInstance().Client_Info;}
		public function get Facility_Info():TextField					{return Histories_Info.getInstance().Facility_Info;}
		public function get Roof_Info():TextField						{return Histories_Info.getInstance().Roof_Info;}
		public function get Photo_Info():FileField						{return Histories_Info.getInstance().Photo_Info;}
		public function get Report_Info():FileField						{return Histories_Info.getInstance().Report_Info;}
		public function get Thumbnail_Info():TextField					{return Histories_Info.getInstance().Thumbnail_Info;}
		public function get Annotation_Info():TextField					{return Histories_Info.getInstance().Annotation_Info;}
		public function get AllowedClientUser_Info():UserIdField		{return Histories_Info.getInstance().AllowedClientUser_Info;}
		public function get AllowedContractorUser_Info():UserIdField	{return Histories_Info.getInstance().AllowedContractorUser_Info;}
		public function get AllowedInspectorUser_Info():UserIdField		{return Histories_Info.getInstance().AllowedInspectorUser_Info;}
		public function get DateCreated_Info():TimeStampField			{return Histories_Info.getInstance().DateCreated_Info;}
		public function get DateModified_Info():TimeStampField			{return Histories_Info.getInstance().DateModified_Info;}
		public function get RecordOwner_Info():UserIdField				{return Histories_Info.getInstance().RecordOwner_Info;}
		public function get LastModifiedBy_Info():UserIdField			{return Histories_Info.getInstance().LastModifiedBy_Info;}

	}
}
