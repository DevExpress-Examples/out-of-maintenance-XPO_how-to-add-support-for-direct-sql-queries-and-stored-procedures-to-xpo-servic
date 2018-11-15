<!-- default file list -->
*Files to look at*:

* [XpoHelper.cs](./CS/Client/XpoHelper.cs) (VB: [XpoHelper.vb](./VB/Client/XpoHelper.vb))
* [CommandChannelServiceDataStore.cs](./CS/CommanChannelServiceDataStore/CommandChannelServiceDataStore.cs) (VB: [CommandChannelServiceDataStore.vb](./VB/CommanChannelServiceDataStore/CommandChannelServiceDataStore.vb))
* [CommandChannelService.cs](./CS/XpoCommandChannelService/CommandChannelService.cs) (VB: [CommandChannelService.vb](./VB/XpoCommandChannelService/CommandChannelService.vb))
* [Global.asax](./CS/XpoCommandChannelService/Global.asax) (VB: [Global.asax](./VB/XpoCommandChannelService/Global.asax))
* [StoredProcedureArgument.cs](./CS/XpoCommandChannelService/StoredProcedureArgument.cs) (VB: [StoredProcedureArgument.vb](./VB/XpoCommandChannelService/StoredProcedureArgument.vb))
<!-- default file list end -->
# How to add support for direct SQL queries and stored procedures to XPO service


<p>Direct SQL queries and stored procedures executed using the ICommandChannel interface methods. This interface should be implemented by the proxy class, and service should expose methods of this interface.</p><p>Starting from version 2011 vol 1, the WebServiceDataStore natively supports this feature. See the corresponding suggestion for additional information: <a href="https://www.devexpress.com/Support/Center/p/S36438">Implement the ICommandChannel interface in the WebServiceDataStore class</a>.</p><p><strong>See also:<br />
</strong><a href="http://community.devexpress.com/blogs/xpo/archive/2006/04/13/xpo-is-good-for-distributed-applications.aspx"><u>XPO is good for distributed applications</u></a><strong><br />
</strong><a href="https://www.devexpress.com/Support/Center/p/AK3911">How to use XPO with a Web Service</a></p>

<br/>


