<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm7.aspx.cs" Inherits="MAZIKONG.WebForm7" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <script src="Scripts/jquery-1.9.1.min.js"></script>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.js"></script>
    <style>
        body {
        padding:10px;
        
        }
        .a1 {
        margin-top:10px;
        margin-bottom:10px;
        overflow:auto;
        }
    </style>
    <script>
        function GetQueryValue(queryName) {
            var query = decodeURI(window.location.search.substring(1));
            var vars = query.split("&");
            for (var i = 0; i < vars.length; i++) {
                var pair = vars[i].split("=");
                if (pair[0] == queryName) { return pair[1]; }
            }
            return null;
        }

        $(document).ready(function () {
            $('#btnOutcel').click(function () {
                var Name = GetQueryValue('Name');
                window.open('Admin/AnalySISS/GetDetailExcel?Name='+Name);
            })
            
        })

        
    </script>
</head>
<body>
    <div  class="text-left a1">
        <a class="btn btn-primary btn-sm" id="btnOutcel" style="float:right">
        <i class="glyphicon glyphicon-save-file"></i>
        导出EXCEL
    </a>
    </div>
    <form id="form1" runat="server">
        <div>
            <asp:GridView CssClass="table"
                ID="GridView1"
                runat="server"
                CellPadding="4"
                ForeColor="#333333"
                GridLines="None"
                AutoGenerateColumns="False"
                >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="Index" HeaderText="序号" />
                    <asp:BoundField DataField="IdCard" HeaderText="身份证号码" />
                    <asp:BoundField DataField="StudentName" HeaderText="学生姓名" />
                    <asp:BoundField DataField="StudentNumber" HeaderText="考生号" />
                    <asp:BoundField DataField="DictValue" HeaderText="字典值" />
                    <asp:BoundField DataField="DictRemark" HeaderText="字典备注" />
                    <asp:BoundField DataField="zhanzhang" HeaderText="站长" />
                    <asp:BoundField DataField="zhanzhangdaqu" HeaderText="站长大区" />
                    <asp:BoundField DataField="zhanzhangquyu" HeaderText="站长区域" />
                    <asp:BoundField DataField="GrandSchool" HeaderText="毕业学校" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
