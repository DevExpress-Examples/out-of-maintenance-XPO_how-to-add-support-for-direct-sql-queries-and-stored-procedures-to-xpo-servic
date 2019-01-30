<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Xpo.v13.1.Web, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.Xpo" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <dx:XpoDataSource ID="customersSource" runat="server" TypeName="DXSample.BO.Customer" />
        <dx:ASPxComboBox ID="customersCombo" runat="server" DataSourceID="customersSource" TextField="Name" ValueField="Oid">
            <ClientSideEvents 
                SelectedIndexChanged="function (s, e) { ordersView.PerformCallback(&quot;CCSIC|&quot; + s.GetSelectedItem().value); }" />
        </dx:ASPxComboBox>
        <dx:ASPxGridView ID="ordersView" runat="server" OnCustomCallback="OnOrdersViewCustomCallback">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="OrderName" />
                <dx:GridViewDataDateColumn FieldName="OrderDate" />
            </Columns>
        </dx:ASPxGridView>
    </div>
    </form>
</body>
</html>
