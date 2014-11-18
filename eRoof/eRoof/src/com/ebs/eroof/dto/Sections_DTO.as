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
	import com.ebs.eroof.business.fieldmaps.Sections_FieldMap;
	import mx.collections.ArrayCollection;

	[Bindable]
	public class Sections_DTO extends KingussieDTOBase implements IValueObject, IKingussieDTO
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
		public function get Designation():String						{return _fields["_Designation"];}
		public function get Designation_isDirty():Boolean				{return _fields["_Designation_isDirty"];}
		public function get RoofName():String							{return _fields["_RoofName"];}
		public function get RoofSystem():String							{return _fields["_RoofSystem"];}
		public function get Age():Number								{return _fields["_Age"];}
		public function get Age_isDirty():Boolean						{return _fields["_Age_isDirty"];}
		public function get ConditionIndex():String						{return _fields["_ConditionIndex"];}
		public function get SqFt():Number								{return _fields["_SqFt"];}
		public function get SqFt_isDirty():Boolean						{return _fields["_SqFt_isDirty"];}
		public function get EstCost():Number							{return _fields["_EstCost"];}
		public function get EstCost_isDirty():Boolean					{return _fields["_EstCost_isDirty"];}
		public function get RelatedFacility():Number					{return _fields["_RelatedFacility"];}
		public function get RelatedFacility_isDirty():Boolean			{return _fields["_RelatedFacility_isDirty"];}
		public function get AddLayer():String							{return _fields["_AddLayer"];}
		public function get AddLayer_isDirty():Boolean					{return _fields["_AddLayer_isDirty"];}
		public function get Layers():String								{return _fields["_Layers"];}
		public function get Layers_isDirty():Boolean					{return _fields["_Layers_isDirty"];}
		public function get AddCorePhoto():String						{return _fields["_AddCorePhoto"];}
		public function get AddCorePhoto_isDirty():Boolean				{return _fields["_AddCorePhoto_isDirty"];}
		public function get CorePhotos():String							{return _fields["_CorePhotos"];}
		public function get CorePhotos_isDirty():Boolean				{return _fields["_CorePhotos_isDirty"];}
		public function get AddDetail():String							{return _fields["_AddDetail"];}
		public function get AddDetail_isDirty():Boolean					{return _fields["_AddDetail_isDirty"];}
		public function get Details():String							{return _fields["_Details"];}
		public function get Details_isDirty():Boolean					{return _fields["_Details_isDirty"];}
		public function get AddDefect():String							{return _fields["_AddDefect"];}
		public function get AddDefect_isDirty():Boolean					{return _fields["_AddDefect_isDirty"];}
		public function get Defects():String							{return _fields["_Defects"];}
		public function get Defects_isDirty():Boolean					{return _fields["_Defects_isDirty"];}
		public function get AddInspection():String						{return _fields["_AddInspection"];}
		public function get AddInspection_isDirty():Boolean				{return _fields["_AddInspection_isDirty"];}
		public function get Inspections():String						{return _fields["_Inspections"];}
		public function get Inspections_isDirty():Boolean				{return _fields["_Inspections_isDirty"];}
		public function get AddSurvey():String							{return _fields["_AddSurvey"];}
		public function get AddSurvey_isDirty():Boolean					{return _fields["_AddSurvey_isDirty"];}
		public function get Surveys():String							{return _fields["_Surveys"];}
		public function get Surveys_isDirty():Boolean					{return _fields["_Surveys_isDirty"];}
		public function get AddExpenditure():String						{return _fields["_AddExpenditure"];}
		public function get AddExpenditure_isDirty():Boolean			{return _fields["_AddExpenditure_isDirty"];}
		public function get Expenditures():String						{return _fields["_Expenditures"];}
		public function get Expenditures_isDirty():Boolean				{return _fields["_Expenditures_isDirty"];}
		public function get AddWarranty():String						{return _fields["_AddWarranty"];}
		public function get AddWarranty_isDirty():Boolean				{return _fields["_AddWarranty_isDirty"];}
		public function get Warranties():String							{return _fields["_Warranties"];}
		public function get Warranties_isDirty():Boolean				{return _fields["_Warranties_isDirty"];}
		public function get Photo():QuickBaseFileDTO {
			if(_fields["_Photo"] == null) {
				_fields["_Photo"] = new QuickBaseFileDTO();
			}
			return _fields["_Photo"];
		}
		public function get PhotoName():String							{return _fields["_PhotoName"];}
		public function get PhotoName_isDirty():Boolean					{return _fields["_PhotoName_isDirty"];}
		public function get YearInstalled():Number						{return _fields["_YearInstalled"];}
		public function get YearInstalled_isDirty():Boolean				{return _fields["_YearInstalled_isDirty"];}
		public function get YearInstalledSource():String				{return _fields["_YearInstalledSource"];}
		public function get EstCostPerSqFt():Number						{return _fields["_EstCostPerSqFt"];}
		public function get EstCostPerSqFt_isDirty():Boolean			{return _fields["_EstCostPerSqFt_isDirty"];}
		public function get Height():Number								{return _fields["_Height"];}
		public function get Height_isDirty():Boolean					{return _fields["_Height_isDirty"];}
		public function get Slope():String								{return _fields["_Slope"];}
		public function get InteriorSensitivity():String				{return _fields["_InteriorSensitivity"];}
		public function get SensitivityDetails():String					{return _fields["_SensitivityDetails"];}
		public function get SensitivityDetails_isDirty():Boolean		{return _fields["_SensitivityDetails_isDirty"];}
		public function get ConditionDetails():String					{return _fields["_ConditionDetails"];}
		public function get ConditionDetails_isDirty():Boolean			{return _fields["_ConditionDetails_isDirty"];}
		public function get Restorable():String							{return _fields["_Restorable"];}
		public function get Drainage():String							{return _fields["_Drainage"];}
		public function get CurrentlyLeaking():String					{return _fields["_CurrentlyLeaking"];}
		public function get HistoryOfLeaking():String					{return _fields["_HistoryOfLeaking"];}
		public function get DrainageDetails():String					{return _fields["_DrainageDetails"];}
		public function get LeakDetails():String						{return _fields["_LeakDetails"];}
		public function get LeakDetails_isDirty():Boolean				{return _fields["_LeakDetails_isDirty"];}
		public function get X1():Number									{return _fields["_X1"];}
		public function get X1_isDirty():Boolean						{return _fields["_X1_isDirty"];}
		public function get Y1():Number									{return _fields["_Y1"];}
		public function get Y1_isDirty():Boolean						{return _fields["_Y1_isDirty"];}
		public function get X2():Number									{return _fields["_X2"];}
		public function get X2_isDirty():Boolean						{return _fields["_X2_isDirty"];}
		public function get Y2():Number									{return _fields["_Y2"];}
		public function get Y2_isDirty():Boolean						{return _fields["_Y2_isDirty"];}
		public function get OverallCoreCondition():String				{return _fields["_OverallCoreCondition"];}
		public function get OverallCoreCondition_isDirty():Boolean		{return _fields["_OverallCoreCondition_isDirty"];}
		public function get Notes():String								{return _fields["_Notes"];}
		public function get SectionUsers():String						{return _fields["_SectionUsers"];}
		public function get SectionUsers_isDirty():Boolean				{return _fields["_SectionUsers_isDirty"];}
		public function get AddSectionUser():String						{return _fields["_AddSectionUser"];}
		public function get AddSectionUser_isDirty():Boolean			{return _fields["_AddSectionUser_isDirty"];}
		public function get AllowedSectionUser():Number					{return _fields["_AllowedSectionUser"];}
		public function get AllowedSectionUser_isDirty():Boolean		{return _fields["_AllowedSectionUser_isDirty"];}
		public function get AllowedClientUser():Number					{return _fields["_AllowedClientUser"];}
		public function get AllowedClientUser_isDirty():Boolean			{return _fields["_AllowedClientUser_isDirty"];}
		public function get AllowedFacilityUser():Number				{return _fields["_AllowedFacilityUser"];}
		public function get AllowedFacilityUser_isDirty():Boolean		{return _fields["_AllowedFacilityUser_isDirty"];}
		public function get AllowedUser():Number						{return _fields["_AllowedUser"];}
		public function get AllowedUser_isDirty():Boolean				{return _fields["_AllowedUser_isDirty"];}
		public function get OldRoofsectionid():Number					{return _fields["_OldRoofsectionid"];}
		public function get OldRoofsectionid_isDirty():Boolean			{return _fields["_OldRoofsectionid_isDirty"];}
		public function get SectionLat():Number							{return _fields["_SectionLat"];}
		public function get SectionLat_isDirty():Boolean				{return _fields["_SectionLat_isDirty"];}
		public function get SectionLong():Number						{return _fields["_SectionLong"];}
		public function get SectionLong_isDirty():Boolean				{return _fields["_SectionLong_isDirty"];}
		public function get PhotoThumbnail():String						{return _fields["_PhotoThumbnail"];}
		public function get PhotoThumbnail_isDirty():Boolean			{return _fields["_PhotoThumbnail_isDirty"];}
		public function get QBMap():String								{return _fields["_QBMap"];}
		public function get QBMap_isDirty():Boolean						{return _fields["_QBMap_isDirty"];}
		public function get QBMapsMarker():String						{return _fields["_QBMapsMarker"];}
		public function get QBMapsMarker_isDirty():Boolean				{return _fields["_QBMapsMarker_isDirty"];}
		public function get QBMapsUrl():String							{return _fields["_QBMapsUrl"];}
		public function get QBMapsUrl_isDirty():Boolean					{return _fields["_QBMapsUrl_isDirty"];}
		public function get QBAddress():String							{return _fields["_QBAddress"];}
		public function get QBAddress_isDirty():Boolean					{return _fields["_QBAddress_isDirty"];}
		public function get CreateReport():String						{return _fields["_CreateReport"];}
		public function get CreateReport_isDirty():Boolean				{return _fields["_CreateReport_isDirty"];}
		public function get Report():QuickBaseFileDTO {
			if(_fields["_Report"] == null) {
				_fields["_Report"] = new QuickBaseFileDTO();
			}
			return _fields["_Report"];
		}
		public function get RoofPlanLatLong():String					{return _fields["_RoofPlanLatLong"];}
		public function get RoofPlanLatLong_isDirty():Boolean			{return _fields["_RoofPlanLatLong_isDirty"];}
		public function get RoofPlanZoom():Number						{return _fields["_RoofPlanZoom"];}
		public function get RoofPlanZoom_isDirty():Boolean				{return _fields["_RoofPlanZoom_isDirty"];}
		public function get Polygon():String							{return _fields["_Polygon"];}
		public function get Polygon_isDirty():Boolean					{return _fields["_Polygon_isDirty"];}
		public function get DateInstalled():Date						{return _fields["_DateInstalled"];}
		public function get DateInstalled_isDirty():Boolean				{return _fields["_DateInstalled_isDirty"];}
		public function get TestDoc():QuickBaseFileDTO {
			if(_fields["_TestDoc"] == null) {
				_fields["_TestDoc"] = new QuickBaseFileDTO();
			}
			return _fields["_TestDoc"];
		}
		public function get DateCreated():Date							{return _fields["_DateCreated"];}
		public function get DateCreated_isDirty():Boolean				{return _fields["_DateCreated_isDirty"];}
		public function get DateModified():Date							{return _fields["_DateModified"];}
		public function get DateModified_isDirty():Boolean				{return _fields["_DateModified_isDirty"];}
		public function get RecordOwner():QuickBaseUserDTO 				{return _fields["_RecordOwner"];}
		public function get RecordOwner_isDirty():Boolean					{return _fields["_RecordOwner_isDirty"];}
		public function get LastModifiedBy():QuickBaseUserDTO 			{return _fields["_LastModifiedBy"];}
		public function get LastModifiedBy_isDirty():Boolean				{return _fields["_LastModifiedBy_isDirty"];}

		// Choice getters
		public function get RoofNameChoices():ArrayCollection			{return RoofName_Info.choiceArray;}
		public function get RoofSystemChoices():ArrayCollection			{return RoofSystem_Info.choiceArray;}
		public function get ConditionIndexChoices():ArrayCollection		{return ConditionIndex_Info.choiceArray;}
		public function get YearInstalledSourceChoices():ArrayCollection{return YearInstalledSource_Info.choiceArray;}
		public function get SlopeChoices():ArrayCollection				{return Slope_Info.choiceArray;}
		public function get InteriorSensitivityChoices():ArrayCollection{return InteriorSensitivity_Info.choiceArray;}
		public function get RestorableChoices():ArrayCollection			{return Restorable_Info.choiceArray;}
		public function get DrainageChoices():ArrayCollection			{return Drainage_Info.choiceArray;}
		public function get CurrentlyLeakingChoices():ArrayCollection	{return CurrentlyLeaking_Info.choiceArray;}
		public function get HistoryOfLeakingChoices():ArrayCollection	{return HistoryOfLeaking_Info.choiceArray;}
		public function get DrainageDetailsChoices():ArrayCollection	{return DrainageDetails_Info.choiceArray;}
		public function get NotesChoices():ArrayCollection				{return Notes_Info.choiceArray;}

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
		public function set Designation(val:String):void				{_fields["_Designation"] = val; _fields["_Designation_isDirty"] = true;}
		public function set RoofName(val:String):void
		{
			if (RoofName_Info.validate(val)) {
				_fields["_RoofName"] = val;
				_fields["_RoofName_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + RoofName_Info.label + " value requested: " + val);
			}
		}

		public function set RoofSystem(val:String):void
		{
			if (RoofSystem_Info.validate(val)) {
				_fields["_RoofSystem"] = val;
				_fields["_RoofSystem_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + RoofSystem_Info.label + " value requested: " + val);
			}
		}

		public function set Age(val:Number):void						{_fields["_Age"] = val; _fields["_Age_isDirty"] = true;}
		public function set ConditionIndex(val:String):void
		{
			if (ConditionIndex_Info.validate(val)) {
				_fields["_ConditionIndex"] = val;
				_fields["_ConditionIndex_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + ConditionIndex_Info.label + " value requested: " + val);
			}
		}

		public function set SqFt(val:Number):void						{_fields["_SqFt"] = val; _fields["_SqFt_isDirty"] = true;}
		public function set RelatedFacility(val:Number):void			{_fields["_RelatedFacility"] = val; _fields["_RelatedFacility_isDirty"] = true;}
		public function set PhotoName(val:String):void					{_fields["_PhotoName"] = val; _fields["_PhotoName_isDirty"] = true;}
		public function set YearInstalled(val:Number):void				{_fields["_YearInstalled"] = val; _fields["_YearInstalled_isDirty"] = true;}
		public function set YearInstalledSource(val:String):void
		{
			if (YearInstalledSource_Info.validate(val)) {
				_fields["_YearInstalledSource"] = val;
				_fields["_YearInstalledSource_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + YearInstalledSource_Info.label + " value requested: " + val);
			}
		}

		public function set EstCostPerSqFt(val:Number):void				{_fields["_EstCostPerSqFt"] = val; _fields["_EstCostPerSqFt_isDirty"] = true;}
		public function set Height(val:Number):void						{_fields["_Height"] = val; _fields["_Height_isDirty"] = true;}
		public function set Slope(val:String):void
		{
			if (Slope_Info.validate(val)) {
				_fields["_Slope"] = val;
				_fields["_Slope_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + Slope_Info.label + " value requested: " + val);
			}
		}

		public function set InteriorSensitivity(val:String):void
		{
			if (InteriorSensitivity_Info.validate(val)) {
				_fields["_InteriorSensitivity"] = val;
				_fields["_InteriorSensitivity_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + InteriorSensitivity_Info.label + " value requested: " + val);
			}
		}

		public function set SensitivityDetails(val:String):void			{_fields["_SensitivityDetails"] = val; _fields["_SensitivityDetails_isDirty"] = true;}
		public function set ConditionDetails(val:String):void			{_fields["_ConditionDetails"] = val; _fields["_ConditionDetails_isDirty"] = true;}
		public function set Restorable(val:String):void
		{
			if (Restorable_Info.validate(val)) {
				_fields["_Restorable"] = val;
				_fields["_Restorable_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + Restorable_Info.label + " value requested: " + val);
			}
		}

		public function set Drainage(val:String):void
		{
			if (Drainage_Info.validate(val)) {
				_fields["_Drainage"] = val;
				_fields["_Drainage_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + Drainage_Info.label + " value requested: " + val);
			}
		}

		public function set CurrentlyLeaking(val:String):void
		{
			if (CurrentlyLeaking_Info.validate(val)) {
				_fields["_CurrentlyLeaking"] = val;
				_fields["_CurrentlyLeaking_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + CurrentlyLeaking_Info.label + " value requested: " + val);
			}
		}

		public function set HistoryOfLeaking(val:String):void
		{
			if (HistoryOfLeaking_Info.validate(val)) {
				_fields["_HistoryOfLeaking"] = val;
				_fields["_HistoryOfLeaking_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + HistoryOfLeaking_Info.label + " value requested: " + val);
			}
		}

		public function set DrainageDetails(val:String):void
		{
			if (DrainageDetails_Info.validate(val)) {
				_fields["_DrainageDetails"] = val;
				_fields["_DrainageDetails_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + DrainageDetails_Info.label + " value requested: " + val);
			}
		}

		public function set LeakDetails(val:String):void				{_fields["_LeakDetails"] = val; _fields["_LeakDetails_isDirty"] = true;}
		public function set X1(val:Number):void							{_fields["_X1"] = val; _fields["_X1_isDirty"] = true;}
		public function set Y1(val:Number):void							{_fields["_Y1"] = val; _fields["_Y1_isDirty"] = true;}
		public function set X2(val:Number):void							{_fields["_X2"] = val; _fields["_X2_isDirty"] = true;}
		public function set Y2(val:Number):void							{_fields["_Y2"] = val; _fields["_Y2_isDirty"] = true;}
		public function set OverallCoreCondition(val:String):void		{_fields["_OverallCoreCondition"] = val; _fields["_OverallCoreCondition_isDirty"] = true;}
		public function set Notes(val:String):void
		{
			if (Notes_Info.validate(val)) {
				_fields["_Notes"] = val;
				_fields["_Notes_isDirty"] = true;
			} else {
				throw new Error("Invalid value for choice field: " + Notes_Info.label + " value requested: " + val);
			}
		}

		public function set OldRoofsectionid(val:Number):void			{_fields["_OldRoofsectionid"] = val; _fields["_OldRoofsectionid_isDirty"] = true;}
		public function set SectionLat(val:Number):void					{_fields["_SectionLat"] = val; _fields["_SectionLat_isDirty"] = true;}
		public function set SectionLong(val:Number):void				{_fields["_SectionLong"] = val; _fields["_SectionLong_isDirty"] = true;}
		public function set QBAddress(val:String):void					{_fields["_QBAddress"] = val; _fields["_QBAddress_isDirty"] = true;}
		public function set RoofPlanLatLong(val:String):void			{_fields["_RoofPlanLatLong"] = val; _fields["_RoofPlanLatLong_isDirty"] = true;}
		public function set RoofPlanZoom(val:Number):void				{_fields["_RoofPlanZoom"] = val; _fields["_RoofPlanZoom_isDirty"] = true;}
		public function set Polygon(val:String):void					{_fields["_Polygon"] = val; _fields["_Polygon_isDirty"] = true;}
		public function set DateInstalled(val:Date):void				{_fields["_DateInstalled"] = val; _fields["_DateInstalled_isDirty"] = true;}

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
		private function set IDNDesignation(val:String):void			{_fields["_Designation"] = val;}
		private function set IDNRoofName(val:String):void				{_fields["_RoofName"] = val;}
		private function set IDNRoofSystem(val:String):void				{_fields["_RoofSystem"] = val;}
		private function set IDNAge(val:String):void					{_fields["_Age"] = Number(val);}
		private function set IDNConditionIndex(val:String):void			{_fields["_ConditionIndex"] = val;}
		private function set IDNSqFt(val:String):void					{_fields["_SqFt"] = Number(val);}
		private function set IDNEstCost(val:String):void				{_fields["_EstCost"] = Number(val);}
		private function set IDNRelatedFacility(val:String):void		{_fields["_RelatedFacility"] = Number(val);}
		private function set IDNAddLayer(val:String):void				{_fields["_AddLayer"] = val;}
		private function set IDNLayers(val:String):void					{_fields["_Layers"] = val;}
		private function set IDNAddCorePhoto(val:String):void			{_fields["_AddCorePhoto"] = val;}
		private function set IDNCorePhotos(val:String):void				{_fields["_CorePhotos"] = val;}
		private function set IDNAddDetail(val:String):void				{_fields["_AddDetail"] = val;}
		private function set IDNDetails(val:String):void				{_fields["_Details"] = val;}
		private function set IDNAddDefect(val:String):void				{_fields["_AddDefect"] = val;}
		private function set IDNDefects(val:String):void				{_fields["_Defects"] = val;}
		private function set IDNAddInspection(val:String):void			{_fields["_AddInspection"] = val;}
		private function set IDNInspections(val:String):void			{_fields["_Inspections"] = val;}
		private function set IDNAddSurvey(val:String):void				{_fields["_AddSurvey"] = val;}
		private function set IDNSurveys(val:String):void				{_fields["_Surveys"] = val;}
		private function set IDNAddExpenditure(val:String):void			{_fields["_AddExpenditure"] = val;}
		private function set IDNExpenditures(val:String):void			{_fields["_Expenditures"] = val;}
		private function set IDNAddWarranty(val:String):void			{_fields["_AddWarranty"] = val;}
		private function set IDNWarranties(val:String):void				{_fields["_Warranties"] = val;}
		private function set IDNPhoto(val:String):void {
			if(_fields["_Photo"] == null) {
				_fields["_Photo"] = new QuickBaseFileDTO();
			}
			_fields["_Photo"].url = val;
		}
		private function set IDNPhotoName(val:String):void				{_fields["_PhotoName"] = val;}
		private function set IDNYearInstalled(val:String):void			{_fields["_YearInstalled"] = Number(val);}
		private function set IDNYearInstalledSource(val:String):void	{_fields["_YearInstalledSource"] = val;}
		private function set IDNEstCostPerSqFt(val:String):void			{_fields["_EstCostPerSqFt"] = Number(val);}
		private function set IDNHeight(val:String):void					{_fields["_Height"] = Number(val);}
		private function set IDNSlope(val:String):void					{_fields["_Slope"] = val;}
		private function set IDNInteriorSensitivity(val:String):void	{_fields["_InteriorSensitivity"] = val;}
		private function set IDNSensitivityDetails(val:String):void		{_fields["_SensitivityDetails"] = val;}
		private function set IDNConditionDetails(val:String):void		{_fields["_ConditionDetails"] = val;}
		private function set IDNRestorable(val:String):void				{_fields["_Restorable"] = val;}
		private function set IDNDrainage(val:String):void				{_fields["_Drainage"] = val;}
		private function set IDNCurrentlyLeaking(val:String):void		{_fields["_CurrentlyLeaking"] = val;}
		private function set IDNHistoryOfLeaking(val:String):void		{_fields["_HistoryOfLeaking"] = val;}
		private function set IDNDrainageDetails(val:String):void		{_fields["_DrainageDetails"] = val;}
		private function set IDNLeakDetails(val:String):void			{_fields["_LeakDetails"] = val;}
		private function set IDNX1(val:String):void						{_fields["_X1"] = Number(val);}
		private function set IDNY1(val:String):void						{_fields["_Y1"] = Number(val);}
		private function set IDNX2(val:String):void						{_fields["_X2"] = Number(val);}
		private function set IDNY2(val:String):void						{_fields["_Y2"] = Number(val);}
		private function set IDNOverallCoreCondition(val:String):void	{_fields["_OverallCoreCondition"] = val;}
		private function set IDNNotes(val:String):void					{_fields["_Notes"] = val;}
		private function set IDNSectionUsers(val:String):void			{_fields["_SectionUsers"] = val;}
		private function set IDNAddSectionUser(val:String):void			{_fields["_AddSectionUser"] = val;}
		private function set IDNAllowedSectionUser(val:String):void		{_fields["_AllowedSectionUser"] = Number(val);}
		private function set IDNAllowedClientUser(val:String):void		{_fields["_AllowedClientUser"] = Number(val);}
		private function set IDNAllowedFacilityUser(val:String):void	{_fields["_AllowedFacilityUser"] = Number(val);}
		private function set IDNAllowedUser(val:String):void			{_fields["_AllowedUser"] = Number(val);}
		private function set IDNOldRoofsectionid(val:String):void		{_fields["_OldRoofsectionid"] = Number(val);}
		private function set IDNSectionLat(val:String):void				{_fields["_SectionLat"] = Number(val);}
		private function set IDNSectionLong(val:String):void			{_fields["_SectionLong"] = Number(val);}
		private function set IDNPhotoThumbnail(val:String):void			{_fields["_PhotoThumbnail"] = val;}
		private function set IDNQBMap(val:String):void					{_fields["_QBMap"] = val;}
		private function set IDNQBMapsMarker(val:String):void			{_fields["_QBMapsMarker"] = val;}
		private function set IDNQBMapsUrl(val:String):void				{_fields["_QBMapsUrl"] = val;}
		private function set IDNQBAddress(val:String):void				{_fields["_QBAddress"] = val;}
		private function set IDNCreateReport(val:String):void			{_fields["_CreateReport"] = val;}
		private function set IDNReport(val:String):void {
			if(_fields["_Report"] == null) {
				_fields["_Report"] = new QuickBaseFileDTO();
			}
			_fields["_Report"].url = val;
		}
		private function set IDNRoofPlanLatLong(val:String):void		{_fields["_RoofPlanLatLong"] = val;}
		private function set IDNRoofPlanZoom(val:String):void			{_fields["_RoofPlanZoom"] = Number(val);}
		private function set IDNPolygon(val:String):void				{_fields["_Polygon"] = val;}
		private function set IDNDateInstalled(val:String):void			{_fields["_DateInstalled"] = new Date(Number(val));}
		private function set IDNTestDoc(val:String):void {
			if(_fields["_TestDoc"] == null) {
				_fields["_TestDoc"] = new QuickBaseFileDTO();
			}
			_fields["_TestDoc"].url = val;
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
		public function getInfoObj():IKingussieInfo						{return Sections_Info.getInstance();}
		public function getFieldMapObj():IQuickBaseFieldMap				{return new Sections_FieldMap();}
		public function getFieldMapClass():Class						{return Sections_FieldMap;}
		public function get dtoClass():Class							{return Sections_DTO;}

		// MetaData Information Objects getters
		public function get Client_Info():TextField						{return Sections_Info.getInstance().Client_Info;}
		public function get Facility_Info():TextField					{return Sections_Info.getInstance().Facility_Info;}
		public function get Designation_Info():TextField				{return Sections_Info.getInstance().Designation_Info;}
		public function get RoofName_Info():ChoiceField					{return Sections_Info.getInstance().RoofName_Info;}
		public function get RoofSystem_Info():ChoiceField				{return Sections_Info.getInstance().RoofSystem_Info;}
		public function get Age_Info():NumberField						{return Sections_Info.getInstance().Age_Info;}
		public function get ConditionIndex_Info():ChoiceField			{return Sections_Info.getInstance().ConditionIndex_Info;}
		public function get SqFt_Info():NumberField						{return Sections_Info.getInstance().SqFt_Info;}
		public function get EstCost_Info():CurrencyField				{return Sections_Info.getInstance().EstCost_Info;}
		public function get RelatedFacility_Info():NumberField			{return Sections_Info.getInstance().RelatedFacility_Info;}
		public function get AddLayer_Info():URLField					{return Sections_Info.getInstance().AddLayer_Info;}
		public function get Layers_Info():DbLinkField					{return Sections_Info.getInstance().Layers_Info;}
		public function get AddCorePhoto_Info():URLField				{return Sections_Info.getInstance().AddCorePhoto_Info;}
		public function get CorePhotos_Info():DbLinkField				{return Sections_Info.getInstance().CorePhotos_Info;}
		public function get AddDetail_Info():URLField					{return Sections_Info.getInstance().AddDetail_Info;}
		public function get Details_Info():DbLinkField					{return Sections_Info.getInstance().Details_Info;}
		public function get AddDefect_Info():URLField					{return Sections_Info.getInstance().AddDefect_Info;}
		public function get Defects_Info():DbLinkField					{return Sections_Info.getInstance().Defects_Info;}
		public function get AddInspection_Info():URLField				{return Sections_Info.getInstance().AddInspection_Info;}
		public function get Inspections_Info():DbLinkField				{return Sections_Info.getInstance().Inspections_Info;}
		public function get AddSurvey_Info():URLField					{return Sections_Info.getInstance().AddSurvey_Info;}
		public function get Surveys_Info():DbLinkField					{return Sections_Info.getInstance().Surveys_Info;}
		public function get AddExpenditure_Info():URLField				{return Sections_Info.getInstance().AddExpenditure_Info;}
		public function get Expenditures_Info():DbLinkField				{return Sections_Info.getInstance().Expenditures_Info;}
		public function get AddWarranty_Info():URLField					{return Sections_Info.getInstance().AddWarranty_Info;}
		public function get Warranties_Info():DbLinkField				{return Sections_Info.getInstance().Warranties_Info;}
		public function get Photo_Info():FileField						{return Sections_Info.getInstance().Photo_Info;}
		public function get PhotoName_Info():TextField					{return Sections_Info.getInstance().PhotoName_Info;}
		public function get YearInstalled_Info():NumberField			{return Sections_Info.getInstance().YearInstalled_Info;}
		public function get YearInstalledSource_Info():ChoiceField		{return Sections_Info.getInstance().YearInstalledSource_Info;}
		public function get EstCostPerSqFt_Info():CurrencyField			{return Sections_Info.getInstance().EstCostPerSqFt_Info;}
		public function get Height_Info():NumberField					{return Sections_Info.getInstance().Height_Info;}
		public function get Slope_Info():ChoiceField					{return Sections_Info.getInstance().Slope_Info;}
		public function get InteriorSensitivity_Info():ChoiceField		{return Sections_Info.getInstance().InteriorSensitivity_Info;}
		public function get SensitivityDetails_Info():TextField			{return Sections_Info.getInstance().SensitivityDetails_Info;}
		public function get ConditionDetails_Info():TextField			{return Sections_Info.getInstance().ConditionDetails_Info;}
		public function get Restorable_Info():ChoiceField				{return Sections_Info.getInstance().Restorable_Info;}
		public function get Drainage_Info():ChoiceField					{return Sections_Info.getInstance().Drainage_Info;}
		public function get CurrentlyLeaking_Info():ChoiceField			{return Sections_Info.getInstance().CurrentlyLeaking_Info;}
		public function get HistoryOfLeaking_Info():ChoiceField			{return Sections_Info.getInstance().HistoryOfLeaking_Info;}
		public function get DrainageDetails_Info():ChoiceField			{return Sections_Info.getInstance().DrainageDetails_Info;}
		public function get LeakDetails_Info():TextField				{return Sections_Info.getInstance().LeakDetails_Info;}
		public function get X1_Info():NumberField						{return Sections_Info.getInstance().X1_Info;}
		public function get Y1_Info():NumberField						{return Sections_Info.getInstance().Y1_Info;}
		public function get X2_Info():NumberField						{return Sections_Info.getInstance().X2_Info;}
		public function get Y2_Info():NumberField						{return Sections_Info.getInstance().Y2_Info;}
		public function get OverallCoreCondition_Info():TextField		{return Sections_Info.getInstance().OverallCoreCondition_Info;}
		public function get Notes_Info():ChoiceField					{return Sections_Info.getInstance().Notes_Info;}
		public function get SectionUsers_Info():DbLinkField				{return Sections_Info.getInstance().SectionUsers_Info;}
		public function get AddSectionUser_Info():URLField				{return Sections_Info.getInstance().AddSectionUser_Info;}
		public function get AllowedSectionUser_Info():NumberField		{return Sections_Info.getInstance().AllowedSectionUser_Info;}
		public function get AllowedClientUser_Info():NumberField		{return Sections_Info.getInstance().AllowedClientUser_Info;}
		public function get AllowedFacilityUser_Info():NumberField		{return Sections_Info.getInstance().AllowedFacilityUser_Info;}
		public function get AllowedUser_Info():NumberField				{return Sections_Info.getInstance().AllowedUser_Info;}
		public function get OldRoofsectionid_Info():NumberField			{return Sections_Info.getInstance().OldRoofsectionid_Info;}
		public function get SectionLat_Info():NumberField				{return Sections_Info.getInstance().SectionLat_Info;}
		public function get SectionLong_Info():NumberField				{return Sections_Info.getInstance().SectionLong_Info;}
		public function get PhotoThumbnail_Info():TextField				{return Sections_Info.getInstance().PhotoThumbnail_Info;}
		public function get QBMap_Info():URLField						{return Sections_Info.getInstance().QBMap_Info;}
		public function get QBMapsMarker_Info():TextField				{return Sections_Info.getInstance().QBMapsMarker_Info;}
		public function get QBMapsUrl_Info():TextField					{return Sections_Info.getInstance().QBMapsUrl_Info;}
		public function get QBAddress_Info():TextField					{return Sections_Info.getInstance().QBAddress_Info;}
		public function get CreateReport_Info():URLField				{return Sections_Info.getInstance().CreateReport_Info;}
		public function get Report_Info():FileField						{return Sections_Info.getInstance().Report_Info;}
		public function get RoofPlanLatLong_Info():TextField			{return Sections_Info.getInstance().RoofPlanLatLong_Info;}
		public function get RoofPlanZoom_Info():NumberField				{return Sections_Info.getInstance().RoofPlanZoom_Info;}
		public function get Polygon_Info():TextField					{return Sections_Info.getInstance().Polygon_Info;}
		public function get DateInstalled_Info():DateField				{return Sections_Info.getInstance().DateInstalled_Info;}
		public function get TestDoc_Info():FileField					{return Sections_Info.getInstance().TestDoc_Info;}
		public function get DateCreated_Info():TimeStampField			{return Sections_Info.getInstance().DateCreated_Info;}
		public function get DateModified_Info():TimeStampField			{return Sections_Info.getInstance().DateModified_Info;}
		public function get RecordOwner_Info():UserIdField				{return Sections_Info.getInstance().RecordOwner_Info;}
		public function get LastModifiedBy_Info():UserIdField			{return Sections_Info.getInstance().LastModifiedBy_Info;}

	}
}
