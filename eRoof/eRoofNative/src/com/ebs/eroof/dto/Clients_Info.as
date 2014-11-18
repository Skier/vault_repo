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

	public class Clients_Info implements IKingussieInfo
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		private static var _instance:Clients_Info = null;

		function Clients_Info(forcePrivateClass:Private)
		{
			// MetaData Initializers
			var field:FieldDescriptor;

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = true;
			field.width = 40;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.required = true;
			field.doesDataCopy = true;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = true;
			field.fieldName = "ClientName";
			field.formula = "";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "ClientName";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 7;
			field.numLines = 0;
			_ClientNameInfo = new TextField(field);
			_fieldInfo.addItem(_ClientNameInfo);

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
			field.fieldName = "Address";
			field.formula = "";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Address";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 9;
			field.numLines = 3;
			_AddressInfo = new TextField(field);
			_fieldInfo.addItem(_AddressInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.width = 20;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.required = false;
			field.doesDataCopy = true;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = true;
			field.fieldName = "City";
			field.formula = "";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "City";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 11;
			field.numLines = 0;
			_CityInfo = new TextField(field);
			_fieldInfo.addItem(_CityInfo);

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
			field.tableName = "Clients";
			field.baseType = ENBaseType.Float;
			field.allowNewChoices = false;
			field.decimalPlaces = 0;
			field.label = "FacilitiesCount";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 30;
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
			field.tableName = "Clients";
			field.baseType = ENBaseType.Float;
			field.allowNewChoices = false;
			field.decimalPlaces = 0;
			field.label = "SectionsCount";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 31;
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
			field.tableName = "Clients";
			field.baseType = ENBaseType.Float;
			field.allowNewChoices = false;
			field.decimalPlaces = 0;
			field.label = "TotalSqFt";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 32;
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
			field.tableName = "Clients";
			field.formula = "";
			field.baseType = ENBaseType.Float;
			field.decimalPlaces = 0;
			field.currencyFormat = "1";
			field.label = "TotalValue";
			field.foreignKey = 0;
			field.fid = 33;
			_TotalValueInfo = new CurrencyField(field);
			_fieldInfo.addItem(_TotalValueInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.width = 20;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.required = false;
			field.doesDataCopy = true;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = true;
			field.fieldName = "Province";
			field.formula = "";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Province";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 12;
			field.numLines = 0;
			_ProvinceInfo = new ChoiceField(field);
			_fieldInfo.addItem(_ProvinceInfo);

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
			field.fieldName = "RelatedSegment";
			field.formula = "";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Float;
			field.allowNewChoices = false;
			field.decimalPlaces = 0;
			field.label = "Related Segment";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 39;
			_RelatedSegmentInfo = new NumberField(field);
			_fieldInfo.addItem(_RelatedSegmentInfo);

			field = new FieldDescriptor();
			field.lusFid = 39;
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
			field.fieldName = "Segment";
			field.formula = "";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Segment";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 40;
			field.numLines = 0;
			_SegmentInfo = new TextField(field);
			_fieldInfo.addItem(_SegmentInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.appearsAs = "Add Document";
			field.fieldType = ENFieldType.URL;
			field.required = false;
			field.doesDataCopy = false;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.Virtual;
			field.findEnabled = false;
			field.fieldName = "AddDocument";
			field.formula = "URLRoot() & \"db/\" & [_DBID_CLIENT_ATTACHMENTS] & \"?a=API_GenAddRecordForm&_fid_12=\" & URLEncode ([Record ID#])& \"&z=\" & Rurl()";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Add Document";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 50;
			_AddDocumentInfo = new URLField(field);
			_fieldInfo.addItem(_AddDocumentInfo);

			field = new FieldDescriptor();
			field.targetFID = 12;
			field.lusFid = 0;
			field.unique = false;
			field.fieldType = ENFieldType.DbLink;
			field.required = false;
			field.doesDataCopy = false;
			field.carryChoices = true;
			field.targetDBID = "-b";
			field.lutFid = 0;
			field.mode = ENMode.Virtual;
			field.sourceFID = 3;
			field.findEnabled = true;
			field.fieldName = "Documents";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.exact = true;
			field.allowNewChoices = false;
			field.label = "Documents";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 49;
			field.coverText = "Client Documents";
			_DocumentsInfo = new DbLinkField(field);
			_fieldInfo.addItem(_DocumentsInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.appearsAs = "Add  Facility";
			field.fieldType = ENFieldType.URL;
			field.required = false;
			field.doesDataCopy = false;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.Virtual;
			field.findEnabled = false;
			field.fieldName = "AddFacility";
			field.formula = "URLRoot() & \"db/\" & [_DBID_FACILITIES] & \"?a=API_GenAddRecordForm&_fid_29=\" & URLEncode ([Record ID#])& \"&z=\" & Rurl()";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Add Facility";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 26;
			_AddFacilityInfo = new URLField(field);
			_fieldInfo.addItem(_AddFacilityInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.width = 20;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.required = false;
			field.doesDataCopy = true;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = true;
			field.fieldName = "Category";
			field.formula = "";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Category";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 44;
			field.numLines = 0;
			_CategoryInfo = new TextField(field);
			_fieldInfo.addItem(_CategoryInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.width = 15;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.required = false;
			field.doesDataCopy = true;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = true;
			field.fieldName = "BriefName";
			field.formula = "";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Brief Name";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 8;
			field.numLines = 0;
			_BriefNameInfo = new TextField(field);
			_fieldInfo.addItem(_BriefNameInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.width = 20;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.required = false;
			field.doesDataCopy = true;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = true;
			field.fieldName = "Country";
			field.formula = "";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Country";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 42;
			field.numLines = 0;
			_CountryInfo = new ChoiceField(field);
			_fieldInfo.addItem(_CountryInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.width = 10;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.required = false;
			field.doesDataCopy = true;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = true;
			field.fieldName = "PostalCode";
			field.formula = "";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Postal Code";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 13;
			field.numLines = 0;
			_PostalCodeInfo = new TextField(field);
			_fieldInfo.addItem(_PostalCodeInfo);

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
			field.fieldName = "PrimaryContact";
			field.formula = "";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Primary Contact";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 14;
			field.numLines = 0;
			_PrimaryContactInfo = new TextField(field);
			_fieldInfo.addItem(_PrimaryContactInfo);

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
			field.fieldName = "Position";
			field.formula = "";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Position";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 15;
			field.numLines = 0;
			_PositionInfo = new TextField(field);
			_fieldInfo.addItem(_PositionInfo);

			field = new FieldDescriptor();
			field.hasExtension = true;
			field.lusFid = 0;
			field.unique = false;
			field.fieldType = ENFieldType.Phone;
			field.required = false;
			field.doesDataCopy = true;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = true;
			field.fieldName = "Phone";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Phone";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 16;
			_PhoneInfo = new PhoneField(field);
			_fieldInfo.addItem(_PhoneInfo);

			field = new FieldDescriptor();
			field.hasExtension = true;
			field.lusFid = 0;
			field.unique = false;
			field.fieldType = ENFieldType.Phone;
			field.required = false;
			field.doesDataCopy = true;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = true;
			field.fieldName = "Cell";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Cell";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 17;
			_CellInfo = new PhoneField(field);
			_fieldInfo.addItem(_CellInfo);

			field = new FieldDescriptor();
			field.hasExtension = true;
			field.lusFid = 0;
			field.unique = false;
			field.fieldType = ENFieldType.Phone;
			field.required = false;
			field.doesDataCopy = true;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = true;
			field.fieldName = "Fax";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Fax";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 18;
			_FaxInfo = new PhoneField(field);
			_fieldInfo.addItem(_FaxInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.fieldType = ENFieldType.Email;
			field.required = false;
			field.doesDataCopy = true;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = true;
			field.fieldName = "EMail";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "EMail";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 19;
			_EMailInfo = new EmailField(field);
			_fieldInfo.addItem(_EMailInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.width = 80;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.required = false;
			field.doesDataCopy = true;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = true;
			field.fieldName = "AdditionalContacts";
			field.formula = "";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Additional Contacts";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 20;
			field.numLines = 3;
			_AdditionalContactsInfo = new TextField(field);
			_fieldInfo.addItem(_AdditionalContactsInfo);

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
			field.fieldName = "FiscalYearEnd";
			field.formula = "";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Fiscal Year End";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 43;
			field.numLines = 1;
			_FiscalYearEndInfo = new TextField(field);
			_fieldInfo.addItem(_FiscalYearEndInfo);

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
			field.fieldName = "BudgetDeadline";
			field.formula = "";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Budget Deadline";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 21;
			field.numLines = 1;
			_BudgetDeadlineInfo = new TextField(field);
			_fieldInfo.addItem(_BudgetDeadlineInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.width = 80;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.required = false;
			field.doesDataCopy = true;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = true;
			field.fieldName = "BudgetNotes";
			field.formula = "";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Budget Notes";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 22;
			field.numLines = 2;
			_BudgetNotesInfo = new TextField(field);
			_fieldInfo.addItem(_BudgetNotesInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.width = 80;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.required = false;
			field.doesDataCopy = true;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = true;
			field.fieldName = "ClientStandards";
			field.formula = "";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Client Standards";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 23;
			field.numLines = 2;
			_ClientStandardsInfo = new TextField(field);
			_fieldInfo.addItem(_ClientStandardsInfo);

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
			field.fieldName = "Notes";
			field.formula = "";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Notes";
			field.foreignKey = 0;
			field.allowHTML = false;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 24;
			field.numLines = 2;
			_NotesInfo = new TextField(field);
			_fieldInfo.addItem(_NotesInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.appearsAs = "";
			field.fieldType = ENFieldType.URL;
			field.required = false;
			field.doesDataCopy = false;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.Virtual;
			field.findEnabled = true;
			field.fieldName = "Test";
			field.formula = "";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Test";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 56;
			_TestInfo = new URLField(field);
			_fieldInfo.addItem(_TestInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.fieldType = ENFieldType.File;
			field.required = false;
			field.doesDataCopy = false;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.NotFound;
			field.findEnabled = true;
			field.fieldName = "Photo";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.maxVersions = 3;
			field.label = "Photo";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 67;
			_PhotoInfo = new FileField(field);
			_fieldInfo.addItem(_PhotoInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.width = 40;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.required = false;
			field.doesDataCopy = false;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.Virtual;
			field.findEnabled = true;
			field.fieldName = "PhotoThumbnail";
			field.formula = "If ([Photo]=\"\",\"\",\"<a href='https://www.quickbase.com/up/\" & Dbid() & \"/a/r\" & [Record ID#] & \"/e67/va/\" & [Photo] & \"' target=_blank>\"";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "Photo Thumbnail";
			field.foreignKey = 0;
			field.allowHTML = true;
			field.role = ENRole.NotFound;
			field.fieldHelp = "";
			field.fid = 68;
			field.numLines = 0;
			_PhotoThumbnailInfo = new TextField(field);
			_fieldInfo.addItem(_PhotoThumbnailInfo);

			field = new FieldDescriptor();
			field.lusFid = 0;
			field.unique = false;
			field.appearsAs = "Map";
			field.fieldType = ENFieldType.URL;
			field.required = false;
			field.doesDataCopy = false;
			field.carryChoices = true;
			field.lutFid = 0;
			field.mode = ENMode.Virtual;
			field.findEnabled = true;
			field.fieldName = "QBMap";
			field.formula = "\"http://www.quickbaseutilities.com/Maybee/QBMaps/default.aspx?\"";
			field.tableName = "Clients";
			field.baseType = ENBaseType.Text;
			field.allowNewChoices = false;
			field.label = "QB Map";
			field.foreignKey = 0;
			field.role = ENRole.NotFound;
			field.fieldHelp = "95";
			field.fid = 72;
			_QBMapInfo = new URLField(field);
			_fieldInfo.addItem(_QBMapInfo);

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
			field.tableName = "Clients";
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
			field.tableName = "Clients";
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
			field.tableName = "Clients";
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
			field.tableName = "Clients";
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

		public static function getInstance():Clients_Info
		{
			if(_instance == null)
				_instance = new Clients_Info(new Private);
			return _instance;
		}

		public function get tableName():String
		{
			return "Clients";
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
		private var _ClientNameInfo:TextField;
		private var _AddressInfo:TextField;
		private var _CityInfo:TextField;
		private var _FacilitiesCountInfo:NumberField;
		private var _SectionsCountInfo:NumberField;
		private var _TotalSqFtInfo:NumberField;
		private var _TotalValueInfo:CurrencyField;
		private var _ProvinceInfo:ChoiceField;
		private var _RelatedSegmentInfo:NumberField;
		private var _SegmentInfo:TextField;
		private var _AddDocumentInfo:URLField;
		private var _DocumentsInfo:DbLinkField;
		private var _AddFacilityInfo:URLField;
		private var _CategoryInfo:TextField;
		private var _BriefNameInfo:TextField;
		private var _CountryInfo:ChoiceField;
		private var _PostalCodeInfo:TextField;
		private var _PrimaryContactInfo:TextField;
		private var _PositionInfo:TextField;
		private var _PhoneInfo:PhoneField;
		private var _CellInfo:PhoneField;
		private var _FaxInfo:PhoneField;
		private var _EMailInfo:EmailField;
		private var _AdditionalContactsInfo:TextField;
		private var _FiscalYearEndInfo:TextField;
		private var _BudgetDeadlineInfo:TextField;
		private var _BudgetNotesInfo:TextField;
		private var _ClientStandardsInfo:TextField;
		private var _NotesInfo:TextField;
		private var _TestInfo:URLField;
		private var _PhotoInfo:FileField;
		private var _PhotoThumbnailInfo:TextField;
		private var _QBMapInfo:URLField;
		private var _DateCreatedInfo:TimeStampField;
		private var _DateModifiedInfo:TimeStampField;
		private var _RecordOwnerInfo:UserIdField;
		private var _LastModifiedByInfo:UserIdField;

		// MetaData Information Objects getters
		public function get ClientName_Info():TextField					{return _ClientNameInfo;}
		public function get Address_Info():TextField					{return _AddressInfo;}
		public function get City_Info():TextField						{return _CityInfo;}
		public function get FacilitiesCount_Info():NumberField			{return _FacilitiesCountInfo;}
		public function get SectionsCount_Info():NumberField			{return _SectionsCountInfo;}
		public function get TotalSqFt_Info():NumberField				{return _TotalSqFtInfo;}
		public function get TotalValue_Info():CurrencyField				{return _TotalValueInfo;}
		public function get Province_Info():ChoiceField					{return _ProvinceInfo;}
		public function get RelatedSegment_Info():NumberField			{return _RelatedSegmentInfo;}
		public function get Segment_Info():TextField					{return _SegmentInfo;}
		public function get AddDocument_Info():URLField					{return _AddDocumentInfo;}
		public function get Documents_Info():DbLinkField				{return _DocumentsInfo;}
		public function get AddFacility_Info():URLField					{return _AddFacilityInfo;}
		public function get Category_Info():TextField					{return _CategoryInfo;}
		public function get BriefName_Info():TextField					{return _BriefNameInfo;}
		public function get Country_Info():ChoiceField					{return _CountryInfo;}
		public function get PostalCode_Info():TextField					{return _PostalCodeInfo;}
		public function get PrimaryContact_Info():TextField				{return _PrimaryContactInfo;}
		public function get Position_Info():TextField					{return _PositionInfo;}
		public function get Phone_Info():PhoneField						{return _PhoneInfo;}
		public function get Cell_Info():PhoneField						{return _CellInfo;}
		public function get Fax_Info():PhoneField						{return _FaxInfo;}
		public function get EMail_Info():EmailField						{return _EMailInfo;}
		public function get AdditionalContacts_Info():TextField			{return _AdditionalContactsInfo;}
		public function get FiscalYearEnd_Info():TextField				{return _FiscalYearEndInfo;}
		public function get BudgetDeadline_Info():TextField				{return _BudgetDeadlineInfo;}
		public function get BudgetNotes_Info():TextField				{return _BudgetNotesInfo;}
		public function get ClientStandards_Info():TextField			{return _ClientStandardsInfo;}
		public function get Notes_Info():TextField						{return _NotesInfo;}
		public function get Test_Info():URLField						{return _TestInfo;}
		public function get Photo_Info():FileField						{return _PhotoInfo;}
		public function get PhotoThumbnail_Info():TextField				{return _PhotoThumbnailInfo;}
		public function get QBMap_Info():URLField						{return _QBMapInfo;}
		public function get DateCreated_Info():TimeStampField			{return _DateCreatedInfo;}
		public function get DateModified_Info():TimeStampField			{return _DateModifiedInfo;}
		public function get RecordOwner_Info():UserIdField				{return _RecordOwnerInfo;}
		public function get LastModifiedBy_Info():UserIdField			{return _LastModifiedByInfo;}

		// Field getter variables
		private var _fieldNames:ArrayCollection = new ArrayCollection(["ClientName", "Address", "City", "FacilitiesCount", 
																		"SectionsCount", "TotalSqFt", "TotalValue", "Province", "RelatedSegment", "Segment", "AddDocument", "Documents", 
																		"AddFacility", "Category", "BriefName", "Country", "PostalCode", "PrimaryContact", "Position", "Phone", "Cell", "Fax", 
																		"EMail", "AdditionalContacts", "FiscalYearEnd", "BudgetDeadline", "BudgetNotes", "ClientStandards", "Notes", 
																		"Test", "Photo", "PhotoThumbnail", "QBMap", "DateCreated", "DateModified", "RecordOwner", "LastModifiedBy", ]);
		private var _fieldInfo:ArrayCollection = new ArrayCollection();

		// Field getters
		public function get FieldNames():ArrayCollection				{return _fieldNames;}
		public function get FieldsInfo():ArrayCollection				{return _fieldInfo;}
	}
}

class Private{}
