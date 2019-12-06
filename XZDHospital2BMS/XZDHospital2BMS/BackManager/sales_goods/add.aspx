<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="add.aspx.cs" Inherits="XZDHospital2BMS.BackManager.sales_goods.add" %>

<%@ Register Src="~/BackManager/wucHeader.ascx" TagPrefix="wuc" TagName="wucHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>填写入库货品</title>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <link rel="stylesheet" href="/static/css/lib/bootstrap.min.css" />
  <link rel="stylesheet" href="/static/css/lib/bootstrap-theme.min.css" />
  <link rel="stylesheet" href="/static/css/lib/datepicker.min.css" />
  <link rel="stylesheet" href="/static/css/common.css" />
  <link rel="stylesheet" href="/static/css/uploadfile.css" />
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
            <h3 class="panel-title">填写入库货品表单</h3>
          </div>
          <div class="panel-body">

            <form runat="server" class="form-horizontal" role="form">

              <div class="form-group">
                <label for="tbProductName" class="col-sm-2 control-label">
                  <strong class="red">*</strong>名称：</label>
                <div class="col-sm-5">
                  <input runat="server" id="tbProductName" type="text"
                    class="form-control" placeholder="输入货品通用名称及剂型...">
                </div>
                <div class="col-sm-5">
                  <asp:Label runat="server" ID="ProductNameError" />
                </div>
              </div>

              <div class="form-group">
                <label for="tbType" class="col-sm-2 control-label">规格型号：</label>
                <div class="col-sm-5">
                  <asp:TextBox runat="server" ID="tbType"
                    CssClass="form-control" placeholder="输入货品规格型号..." />
                </div>
                <div class="col-sm-5">
                  <asp:Label runat="server" ID="TypeError" />
                </div>
              </div>

              <div class="form-group">
                <label for="tbFactoryName" class="col-sm-2 control-label">厂家产地：</label>
                <div class="col-sm-5">
                  <input runat="server" id="tbFactoryName" type="text"
                    class="form-control" placeholder="输入生产厂家/产地...">
                </div>
                <div class="col-sm-5">
                  <asp:Label runat="server" ID="FactoryNameError" />
                </div>
              </div>

              <div class="form-group">
                <label for="tbUnit" class="col-sm-2 control-label">货品单位：</label>
                <div class="col-sm-3">
                  <asp:TextBox runat="server" ID="tbUnit"
                    CssClass="form-control" placeholder="输入货品单位..." />
                </div>
                <div class="col-sm-3">
                  <select id="selectType" class="form-control"
                    style="width: 80px;" onchange="selectOnChang(this)">
                    <option value="个">个</option>
                    <option value="把">把</option>
                    <option value="只">只</option>
                    <option value="套">套</option>
                    <option value="双">双</option>
                    <option value="支">支</option>
                  </select>
                </div>
                <div class="col-sm-5">
                  <asp:Label runat="server" ID="UnitError" />
                </div>
              </div>

              <div class="form-group">
                <label for="tbAmount" class="col-sm-2 control-label">
                  <strong class="red">*</strong>货品数量：</label>
                <div class="col-sm-5">
                  <asp:TextBox runat="server" ID="tbAmount"
                    onkeyup="if(isNaN(value))execCommand('undo')"
                    onafterpaste="if(isNaN(value))execCommand('undo')"
                    CssClass="form-control" placeholder="输入货品数量..." />
                </div>
                <div class="col-sm-5">
                  <asp:Label runat="server" ID="AmountError" />
                </div>
              </div>

              <div class="form-group">
                <label for="tbPriceUnit" class="col-sm-2 control-label">
                  <strong class="red">*</strong>货品单价：</label>
                <div class="col-sm-5">
                  <asp:TextBox runat="server" ID="tbPriceUnit"
                    onkeyup="if(isNaN(value))execCommand('undo')"
                    onafterpaste="if(isNaN(value))execCommand('undo')"
                    CssClass="form-control" placeholder="输入货品单价（含税价）..." />
                </div>
                <div class="col-sm-5">
                  <asp:Label runat="server" ID="PriceUnitError" />
                </div>
              </div>

              <div class="form-group">
                <label for="tbBatchNumber" class="col-sm-2 control-label">
                  <strong class="red">*</strong>批号/序列号：</label>
                <div class="col-sm-5">
                  <input runat="server" id="tbBatchNumber" type="text"
                    class="form-control" placeholder="输入批号/序列号...">
                </div>
                <div class="col-sm-5">
                  <asp:Label runat="server" ID="BatchNumberError" />
                </div>
              </div>

              <div class="form-group">
                <label for="tbValidityPeriod" class="col-sm-2 control-label">
                  <strong class="red">*</strong>有效期至：</label>
                <div class="col-sm-5">
                  <input runat="server" id="tbValidityPeriod" type='text' style="width: 200px;"
                    class="form-control datepicker-here" data-language='zh' data-position="right top" />
                </div>
                <div class="col-sm-5">
                  <asp:Label runat="server" ID="ValidityPeriodError" />
                </div>
              </div>

              <div class="form-group">
                <label for="tbPhotoUrls" class="col-sm-2 control-label">照片：</label>
                <div class="col-sm-8">
                  <div class="wrapper-photos">
                    <div class="wrapper-file-uploader">
                      <div id="wrapper-file-select" class="form-inline">
                        <input id="inputFile" type="file" class="form-control" />
                        <input type="button" id="btnUpload" value="开始上传"
                          class="btn btn-sm btn-success" />
                      </div>
                      <asp:Panel runat="server" ID="pnlFileShow" />
                      <div id="wrapper-file-uploaded">
                        <input runat="server" id="tbPhotoUrls" type="hidden" class="form-control" />
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <div class="form-group">
                <label for="tbComment" class="col-sm-2 control-label">备注：</label>
                <div class="col-sm-8">
                  <asp:TextBox runat="server" ID="tbComment" TextMode="MultiLine" Rows="6"
                    class="form-control" placeholder="其他需要注意的事项..." />
                </div>
              </div>

              <div class="form-group">
                <div class="col-sm-offset-5 col-sm-7">
                  <asp:Button runat="server" ID="btnAdd" Text="确认提交"
                    CssClass="btn btn-primary" OnClick="btnAdd_Click" />
                </div>
              </div>

            </form>

          </div>
        </div>

      </div>
    </div>
  </div>

  <script type="text/javascript" src="/static/js/lib/jquery-1.12.4.min.js"></script>
  <script type="text/javascript" src="/static/js/lib/bootstrap.min.js"></script>
  <script type="text/javascript" src="/static/js/lib/datepicker.min.js"></script>
  <script type="text/javascript" src="/static/js/lib/i18n/datepicker.zh.js"></script>
  <script type="text/javascript" src="/static/js/upload-photo.js"></script>
  <script type="text/javascript" src="/static/js/check-form/sales_goods.js"></script>
</body>
</html>
