using System;
using DevExpress.Data.Filtering;

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