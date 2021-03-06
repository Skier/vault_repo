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
    public class dsCertResult : DataSet {
        
        private spCertResults_Get_StoreCodeDataTable tablespCertResults_Get_StoreCode;
        
        public dsCertResult() {
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        protected dsCertResult(SerializationInfo info, StreamingContext context) {
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((strSchema != null)) {
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
                if ((ds.Tables["spCertResults_Get_StoreCode"] != null)) {
                    this.Tables.Add(new spCertResults_Get_StoreCodeDataTable(ds.Tables["spCertResults_Get_StoreCode"]));
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
        public spCertResults_Get_StoreCodeDataTable spCertResults_Get_StoreCode {
            get {
                return this.tablespCertResults_Get_StoreCode;
            }
        }
        
        public override DataSet Clone() {
            dsCertResult cln = ((dsCertResult)(base.Clone()));
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
            if ((ds.Tables["spCertResults_Get_StoreCode"] != null)) {
                this.Tables.Add(new spCertResults_Get_StoreCodeDataTable(ds.Tables["spCertResults_Get_StoreCode"]));
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
            this.tablespCertResults_Get_StoreCode = ((spCertResults_Get_StoreCodeDataTable)(this.Tables["spCertResults_Get_StoreCode"]));
            if ((this.tablespCertResults_Get_StoreCode != null)) {
                this.tablespCertResults_Get_StoreCode.InitVars();
            }
        }
        
        private void InitClass() {
            this.DataSetName = "dsCertResult";
            this.Prefix = "";
            this.Namespace = "http://tempuri.org/dsCertResult.xsd";
            this.Locale = new System.Globalization.CultureInfo("en-US");
            this.CaseSensitive = false;
            this.EnforceConstraints = true;
            this.tablespCertResults_Get_StoreCode = new spCertResults_Get_StoreCodeDataTable();
            this.Tables.Add(this.tablespCertResults_Get_StoreCode);
        }
        
        private bool ShouldSerializespCertResults_Get_StoreCode() {
            return false;
        }
        
        private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        public delegate void spCertResults_Get_StoreCodeRowChangeEventHandler(object sender, spCertResults_Get_StoreCodeRowChangeEvent e);
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class spCertResults_Get_StoreCodeDataTable : DataTable, System.Collections.IEnumerable {
            
            private DataColumn columnType;
            
            private DataColumn columnStoreNumber;
            
            private DataColumn columnStoreCode;
            
            private DataColumn columnName;
            
            private DataColumn columnCertDate;
            
            private DataColumn columnStatus;
            
            internal spCertResults_Get_StoreCodeDataTable() : 
                    base("spCertResults_Get_StoreCode") {
                this.InitClass();
            }
            
            internal spCertResults_Get_StoreCodeDataTable(DataTable table) : 
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
            
            internal DataColumn TypeColumn {
                get {
                    return this.columnType;
                }
            }
            
            internal DataColumn StoreNumberColumn {
                get {
                    return this.columnStoreNumber;
                }
            }
            
            internal DataColumn StoreCodeColumn {
                get {
                    return this.columnStoreCode;
                }
            }
            
            internal DataColumn NameColumn {
                get {
                    return this.columnName;
                }
            }
            
            internal DataColumn CertDateColumn {
                get {
                    return this.columnCertDate;
                }
            }
            
            internal DataColumn StatusColumn {
                get {
                    return this.columnStatus;
                }
            }
            
            public spCertResults_Get_StoreCodeRow this[int index] {
                get {
                    return ((spCertResults_Get_StoreCodeRow)(this.Rows[index]));
                }
            }
            
            public event spCertResults_Get_StoreCodeRowChangeEventHandler spCertResults_Get_StoreCodeRowChanged;
            
            public event spCertResults_Get_StoreCodeRowChangeEventHandler spCertResults_Get_StoreCodeRowChanging;
            
            public event spCertResults_Get_StoreCodeRowChangeEventHandler spCertResults_Get_StoreCodeRowDeleted;
            
            public event spCertResults_Get_StoreCodeRowChangeEventHandler spCertResults_Get_StoreCodeRowDeleting;
            
            public void AddspCertResults_Get_StoreCodeRow(spCertResults_Get_StoreCodeRow row) {
                this.Rows.Add(row);
            }
            
            public spCertResults_Get_StoreCodeRow AddspCertResults_Get_StoreCodeRow(string Type, string StoreNumber, string StoreCode, string Name, System.DateTime CertDate, string Status) {
                spCertResults_Get_StoreCodeRow rowspCertResults_Get_StoreCodeRow = ((spCertResults_Get_StoreCodeRow)(this.NewRow()));
                rowspCertResults_Get_StoreCodeRow.ItemArray = new object[] {
                        Type,
                        StoreNumber,
                        StoreCode,
                        Name,
                        CertDate,
                        Status};
                this.Rows.Add(rowspCertResults_Get_StoreCodeRow);
                return rowspCertResults_Get_StoreCodeRow;
            }
            
            public System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            public override DataTable Clone() {
                spCertResults_Get_StoreCodeDataTable cln = ((spCertResults_Get_StoreCodeDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            protected override DataTable CreateInstance() {
                return new spCertResults_Get_StoreCodeDataTable();
            }
            
            internal void InitVars() {
                this.columnType = this.Columns["Type"];
                this.columnStoreNumber = this.Columns["StoreNumber"];
                this.columnStoreCode = this.Columns["StoreCode"];
                this.columnName = this.Columns["Name"];
                this.columnCertDate = this.Columns["CertDate"];
                this.columnStatus = this.Columns["Status"];
            }
            
            private void InitClass() {
                this.columnType = new DataColumn("Type", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnType);
                this.columnStoreNumber = new DataColumn("StoreNumber", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnStoreNumber);
                this.columnStoreCode = new DataColumn("StoreCode", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnStoreCode);
                this.columnName = new DataColumn("Name", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnName);
                this.columnCertDate = new DataColumn("CertDate", typeof(System.DateTime), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnCertDate);
                this.columnStatus = new DataColumn("Status", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnStatus);
            }
            
            public spCertResults_Get_StoreCodeRow NewspCertResults_Get_StoreCodeRow() {
                return ((spCertResults_Get_StoreCodeRow)(this.NewRow()));
            }
            
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder) {
                return new spCertResults_Get_StoreCodeRow(builder);
            }
            
            protected override System.Type GetRowType() {
                return typeof(spCertResults_Get_StoreCodeRow);
            }
            
            protected override void OnRowChanged(DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.spCertResults_Get_StoreCodeRowChanged != null)) {
                    this.spCertResults_Get_StoreCodeRowChanged(this, new spCertResults_Get_StoreCodeRowChangeEvent(((spCertResults_Get_StoreCodeRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowChanging(DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.spCertResults_Get_StoreCodeRowChanging != null)) {
                    this.spCertResults_Get_StoreCodeRowChanging(this, new spCertResults_Get_StoreCodeRowChangeEvent(((spCertResults_Get_StoreCodeRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleted(DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.spCertResults_Get_StoreCodeRowDeleted != null)) {
                    this.spCertResults_Get_StoreCodeRowDeleted(this, new spCertResults_Get_StoreCodeRowChangeEvent(((spCertResults_Get_StoreCodeRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleting(DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.spCertResults_Get_StoreCodeRowDeleting != null)) {
                    this.spCertResults_Get_StoreCodeRowDeleting(this, new spCertResults_Get_StoreCodeRowChangeEvent(((spCertResults_Get_StoreCodeRow)(e.Row)), e.Action));
                }
            }
            
            public void RemovespCertResults_Get_StoreCodeRow(spCertResults_Get_StoreCodeRow row) {
                this.Rows.Remove(row);
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class spCertResults_Get_StoreCodeRow : DataRow {
            
            private spCertResults_Get_StoreCodeDataTable tablespCertResults_Get_StoreCode;
            
            internal spCertResults_Get_StoreCodeRow(DataRowBuilder rb) : 
                    base(rb) {
                this.tablespCertResults_Get_StoreCode = ((spCertResults_Get_StoreCodeDataTable)(this.Table));
            }
            
            public string Type {
                get {
                    try {
                        return ((string)(this[this.tablespCertResults_Get_StoreCode.TypeColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablespCertResults_Get_StoreCode.TypeColumn] = value;
                }
            }
            
            public string StoreNumber {
                get {
                    try {
                        return ((string)(this[this.tablespCertResults_Get_StoreCode.StoreNumberColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablespCertResults_Get_StoreCode.StoreNumberColumn] = value;
                }
            }
            
            public string StoreCode {
                get {
                    try {
                        return ((string)(this[this.tablespCertResults_Get_StoreCode.StoreCodeColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablespCertResults_Get_StoreCode.StoreCodeColumn] = value;
                }
            }
            
            public string Name {
                get {
                    try {
                        return ((string)(this[this.tablespCertResults_Get_StoreCode.NameColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablespCertResults_Get_StoreCode.NameColumn] = value;
                }
            }
            
            public System.DateTime CertDate {
                get {
                    try {
                        return ((System.DateTime)(this[this.tablespCertResults_Get_StoreCode.CertDateColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablespCertResults_Get_StoreCode.CertDateColumn] = value;
                }
            }
            
            public string Status {
                get {
                    try {
                        return ((string)(this[this.tablespCertResults_Get_StoreCode.StatusColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tablespCertResults_Get_StoreCode.StatusColumn] = value;
                }
            }
            
            public bool IsTypeNull() {
                return this.IsNull(this.tablespCertResults_Get_StoreCode.TypeColumn);
            }
            
            public void SetTypeNull() {
                this[this.tablespCertResults_Get_StoreCode.TypeColumn] = System.Convert.DBNull;
            }
            
            public bool IsStoreNumberNull() {
                return this.IsNull(this.tablespCertResults_Get_StoreCode.StoreNumberColumn);
            }
            
            public void SetStoreNumberNull() {
                this[this.tablespCertResults_Get_StoreCode.StoreNumberColumn] = System.Convert.DBNull;
            }
            
            public bool IsStoreCodeNull() {
                return this.IsNull(this.tablespCertResults_Get_StoreCode.StoreCodeColumn);
            }
            
            public void SetStoreCodeNull() {
                this[this.tablespCertResults_Get_StoreCode.StoreCodeColumn] = System.Convert.DBNull;
            }
            
            public bool IsNameNull() {
                return this.IsNull(this.tablespCertResults_Get_StoreCode.NameColumn);
            }
            
            public void SetNameNull() {
                this[this.tablespCertResults_Get_StoreCode.NameColumn] = System.Convert.DBNull;
            }
            
            public bool IsCertDateNull() {
                return this.IsNull(this.tablespCertResults_Get_StoreCode.CertDateColumn);
            }
            
            public void SetCertDateNull() {
                this[this.tablespCertResults_Get_StoreCode.CertDateColumn] = System.Convert.DBNull;
            }
            
            public bool IsStatusNull() {
                return this.IsNull(this.tablespCertResults_Get_StoreCode.StatusColumn);
            }
            
            public void SetStatusNull() {
                this[this.tablespCertResults_Get_StoreCode.StatusColumn] = System.Convert.DBNull;
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class spCertResults_Get_StoreCodeRowChangeEvent : EventArgs {
            
            private spCertResults_Get_StoreCodeRow eventRow;
            
            private DataRowAction eventAction;
            
            public spCertResults_Get_StoreCodeRowChangeEvent(spCertResults_Get_StoreCodeRow row, DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            public spCertResults_Get_StoreCodeRow Row {
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
