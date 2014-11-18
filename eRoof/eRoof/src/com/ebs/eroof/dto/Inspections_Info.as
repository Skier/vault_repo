package com.ebs.eroof.dto
{
	import com.adobe.cairngorm.vo.IValueObject;
	import com.quickbase.idn.dto.IKingussieInfo;
	import com.quickbase.idn.fieldtypes.*;
	import com.quickbase.idn.fieldtypes.bool.*;
	import com.quickbase.idn.fieldtypes.float.*;
	import com.quickbase.idn.fieldtypes.int32.*;
	import com.quickbase.idn.fieldtypes.int64.*;
	import com.quickbase.idn.fieldtypes.text.*;
	import mx.collections.ArrayCollection;

	public class Inspections_Info implements IKingussieInfo
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		private static var _instance:Inspections_Info = null;

		function Inspections_Info(forcePrivateClass:Private)
		{
			// MetaData Initializers
			var field:FieldDescriptor;

			field = new FieldDescriptor();
			field.fid = 12;
			field.lutFid = 30;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 11;
			field.label = "Client";
			field.allowNewChoices = false;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Inspections";
			field.role = ENRole.NotFound;
			field.fieldName = "Client";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_ClientInfo = new TextField(field);
			_fieldInfo.addItem(_ClientInfo);

			field = new FieldDescriptor();
			field.fid = 13;
			field.lutFid = 31;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 11;
			field.label = "Facility";
			field.allowNewChoices = false;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Inspections";
			field.role = ENRole.NotFound;
			field.fieldName = "Facility";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_FacilityInfo = new TextField(field);
			_fieldInfo.addItem(_FacilityInfo);

			field = new FieldDescriptor();
			field.fid = 14;
			field.lutFid = 6;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 11;
			field.label = "Roof";
			field.allowNewChoices = false;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Inspections";
			field.role = ENRole.NotFound;
			field.fieldName = "Roof";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_RoofInfo = new TextField(field);
			_fieldInfo.addItem(_RoofInfo);

			field = new FieldDescriptor();
			field.fid = 6;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.Date;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "InspectionDate";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Int64;
			field.tableName = "Inspections";
			field.role = ENRole.NotFound;
			field.fieldName = "InspectionDate";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_InspectionDateInfo = new DateField(field);
			_fieldInfo.addItem(_InspectionDateInfo);

			field = new FieldDescriptor();
			field.fid = 7;
			field.lutFid = 0;
			field.numLines = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 30;
			field.lusFid = 0;
			field.label = "InspectionType";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Inspections";
			field.role = ENRole.NotFound;
			field.fieldName = "InspectionType";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_InspectionTypeInfo = new ChoiceField(field);
			_fieldInfo.addItem(_InspectionTypeInfo);

			field = new FieldDescriptor();
			field.fid = 8;
			field.lutFid = 0;
			field.numLines = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 0;
			field.label = "InspectorCompany";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Inspections";
			field.role = ENRole.NotFound;
			field.fieldName = "InspectorCompany";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_InspectorCompanyInfo = new ChoiceField(field);
			_fieldInfo.addItem(_InspectorCompanyInfo);

			field = new FieldDescriptor();
			field.fid = 9;
			field.lutFid = 0;
			field.numLines = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 0;
			field.label = "InspectorName";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Inspections";
			field.role = ENRole.NotFound;
			field.fieldName = "InspectorName";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_InspectorNameInfo = new TextField(field);
			_fieldInfo.addItem(_InspectorNameInfo);

			field = new FieldDescriptor();
			field.fid = 11;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.commaStart = 0;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Related Section";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = 0;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Inspections";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.fieldName = "RelatedSection";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_RelatedSectionInfo = new NumberField(field);
			_fieldInfo.addItem(_RelatedSectionInfo);

			field = new FieldDescriptor();
			field.fid = 27;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.URL;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Add Photo";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Inspections";
			field.role = ENRole.NotFound;
			field.appearsAs = "Add  Inspection Photo";
			field.fieldName = "AddPhoto";
			field.required = false;
			field.formula = "URLRoot() & \"db/\" & [_DBID_INSPECTION_PHOTOS] & \"?a=API_GenAddRecordForm&_fid_10=\" & URLEncode ([Record ID#])& \"&z=\" & Rurl()";
			field.foreignKey = 0;
			field.findEnabled = false;
			_AddPhotoInfo = new URLField(field);
			_fieldInfo.addItem(_AddPhotoInfo);

			field = new FieldDescriptor();
			field.fid = 26;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.sourceFID = 3;
			field.unique = false;
			field.fieldType = ENFieldType.DbLink;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.targetFID = 10;
			field.targetDBID = "be9nwdi2f";
			field.label = "Photos";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.coverText = "Inspection Photos";
			field.baseType = ENBaseType.Text;
			field.tableName = "Inspections";
			field.exact = true;
			field.role = ENRole.NotFound;
			field.fieldName = "Photos";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_PhotosInfo = new DbLinkField(field);
			_fieldInfo.addItem(_PhotosInfo);

			field = new FieldDescriptor();
			field.fid = 18;
			field.lutFid = 0;
			field.numLines = 1;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 0;
			field.label = "Date Sort";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Inspections";
			field.role = ENRole.NotFound;
			field.fieldName = "DateSort";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_DateSortInfo = new TextField(field);
			_fieldInfo.addItem(_DateSortInfo);

			field = new FieldDescriptor();
			field.fid = 19;
			field.lutFid = 0;
			field.numLines = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 0;
			field.label = "Weather";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Inspections";
			field.role = ENRole.NotFound;
			field.fieldName = "Weather";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_WeatherInfo = new TextField(field);
			_fieldInfo.addItem(_WeatherInfo);

			field = new FieldDescriptor();
			field.fid = 20;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Core Taken";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Inspections";
			field.role = ENRole.NotFound;
			field.fieldName = "CoreTaken";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_CoreTakenInfo = new BooleanField(field);
			_fieldInfo.addItem(_CoreTakenInfo);

			field = new FieldDescriptor();
			field.fid = 21;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Further Investigation";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Inspections";
			field.role = ENRole.NotFound;
			field.fieldName = "FurtherInvestigation";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_FurtherInvestigationInfo = new BooleanField(field);
			_fieldInfo.addItem(_FurtherInvestigationInfo);

			field = new FieldDescriptor();
			field.fid = 22;
			field.lutFid = 0;
			field.numLines = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 0;
			field.label = "Further Investigation Notes";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Inspections";
			field.role = ENRole.NotFound;
			field.fieldName = "FurtherInvestigationNotes";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_FurtherInvestigationNotesInfo = new TextField(field);
			_fieldInfo.addItem(_FurtherInvestigationNotesInfo);

			field = new FieldDescriptor();
			field.fid = 23;
			field.lutFid = 0;
			field.numLines = 1;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 0;
			field.label = "Overall Rating";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Inspections";
			field.role = ENRole.NotFound;
			field.fieldName = "OverallRating";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_OverallRatingInfo = new TextField(field);
			_fieldInfo.addItem(_OverallRatingInfo);

			field = new FieldDescriptor();
			field.fid = 10;
			field.lutFid = 0;
			field.numLines = 6;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 80;
			field.lusFid = 0;
			field.label = "Assessment";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Inspections";
			field.role = ENRole.NotFound;
			field.fieldName = "Assessment";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_AssessmentInfo = new TextField(field);
			_fieldInfo.addItem(_AssessmentInfo);

			field = new FieldDescriptor();
			field.fid = 24;
			field.lutFid = 0;
			field.numLines = 1;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 0;
			field.label = "PhotoSetID";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Inspections";
			field.role = ENRole.NotFound;
			field.fieldName = "PhotoSetID";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_PhotoSetIDInfo = new TextField(field);
			_fieldInfo.addItem(_PhotoSetIDInfo);

			field = new FieldDescriptor();
			field.fid = 29;
			field.lutFid = 78;
			field.doesDataCopy = false;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 11;
			field.label = "Allowed Client User";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = -1;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Inspections";
			field.blankIsZero = false;
			field.role = ENRole.NotFound;
			field.fieldName = "AllowedClientUser";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_AllowedClientUserInfo = new NumberField(field);
			_fieldInfo.addItem(_AllowedClientUserInfo);

			field = new FieldDescriptor();
			field.fid = 30;
			field.lutFid = 79;
			field.doesDataCopy = false;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 11;
			field.label = "Allowed Facility User";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = -1;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Inspections";
			field.blankIsZero = false;
			field.role = ENRole.NotFound;
			field.fieldName = "AllowedFacilityUser";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_AllowedFacilityUserInfo = new NumberField(field);
			_fieldInfo.addItem(_AllowedFacilityUserInfo);

			field = new FieldDescriptor();
			field.fid = 31;
			field.lutFid = 77;
			field.doesDataCopy = false;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 11;
			field.label = "Allowed Section User";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = -1;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Inspections";
			field.blankIsZero = false;
			field.role = ENRole.NotFound;
			field.fieldName = "AllowedSectionUser";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_AllowedSectionUserInfo = new NumberField(field);
			_fieldInfo.addItem(_AllowedSectionUserInfo);

			field = new FieldDescriptor();
			field.fid = 32;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Allowed User";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = -1;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Inspections";
			field.blankIsZero = false;
			field.role = ENRole.NotFound;
			field.fieldName = "AllowedUser";
			field.required = false;
			field.formula = "[Allowed Client User] + [Allowed Facility User] + [Allowed Section User]";
			field.foreignKey = 0;
			field.findEnabled = true;
			_AllowedUserInfo = new NumberField(field);
			_fieldInfo.addItem(_AllowedUserInfo);

			field = new FieldDescriptor();
			field.fid = 1;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.TimeStamp;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Date Created";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Int64;
			field.tableName = "Inspections";
			field.role = ENRole.Created;
			field.fieldName = "DateCreated";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = false;
			_DateCreatedInfo = new TimeStampField(field);
			_fieldInfo.addItem(_DateCreatedInfo);

			field = new FieldDescriptor();
			field.fid = 2;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.TimeStamp;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Date Modified";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Int64;
			field.tableName = "Inspections";
			field.role = ENRole.Modifed;
			field.fieldName = "DateModified";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = false;
			_DateModifiedInfo = new TimeStampField(field);
			_fieldInfo.addItem(_DateModifiedInfo);

			field = new FieldDescriptor();
			field.fid = 4;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.UserId;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Record Owner";
			field.allowNewChoices = true;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Inspections";
			field.role = ENRole.Owner;
			field.fieldName = "RecordOwner";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_RecordOwnerInfo = new UserIdField(field);
			_fieldInfo.addItem(_RecordOwnerInfo);

			field = new FieldDescriptor();
			field.fid = 5;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.UserId;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Last Modified By";
			field.allowNewChoices = true;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Inspections";
			field.role = ENRole.Modifier;
			field.fieldName = "LastModifiedBy";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_LastModifiedByInfo = new UserIdField(field);
			_fieldInfo.addItem(_LastModifiedByInfo);

		}

		public static function getInstance():Inspections_Info
		{
			if(_instance == null)
				_instance = new Inspections_Info(new Private);
			return _instance;
		}

		public function get tableName():String
		{
			return "Inspections";
		}

		public function get isMSATable():Boolean
		{
			return false;
		}

		public function getFieldInfo(name:String):AbstractField
		{
			return this[name + "_Info"];
		}

		// MetaData Information Objects
		private var _ClientInfo:TextField;
		private var _FacilityInfo:TextField;
		private var _RoofInfo:TextField;
		private var _InspectionDateInfo:DateField;
		private var _InspectionTypeInfo:ChoiceField;
		private var _InspectorCompanyInfo:ChoiceField;
		private var _InspectorNameInfo:TextField;
		private var _RelatedSectionInfo:NumberField;
		private var _AddPhotoInfo:URLField;
		private var _PhotosInfo:DbLinkField;
		private var _DateSortInfo:TextField;
		private var _WeatherInfo:TextField;
		private var _CoreTakenInfo:BooleanField;
		private var _FurtherInvestigationInfo:BooleanField;
		private var _FurtherInvestigationNotesInfo:TextField;
		private var _OverallRatingInfo:TextField;
		private var _AssessmentInfo:TextField;
		private var _PhotoSetIDInfo:TextField;
		private var _AllowedClientUserInfo:NumberField;
		private var _AllowedFacilityUserInfo:NumberField;
		private var _AllowedSectionUserInfo:NumberField;
		private var _AllowedUserInfo:NumberField;
		private var _DateCreatedInfo:TimeStampField;
		private var _DateModifiedInfo:TimeStampField;
		private var _RecordOwnerInfo:UserIdField;
		private var _LastModifiedByInfo:UserIdField;

		// MetaData Information Objects getters
		public function get Client_Info():TextField						{return _ClientInfo;}
		public function get Facility_Info():TextField					{return _FacilityInfo;}
		public function get Roof_Info():TextField						{return _RoofInfo;}
		public function get InspectionDate_Info():DateField				{return _InspectionDateInfo;}
		public function get InspectionType_Info():ChoiceField			{return _InspectionTypeInfo;}
		public function get InspectorCompany_Info():ChoiceField			{return _InspectorCompanyInfo;}
		public function get InspectorName_Info():TextField				{return _InspectorNameInfo;}
		public function get RelatedSection_Info():NumberField			{return _RelatedSectionInfo;}
		public function get AddPhoto_Info():URLField					{return _AddPhotoInfo;}
		public function get Photos_Info():DbLinkField					{return _PhotosInfo;}
		public function get DateSort_Info():TextField					{return _DateSortInfo;}
		public function get Weather_Info():TextField					{return _WeatherInfo;}
		public function get CoreTaken_Info():BooleanField				{return _CoreTakenInfo;}
		public function get FurtherInvestigation_Info():BooleanField	{return _FurtherInvestigationInfo;}
		public function get FurtherInvestigationNotes_Info():TextField	{return _FurtherInvestigationNotesInfo;}
		public function get OverallRating_Info():TextField				{return _OverallRatingInfo;}
		public function get Assessment_Info():TextField					{return _AssessmentInfo;}
		public function get PhotoSetID_Info():TextField					{return _PhotoSetIDInfo;}
		public function get AllowedClientUser_Info():NumberField		{return _AllowedClientUserInfo;}
		public function get AllowedFacilityUser_Info():NumberField		{return _AllowedFacilityUserInfo;}
		public function get AllowedSectionUser_Info():NumberField		{return _AllowedSectionUserInfo;}
		public function get AllowedUser_Info():NumberField				{return _AllowedUserInfo;}
		public function get DateCreated_Info():TimeStampField			{return _DateCreatedInfo;}
		public function get DateModified_Info():TimeStampField			{return _DateModifiedInfo;}
		public function get RecordOwner_Info():UserIdField				{return _RecordOwnerInfo;}
		public function get LastModifiedBy_Info():UserIdField			{return _LastModifiedByInfo;}

		// Field getter variables
		private var _fieldNames:ArrayCollection = new ArrayCollection(["Client", "Facility", "Roof", "InspectionDate", 
																		"InspectionType", "InspectorCompany", "InspectorName", "RelatedSection", "AddPhoto", "Photos", "DateSort", "Weather", 
																		"CoreTaken", "FurtherInvestigation", "FurtherInvestigationNotes", "OverallRating", "Assessment", "PhotoSetID", 
																		"AllowedClientUser", "AllowedFacilityUser", "AllowedSectionUser", "AllowedUser", "DateCreated", "DateModified", 
																		"RecordOwner", "LastModifiedBy", ]);
		private var _fieldInfo:ArrayCollection = new ArrayCollection();

		// Field getters
		public function get FieldNames():ArrayCollection				{return _fieldNames;}
		public function get FieldsInfo():ArrayCollection				{return _fieldInfo;}
	}
}

class Private{}
