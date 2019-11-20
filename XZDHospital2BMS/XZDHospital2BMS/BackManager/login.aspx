<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs"
  Inherits="XZDHospital2BMS.BackManager.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>请登录——欢迎使用信州区第二人民医院后台管理系统</title>
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
  <div class="container">
    <div class="row">
      <div class="col-xs-12">

        <form runat="server" class="form-horizontal" role="form">
          <div class="form-group">
            <label for="tbUsername" class="col-sm-2 control-label">登录名称</label>
            <div class="col-sm-10">
              <input runat="server" type="text" class="form-control" id="tbUsername" placeholder="请输入登录名称...">
            </div>
          </div>
          <div class="form-group">
            <label for="tbPassword" class="col-sm-2 control-label">登录密码</label>
            <div class="col-sm-10">
              <input runat="server" type="password" class="form-control" id="tbPassword" placeholder="请输入登录密码...">
            </div>
          </div>
          <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
              <div class="checkbox">
                <label>
                  <input runat="server" type="checkbox" id="cbRememberMe">记住登录状态
                </label>
              </div>
            </div>
          </div>
          <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
              <asp:Button runat="server" ID="btnLogin" Text="登录"
                CssClass="btn btn-primary" OnClick="btnLogin_Click" />
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
