Imports Microsoft.VisualBasic
Imports System.Web.Services
Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo.Helpers
Imports System.Xml.Serialization

<WebService(Namespace := "http://devexpress.example/"), WebServiceBinding(ConformsTo := WsiProfiles.BasicProfile1_1)> _
Public Class CommandChannelSercice
	Inherits WebService
	<WebMethod> _
	Public Function ModifyData(ParamArray ByVal dmlStatements() As ModificationStatement) As ModificationResult
		Return (CType(Application("DataStore"), IDataStore)).ModifyData(dmlStatements)
	End Function

	<WebMethod> _
	Public Function SelectData(ParamArray ByVal selects() As SelectStatement) As SelectedData
		Return (CType(Application("DataStore"), IDataStore)).SelectData(selects)
	End Function

	<WebMethod, XmlInclude(GetType(StoredProcedureArgument))> _
	Public Function [Do](ByVal command As String, ByVal args As Object) As Object
		Dim result As Object = (CType(Application("DataStore"), ICommandChannel)).Do(command, PrepareArguments(args))
		Return result
	End Function

	Private Function PrepareArguments(ByVal args As Object) As Object
		Dim argument As StoredProcedureArgument = TryCast(args, StoredProcedureArgument)
		If argument Is Nothing Then
			Return args
		End If
		Return New Object() { argument.StoredProcedureName, argument.Arguments }
	End Function
End Class
