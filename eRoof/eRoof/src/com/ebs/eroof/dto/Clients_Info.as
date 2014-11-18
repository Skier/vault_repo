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
			field.fid = 7;
			field.lutFid = 0;
			field.numLines = 0;
			field.doesDataCopy = true;
			field.unique = true;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 0;
			field.label = "Client Name";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.fieldName = "ClientName";
			field.required = true;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_ClientNameInfo = new TextField(field);
			_fieldInfo.addItem(_ClientNameInfo);

			field = new FieldDescriptor();
			field.fid = 25;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.URL;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Facilities";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.appearsAs = "Facilities";
			field.fieldName = "Facilities";
			field.required = false;
			field.formula = "\"https://workplace.intuit.com/db/be9nwdiyu?a=q&qt=tab&dvqid=1&clist=30.6.9.11.33.34.35.31&slist=30.6&query={Related Client.ex.'\" & [Record ID#] & \"'}&opts=so-AA.gb-XX.\"";
			field.foreignKey = 0;
			field.findEnabled = true;
			_FacilitiesInfo = new URLField(field);
			_fieldInfo.addItem(_FacilitiesInfo);

			field = new FieldDescriptor();
			field.fid = 9;
			field.lutFid = 0;
			field.numLines = 3;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 0;
			field.label = "Address";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.fieldName = "Address";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_AddressInfo = new TextField(field);
			_fieldInfo.addItem(_AddressInfo);

			field = new FieldDescriptor();
			field.fid = 11;
			field.lutFid = 0;
			field.numLines = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 20;
			field.lusFid = 0;
			field.label = "City";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.fieldName = "City";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_CityInfo = new TextField(field);
			_fieldInfo.addItem(_CityInfo);

			field = new FieldDescriptor();
			field.fid = 30;
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
			field.tableName = "Clients";
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
			field.fid = 31;
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
			field.tableName = "Clients";
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
			field.fid = 32;
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
			field.tableName = "Clients";
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
			field.fid = 33;
			field.unique = false;
			field.commaStart = 3;
			field.fieldHelp = "";
			field.allowNewChoices = false;
			field.mode = ENMode.Summary;
			field.baseType = ENBaseType.Float;
			field.tableName = "Clients";
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
			field.fid = 12;
			field.lutFid = 0;
			field.numLines = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 20;
			field.lusFid = 0;
			field.label = "Province";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.fieldName = "Province";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_ProvinceInfo = new ChoiceField(field);
			_fieldInfo.addItem(_ProvinceInfo);

			field = new FieldDescriptor();
			field.fid = 39;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.commaStart = 0;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Related Segment";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = 0;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Clients";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.fieldName = "RelatedSegment";
			field.required = true;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_RelatedSegmentInfo = new NumberField(field);
			_fieldInfo.addItem(_RelatedSegmentInfo);

			field = new FieldDescriptor();
			field.fid = 40;
			field.lutFid = 6;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 39;
			field.label = "Segment";
			field.allowNewChoices = false;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.fieldName = "Segment";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_SegmentInfo = new TextField(field);
			_fieldInfo.addItem(_SegmentInfo);

			field = new FieldDescriptor();
			field.fid = 50;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.URL;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Add Document";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.appearsAs = "Add Document";
			field.fieldName = "AddDocument";
			field.required = false;
			field.formula = "URLRoot() & \"db/\" & [_DBID_CLIENT_ATTACHMENTS] & \"?a=API_GenAddRecordForm&_fid_12=\" & URLEncode ([Record ID#])& \"&z=\" & Rurl()";
			field.foreignKey = 0;
			field.findEnabled = false;
			_AddDocumentInfo = new URLField(field);
			_fieldInfo.addItem(_AddDocumentInfo);

			field = new FieldDescriptor();
			field.fid = 49;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.sourceFID = 3;
			field.unique = false;
			field.fieldType = ENFieldType.DbLink;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.targetFID = 12;
			field.targetDBID = "be9nwdizc";
			field.label = "Documents";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.coverText = "Client Documents";
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.exact = true;
			field.role = ENRole.NotFound;
			field.fieldName = "Documents";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_DocumentsInfo = new DbLinkField(field);
			_fieldInfo.addItem(_DocumentsInfo);

			field = new FieldDescriptor();
			field.fid = 26;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.URL;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Add Facility";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.appearsAs = "Add  Facility";
			field.fieldName = "AddFacility";
			field.required = false;
			field.formula = "URLRoot() & \"db/\" & [_DBID_FACILITIES] & \"?a=API_GenAddRecordForm&_fid_29=\" & URLEncode ([Record ID#])& \"&z=\" & Rurl()";
			field.foreignKey = 0;
			field.findEnabled = false;
			_AddFacilityInfo = new URLField(field);
			_fieldInfo.addItem(_AddFacilityInfo);

			field = new FieldDescriptor();
			field.fid = 44;
			field.lutFid = 0;
			field.numLines = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 20;
			field.lusFid = 0;
			field.label = "Category";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.fieldName = "Category";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_CategoryInfo = new TextField(field);
			_fieldInfo.addItem(_CategoryInfo);

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
			field.width = 15;
			field.lusFid = 0;
			field.label = "Brief Name";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.fieldName = "BriefName";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_BriefNameInfo = new TextField(field);
			_fieldInfo.addItem(_BriefNameInfo);

			field = new FieldDescriptor();
			field.fid = 42;
			field.lutFid = 0;
			field.numLines = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 20;
			field.lusFid = 0;
			field.label = "Country";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.fieldName = "Country";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_CountryInfo = new ChoiceField(field);
			_fieldInfo.addItem(_CountryInfo);

			field = new FieldDescriptor();
			field.fid = 13;
			field.lutFid = 0;
			field.numLines = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 10;
			field.lusFid = 0;
			field.label = "Postal Code";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.fieldName = "PostalCode";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_PostalCodeInfo = new TextField(field);
			_fieldInfo.addItem(_PostalCodeInfo);

			field = new FieldDescriptor();
			field.fid = 14;
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
			field.label = "Primary Contact";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.fieldName = "PrimaryContact";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_PrimaryContactInfo = new TextField(field);
			_fieldInfo.addItem(_PrimaryContactInfo);

			field = new FieldDescriptor();
			field.fid = 15;
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
			field.label = "Position";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.fieldName = "Position";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_PositionInfo = new TextField(field);
			_fieldInfo.addItem(_PositionInfo);

			field = new FieldDescriptor();
			field.fid = 16;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.fieldType = ENFieldType.Phone;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Phone";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.hasExtension = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.fieldName = "Phone";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_PhoneInfo = new PhoneField(field);
			_fieldInfo.addItem(_PhoneInfo);

			field = new FieldDescriptor();
			field.fid = 17;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.fieldType = ENFieldType.Phone;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Cell";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.hasExtension = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.fieldName = "Cell";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_CellInfo = new PhoneField(field);
			_fieldInfo.addItem(_CellInfo);

			field = new FieldDescriptor();
			field.fid = 18;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.fieldType = ENFieldType.Phone;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Fax";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.hasExtension = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.fieldName = "Fax";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_FaxInfo = new PhoneField(field);
			_fieldInfo.addItem(_FaxInfo);

			field = new FieldDescriptor();
			field.fid = 19;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.Email;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "EMail";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.fieldName = "EMail";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_EMailInfo = new EmailField(field);
			_fieldInfo.addItem(_EMailInfo);

			field = new FieldDescriptor();
			field.fid = 20;
			field.lutFid = 0;
			field.numLines = 3;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 80;
			field.lusFid = 0;
			field.label = "Additional Contacts";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.fieldName = "AdditionalContacts";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_AdditionalContactsInfo = new TextField(field);
			_fieldInfo.addItem(_AdditionalContactsInfo);

			field = new FieldDescriptor();
			field.fid = 43;
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
			field.label = "Fiscal Year End";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.fieldName = "FiscalYearEnd";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_FiscalYearEndInfo = new TextField(field);
			_fieldInfo.addItem(_FiscalYearEndInfo);

			field = new FieldDescriptor();
			field.fid = 21;
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
			field.label = "Budget Deadline";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.fieldName = "BudgetDeadline";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_BudgetDeadlineInfo = new TextField(field);
			_fieldInfo.addItem(_BudgetDeadlineInfo);

			field = new FieldDescriptor();
			field.fid = 22;
			field.lutFid = 0;
			field.numLines = 2;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 80;
			field.lusFid = 0;
			field.label = "Budget Notes";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.fieldName = "BudgetNotes";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_BudgetNotesInfo = new TextField(field);
			_fieldInfo.addItem(_BudgetNotesInfo);

			field = new FieldDescriptor();
			field.fid = 23;
			field.lutFid = 0;
			field.numLines = 2;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 80;
			field.lusFid = 0;
			field.label = "Client Standards";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.fieldName = "ClientStandards";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_ClientStandardsInfo = new TextField(field);
			_fieldInfo.addItem(_ClientStandardsInfo);

			field = new FieldDescriptor();
			field.fid = 24;
			field.lutFid = 0;
			field.numLines = 2;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 0;
			field.label = "Notes";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.fieldName = "Notes";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_NotesInfo = new TextField(field);
			_fieldInfo.addItem(_NotesInfo);

			field = new FieldDescriptor();
			field.fid = 51;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.sourceFID = 3;
			field.unique = false;
			field.fieldType = ENFieldType.DbLink;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.targetFID = 6;
			field.targetDBID = "be9nwdi3c";
			field.label = "Client Users";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.coverText = "Client Users";
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.exact = true;
			field.role = ENRole.NotFound;
			field.fieldName = "ClientUsers";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_ClientUsersInfo = new DbLinkField(field);
			_fieldInfo.addItem(_ClientUsersInfo);

			field = new FieldDescriptor();
			field.fid = 52;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.URL;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Add Client User";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.appearsAs = "Add Client User";
			field.fieldName = "AddClientUser";
			field.required = false;
			field.formula = "URLRoot() & \"db/\" & [_DBID_CLIENTUSERS] & \"?a=API_GenAddRecordForm&_fid_6=\" & URLEncode ([Record ID#])& \"&z=\" & Rurl()";
			field.foreignKey = 0;
			field.findEnabled = false;
			_AddClientUserInfo = new URLField(field);
			_fieldInfo.addItem(_AddClientUserInfo);

			field = new FieldDescriptor();
			field.fid = 53;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Allowed Client User";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = -1;
			field.mode = ENMode.Summary;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Clients";
			field.blankIsZero = false;
			field.role = ENRole.NotFound;
			field.fieldName = "AllowedClientUser";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = false;
			_AllowedClientUserInfo = new NumberField(field);
			_fieldInfo.addItem(_AllowedClientUserInfo);

			field = new FieldDescriptor();
			field.fid = 55;
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
			field.tableName = "Clients";
			field.blankIsZero = false;
			field.role = ENRole.NotFound;
			field.fieldName = "AllowedUser";
			field.required = false;
			field.formula = "[Allowed Client User]";
			field.foreignKey = 0;
			field.findEnabled = true;
			_AllowedUserInfo = new NumberField(field);
			_fieldInfo.addItem(_AllowedUserInfo);

			field = new FieldDescriptor();
			field.fid = 56;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.URL;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Test";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.appearsAs = "";
			field.fieldName = "Test";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_TestInfo = new URLField(field);
			_fieldInfo.addItem(_TestInfo);

			field = new FieldDescriptor();
			field.fid = 67;
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
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.fieldName = "Photo";
			field.required = false;
			field.maxVersions = 3;
			field.foreignKey = 0;
			field.findEnabled = true;
			_PhotoInfo = new FileField(field);
			_fieldInfo.addItem(_PhotoInfo);

			field = new FieldDescriptor();
			field.fid = 68;
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
			field.label = "Photo Thumbnail";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.fieldName = "PhotoThumbnail";
			field.required = false;
			field.formula = "If ([Photo]=\"\",\"\",\"<a href='https://www.quickbase.com/up/\" & Dbid() & \"/a/r\" & [Record ID#] & \"/e67/va/\" & [Photo] & \"' target=_blank>\"";
			field.foreignKey = 0;
			field.findEnabled = true;
			_PhotoThumbnailInfo = new TextField(field);
			_fieldInfo.addItem(_PhotoThumbnailInfo);

			field = new FieldDescriptor();
			field.fid = 72;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.URL;
			field.fieldHelp = "95";
			field.lusFid = 0;
			field.label = "QB Map";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.appearsAs = "Map";
			field.fieldName = "QBMap";
			field.required = false;
			field.formula = "\"http://www.quickbaseutilities.com/Maybee/QBMaps/default.aspx?\"";
			field.foreignKey = 0;
			field.findEnabled = true;
			_QBMapInfo = new URLField(field);
			_fieldInfo.addItem(_QBMapInfo);

			field = new FieldDescriptor();
			field.fid = 76;
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
			field.tableName = "Clients";
			field.role = ENRole.NotFound;
			field.fieldName = "MapLatLong";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_MapLatLongInfo = new TextField(field);
			_fieldInfo.addItem(_MapLatLongInfo);

			field = new FieldDescriptor();
			field.fid = 77;
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
			field.tableName = "Clients";
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
			field.tableName = "Clients";
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
			field.tableName = "Clients";
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
			field.tableName = "Clients";
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
			field.tableName = "Clients";
			field.role = ENRole.Modifier;
			field.fieldName = "LastModifiedBy";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
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
		private var _FacilitiesInfo:URLField;
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
		private var _ClientUsersInfo:DbLinkField;
		private var _AddClientUserInfo:URLField;
		private var _AllowedClientUserInfo:NumberField;
		private var _AllowedUserInfo:NumberField;
		private var _TestInfo:URLField;
		private var _PhotoInfo:FileField;
		private var _PhotoThumbnailInfo:TextField;
		private var _QBMapInfo:URLField;
		private var _MapLatLongInfo:TextField;
		private var _MapZoomInfo:NumberField;
		private var _DateCreatedInfo:TimeStampField;
		private var _DateModifiedInfo:TimeStampField;
		private var _RecordOwnerInfo:UserIdField;
		private var _LastModifiedByInfo:UserIdField;

		// MetaData Information Objects getters
		public function get ClientName_Info():TextField					{return _ClientNameInfo;}
		public function get Facilities_Info():URLField					{return _FacilitiesInfo;}
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
		public function get ClientUsers_Info():DbLinkField				{return _ClientUsersInfo;}
		public function get AddClientUser_Info():URLField				{return _AddClientUserInfo;}
		public function get AllowedClientUser_Info():NumberField		{return _AllowedClientUserInfo;}
		public function get AllowedUser_Info():NumberField				{return _AllowedUserInfo;}
		public function get Test_Info():URLField						{return _TestInfo;}
		public function get Photo_Info():FileField						{return _PhotoInfo;}
		public function get PhotoThumbnail_Info():TextField				{return _PhotoThumbnailInfo;}
		public function get QBMap_Info():URLField						{return _QBMapInfo;}
		public function get MapLatLong_Info():TextField					{return _MapLatLongInfo;}
		public function get MapZoom_Info():NumberField					{return _MapZoomInfo;}
		public function get DateCreated_Info():TimeStampField			{return _DateCreatedInfo;}
		public function get DateModified_Info():TimeStampField			{return _DateModifiedInfo;}
		public function get RecordOwner_Info():UserIdField				{return _RecordOwnerInfo;}
		public function get LastModifiedBy_Info():UserIdField			{return _LastModifiedByInfo;}

		// Field getter variables
		private var _fieldNames:ArrayCollection = new ArrayCollection(["ClientName", "Facilities", "Address", "City", 
																		"FacilitiesCount", "SectionsCount", "TotalSqFt", "TotalValue", "Province", "RelatedSegment", "Segment", "AddDocument", 
																		"Documents", "AddFacility", "Category", "BriefName", "Country", "PostalCode", "PrimaryContact", "Position", "Phone", 
																		"Cell", "Fax", "EMail", "AdditionalContacts", "FiscalYearEnd", "BudgetDeadline", "BudgetNotes", 
																		"ClientStandards", "Notes", "ClientUsers", "AddClientUser", "AllowedClientUser", "AllowedUser", "Test", "Photo", 
																		"PhotoThumbnail", "QBMap", "MapLatLong", "MapZoom", "DateCreated", "DateModified", "RecordOwner", "LastModifiedBy", ]);
		private var _fieldInfo:ArrayCollection = new ArrayCollection();

		// Field getters
		public function get FieldNames():ArrayCollection				{return _fieldNames;}
		public function get FieldsInfo():ArrayCollection				{return _fieldInfo;}
	}
}

class Private{}
