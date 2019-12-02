<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="show.aspx.cs" Inherits="XZDHospital2BMS.BackManager.sales_goods.show" %>

<%@ Register Src="~/BackManager/wucHeader.ascx" TagPrefix="wuc" TagName="wucHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>入库货品信息显示</title>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <link rel="stylesheet" href="/static/css/lib/bootstrap.min.css" />
  <link rel="stylesheet" href="/static/css/lib/bootstrap-theme.min.css" />
  <link rel="stylesheet" href="/static/css/common.css" />
  <style type="text/css">
    .form-group div span {
      line-height: 35px;
    }
  </style>
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
            <h3 class="panel-title">入库货品详细信息显示</h3>
          </div>
          <div class="panel-body">

            <table class="table">
              <tr>
                <td>
                  <label for="lblProductName">名称：</label></td>
                <td>
                  <asp:Label runat="server" ID="lblProductName" /></td>
              </tr>
              <tr>
                <td>
                  <label for="lblType">规格型号：</label></td>
                <td>
                  <asp:Label runat="server" ID="lblType" />
                </td>
              </tr>
              <tr>
                <td>
                  <label for="lblFactoryName">厂家产地：</label></td>
                <td>
                  <asp:Label runat="server" ID="lblFactoryName" />
                </td>
              </tr>
              <tr>
                <td>
                  <label for="lblUnit">货品单位：</label></td>
                <td>
                  <asp:Label runat="server" ID="lblUnit" />
                </td>
              </tr>
              <tr>
                <td>
                  <label for="lblAmount">货品数量：</label></td>
                <td>
                  <asp:Label runat="server" ID="lblAmount" />
                </td>
              </tr>
              <tr>
                <td>
                  <label for="lblPriceUnit">货品单价：</label></td>
                <td>
                  <asp:Label runat="server" ID="lblPriceUnit" />
                </td>
              </tr>
              <tr>
                <td>
                  <label for="lblPriceTotal">总价：</label></td>
                <td>
                  <asp:Label runat="server" ID="lblPriceTotal" />
                </td>
              </tr>
              <tr>
                <td>
                  <label for="lblBatchNumber">批号/序列号：</label></td>
                <td>
                  <asp:Label runat="server" ID="lblBatchNumber" />
                </td>
              </tr>
              <tr>
                <td>
                  <label for="lblValidityPeriod">有效期至：</label></td>
                <td>
                  <asp:Label runat="server" ID="lblValidityPeriod" />
                </td>
              </tr>
              <tr>
                <td>
                  <label for="pnlShowPhoto">照片：</label></td>
                <td>
                  <div class="wrapper-photos">
                    <asp:Panel runat="server" ID="pnlShowPhoto">
                      <asp:Literal runat="server" ID="ltrShowPhoto" />
                    </asp:Panel>
                  </div>
                </td>
              </tr>
              <tr>
                <td>
                  <label for="lblComment">备注：</label></td>
                <td>
                  <asp:Label runat="server" ID="lblComment" />
                </td>
              </tr>
            </table>

          </div>
        </div>

      </div>
    </div>
  </div>

  <script type="text/javascript" src="/static/js/lib/jquery-1.12.4.min.js"></script>
  <script type="text/javascript" src="/static/js/lib/bootstrap.min.js"></script>
</body>
</html>
