using DevExpress.Xpo;

namespace DXSample.BO {
    public class Customer :XPObject {
        public Customer(Session session) : base(session) { }

        private string fName;
        public string Name {
            get { return fName; }
            set { SetPropertyValue("Name", ref fName, value); }
        }

        private int fAge;
        public int Age {
            get { return fAge; }
            set { SetPropertyValue("Age", ref fAge, value); }
        }

        [Association("Customer-Orders")]
        public XPCollection<Order> Orders { get { return GetCollection<Order>("Orders"); } }
    }
}