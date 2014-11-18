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

	public class FacilityUsers_Info implements IKingussieInfo
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		private static var _instance:FacilityUsers_Info = null;

		function FacilityUsers_Info(forcePrivateClass:Private)
		{
			// MetaData Initializers
			var field:FieldDescriptor;

			field = new FieldDescriptor();
			field.fid = 10;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.UserId;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Facility User";
			field.allowNewChoices = true;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "FacilityUsers";
			field.role = ENRole.NotFound;
			field.fieldName = "FacilityUser";
			field.required = true;
			field.foreignKey = 0;
			field.findEnabled = true;
			_FacilityUserInfo = new UserIdField(field);
			_fieldInfo.addItem(_FacilityUserInfo);

			field = new FieldDescriptor();
			field.fid = 6;
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
			field.tableName = "FacilityUsers";
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
			field.fid = 7;
			field.lutFid = 30;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 6;
			field.label = "Client";
			field.allowNewChoices = false;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "FacilityUsers";
			field.role = ENRole.NotFound;
			field.fieldName = "Client";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_ClientInfo = new TextField(field);
			_fieldInfo.addItem(_ClientInfo);

			field = new FieldDescriptor();
			field.fid = 8;
			field.lutFid = 6;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 6;
			field.label = "Facility";
			field.allowNewChoices = false;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "FacilityUsers";
			field.role = ENRole.NotFound;
			field.fieldName = "Facility";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_FacilityInfo = new TextField(field);
			_fieldInfo.addItem(_FacilityInfo);

			field = new FieldDescriptor();
			field.fid = 9;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Allowed Facility User";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = -1;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "FacilityUsers";
			field.blankIsZero = false;
			field.role = ENRole.NotFound;
			field.fieldName = "AllowedFacilityUser";
			field.required = false;
			field.formula = "If([Facility User]=User(),1,0)";
			field.foreignKey = 0;
			field.findEnabled = true;
			_AllowedFacilityUserInfo = new NumberField(field);
			_fieldInfo.addItem(_AllowedFacilityUserInfo);

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
			field.tableName = "FacilityUsers";
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
			field.tableName = "FacilityUsers";
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
			field.tableName = "FacilityUsers";
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
			field.tableName = "FacilityUsers";
			field.role = ENRole.Modifier;
			field.fieldName = "LastModifiedBy";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_LastModifiedByInfo = new UserIdField(field);
			_fieldInfo.addItem(_LastModifiedByInfo);

		}

		public static function getInstance():FacilityUsers_Info
		{
			if(_instance == null)
				_instance = new FacilityUsers_Info(new Private);
			return _instance;
		}

		public function get tableName():String
		{
			return "FacilityUsers";
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
		private var _FacilityUserInfo:UserIdField;
		private var _RelatedFacilityInfo:NumberField;
		private var _ClientInfo:TextField;
		private var _FacilityInfo:TextField;
		private var _AllowedFacilityUserInfo:NumberField;
		private var _DateCreatedInfo:TimeStampField;
		private var _DateModifiedInfo:TimeStampField;
		private var _RecordOwnerInfo:UserIdField;
		private var _LastModifiedByInfo:UserIdField;

		// MetaData Information Objects getters
		public function get FacilityUser_Info():UserIdField				{return _FacilityUserInfo;}
		public function get RelatedFacility_Info():NumberField			{return _RelatedFacilityInfo;}
		public function get Client_Info():TextField						{return _ClientInfo;}
		public function get Facility_Info():TextField					{return _FacilityInfo;}
		public function get AllowedFacilityUser_Info():NumberField		{return _AllowedFacilityUserInfo;}
		public function get DateCreated_Info():TimeStampField			{return _DateCreatedInfo;}
		public function get DateModified_Info():TimeStampField			{return _DateModifiedInfo;}
		public function get RecordOwner_Info():UserIdField				{return _RecordOwnerInfo;}
		public function get LastModifiedBy_Info():UserIdField			{return _LastModifiedByInfo;}

		// Field getter variables
		private var _fieldNames:ArrayCollection = new ArrayCollection(["FacilityUser", "RelatedFacility", "Client", "Facility", 
																		"AllowedFacilityUser", "DateCreated", "DateModified", "RecordOwner", "LastModifiedBy", ]);
		private var _fieldInfo:ArrayCollection = new ArrayCollection();

		// Field getters
		public function get FieldNames():ArrayCollection				{return _fieldNames;}
		public function get FieldsInfo():ArrayCollection				{return _fieldInfo;}
	}
}

class Private{}
