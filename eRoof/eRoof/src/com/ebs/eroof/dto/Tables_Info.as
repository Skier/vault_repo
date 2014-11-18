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

	public class Tables_Info implements IKingussieInfo
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		private static var _instance:Tables_Info = null;

		function Tables_Info(forcePrivateClass:Private)
		{
			// MetaData Initializers
			var field:FieldDescriptor;

			field = new FieldDescriptor();
			field.fid = 6;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Seq";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = -1;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Tables";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.fieldName = "Seq";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_SeqInfo = new NumberField(field);
			_fieldInfo.addItem(_SeqInfo);

			field = new FieldDescriptor();
			field.fid = 7;
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
			field.label = "Name";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Tables";
			field.role = ENRole.NotFound;
			field.fieldName = "Name";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_NameInfo = new TextField(field);
			_fieldInfo.addItem(_NameInfo);

			field = new FieldDescriptor();
			field.fid = 8;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.sourceFID = 3;
			field.unique = false;
			field.fieldType = ENFieldType.DbLink;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.targetFID = 6;
			field.targetDBID = "be9nwdi29";
			field.label = "Fields";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.coverText = "Fields";
			field.baseType = ENBaseType.Text;
			field.tableName = "Tables";
			field.exact = true;
			field.role = ENRole.NotFound;
			field.fieldName = "Fields";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_FieldsInfo = new DbLinkField(field);
			_fieldInfo.addItem(_FieldsInfo);

			field = new FieldDescriptor();
			field.fid = 9;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.URL;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Add Field";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Tables";
			field.role = ENRole.NotFound;
			field.appearsAs = "Add  Field";
			field.fieldName = "AddField";
			field.required = false;
			field.formula = "URLRoot() & \"db/\" & [_DBID_FIELDS] & \"?a=API_GenAddRecordForm&_fid_6=\" & URLEncode ([Record ID#])& \"&z=\" & Rurl()";
			field.foreignKey = 0;
			field.findEnabled = false;
			_AddFieldInfo = new URLField(field);
			_fieldInfo.addItem(_AddFieldInfo);

			field = new FieldDescriptor();
			field.fid = 10;
			field.lutFid = 0;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 0;
			field.label = "Seq and Name";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Tables";
			field.role = ENRole.NotFound;
			field.fieldName = "SeqAndName";
			field.required = false;
			field.formula = "If([Seq]<10,\" \",\"\") & [Seq] & \" \" & [Name]";
			field.foreignKey = 0;
			field.findEnabled = true;
			_SeqAndNameInfo = new TextField(field);
			_fieldInfo.addItem(_SeqAndNameInfo);

			field = new FieldDescriptor();
			field.fid = 13;
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
			field.label = "Record Picker";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Tables";
			field.role = ENRole.NotFound;
			field.fieldName = "RecordPicker";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_RecordPickerInfo = new TextField(field);
			_fieldInfo.addItem(_RecordPickerInfo);

			field = new FieldDescriptor();
			field.fid = 14;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsHide";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Tables";
			field.role = ENRole.NotFound;
			field.fieldName = "IsHide";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsHideInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsHideInfo);

			field = new FieldDescriptor();
			field.fid = 15;
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
			field.label = "DBID Name";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Tables";
			field.role = ENRole.NotFound;
			field.fieldName = "DBIDName";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_DBIDNameInfo = new TextField(field);
			_fieldInfo.addItem(_DBIDNameInfo);

			field = new FieldDescriptor();
			field.fid = 11;
			field.lutFid = 0;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 0;
			field.label = "DBID";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Tables";
			field.role = ENRole.NotFound;
			field.fieldName = "DBID";
			field.required = false;
			field.formula = "Case([Name],";
			field.foreignKey = 0;
			field.findEnabled = true;
			_DBIDInfo = new TextField(field);
			_fieldInfo.addItem(_DBIDInfo);

			field = new FieldDescriptor();
			field.fid = 16;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.sourceFID = 3;
			field.unique = false;
			field.fieldType = ENFieldType.DbLink;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.targetFID = 6;
			field.targetDBID = "be9nwdi3b";
			field.label = "Reports";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.coverText = "Reports";
			field.baseType = ENBaseType.Text;
			field.tableName = "Tables";
			field.exact = true;
			field.role = ENRole.NotFound;
			field.fieldName = "Reports";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_ReportsInfo = new DbLinkField(field);
			_fieldInfo.addItem(_ReportsInfo);

			field = new FieldDescriptor();
			field.fid = 17;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.URL;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Add Report";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Tables";
			field.role = ENRole.NotFound;
			field.appearsAs = "Add  Report";
			field.fieldName = "AddReport";
			field.required = false;
			field.formula = "URLRoot() & \"db/\" & [_DBID_REPORTS] & \"?a=API_GenAddRecordForm&_fid_6=\" & URLEncode ([Record ID#])& \"&z=\" & Rurl()";
			field.foreignKey = 0;
			field.findEnabled = false;
			_AddReportInfo = new URLField(field);
			_fieldInfo.addItem(_AddReportInfo);

			field = new FieldDescriptor();
			field.fid = 18;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsConsultant";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Tables";
			field.role = ENRole.NotFound;
			field.fieldName = "IsConsultant";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsConsultantInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsConsultantInfo);

			field = new FieldDescriptor();
			field.fid = 19;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsClient";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Tables";
			field.role = ENRole.NotFound;
			field.fieldName = "IsClient";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsClientInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsClientInfo);

			field = new FieldDescriptor();
			field.fid = 20;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsContractor";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Tables";
			field.role = ENRole.NotFound;
			field.fieldName = "IsContractor";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsContractorInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsContractorInfo);

			field = new FieldDescriptor();
			field.fid = 21;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsInspector";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Tables";
			field.role = ENRole.NotFound;
			field.fieldName = "IsInspector";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsInspectorInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsInspectorInfo);

			field = new FieldDescriptor();
			field.fid = 22;
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
			field.label = "IRPSource";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Tables";
			field.role = ENRole.NotFound;
			field.fieldName = "IRPSource";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_IRPSourceInfo = new TextField(field);
			_fieldInfo.addItem(_IRPSourceInfo);

			field = new FieldDescriptor();
			field.fid = 23;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsParent";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Tables";
			field.role = ENRole.NotFound;
			field.fieldName = "IsParent";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsParentInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsParentInfo);

			field = new FieldDescriptor();
			field.fid = 25;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IRPConvert";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Tables";
			field.role = ENRole.NotFound;
			field.fieldName = "IRPConvert";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IRPConvertInfo = new BooleanField(field);
			_fieldInfo.addItem(_IRPConvertInfo);

			field = new FieldDescriptor();
			field.fid = 26;
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
			field.label = "KeyField";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Tables";
			field.role = ENRole.NotFound;
			field.fieldName = "KeyField";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_KeyFieldInfo = new TextField(field);
			_fieldInfo.addItem(_KeyFieldInfo);

			field = new FieldDescriptor();
			field.fid = 27;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsChild";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Tables";
			field.role = ENRole.NotFound;
			field.fieldName = "IsChild";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsChildInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsChildInfo);

			field = new FieldDescriptor();
			field.fid = 28;
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
			field.label = "ParentTableName";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Tables";
			field.role = ENRole.NotFound;
			field.fieldName = "ParentTableName";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_ParentTableNameInfo = new TextField(field);
			_fieldInfo.addItem(_ParentTableNameInfo);

			field = new FieldDescriptor();
			field.fid = 29;
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
			field.label = "ParentKeyField";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Tables";
			field.role = ENRole.NotFound;
			field.fieldName = "ParentKeyField";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_ParentKeyFieldInfo = new TextField(field);
			_fieldInfo.addItem(_ParentKeyFieldInfo);

			field = new FieldDescriptor();
			field.fid = 30;
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
			field.label = "OldParentKeyField";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Tables";
			field.role = ENRole.NotFound;
			field.fieldName = "OldParentKeyField";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_OldParentKeyFieldInfo = new TextField(field);
			_fieldInfo.addItem(_OldParentKeyFieldInfo);

			field = new FieldDescriptor();
			field.fid = 31;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IRPConvert2";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Tables";
			field.role = ENRole.NotFound;
			field.fieldName = "IRPConvert2";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IRPConvert2Info = new BooleanField(field);
			_fieldInfo.addItem(_IRPConvert2Info);

			field = new FieldDescriptor();
			field.fid = 32;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.URL;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Client List";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Tables";
			field.role = ENRole.NotFound;
			field.appearsAs = "";
			field.fieldName = "ClientList";
			field.required = false;
			field.formula = "\"?a=q&qt=tab&dvqid=1&clist=7.11.12.30.31.32.33.25&slist=7&opts=so-A.gb-X.&gemi=s\"";
			field.foreignKey = 0;
			field.findEnabled = true;
			_ClientListInfo = new URLField(field);
			_fieldInfo.addItem(_ClientListInfo);

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
			field.tableName = "Tables";
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
			field.tableName = "Tables";
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
			field.tableName = "Tables";
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
			field.tableName = "Tables";
			field.role = ENRole.Modifier;
			field.fieldName = "LastModifiedBy";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_LastModifiedByInfo = new UserIdField(field);
			_fieldInfo.addItem(_LastModifiedByInfo);

		}

		public static function getInstance():Tables_Info
		{
			if(_instance == null)
				_instance = new Tables_Info(new Private);
			return _instance;
		}

		public function get tableName():String
		{
			return "Tables";
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
		private var _SeqInfo:NumberField;
		private var _NameInfo:TextField;
		private var _FieldsInfo:DbLinkField;
		private var _AddFieldInfo:URLField;
		private var _SeqAndNameInfo:TextField;
		private var _RecordPickerInfo:TextField;
		private var _IsHideInfo:BooleanField;
		private var _DBIDNameInfo:TextField;
		private var _DBIDInfo:TextField;
		private var _ReportsInfo:DbLinkField;
		private var _AddReportInfo:URLField;
		private var _IsConsultantInfo:BooleanField;
		private var _IsClientInfo:BooleanField;
		private var _IsContractorInfo:BooleanField;
		private var _IsInspectorInfo:BooleanField;
		private var _IRPSourceInfo:TextField;
		private var _IsParentInfo:BooleanField;
		private var _IRPConvertInfo:BooleanField;
		private var _KeyFieldInfo:TextField;
		private var _IsChildInfo:BooleanField;
		private var _ParentTableNameInfo:TextField;
		private var _ParentKeyFieldInfo:TextField;
		private var _OldParentKeyFieldInfo:TextField;
		private var _IRPConvert2Info:BooleanField;
		private var _ClientListInfo:URLField;
		private var _DateCreatedInfo:TimeStampField;
		private var _DateModifiedInfo:TimeStampField;
		private var _RecordOwnerInfo:UserIdField;
		private var _LastModifiedByInfo:UserIdField;

		// MetaData Information Objects getters
		public function get Seq_Info():NumberField						{return _SeqInfo;}
		public function get Name_Info():TextField						{return _NameInfo;}
		public function get Fields_Info():DbLinkField					{return _FieldsInfo;}
		public function get AddField_Info():URLField					{return _AddFieldInfo;}
		public function get SeqAndName_Info():TextField					{return _SeqAndNameInfo;}
		public function get RecordPicker_Info():TextField				{return _RecordPickerInfo;}
		public function get IsHide_Info():BooleanField					{return _IsHideInfo;}
		public function get DBIDName_Info():TextField					{return _DBIDNameInfo;}
		public function get DBID_Info():TextField						{return _DBIDInfo;}
		public function get Reports_Info():DbLinkField					{return _ReportsInfo;}
		public function get AddReport_Info():URLField					{return _AddReportInfo;}
		public function get IsConsultant_Info():BooleanField			{return _IsConsultantInfo;}
		public function get IsClient_Info():BooleanField				{return _IsClientInfo;}
		public function get IsContractor_Info():BooleanField			{return _IsContractorInfo;}
		public function get IsInspector_Info():BooleanField				{return _IsInspectorInfo;}
		public function get IRPSource_Info():TextField					{return _IRPSourceInfo;}
		public function get IsParent_Info():BooleanField				{return _IsParentInfo;}
		public function get IRPConvert_Info():BooleanField				{return _IRPConvertInfo;}
		public function get KeyField_Info():TextField					{return _KeyFieldInfo;}
		public function get IsChild_Info():BooleanField					{return _IsChildInfo;}
		public function get ParentTableName_Info():TextField			{return _ParentTableNameInfo;}
		public function get ParentKeyField_Info():TextField				{return _ParentKeyFieldInfo;}
		public function get OldParentKeyField_Info():TextField			{return _OldParentKeyFieldInfo;}
		public function get IRPConvert2_Info():BooleanField				{return _IRPConvert2Info;}
		public function get ClientList_Info():URLField					{return _ClientListInfo;}
		public function get DateCreated_Info():TimeStampField			{return _DateCreatedInfo;}
		public function get DateModified_Info():TimeStampField			{return _DateModifiedInfo;}
		public function get RecordOwner_Info():UserIdField				{return _RecordOwnerInfo;}
		public function get LastModifiedBy_Info():UserIdField			{return _LastModifiedByInfo;}

		// Field getter variables
		private var _fieldNames:ArrayCollection = new ArrayCollection(["Seq", "Name", "Fields", "AddField", "SeqAndName", 
																		"RecordPicker", "IsHide", "DBIDName", "DBID", "Reports", "AddReport", "IsConsultant", "IsClient", "IsContractor", 
																		"IsInspector", "IRPSource", "IsParent", "IRPConvert", "KeyField", "IsChild", "ParentTableName", "ParentKeyField", 
																		"OldParentKeyField", "IRPConvert2", "ClientList", "DateCreated", "DateModified", "RecordOwner", "LastModifiedBy", ]);
		private var _fieldInfo:ArrayCollection = new ArrayCollection();

		// Field getters
		public function get FieldNames():ArrayCollection				{return _fieldNames;}
		public function get FieldsInfo():ArrayCollection				{return _fieldInfo;}
	}
}

class Private{}
