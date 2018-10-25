using DXSample.BO;
using DevExpress.Xpo;
using DXSample.DataStore;
using System.Configuration;
using DevExpress.Xpo.Metadata;

namespace DXSample.Client {
    public static class XpoHelper {
        public static Session GetNewSession() {
            return new Session(DataLayer);
        }

        public static UnitOfWork GetNewUnitOfWork() {
            return new UnitOfWork(DataLayer);
        }

        private readonly static object lockObject = new object();

        static volatile IDataLayer fDataLayer;
        static IDataLayer DataLayer {
            get {
                if (fDataLayer == null) {
                    lock (lockObject) {
                        if (fDataLayer == null) {
                            fDataLayer = GetDataLayer();
                        }
                    }
                }
                return fDataLayer;
            }
        }

        private static IDataLayer GetDataLayer() {
            XpoDefault.Session = null;
            XPDictionary dict = new ReflectionDictionary();
            dict.GetDataStoreSchema(typeof(Customer).Assembly);
            return new ThreadSafeDataLayer(dict, 
                new CommandChannelServiceDataStore(ConfigurationManager.ConnectionStrings["XpoServiceUrl"].ConnectionString));
        }
    }
}