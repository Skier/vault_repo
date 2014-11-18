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
			field.lusFid = 0;
			field.unique = false;
			field.width = 40;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.required = true;
			field.doesDataCopy = true;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = true;
			field.fieldName = "SegmentName";
			field.formula = "";
			field.tableName = "Segments";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "SegmentName";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 6;
			field.numLines = 0;
			_SegmentNameInfo = new TextField(field);
			_fieldInfo.addItem(_SegmentNameInfo);

			field = new FieldDescriptor();
			field.commaStart = 4;
			field.lusFid = 0;
			field.unique = false;
			field.blankIsZero = true;
			field.fieldType = ENFieldType.Float;
			field.required = false;
			field.doesDataCopy = false;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.Summary;
			field.units = "";
			field.findEnabled = false;
			field.fieldName = "ClientsCount";
			field.formula = "";
			field.tableName = "Segments";
			field.baseType = ENBaseType.Float;
			field.allowNewChoices = false;
			field.decimalPlaces = 0;
			field.label = "ClientsCount";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 9;
			_ClientsCountInfo = new NumberField(field);
			_fieldInfo.addItem(_ClientsCountInfo);

			field = new FieldDescriptor();
			field.commaStart = 4;
			field.lusFid = 0;
			field.unique = false;
			field.blankIsZero = true;
			field.fieldType = ENFieldType.Float;
			field.required = false;
			field.doesDataCopy = false;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.Summary;
			field.units = "";
			field.findEnabled = false;
			field.fieldName = "FacilitiesCount";
			field.formula = "";
			field.tableName = "Segments";
			field.baseType = ENBaseType.Float;
			field.allowNewChoices = false;
			field.decimalPlaces = 0;
			field.label = "FacilitiesCount";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 10;
			_FacilitiesCountInfo = new NumberField(field);
			_fieldInfo.addItem(_FacilitiesCountInfo);

			field = new FieldDescriptor();
			field.commaStart = 4;
			field.lusFid = 0;
			field.unique = false;
			field.blankIsZero = true;
			field.fieldType = ENFieldType.Float;
			field.required = false;
			field.doesDataCopy = false;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.Summary;
			field.units = "";
			field.findEnabled = false;
			field.fieldName = "SectionsCount";
			field.formula = "";
			field.tableName = "Segments";
			field.baseType = ENBaseType.Float;
			field.allowNewChoices = false;
			field.decimalPlaces = 0;
			field.label = "SectionsCount";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 11;
			_SectionsCountInfo = new NumberField(field);
			_fieldInfo.addItem(_SectionsCountInfo);

			field = new FieldDescriptor();
			field.commaStart = 4;
			field.lusFid = 0;
			field.unique = false;
			field.blankIsZero = true;
			field.fieldType = ENFieldType.Float;
			field.required = false;
			field.doesDataCopy = false;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.Summary;
			field.units = "";
			field.findEnabled = false;
			field.fieldName = "TotalSqFt";
			field.formula = "";
			field.tableName = "Segments";
			field.baseType = ENBaseType.Float;
			field.allowNewChoices = false;
			field.decimalPlaces = 0;
			field.label = "TotalSqFt";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 12;
			_TotalSqFtInfo = new NumberField(field);
			_fieldInfo.addItem(_TotalSqFtInfo);

			field = new FieldDescriptor();
			field.commaStart = 3;
			field.unique = false;
			field.lusFid = 0;
			field.fieldType = ENFieldType.Currency;
			field.carryChoices = true;
			field.doesDataCopy = false;
			field.units = "";
			field.allowNewChoices = false;
			field.currencySymbol = "$";
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.blankIsZero = true;
			field.required = false;
			field.mode = ENMode.Summary;
			field.lutFid = 0;
			field.findEnabled = false;
			field.fieldName = "TotalValue";
			field.tableName = "Segments";
			field.formula = "";
			field.baseType = ENBaseType.Float;
			field.decimalPlaces = 0;
			field.currencyFormat = "1";
			field.label = "TotalValue";
			field.foreignKey = 0;
			field.fid = 13;
			_TotalValueInfo = new CurrencyField(field);
			_fieldInfo.addItem(_TotalValueInfo);

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
			field.tableName = "Segments";
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
			field.tableName = "Segments";
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
			field.tableName = "Segments";
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
			field.tableName = "Segments";
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
		public function get DateCreated_Info():TimeStampField			{return _DateCreatedInfo;}
		public function get DateModified_Info():TimeStampField			{return _DateModifiedInfo;}
		public function get RecordOwner_Info():UserIdField				{return _RecordOwnerInfo;}
		public function get LastModifiedBy_Info():UserIdField			{return _LastModifiedByInfo;}

		// Field getter variables
		private var _fieldNames:ArrayCollection = new ArrayCollection(["SegmentName", "ClientsCount", "FacilitiesCount", 
																		"SectionsCount", "TotalSqFt", "TotalValue", "DateCreated", "DateModified", "RecordOwner", "LastModifiedBy", ]);
		private var _fieldInfo:ArrayCollection = new ArrayCollection();

		// Field getters
		public function get FieldNames():ArrayCollection				{return _fieldNames;}
		public function get FieldsInfo():ArrayCollection				{return _fieldInfo;}
	}
}

class Private{}
