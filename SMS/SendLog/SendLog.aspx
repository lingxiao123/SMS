<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendLog.aspx.cs" Inherits="SMS.SendLog.SendLog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Width="100%" ShowBorder="false" ShowHeader="false" Layout="Region">
    <Toolbars>
        <f:Toolbar ID="toolb1" runat="server">
            <Items>
                <f:TextBox ID="txtDLCode" runat="server" Label="发货单号" Text=""></f:TextBox>
                <f:ToolbarSeparator ID="ToolbarSeparator4" runat="server"></f:ToolbarSeparator>
                <f:DatePicker ID="dateTime" runat="server" Label="发送时间"></f:DatePicker>
                <f:ToolbarSeparator ID="ToolbarSeparator5" runat="server"></f:ToolbarSeparator>
                <f:Button ID="btnSerach" runat="server" Text="搜索" OnClick="btnSerach_Click"></f:Button>
            </Items>
        </f:Toolbar>
    </Toolbars>
    <Items>
        <f:Grid ID="Grid1" Title="发货通知"   Width="100%" PageSize="20" ShowBorder="true" ShowHeader="true"
            AllowPaging="true" runat="server" EnableCheckBoxSelect="True"
            DataKeyNames="Id,AddTime" IsDatabasePaging="true"   OnPageIndexChange="Grid1_PageIndexChange"
            AllowSorting="true" SortField="AddTime" SortDirection="DESC"  ClicksToEdit="1" AllowCellEditing="true"
            OnSort="Grid1_Sort">
            <Columns>
                <f:RowNumberField EnablePagingNumber="true" />         
                <f:BoundField ID="CusName" DataField="CusName" HeaderText="客户名称"></f:BoundField>
                <f:BoundField ID="CusHand" DataField="CusHand" HeaderText="手机号码"></f:BoundField>
                <f:BoundField ID="DLCode" DataField="DLCode" HeaderText="发货单号"></f:BoundField>
                <f:CheckBoxField DataField="Status" HeaderText="是否发送成功" />
                <f:BoundField ID="AddTime" DataField="AddTime" Width="150px" HeaderText="发送时间"></f:BoundField>
            </Columns>
        </f:Grid>
    </Items>
    </f:Panel>
    </form>
</body>
</html>
