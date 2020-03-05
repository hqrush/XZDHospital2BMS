<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="XZDHospital2BMS.BackManager.sales_goods.list" %>

<%@ Register Src="~/BackManager/wucHeader.ascx" TagPrefix="wuc" TagName="wucHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>欢迎使用信州区第二人民医院后台管理系统 入库货品列表</title>
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
  <form runat="server" id="formShow" class="form-horizontal">

    <wuc:wucHeader runat="server" ID="wucHeader" />

    <div class="container">

      <div class="row">
        <div class="col-lg-12">

          <div class="wrapper-op form-group">
            <div class="wrapper-btn col-sm-5">
              <asp:HyperLink runat="server" ID="hlBackContract"
                Target="_self" Text="返回列表" CssClass="btn btn-sm btn-success" />
              <asp:HyperLink runat="server" ID="hlAddNew"
                Target="_self" Text="添加货品" CssClass="btn btn-sm btn-warning" />
              <asp:Button runat="server" ID="btnExportExcel" Text="导出表格"
                CssClass="btn btn-sm btn-info" OnClick="btnExportExcel_Click" />
              <asp:HyperLink runat="server" ID="hlDownloadExcel" Text="下载表格"
                CssClass="btn btn-sm btn-info" Visible="false" />
            </div>
            <div class="wrapper-info col-sm-7">
              <p>
                <asp:Label runat="server" ID="lblCompanyName" />&nbsp;
                <asp:Label runat="server" ID="lblTimeCreate" />&nbsp;
                总金额（单位：元）：<asp:Label runat="server" ID="lblPriceTotal" CssClass="red" />
              </p>
            </div>
          </div>

          <div class="wrapper-gvshow table-responsive">

            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
              OnRowDataBound="gvShow_RowDataBound" CssClass="table table-condensed">
              <RowStyle BackColor="#e6eaee" />
              <AlternatingRowStyle BackColor="#f5f5f5" />
              <Columns>

                <asp:TemplateField HeaderText="选择">
                  <ItemTemplate>
                    <asp:CheckBox runat="server" ID="cbSelect" />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="通用名称及剂型">
                  <ItemTemplate>
                    <asp:HyperLink runat="server" ID="hlProductName" Target="_blank"
                      Text='<%# Eval("name_product").ToString() %>'
                      NavigateUrl='show.aspx?id=<%# Eval("id").ToString() %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="厂家产地">
                  <ItemStyle Width="240px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblFactoryName"
                      Text='<%# Eval("name_factory").ToString() %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="数量">
                  <ItemStyle Width="100px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblAmount"
                      Text='<%# Eval("amount", "{0:f2}") %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="单价">
                  <ItemStyle Width="100px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblPriceUnit"
                      Text='<%# Eval("price_unit", "{0:f2}") %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="批号/序列号">
                  <ItemStyle Width="130px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblBatchNumber"
                      Text='<%# Eval("batch_number").ToString() %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="有效期至">
                  <ItemStyle Width="110px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblValidityPeriod"
                      Text='<%# Eval("validity_period", "{0:yyyy-MM-dd}") %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="添加人">
                  <ItemStyle Width="70px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblAdminId"
                      Text='<%# Eval("id_admin").ToString() %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="操作">
                  <ItemStyle Width="150px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblId" Text='<%# Eval("id").ToString() %>' Visible="false" />
                    <asp:HyperLink runat="server" ID="hlShow" Target="_blank"
                      CssClass="btn btn-info btn-xs" Text="查看" />
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

        </div>
      </div>

      <div class="row">
        <div class="col-lg-12 wrapper-op-bottom">

          <div class="col-lg-7 wrapper-pager">
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

          <div class="col-lg-5 wrapper-trans">
            <asp:DropDownList runat="server" ID="ddlSalesContract" />
            <asp:Button runat="server" ID="btnTrans" CssClass="btn btn-xs btn-info"
              OnClick="btnTrans_Click" Text="转移选中的货品" />
          </div>

        </div>
      </div>

    </div>

  </form>
  <script src="/static/js/lib/jquery-1.12.4.min.js"></script>
  <script src="/static/js/lib/bootstrap.min.js"></script>
</body>
</html>
