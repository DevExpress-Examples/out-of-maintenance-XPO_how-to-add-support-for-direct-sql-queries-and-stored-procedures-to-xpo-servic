using System;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DXSample.BO;

namespace DBUpdater {
    class Program {
        static void Main(string[] args) {
            UnitOfWork uow = new UnitOfWork(XpoDefault.GetDataLayer(MSSqlConnectionProvider.GetConnectionString(".", "CommandChannelExample"),
                AutoCreateOption.DatabaseAndSchema));
            uow.UpdateSchema(typeof(Customer).Assembly);
            uow.CreateObjectTypeRecords(typeof(Customer).Assembly);
            if (uow.FindObject<Customer>(null) == null) {
                Customer john = new Customer(uow);
                john.Name = "John";
                john.Age = 26;
                Customer bob = new Customer(uow);
                bob.Name = "Bob";
                bob.Age = 31;
                Order chai = new Order(uow);
                chai.OrderName = "Chai";
                chai.OrderDate = new DateTime(2010, 12, 20);
                chai.Customer = john;
                Order chang = new Order(uow);
                chang.OrderName = "Chang";
                chang.OrderDate = new DateTime(2010, 12, 21);
                chang.Customer = john;
                Order queso = new Order(uow);
                queso.OrderName = "Queso Caprale";
                queso.OrderDate = new DateTime(2010, 12, 22);
                queso.Customer = bob;
                uow.CommitChanges();
            }
            uow.ExecuteNonQuery(string.Concat("if exists (",
                                                "select * ",
                                                "from sys.objects ",
                                                "where object_id = OBJECT_ID('CustomerOrders') and ",
                                                    "type in ('P', 'PC')) ",
                                                "drop procedure CustomerOrders"));
            uow.ExecuteNonQuery(string.Concat("create procedure CustomerOrders @CustomerID int as ",
                                                "select [Order].OID ",
                                                "from [Order] ",
                                                "inner join Customer ",
                                                "on Customer.OID = [Order].Customer ",
                                                "where Customer.OID = @CustomerID"));
        }
    }
}
