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

  <wuc:wucHeader runat="server" id="wucHeader" />

  <div class="container">
    <div class="row">
      <div class="col-lg-12">
        <div>
          <h1>我是标题1 h1. <small>我是副标题1 h1</small></h1>
          <h2>我是标题2 h2. <small>我是副标题2 h2</small></h2>
          <h3>我是标题3 h3. <small>我是副标题3 h3</small></h3>
          <h4>我是标题4 h4. <small>我是副标题4 h4</small></h4>
          <h5>我是标题5 h5. <small>我是副标题5 h5</small></h5>
          <h6>我是标题6 h6. <small>我是副标题6 h6</small></h6>
        </div>
      </div>
    </div>
  </div>

  <script src="/static/js/jquery-1.12.4.min.js"></script>
  <script src="/static/js/bootstrap.min.js"></script>
</body>
</html>
