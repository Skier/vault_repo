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

	public class Fields_Info implements IKingussieInfo
	{
		// Important Note:
		//    This class was automatically generated.  If you make changes to it and
		//    subsequently run the generator tool again, all changes will be overwritten!

		private static var _instance:Fields_Info = null;

		function Fields_Info(forcePrivateClass:Private)
		{
			// MetaData Initializers
			var field:FieldDescriptor;

			field = new FieldDescriptor();
			field.fid = 6;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.commaStart = 0;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Related Table";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = 0;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Fields";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.fieldName = "RelatedTable";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_RelatedTableInfo = new NumberField(field);
			_fieldInfo.addItem(_RelatedTableInfo);

			field = new FieldDescriptor();
			field.fid = 7;
			field.lutFid = 7;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 6;
			field.label = "Table";
			field.allowNewChoices = false;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "Table";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_TableInfo = new TextField(field);
			_fieldInfo.addItem(_TableInfo);

			field = new FieldDescriptor();
			field.fid = 39;
			field.lutFid = 10;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 6;
			field.label = "Seq and Name";
			field.allowNewChoices = false;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "SeqAndName";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_SeqAndNameInfo = new TextField(field);
			_fieldInfo.addItem(_SeqAndNameInfo);

			field = new FieldDescriptor();
			field.fid = 45;
			field.lutFid = 11;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 6;
			field.label = "DBID";
			field.allowNewChoices = false;
			field.mode = ENMode.Lookup;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "DBID";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_DBIDInfo = new TextField(field);
			_fieldInfo.addItem(_DBIDInfo);

			field = new FieldDescriptor();
			field.fid = 40;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Seq";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = -1;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Fields";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.fieldName = "Seq";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_SeqInfo = new NumberField(field);
			_fieldInfo.addItem(_SeqInfo);

			field = new FieldDescriptor();
			field.fid = 54;
			field.lutFid = 0;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 0;
			field.label = "Seq2";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "Seq2";
			field.required = false;
			field.formula = "If([Col#]>0,\"C\" & If([Col#]<10,\"0\",\"\") & ToText([Col#]), \"S\" & If([Seq]<10,\"0\",\"\") & ToText([Seq]))";
			field.foreignKey = 0;
			field.findEnabled = true;
			_Seq2Info = new TextField(field);
			_fieldInfo.addItem(_Seq2Info);

			field = new FieldDescriptor();
			field.fid = 38;
			field.lutFid = 0;
			field.numLines = 1;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 25;
			field.lusFid = 0;
			field.label = "Label";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "Label";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_LabelInfo = new TextField(field);
			_fieldInfo.addItem(_LabelInfo);

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
			field.width = 10;
			field.lusFid = 0;
			field.label = "Type";
			field.allowNewChoices = true;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "Type";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_TypeInfo = new ChoiceField(field);
			_fieldInfo.addItem(_TypeInfo);

			field = new FieldDescriptor();
			field.fid = 46;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "FieldID";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = -1;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Fields";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.fieldName = "FieldID";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_FieldIDInfo = new NumberField(field);
			_fieldInfo.addItem(_FieldIDInfo);

			field = new FieldDescriptor();
			field.fid = 44;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.URL;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Set";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.appearsAs = "Set";
			field.fieldName = "Set";
			field.required = false;
			field.formula = "URLRoot() & \"db/\" & [DBID] & \"?act=API_SetFieldProperties&username=jmaybee@eBusiness-Solutions.ca&password=TessTess&fid=\" & [FieldID] &";
			field.foreignKey = 0;
			field.findEnabled = true;
			_SetInfo = new URLField(field);
			_fieldInfo.addItem(_SetInfo);

			field = new FieldDescriptor();
			field.fid = 9;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsReqd";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "IsReqd";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsReqdInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsReqdInfo);

			field = new FieldDescriptor();
			field.fid = 10;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsWrap";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "IsWrap";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsWrapInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsWrapInfo);

			field = new FieldDescriptor();
			field.fid = 11;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Col#";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = -1;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Fields";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.fieldName = "Col";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_ColInfo = new NumberField(field);
			_fieldInfo.addItem(_ColInfo);

			field = new FieldDescriptor();
			field.fid = 12;
			field.lutFid = 0;
			field.numLines = 1;
			field.doesDataCopy = true;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 20;
			field.lusFid = 0;
			field.label = "Default";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "Default";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_DefaultInfo = new TextField(field);
			_fieldInfo.addItem(_DefaultInfo);

			field = new FieldDescriptor();
			field.fid = 13;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsUnique";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "IsUnique";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsUniqueInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsUniqueInfo);

			field = new FieldDescriptor();
			field.fid = 14;
			field.lutFid = 0;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 0;
			field.label = "T:";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "T";
			field.required = false;
			field.formula = "\"T:\"";
			field.foreignKey = 0;
			field.findEnabled = true;
			_TInfo = new ChoiceField(field);
			_fieldInfo.addItem(_TInfo);

			field = new FieldDescriptor();
			field.fid = 15;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Lines";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = -1;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Fields";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.fieldName = "Lines";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_LinesInfo = new NumberField(field);
			_fieldInfo.addItem(_LinesInfo);

			field = new FieldDescriptor();
			field.fid = 16;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Len";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = -1;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Fields";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.fieldName = "Len";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_LenInfo = new NumberField(field);
			_fieldInfo.addItem(_LenInfo);

			field = new FieldDescriptor();
			field.fid = 17;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsApp";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "IsApp";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsAppInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsAppInfo);

			field = new FieldDescriptor();
			field.fid = 18;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Txt Width";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = -1;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Fields";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.fieldName = "TxtWidth";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_TxtWidthInfo = new NumberField(field);
			_fieldInfo.addItem(_TxtWidthInfo);

			field = new FieldDescriptor();
			field.fid = 19;
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
			field.label = "Entry";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "Entry";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_EntryInfo = new TextField(field);
			_fieldInfo.addItem(_EntryInfo);

			field = new FieldDescriptor();
			field.fid = 20;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsMult";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "IsMult";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsMultInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsMultInfo);

			field = new FieldDescriptor();
			field.fid = 21;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsNew";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "IsNew";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsNewInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsNewInfo);

			field = new FieldDescriptor();
			field.fid = 22;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsSort";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "IsSort";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsSortInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsSortInfo);

			field = new FieldDescriptor();
			field.fid = 23;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsHTML";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "IsHTML";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsHTMLInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsHTMLInfo);

			field = new FieldDescriptor();
			field.fid = 24;
			field.lutFid = 0;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 0;
			field.label = "N:";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "N";
			field.required = false;
			field.formula = "\"N:\"";
			field.foreignKey = 0;
			field.findEnabled = true;
			_NInfo = new ChoiceField(field);
			_fieldInfo.addItem(_NInfo);

			field = new FieldDescriptor();
			field.fid = 25;
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
			field.label = "Units";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "Units";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_UnitsInfo = new TextField(field);
			_fieldInfo.addItem(_UnitsInfo);

			field = new FieldDescriptor();
			field.fid = 26;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Num Width";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = -1;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Fields";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.fieldName = "NumWidth";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_NumWidthInfo = new NumberField(field);
			_fieldInfo.addItem(_NumWidthInfo);

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
			field.label = "Treat As";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "TreatAs";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_TreatAsInfo = new TextField(field);
			_fieldInfo.addItem(_TreatAsInfo);

			field = new FieldDescriptor();
			field.fid = 28;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Dec";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = -1;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Fields";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.fieldName = "Dec";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_DecInfo = new NumberField(field);
			_fieldInfo.addItem(_DecInfo);

			field = new FieldDescriptor();
			field.fid = 29;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsTotals";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "IsTotals";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsTotalsInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsTotalsInfo);

			field = new FieldDescriptor();
			field.fid = 30;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsAvg";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "IsAvg";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsAvgInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsAvgInfo);

			field = new FieldDescriptor();
			field.fid = 31;
			field.lutFid = 0;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 0;
			field.label = "D:";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "D";
			field.required = false;
			field.formula = "\"D:\"";
			field.foreignKey = 0;
			field.findEnabled = true;
			_DInfo = new ChoiceField(field);
			_fieldInfo.addItem(_DInfo);

			field = new FieldDescriptor();
			field.fid = 32;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsAlpha";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "IsAlpha";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsAlphaInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsAlphaInfo);

			field = new FieldDescriptor();
			field.fid = 33;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsSmartDate";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "IsSmartDate";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsSmartDateInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsSmartDateInfo);

			field = new FieldDescriptor();
			field.fid = 34;
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
			field.label = "Date Type";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "DateType";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_DateTypeInfo = new TextField(field);
			_fieldInfo.addItem(_DateTypeInfo);

			field = new FieldDescriptor();
			field.fid = 35;
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
			field.label = "Text";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "Text";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_TextInfo = new TextField(field);
			_fieldInfo.addItem(_TextInfo);

			field = new FieldDescriptor();
			field.fid = 36;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsJPG";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "IsJPG";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsJPGInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsJPGInfo);

			field = new FieldDescriptor();
			field.fid = 37;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.commaStart = 4;
			field.fieldType = ENFieldType.Float;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "Revs";
			field.allowNewChoices = false;
			field.units = "";
			field.decimalPlaces = -1;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Float;
			field.tableName = "Fields";
			field.blankIsZero = true;
			field.role = ENRole.NotFound;
			field.fieldName = "Revs";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_RevsInfo = new NumberField(field);
			_fieldInfo.addItem(_RevsInfo);

			field = new FieldDescriptor();
			field.fid = 47;
			field.lutFid = 0;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 0;
			field.label = "test";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "test";
			field.required = false;
			field.formula = "[IsReqd]";
			field.foreignKey = 0;
			field.findEnabled = true;
			_testInfo = new TextField(field);
			_fieldInfo.addItem(_testInfo);

			field = new FieldDescriptor();
			field.fid = 48;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsUpdateable";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "IsUpdateable";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsUpdateableInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsUpdateableInfo);

			field = new FieldDescriptor();
			field.fid = 49;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsText";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "IsText";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsTextInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsTextInfo);

			field = new FieldDescriptor();
			field.fid = 50;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsNumeric";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "IsNumeric";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsNumericInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsNumericInfo);

			field = new FieldDescriptor();
			field.fid = 51;
			field.lutFid = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsDate";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "IsDate";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsDateInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsDateInfo);

			field = new FieldDescriptor();
			field.fid = 55;
			field.lutFid = 0;
			field.numLines = 0;
			field.doesDataCopy = false;
			field.unique = false;
			field.allowHTML = false;
			field.appendOnly = false;
			field.fieldType = ENFieldType.Text;
			field.fieldHelp = "";
			field.width = 40;
			field.lusFid = 0;
			field.label = "Eqn";
			field.allowNewChoices = false;
			field.mode = ENMode.Virtual;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "Eqn";
			field.required = false;
			field.formula = "URLRoot() & \"db/\" & [DBID] & \"?act=API_SetFieldProperties&username=jmaybee@eBusiness-Solutions.ca&password=TessTess&fid=\" & [FieldID] &";
			field.foreignKey = 0;
			field.findEnabled = true;
			_EqnInfo = new TextField(field);
			_fieldInfo.addItem(_EqnInfo);

			field = new FieldDescriptor();
			field.fid = 62;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsConsultant";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "IsConsultant";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsConsultantInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsConsultantInfo);

			field = new FieldDescriptor();
			field.fid = 63;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsClient";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "IsClient";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsClientInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsClientInfo);

			field = new FieldDescriptor();
			field.fid = 64;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsContractor";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "IsContractor";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsContractorInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsContractorInfo);

			field = new FieldDescriptor();
			field.fid = 65;
			field.lutFid = 0;
			field.doesDataCopy = true;
			field.unique = false;
			field.fieldType = ENFieldType.CheckBox;
			field.fieldHelp = "";
			field.lusFid = 0;
			field.label = "IsInspector";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Boolean;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "IsInspector";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_IsInspectorInfo = new BooleanField(field);
			_fieldInfo.addItem(_IsInspectorInfo);

			field = new FieldDescriptor();
			field.fid = 66;
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
			field.label = "IRPSource";
			field.allowNewChoices = false;
			field.mode = ENMode.NotFound;
			field.carryChoices = true;
			field.baseType = ENBaseType.Text;
			field.tableName = "Fields";
			field.role = ENRole.NotFound;
			field.fieldName = "IRPSource";
			field.required = false;
			field.formula = "";
			field.foreignKey = 0;
			field.findEnabled = true;
			_IRPSourceInfo = new TextField(field);
			_fieldInfo.addItem(_IRPSourceInfo);

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
			field.tableName = "Fields";
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
			field.tableName = "Fields";
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
			field.tableName = "Fields";
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
			field.tableName = "Fields";
			field.role = ENRole.Modifier;
			field.fieldName = "LastModifiedBy";
			field.required = false;
			field.foreignKey = 0;
			field.findEnabled = true;
			_LastModifiedByInfo = new UserIdField(field);
			_fieldInfo.addItem(_LastModifiedByInfo);

		}

		public static function getInstance():Fields_Info
		{
			if(_instance == null)
				_instance = new Fields_Info(new Private);
			return _instance;
		}

		public function get tableName():String
		{
			return "Fields";
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
		private var _RelatedTableInfo:NumberField;
		private var _TableInfo:TextField;
		private var _SeqAndNameInfo:TextField;
		private var _DBIDInfo:TextField;
		private var _SeqInfo:NumberField;
		private var _Seq2Info:TextField;
		private var _LabelInfo:TextField;
		private var _TypeInfo:ChoiceField;
		private var _FieldIDInfo:NumberField;
		private var _SetInfo:URLField;
		private var _IsReqdInfo:BooleanField;
		private var _IsWrapInfo:BooleanField;
		private var _ColInfo:NumberField;
		private var _DefaultInfo:TextField;
		private var _IsUniqueInfo:BooleanField;
		private var _TInfo:ChoiceField;
		private var _LinesInfo:NumberField;
		private var _LenInfo:NumberField;
		private var _IsAppInfo:BooleanField;
		private var _TxtWidthInfo:NumberField;
		private var _EntryInfo:TextField;
		private var _IsMultInfo:BooleanField;
		private var _IsNewInfo:BooleanField;
		private var _IsSortInfo:BooleanField;
		private var _IsHTMLInfo:BooleanField;
		private var _NInfo:ChoiceField;
		private var _UnitsInfo:TextField;
		private var _NumWidthInfo:NumberField;
		private var _TreatAsInfo:TextField;
		private var _DecInfo:NumberField;
		private var _IsTotalsInfo:BooleanField;
		private var _IsAvgInfo:BooleanField;
		private var _DInfo:ChoiceField;
		private var _IsAlphaInfo:BooleanField;
		private var _IsSmartDateInfo:BooleanField;
		private var _DateTypeInfo:TextField;
		private var _TextInfo:TextField;
		private var _IsJPGInfo:BooleanField;
		private var _RevsInfo:NumberField;
		private var _testInfo:TextField;
		private var _IsUpdateableInfo:BooleanField;
		private var _IsTextInfo:BooleanField;
		private var _IsNumericInfo:BooleanField;
		private var _IsDateInfo:BooleanField;
		private var _EqnInfo:TextField;
		private var _IsConsultantInfo:BooleanField;
		private var _IsClientInfo:BooleanField;
		private var _IsContractorInfo:BooleanField;
		private var _IsInspectorInfo:BooleanField;
		private var _IRPSourceInfo:TextField;
		private var _DateCreatedInfo:TimeStampField;
		private var _DateModifiedInfo:TimeStampField;
		private var _RecordOwnerInfo:UserIdField;
		private var _LastModifiedByInfo:UserIdField;

		// MetaData Information Objects getters
		public function get RelatedTable_Info():NumberField				{return _RelatedTableInfo;}
		public function get Table_Info():TextField						{return _TableInfo;}
		public function get SeqAndName_Info():TextField					{return _SeqAndNameInfo;}
		public function get DBID_Info():TextField						{return _DBIDInfo;}
		public function get Seq_Info():NumberField						{return _SeqInfo;}
		public function get Seq2_Info():TextField						{return _Seq2Info;}
		public function get Label_Info():TextField						{return _LabelInfo;}
		public function get Type_Info():ChoiceField						{return _TypeInfo;}
		public function get FieldID_Info():NumberField					{return _FieldIDInfo;}
		public function get Set_Info():URLField							{return _SetInfo;}
		public function get IsReqd_Info():BooleanField					{return _IsReqdInfo;}
		public function get IsWrap_Info():BooleanField					{return _IsWrapInfo;}
		public function get Col_Info():NumberField						{return _ColInfo;}
		public function get Default_Info():TextField					{return _DefaultInfo;}
		public function get IsUnique_Info():BooleanField				{return _IsUniqueInfo;}
		public function get T_Info():ChoiceField						{return _TInfo;}
		public function get Lines_Info():NumberField					{return _LinesInfo;}
		public function get Len_Info():NumberField						{return _LenInfo;}
		public function get IsApp_Info():BooleanField					{return _IsAppInfo;}
		public function get TxtWidth_Info():NumberField					{return _TxtWidthInfo;}
		public function get Entry_Info():TextField						{return _EntryInfo;}
		public function get IsMult_Info():BooleanField					{return _IsMultInfo;}
		public function get IsNew_Info():BooleanField					{return _IsNewInfo;}
		public function get IsSort_Info():BooleanField					{return _IsSortInfo;}
		public function get IsHTML_Info():BooleanField					{return _IsHTMLInfo;}
		public function get N_Info():ChoiceField						{return _NInfo;}
		public function get Units_Info():TextField						{return _UnitsInfo;}
		public function get NumWidth_Info():NumberField					{return _NumWidthInfo;}
		public function get TreatAs_Info():TextField					{return _TreatAsInfo;}
		public function get Dec_Info():NumberField						{return _DecInfo;}
		public function get IsTotals_Info():BooleanField				{return _IsTotalsInfo;}
		public function get IsAvg_Info():BooleanField					{return _IsAvgInfo;}
		public function get D_Info():ChoiceField						{return _DInfo;}
		public function get IsAlpha_Info():BooleanField					{return _IsAlphaInfo;}
		public function get IsSmartDate_Info():BooleanField				{return _IsSmartDateInfo;}
		public function get DateType_Info():TextField					{return _DateTypeInfo;}
		public function get Text_Info():TextField						{return _TextInfo;}
		public function get IsJPG_Info():BooleanField					{return _IsJPGInfo;}
		public function get Revs_Info():NumberField						{return _RevsInfo;}
		public function get test_Info():TextField						{return _testInfo;}
		public function get IsUpdateable_Info():BooleanField			{return _IsUpdateableInfo;}
		public function get IsText_Info():BooleanField					{return _IsTextInfo;}
		public function get IsNumeric_Info():BooleanField				{return _IsNumericInfo;}
		public function get IsDate_Info():BooleanField					{return _IsDateInfo;}
		public function get Eqn_Info():TextField						{return _EqnInfo;}
		public function get IsConsultant_Info():BooleanField			{return _IsConsultantInfo;}
		public function get IsClient_Info():BooleanField				{return _IsClientInfo;}
		public function get IsContractor_Info():BooleanField			{return _IsContractorInfo;}
		public function get IsInspector_Info():BooleanField				{return _IsInspectorInfo;}
		public function get IRPSource_Info():TextField					{return _IRPSourceInfo;}
		public function get DateCreated_Info():TimeStampField			{return _DateCreatedInfo;}
		public function get DateModified_Info():TimeStampField			{return _DateModifiedInfo;}
		public function get RecordOwner_Info():UserIdField				{return _RecordOwnerInfo;}
		public function get LastModifiedBy_Info():UserIdField			{return _LastModifiedByInfo;}

		// Field getter variables
		private var _fieldNames:ArrayCollection = new ArrayCollection(["RelatedTable", "Table", "SeqAndName", "DBID", "Seq", "Seq2", 
																		"Label", "Type", "FieldID", "Set", "IsReqd", "IsWrap", "Col", "Default", "IsUnique", "T", "Lines", "Len", 
																		"IsApp", "TxtWidth", "Entry", "IsMult", "IsNew", "IsSort", "IsHTML", "N", "Units", "NumWidth", "TreatAs", "Dec", 
																		"IsTotals", "IsAvg", "D", "IsAlpha", "IsSmartDate", "DateType", "Text", "IsJPG", "Revs", "test", "IsUpdateable", 
																		"IsText", "IsNumeric", "IsDate", "Eqn", "IsConsultant", "IsClient", "IsContractor", "IsInspector", "IRPSource", 
																		"DateCreated", "DateModified", "RecordOwner", "LastModifiedBy", ]);
		private var _fieldInfo:ArrayCollection = new ArrayCollection();

		// Field getters
		public function get FieldNames():ArrayCollection				{return _fieldNames;}
		public function get FieldsInfo():ArrayCollection				{return _fieldInfo;}
	}
}

class Private{}
