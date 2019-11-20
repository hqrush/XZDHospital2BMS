<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="form.aspx.cs"
  Inherits="XZDHospital2BMS.BackManager.admin.form" %>

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
                <div class="col-sm-10">
                  <input runat="server" class="form-control" id="tbUsername" type="text" placeholder="输入登录时填写的用户名...">
                </div>
              </div>
              <div class="form-group">
                <label for="tbPassword" class="col-sm-2 control-label">密码：</label>
                <div class="col-sm-10">
                  <input runat="server" class="form-control" id="tbPassword" type="password" placeholder="输入登录时填写的密码...">
                </div>
              </div>
              <div class="form-group">
                <label for="tbPassword2" class="col-sm-2 control-label">密码确认：</label>
                <div class="col-sm-10">
                  <input runat="server" class="form-control" id="tbPassword2" type="password" placeholder="再输入一次确认密码...">
                </div>
              </div>
              <div class="form-group">
                <label for="tbRealName" class="col-sm-2 control-label">真实姓名：</label>
                <div class="col-sm-10">
                  <input runat="server" class="form-control" id="tbRealName" type="text" placeholder="输入真实姓名...">
                </div>
              </div>
              <div class="form-group">
                <label for="tbIdCard" class="col-sm-2 control-label">身份证号：</label>
                <div class="col-sm-10">
                  <input runat="server" class="form-control" id="tbIdCard" type="text" placeholder="输入身份证号...">
                </div>
              </div>
              <div class="form-group">
                <label for="tbMobilePhone" class="col-sm-2 control-label">手机号码：</label>
                <div class="col-sm-10">
                  <input runat="server" class="form-control" id="tbMobilePhone" type="text" placeholder="输入手机号码...">
                </div>
              </div>

              <label for="name">设置权限</label>

              <div class="panel panel-info">
                <div class="panel-heading">
                  <h3 class="panel-title">入库单管理</h3>
                </div>
                <div class="panel-body">
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox1" value="option1">添加入库单</label>
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox2" value="option2">删除入库单</label>
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox3" value="option3">修改入库单</label>
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox3" value="option3">查看入库单</label>
                </div>
              </div>

              <div class="panel panel-info">
                <div class="panel-heading">
                  <h3 class="panel-title">入库药品及器械管理</h3>
                </div>
                <div class="panel-body">
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox1" value="option1">添加入库药品及器械</label>
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox2" value="option2">删除入库药品及器械</label>
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox3" value="option3">修改入库药品及器械</label>
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox3" value="option3">查看入库药品及器械</label>
                </div>
              </div>

              <div class="panel panel-info">
                <div class="panel-heading">
                  <h3 class="panel-title">出库单管理</h3>
                </div>
                <div class="panel-body">
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox1" value="option1">添加出库单</label>
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox2" value="option2">删除出库单</label>
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox3" value="option3">修改出库单</label>
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox3" value="option3">查看出库单</label>
                </div>
              </div>

              <div class="panel panel-info">
                <div class="panel-heading">
                  <h3 class="panel-title">出库药品及器械管理</h3>
                </div>
                <div class="panel-body">
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox1" value="option1">添加出库药品及器械</label>
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox2" value="option2">删除出库药品及器械</label>
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox3" value="option3">修改出库药品及器械</label>
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox3" value="option3">查看出库药品及器械</label>
                </div>
              </div>

              <div class="panel panel-info">
                <div class="panel-heading">
                  <h3 class="panel-title">盘点单管理</h3>
                </div>
                <div class="panel-body">
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox1" value="option1">添加盘点单</label>
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox2" value="option2">删除盘点单</label>
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox3" value="option3">修改盘点单</label>
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox3" value="option3">查看盘点单</label>
                </div>
              </div>

              <div class="panel panel-info">
                <div class="panel-heading">
                  <h3 class="panel-title">盘点药品及器械管理</h3>
                </div>
                <div class="panel-body">
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox1" value="option1">添加盘点药品及器械</label>
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox2" value="option2">删除盘点药品及器械</label>
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox3" value="option3">修改盘点药品及器械</label>
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox3" value="option3">查看盘点药品及器械</label>
                </div>
              </div>

              <div class="panel panel-info">
                <div class="panel-heading">
                  <h3 class="panel-title">系统设置</h3>
                </div>
                <div class="panel-body">
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox1" value="option1">添加管理员</label>
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox2" value="option2">删除管理员</label>
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox3" value="option3">修改管理员</label>
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox3" value="option3">查看管理员</label>
                </div>
              </div>

              <div class="panel panel-info">
                <div class="panel-heading">
                  <h3 class="panel-title">医药销售公司管理</h3>
                </div>
                <div class="panel-body">
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox1" value="option1">添加医药销售公司</label>
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox2" value="option2">删除医药销售公司</label>
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox3" value="option3">修改医药销售公司</label>
                  <label class="checkbox-inline">
                    <input type="checkbox" id="inlineCheckbox3" value="option3">查看医药销售公司</label>
                </div>
              </div>

              <div class="form-group">
                <div class="col-sm-offset-5 col-sm-7">
                  <asp:Button runat="server" ID="btnSubmit" Text="确认提交"
                    CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
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
</body>
</html>
