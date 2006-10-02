﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.2032
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

namespace PTM.Data {
    using System;
    using System.Data;
    using System.Xml;
    using System.Runtime.Serialization;
    
    
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.ToolboxItem(true)]
    public class PTMDataset : DataSet {
        
        private TasksDataTable tableTasks;
        
        private DataRelation relationTasksTasks;
        
        public PTMDataset() {
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        protected PTMDataset(SerializationInfo info, StreamingContext context) {
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((strSchema != null)) {
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
                if ((ds.Tables["Tasks"] != null)) {
                    this.Tables.Add(new TasksDataTable(ds.Tables["Tasks"]));
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
        public TasksDataTable Tasks {
            get {
                return this.tableTasks;
            }
        }
        
        public override DataSet Clone() {
            PTMDataset cln = ((PTMDataset)(base.Clone()));
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
            if ((ds.Tables["Tasks"] != null)) {
                this.Tables.Add(new TasksDataTable(ds.Tables["Tasks"]));
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
            this.tableTasks = ((TasksDataTable)(this.Tables["Tasks"]));
            if ((this.tableTasks != null)) {
                this.tableTasks.InitVars();
            }
            this.relationTasksTasks = this.Relations["TasksTasks"];
        }
        
        private void InitClass() {
            this.DataSetName = "PTMDataset";
            this.Prefix = "";
            this.Namespace = "http://www.tempuri.org/PTMDataset.xsd";
            this.Locale = new System.Globalization.CultureInfo("en-US");
            this.CaseSensitive = false;
            this.EnforceConstraints = true;
            this.tableTasks = new TasksDataTable();
            this.Tables.Add(this.tableTasks);
            ForeignKeyConstraint fkc;
            fkc = new ForeignKeyConstraint("TasksTasks", new DataColumn[] {
                        this.tableTasks.IdColumn}, new DataColumn[] {
                        this.tableTasks.ParentIdColumn});
            this.tableTasks.Constraints.Add(fkc);
            fkc.AcceptRejectRule = System.Data.AcceptRejectRule.None;
            fkc.DeleteRule = System.Data.Rule.Cascade;
            fkc.UpdateRule = System.Data.Rule.Cascade;
            this.relationTasksTasks = new DataRelation("TasksTasks", new DataColumn[] {
                        this.tableTasks.IdColumn}, new DataColumn[] {
                        this.tableTasks.ParentIdColumn}, false);
            this.Relations.Add(this.relationTasksTasks);
        }
        
        private bool ShouldSerializeTasks() {
            return false;
        }
        
        private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        public delegate void TasksRowChangeEventHandler(object sender, TasksRowChangeEvent e);
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class TasksDataTable : DataTable, System.Collections.IEnumerable {
            
            private DataColumn columnDefaultTaskId;
            
            private DataColumn columnDescription;
            
            private DataColumn columnId;
            
            private DataColumn columnIsDefaultTask;
            
            private DataColumn columnIsFinished;
            
            private DataColumn columnParentId;
            
            private DataColumn columnStartDate;
            
            private DataColumn columnStopDate;
            
            private DataColumn columnTotalTime;
            
            internal TasksDataTable() : 
                    base("Tasks") {
                this.InitClass();
            }
            
            internal TasksDataTable(DataTable table) : 
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
            
            internal DataColumn DefaultTaskIdColumn {
                get {
                    return this.columnDefaultTaskId;
                }
            }
            
            internal DataColumn DescriptionColumn {
                get {
                    return this.columnDescription;
                }
            }
            
            internal DataColumn IdColumn {
                get {
                    return this.columnId;
                }
            }
            
            internal DataColumn IsDefaultTaskColumn {
                get {
                    return this.columnIsDefaultTask;
                }
            }
            
            internal DataColumn IsFinishedColumn {
                get {
                    return this.columnIsFinished;
                }
            }
            
            internal DataColumn ParentIdColumn {
                get {
                    return this.columnParentId;
                }
            }
            
            internal DataColumn StartDateColumn {
                get {
                    return this.columnStartDate;
                }
            }
            
            internal DataColumn StopDateColumn {
                get {
                    return this.columnStopDate;
                }
            }
            
            internal DataColumn TotalTimeColumn {
                get {
                    return this.columnTotalTime;
                }
            }
            
            public TasksRow this[int index] {
                get {
                    return ((TasksRow)(this.Rows[index]));
                }
            }
            
            public event TasksRowChangeEventHandler TasksRowChanged;
            
            public event TasksRowChangeEventHandler TasksRowChanging;
            
            public event TasksRowChangeEventHandler TasksRowDeleted;
            
            public event TasksRowChangeEventHandler TasksRowDeleting;
            
            public void AddTasksRow(TasksRow row) {
                this.Rows.Add(row);
            }
            
            public TasksRow AddTasksRow(int DefaultTaskId, string Description, bool IsDefaultTask, bool IsFinished, TasksRow parentTasksRowByTasksTasks, System.DateTime StartDate, System.DateTime StopDate, int TotalTime) {
                TasksRow rowTasksRow = ((TasksRow)(this.NewRow()));
                rowTasksRow.ItemArray = new object[] {
                        DefaultTaskId,
                        Description,
                        null,
                        IsDefaultTask,
                        IsFinished,
                        parentTasksRowByTasksTasks[2],
                        StartDate,
                        StopDate,
                        TotalTime};
                this.Rows.Add(rowTasksRow);
                return rowTasksRow;
            }
            
            public TasksRow FindById(int Id) {
                return ((TasksRow)(this.Rows.Find(new object[] {
                            Id})));
            }
            
            public System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            public override DataTable Clone() {
                TasksDataTable cln = ((TasksDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            protected override DataTable CreateInstance() {
                return new TasksDataTable();
            }
            
            internal void InitVars() {
                this.columnDefaultTaskId = this.Columns["DefaultTaskId"];
                this.columnDescription = this.Columns["Description"];
                this.columnId = this.Columns["Id"];
                this.columnIsDefaultTask = this.Columns["IsDefaultTask"];
                this.columnIsFinished = this.Columns["IsFinished"];
                this.columnParentId = this.Columns["ParentId"];
                this.columnStartDate = this.Columns["StartDate"];
                this.columnStopDate = this.Columns["StopDate"];
                this.columnTotalTime = this.Columns["TotalTime"];
            }
            
            private void InitClass() {
                this.columnDefaultTaskId = new DataColumn("DefaultTaskId", typeof(int), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnDefaultTaskId);
                this.columnDescription = new DataColumn("Description", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnDescription);
                this.columnId = new DataColumn("Id", typeof(int), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnId);
                this.columnIsDefaultTask = new DataColumn("IsDefaultTask", typeof(bool), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnIsDefaultTask);
                this.columnIsFinished = new DataColumn("IsFinished", typeof(bool), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnIsFinished);
                this.columnParentId = new DataColumn("ParentId", typeof(int), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnParentId);
                this.columnStartDate = new DataColumn("StartDate", typeof(System.DateTime), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnStartDate);
                this.columnStopDate = new DataColumn("StopDate", typeof(System.DateTime), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnStopDate);
                this.columnTotalTime = new DataColumn("TotalTime", typeof(int), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnTotalTime);
                this.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[] {
                                this.columnId}, true));
                this.columnId.AutoIncrement = true;
                this.columnId.AllowDBNull = false;
                this.columnId.Unique = true;
            }
            
            public TasksRow NewTasksRow() {
                return ((TasksRow)(this.NewRow()));
            }
            
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder) {
                return new TasksRow(builder);
            }
            
            protected override System.Type GetRowType() {
                return typeof(TasksRow);
            }
            
            protected override void OnRowChanged(DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.TasksRowChanged != null)) {
                    this.TasksRowChanged(this, new TasksRowChangeEvent(((TasksRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowChanging(DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.TasksRowChanging != null)) {
                    this.TasksRowChanging(this, new TasksRowChangeEvent(((TasksRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleted(DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.TasksRowDeleted != null)) {
                    this.TasksRowDeleted(this, new TasksRowChangeEvent(((TasksRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleting(DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.TasksRowDeleting != null)) {
                    this.TasksRowDeleting(this, new TasksRowChangeEvent(((TasksRow)(e.Row)), e.Action));
                }
            }
            
            public void RemoveTasksRow(TasksRow row) {
                this.Rows.Remove(row);
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class TasksRow : DataRow {
            
            private TasksDataTable tableTasks;
            
            internal TasksRow(DataRowBuilder rb) : 
                    base(rb) {
                this.tableTasks = ((TasksDataTable)(this.Table));
            }
            
            public int DefaultTaskId {
                get {
                    try {
                        return ((int)(this[this.tableTasks.DefaultTaskIdColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("No se puede obtener el valor porque es DBNull.", e);
                    }
                }
                set {
                    this[this.tableTasks.DefaultTaskIdColumn] = value;
                }
            }
            
            public string Description {
                get {
                    try {
                        return ((string)(this[this.tableTasks.DescriptionColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("No se puede obtener el valor porque es DBNull.", e);
                    }
                }
                set {
                    this[this.tableTasks.DescriptionColumn] = value;
                }
            }
            
            public int Id {
                get {
                    return ((int)(this[this.tableTasks.IdColumn]));
                }
                set {
                    this[this.tableTasks.IdColumn] = value;
                }
            }
            
            public bool IsDefaultTask {
                get {
                    try {
                        return ((bool)(this[this.tableTasks.IsDefaultTaskColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("No se puede obtener el valor porque es DBNull.", e);
                    }
                }
                set {
                    this[this.tableTasks.IsDefaultTaskColumn] = value;
                }
            }
            
            public bool IsFinished {
                get {
                    try {
                        return ((bool)(this[this.tableTasks.IsFinishedColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("No se puede obtener el valor porque es DBNull.", e);
                    }
                }
                set {
                    this[this.tableTasks.IsFinishedColumn] = value;
                }
            }
            
            public int ParentId {
                get {
                    try {
                        return ((int)(this[this.tableTasks.ParentIdColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("No se puede obtener el valor porque es DBNull.", e);
                    }
                }
                set {
                    this[this.tableTasks.ParentIdColumn] = value;
                }
            }
            
            public System.DateTime StartDate {
                get {
                    try {
                        return ((System.DateTime)(this[this.tableTasks.StartDateColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("No se puede obtener el valor porque es DBNull.", e);
                    }
                }
                set {
                    this[this.tableTasks.StartDateColumn] = value;
                }
            }
            
            public System.DateTime StopDate {
                get {
                    try {
                        return ((System.DateTime)(this[this.tableTasks.StopDateColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("No se puede obtener el valor porque es DBNull.", e);
                    }
                }
                set {
                    this[this.tableTasks.StopDateColumn] = value;
                }
            }
            
            public int TotalTime {
                get {
                    try {
                        return ((int)(this[this.tableTasks.TotalTimeColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("No se puede obtener el valor porque es DBNull.", e);
                    }
                }
                set {
                    this[this.tableTasks.TotalTimeColumn] = value;
                }
            }
            
            public TasksRow TasksRowParent {
                get {
                    return ((TasksRow)(this.GetParentRow(this.Table.ParentRelations["TasksTasks"])));
                }
                set {
                    this.SetParentRow(value, this.Table.ParentRelations["TasksTasks"]);
                }
            }
            
            public bool IsDefaultTaskIdNull() {
                return this.IsNull(this.tableTasks.DefaultTaskIdColumn);
            }
            
            public void SetDefaultTaskIdNull() {
                this[this.tableTasks.DefaultTaskIdColumn] = System.Convert.DBNull;
            }
            
            public bool IsDescriptionNull() {
                return this.IsNull(this.tableTasks.DescriptionColumn);
            }
            
            public void SetDescriptionNull() {
                this[this.tableTasks.DescriptionColumn] = System.Convert.DBNull;
            }
            
            public bool IsIsDefaultTaskNull() {
                return this.IsNull(this.tableTasks.IsDefaultTaskColumn);
            }
            
            public void SetIsDefaultTaskNull() {
                this[this.tableTasks.IsDefaultTaskColumn] = System.Convert.DBNull;
            }
            
            public bool IsIsFinishedNull() {
                return this.IsNull(this.tableTasks.IsFinishedColumn);
            }
            
            public void SetIsFinishedNull() {
                this[this.tableTasks.IsFinishedColumn] = System.Convert.DBNull;
            }
            
            public bool IsParentIdNull() {
                return this.IsNull(this.tableTasks.ParentIdColumn);
            }
            
            public void SetParentIdNull() {
                this[this.tableTasks.ParentIdColumn] = System.Convert.DBNull;
            }
            
            public bool IsStartDateNull() {
                return this.IsNull(this.tableTasks.StartDateColumn);
            }
            
            public void SetStartDateNull() {
                this[this.tableTasks.StartDateColumn] = System.Convert.DBNull;
            }
            
            public bool IsStopDateNull() {
                return this.IsNull(this.tableTasks.StopDateColumn);
            }
            
            public void SetStopDateNull() {
                this[this.tableTasks.StopDateColumn] = System.Convert.DBNull;
            }
            
            public bool IsTotalTimeNull() {
                return this.IsNull(this.tableTasks.TotalTimeColumn);
            }
            
            public void SetTotalTimeNull() {
                this[this.tableTasks.TotalTimeColumn] = System.Convert.DBNull;
            }
            
            public TasksRow[] GetTasksRows() {
                return ((TasksRow[])(this.GetChildRows(this.Table.ChildRelations["TasksTasks"])));
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class TasksRowChangeEvent : EventArgs {
            
            private TasksRow eventRow;
            
            private DataRowAction eventAction;
            
            public TasksRowChangeEvent(TasksRow row, DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            public TasksRow Row {
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
