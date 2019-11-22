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

          <div runat="server" id="divAlert" class="alert alert-info" role="alert">
            <h4>暂无数据！</h4>
          </div>

          <div class="wrapper-gvshow table-responsive">

            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
              OnRowDataBound="gvShow_RowDataBound" OnRowEditing="gvShow_RowEditing"
              OnRowDeleting="gvShow_RowDeleting" OnRowUpdating="gvShow_RowUpdating"
              OnRowCancelingEdit="gvShow_RowCancelingEdit"
              CssClass="table table-condensed">

              <Columns>

                <asp:TemplateField HeaderText="公司名称">
                  <ItemStyle Width="90px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblName"
                      Text='<%# Eval("name").ToString() %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="添加人">
                  <ItemStyle Width="80px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblAdminId"
                      Text='<%# Eval("id_admin").ToString() %>' />
                    <asp:Label runat="server" ID="lblRealName" />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="添加时间">
                  <ItemStyle Width="120px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblTimeCreate"
                      Text='<%# Eval("time_create").ToString() %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="删除">
                  <ItemStyle Width="40px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblIsDeleted"
                      Text='<%# Eval("is_deleted").ToString() %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                  <ItemStyle Width="30px" />
                  <ItemTemplate>
                    <asp:Button runat="server" ID="btnEdit" CssClass="btn btn-info btn-xs"
                      CommandName="Edit" Text="编辑" />
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:LinkButton runat="server" ID="lbtnUpdate" Text="更新" CommandName="Update" />
                    <asp:LinkButton runat="server" ID="lbtnCancel" Text="取消" CommandName="Cancel" />
                  </EditItemTemplate>
                </asp:TemplateField>

              </Columns>

            </asp:GridView>

          </div>

        </form>
      </div>
    </div>
  </div>

  <script src="/static/js/jquery-1.12.4.min.js"></script>
  <script src="/static/js/bootstrap.min.js"></script>
</body>
</html>
