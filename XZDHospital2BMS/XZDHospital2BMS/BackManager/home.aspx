<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs"
  Inherits="XZDHospital2BMS.BackManager._home" %>

<%@ Register Src="~/BackManager/wucHeader.ascx" TagPrefix="wuc" TagName="wucHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>欢迎使用信州区第二人民医院后台管理系统</title>
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
        <div>
          <h4>
            <asp:Label runat="server" ID="lblAdminId" /></h4>
          <form runat="server">
            <asp:Button runat="server" ID="btnNetButtonTest" CssClass="btn btn-warning" Text="net按钮点击调研ajax请求" />
            <button id="btnButtonTest" class="btn btn-danger">html button 按钮点击调用ajax请求</button>
            <input id="btnInputTest" type="button" class="btn btn-primary" value="html input 按钮点击调用ajax请求" />
          </form>
        </div>
      </div>
    </div>
  </div>

  <script src="/static/js/jquery-1.12.4.min.js"></script>
  <script src="/static/js/bootstrap.min.js"></script>
  <script>
    $(function () {
      $("#btnInputTest").click(function () {
        $.ajax({
          //要用post方式   
          type: "Post",
          //方法所在页面和方法名   
          url: "home.aspx/SayHello",
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          success: function (data) {
            //返回的数据用data.d获取内容   
            alert(data.d);
          },
          error: function (err) {
            alert(err);
          }
        });
        //禁用按钮的提交   
        return false;
      });
    });
  </script>
</body>
</html>
