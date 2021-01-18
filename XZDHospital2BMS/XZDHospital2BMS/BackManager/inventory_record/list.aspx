<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="XZDHospital2BMS.BackManager.inventory_record.list" %>

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

            <div class="wrapper-btn col-sm-4">
              <asp:HyperLink runat="server" ID="hlBackContract"
                Target="_self" Text="返回盘点单" CssClass="btn btn-xs btn-success" />
              <asp:Button runat="server" ID="btnExportExcel" Text="导出Excel表格"
                CssClass="btn btn-xs btn-info" OnClick="btnExportExcel_Click" />
              <asp:HyperLink runat="server" ID="hlDownloadExcel" Text="下载此Excel"
                CssClass="btn btn-xs btn-info" Visible="false" />
              <asp:Button runat="server" ID="btnClearZero" Text="将所有库存清零"
                CssClass="btn btn-xs btn-info" OnClick="btnClearZero_Click"
                OnClientClick="return confirm('将次盘点单下所有库存清零，确定要执行此操作？');" />
            </div>

            <div class="wrapper-query col-sm-8">

              <div class="form-group">
                <label for="tbProductName" class="col-sm-4 control-label">货品名称：</label>
                <div class="col-sm-4">
                  <input runat="server" id="tbProductName" type="text"
                    class="form-control" placeholder="输入货品通用名称及剂型...">
                </div>
                <div class="col-sm-4">
                  <asp:Button runat="server" ID="btnQuery" class="btn btn-success btn-sm"
                    Text="查询" OnClientClick="return checkNotNull();" OnClick="btnQuery_Click" />
                  <asp:Button runat="server" ID="btnShowList" Text="查看所有"
                    CssClass="btn btn-success btn-sm" OnClick="btnShowList_Click" />
                </div>
              </div>

            </div>

          </div>

          <div class="wrapper-gvshow table-responsive">

            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
              OnRowDataBound="gvShow_RowDataBound" OnRowEditing="gvShow_RowEditing"
              OnRowUpdating="gvShow_RowUpdating" OnRowCancelingEdit="gvShow_RowCancelingEdit"
              CssClass="table table-condensed">
              <RowStyle BackColor="#e6eaee" />
              <AlternatingRowStyle BackColor="#f5f5f5" />
              <Columns>

                <asp:TemplateField HeaderText="通用名称及剂型">
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblId" Text='<%# Eval("id").ToString() %>' Visible="false" />
                    <asp:Label runat="server" ID="lblGoodsId" Text='<%# Eval("id_goods").ToString() %>' Visible="false" />
                    <asp:HyperLink runat="server" ID="hlProductName" Target="_blank" Text='<%# Eval("name_product").ToString() %>' />
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

                <asp:TemplateField HeaderText="盘点数">
                  <ItemStyle Width="80px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblAmountReal"
                      Text='<%# Eval("amount_real", "{0:f2}") %>' />
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:TextBox runat="server" ID="tbInventoryAmountReal"
                      Text='<%# Eval("amount_real", "{0:f2}") %>'
                      onkeyup="if(isNaN(value))execCommand('undo')"
                      onafterpaste="if(isNaN(value))execCommand('undo')"
                      CssClass="form-control" Width="60" MaxLength="12" />
                  </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="修改数">
                  <ItemStyle Width="80px" />
                  <ItemTemplate>
                    <asp:Label runat="server" ID="lblAmountShow"
                      Text='<%# Eval("amount_show", "{0:f2}") %>' />
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:TextBox runat="server" ID="tbInventoryAmountShow"
                      Text='<%# Eval("amount_show", "{0:f2}") %>'
                      onkeyup="if(isNaN(value))execCommand('undo')"
                      onafterpaste="if(isNaN(value))execCommand('undo')"
                      CssClass="form-control" Width="60" MaxLength="12" />
                  </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="操作">
                  <ItemStyle Width="80px" />
                  <ItemTemplate>
                    <asp:Button runat="server" ID="btnEdit" CommandName="Edit"
                      CssClass="btn btn-warning btn-xs" Text="修改" />
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:LinkButton runat="server" ID="lbtnUpdate" Text="更新"
                      CssClass="btn btn-info btn-xs" CommandName="Update" />
                    <asp:LinkButton runat="server" ID="lbtnCancel" Text="取消"
                      CssClass="btn btn-warning btn-xs" CommandName="Cancel" />
                  </EditItemTemplate>
                </asp:TemplateField>

              </Columns>

            </asp:GridView>

          </div>

          <div class="wrapper-info">

            <div class="wrapper-pager col-sm-7">
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

            <div class="wrapper-price col-sm-5">
              <p>
                库存：<asp:Label runat="server" ID="lblPriceTotalStock" CssClass="red" />
                盘点：<asp:Label runat="server" ID="lblPriceTotalInventoryReal" CssClass="red" />
                <asp:Label runat="server" ID="lblPriceTotalInventoryShow" Visible="false" />
              </p>
            </div>

          </div>

        </div>
      </div>
    </div>

  </form>
  <script src="/static/js/lib/jquery-1.12.4.min.js"></script>
  <script src="/static/js/lib/bootstrap.min.js"></script>
</body>
</html>
