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
	import com.ebs.eroof.business.fieldmaps.Fields_FieldMap;
	import mx.collections.ArrayCollection;

	[Bindable]
	public class Fields_DTO extends KingussieDTOBase implements IValueObject, IKingussieDTO
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		// Record Owner Dirty Bit
		public function get IDNIsRecordOwnerDirty():Boolean				{return _fields["_isRecordOwnerDirty"];}


		// Current value getters
		public function get rid():String								{return _fields["_rid"];}
		public function get RelatedTable():Number						{return _fields["_RelatedTable"];}
		public function get RelatedTable_isDirty():Boolean				{return _fields["_RelatedTable_isDirty"];}
		public function get Table():String								{return _fields["_Table"];}
		public function get Table_isDirty():Boolean						{return _fields["_Table_isDirty"];}
		public function get SeqAndName():String							{return _fields["_SeqAndName"];}
		public function get SeqAndName_isDirty():Boolean				{return _fields["_SeqAndName_isDirty"];}
		public function get DBID():String								{return _fields["_DBID"];}
		public function get DBID_isDirty():Boolean						{return _fields["_DBID_isDirty"];}
		public function get Seq():Number								{return _fields["_Seq"];}
		public function get Seq_isDirty():Boolean						{return _fields["_Seq_isDirty"];}
		public function get Seq2():String								{return _fields["_Seq2"];}
		public function get Seq2_isDirty():Boolean						{return _fields["_Seq2_isDirty"];}
		public function get Label():String								{return _fields["_Label"];}
		public function get Label_isDirty():Boolean						{return _fields["_Label_isDirty"];}
		public function get Type():String								{return _fields["_Type"];}
		public function get FieldID():Number							{return _fields["_FieldID"];}
		public function get FieldID_isDirty():Boolean					{return _fields["_FieldID_isDirty"];}
		public function get Set():String								{return _fields["_Set"];}
		public function get Set_isDirty():Boolean						{return _fields["_Set_isDirty"];}
		public function get IsReqd():Boolean							{return _fields["_IsReqd"];}
		public function get IsReqd_isDirty():Boolean					{return _fields["_IsReqd_isDirty"];}
		public function get IsWrap():Boolean							{return _fields["_IsWrap"];}
		public function get IsWrap_isDirty():Boolean					{return _fields["_IsWrap_isDirty"];}
		public function get Col():Number								{return _fields["_Col"];}
		public function get Col_isDirty():Boolean						{return _fields["_Col_isDirty"];}
		public function get Default():String							{return _fields["_Default"];}
		public function get Default_isDirty():Boolean					{return _fields["_Default_isDirty"];}
		public function get IsUnique():Boolean							{return _fields["_IsUnique"];}
		public function get IsUnique_isDirty():Boolean					{return _fields["_IsUnique_isDirty"];}
		public function get T():String									{return _fields["_T"];}
		public function get Lines():Number								{return _fields["_Lines"];}
		public function get Lines_isDirty():Boolean						{return _fields["_Lines_isDirty"];}
		public function get Len():Number								{return _fields["_Len"];}
		public function get Len_isDirty():Boolean						{return _fields["_Len_isDirty"];}
		public function get IsApp():Boolean								{return _fields["_IsApp"];}
		public function get IsApp_isDirty():Boolean						{return _fields["_IsApp_isDirty"];}
		public function get TxtWidth():Number							{return _fields["_TxtWidth"];}
		public function get TxtWidth_isDirty():Boolean					{return _fields["_TxtWidth_isDirty"];}
		public function get Entry():String								{return _fields["_Entry"];}
		public function get Entry_isDirty():Boolean						{return _fields["_Entry_isDirty"];}
		public function get IsMult():Boolean							{return _fields["_IsMult"];}
		public function get IsMult_isDirty():Boolean					{return _fields["_IsMult_isDirty"];}
		public function get IsNew():Boolean								{return _fields["_IsNew"];}
		public function get IsNew_isDirty():Boolean						{return _fields["_IsNew_isDirty"];}
		public function get IsSort():Boolean							{return _fields["_IsSort"];}
		public function get IsSort_isDirty():Boolean					{return _fields["_IsSort_isDirty"];}
		public function get IsHTML():Boolean							{return _fields["_IsHTML"];}
		public function get IsHTML_isDirty():Boolean					{return _fields["_IsHTML_isDirty"];}
		public function get N():String									{return _fields["_N"];}
		public function get Units():String								{return _fields["_Units"];}
		public function get Units_isDirty():Boolean						{return _fields["_Units_isDirty"];}
		public function get NumWidth():Number							{return _fields["_NumWidth"];}
		public function get NumWidth_isDirty():Boolean					{return _fields["_NumWidth_isDirty"];}
		public function get TreatAs():String							{return _fields["_TreatAs"];}
		public function get TreatAs_isDirty():Boolean					{return _fields["_TreatAs_isDirty"];}
		public function get Dec():Number								{return _fields["_Dec"];}
		public function get Dec_isDirty():Boolean						{return _fields["_Dec_isDirty"];}
		public function get IsTotals():Boolean							{return _fields["_IsTotals"];}
		public function get IsTotals_isDirty():Boolean					{return _fields["_IsTotals_isDirty"];}
		public function get IsAvg():Boolean								{return _fields["_IsAvg"];}
		public function get IsAvg_isDirty():Boolean						{return _fields["_IsAvg_isDirty"];}
		public function get D():String									{return _fields["_D"];}
		public function get IsAlpha():Boolean							{return _fields["_IsAlpha"];}
		public function get IsAlpha_isDirty():Boolean					{return _fields["_IsAlpha_isDirty"];}
		public function get IsSmartDate():Boolean						{return _fields["_IsSmartDate"];}
		public function get IsSmartDate_isDirty():Boolean				{return _fields["_IsSmartDate_isDirty"];}
		public function get DateType():String							{return _fields["_DateType"];}
		public function get DateType_isDirty():Boolean					{return _fields["_DateType_isDirty"];}
		public function get Text():String								{return _fields["_Text"];}
		public function get Text_isDirty():Boolean						{return _fields["_Text_isDirty"];}
		public function get IsJPG():Boolean								{return _fields["_IsJPG"];}
		public function get IsJPG_isDirty():Boolean						{return _fields["_IsJPG_isDirty"];}
		public function get Revs():Number								{return _fields["_Revs"];}
		public function get Revs_isDirty():Boolean						{return _fields["_Revs_isDirty"];}
		public function get test():String								{return _fields["_test"];}
		public function get test_isDirty():Boolean						{return _fields["_test_isDirty"];}
		public function get IsUpdateable():Boolean						{return _fields["_IsUpdateable"];}
		public function get IsUpdateable_isDirty():Boolean				{return _fields["_IsUpdateable_isDirty"];}
		public function get IsText():Boolean							{return _fields["_IsText"];}
		public function get IsText_isDirty():Boolean					{return _fields["_IsText_isDirty"];}
		public function get IsNumeric():Boolean							{return _fields["_IsNumeric"];}
		public function get IsNumeric_isDirty():Boolean					{return _fields["_IsNumeric_isDirty"];}
		public function get IsDate():Boolean							{return _fields["_IsDate"];}
		public function get IsDate_isDirty():Boolean					{return _fields["_IsDate_isDirty"];}
		public function get Eqn():String								{return _fields["_Eqn"];}
		public function get Eqn_isDirty():Boolean						{return _fields["_Eqn_isDirty"];}
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
		public function get TChoices():ArrayCollection					{return T_Info.choiceArray;}
		public function get NChoices():ArrayCollection					{return N_Info.choiceArray;}
		public function get DChoices():ArrayCollection					{return D_Info.choiceArray;}

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
		public function set Seq(val:Number):void						{_fields["_Seq"] = val; _fields["_Seq_isDirty"] = true;}
		public function set Label(val:String):void						{_fields["_Label"] = val; _fields["_Label_isDirty"] = true;}
		public function set Type(val:String):void
		{
			if (Type_Info.validate(val)) {
				_fields["_Type"] = val;
				_fields["_Type_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + Type_Info.label + " value requested: " + val);
			}
		}

		public function set FieldID(val:Number):void					{_fields["_FieldID"] = val; _fields["_FieldID_isDirty"] = true;}
		public function set IsReqd(val:Boolean):void					{_fields["_IsReqd"] = val; _fields["_IsReqd_isDirty"] = true;}
		public function set IsWrap(val:Boolean):void					{_fields["_IsWrap"] = val; _fields["_IsWrap_isDirty"] = true;}
		public function set Col(val:Number):void						{_fields["_Col"] = val; _fields["_Col_isDirty"] = true;}
		public function set Default(val:String):void					{_fields["_Default"] = val; _fields["_Default_isDirty"] = true;}
		public function set IsUnique(val:Boolean):void					{_fields["_IsUnique"] = val; _fields["_IsUnique_isDirty"] = true;}
		public function set T(val:String):void
		{
			if (T_Info.validate(val)) {
				_fields["_T"] = val;
				_fields["_T_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + T_Info.label + " value requested: " + val);
			}
		}

		public function set Lines(val:Number):void						{_fields["_Lines"] = val; _fields["_Lines_isDirty"] = true;}
		public function set Len(val:Number):void						{_fields["_Len"] = val; _fields["_Len_isDirty"] = true;}
		public function set IsApp(val:Boolean):void						{_fields["_IsApp"] = val; _fields["_IsApp_isDirty"] = true;}
		public function set TxtWidth(val:Number):void					{_fields["_TxtWidth"] = val; _fields["_TxtWidth_isDirty"] = true;}
		public function set Entry(val:String):void						{_fields["_Entry"] = val; _fields["_Entry_isDirty"] = true;}
		public function set IsMult(val:Boolean):void					{_fields["_IsMult"] = val; _fields["_IsMult_isDirty"] = true;}
		public function set IsNew(val:Boolean):void						{_fields["_IsNew"] = val; _fields["_IsNew_isDirty"] = true;}
		public function set IsSort(val:Boolean):void					{_fields["_IsSort"] = val; _fields["_IsSort_isDirty"] = true;}
		public function set IsHTML(val:Boolean):void					{_fields["_IsHTML"] = val; _fields["_IsHTML_isDirty"] = true;}
		public function set N(val:String):void
		{
			if (N_Info.validate(val)) {
				_fields["_N"] = val;
				_fields["_N_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + N_Info.label + " value requested: " + val);
			}
		}

		public function set Units(val:String):void						{_fields["_Units"] = val; _fields["_Units_isDirty"] = true;}
		public function set NumWidth(val:Number):void					{_fields["_NumWidth"] = val; _fields["_NumWidth_isDirty"] = true;}
		public function set TreatAs(val:String):void					{_fields["_TreatAs"] = val; _fields["_TreatAs_isDirty"] = true;}
		public function set Dec(val:Number):void						{_fields["_Dec"] = val; _fields["_Dec_isDirty"] = true;}
		public function set IsTotals(val:Boolean):void					{_fields["_IsTotals"] = val; _fields["_IsTotals_isDirty"] = true;}
		public function set IsAvg(val:Boolean):void						{_fields["_IsAvg"] = val; _fields["_IsAvg_isDirty"] = true;}
		public function set D(val:String):void
		{
			if (D_Info.validate(val)) {
				_fields["_D"] = val;
				_fields["_D_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + D_Info.label + " value requested: " + val);
			}
		}

		public function set IsAlpha(val:Boolean):void					{_fields["_IsAlpha"] = val; _fields["_IsAlpha_isDirty"] = true;}
		public function set IsSmartDate(val:Boolean):void				{_fields["_IsSmartDate"] = val; _fields["_IsSmartDate_isDirty"] = true;}
		public function set DateType(val:String):void					{_fields["_DateType"] = val; _fields["_DateType_isDirty"] = true;}
		public function set Text(val:String):void						{_fields["_Text"] = val; _fields["_Text_isDirty"] = true;}
		public function set IsJPG(val:Boolean):void						{_fields["_IsJPG"] = val; _fields["_IsJPG_isDirty"] = true;}
		public function set Revs(val:Number):void						{_fields["_Revs"] = val; _fields["_Revs_isDirty"] = true;}
		public function set IsConsultant(val:Boolean):void				{_fields["_IsConsultant"] = val; _fields["_IsConsultant_isDirty"] = true;}
		public function set IsClient(val:Boolean):void					{_fields["_IsClient"] = val; _fields["_IsClient_isDirty"] = true;}
		public function set IsContractor(val:Boolean):void				{_fields["_IsContractor"] = val; _fields["_IsContractor_isDirty"] = true;}
		public function set IsInspector(val:Boolean):void				{_fields["_IsInspector"] = val; _fields["_IsInspector_isDirty"] = true;}
		public function set IRPSource(val:String):void					{_fields["_IRPSource"] = val; _fields["_IRPSource_isDirty"] = true;}

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
		private function set IDNRelatedTable(val:String):void			{_fields["_RelatedTable"] = Number(val);}
		private function set IDNTable(val:String):void					{_fields["_Table"] = val;}
		private function set IDNSeqAndName(val:String):void				{_fields["_SeqAndName"] = val;}
		private function set IDNDBID(val:String):void					{_fields["_DBID"] = val;}
		private function set IDNSeq(val:String):void					{_fields["_Seq"] = Number(val);}
		private function set IDNSeq2(val:String):void					{_fields["_Seq2"] = val;}
		private function set IDNLabel(val:String):void					{_fields["_Label"] = val;}
		private function set IDNType(val:String):void					{_fields["_Type"] = val;}
		private function set IDNFieldID(val:String):void				{_fields["_FieldID"] = Number(val);}
		private function set IDNSet(val:String):void					{_fields["_Set"] = val;}
		private function set IDNIsReqd(val:String):void					{_fields["_IsReqd"] = Boolean(Number(val));}
		private function set IDNIsWrap(val:String):void					{_fields["_IsWrap"] = Boolean(Number(val));}
		private function set IDNCol(val:String):void					{_fields["_Col"] = Number(val);}
		private function set IDNDefault(val:String):void				{_fields["_Default"] = val;}
		private function set IDNIsUnique(val:String):void				{_fields["_IsUnique"] = Boolean(Number(val));}
		private function set IDNT(val:String):void						{_fields["_T"] = val;}
		private function set IDNLines(val:String):void					{_fields["_Lines"] = Number(val);}
		private function set IDNLen(val:String):void					{_fields["_Len"] = Number(val);}
		private function set IDNIsApp(val:String):void					{_fields["_IsApp"] = Boolean(Number(val));}
		private function set IDNTxtWidth(val:String):void				{_fields["_TxtWidth"] = Number(val);}
		private function set IDNEntry(val:String):void					{_fields["_Entry"] = val;}
		private function set IDNIsMult(val:String):void					{_fields["_IsMult"] = Boolean(Number(val));}
		private function set IDNIsNew(val:String):void					{_fields["_IsNew"] = Boolean(Number(val));}
		private function set IDNIsSort(val:String):void					{_fields["_IsSort"] = Boolean(Number(val));}
		private function set IDNIsHTML(val:String):void					{_fields["_IsHTML"] = Boolean(Number(val));}
		private function set IDNN(val:String):void						{_fields["_N"] = val;}
		private function set IDNUnits(val:String):void					{_fields["_Units"] = val;}
		private function set IDNNumWidth(val:String):void				{_fields["_NumWidth"] = Number(val);}
		private function set IDNTreatAs(val:String):void				{_fields["_TreatAs"] = val;}
		private function set IDNDec(val:String):void					{_fields["_Dec"] = Number(val);}
		private function set IDNIsTotals(val:String):void				{_fields["_IsTotals"] = Boolean(Number(val));}
		private function set IDNIsAvg(val:String):void					{_fields["_IsAvg"] = Boolean(Number(val));}
		private function set IDND(val:String):void						{_fields["_D"] = val;}
		private function set IDNIsAlpha(val:String):void				{_fields["_IsAlpha"] = Boolean(Number(val));}
		private function set IDNIsSmartDate(val:String):void			{_fields["_IsSmartDate"] = Boolean(Number(val));}
		private function set IDNDateType(val:String):void				{_fields["_DateType"] = val;}
		private function set IDNText(val:String):void					{_fields["_Text"] = val;}
		private function set IDNIsJPG(val:String):void					{_fields["_IsJPG"] = Boolean(Number(val));}
		private function set IDNRevs(val:String):void					{_fields["_Revs"] = Number(val);}
		private function set IDNtest(val:String):void					{_fields["_test"] = val;}
		private function set IDNIsUpdateable(val:String):void			{_fields["_IsUpdateable"] = Boolean(Number(val));}
		private function set IDNIsText(val:String):void					{_fields["_IsText"] = Boolean(Number(val));}
		private function set IDNIsNumeric(val:String):void				{_fields["_IsNumeric"] = Boolean(Number(val));}
		private function set IDNIsDate(val:String):void					{_fields["_IsDate"] = Boolean(Number(val));}
		private function set IDNEqn(val:String):void					{_fields["_Eqn"] = val;}
		private function set IDNIsConsultant(val:String):void			{_fields["_IsConsultant"] = Boolean(Number(val));}
		private function set IDNIsClient(val:String):void				{_fields["_IsClient"] = Boolean(Number(val));}
		private function set IDNIsContractor(val:String):void			{_fields["_IsContractor"] = Boolean(Number(val));}
		private function set IDNIsInspector(val:String):void			{_fields["_IsInspector"] = Boolean(Number(val));}
		private function set IDNIRPSource(val:String):void				{_fields["_IRPSource"] = val;}
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
		public function getInfoObj():IKingussieInfo						{return Fields_Info.getInstance();}
		public function getFieldMapObj():IQuickBaseFieldMap				{return new Fields_FieldMap();}
		public function getFieldMapClass():Class						{return Fields_FieldMap;}
		public function get dtoClass():Class							{return Fields_DTO;}

		// MetaData Information Objects getters
		public function get RelatedTable_Info():NumberField				{return Fields_Info.getInstance().RelatedTable_Info;}
		public function get Table_Info():TextField						{return Fields_Info.getInstance().Table_Info;}
		public function get SeqAndName_Info():TextField					{return Fields_Info.getInstance().SeqAndName_Info;}
		public function get DBID_Info():TextField						{return Fields_Info.getInstance().DBID_Info;}
		public function get Seq_Info():NumberField						{return Fields_Info.getInstance().Seq_Info;}
		public function get Seq2_Info():TextField						{return Fields_Info.getInstance().Seq2_Info;}
		public function get Label_Info():TextField						{return Fields_Info.getInstance().Label_Info;}
		public function get Type_Info():ChoiceField						{return Fields_Info.getInstance().Type_Info;}
		public function get FieldID_Info():NumberField					{return Fields_Info.getInstance().FieldID_Info;}
		public function get Set_Info():URLField							{return Fields_Info.getInstance().Set_Info;}
		public function get IsReqd_Info():BooleanField					{return Fields_Info.getInstance().IsReqd_Info;}
		public function get IsWrap_Info():BooleanField					{return Fields_Info.getInstance().IsWrap_Info;}
		public function get Col_Info():NumberField						{return Fields_Info.getInstance().Col_Info;}
		public function get Default_Info():TextField					{return Fields_Info.getInstance().Default_Info;}
		public function get IsUnique_Info():BooleanField				{return Fields_Info.getInstance().IsUnique_Info;}
		public function get T_Info():ChoiceField						{return Fields_Info.getInstance().T_Info;}
		public function get Lines_Info():NumberField					{return Fields_Info.getInstance().Lines_Info;}
		public function get Len_Info():NumberField						{return Fields_Info.getInstance().Len_Info;}
		public function get IsApp_Info():BooleanField					{return Fields_Info.getInstance().IsApp_Info;}
		public function get TxtWidth_Info():NumberField					{return Fields_Info.getInstance().TxtWidth_Info;}
		public function get Entry_Info():TextField						{return Fields_Info.getInstance().Entry_Info;}
		public function get IsMult_Info():BooleanField					{return Fields_Info.getInstance().IsMult_Info;}
		public function get IsNew_Info():BooleanField					{return Fields_Info.getInstance().IsNew_Info;}
		public function get IsSort_Info():BooleanField					{return Fields_Info.getInstance().IsSort_Info;}
		public function get IsHTML_Info():BooleanField					{return Fields_Info.getInstance().IsHTML_Info;}
		public function get N_Info():ChoiceField						{return Fields_Info.getInstance().N_Info;}
		public function get Units_Info():TextField						{return Fields_Info.getInstance().Units_Info;}
		public function get NumWidth_Info():NumberField					{return Fields_Info.getInstance().NumWidth_Info;}
		public function get TreatAs_Info():TextField					{return Fields_Info.getInstance().TreatAs_Info;}
		public function get Dec_Info():NumberField						{return Fields_Info.getInstance().Dec_Info;}
		public function get IsTotals_Info():BooleanField				{return Fields_Info.getInstance().IsTotals_Info;}
		public function get IsAvg_Info():BooleanField					{return Fields_Info.getInstance().IsAvg_Info;}
		public function get D_Info():ChoiceField						{return Fields_Info.getInstance().D_Info;}
		public function get IsAlpha_Info():BooleanField					{return Fields_Info.getInstance().IsAlpha_Info;}
		public function get IsSmartDate_Info():BooleanField				{return Fields_Info.getInstance().IsSmartDate_Info;}
		public function get DateType_Info():TextField					{return Fields_Info.getInstance().DateType_Info;}
		public function get Text_Info():TextField						{return Fields_Info.getInstance().Text_Info;}
		public function get IsJPG_Info():BooleanField					{return Fields_Info.getInstance().IsJPG_Info;}
		public function get Revs_Info():NumberField						{return Fields_Info.getInstance().Revs_Info;}
		public function get test_Info():TextField						{return Fields_Info.getInstance().test_Info;}
		public function get IsUpdateable_Info():BooleanField			{return Fields_Info.getInstance().IsUpdateable_Info;}
		public function get IsText_Info():BooleanField					{return Fields_Info.getInstance().IsText_Info;}
		public function get IsNumeric_Info():BooleanField				{return Fields_Info.getInstance().IsNumeric_Info;}
		public function get IsDate_Info():BooleanField					{return Fields_Info.getInstance().IsDate_Info;}
		public function get Eqn_Info():TextField						{return Fields_Info.getInstance().Eqn_Info;}
		public function get IsConsultant_Info():BooleanField			{return Fields_Info.getInstance().IsConsultant_Info;}
		public function get IsClient_Info():BooleanField				{return Fields_Info.getInstance().IsClient_Info;}
		public function get IsContractor_Info():BooleanField			{return Fields_Info.getInstance().IsContractor_Info;}
		public function get IsInspector_Info():BooleanField				{return Fields_Info.getInstance().IsInspector_Info;}
		public function get IRPSource_Info():TextField					{return Fields_Info.getInstance().IRPSource_Info;}
		public function get DateCreated_Info():TimeStampField			{return Fields_Info.getInstance().DateCreated_Info;}
		public function get DateModified_Info():TimeStampField			{return Fields_Info.getInstance().DateModified_Info;}
		public function get RecordOwner_Info():UserIdField				{return Fields_Info.getInstance().RecordOwner_Info;}
		public function get LastModifiedBy_Info():UserIdField			{return Fields_Info.getInstance().LastModifiedBy_Info;}

	}
}
