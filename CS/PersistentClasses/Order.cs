using System;
using DevExpress.Xpo;

namespace DXSample.BO {
    public class Order :XPObject {
        public Order(Session session) : base(session) { }

        private string fOrderName;
        public string OrderName {
            get { return fOrderName; }
            set { SetPropertyValue("OrderName", ref fOrderName, value); }
        }

        private DateTime fOrderDate;
        public DateTime OrderDate {
            get { return fOrderDate; }
            set { SetPropertyValue("OrderDate", ref fOrderDate, value); }
        }

        private Customer fCustomer;
        [Association("Customer-Orders")]
        public Customer Customer {
            get { return fCustomer; }
            set { SetPropertyValue("Customer", ref fCustomer, value); }
        }
    }
}