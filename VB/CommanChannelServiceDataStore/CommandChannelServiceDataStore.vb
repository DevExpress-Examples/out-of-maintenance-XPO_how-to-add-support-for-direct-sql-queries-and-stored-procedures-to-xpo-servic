Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Xpo.DB
Imports System.Web.Services
Imports DevExpress.Xpo.Helpers
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic

Namespace DXSample.DataStore
	<WebServiceBinding(Namespace:="http://devexpress.example/")> _
	Public Class CommandChannelServiceDataStore
		Inherits SoapHttpClientProtocol
		Implements IDataStore, ICommandChannel
		Public Sub New(ByVal url As String)
			MyBase.New()
			Me.Url = url
		End Sub

		Protected Overridable ReadOnly Property AutoCreateOption() As AutoCreateOption
			Get
				Return AutoCreateOption.SchemaAlreadyExists
			End Get
		End Property

		<SoapDocumentMethod("http://devexpress.example/ModifyData", RequestNamespace := "http://devexpress.example/", ResponseNamespace := "http://devexpress.example/")> _
		Public Overridable Function ModifyData(ParamArray ByVal dmlStatements() As ModificationStatement) As ModificationResult
			Return CType(Invoke("ModifyData", New Object() { dmlStatements })(0), ModificationResult)
		End Function

		<SoapDocumentMethod("http://devexpress.example/SelectData", RequestNamespace := "http://devexpress.example/", ResponseNamespace := "http://devexpress.example/")> _
		Public Overridable Function SelectData(ParamArray ByVal selects() As SelectStatement) As SelectedData
			Return CType(Invoke("SelectData", New Object() { selects })(0), SelectedData)
		End Function

		Protected Overridable Function UpdateSchema(ByVal dontCreateIfFirstTableNotExist As Boolean, ParamArray ByVal tables() As DBTable) As UpdateSchemaResult
			Throw New InvalidOperationException("Schema modification is not allowed")
		End Function

		<SoapDocumentMethod("http://devexpress.example/Do", RequestNamespace := "http://devexpress.example/", ResponseNamespace := "http://devexpress.example/"), XmlInclude(GetType(StoredProcedureArgument))> _
		Public Overridable Function [Do](ByVal command As String, ByVal args As Object) As Object
			Return Invoke("Do", New Object() { command, PrepareArguments(args) })(0)
		End Function

		Private Function PrepareArguments(ByVal args As Object) As Object
			Dim arguments() As Object = TryCast(args, Object())
			If arguments Is Nothing Then
				Return args
			End If
			Return New StoredProcedureArgument(arguments(0), arguments(1))
		End Function

		#Region "IDataStore Members"

		Private ReadOnly Property IDataStore_AutoCreateOption() As AutoCreateOption Implements IDataStore.AutoCreateOption
			Get
				Return AutoCreateOption
			End Get
		End Property

		Private Function IDataStore_ModifyData(ParamArray ByVal dmlStatements() As ModificationStatement) As ModificationResult Implements IDataStore.ModifyData
			Return ModifyData(dmlStatements)
		End Function

		Private Function IDataStore_SelectData(ParamArray ByVal selects() As SelectStatement) As SelectedData Implements IDataStore.SelectData
			Return SelectData(selects)
		End Function

		Private Function IDataStore_UpdateSchema(ByVal dontCreateIfFirstTableNotExist As Boolean, ParamArray ByVal tables() As DBTable) As UpdateSchemaResult Implements IDataStore.UpdateSchema
			Return UpdateSchema(dontCreateIfFirstTableNotExist, tables)
		End Function

		#End Region

		#Region "ICommandChannel Members"

		Private Function ICommandChannel_Do(ByVal command As String, ByVal args As Object) As Object Implements ICommandChannel.Do
			Return [Do](command, args)
		End Function

		#End Region
	End Class

	<Serializable> _
	Public Class StoredProcedureArgument
		Public Sub New()
		End Sub

		Public Sub New(ByVal sprocName As Object, ByVal args As Object)
			StoredProcedureName = sprocName.ToString()
			Arguments = CType(args, OperandValue())
		End Sub

		Private fStoredProcedureName As String
		Public Property StoredProcedureName() As String
			Get
				Return fStoredProcedureName
			End Get
			Set(ByVal value As String)
				fStoredProcedureName = value
			End Set
		End Property

		Private fArguments() As OperandValue
		Public Property Arguments() As OperandValue()
			Get
				Return fArguments
			End Get
			Set(ByVal value As OperandValue())
				fArguments = value
			End Set
		End Property
	End Class
End Namespace