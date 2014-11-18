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

	public class Layers_Info implements IKingussieInfo
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		private static var _instance:Layers_Info = null;

		function Layers_Info(forcePrivateClass:Private)
		{
			// MetaData Initializers
			var field:FieldDescriptor;

			field = new FieldDescriptor();
			field.fid = 11;
			field.lutFid = 30;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 10;
			field.label = "Client";
			field.allowNewChoices = false;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Layers";
			field.role = ENRole.NotFound;
			field.fieldName = "Client";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_ClientInfo = new TextField(field);
			_fieldInfo.addItem(_ClientInfo);

			field = new FieldDescriptor();
			field.fid = 12;
			field.lutFid = 31;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 10;
			field.label = "Facility";
			field.allowNewChoices = false;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Layers";
			field.role = ENRole.NotFound;
			field.fieldName = "Facility";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_FacilityInfo = new TextField(field);
			_fieldInfo.addItem(_FacilityInfo);

			field = new FieldDescriptor();
			field.fid = 13;
			field.lutFid = 6;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 10;
			field.label = "Roof";
			field.allowNewChoices = false;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Layers";
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
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Layer Number";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = 0;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Layers";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.fieldName = "LayerNumber";
			field.required = true;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_LayerNumberInfo = new NumberField(field);
			_fieldInfo.addItem(_LayerNumberInfo);

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
			field.width = 40;
			field.lusFid = 0;
			field.label = "Layer Type";
			field.allowNewChoices = true;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Layers";
			field.role = ENRole.NotFound;
			field.fieldName = "LayerType";
			field.required = true;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_LayerTypeInfo = new ChoiceField(field);
			_fieldInfo.addItem(_LayerTypeInfo);

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
			field.label = "Description";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Layers";
			field.role = ENRole.NotFound;
			field.fieldName = "Description";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_DescriptionInfo = new TextField(field);
			_fieldInfo.addItem(_DescriptionInfo);

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
			field.label = "Attachment";
			field.allowNewChoices = true;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Layers";
			field.role = ENRole.NotFound;
			field.fieldName = "Attachment";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_AttachmentInfo = new ChoiceField(field);
			_fieldInfo.addItem(_AttachmentInfo);

			field = new FieldDescriptor();
			field.fid = 10;
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
			field.tableName = "Layers";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.fieldName = "RelatedSection";
			field.required = true;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_RelatedSectionInfo = new NumberField(field);
			_fieldInfo.addItem(_RelatedSectionInfo);

			field = new FieldDescriptor();
			field.fid = 18;
			field.lutFid = 77;
			field.doesDataCopy = false;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 10;
			field.label = "Allowed Section User";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = -1;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Layers";
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
			field.fid = 19;
			field.lutFid = 78;
			field.doesDataCopy = false;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 10;
			field.label = "Allowed Client User";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = -1;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Layers";
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
			field.lutFid = 79;
			field.doesDataCopy = false;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 10;
			field.label = "Allowed Facility User";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = -1;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Layers";
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
			field.tableName = "Layers";
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
			field.tableName = "Layers";
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
			field.tableName = "Layers";
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
			field.tableName = "Layers";
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
			field.tableName = "Layers";
			field.role = ENRole.Modifier;
			field.fieldName = "LastModifiedBy";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_LastModifiedByInfo = new UserIdField(field);
			_fieldInfo.addItem(_LastModifiedByInfo);

		}

		public static function getInstance():Layers_Info
		{
			if(_instance == null)
				_instance = new Layers_Info(new Private);
			return _instance;
		}

		public function get tableName():String
		{
			return "Layers";
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
		private var _LayerNumberInfo:NumberField;
		private var _LayerTypeInfo:ChoiceField;
		private var _DescriptionInfo:TextField;
		private var _AttachmentInfo:ChoiceField;
		private var _RelatedSectionInfo:NumberField;
		private var _AllowedSectionUserInfo:NumberField;
		private var _AllowedClientUserInfo:NumberField;
		private var _AllowedFacilityUserInfo:NumberField;
		private var _AllowedUserInfo:NumberField;
		private var _DateCreatedInfo:TimeStampField;
		private var _DateModifiedInfo:TimeStampField;
		private var _RecordOwnerInfo:UserIdField;
		private var _LastModifiedByInfo:UserIdField;

		// MetaData Information Objects getters
		public function get Client_Info():TextField						{return _ClientInfo;}
		public function get Facility_Info():TextField					{return _FacilityInfo;}
		public function get Roof_Info():TextField						{return _RoofInfo;}
		public function get LayerNumber_Info():NumberField				{return _LayerNumberInfo;}
		public function get LayerType_Info():ChoiceField				{return _LayerTypeInfo;}
		public function get Description_Info():TextField				{return _DescriptionInfo;}
		public function get Attachment_Info():ChoiceField				{return _AttachmentInfo;}
		public function get RelatedSection_Info():NumberField			{return _RelatedSectionInfo;}
		public function get AllowedSectionUser_Info():NumberField		{return _AllowedSectionUserInfo;}
		public function get AllowedClientUser_Info():NumberField		{return _AllowedClientUserInfo;}
		public function get AllowedFacilityUser_Info():NumberField		{return _AllowedFacilityUserInfo;}
		public function get AllowedUser_Info():NumberField				{return _AllowedUserInfo;}
		public function get DateCreated_Info():TimeStampField			{return _DateCreatedInfo;}
		public function get DateModified_Info():TimeStampField			{return _DateModifiedInfo;}
		public function get RecordOwner_Info():UserIdField				{return _RecordOwnerInfo;}
		public function get LastModifiedBy_Info():UserIdField			{return _LastModifiedByInfo;}

		// Field getter variables
		private var _fieldNames:ArrayCollection = new ArrayCollection(["Client", "Facility", "Roof", "LayerNumber", "LayerType", 
																		"Description", "Attachment", "RelatedSection", "AllowedSectionUser", "AllowedClientUser", "AllowedFacilityUser", 
																		"AllowedUser", "DateCreated", "DateModified", "RecordOwner", "LastModifiedBy", ]);
		private var _fieldInfo:ArrayCollection = new ArrayCollection();

		// Field getters
		public function get FieldNames():ArrayCollection				{return _fieldNames;}
		public function get FieldsInfo():ArrayCollection				{return _fieldInfo;}
	}
}

class Private{}
