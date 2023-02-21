<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="MAZIKONG.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>财务处原始数据查询</title>
</head>
<body>
    <form id="form1" runat="server">
        
        <div>
            <h4>财务处原始数据查询</h4>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="CWCId" DataSourceID="SqlDataSource1" EmptyDataText="数据为空" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="CWCId" HeaderText="CWCId" ReadOnly="True" SortExpression="CWCId" />
                    <asp:BoundField DataField="StudentName" HeaderText="StudentName" SortExpression="StudentName" />
                    <asp:BoundField DataField="IdCard" HeaderText="IdCard" SortExpression="IdCard" />
                    <asp:BoundField DataField="InsertTime" HeaderText="InsertTime" SortExpression="InsertTime" />
                    <asp:BoundField DataField="LastUpdateTime" HeaderText="LastUpdateTime" SortExpression="LastUpdateTime" />
                    <asp:BoundField DataField="DataFrom" HeaderText="DataFrom" SortExpression="DataFrom" />
                    <asp:BoundField DataField="FeeItems" HeaderText="FeeItems" SortExpression="FeeItems" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MathRoleAuthorConnectionString %>" SelectCommand="SELECT * FROM [Math_CWC]"></asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
