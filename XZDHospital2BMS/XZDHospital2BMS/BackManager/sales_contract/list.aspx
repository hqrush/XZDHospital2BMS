<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="XZDHospital2BMS.BackManager.sales_contract.list" %>

<%@ Register Src="~/BackManager/wucHeader.ascx" TagPrefix="wuc" TagName="wucHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>欢迎使用信州区第二人民医院后台管理系统 入库单列表</title>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <link rel="stylesheet" href="/static/css/lib/bootstrap.min.css" />
  <link rel="stylesheet" href="/static/css/lib/bootstrap-theme.min.css" />
  <link rel="stylesheet" href="/static/css/common.css" />
  <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
     <![endif]-->
</head>
<body>
  <form id="formShow" runat="server">

    <wuc:wucHeader runat="server" ID="wucHeader" />

    <div class="container">
      <div class="row">
        <div class="col-lg-12">

          <div class="wrapper-add">
            <a href="add.aspx" class="btn btn-info">添加入库单</a>
          </div>

          <div class="wrapper-gvshow table-responsive">

            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
              OnRowDataBound="gvShow_RowDataBound" CssClass="table table-condensed">
              <RowStyle BackColor="#e6eaee" />
              <AlternatingRowStyle BackColor="#f5f5f5" />
              <Columns>

                <asp:TemplateField HeaderText="销售公司">
                  <ItemStyle Width="180px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblCompanyId"
                      Text='<%# Eval("id_company").ToString() %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="填写人">
                  <ItemStyle Width="80px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblAdminId"
                      Text='<%# Eval("id_admin").ToString() %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="入库时间">
                  <ItemStyle Width="90px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblTimeSign"
                      Text='<%# Eval("time_sign", "{0:yyyy-MM-dd}") %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="备注">
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblComment"
                      Text='<%# Eval("comment").ToString() %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="货品总数">
                  <ItemStyle Width="80px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblId" Visible="false"
                      Text='<%# Eval("id").ToString() %>' />
                    <asp:Label runat="server" ID="lblGoodsAmount" />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="货品总价">
                  <ItemStyle Width="100px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblPriceTotal" />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="货品操作">
                  <ItemStyle Width="100px" />
                  <ItemTemplate>
                    <asp:Button runat="server" ID="btnGoodsList" CssClass="btn btn-info btn-xs" Text="查看"
                      OnCommand="OP_Command" CommandName="ShowGoodsList" CommandArgument='<%# Eval("id") %>' />
                    <asp:Button runat="server" ID="btnGoodsAdd" CssClass="btn btn-info btn-xs" Text="添加"
                      OnCommand="OP_Command" CommandName="AddGoods" CommandArgument='<%# Eval("id") %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="入库单操作">
                  <ItemStyle Width="100px" />
                  <ItemTemplate>
                    <asp:Button runat="server" ID="btnEdit" CssClass="btn btn-info btn-xs" Text="编辑"
                      OnCommand="OP_Command" CommandName="edit" CommandArgument='<%# Eval("id") %>' />
                    <asp:Button runat="server" ID="btnDel" CssClass="btn btn-warning btn-xs" Text="删除"
                      OnClientClick="return confirm('确定要删除？');"
                      OnCommand="OP_Command" CommandName="del" CommandArgument='<%# Eval("id") %>' />
                  </ItemTemplate>
                </asp:TemplateField>

              </Columns>

            </asp:GridView>

          </div>

          <div class="wrapper-pager">
            <span>共有<asp:Label ID="lblRecordCount" runat="server" />条记录，
                当前页数：<asp:Label ID="lblCurentPage" runat="server" Text="1" />，
                总页数：<asp:Label ID="lblPageCount" runat="server" />
            </span>
            <asp:LinkButton ID="lbtnFirst" runat="server" OnClick="lbtnFirst_Click">首页</asp:LinkButton>
            <asp:LinkButton ID="lbtnPrev" runat="server" OnClick="lbtnPrev_Click">上一页</asp:LinkButton>
            <asp:LinkButton ID="lbtnNext" runat="server" OnClick="lbtnNext_Click">下一页</asp:LinkButton>
            <asp:LinkButton ID="lbtnLast" runat="server" OnClick="lbtnLast_Click">尾页</asp:LinkButton>
            <asp:TextBox runat="server" ID="tbPageNum" TextMode="Number" Width="40" />
            <asp:Button runat="server" ID="btnJumpTo" CssClass="btn btn-xs btn-info"
              Text="跳转至" OnClick="btnJumpTo_Click" />
          </div>

        </div>
      </div>
    </div>

  </form>
  <script src="/static/js/lib/jquery-1.12.4.min.js"></script>
  <script src="/static/js/lib/bootstrap.min.js"></script>
</body>
</html>
