<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs"
  Inherits="XZDHospital2BMS.BackManager.admin.edit" %>

<%@ Register Src="~/BackManager/wucHeader.ascx" TagPrefix="wuc" TagName="wucHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>管理员表单</title>
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

        <div class="panel panel-primary">
          <div class="panel-heading">
            <h3 class="panel-title">填写管理员资料表单</h3>
          </div>
          <div class="panel-body">

            <form runat="server" class="form-horizontal" role="form">

              <div class="form-group">
                <label for="tbUsername" class="col-sm-2 control-label">用户名：</label>
                <div class="col-sm-5">
                  <input runat="server" id="tbUsername" type="text"
                    class="form-control" disabled>
                </div>
                <div class="col-sm-5">
                  <asp:Label runat="server" ID="UsernameError" />
                </div>
              </div>
              <div class="form-group">
                <label for="tbPassword" class="col-sm-2 control-label">密码：</label>
                <div class="col-sm-5">
                  <input runat="server" id="tbPassword" type="password"
                    class="form-control">
                </div>
                <div class="col-sm-5">
                  <asp:Label runat="server" ID="PasswordError" Text="如果不修改密码则此处为空！" />
                </div>
              </div>
              <div class="form-group">
                <label for="tbPassword2" class="col-sm-2 control-label">密码确认：</label>
                <div class="col-sm-5">
                  <input runat="server" id="tbPassword2" type="password"
                    class="form-control" placeholder="再输入一次确认密码...">
                </div>
                <div class="col-sm-5">
                  <asp:Label runat="server" ID="Password2Error" />
                </div>
              </div>
              <div class="form-group">
                <label for="tbRealName" class="col-sm-2 control-label">真实姓名：</label>
                <div class="col-sm-10">
                  <input runat="server" id="tbRealName" type="text"
                    class="form-control" placeholder="输入真实姓名...">
                </div>
                <div class="col-sm-5">
                  <asp:Label runat="server" ID="RealNameError" />
                </div>
              </div>
              <div class="form-group">
                <label for="tbMobilePhone" class="col-sm-2 control-label">手机号码：</label>
                <div class="col-sm-10">
                  <input runat="server" id="tbMobilePhone" type="text"
                    class="form-control" placeholder="输入手机号码...">
                </div>
                <div class="col-sm-5">
                  <asp:Label runat="server" ID="MobilePhoneError" />
                </div>
              </div>

              <asp:Panel runat="server" ID="pnlPurviews" CssClass="panel panel-warning">

                <div class="panel-heading">
                  <div class="panel-title">
                    <h4>权限设置</h4>
                  </div>
                </div>

                <div class="panel-body">

                  <div class="panel panel-info">
                    <div class="panel-heading">
                      <div class="panel-title">
                        <label class="checkbox-inline">
                          <input runat="server" type="checkbox" id="cbSalesContract" value="SalesContract">入库单管理</label>
                      </div>
                    </div>
                    <div class="panel-body">
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbSalesContract_Add" value="SalesContract_add">添加入库单</label>
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbSalesContract_Del" value="SalesContract_del">删除入库单</label>
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbSalesContract_Update" value="SalesContract_update">修改入库单</label>
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbSalesContract_Show" value="SalesContract_show">查看入库单</label>
                    </div>
                  </div>

                  <div class="panel panel-info">
                    <div class="panel-heading">
                      <div class="panel-title">
                        <label class="checkbox-inline">
                          <input runat="server" type="checkbox" id="cbSalesGoods" value="SalesGoods">入库药品及器械管理</label>
                      </div>
                    </div>
                    <div class="panel-body">
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbSalesGoods_Add" value="SalesGoods_add">添加入库药品及器械</label>
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbSalesGoods_Del" value="SalesGoods_del">删除入库药品及器械</label>
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbSalesGoods_Update" value="SalesGoods_update">修改入库药品及器械</label>
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbSalesGoods_Show" value="SalesGoods_show">查看入库药品及器械</label>
                    </div>
                  </div>

                  <div class="panel panel-info">
                    <div class="panel-heading">
                      <div class="panel-title">
                        <label class="checkbox-inline">
                          <input runat="server" type="checkbox" id="cbCheckoutContract" value="CheckoutContract">出库单管理</label>
                      </div>
                    </div>
                    <div class="panel-body">
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbCheckoutContract_Add" value="CheckoutContract_add">添加出库单</label>
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbCheckoutContract_Del" value="CheckoutContract_del">删除出库单</label>
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbCheckoutContract_Update" value="CheckoutContract_update">修改出库单</label>
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbCheckoutContract_Show" value="CheckoutContract_show">查看出库单</label>
                    </div>
                  </div>

                  <div class="panel panel-info">
                    <div class="panel-heading">
                      <div class="panel-title">
                        <label class="checkbox-inline">
                          <input runat="server" type="checkbox" id="cbCheckoutRecord" value="CheckoutRecord">出库药品及器械管理</label>
                      </div>
                    </div>
                    <div class="panel-body">
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbCheckoutRecord_Add" value="CheckoutRecord_add">添加出库药品及器械</label>
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbCheckoutRecord_Del" value="CheckoutRecord_del">删除出库药品及器械</label>
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbCheckoutRecord_Update" value="CheckoutRecord_update">修改出库药品及器械</label>
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbCheckoutRecord_Show" value="CheckoutRecord_show">查看出库药品及器械</label>
                    </div>
                  </div>

                  <div class="panel panel-info">
                    <div class="panel-heading">
                      <div class="panel-title">
                        <label class="checkbox-inline">
                          <input runat="server" type="checkbox" id="cbInventoryContract" value="InventoryContract">盘点单管理</label>
                      </div>
                    </div>
                    <div class="panel-body">
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbInventoryContract_Add" value="InventoryContract_add">添加盘点单</label>
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbInventoryContract_Del" value="InventoryContract_del">删除盘点单</label>
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbInventoryContract_Update" value="InventoryContract_update">修改盘点单</label>
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbInventoryContract_Show" value="InventoryContract_show">查看盘点单</label>
                    </div>
                  </div>

                  <div class="panel panel-info">
                    <div class="panel-heading">
                      <div class="panel-title">
                        <label class="checkbox-inline">
                          <input runat="server" type="checkbox" id="cbInventoryRecord" value="InventoryRecord">盘点药品及器械管理</label>
                      </div>
                    </div>
                    <div class="panel-body">
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbInventoryRecord_Add" value="InventoryRecord_add">添加盘点药品及器械</label>
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbInventoryRecord_Del" value="InventoryRecord_del">删除盘点药品及器械</label>
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbInventoryRecord_Update" value="InventoryRecord_update">修改盘点药品及器械</label>
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbInventoryRecord_Show" value="InventoryRecord_show">查看盘点药品及器械</label>
                    </div>
                  </div>

                  <div class="panel panel-info">
                    <div class="panel-heading">
                      <div class="panel-title">
                        <label class="checkbox-inline">
                          <input runat="server" type="checkbox" id="cbSysAdmin" value="SysAdmin">管理员管理</label>
                      </div>
                    </div>
                    <div class="panel-body">
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbSysAdmin_Add" value="SysAdmin_add">添加管理员</label>
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbSysAdmin_Del" value="SysAdmin_del">删除管理员</label>
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbSysAdmin_Update" value="SysAdmin_update">修改管理员</label>
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbSysAdmin_Show" value="SysAdmin_show">查看管理员</label>
                    </div>
                  </div>

                  <div class="panel panel-info">
                    <div class="panel-heading">
                      <div class="panel-title">
                        <label class="checkbox-inline">
                          <input runat="server" type="checkbox" id="cbSalesCompany" value="SalesCompany">医药销售公司管理</label>
                      </div>
                    </div>
                    <div class="panel-body">
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbSalesCompany_Add" value="SalesCompany_add">添加医药销售公司</label>
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbSalesCompany_Del" value="SalesCompany_del">删除医药销售公司</label>
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbSalesCompany_Update" value="SalesCompany_update">修改医药销售公司</label>
                      <label class="checkbox-inline">
                        <input runat="server" type="checkbox" id="cbSalesCompany_Show" value="SalesCompany_show">查看医药销售公司</label>
                    </div>
                  </div>

                </div>

              </asp:Panel>

              <div class="form-group">
                <div class="col-sm-offset-5 col-sm-7">
                  <asp:Button runat="server" ID="btnEdit" Text="确认提交"
                    CssClass="btn btn-primary" OnClick="btnEdit_Click" />
                </div>
              </div>

            </form>

          </div>
        </div>

      </div>
    </div>
  </div>

  <script src="/static/js/jquery-1.12.4.min.js"></script>
  <script src="/static/js/bootstrap.min.js"></script>
  <script type="text/javascript" src="/static/js/check-form-admin.js"></script>

</body>
</html>
