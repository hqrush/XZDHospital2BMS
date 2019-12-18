﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="XZDHospital2BMS.BackManager.inventory_record.list" %>

<%@ Register Src="~/BackManager/wucHeader.ascx" TagPrefix="wuc" TagName="wucHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>欢迎使用信州区第二人民医院后台管理系统 盘点货品列表</title>
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
            <div class="wrapper-btn col-sm-6">
              <asp:HyperLink runat="server" ID="hlBackContract"
                Target="_self" Text="返回盘点单" CssClass="btn btn-sm btn-success" />
              <asp:HyperLink runat="server" ID="hlAddNew"
                Target="_self" Text="添加盘点货品" CssClass="btn btn-sm btn-success" />
              <button type="button" id="btnExportExcel" class="btn btn-info btn-sm">
                <span class="glyphicon glyphicon-print" />导出Excel表格
              </button>
              <button type="button" id="btnPrint" class="btn btn-info btn-sm">
                <span class="glyphicon glyphicon-print" />打印清单
              </button>
            </div>
            <div class="wrapper-info col-sm-6">
              <p>总金额（单位：元）：<asp:Label runat="server" ID="lblPriceTotal" CssClass="red" /></p>
            </div>
          </div>

          <div class="wrapper-gvshow table-responsive">

            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
              OnRowDataBound="gvShow_RowDataBound" CssClass="table table-condensed">
              <RowStyle BackColor="#e6eaee" />
              <AlternatingRowStyle BackColor="#f5f5f5" />
              <Columns>

                <asp:TemplateField HeaderText="通用名称及剂型">
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblGoodsId"
                      Text='<%# Eval("id_goods").ToString() %>' Visible="false" />
                    <asp:HyperLink runat="server" ID="hlProductName" Target="_blank"
                      Text='<%# Eval("name_product").ToString() %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="厂家产地">
                  <ItemStyle Width="180px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblFactoryName"
                      Text='<%# Eval("name_factory").ToString() %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="入库时间">
                  <ItemStyle Width="100px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblSignTime"
                      Text='<%# Eval("time_sign", "{0:yyyy-MM-dd}") %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="规格">
                  <ItemStyle Width="100px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblType"
                      Text='<%# Eval("type").ToString() %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="批号/序列号">
                  <ItemStyle Width="100px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblBatchNumber"
                      Text='<%# Eval("batch_number").ToString() %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="有效期至">
                  <ItemStyle Width="100px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblValidityPeriod"
                      Text='<%# Eval("validity_period", "{0:yyyy-MM-dd}") %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="单价">
                  <ItemStyle Width="80px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblPriceUnit"
                      Text='<%# Eval("price_unit", "{0:f2}") %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="库存">
                  <ItemStyle Width="80px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblAmountStock"
                      Text='<%# Eval("amount_stock", "{0:f2}") %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="盘点数量">
                  <ItemStyle Width="80px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblAmountReal"
                      Text='<%# Eval("amount_real", "{0:f2}") %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="操作">
                  <ItemStyle Width="50px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblId" Text='<%# Eval("id").ToString() %>' Visible="false" />
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
