using System;
using DevExpress.Xpo.DB;
using System.Web.Services;
using DevExpress.Xpo.Helpers;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using DevExpress.Data.Filtering;
using System.Collections.Generic;

namespace DXSample.DataStore {
    [WebServiceBinding(Namespace="http://devexpress.example/")]
    public class CommandChannelServiceDataStore :SoapHttpClientProtocol, IDataStore, ICommandChannel {
        public CommandChannelServiceDataStore(string url) : base() {
            Url = url;
        }

        protected virtual AutoCreateOption AutoCreateOption { get { return AutoCreateOption.SchemaAlreadyExists; } }

        [SoapDocumentMethod("http://devexpress.example/ModifyData", RequestNamespace = "http://devexpress.example/", 
            ResponseNamespace = "http://devexpress.example/")]
        public virtual ModificationResult ModifyData(params ModificationStatement[] dmlStatements) {
            return (ModificationResult)Invoke("ModifyData", new object[] { dmlStatements })[0];
        }

        [SoapDocumentMethod("http://devexpress.example/SelectData", RequestNamespace = "http://devexpress.example/", 
            ResponseNamespace = "http://devexpress.example/")]
        public virtual SelectedData SelectData(params SelectStatement[] selects) {
            return (SelectedData)Invoke("SelectData", new object[] { selects })[0];
        }

        protected virtual UpdateSchemaResult UpdateSchema(bool dontCreateIfFirstTableNotExist, params DBTable[] tables) {
            throw new InvalidOperationException("Schema modification is not allowed");
        }

        [SoapDocumentMethod("http://devexpress.example/Do", RequestNamespace = "http://devexpress.example/", 
            ResponseNamespace = "http://devexpress.example/")]
        [XmlInclude(typeof(StoredProcedureArgument))]
        public virtual object Do(string command, object args) {
            return Invoke("Do", new object[] { command, PrepareArguments(args) })[0];
        }

        object PrepareArguments(object args) {
            object[] arguments = args as object[];
            if (arguments == null) return args;
            return new StoredProcedureArgument(arguments[0], arguments[1]);
        }

        #region IDataStore Members

        AutoCreateOption IDataStore.AutoCreateOption {
            get { return AutoCreateOption; }
        }

        ModificationResult IDataStore.ModifyData(params ModificationStatement[] dmlStatements) {
            return ModifyData(dmlStatements);
        }

        SelectedData IDataStore.SelectData(params SelectStatement[] selects) {
            return SelectData(selects);
        }

        UpdateSchemaResult IDataStore.UpdateSchema(bool dontCreateIfFirstTableNotExist, params DBTable[] tables) {
            return UpdateSchema(dontCreateIfFirstTableNotExist, tables);
        }

        #endregion

        #region ICommandChannel Members

        object ICommandChannel.Do(string command, object args) {
            return Do(command, args);
        }

        #endregion
    }

    [Serializable]
    public class StoredProcedureArgument {
        public StoredProcedureArgument() { }

        public StoredProcedureArgument(object sprocName, object args) {
            StoredProcedureName = sprocName.ToString();
            Arguments = (OperandValue[])args;
        }

        private string fStoredProcedureName;
        public string StoredProcedureName {
            get { return fStoredProcedureName; }
            set { fStoredProcedureName = value; }
        }

        private OperandValue[] fArguments;
        public OperandValue[] Arguments {
            get { return fArguments; }
            set { fArguments = value; }
        }
    }
}
