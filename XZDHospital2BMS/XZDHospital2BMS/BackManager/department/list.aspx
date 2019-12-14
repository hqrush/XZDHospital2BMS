<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="XZDHospital2BMS.BackManager.department.list" %>


<%@ Register Src="~/BackManager/wucHeader.ascx" TagPrefix="wuc" TagName="wucHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>科室名称列表</title>
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
  <form id="formShow" runat="server">

    <wuc:wucHeader runat="server" ID="wucHeader" />

    <div class="container">
      <div class="row">
        <div class="col-lg-12">

          <div class="wrapper-add col-sm-12">

            <div class="form-group">
              <label for="tbCompanyName">公司名称：（可批量添加，一行为一条记录）</label>
              <textarea runat="server" id="tbDepartmentName"
                class="form-control" width="300" rows="20"></textarea><br />
              <asp:Button runat="server" ID="btnSubmit" Text="确定提交"
                CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
              <input type="button" value="清空" class="btn btn-warning" />
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
