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

	public class Reports_Info implements IKingussieInfo
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		private static var _instance:Reports_Info = null;

		function Reports_Info(forcePrivateClass:Private)
		{
			// MetaData Initializers
			var field:FieldDescriptor;

			field = new FieldDescriptor();
			field.fid = 8;
			field.lutFid = 10;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 6;
			field.label = "Seq and Name";
			field.allowNewChoices = false;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Reports";
			field.role = ENRole.NotFound;
			field.fieldName = "SeqAndName";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_SeqAndNameInfo = new TextField(field);
			_fieldInfo.addItem(_SeqAndNameInfo);

			field = new FieldDescriptor();
			field.fid = 6;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.commaStart = 0;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Related Table";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = 0;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Reports";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.fieldName = "RelatedTable";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_RelatedTableInfo = new NumberField(field);
			_fieldInfo.addItem(_RelatedTableInfo);

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
			field.label = "Report Name";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Reports";
			field.role = ENRole.NotFound;
			field.fieldName = "ReportName";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_ReportNameInfo = new TextField(field);
			_fieldInfo.addItem(_ReportNameInfo);

			field = new FieldDescriptor();
			field.fid = 9;
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
			field.label = "Filter";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Reports";
			field.role = ENRole.NotFound;
			field.fieldName = "Filter";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_FilterInfo = new TextField(field);
			_fieldInfo.addItem(_FilterInfo);

			field = new FieldDescriptor();
			field.fid = 10;
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
			field.label = "Sort";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Reports";
			field.role = ENRole.NotFound;
			field.fieldName = "Sort";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_SortInfo = new TextField(field);
			_fieldInfo.addItem(_SortInfo);

			field = new FieldDescriptor();
			field.fid = 11;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsSummary";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Reports";
			field.role = ENRole.NotFound;
			field.fieldName = "IsSummary";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsSummaryInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsSummaryInfo);

			field = new FieldDescriptor();
			field.fid = 12;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsSuppEdit";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Reports";
			field.role = ENRole.NotFound;
			field.fieldName = "IsSuppEdit";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsSuppEditInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsSuppEditInfo);

			field = new FieldDescriptor();
			field.fid = 13;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsSuppView";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Reports";
			field.role = ENRole.NotFound;
			field.fieldName = "IsSuppView";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsSuppViewInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsSuppViewInfo);

			field = new FieldDescriptor();
			field.fid = 14;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsGridEdit";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Reports";
			field.role = ENRole.NotFound;
			field.fieldName = "IsGridEdit";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsGridEditInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsGridEditInfo);

			field = new FieldDescriptor();
			field.fid = 7;
			field.lutFid = 7;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 6;
			field.label = "Table";
			field.allowNewChoices = false;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Reports";
			field.role = ENRole.NotFound;
			field.fieldName = "Table";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_TableInfo = new TextField(field);
			_fieldInfo.addItem(_TableInfo);

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
			field.tableName = "Reports";
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
			field.tableName = "Reports";
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
			field.tableName = "Reports";
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
			field.tableName = "Reports";
			field.role = ENRole.Modifier;
			field.fieldName = "LastModifiedBy";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_LastModifiedByInfo = new UserIdField(field);
			_fieldInfo.addItem(_LastModifiedByInfo);

		}

		public static function getInstance():Reports_Info
		{
			if(_instance == null)
				_instance = new Reports_Info(new Private);
			return _instance;
		}

		public function get tableName():String
		{
			return "Reports";
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
		private var _SeqAndNameInfo:TextField;
		private var _RelatedTableInfo:NumberField;
		private var _ReportNameInfo:TextField;
		private var _FilterInfo:TextField;
		private var _SortInfo:TextField;
		private var _IsSummaryInfo:BooleanField;
		private var _IsSuppEditInfo:BooleanField;
		private var _IsSuppViewInfo:BooleanField;
		private var _IsGridEditInfo:BooleanField;
		private var _TableInfo:TextField;
		private var _DateCreatedInfo:TimeStampField;
		private var _DateModifiedInfo:TimeStampField;
		private var _RecordOwnerInfo:UserIdField;
		private var _LastModifiedByInfo:UserIdField;

		// MetaData Information Objects getters
		public function get SeqAndName_Info():TextField					{return _SeqAndNameInfo;}
		public function get RelatedTable_Info():NumberField				{return _RelatedTableInfo;}
		public function get ReportName_Info():TextField					{return _ReportNameInfo;}
		public function get Filter_Info():TextField						{return _FilterInfo;}
		public function get Sort_Info():TextField						{return _SortInfo;}
		public function get IsSummary_Info():BooleanField				{return _IsSummaryInfo;}
		public function get IsSuppEdit_Info():BooleanField				{return _IsSuppEditInfo;}
		public function get IsSuppView_Info():BooleanField				{return _IsSuppViewInfo;}
		public function get IsGridEdit_Info():BooleanField				{return _IsGridEditInfo;}
		public function get Table_Info():TextField						{return _TableInfo;}
		public function get DateCreated_Info():TimeStampField			{return _DateCreatedInfo;}
		public function get DateModified_Info():TimeStampField			{return _DateModifiedInfo;}
		public function get RecordOwner_Info():UserIdField				{return _RecordOwnerInfo;}
		public function get LastModifiedBy_Info():UserIdField			{return _LastModifiedByInfo;}

		// Field getter variables
		private var _fieldNames:ArrayCollection = new ArrayCollection(["SeqAndName", "RelatedTable", "ReportName", "Filter", "Sort", 
																		"IsSummary", "IsSuppEdit", "IsSuppView", "IsGridEdit", "Table", "DateCreated", "DateModified", "RecordOwner", 
																		"LastModifiedBy", ]);
		private var _fieldInfo:ArrayCollection = new ArrayCollection();

		// Field getters
		public function get FieldNames():ArrayCollection				{return _fieldNames;}
		public function get FieldsInfo():ArrayCollection				{return _fieldInfo;}
	}
}

class Private{}
