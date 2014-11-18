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

	public class Expenditures_Info implements IKingussieInfo
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		private static var _instance:Expenditures_Info = null;

		function Expenditures_Info(forcePrivateClass:Private)
		{
			// MetaData Initializers
			var field:FieldDescriptor;

			field = new FieldDescriptor();
			field.fid = 13;
			field.lutFid = 30;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 12;
			field.label = "Client";
			field.allowNewChoices = false;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.fieldName = "Client";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_ClientInfo = new TextField(field);
			_fieldInfo.addItem(_ClientInfo);

			field = new FieldDescriptor();
			field.fid = 14;
			field.lutFid = 31;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 12;
			field.label = "Facility";
			field.allowNewChoices = false;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.fieldName = "Facility";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_FacilityInfo = new TextField(field);
			_fieldInfo.addItem(_FacilityInfo);

			field = new FieldDescriptor();
			field.fid = 15;
			field.lutFid = 6;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 12;
			field.label = "Roof";
			field.allowNewChoices = false;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.fieldName = "Roof";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_RoofInfo = new TextField(field);
			_fieldInfo.addItem(_RoofInfo);

			field = new FieldDescriptor();
			field.fid = 27;
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
			field.label = "ExpeditureDate";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.fieldName = "ExpeditureDate";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_ExpeditureDateInfo = new TextField(field);
			_fieldInfo.addItem(_ExpeditureDateInfo);

			field = new FieldDescriptor();
			field.fid = 34;
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
			field.label = "Type of Work";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.fieldName = "TypeOfWork";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_TypeOfWorkInfo = new ChoiceField(field);
			_fieldInfo.addItem(_TypeOfWorkInfo);

			field = new FieldDescriptor();
			field.fid = 35;
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
			field.label = "Status";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.fieldName = "Status";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_StatusInfo = new ChoiceField(field);
			_fieldInfo.addItem(_StatusInfo);

			field = new FieldDescriptor();
			field.fid = 32;
			field.unique = false;
			field.commaStart = 3;
			field.fieldHelp = "";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.baseType = ENBaseType.Float;
			field.tableName = "Expenditures";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.required = false;
			field.fieldName = "Amount";
			field.foreignKey = 0;
			field.currencyFormat = "1";
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.fieldType = ENFieldType.Currency;
			field.lusFid = 0;
			field.label = "Amount";
			field.units = "";
			field.decimalPlaces = 2;
			field.carryChoices = true;
			field.currencySymbol = "$";
			field.findEnabled = true;
			field.formula = "";
			_AmountInfo = new CurrencyField(field);
			_fieldInfo.addItem(_AmountInfo);

			field = new FieldDescriptor();
			field.fid = 12;
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
			field.tableName = "Expenditures";
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
			field.fid = 42;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.URL;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Add Documents";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.appearsAs = "Add  Expenditure Document";
			field.fieldName = "AddDocuments";
			field.required = false;
			field.formula = "URLRoot() & \"db/\" & [_DBID_EXPENDITURE_ATTACHMENTS] & \"?a=API_GenAddRecordForm&_fid_12=\" & URLEncode ([Record ID#])& \"&z=\" & Rurl()";
			field.foreignKey = 0;
			field.findEnabled = false;
			_AddDocumentsInfo = new URLField(field);
			_fieldInfo.addItem(_AddDocumentsInfo);

			field = new FieldDescriptor();
			field.fid = 41;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.sourceFID = 3;
			field.unique = false;
			field.fieldType = ENFieldType.DbLink;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.targetFID = 12;
			field.targetDBID = "be9nwdi2x";
			field.label = "Documents";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.coverText = "Expenditure Documents";
			field.baseType = ENBaseType.Text;
			field.tableName = "Expenditures";
			field.exact = true;
			field.role = ENRole.NotFound;
			field.fieldName = "Documents";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_DocumentsInfo = new DbLinkField(field);
			_fieldInfo.addItem(_DocumentsInfo);

			field = new FieldDescriptor();
			field.fid = 40;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Budget Year";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = 0;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Expenditures";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.fieldName = "BudgetYear";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_BudgetYearInfo = new NumberField(field);
			_fieldInfo.addItem(_BudgetYearInfo);

			field = new FieldDescriptor();
			field.fid = 43;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.Date;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Date Sort";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Int64;
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.fieldName = "DateSort";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_DateSortInfo = new DateField(field);
			_fieldInfo.addItem(_DateSortInfo);

			field = new FieldDescriptor();
			field.fid = 29;
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
			field.label = "Contractor";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.fieldName = "Contractor";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_ContractorInfo = new TextField(field);
			_fieldInfo.addItem(_ContractorInfo);

			field = new FieldDescriptor();
			field.fid = 33;
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
			field.label = "Action Item";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.fieldName = "ActionItem";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_ActionItemInfo = new TextField(field);
			_fieldInfo.addItem(_ActionItemInfo);

			field = new FieldDescriptor();
			field.fid = 39;
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
			field.label = "Allocation";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.fieldName = "Allocation";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_AllocationInfo = new ChoiceField(field);
			_fieldInfo.addItem(_AllocationInfo);

			field = new FieldDescriptor();
			field.fid = 36;
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
			field.label = "Urgency";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.fieldName = "Urgency";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_UrgencyInfo = new ChoiceField(field);
			_fieldInfo.addItem(_UrgencyInfo);

			field = new FieldDescriptor();
			field.fid = 31;
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
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.fieldName = "Description";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_DescriptionInfo = new TextField(field);
			_fieldInfo.addItem(_DescriptionInfo);

			field = new FieldDescriptor();
			field.fid = 16;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsCompleted";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.fieldName = "IsCompleted";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsCompletedInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsCompletedInfo);

			field = new FieldDescriptor();
			field.fid = 17;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.Date;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Completion Date";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Int64;
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.fieldName = "CompletionDate";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_CompletionDateInfo = new DateField(field);
			_fieldInfo.addItem(_CompletionDateInfo);

			field = new FieldDescriptor();
			field.fid = 18;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsInvoiced";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.fieldName = "IsInvoiced";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsInvoicedInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsInvoicedInfo);

			field = new FieldDescriptor();
			field.fid = 19;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.Date;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Invoice Date";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Int64;
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.fieldName = "InvoiceDate";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_InvoiceDateInfo = new DateField(field);
			_fieldInfo.addItem(_InvoiceDateInfo);

			field = new FieldDescriptor();
			field.fid = 20;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsApproved";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.fieldName = "IsApproved";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsApprovedInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsApprovedInfo);

			field = new FieldDescriptor();
			field.fid = 21;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.Date;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Approval Date";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Int64;
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.fieldName = "ApprovalDate";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_ApprovalDateInfo = new DateField(field);
			_fieldInfo.addItem(_ApprovalDateInfo);

			field = new FieldDescriptor();
			field.fid = 22;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsPaid";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.fieldName = "IsPaid";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsPaidInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsPaidInfo);

			field = new FieldDescriptor();
			field.fid = 23;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.Date;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Payment date";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Int64;
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.fieldName = "PaymentDate";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_PaymentDateInfo = new DateField(field);
			_fieldInfo.addItem(_PaymentDateInfo);

			field = new FieldDescriptor();
			field.fid = 28;
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
			field.label = "Init Date";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.fieldName = "InitDate";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_InitDateInfo = new TextField(field);
			_fieldInfo.addItem(_InitDateInfo);

			field = new FieldDescriptor();
			field.fid = 30;
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
			field.label = "Init by";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.fieldName = "InitBy";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_InitByInfo = new TextField(field);
			_fieldInfo.addItem(_InitByInfo);

			field = new FieldDescriptor();
			field.fid = 37;
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
			field.label = "Notes";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.fieldName = "Notes";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_NotesInfo = new TextField(field);
			_fieldInfo.addItem(_NotesInfo);

			field = new FieldDescriptor();
			field.fid = 44;
			field.lutFid = 78;
			field.doesDataCopy = false;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 12;
			field.label = "Allowed Client User";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = -1;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Expenditures";
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
			field.fid = 45;
			field.lutFid = 79;
			field.doesDataCopy = false;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 12;
			field.label = "Allowed Facility User";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = -1;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Expenditures";
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
			field.fid = 46;
			field.lutFid = 77;
			field.doesDataCopy = false;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 12;
			field.label = "Allowed Section User";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = -1;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Expenditures";
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
			field.fid = 48;
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
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.fieldName = "PhotoThumbnail";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_PhotoThumbnailInfo = new TextField(field);
			_fieldInfo.addItem(_PhotoThumbnailInfo);

			field = new FieldDescriptor();
			field.fid = 49;
			field.lutFid = 0;
			field.numLines = 1;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 0;
			field.label = "Test";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Expenditures";
			field.role = ENRole.NotFound;
			field.fieldName = "Test";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_TestInfo = new TextField(field);
			_fieldInfo.addItem(_TestInfo);

			field = new FieldDescriptor();
			field.fid = 50;
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
			field.tableName = "Expenditures";
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
			field.tableName = "Expenditures";
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
			field.tableName = "Expenditures";
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
			field.tableName = "Expenditures";
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
			field.tableName = "Expenditures";
			field.role = ENRole.Modifier;
			field.fieldName = "LastModifiedBy";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_LastModifiedByInfo = new UserIdField(field);
			_fieldInfo.addItem(_LastModifiedByInfo);

		}

		public static function getInstance():Expenditures_Info
		{
			if(_instance == null)
				_instance = new Expenditures_Info(new Private);
			return _instance;
		}

		public function get tableName():String
		{
			return "Expenditures";
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
		private var _ExpeditureDateInfo:TextField;
		private var _TypeOfWorkInfo:ChoiceField;
		private var _StatusInfo:ChoiceField;
		private var _AmountInfo:CurrencyField;
		private var _RelatedSectionInfo:NumberField;
		private var _AddDocumentsInfo:URLField;
		private var _DocumentsInfo:DbLinkField;
		private var _BudgetYearInfo:NumberField;
		private var _DateSortInfo:DateField;
		private var _ContractorInfo:TextField;
		private var _ActionItemInfo:TextField;
		private var _AllocationInfo:ChoiceField;
		private var _UrgencyInfo:ChoiceField;
		private var _DescriptionInfo:TextField;
		private var _IsCompletedInfo:BooleanField;
		private var _CompletionDateInfo:DateField;
		private var _IsInvoicedInfo:BooleanField;
		private var _InvoiceDateInfo:DateField;
		private var _IsApprovedInfo:BooleanField;
		private var _ApprovalDateInfo:DateField;
		private var _IsPaidInfo:BooleanField;
		private var _PaymentDateInfo:DateField;
		private var _InitDateInfo:TextField;
		private var _InitByInfo:TextField;
		private var _NotesInfo:TextField;
		private var _AllowedClientUserInfo:NumberField;
		private var _AllowedFacilityUserInfo:NumberField;
		private var _AllowedSectionUserInfo:NumberField;
		private var _PhotoThumbnailInfo:TextField;
		private var _TestInfo:TextField;
		private var _AllowedUserInfo:NumberField;
		private var _DateCreatedInfo:TimeStampField;
		private var _DateModifiedInfo:TimeStampField;
		private var _RecordOwnerInfo:UserIdField;
		private var _LastModifiedByInfo:UserIdField;

		// MetaData Information Objects getters
		public function get Client_Info():TextField						{return _ClientInfo;}
		public function get Facility_Info():TextField					{return _FacilityInfo;}
		public function get Roof_Info():TextField						{return _RoofInfo;}
		public function get ExpeditureDate_Info():TextField				{return _ExpeditureDateInfo;}
		public function get TypeOfWork_Info():ChoiceField				{return _TypeOfWorkInfo;}
		public function get Status_Info():ChoiceField					{return _StatusInfo;}
		public function get Amount_Info():CurrencyField					{return _AmountInfo;}
		public function get RelatedSection_Info():NumberField			{return _RelatedSectionInfo;}
		public function get AddDocuments_Info():URLField				{return _AddDocumentsInfo;}
		public function get Documents_Info():DbLinkField				{return _DocumentsInfo;}
		public function get BudgetYear_Info():NumberField				{return _BudgetYearInfo;}
		public function get DateSort_Info():DateField					{return _DateSortInfo;}
		public function get Contractor_Info():TextField					{return _ContractorInfo;}
		public function get ActionItem_Info():TextField					{return _ActionItemInfo;}
		public function get Allocation_Info():ChoiceField				{return _AllocationInfo;}
		public function get Urgency_Info():ChoiceField					{return _UrgencyInfo;}
		public function get Description_Info():TextField				{return _DescriptionInfo;}
		public function get IsCompleted_Info():BooleanField				{return _IsCompletedInfo;}
		public function get CompletionDate_Info():DateField				{return _CompletionDateInfo;}
		public function get IsInvoiced_Info():BooleanField				{return _IsInvoicedInfo;}
		public function get InvoiceDate_Info():DateField				{return _InvoiceDateInfo;}
		public function get IsApproved_Info():BooleanField				{return _IsApprovedInfo;}
		public function get ApprovalDate_Info():DateField				{return _ApprovalDateInfo;}
		public function get IsPaid_Info():BooleanField					{return _IsPaidInfo;}
		public function get PaymentDate_Info():DateField				{return _PaymentDateInfo;}
		public function get InitDate_Info():TextField					{return _InitDateInfo;}
		public function get InitBy_Info():TextField						{return _InitByInfo;}
		public function get Notes_Info():TextField						{return _NotesInfo;}
		public function get AllowedClientUser_Info():NumberField		{return _AllowedClientUserInfo;}
		public function get AllowedFacilityUser_Info():NumberField		{return _AllowedFacilityUserInfo;}
		public function get AllowedSectionUser_Info():NumberField		{return _AllowedSectionUserInfo;}
		public function get PhotoThumbnail_Info():TextField				{return _PhotoThumbnailInfo;}
		public function get Test_Info():TextField						{return _TestInfo;}
		public function get AllowedUser_Info():NumberField				{return _AllowedUserInfo;}
		public function get DateCreated_Info():TimeStampField			{return _DateCreatedInfo;}
		public function get DateModified_Info():TimeStampField			{return _DateModifiedInfo;}
		public function get RecordOwner_Info():UserIdField				{return _RecordOwnerInfo;}
		public function get LastModifiedBy_Info():UserIdField			{return _LastModifiedByInfo;}

		// Field getter variables
		private var _fieldNames:ArrayCollection = new ArrayCollection(["Client", "Facility", "Roof", "ExpeditureDate", "TypeOfWork", 
																		"Status", "Amount", "RelatedSection", "AddDocuments", "Documents", "BudgetYear", "DateSort", "Contractor", 
																		"ActionItem", "Allocation", "Urgency", "Description", "IsCompleted", "CompletionDate", "IsInvoiced", "InvoiceDate", 
																		"IsApproved", "ApprovalDate", "IsPaid", "PaymentDate", "InitDate", "InitBy", "Notes", "AllowedClientUser", 
																		"AllowedFacilityUser", "AllowedSectionUser", "PhotoThumbnail", "Test", "AllowedUser", "DateCreated", 
																		"DateModified", "RecordOwner", "LastModifiedBy", ]);
		private var _fieldInfo:ArrayCollection = new ArrayCollection();

		// Field getters
		public function get FieldNames():ArrayCollection				{return _fieldNames;}
		public function get FieldsInfo():ArrayCollection				{return _fieldInfo;}
	}
}

class Private{}
