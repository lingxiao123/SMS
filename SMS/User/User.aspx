<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="SMS.User.User" %>

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
                <f:ToolbarSeparator ID="ToolbarSeparator4" runat="server"></f:ToolbarSeparator>
                <f:Button ID="btnImport" runat="server" Text="新增用户"
                        Icon="Add" EnablePostBack="false" >
                </f:Button>
                <f:Button ID="btnSave" runat="server" Text="保存修改" OnClick="btnSave_Click" Icon="Disk" >

                </f:Button>
                <f:Window ID="Window1" IconUrl="~/res/images/16/10.png" runat="server" Hidden="true"
                    WindowPosition="Center" IsModal="true" Title="新增用户" EnableMaximize="true"
                    EnableResize="true" Target="Self" EnableIFrame="true"
                    Height="500px" Width="650px" >
                </f:Window>
                <f:ToolbarSeparator ID="ToolbarSeparator5" runat="server"></f:ToolbarSeparator>
            </Items>
        </f:Toolbar>
    </Toolbars>
    <Items>
        <f:Grid ID="Grid1" Title="用户信息"   Width="100%" PageSize="5" ShowBorder="true" ShowHeader="true"
            AllowPaging="true" runat="server" EnableCheckBoxSelect="True"
            DataKeyNames="Id,Name" IsDatabasePaging="true"   OnPageIndexChange="Grid1_PageIndexChange"
            AllowSorting="true" SortField="id" SortDirection="ASC"  ClicksToEdit="1" AllowCellEditing="true"
            OnSort="Grid1_Sort" OnRowCommand="Grid1_RowCommand" >
            <Columns>
                <f:BoundField ID="Id" DataField="Id" HeaderText="序号"></f:BoundField>
                <f:BoundField ID="Name" DataField="UserName" HeaderText="用户名"></f:BoundField>
                <%--<f:CheckBoxField DataField="Status" HeaderText="是否启用" />--%>
                <f:RenderField Width="100px" ColumnID="Status" DataField="Status" FieldType="Int"
                    RendererFunction="renderGender" HeaderText="状态">
                    <Editor>
                        <f:DropDownList ID="ddlStatus" Required="true" runat="server">
                            <f:ListItem Text="启用" Value="1" />
                            <f:ListItem Text="禁用" Value="0" />
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                <f:BoundField ID="AddTime" DataField="AddTime" HeaderText="注册时间"></f:BoundField>
                <f:BoundField ID="UpdateTime" DataField="UpdateTime" HeaderText="修改时间"></f:BoundField>
                <f:LinkButtonField HeaderText="操作" Width="80px" CommandName="Action1" Text="重置密码" />
            </Columns>
        </f:Grid>
    </Items>
    </f:Panel>
    </form>
    <script>
 
        function renderGender(value) {
            return value == 1 ? '启用' : '禁用';
        }
 
 
    </script>
</body>
</html>
