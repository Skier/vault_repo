﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.573
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

namespace DPI.Reports {
    using System;
    using System.Data;
    using System.Xml;
    using System.Runtime.Serialization;
    
    
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.ToolboxItem(true)]
    public class dsPin_Receipt : DataSet {
        
        private Pin_ReceiptDataTable tablePin_Receipt;
        
        public dsPin_Receipt() {
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        protected dsPin_Receipt(SerializationInfo info, StreamingContext context) {
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((strSchema != null)) {
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
                if ((ds.Tables["Pin_Receipt"] != null)) {
                    this.Tables.Add(new Pin_ReceiptDataTable(ds.Tables["Pin_Receipt"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.InitClass();
            }
            this.GetSerializationData(info, context);
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Content)]
        public Pin_ReceiptDataTable Pin_Receipt {
            get {
                return this.tablePin_Receipt;
            }
        }
        
        public override DataSet Clone() {
            dsPin_Receipt cln = ((dsPin_Receipt)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override bool ShouldSerializeTables() {
            return false;
        }
        
        protected override bool ShouldSerializeRelations() {
            return false;
        }
        
        protected override void ReadXmlSerializable(XmlReader reader) {
            this.Reset();
            DataSet ds = new DataSet();
            ds.ReadXml(reader);
            if ((ds.Tables["Pin_Receipt"] != null)) {
                this.Tables.Add(new Pin_ReceiptDataTable(ds.Tables["Pin_Receipt"]));
            }
            this.DataSetName = ds.DataSetName;
            this.Prefix = ds.Prefix;
            this.Namespace = ds.Namespace;
            this.Locale = ds.Locale;
            this.CaseSensitive = ds.CaseSensitive;
            this.EnforceConstraints = ds.EnforceConstraints;
            this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
            this.InitVars();
        }
        
        protected override System.Xml.Schema.XmlSchema GetSchemaSerializable() {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            this.WriteXmlSchema(new XmlTextWriter(stream, null));
            stream.Position = 0;
            return System.Xml.Schema.XmlSchema.Read(new XmlTextReader(stream), null);
        }
        
        internal void InitVars() {
            this.tablePin_Receipt = ((Pin_ReceiptDataTable)(this.Tables["Pin_Receipt"]));
            if ((this.tablePin_Receipt != null)) {
                this.tablePin_Receipt.InitVars();
            }
        }
        
        private void InitClass() {
            this.DataSetName = "dsPin_Receipt";
            this.Prefix = "";
            this.Namespace = "http://tempuri.org/dsPin_Receipt.xsd";
            this.Locale = new System.Globalization.CultureInfo("en-US");
            this.CaseSensitive = false;
            this.EnforceConstraints = true;
            this.tablePin_Receipt = new Pin_ReceiptDataTable();
            this.Tables.Add(this.tablePin_Receipt);
        }
        
        private bool ShouldSerializePin_Receipt() {
            return false;
        }
        
        private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        public delegate void Pin_ReceiptRowChangeEventHandler(object sender, Pin_ReceiptRowChangeEvent e);
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class Pin_ReceiptDataTable : DataTable, System.Collections.IEnumerable {
            
            private DataColumn columnHeading1;
            
            private DataColumn columnHeading2;
            
            private DataColumn columnConfirmation_Number;
            
            private DataColumn columnPin;
            
            private DataColumn columnStoreCode;
            
            private DataColumn columnPayDate;
            
            private DataColumn columnPaymentType;
            
            private DataColumn columnProduct_ID;
            
            private DataColumn columnProduct_Name;
            
            private DataColumn columnProduct_Price;
            
            private DataColumn columnReceipt_Text;
            
            private DataColumn columnSubTotal;
            
            private DataColumn columnTotalAmountDue;
            
            private DataColumn columnAmountTendered;
            
            private DataColumn columnChangeDue;
            
            private DataColumn columnMessage1;
            
            private DataColumn columnMessage2;
            
            private DataColumn columnMessage3;
            
            private DataColumn columnMessage4;
            
            private DataColumn columnMessage5;
            
            internal Pin_ReceiptDataTable() : 
                    base("Pin_Receipt") {
                this.InitClass();
            }
            
            internal Pin_ReceiptDataTable(DataTable table) : 
                    base(table.TableName) {
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
                this.DisplayExpression = table.DisplayExpression;
            }
            
            [System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            internal DataColumn Heading1Column {
                get {
                    return this.columnHeading1;
                }
            }
            
            internal DataColumn Heading2Column {
                get {
                    return this.columnHeading2;
                }
            }
            
            internal DataColumn Confirmation_NumberColumn {
                get {
                    return this.columnConfirmation_Number;
                }
            }
            
            internal DataColumn PinColumn {
                get {
                    return this.columnPin;
                }
            }
            
            internal DataColumn StoreCodeColumn {
                get {
                    return this.columnStoreCode;
                }
            }
            
            internal DataColumn PayDateColumn {
                get {
                    return this.columnPayDate;
                }
            }
            
            internal DataColumn PaymentTypeColumn {
                get {
                    return this.columnPaymentType;
                }
            }
            
            internal DataColumn Product_IDColumn {
                get {
                    return this.columnProduct_ID;
                }
            }
            
            internal DataColumn Product_NameColumn {
                get {
                    return this.columnProduct_Name;
                }
            }
            
            internal DataColumn Product_PriceColumn {
                get {
                    return this.columnProduct_Price;
                }
            }
            
            internal DataColumn Receipt_TextColumn {
                get {
                    return this.columnReceipt_Text;
                }
            }
            
            internal DataColumn SubTotalColumn {
                get {
                    return this.columnSubTotal;
                }
            }
            
            internal DataColumn TotalAmountDueColumn {
                get {
                    return this.columnTotalAmountDue;
                }
            }
            
            internal DataColumn AmountTenderedColumn {
                get {
                    return this.columnAmountTendered;
                }
            }
            
            internal DataColumn ChangeDueColumn {
                get {
                    return this.columnChangeDue;
                }
            }
            
            internal DataColumn Message1Column {
                get {
                    return this.columnMessage1;
                }
            }
            
            internal DataColumn Message2Column {
                get {
                    return this.columnMessage2;
                }
            }
            
            internal DataColumn Message3Column {
                get {
                    return this.columnMessage3;
                }
            }
            
            internal DataColumn Message4Column {
                get {
                    return this.columnMessage4;
                }
            }
            
            internal DataColumn Message5Column {
                get {
                    return this.columnMessage5;
                }
            }
            
            public Pin_ReceiptRow this[int index] {
                get {
                    return ((Pin_ReceiptRow)(this.Rows[index]));
                }
            }
            
            public event Pin_ReceiptRowChangeEventHandler Pin_ReceiptRowChanged;
            
            public event Pin_ReceiptRowChangeEventHandler Pin_ReceiptRowChanging;
            
            public event Pin_ReceiptRowChangeEventHandler Pin_ReceiptRowDeleted;
            
            public event Pin_ReceiptRowChangeEventHandler Pin_ReceiptRowDeleting;
            
            public void AddPin_ReceiptRow(Pin_ReceiptRow row) {
                this.Rows.Add(row);
            }
            
            public Pin_ReceiptRow AddPin_ReceiptRow(
                        string Heading1, 
                        string Heading2, 
                        string Confirmation_Number, 
                        string Pin, 
                        string StoreCode, 
                        System.DateTime PayDate, 
                        string PaymentType, 
                        int Product_ID, 
                        string Product_Name, 
                        System.Decimal Product_Price, 
                        string Receipt_Text, 
                        System.Decimal SubTotal, 
                        System.Decimal TotalAmountDue, 
                        System.Decimal AmountTendered, 
                        System.Decimal ChangeDue, 
                        string Message1, 
                        string Message2, 
                        string Message3, 
                        string Message4, 
                        string Message5) {
                Pin_ReceiptRow rowPin_ReceiptRow = ((Pin_ReceiptRow)(this.NewRow()));
                rowPin_ReceiptRow.ItemArray = new object[] {
                        Heading1,
                        Heading2,
                        Confirmation_Number,
                        Pin,
                        StoreCode,
                        PayDate,
                        PaymentType,
                        Product_ID,
                        Product_Name,
                        Product_Price,
                        Receipt_Text,
                        SubTotal,
                        TotalAmountDue,
                        AmountTendered,
                        ChangeDue,
                        Message1,
                        Message2,
                        Message3,
                        Message4,
                        Message5};
                this.Rows.Add(rowPin_ReceiptRow);
                return rowPin_ReceiptRow;
            }
            
            public System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            public override DataTable Clone() {
                Pin_ReceiptDataTable cln = ((Pin_ReceiptDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            protected override DataTable CreateInstance() {
                return new Pin_ReceiptDataTable();
            }
            
            internal void InitVars() {
                this.columnHeading1 = this.Columns["Heading1"];
                this.columnHeading2 = this.Columns["Heading2"];
                this.columnConfirmation_Number = this.Columns["Confirmation_Number"];
                this.columnPin = this.Columns["Pin"];
                this.columnStoreCode = this.Columns["StoreCode"];
                this.columnPayDate = this.Columns["PayDate"];
                this.columnPaymentType = this.Columns["PaymentType"];
                this.columnProduct_ID = this.Columns["Product_ID"];
                this.columnProduct_Name = this.Columns["Product_Name"];
                this.columnProduct_Price = this.Columns["Product_Price"];
                this.columnReceipt_Text = this.Columns["Receipt_Text"];
                this.columnSubTotal = this.Columns["SubTotal"];
                this.columnTotalAmountDue = this.Columns["TotalAmountDue"];
                this.columnAmountTendered = this.Columns["AmountTendered"];
                this.columnChangeDue = this.Columns["ChangeDue"];
                this.columnMessage1 = this.Columns["Message1"];
                this.columnMessage2 = this.Columns["Message2"];
                this.columnMessage3 = this.Columns["Message3"];
                this.columnMessage4 = this.Columns["Message4"];
                this.columnMessage5 = this.Columns["Message5"];
            }
            
            private void InitClass() {
                this.columnHeading1 = new DataColumn("Heading1", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnHeading1);
                this.columnHeading2 = new DataColumn("Heading2", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnHeading2);
                this.columnConfirmation_Number = new DataColumn("Confirmation_Number", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnConfirmation_Number);
                this.columnPin = new DataColumn("Pin", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnPin);
                this.columnStoreCode = new DataColumn("StoreCode", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnStoreCode);
                this.columnPayDate = new DataColumn("PayDate", typeof(System.DateTime), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnPayDate);
                this.columnPaymentType = new DataColumn("PaymentType", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnPaymentType);
                this.columnProduct_ID = new DataColumn("Product_ID", typeof(int), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnProduct_ID);
                this.columnProduct_Name = new DataColumn("Product_Name", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnProduct_Name);
                this.columnProduct_Price = new DataColumn("Product_Price", typeof(System.Decimal), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnProduct_Price);
                this.columnReceipt_Text = new DataColumn("Receipt_Text", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnReceipt_Text);
                this.columnSubTotal = new DataColumn("SubTotal", typeof(System.Decimal), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnSubTotal);
                this.columnTotalAmountDue = new DataColumn("TotalAmountDue", typeof(System.Decimal), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnTotalAmountDue);
                this.columnAmountTendered = new DataColumn("AmountTendered", typeof(System.Decimal), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnAmountTendered);
                this.columnChangeDue = new DataColumn("ChangeDue", typeof(System.Decimal), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnChangeDue);
                this.columnMessage1 = new DataColumn("Message1", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnMessage1);
                this.columnMessage2 = new DataColumn("Message2", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnMessage2);
                this.columnMessage3 = new DataColumn("Message3", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnMessage3);
                this.columnMessage4 = new DataColumn("Message4", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnMessage4);
                this.columnMessage5 = new DataColumn("Message5", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnMessage5);
            }
            
            public Pin_ReceiptRow NewPin_ReceiptRow() {
                return ((Pin_ReceiptRow)(this.NewRow()));
            }
            
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder) {
                return new Pin_ReceiptRow(builder);
            }
            
            protected override System.Type GetRowType() {
                return typeof(Pin_ReceiptRow);
            }
            
            protected override void OnRowChanged(DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.Pin_ReceiptRowChanged != null)) {
                    this.Pin_ReceiptRowChanged(this, new Pin_ReceiptRowChangeEvent(((Pin_ReceiptRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowChanging(DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.Pin_ReceiptRowChanging != null)) {
                    this.Pin_ReceiptRowChanging(this, new Pin_ReceiptRowChangeEvent(((Pin_ReceiptRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleted(DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.Pin_ReceiptRowDeleted != null)) {
                    this.Pin_ReceiptRowDeleted(this, new Pin_ReceiptRowChangeEvent(((Pin_ReceiptRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleting(DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.Pin_ReceiptRowDeleting != null)) {
                    this.Pin_ReceiptRowDeleting(this, new Pin_ReceiptRowChangeEvent(((Pin_ReceiptRow)(e.Row)), e.Action));
                }
            }
            
            public void RemovePin_ReceiptRow(Pin_ReceiptRow row) {
                this.Rows.Remove(row);
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class Pin_ReceiptRow : DataRow {
            
            private Pin_ReceiptDataTable tablePin_Receipt;
            
            internal Pin_ReceiptRow(DataRowBuilder rb) : 
                    base(rb) {
                this.tablePin_Receipt = ((Pin_ReceiptDataTable)(this.Table));
            }
            
            public string Heading1 {
                get {
                    try {
                        return ((string)(this[this.tablePin_Receipt.Heading1Column]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablePin_Receipt.Heading1Column] = value;
                }
            }
            
            public string Heading2 {
                get {
                    try {
                        return ((string)(this[this.tablePin_Receipt.Heading2Column]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablePin_Receipt.Heading2Column] = value;
                }
            }
            
            public string Confirmation_Number {
                get {
                    try {
                        return ((string)(this[this.tablePin_Receipt.Confirmation_NumberColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablePin_Receipt.Confirmation_NumberColumn] = value;
                }
            }
            
            public string Pin {
                get {
                    try {
                        return ((string)(this[this.tablePin_Receipt.PinColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablePin_Receipt.PinColumn] = value;
                }
            }
            
            public string StoreCode {
                get {
                    try {
                        return ((string)(this[this.tablePin_Receipt.StoreCodeColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablePin_Receipt.StoreCodeColumn] = value;
                }
            }
            
            public System.DateTime PayDate {
                get {
                    try {
                        return ((System.DateTime)(this[this.tablePin_Receipt.PayDateColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablePin_Receipt.PayDateColumn] = value;
                }
            }
            
            public string PaymentType {
                get {
                    try {
                        return ((string)(this[this.tablePin_Receipt.PaymentTypeColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablePin_Receipt.PaymentTypeColumn] = value;
                }
            }
            
            public int Product_ID {
                get {
                    try {
                        return ((int)(this[this.tablePin_Receipt.Product_IDColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablePin_Receipt.Product_IDColumn] = value;
                }
            }
            
            public string Product_Name {
                get {
                    try {
                        return ((string)(this[this.tablePin_Receipt.Product_NameColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablePin_Receipt.Product_NameColumn] = value;
                }
            }
            
            public System.Decimal Product_Price {
                get {
                    try {
                        return ((System.Decimal)(this[this.tablePin_Receipt.Product_PriceColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablePin_Receipt.Product_PriceColumn] = value;
                }
            }
            
            public string Receipt_Text {
                get {
                    try {
                        return ((string)(this[this.tablePin_Receipt.Receipt_TextColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablePin_Receipt.Receipt_TextColumn] = value;
                }
            }
            
            public System.Decimal SubTotal {
                get {
                    try {
                        return ((System.Decimal)(this[this.tablePin_Receipt.SubTotalColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablePin_Receipt.SubTotalColumn] = value;
                }
            }
            
            public System.Decimal TotalAmountDue {
                get {
                    try {
                        return ((System.Decimal)(this[this.tablePin_Receipt.TotalAmountDueColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablePin_Receipt.TotalAmountDueColumn] = value;
                }
            }
            
            public System.Decimal AmountTendered {
                get {
                    try {
                        return ((System.Decimal)(this[this.tablePin_Receipt.AmountTenderedColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablePin_Receipt.AmountTenderedColumn] = value;
                }
            }
            
            public System.Decimal ChangeDue {
                get {
                    try {
                        return ((System.Decimal)(this[this.tablePin_Receipt.ChangeDueColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablePin_Receipt.ChangeDueColumn] = value;
                }
            }
            
            public string Message1 {
                get {
                    try {
                        return ((string)(this[this.tablePin_Receipt.Message1Column]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablePin_Receipt.Message1Column] = value;
                }
            }
            
            public string Message2 {
                get {
                    try {
                        return ((string)(this[this.tablePin_Receipt.Message2Column]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablePin_Receipt.Message2Column] = value;
                }
            }
            
            public string Message3 {
                get {
                    try {
                        return ((string)(this[this.tablePin_Receipt.Message3Column]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablePin_Receipt.Message3Column] = value;
                }
            }
            
            public string Message4 {
                get {
                    try {
                        return ((string)(this[this.tablePin_Receipt.Message4Column]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablePin_Receipt.Message4Column] = value;
                }
            }
            
            public string Message5 {
                get {
                    try {
                        return ((string)(this[this.tablePin_Receipt.Message5Column]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablePin_Receipt.Message5Column] = value;
                }
            }
            
            public bool IsHeading1Null() {
                return this.IsNull(this.tablePin_Receipt.Heading1Column);
            }
            
            public void SetHeading1Null() {
                this[this.tablePin_Receipt.Heading1Column] = System.Convert.DBNull;
            }
            
            public bool IsHeading2Null() {
                return this.IsNull(this.tablePin_Receipt.Heading2Column);
            }
            
            public void SetHeading2Null() {
                this[this.tablePin_Receipt.Heading2Column] = System.Convert.DBNull;
            }
            
            public bool IsConfirmation_NumberNull() {
                return this.IsNull(this.tablePin_Receipt.Confirmation_NumberColumn);
            }
            
            public void SetConfirmation_NumberNull() {
                this[this.tablePin_Receipt.Confirmation_NumberColumn] = System.Convert.DBNull;
            }
            
            public bool IsPinNull() {
                return this.IsNull(this.tablePin_Receipt.PinColumn);
            }
            
            public void SetPinNull() {
                this[this.tablePin_Receipt.PinColumn] = System.Convert.DBNull;
            }
            
            public bool IsStoreCodeNull() {
                return this.IsNull(this.tablePin_Receipt.StoreCodeColumn);
            }
            
            public void SetStoreCodeNull() {
                this[this.tablePin_Receipt.StoreCodeColumn] = System.Convert.DBNull;
            }
            
            public bool IsPayDateNull() {
                return this.IsNull(this.tablePin_Receipt.PayDateColumn);
            }
            
            public void SetPayDateNull() {
                this[this.tablePin_Receipt.PayDateColumn] = System.Convert.DBNull;
            }
            
            public bool IsPaymentTypeNull() {
                return this.IsNull(this.tablePin_Receipt.PaymentTypeColumn);
            }
            
            public void SetPaymentTypeNull() {
                this[this.tablePin_Receipt.PaymentTypeColumn] = System.Convert.DBNull;
            }
            
            public bool IsProduct_IDNull() {
                return this.IsNull(this.tablePin_Receipt.Product_IDColumn);
            }
            
            public void SetProduct_IDNull() {
                this[this.tablePin_Receipt.Product_IDColumn] = System.Convert.DBNull;
            }
            
            public bool IsProduct_NameNull() {
                return this.IsNull(this.tablePin_Receipt.Product_NameColumn);
            }
            
            public void SetProduct_NameNull() {
                this[this.tablePin_Receipt.Product_NameColumn] = System.Convert.DBNull;
            }
            
            public bool IsProduct_PriceNull() {
                return this.IsNull(this.tablePin_Receipt.Product_PriceColumn);
            }
            
            public void SetProduct_PriceNull() {
                this[this.tablePin_Receipt.Product_PriceColumn] = System.Convert.DBNull;
            }
            
            public bool IsReceipt_TextNull() {
                return this.IsNull(this.tablePin_Receipt.Receipt_TextColumn);
            }
            
            public void SetReceipt_TextNull() {
                this[this.tablePin_Receipt.Receipt_TextColumn] = System.Convert.DBNull;
            }
            
            public bool IsSubTotalNull() {
                return this.IsNull(this.tablePin_Receipt.SubTotalColumn);
            }
            
            public void SetSubTotalNull() {
                this[this.tablePin_Receipt.SubTotalColumn] = System.Convert.DBNull;
            }
            
            public bool IsTotalAmountDueNull() {
                return this.IsNull(this.tablePin_Receipt.TotalAmountDueColumn);
            }
            
            public void SetTotalAmountDueNull() {
                this[this.tablePin_Receipt.TotalAmountDueColumn] = System.Convert.DBNull;
            }
            
            public bool IsAmountTenderedNull() {
                return this.IsNull(this.tablePin_Receipt.AmountTenderedColumn);
            }
            
            public void SetAmountTenderedNull() {
                this[this.tablePin_Receipt.AmountTenderedColumn] = System.Convert.DBNull;
            }
            
            public bool IsChangeDueNull() {
                return this.IsNull(this.tablePin_Receipt.ChangeDueColumn);
            }
            
            public void SetChangeDueNull() {
                this[this.tablePin_Receipt.ChangeDueColumn] = System.Convert.DBNull;
            }
            
            public bool IsMessage1Null() {
                return this.IsNull(this.tablePin_Receipt.Message1Column);
            }
            
            public void SetMessage1Null() {
                this[this.tablePin_Receipt.Message1Column] = System.Convert.DBNull;
            }
            
            public bool IsMessage2Null() {
                return this.IsNull(this.tablePin_Receipt.Message2Column);
            }
            
            public void SetMessage2Null() {
                this[this.tablePin_Receipt.Message2Column] = System.Convert.DBNull;
            }
            
            public bool IsMessage3Null() {
                return this.IsNull(this.tablePin_Receipt.Message3Column);
            }
            
            public void SetMessage3Null() {
                this[this.tablePin_Receipt.Message3Column] = System.Convert.DBNull;
            }
            
            public bool IsMessage4Null() {
                return this.IsNull(this.tablePin_Receipt.Message4Column);
            }
            
            public void SetMessage4Null() {
                this[this.tablePin_Receipt.Message4Column] = System.Convert.DBNull;
            }
            
            public bool IsMessage5Null() {
                return this.IsNull(this.tablePin_Receipt.Message5Column);
            }
            
            public void SetMessage5Null() {
                this[this.tablePin_Receipt.Message5Column] = System.Convert.DBNull;
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class Pin_ReceiptRowChangeEvent : EventArgs {
            
            private Pin_ReceiptRow eventRow;
            
            private DataRowAction eventAction;
            
            public Pin_ReceiptRowChangeEvent(Pin_ReceiptRow row, DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            public Pin_ReceiptRow Row {
                get {
                    return this.eventRow;
                }
            }
            
            public DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
    }
}