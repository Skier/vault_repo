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
			field.lusFid = 10;
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
			field.tableName = "Layers";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Client";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 11;
			field.numLines = 0;
			_ClientInfo = new TextField(field);
			_fieldInfo.addItem(_ClientInfo);

			field = new FieldDescriptor();
			field.lusFid = 10;
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
			field.tableName = "Layers";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Facility";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 12;
			field.numLines = 0;
			_FacilityInfo = new TextField(field);
			_fieldInfo.addItem(_FacilityInfo);

			field = new FieldDescriptor();
			field.lusFid = 10;
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
			field.tableName = "Layers";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Roof";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 13;
			field.numLines = 0;
			_RoofInfo = new TextField(field);
			_fieldInfo.addItem(_RoofInfo);

			field = new FieldDescriptor();
			field.commaStart = 4;
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
			field.fieldName = "LayerNumber";
			field.formula = "";
			field.tableName = "Layers";
			field.baseType = ENBaseType.Float;
			field.allowNewChoices = false;
			field.decimalPlaces = 0;
			field.label = "Layer Number";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 6;
			_LayerNumberInfo = new NumberField(field);
			_fieldInfo.addItem(_LayerNumberInfo);

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
			field.fieldName = "LayerType";
			field.formula = "";
			field.tableName = "Layers";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = true;
			field.label = "Layer Type";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 7;
			field.numLines = 0;
			_LayerTypeInfo = new ChoiceField(field);
			_fieldInfo.addItem(_LayerTypeInfo);

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
			field.fieldName = "Description";
			field.formula = "";
			field.tableName = "Layers";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Description";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 8;
			field.numLines = 0;
			_DescriptionInfo = new TextField(field);
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
			field.fieldName = "Attachment";
			field.formula = "";
			field.tableName = "Layers";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = true;
			field.label = "Attachment";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 9;
			field.numLines = 0;
			_AttachmentInfo = new ChoiceField(field);
			_fieldInfo.addItem(_AttachmentInfo);

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
			field.tableName = "Layers";
			field.baseType = ENBaseType.Float;
			field.allowNewChoices = false;
			field.decimalPlaces = 0;
			field.label = "Related Section";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 10;
			_RelatedSectionInfo = new NumberField(field);
			_fieldInfo.addItem(_RelatedSectionInfo);

			field = new FieldDescriptor();
			field.commaStart = 4;
			field.lusFid = 10;
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
			field.tableName = "Layers";
			field.baseType = ENBaseType.Float;
			field.allowNewChoices = false;
			field.decimalPlaces = -1;
			field.label = "Allowed Section User";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 18;
			_AllowedSectionUserInfo = new NumberField(field);
			_fieldInfo.addItem(_AllowedSectionUserInfo);

			field = new FieldDescriptor();
			field.commaStart = 4;
			field.lusFid = 10;
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
			field.tableName = "Layers";
			field.baseType = ENBaseType.Float;
			field.allowNewChoices = false;
			field.decimalPlaces = -1;
			field.label = "Allowed Client User";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 19;
			_AllowedClientUserInfo = new NumberField(field);
			_fieldInfo.addItem(_AllowedClientUserInfo);

			field = new FieldDescriptor();
			field.commaStart = 4;
			field.lusFid = 10;
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
			field.tableName = "Layers";
			field.baseType = ENBaseType.Float;
			field.allowNewChoices = false;
			field.decimalPlaces = -1;
			field.label = "Allowed Facility User";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 20;
			_AllowedFacilityUserInfo = new NumberField(field);
			_fieldInfo.addItem(_AllowedFacilityUserInfo);

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
			field.tableName = "Layers";
			field.baseType = ENBaseType.Float;
			field.allowNewChoices = false;
			field.decimalPlaces = -1;
			field.label = "Allowed User";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 21;
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
			field.tableName = "Layers";
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
			field.tableName = "Layers";
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
			field.tableName = "Layers";
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
			field.tableName = "Layers";
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
