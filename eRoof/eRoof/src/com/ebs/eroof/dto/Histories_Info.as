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

	public class Histories_Info implements IKingussieInfo
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		private static var _instance:Histories_Info = null;

		function Histories_Info(forcePrivateClass:Private)
		{
			// MetaData Initializers
			var field:FieldDescriptor;

			field = new FieldDescriptor();
			field.fid = 6;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.Date;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Date Completed";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Int64;
			field.tableName = "Histories";
			field.role = ENRole.NotFound;
			field.fieldName = "DateCompleted";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_DateCompletedInfo = new DateField(field);
			_fieldInfo.addItem(_DateCompletedInfo);

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
			field.label = "Type of Work";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Histories";
			field.role = ENRole.NotFound;
			field.fieldName = "TypeOfWork";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_TypeOfWorkInfo = new ChoiceField(field);
			_fieldInfo.addItem(_TypeOfWorkInfo);

			field = new FieldDescriptor();
			field.fid = 8;
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
			field.label = "Company";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Histories";
			field.role = ENRole.NotFound;
			field.fieldName = "Company";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_CompanyInfo = new TextField(field);
			_fieldInfo.addItem(_CompanyInfo);

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
			field.label = "Allocation";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Histories";
			field.role = ENRole.NotFound;
			field.fieldName = "Allocation";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_AllocationInfo = new ChoiceField(field);
			_fieldInfo.addItem(_AllocationInfo);

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
			field.label = "Status";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Histories";
			field.role = ENRole.NotFound;
			field.fieldName = "Status";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_StatusInfo = new ChoiceField(field);
			_fieldInfo.addItem(_StatusInfo);

			field = new FieldDescriptor();
			field.fid = 11;
			field.unique = false;
			field.commaStart = 3;
			field.fieldHelp = "";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.baseType = ENBaseType.Float;
			field.tableName = "Histories";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.required = false;
			field.fieldName = "ActualCost";
			field.foreignKey = 0;
			field.currencyFormat = "1";
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.fieldType = ENFieldType.Currency;
			field.lusFid = 0;
			field.label = "Actual Cost";
			field.units = "";
			field.decimalPlaces = 2;
			field.carryChoices = true;
			field.currencySymbol = "$";
			field.findEnabled = true;
			field.formula = "";
			_ActualCostInfo = new CurrencyField(field);
			_fieldInfo.addItem(_ActualCostInfo);

			field = new FieldDescriptor();
			field.fid = 12;
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
			field.label = "Details";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Histories";
			field.role = ENRole.NotFound;
			field.fieldName = "Details";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_DetailsInfo = new ChoiceField(field);
			_fieldInfo.addItem(_DetailsInfo);

			field = new FieldDescriptor();
			field.fid = 14;
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
			field.tableName = "Histories";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.fieldName = "RelatedSection";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_RelatedSectionInfo = new NumberField(field);
			_fieldInfo.addItem(_RelatedSectionInfo);

			field = new FieldDescriptor();
			field.fid = 15;
			field.lutFid = 30;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 14;
			field.label = "Client";
			field.allowNewChoices = false;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Histories";
			field.role = ENRole.NotFound;
			field.fieldName = "Client";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_ClientInfo = new TextField(field);
			_fieldInfo.addItem(_ClientInfo);

			field = new FieldDescriptor();
			field.fid = 16;
			field.lutFid = 31;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 14;
			field.label = "Facility";
			field.allowNewChoices = false;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Histories";
			field.role = ENRole.NotFound;
			field.fieldName = "Facility";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_FacilityInfo = new TextField(field);
			_fieldInfo.addItem(_FacilityInfo);

			field = new FieldDescriptor();
			field.fid = 17;
			field.lutFid = 6;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 14;
			field.label = "Roof";
			field.allowNewChoices = false;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Histories";
			field.role = ENRole.NotFound;
			field.fieldName = "Roof";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_RoofInfo = new TextField(field);
			_fieldInfo.addItem(_RoofInfo);

			field = new FieldDescriptor();
			field.fid = 18;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.File;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Photo";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Histories";
			field.role = ENRole.NotFound;
			field.fieldName = "Photo";
			field.required = false;
			field.maxVersions = 3;
			field.foreignKey = 0;
			field.findEnabled = true;
			_PhotoInfo = new FileField(field);
			_fieldInfo.addItem(_PhotoInfo);

			field = new FieldDescriptor();
			field.fid = 19;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.File;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Report";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Histories";
			field.role = ENRole.NotFound;
			field.fieldName = "Report";
			field.required = false;
			field.maxVersions = 3;
			field.foreignKey = 0;
			field.findEnabled = true;
			_ReportInfo = new FileField(field);
			_fieldInfo.addItem(_ReportInfo);

			field = new FieldDescriptor();
			field.fid = 20;
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
			field.label = "Thumbnail";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Histories";
			field.role = ENRole.NotFound;
			field.fieldName = "Thumbnail";
			field.required = false;
			field.formula = "\"<img src='\" & [Photo_URL] & [Photo] & \"' height=100>\"";
			field.foreignKey = 0;
			field.findEnabled = true;
			_ThumbnailInfo = new TextField(field);
			_fieldInfo.addItem(_ThumbnailInfo);

			field = new FieldDescriptor();
			field.fid = 21;
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
			field.label = "Annotation";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Histories";
			field.role = ENRole.NotFound;
			field.fieldName = "Annotation";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_AnnotationInfo = new TextField(field);
			_fieldInfo.addItem(_AnnotationInfo);

			field = new FieldDescriptor();
			field.fid = 22;
			field.lutFid = 54;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.UserId;
			field.fieldHelp = "";
			field.lusFid = 14;
			field.label = "Allowed Client User";
			field.allowNewChoices = false;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Histories";
			field.role = ENRole.NotFound;
			field.fieldName = "AllowedClientUser";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_AllowedClientUserInfo = new UserIdField(field);
			_fieldInfo.addItem(_AllowedClientUserInfo);

			field = new FieldDescriptor();
			field.fid = 23;
			field.lutFid = 55;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.UserId;
			field.fieldHelp = "";
			field.lusFid = 14;
			field.label = "Allowed Contractor User";
			field.allowNewChoices = false;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Histories";
			field.role = ENRole.NotFound;
			field.fieldName = "AllowedContractorUser";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_AllowedContractorUserInfo = new UserIdField(field);
			_fieldInfo.addItem(_AllowedContractorUserInfo);

			field = new FieldDescriptor();
			field.fid = 24;
			field.lutFid = 49;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.UserId;
			field.fieldHelp = "";
			field.lusFid = 14;
			field.label = "Allowed Inspector User";
			field.allowNewChoices = false;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Histories";
			field.role = ENRole.NotFound;
			field.fieldName = "AllowedInspectorUser";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_AllowedInspectorUserInfo = new UserIdField(field);
			_fieldInfo.addItem(_AllowedInspectorUserInfo);

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
			field.tableName = "Histories";
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
			field.tableName = "Histories";
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
			field.tableName = "Histories";
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
			field.tableName = "Histories";
			field.role = ENRole.Modifier;
			field.fieldName = "LastModifiedBy";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_LastModifiedByInfo = new UserIdField(field);
			_fieldInfo.addItem(_LastModifiedByInfo);

		}

		public static function getInstance():Histories_Info
		{
			if(_instance == null)
				_instance = new Histories_Info(new Private);
			return _instance;
		}

		public function get tableName():String
		{
			return "Histories";
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
		private var _DateCompletedInfo:DateField;
		private var _TypeOfWorkInfo:ChoiceField;
		private var _CompanyInfo:TextField;
		private var _AllocationInfo:ChoiceField;
		private var _StatusInfo:ChoiceField;
		private var _ActualCostInfo:CurrencyField;
		private var _DetailsInfo:ChoiceField;
		private var _RelatedSectionInfo:NumberField;
		private var _ClientInfo:TextField;
		private var _FacilityInfo:TextField;
		private var _RoofInfo:TextField;
		private var _PhotoInfo:FileField;
		private var _ReportInfo:FileField;
		private var _ThumbnailInfo:TextField;
		private var _AnnotationInfo:TextField;
		private var _AllowedClientUserInfo:UserIdField;
		private var _AllowedContractorUserInfo:UserIdField;
		private var _AllowedInspectorUserInfo:UserIdField;
		private var _DateCreatedInfo:TimeStampField;
		private var _DateModifiedInfo:TimeStampField;
		private var _RecordOwnerInfo:UserIdField;
		private var _LastModifiedByInfo:UserIdField;

		// MetaData Information Objects getters
		public function get DateCompleted_Info():DateField				{return _DateCompletedInfo;}
		public function get TypeOfWork_Info():ChoiceField				{return _TypeOfWorkInfo;}
		public function get Company_Info():TextField					{return _CompanyInfo;}
		public function get Allocation_Info():ChoiceField				{return _AllocationInfo;}
		public function get Status_Info():ChoiceField					{return _StatusInfo;}
		public function get ActualCost_Info():CurrencyField				{return _ActualCostInfo;}
		public function get Details_Info():ChoiceField					{return _DetailsInfo;}
		public function get RelatedSection_Info():NumberField			{return _RelatedSectionInfo;}
		public function get Client_Info():TextField						{return _ClientInfo;}
		public function get Facility_Info():TextField					{return _FacilityInfo;}
		public function get Roof_Info():TextField						{return _RoofInfo;}
		public function get Photo_Info():FileField						{return _PhotoInfo;}
		public function get Report_Info():FileField						{return _ReportInfo;}
		public function get Thumbnail_Info():TextField					{return _ThumbnailInfo;}
		public function get Annotation_Info():TextField					{return _AnnotationInfo;}
		public function get AllowedClientUser_Info():UserIdField		{return _AllowedClientUserInfo;}
		public function get AllowedContractorUser_Info():UserIdField	{return _AllowedContractorUserInfo;}
		public function get AllowedInspectorUser_Info():UserIdField		{return _AllowedInspectorUserInfo;}
		public function get DateCreated_Info():TimeStampField			{return _DateCreatedInfo;}
		public function get DateModified_Info():TimeStampField			{return _DateModifiedInfo;}
		public function get RecordOwner_Info():UserIdField				{return _RecordOwnerInfo;}
		public function get LastModifiedBy_Info():UserIdField			{return _LastModifiedByInfo;}

		// Field getter variables
		private var _fieldNames:ArrayCollection = new ArrayCollection(["DateCompleted", "TypeOfWork", "Company", "Allocation", 
																		"Status", "ActualCost", "Details", "RelatedSection", "Client", "Facility", "Roof", "Photo", "Report", "Thumbnail", 
																		"Annotation", "AllowedClientUser", "AllowedContractorUser", "AllowedInspectorUser", "DateCreated", "DateModified", 
																		"RecordOwner", "LastModifiedBy", ]);
		private var _fieldInfo:ArrayCollection = new ArrayCollection();

		// Field getters
		public function get FieldNames():ArrayCollection				{return _fieldNames;}
		public function get FieldsInfo():ArrayCollection				{return _fieldInfo;}
	}
}

class Private{}
