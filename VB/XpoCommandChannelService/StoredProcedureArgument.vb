Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Data.Filtering

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