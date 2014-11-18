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

	public class Consultant_Info implements IKingussieInfo
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		private static var _instance:Consultant_Info = null;

		function Consultant_Info(forcePrivateClass:Private)
		{
			// MetaData Initializers
			var field:FieldDescriptor;

			field = new FieldDescriptor();
			field.fid = 6;
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
			field.label = "CompanyName";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Consultant";
			field.role = ENRole.NotFound;
			field.fieldName = "CompanyName";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_CompanyNameInfo = new TextField(field);
			_fieldInfo.addItem(_CompanyNameInfo);

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
			field.label = "Contact";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Consultant";
			field.role = ENRole.NotFound;
			field.fieldName = "Contact";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_ContactInfo = new TextField(field);
			_fieldInfo.addItem(_ContactInfo);

			field = new FieldDescriptor();
			field.fid = 8;
			field.lutFid = 0;
			field.numLines = 6;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 60;
			field.lusFid = 0;
			field.label = "Address";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Consultant";
			field.role = ENRole.NotFound;
			field.fieldName = "Address";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_AddressInfo = new TextField(field);
			_fieldInfo.addItem(_AddressInfo);

			field = new FieldDescriptor();
			field.fid = 9;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.File;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Report Banner";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Consultant";
			field.role = ENRole.NotFound;
			field.fieldName = "ReportBanner";
			field.required = false;
			field.maxVersions = 3;
			field.foreignKey = 0;
			field.findEnabled = true;
			_ReportBannerInfo = new FileField(field);
			_fieldInfo.addItem(_ReportBannerInfo);

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
			field.label = "MapLatLong";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Consultant";
			field.role = ENRole.NotFound;
			field.fieldName = "MapLatLong";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_MapLatLongInfo = new TextField(field);
			_fieldInfo.addItem(_MapLatLongInfo);

			field = new FieldDescriptor();
			field.fid = 11;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "MapZoom";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = -1;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Consultant";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.fieldName = "MapZoom";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_MapZoomInfo = new NumberField(field);
			_fieldInfo.addItem(_MapZoomInfo);

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
			field.tableName = "Consultant";
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
			field.tableName = "Consultant";
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
			field.tableName = "Consultant";
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
			field.tableName = "Consultant";
			field.role = ENRole.Modifier;
			field.fieldName = "LastModifiedBy";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_LastModifiedByInfo = new UserIdField(field);
			_fieldInfo.addItem(_LastModifiedByInfo);

		}

		public static function getInstance():Consultant_Info
		{
			if(_instance == null)
				_instance = new Consultant_Info(new Private);
			return _instance;
		}

		public function get tableName():String
		{
			return "Consultant";
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
		private var _CompanyNameInfo:TextField;
		private var _ContactInfo:TextField;
		private var _AddressInfo:TextField;
		private var _ReportBannerInfo:FileField;
		private var _MapLatLongInfo:TextField;
		private var _MapZoomInfo:NumberField;
		private var _DateCreatedInfo:TimeStampField;
		private var _DateModifiedInfo:TimeStampField;
		private var _RecordOwnerInfo:UserIdField;
		private var _LastModifiedByInfo:UserIdField;

		// MetaData Information Objects getters
		public function get CompanyName_Info():TextField				{return _CompanyNameInfo;}
		public function get Contact_Info():TextField					{return _ContactInfo;}
		public function get Address_Info():TextField					{return _AddressInfo;}
		public function get ReportBanner_Info():FileField				{return _ReportBannerInfo;}
		public function get MapLatLong_Info():TextField					{return _MapLatLongInfo;}
		public function get MapZoom_Info():NumberField					{return _MapZoomInfo;}
		public function get DateCreated_Info():TimeStampField			{return _DateCreatedInfo;}
		public function get DateModified_Info():TimeStampField			{return _DateModifiedInfo;}
		public function get RecordOwner_Info():UserIdField				{return _RecordOwnerInfo;}
		public function get LastModifiedBy_Info():UserIdField			{return _LastModifiedByInfo;}

		// Field getter variables
		private var _fieldNames:ArrayCollection = new ArrayCollection(["CompanyName", "Contact", "Address", "ReportBanner", 
																		"MapLatLong", "MapZoom", "DateCreated", "DateModified", "RecordOwner", "LastModifiedBy", ]);
		private var _fieldInfo:ArrayCollection = new ArrayCollection();

		// Field getters
		public function get FieldNames():ArrayCollection				{return _fieldNames;}
		public function get FieldsInfo():ArrayCollection				{return _fieldInfo;}
	}
}

class Private{}
