<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="form.aspx.cs"
  Inherits="XZDHospital2BMS.BackManager.sales_company.form" %>

<%@ Register Src="~/BackManager/wucHeader.ascx" TagPrefix="wuc" TagName="wucHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>医药器械销售公司表单</title>
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
            <h3 class="panel-title">填写医药器械销售公司资料</h3>
          </div>
          <div class="panel-body">

            <form runat="server" class="form-horizontal" role="form">
              <div class="form-group">
                <label for="tbName" class="col-sm-2 control-label">公司名称：</label>
                <div class="col-sm-10">
                  <input runat="server" class="form-control" id="tbName" type="text" placeholder="输入公司名称...">
                </div>
              </div>
              <div class="form-group">
                <div class="col-sm-offset-5 col-sm-7">
                  <asp:Button runat="server" ID="btnSubmit" Text="确认提交" CssClass="btn btn-primary" />
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
