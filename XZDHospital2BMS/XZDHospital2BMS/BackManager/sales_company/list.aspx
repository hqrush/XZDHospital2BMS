<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs"
  Inherits="XZDHospital2BMS.BackManager.sales_company.list" %>

<%@ Register Src="~/BackManager/wucHeader.ascx" TagPrefix="wuc" TagName="wucHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>医药器械销售公司列表</title>
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

          <div class="wrapper-add col-sm-5">

            <div class="form-group">
              <label for="tbCompanyName">公司名称：（可批量添加，一行为一条记录）</label>
              <textarea runat="server" id="tbCompanyName" class="form-control" width="150" rows="30"></textarea><br />
              <asp:Button runat="server" ID="btnAdd" Text="确定添加"
                CssClass="btn btn-primary" OnClick="btnAdd_Click" />
              <input type="button" value="清空" class="btn btn-warning" />
            </div>

          </div>

          <div class="wrapper-gvshow col-sm-7">

            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
              OnRowDataBound="gvShow_RowDataBound" OnRowEditing="gvShow_RowEditing"
              OnRowUpdating="gvShow_RowUpdating" OnRowCancelingEdit="gvShow_RowCancelingEdit"
              CssClass="table table-condensed">
              <RowStyle BackColor="#e6eaee" />
              <AlternatingRowStyle BackColor="#f5f5f5" />
              <Columns>

                <asp:TemplateField HeaderText="公司名称">
                  <ItemStyle Width="90px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblName"
                      Text='<%# Eval("name").ToString() %>' />
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:TextBox runat="server" ID="tbName" Width="120px" MaxLength="20"
                      CssClass="form-control" Text='<%# Eval("name").ToString() %>' />
                  </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="添加人">
                  <ItemStyle Width="80px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblAdminId"
                      Text='<%# Eval("id_admin").ToString() %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="添加时间">
                  <ItemStyle Width="120px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblTimeCreate"
                      Text='<%# Eval("time_create").ToString() %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                  <ItemStyle Width="80px" />
                  <ItemTemplate>
                    <asp:Button runat="server" ID="btnEdit" CssClass="btn btn-info btn-xs"
                      CommandName="edit" Text="编辑" />
                    <asp:Button runat="server" ID="btnDel" CssClass="btn btn-warning btn-xs" Text="删除"
                      OnClientClick="return confirm('确定要删除？');"
                      OnCommand="OP_Command" CommandName="Delete" CommandArgument='<%# Eval("id") %>' />
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:Button runat="server" ID="btnUpdate" Text="更新" CommandName="Update" />
                    <asp:Button runat="server" ID="btnCancel" Text="取消" CommandName="Cancel" />
                  </EditItemTemplate>
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
