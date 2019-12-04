<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="add.aspx.cs" Inherits="XZDHospital2BMS.BackManager.checkout_contract.add" %>

<%@ Register Src="~/BackManager/WUCHeader.ascx" TagPrefix="wuc" TagName="wucHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>填写出库单</title>
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
            <h3 class="panel-title">填写出库单信息</h3>
          </div>
          <div class="panel-body">

            <form runat="server" class="form-horizontal" role="form">

              <div class="form-group">
                <label class="col-sm-2 control-label">
                  <strong class="red">*</strong>申请单位：</label>
                <div class="col-sm-4">
                  <label class="checkbox-inline">
                    <input runat="server" type="checkbox" id="cbUnitName1" value="信州区第二人民医院" checked>
                    信州区第二人民医院
                  </label>
                  <label class="checkbox-inline">
                    <input runat="server" type="checkbox" id="cbUnitName2" value="东市街道卫生站">
                    东市街道卫生站
                  </label>
                </div>
              </div>

              <div class="form-group">
                <label for="tbDepartmentName" class="col-sm-2 control-label">
                  <strong class="red">*</strong>申请部门\科室：</label>
                <div class="col-sm-5">
                  <input runat="server" id="tbDepartmentName" type='text'
                    class="form-control" placeholder="请填写提出本次出库申请的部门\科室..." />
                </div>
              </div>

              <div class="form-group">
                <label for="tbSignName" class="col-sm-2 control-label">
                  <strong class="red">*</strong>申请人：</label>
                <div class="col-sm-5">
                  <input runat="server" id="tbSignName" type='text'
                    class="form-control" placeholder="请填写提出本次出库申请人姓名..." />
                </div>
              </div>

              <div class="form-group">
                <label for="tbPhotoUrls" class="col-sm-2 control-label">申请人签名照片：</label>
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
                    class="form-control" placeholder="本次出库是否有需要注意的事项..." />
                </div>
              </div>

              <div class="form-group">
                <div class="col-sm-offset-5 col-sm-7">
                  <asp:Button runat="server" ID="btnAdd" Text="确认提交"
                    CssClass="btn btn-primary" OnClientClick="return checkNameTime();"
                    OnClick="btnAdd_Click" />
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
</body>
</html>
