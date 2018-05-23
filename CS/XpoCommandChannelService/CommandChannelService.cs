using System.Web.Services;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Helpers;
using System.Xml.Serialization;

[WebService(Namespace = "http://devexpress.example/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class CommandChannelSercice : WebService
{
    [WebMethod]
    public ModificationResult ModifyData(params ModificationStatement[] dmlStatements) {
        return ((IDataStore)Application["DataStore"]).ModifyData(dmlStatements);
    }

    [WebMethod]
    public SelectedData SelectData(params SelectStatement[] selects) {
        return ((IDataStore)Application["DataStore"]).SelectData(selects);
    }

    [WebMethod, XmlInclude(typeof(StoredProcedureArgument))]
    public object Do(string command, object args) {
        object result = ((ICommandChannel)Application["DataStore"]).Do(command, PrepareArguments(args));
        return result;
    }

    object PrepareArguments(object args) {
        StoredProcedureArgument argument = args as StoredProcedureArgument;
        if (argument == null) return args;
        return new object[] { argument.StoredProcedureName, argument.Arguments };
    }
}
