Imports Microsoft.VisualBasic
Imports System
Imports DXSample.BO
Imports System.Web.UI
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.Web.ASPxGridView
Imports DXSample.Client

Partial Public Class _Default
	Inherits Page
	Private xpoSession As Session = XpoHelper.GetNewSession()

	Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Init
		customersSource.Session = xpoSession
		Dim customerId As Integer = Convert.ToInt32(customersCombo.Value)
		ordersView.DataSource = xpoSession.GetObjectsByKeyFromSproc(xpoSession.GetClassInfo(Of Order)(), True, "CustomerOrders", New OperandValue(customerId))
	End Sub

	Protected Sub OnOrdersViewCustomCallback(ByVal sender As Object, ByVal args As ASPxGridViewCustomCallbackEventArgs) Handles ordersView.CustomCallback
		Dim parameters() As String = args.Parameters.Split("|"c)
		If parameters(0) = "CCSIC" AndAlso parameters.Length = 2 Then
			Dim customerId As Integer = Convert.ToInt32(parameters(1))
			ordersView.DataSource = xpoSession.GetObjectsByKeyFromSproc(xpoSession.GetClassInfo(Of Order)(), True, "CustomerOrders", New OperandValue(customerId))
			ordersView.DataBind()
		End If
	End Sub
End Class
