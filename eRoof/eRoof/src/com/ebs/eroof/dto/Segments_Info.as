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

	public class Segments_Info implements IKingussieInfo
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		private static var _instance:Segments_Info = null;

		function Segments_Info(forcePrivateClass:Private)
		{
			// MetaData Initializers
			var field:FieldDescriptor;

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
			field.width = 40;
			field.lusFid = 0;
			field.label = "Segment Name";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Segments";
			field.role = ENRole.NotFound;
			field.fieldName = "SegmentName";
			field.required = true;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_SegmentNameInfo = new TextField(field);
			_fieldInfo.addItem(_SegmentNameInfo);

			field = new FieldDescriptor();
			field.fid = 9;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "ClientsCount";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = 0;
			field.mode = ENMode.Summary;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Segments";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.fieldName = "ClientsCount";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = false;
			_ClientsCountInfo = new NumberField(field);
			_fieldInfo.addItem(_ClientsCountInfo);

			field = new FieldDescriptor();
			field.fid = 10;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "FacilitiesCount";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = 0;
			field.mode = ENMode.Summary;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Segments";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.fieldName = "FacilitiesCount";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = false;
			_FacilitiesCountInfo = new NumberField(field);
			_fieldInfo.addItem(_FacilitiesCountInfo);

			field = new FieldDescriptor();
			field.fid = 11;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "SectionsCount";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = 0;
			field.mode = ENMode.Summary;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Segments";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.fieldName = "SectionsCount";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = false;
			_SectionsCountInfo = new NumberField(field);
			_fieldInfo.addItem(_SectionsCountInfo);

			field = new FieldDescriptor();
			field.fid = 12;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Total Sq Ft";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = 0;
			field.mode = ENMode.Summary;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Segments";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.fieldName = "TotalSqFt";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = false;
			_TotalSqFtInfo = new NumberField(field);
			_fieldInfo.addItem(_TotalSqFtInfo);

			field = new FieldDescriptor();
			field.fid = 13;
			field.unique = false;
			field.commaStart = 3;
			field.fieldHelp = "";
			field.allowNewChoices = false;
			field.mode = ENMode.Summary;
			field.baseType = ENBaseType.Float;
			field.tableName = "Segments";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.required = false;
			field.fieldName = "TotalValue";
			field.foreignKey = 0;
			field.currencyFormat = "1";
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.fieldType = ENFieldType.Currency;
			field.lusFid = 0;
			field.label = "Total Value";
			field.units = "";
			field.decimalPlaces = 0;
			field.carryChoices = true;
			field.currencySymbol = "$";
			field.findEnabled = false;
			field.formula = "";
			_TotalValueInfo = new CurrencyField(field);
			_fieldInfo.addItem(_TotalValueInfo);

			field = new FieldDescriptor();
			field.fid = 8;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.URL;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Add Client";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Segments";
			field.role = ENRole.NotFound;
			field.appearsAs = "Add  Client";
			field.fieldName = "AddClient";
			field.required = false;
			field.formula = "URLRoot() & \"db/\" & [_DBID_CLIENTS] & \"?a=API_GenAddRecordForm&_fid_39=\" & URLEncode ([Record ID#])& \"&z=\" & Rurl()";
			field.foreignKey = 0;
			field.findEnabled = false;
			_AddClientInfo = new URLField(field);
			_fieldInfo.addItem(_AddClientInfo);

			field = new FieldDescriptor();
			field.fid = 15;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.URL;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Clients";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Segments";
			field.role = ENRole.NotFound;
			field.appearsAs = "Clients";
			field.fieldName = "Clients";
			field.required = false;
			field.formula = "\"https://workplace.intuit.com/db/be9nwdiyu?a=q&qt=tab&dvqid=1&clist=7.11.12.30.31.32.33.25.72&slist=7&query={Related Segment.ex.'\" & [Record ID#] & \"'}&opts=so-A.gb-X.&gemi=s\"";
			field.foreignKey = 0;
			field.findEnabled = true;
			_ClientsInfo = new URLField(field);
			_fieldInfo.addItem(_ClientsInfo);

			field = new FieldDescriptor();
			field.fid = 16;
			field.lutFid = 0;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = true;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 0;
			field.label = "Calc Name";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Segments";
			field.role = ENRole.NotFound;
			field.fieldName = "CalcName";
			field.required = false;
			field.formula = "\"<span style='width:300px'><b>\" & [Segment Name] & \"</b> test</span>\"";
			field.foreignKey = 0;
			field.findEnabled = true;
			_CalcNameInfo = new TextField(field);
			_fieldInfo.addItem(_CalcNameInfo);

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
			field.tableName = "Segments";
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
			field.tableName = "Segments";
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
			field.tableName = "Segments";
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
			field.tableName = "Segments";
			field.role = ENRole.Modifier;
			field.fieldName = "LastModifiedBy";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_LastModifiedByInfo = new UserIdField(field);
			_fieldInfo.addItem(_LastModifiedByInfo);

		}

		public static function getInstance():Segments_Info
		{
			if(_instance == null)
				_instance = new Segments_Info(new Private);
			return _instance;
		}

		public function get tableName():String
		{
			return "Segments";
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
		private var _SegmentNameInfo:TextField;
		private var _ClientsCountInfo:NumberField;
		private var _FacilitiesCountInfo:NumberField;
		private var _SectionsCountInfo:NumberField;
		private var _TotalSqFtInfo:NumberField;
		private var _TotalValueInfo:CurrencyField;
		private var _AddClientInfo:URLField;
		private var _ClientsInfo:URLField;
		private var _CalcNameInfo:TextField;
		private var _DateCreatedInfo:TimeStampField;
		private var _DateModifiedInfo:TimeStampField;
		private var _RecordOwnerInfo:UserIdField;
		private var _LastModifiedByInfo:UserIdField;

		// MetaData Information Objects getters
		public function get SegmentName_Info():TextField				{return _SegmentNameInfo;}
		public function get ClientsCount_Info():NumberField				{return _ClientsCountInfo;}
		public function get FacilitiesCount_Info():NumberField			{return _FacilitiesCountInfo;}
		public function get SectionsCount_Info():NumberField			{return _SectionsCountInfo;}
		public function get TotalSqFt_Info():NumberField				{return _TotalSqFtInfo;}
		public function get TotalValue_Info():CurrencyField				{return _TotalValueInfo;}
		public function get AddClient_Info():URLField					{return _AddClientInfo;}
		public function get Clients_Info():URLField						{return _ClientsInfo;}
		public function get CalcName_Info():TextField					{return _CalcNameInfo;}
		public function get DateCreated_Info():TimeStampField			{return _DateCreatedInfo;}
		public function get DateModified_Info():TimeStampField			{return _DateModifiedInfo;}
		public function get RecordOwner_Info():UserIdField				{return _RecordOwnerInfo;}
		public function get LastModifiedBy_Info():UserIdField			{return _LastModifiedByInfo;}

		// Field getter variables
		private var _fieldNames:ArrayCollection = new ArrayCollection(["SegmentName", "ClientsCount", "FacilitiesCount", 
																		"SectionsCount", "TotalSqFt", "TotalValue", "AddClient", "Clients", "CalcName", "DateCreated", "DateModified", 
																		"RecordOwner", "LastModifiedBy", ]);
		private var _fieldInfo:ArrayCollection = new ArrayCollection();

		// Field getters
		public function get FieldNames():ArrayCollection				{return _fieldNames;}
		public function get FieldsInfo():ArrayCollection				{return _fieldInfo;}
	}
}

class Private{}
