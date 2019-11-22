<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs"
  Inherits="XZDHospital2BMS.BackManager.admin.list" %>

<%@ Register Src="~/BackManager/wucHeader.ascx" TagPrefix="wuc" TagName="wucHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>欢迎使用信州区第二人民医院后台管理系统 管理员列表</title>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <link rel="stylesheet" href="/static/css/bootstrap.min.css" />
  <link rel="stylesheet" href="/static/css/bootstrap-theme.min.css" />
  <link rel="stylesheet" href="/static/css/common.css" />
  <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
     <![endif]-->
</head>
<body>

  <wuc:wucHeader runat="server" ID="wucHeader" />

  <div class="container">
    <div class="row">
      <div class="col-lg-12">
        <form id="formShow" runat="server">
          <div class="wrapper-gvshow table-responsive">

            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
              OnRowDataBound="gvShow_RowDataBound" CssClass="table table-condensed">
              <RowStyle BackColor="#e6eaee" />
              <AlternatingRowStyle BackColor="#f5f5f5" />
              <Columns>

                <asp:TemplateField HeaderText="用户名">
                  <ItemStyle Width="60px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblUsername"
                      Text='<%# Eval("username").ToString() %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="真实姓名">
                  <ItemStyle Width="60px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblRealname"
                      Text='<%# Eval("real_name").ToString() %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="手机号码">
                  <ItemStyle Width="80px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblMobilePhone"
                      Text='<%# Eval("mobile_phone").ToString() %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="添加时间">
                  <ItemStyle Width="120px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblTimeAdd"
                      Text='<%# Eval("time_add").ToString() %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="最后登录">
                  <ItemStyle Width="120px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblTimeLastLogin"
                      Text='<%# Eval("time_last_login").ToString() %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="是否启用">
                  <ItemStyle Width="40px" />
                  <ItemTemplate>
                    <asp:ImageButton runat="server" ID="imgbtnEnabled" Width="16"
                      AlternateText='<%# Eval("enabled").ToString() %>'
                      OnCommand="OP_Command" CommandName="ChangeEnabled"
                      CommandArgument='<%# Eval("id") %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="是否删除">
                  <ItemStyle Width="40px" />
                  <ItemTemplate>
                    <asp:ImageButton runat="server" ID="imgbtnIsDeleted" Width="16"
                      AlternateText='<%# Eval("is_deleted").ToString() %>'
                      OnCommand="OP_Command" CommandName="ChangeIsDeleted"
                      CommandArgument='<%# Eval("id") %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="操作">
                  <ItemStyle Width="150px" />
                  <ItemTemplate>
                    <asp:Button runat="server" ID="btnEdit" CssClass="btn btn-info btn-xs" Text="编辑"
                      OnCommand="OP_Command" CommandName="Edit" CommandArgument='<%# Eval("id") %>' />
                    <asp:Button runat="server" ID="btnDel" CssClass="btn btn-warning btn-xs" Text="删除"
                      OnClientClick="return confirm('确定要删除？');"
                      OnCommand="OP_Command" CommandName="Delete" CommandArgument='<%# Eval("id") %>' />
                  </ItemTemplate>
                </asp:TemplateField>

              </Columns>

            </asp:GridView>

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
        </form>
      </div>
    </div>
  </div>

  <script src="/static/js/jquery-1.12.4.min.js"></script>
  <script src="/static/js/bootstrap.min.js"></script>
</body>
</html>
