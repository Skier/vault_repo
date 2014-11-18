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

	public class Details_Info implements IKingussieInfo
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		private static var _instance:Details_Info = null;

		function Details_Info(forcePrivateClass:Private)
		{
			// MetaData Initializers
			var field:FieldDescriptor;

			field = new FieldDescriptor();
			field.lusFid = 12;
			field.unique = false;
			field.width = 40;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.required = false;
			field.doesDataCopy = false;
			field.carryChoices = true;
			field.lutFid = 30;
			field.mode = ENMode.Lookup;
			field.findEnabled = true;
			field.fieldName = "Client";
			field.formula = "";
			field.tableName = "Details";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Client";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 13;
			field.numLines = 0;
			_ClientInfo = new TextField(field);
			_fieldInfo.addItem(_ClientInfo);

			field = new FieldDescriptor();
			field.lusFid = 12;
			field.unique = false;
			field.width = 40;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.required = false;
			field.doesDataCopy = false;
			field.carryChoices = true;
			field.lutFid = 31;
			field.mode = ENMode.Lookup;
			field.findEnabled = true;
			field.fieldName = "Facility";
			field.formula = "";
			field.tableName = "Details";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Facility";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 14;
			field.numLines = 0;
			_FacilityInfo = new TextField(field);
			_fieldInfo.addItem(_FacilityInfo);

			field = new FieldDescriptor();
			field.lusFid = 12;
			field.unique = false;
			field.width = 40;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.required = false;
			field.doesDataCopy = false;
			field.carryChoices = true;
			field.lutFid = 6;
			field.mode = ENMode.Lookup;
			field.findEnabled = true;
			field.fieldName = "Roof";
			field.formula = "";
			field.tableName = "Details";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Roof";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 15;
			field.numLines = 0;
			_RoofInfo = new TextField(field);
			_fieldInfo.addItem(_RoofInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.width = 20;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.required = true;
			field.doesDataCopy = true;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = true;
			field.fieldName = "DetailType";
			field.formula = "";
			field.tableName = "Details";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = true;
			field.label = "Detail Type";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 7;
			field.numLines = 0;
			_DetailTypeInfo = new ChoiceField(field);
			_fieldInfo.addItem(_DetailTypeInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.width = 30;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.required = false;
			field.doesDataCopy = true;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = true;
			field.fieldName = "Description";
			field.formula = "";
			field.tableName = "Details";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Description";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 8;
			field.numLines = 0;
			_DescriptionInfo = new ChoiceField(field);
			_fieldInfo.addItem(_DescriptionInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.width = 40;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.required = false;
			field.doesDataCopy = true;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = true;
			field.fieldName = "FlashingMembrane";
			field.formula = "";
			field.tableName = "Details";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = true;
			field.label = "Flashing Membrane";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 9;
			field.numLines = 0;
			_FlashingMembraneInfo = new ChoiceField(field);
			_fieldInfo.addItem(_FlashingMembraneInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.width = 40;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.required = false;
			field.doesDataCopy = true;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = true;
			field.fieldName = "FlashingMetal";
			field.formula = "";
			field.tableName = "Details";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Flashing Metal";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 10;
			field.numLines = 0;
			_FlashingMetalInfo = new ChoiceField(field);
			_fieldInfo.addItem(_FlashingMetalInfo);

			field = new FieldDescriptor();
			field.commaStart = 0;
			field.lusFid = 0;
			field.unique = false;
			field.blankIsZero = true;
			field.fieldType = ENFieldType.Float;
			field.required = true;
			field.doesDataCopy = true;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.units = "";
			field.findEnabled = true;
			field.fieldName = "RelatedSection";
			field.formula = "";
			field.tableName = "Details";
			field.baseType = ENBaseType.Float;
			field.allowNewChoices = false;
			field.decimalPlaces = 0;
			field.label = "Related Section";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 12;
			_RelatedSectionInfo = new NumberField(field);
			_fieldInfo.addItem(_RelatedSectionInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.appearsAs = "Add  Detail Photo";
			field.fieldType = ENFieldType.URL;
			field.required = false;
			field.doesDataCopy = false;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.Virtual;
			field.findEnabled = false;
			field.fieldName = "AddPhoto";
			field.formula = "URLRoot() & \"db/\" & [_DBID_DETAIL_PHOTOS] & \"?a=API_GenAddRecordForm&_fid_11=\" & URLEncode ([Record ID#])& \"&z=\" & Rurl()";
			field.tableName = "Details";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Add Photo";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 25;
			_AddPhotoInfo = new URLField(field);
			_fieldInfo.addItem(_AddPhotoInfo);

			field = new FieldDescriptor();
			field.targetFID = 11;
			field.lusFid = 0;
			field.unique = false;
			field.fieldType = ENFieldType.DbLink;
			field.required = false;
			field.doesDataCopy = false;
			field.carryChoices = true;
			field.targetDBID = "-b";
			field.lutFid = 0;
			field.mode = ENMode.Virtual;
			field.sourceFID = 3;
			field.findEnabled = true;
			field.fieldName = "Photos";
			field.tableName = "Details";
			field.baseType = ENBaseType.Text;
			field.exact = true;
			field.allowNewChoices = false;
			field.label = "Photos";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 24;
			field.coverText = "Detail Photos";
			_PhotosInfo = new DbLinkField(field);
			_fieldInfo.addItem(_PhotosInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.fieldType = ENFieldType.File;
			field.required = false;
			field.doesDataCopy = false;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = true;
			field.fieldName = "Photo";
			field.tableName = "Details";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.maxVersions = 3;
			field.label = "Photo";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 6;
			_PhotoInfo = new FileField(field);
			_fieldInfo.addItem(_PhotoInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.width = 40;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.required = false;
			field.doesDataCopy = true;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = true;
			field.fieldName = "PhotoName";
			field.formula = "";
			field.tableName = "Details";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Photo Name";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 20;
			field.numLines = 1;
			_PhotoNameInfo = new TextField(field);
			_fieldInfo.addItem(_PhotoNameInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.width = 80;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.required = false;
			field.doesDataCopy = true;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = true;
			field.fieldName = "Condition";
			field.formula = "";
			field.tableName = "Details";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Condition";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 19;
			field.numLines = 3;
			_ConditionInfo = new TextField(field);
			_fieldInfo.addItem(_ConditionInfo);

			field = new FieldDescriptor();
			field.commaStart = 4;
			field.lusFid = 12;
			field.unique = false;
			field.blankIsZero = false;
			field.fieldType = ENFieldType.Float;
			field.required = false;
			field.doesDataCopy = false;
			field.carryChoices = true;
			field.lutFid = 77;
			field.mode = ENMode.Lookup;
			field.units = "";
			field.findEnabled = true;
			field.fieldName = "AllowedSectionUser";
			field.formula = "";
			field.tableName = "Details";
			field.baseType = ENBaseType.Float;
			field.allowNewChoices = false;
			field.decimalPlaces = -1;
			field.label = "Allowed Section User";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 29;
			_AllowedSectionUserInfo = new NumberField(field);
			_fieldInfo.addItem(_AllowedSectionUserInfo);

			field = new FieldDescriptor();
			field.commaStart = 4;
			field.lusFid = 12;
			field.unique = false;
			field.blankIsZero = false;
			field.fieldType = ENFieldType.Float;
			field.required = false;
			field.doesDataCopy = false;
			field.carryChoices = true;
			field.lutFid = 79;
			field.mode = ENMode.Lookup;
			field.units = "";
			field.findEnabled = true;
			field.fieldName = "AllowedFacilityUser";
			field.formula = "";
			field.tableName = "Details";
			field.baseType = ENBaseType.Float;
			field.allowNewChoices = false;
			field.decimalPlaces = -1;
			field.label = "Allowed Facility User";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 30;
			_AllowedFacilityUserInfo = new NumberField(field);
			_fieldInfo.addItem(_AllowedFacilityUserInfo);

			field = new FieldDescriptor();
			field.commaStart = 4;
			field.lusFid = 12;
			field.unique = false;
			field.blankIsZero = false;
			field.fieldType = ENFieldType.Float;
			field.required = false;
			field.doesDataCopy = false;
			field.carryChoices = true;
			field.lutFid = 78;
			field.mode = ENMode.Lookup;
			field.units = "";
			field.findEnabled = true;
			field.fieldName = "AllowedClientUser";
			field.formula = "";
			field.tableName = "Details";
			field.baseType = ENBaseType.Float;
			field.allowNewChoices = false;
			field.decimalPlaces = -1;
			field.label = "Allowed Client User";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 31;
			_AllowedClientUserInfo = new NumberField(field);
			_fieldInfo.addItem(_AllowedClientUserInfo);

			field = new FieldDescriptor();
			field.commaStart = 4;
			field.lusFid = 0;
			field.unique = false;
			field.blankIsZero = false;
			field.fieldType = ENFieldType.Float;
			field.required = false;
			field.doesDataCopy = false;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.Virtual;
			field.units = "";
			field.findEnabled = true;
			field.fieldName = "AllowedUser";
			field.formula = "[Allowed Client User] + [Allowed Facility User] + [Allowed Section User]";
			field.tableName = "Details";
			field.baseType = ENBaseType.Float;
			field.allowNewChoices = false;
			field.decimalPlaces = -1;
			field.label = "Allowed User";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 32;
			_AllowedUserInfo = new NumberField(field);
			_fieldInfo.addItem(_AllowedUserInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.fieldType = ENFieldType.TimeStamp;
			field.required = false;
			field.doesDataCopy = false;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = false;
			field.fieldName = "DateCreated";
			field.tableName = "Details";
			field.baseType = ENBaseType.Int64;
			field.allowNewChoices = false;
			field.label = "Date Created";
			field.foreignKey = 0;
			field.role = ENRole.Created;
			field.fieldHelp = "";
			field.fid = 1;
			_DateCreatedInfo = new TimeStampField(field);
			_fieldInfo.addItem(_DateCreatedInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.fieldType = ENFieldType.TimeStamp;
			field.required = false;
			field.doesDataCopy = false;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = false;
			field.fieldName = "DateModified";
			field.tableName = "Details";
			field.baseType = ENBaseType.Int64;
			field.allowNewChoices = false;
			field.label = "Date Modified";
			field.foreignKey = 0;
			field.role = ENRole.Modifed;
			field.fieldHelp = "";
			field.fid = 2;
			_DateModifiedInfo = new TimeStampField(field);
			_fieldInfo.addItem(_DateModifiedInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.fieldType = ENFieldType.UserId;
			field.required = false;
			field.doesDataCopy = false;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = true;
			field.fieldName = "RecordOwner";
			field.tableName = "Details";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = true;
			field.label = "Record Owner";
			field.foreignKey = 0;
			field.role = ENRole.Owner;
			field.fieldHelp = "";
			field.fid = 4;
			_RecordOwnerInfo = new UserIdField(field);
			_fieldInfo.addItem(_RecordOwnerInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.fieldType = ENFieldType.UserId;
			field.required = false;
			field.doesDataCopy = false;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = true;
			field.fieldName = "LastModifiedBy";
			field.tableName = "Details";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = true;
			field.label = "Last Modified By";
			field.foreignKey = 0;
			field.role = ENRole.Modifier;
			field.fieldHelp = "";
			field.fid = 5;
			_LastModifiedByInfo = new UserIdField(field);
			_fieldInfo.addItem(_LastModifiedByInfo);

		}

		public static function getInstance():Details_Info
		{
			if(_instance == null)
				_instance = new Details_Info(new Private);
			return _instance;
		}

		public function get tableName():String
		{
			return "Details";
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
		private var _DetailTypeInfo:ChoiceField;
		private var _DescriptionInfo:ChoiceField;
		private var _FlashingMembraneInfo:ChoiceField;
		private var _FlashingMetalInfo:ChoiceField;
		private var _RelatedSectionInfo:NumberField;
		private var _AddPhotoInfo:URLField;
		private var _PhotosInfo:DbLinkField;
		private var _PhotoInfo:FileField;
		private var _PhotoNameInfo:TextField;
		private var _ConditionInfo:TextField;
		private var _AllowedSectionUserInfo:NumberField;
		private var _AllowedFacilityUserInfo:NumberField;
		private var _AllowedClientUserInfo:NumberField;
		private var _AllowedUserInfo:NumberField;
		private var _DateCreatedInfo:TimeStampField;
		private var _DateModifiedInfo:TimeStampField;
		private var _RecordOwnerInfo:UserIdField;
		private var _LastModifiedByInfo:UserIdField;

		// MetaData Information Objects getters
		public function get Client_Info():TextField						{return _ClientInfo;}
		public function get Facility_Info():TextField					{return _FacilityInfo;}
		public function get Roof_Info():TextField						{return _RoofInfo;}
		public function get DetailType_Info():ChoiceField				{return _DetailTypeInfo;}
		public function get Description_Info():ChoiceField				{return _DescriptionInfo;}
		public function get FlashingMembrane_Info():ChoiceField			{return _FlashingMembraneInfo;}
		public function get FlashingMetal_Info():ChoiceField			{return _FlashingMetalInfo;}
		public function get RelatedSection_Info():NumberField			{return _RelatedSectionInfo;}
		public function get AddPhoto_Info():URLField					{return _AddPhotoInfo;}
		public function get Photos_Info():DbLinkField					{return _PhotosInfo;}
		public function get Photo_Info():FileField						{return _PhotoInfo;}
		public function get PhotoName_Info():TextField					{return _PhotoNameInfo;}
		public function get Condition_Info():TextField					{return _ConditionInfo;}
		public function get AllowedSectionUser_Info():NumberField		{return _AllowedSectionUserInfo;}
		public function get AllowedFacilityUser_Info():NumberField		{return _AllowedFacilityUserInfo;}
		public function get AllowedClientUser_Info():NumberField		{return _AllowedClientUserInfo;}
		public function get AllowedUser_Info():NumberField				{return _AllowedUserInfo;}
		public function get DateCreated_Info():TimeStampField			{return _DateCreatedInfo;}
		public function get DateModified_Info():TimeStampField			{return _DateModifiedInfo;}
		public function get RecordOwner_Info():UserIdField				{return _RecordOwnerInfo;}
		public function get LastModifiedBy_Info():UserIdField			{return _LastModifiedByInfo;}

		// Field getter variables
		private var _fieldNames:ArrayCollection = new ArrayCollection(["Client", "Facility", "Roof", "DetailType", "Description", 
																		"FlashingMembrane", "FlashingMetal", "RelatedSection", "AddPhoto", "Photos", "Photo", "PhotoName", "Condition", 
																		"AllowedSectionUser", "AllowedFacilityUser", "AllowedClientUser", "AllowedUser", "DateCreated", "DateModified", 
																		"RecordOwner", "LastModifiedBy", ]);
		private var _fieldInfo:ArrayCollection = new ArrayCollection();

		// Field getters
		public function get FieldNames():ArrayCollection				{return _fieldNames;}
		public function get FieldsInfo():ArrayCollection				{return _fieldInfo;}
	}
}

class Private{}
