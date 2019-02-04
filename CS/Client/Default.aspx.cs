using System;
using DXSample.BO;
using System.Web.UI;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Web;
using DXSample.Client;

public partial class _Default : Page 
{
    Session session = XpoHelper.GetNewSession();

    protected void Page_Init(object sender, EventArgs e)
    {
        customersSource.Session = session;
        int customerId = Convert.ToInt32(customersCombo.Value);
        ordersView.DataSource = session.GetObjectsByKeyFromSproc(session.GetClassInfo<Order>(), true, "CustomerOrders", new OperandValue(customerId));
    }

    protected void OnOrdersViewCustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs args) {
        string[] parameters = args.Parameters.Split('|');
        if (parameters[0] == "CCSIC" && parameters.Length == 2) {
            int customerId = Convert.ToInt32(parameters[1]);
            ordersView.DataSource = session.GetObjectsByKeyFromSproc(session.GetClassInfo<Order>(), true, "CustomerOrders", 
                new OperandValue(customerId));
            ordersView.DataBind();
        }
    }
}
