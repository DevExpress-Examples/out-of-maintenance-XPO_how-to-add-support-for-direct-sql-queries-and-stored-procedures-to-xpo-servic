Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB
Imports DXSample.BO

Namespace DBUpdater
	Friend Class Program
		Shared Sub Main(ByVal args() As String)
			Dim uow As New UnitOfWork(XpoDefault.GetDataLayer(MSSqlConnectionProvider.GetConnectionString(".", "CommandChannelExample"), AutoCreateOption.DatabaseAndSchema))
			uow.UpdateSchema(GetType(Customer).Assembly)
			uow.CreateObjectTypeRecords(GetType(Customer).Assembly)
			If uow.FindObject(Of Customer)(Nothing) Is Nothing Then
				Dim john As New Customer(uow)
				john.Name = "John"
				john.Age = 26
				Dim bob As New Customer(uow)
				bob.Name = "Bob"
				bob.Age = 31
				Dim chai As New Order(uow)
				chai.OrderName = "Chai"
				chai.OrderDate = New DateTime(2010, 12, 20)
				chai.Customer = john
				Dim chang As New Order(uow)
				chang.OrderName = "Chang"
				chang.OrderDate = New DateTime(2010, 12, 21)
				chang.Customer = john
				Dim queso As New Order(uow)
				queso.OrderName = "Queso Caprale"
				queso.OrderDate = New DateTime(2010, 12, 22)
				queso.Customer = bob
				uow.CommitChanges()
			End If
			uow.ExecuteNonQuery(String.Concat("if exists (", "select * ", "from sys.objects ", "where object_id = OBJECT_ID('CustomerOrders') and ", "type in ('P', 'PC')) ", "drop procedure CustomerOrders"))
			uow.ExecuteNonQuery(String.Concat("create procedure CustomerOrders @CustomerID int as ", "select [Order].OID ", "from [Order] ", "inner join Customer ", "on Customer.OID = [Order].Customer ", "where Customer.OID = @CustomerID"))
		End Sub
	End Class
End Namespace
