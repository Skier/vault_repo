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

	public class FacilityDocuments_Info implements IKingussieInfo
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		private static var _instance:FacilityDocuments_Info = null;

		function FacilityDocuments_Info(forcePrivateClass:Private)
		{
			// MetaData Initializers
			var field:FieldDescriptor;

			field = new FieldDescriptor();
			field.fid = 12;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.commaStart = 0;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Related Facility";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = 0;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "FacilityDocuments";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.fieldName = "RelatedFacility";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_RelatedFacilityInfo = new NumberField(field);
			_fieldInfo.addItem(_RelatedFacilityInfo);

			field = new FieldDescriptor();
			field.fid = 14;
			field.lutFid = 30;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 12;
			field.label = "Client";
			field.allowNewChoices = false;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "FacilityDocuments";
			field.role = ENRole.NotFound;
			field.fieldName = "Client";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_ClientInfo = new TextField(field);
			_fieldInfo.addItem(_ClientInfo);

			field = new FieldDescriptor();
			field.fid = 15;
			field.lutFid = 6;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 12;
			field.label = "Facility";
			field.allowNewChoices = false;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "FacilityDocuments";
			field.role = ENRole.NotFound;
			field.fieldName = "Facility";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_FacilityInfo = new TextField(field);
			_fieldInfo.addItem(_FacilityInfo);

			field = new FieldDescriptor();
			field.fid = 18;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.File;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Document";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "FacilityDocuments";
			field.role = ENRole.NotFound;
			field.fieldName = "Document";
			field.required = false;
			field.maxVersions = 3;
			field.foreignKey = 0;
			field.findEnabled = true;
			_DocumentInfo = new FileField(field);
			_fieldInfo.addItem(_DocumentInfo);

			field = new FieldDescriptor();
			field.fid = 6;
			field.lutFid = 0;
			field.numLines = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 60;
			field.lusFid = 0;
			field.label = "Title";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "FacilityDocuments";
			field.role = ENRole.NotFound;
			field.fieldName = "Title";
			field.required = true;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_TitleInfo = new TextField(field);
			_fieldInfo.addItem(_TitleInfo);

			field = new FieldDescriptor();
			field.fid = 7;
			field.lutFid = 0;
			field.numLines = 4;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 0;
			field.label = "Description";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "FacilityDocuments";
			field.role = ENRole.NotFound;
			field.fieldName = "Description";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_DescriptionInfo = new TextField(field);
			_fieldInfo.addItem(_DescriptionInfo);

			field = new FieldDescriptor();
			field.fid = 8;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.Date;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Date";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Int64;
			field.tableName = "FacilityDocuments";
			field.role = ENRole.NotFound;
			field.fieldName = "Date";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_DateInfo = new DateField(field);
			_fieldInfo.addItem(_DateInfo);

			field = new FieldDescriptor();
			field.fid = 19;
			field.lutFid = 69;
			field.doesDataCopy = false;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 12;
			field.label = "Allowed Client User";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = -1;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "FacilityDocuments";
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
			field.fid = 20;
			field.lutFid = 68;
			field.doesDataCopy = false;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 12;
			field.label = "Allowed Facility User";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = -1;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "FacilityDocuments";
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
			field.fid = 21;
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
			field.tableName = "FacilityDocuments";
			field.blankIsZero = false;
			field.role = ENRole.NotFound;
			field.fieldName = "AllowedUser";
			field.required = false;
			field.formula = "[Allowed Client User] + [Allowed Facility User]";
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
			field.tableName = "FacilityDocuments";
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
			field.tableName = "FacilityDocuments";
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
			field.tableName = "FacilityDocuments";
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
			field.tableName = "FacilityDocuments";
			field.role = ENRole.Modifier;
			field.fieldName = "LastModifiedBy";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_LastModifiedByInfo = new UserIdField(field);
			_fieldInfo.addItem(_LastModifiedByInfo);

		}

		public static function getInstance():FacilityDocuments_Info
		{
			if(_instance == null)
				_instance = new FacilityDocuments_Info(new Private);
			return _instance;
		}

		public function get tableName():String
		{
			return "FacilityDocuments";
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
		private var _RelatedFacilityInfo:NumberField;
		private var _ClientInfo:TextField;
		private var _FacilityInfo:TextField;
		private var _DocumentInfo:FileField;
		private var _TitleInfo:TextField;
		private var _DescriptionInfo:TextField;
		private var _DateInfo:DateField;
		private var _AllowedClientUserInfo:NumberField;
		private var _AllowedFacilityUserInfo:NumberField;
		private var _AllowedUserInfo:NumberField;
		private var _DateCreatedInfo:TimeStampField;
		private var _DateModifiedInfo:TimeStampField;
		private var _RecordOwnerInfo:UserIdField;
		private var _LastModifiedByInfo:UserIdField;

		// MetaData Information Objects getters
		public function get RelatedFacility_Info():NumberField			{return _RelatedFacilityInfo;}
		public function get Client_Info():TextField						{return _ClientInfo;}
		public function get Facility_Info():TextField					{return _FacilityInfo;}
		public function get Document_Info():FileField					{return _DocumentInfo;}
		public function get Title_Info():TextField						{return _TitleInfo;}
		public function get Description_Info():TextField				{return _DescriptionInfo;}
		public function get Date_Info():DateField						{return _DateInfo;}
		public function get AllowedClientUser_Info():NumberField		{return _AllowedClientUserInfo;}
		public function get AllowedFacilityUser_Info():NumberField		{return _AllowedFacilityUserInfo;}
		public function get AllowedUser_Info():NumberField				{return _AllowedUserInfo;}
		public function get DateCreated_Info():TimeStampField			{return _DateCreatedInfo;}
		public function get DateModified_Info():TimeStampField			{return _DateModifiedInfo;}
		public function get RecordOwner_Info():UserIdField				{return _RecordOwnerInfo;}
		public function get LastModifiedBy_Info():UserIdField			{return _LastModifiedByInfo;}

		// Field getter variables
		private var _fieldNames:ArrayCollection = new ArrayCollection(["RelatedFacility", "Client", "Facility", "Document", "Title", 
																		"Description", "Date", "AllowedClientUser", "AllowedFacilityUser", "AllowedUser", "DateCreated", "DateModified", 
																		"RecordOwner", "LastModifiedBy", ]);
		private var _fieldInfo:ArrayCollection = new ArrayCollection();

		// Field getters
		public function get FieldNames():ArrayCollection				{return _fieldNames;}
		public function get FieldsInfo():ArrayCollection				{return _fieldInfo;}
	}
}

class Private{}
