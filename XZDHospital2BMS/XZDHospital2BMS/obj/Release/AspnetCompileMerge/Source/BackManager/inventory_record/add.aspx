<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="add.aspx.cs" Inherits="XZDHospital2BMS.BackManager.inventory_record.add" %>

<%@ Register Src="~/BackManager/wucHeader.ascx" TagPrefix="wuc" TagName="wucHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>填写盘点货品</title>
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
  <form runat="server" class="form-horizontal" role="form">

    <wuc:wucHeader runat="server" ID="wucHeader" />

    <div class="container">
      <div class="row">
        <div class="col-lg-12">

          <div class="panel panel-primary">
            <div class="panel-heading">
              <h3 class="panel-title">查询货品</h3>
            </div>
            <div class="panel-body">

              <div class="form-group">
                <label for="tbProductName" class="col-sm-2 control-label">
                  <strong class="red">*</strong>货品名称：</label>
                <div class="col-sm-2">
                  <input runat="server" id="tbProductName" type="text"
                    class="form-control" placeholder="输入货品通用名称及剂型...">
                </div>
                <label for="tbFactoryName" class="col-sm-2 control-label">
                  <strong class="red">*</strong>厂家名称：</label>
                <div class="col-sm-2">
                  <input runat="server" id="tbFactoryName" type="text"
                    class="form-control" placeholder="输入厂家名称...">
                </div>
                <div class="col-sm-2">
                  <asp:Button runat="server" ID="btnQuery" class="btn btn-success btn-sm"
                    Text="查询" OnClientClick="return checkNotNull();" OnClick="btnQuery_Click" />
                </div>
                <div class="col-sm-2">
                  <asp:Button runat="server" ID="btnShowListAll" Text="查看盘点清单所有货品"
                    CssClass="btn btn-primary right" OnClick="btnShowListAll_Click" />
                </div>
              </div>

              <asp:Panel runat="server" ID="pnlInfo">
                <p>没有相关数据！</p>
              </asp:Panel>

              <div class="wrapper-gvshow table-responsive">

                <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                  OnRowDataBound="gvShow_RowDataBound" CssClass="table table-condensed">
                  <RowStyle BackColor="#e6eaee" />
                  <AlternatingRowStyle BackColor="#f5f5f5" />
                  <Columns>

                    <asp:TemplateField HeaderText="名称及剂型">
                      <ItemTemplate>
                        <asp:Label runat="server" ID="lblGoodsId" Visible="false"
                          Text='<%# Eval("id").ToString() %>' />
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

                    <asp:TemplateField HeaderText="规格">
                      <ItemStyle Width="150px" />
                      <ItemTemplate>
                        <asp:Label runat="server" ID="lblType"
                          Text='<%# Eval("type").ToString() %>' />
                      </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="单价">
                      <ItemStyle Width="60px" />
                      <ItemTemplate>
                        <asp:Label runat="server" ID="lblPriceUnit"
                          Text='<%# Eval("price_unit", "{0:f2}") %>' />
                      </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="入库时间">
                      <ItemStyle Width="100px" />
                      <ItemTemplate>
                        <asp:Label runat="server" ID="lblSalesContractTime"
                          Text='<%# Eval("time_sign", "{0:yyyy-MM-dd}") %>' />
                      </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="有效期至">
                      <ItemStyle Width="100px" />
                      <ItemTemplate>
                        <asp:Label runat="server" ID="lblValidityPeriod"
                          Text='<%# Eval("validity_period", "{0:yyyy-MM-dd}") %>' />
                      </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="入库量">
                      <ItemStyle Width="70px" />
                      <ItemTemplate>
                        <asp:Label runat="server" ID="lblAmountIn"
                          Text='<%# Eval("amount", "{0:f2}") %>' />
                      </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="计算库存">
                      <ItemStyle Width="70px" />
                      <ItemTemplate>
                        <asp:Label runat="server" ID="lblAmountReal" Text="0" />
                      </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="实时库存">
                      <ItemStyle Width="70px" />
                      <ItemTemplate>
                        <asp:Label runat="server" ID="lblAmountStock"
                          Text='<%# Eval("amount_stock", "{0:f2}") %>' />
                      </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="盘点库存">
                      <ItemStyle Width="70px" />
                      <ItemTemplate>
                        <asp:TextBox runat="server" ID="tbAmountFill" Text="0"
                          onkeyup="if(isNaN(value))execCommand('undo')"
                          onafterpaste="if(isNaN(value))execCommand('undo')"
                          CssClass="form-control" Width="80" MaxLength="12" />
                      </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="操作">
                      <ItemStyle Width="100px" />
                      <ItemTemplate>
                        <asp:Button runat="server" ID="btnAddGoods" CssClass="btn btn-warning btn-xs" Text="添加至盘点单"
                          OnCommand="OP_Command" CommandName="AddGoods" CommandArgument='<%# Eval("id") %>' />
                      </ItemTemplate>
                    </asp:TemplateField>

                  </Columns>

                </asp:GridView>

              </div>

            </div>
          </div>

        </div>
      </div>
    </div>

  </form>
  <script type="text/javascript" src="/static/js/lib/jquery-1.12.4.min.js"></script>
  <script type="text/javascript" src="/static/js/lib/bootstrap.min.js"></script>
  <script type="text/javascript" src="/static/js/check-form/inventory_record.js"></script>
</body>
</html>
