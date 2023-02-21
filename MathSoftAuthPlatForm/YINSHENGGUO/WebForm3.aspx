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
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="CWCId" DataSourceID="SqlDataSource1" EmptyDataText="数据为空">
                <Columns>
                    <asp:BoundField DataField="CWCId" HeaderText="CWCId" ReadOnly="True" SortExpression="CWCId" />
                    <asp:BoundField DataField="StudentName" HeaderText="StudentName" SortExpression="StudentName" />
                    <asp:BoundField DataField="IdCard" HeaderText="IdCard" SortExpression="IdCard" />
                    <asp:BoundField DataField="InsertTime" HeaderText="InsertTime" SortExpression="InsertTime" />
                    <asp:BoundField DataField="LastUpdateTime" HeaderText="LastUpdateTime" SortExpression="LastUpdateTime" />
                    <asp:BoundField DataField="DataFrom" HeaderText="DataFrom" SortExpression="DataFrom" />
                    <asp:BoundField DataField="FeeItems" HeaderText="FeeItems" SortExpression="FeeItems" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MathRoleAuthorConnectionString %>" SelectCommand="SELECT * FROM [Math_CWC]"></asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
