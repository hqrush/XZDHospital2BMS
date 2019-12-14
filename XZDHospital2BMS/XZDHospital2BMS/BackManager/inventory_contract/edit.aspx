﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="XZDHospital2BMS.BackManager.inventory_contract.edit" %>

<%@ Register Src="~/BackManager/WUCHeader.ascx" TagPrefix="wuc" TagName="wucHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>修改盘点单</title>
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
            <h3 class="panel-title">填写盘点单信息</h3>
          </div>
          <div class="panel-body">

            <form runat="server" class="form-horizontal" role="form">

              <div class="form-group">
                <label for="tbNameSign" class="col-sm-2 control-label">
                  <strong class="red">*</strong>签名：</label>
                <div class="col-sm-6">
                  <asp:TextBox runat="server" ID="tbNameSign" MaxLength="20"
                    CssClass="form-control" placeholder="输入负责此次盘点的人员姓名..." />
                </div>
              </div>

              <div class="form-group">
                <label for="tbPhotoUrls" class="col-sm-2 control-label">盘点单照片：</label>
                <div class="col-sm-8">
                  <div class="wrapper-photos">
                    <div class="wrapper-file-uploader">
                      <div id="wrapper-file-select" class="form-inline">
                        <input id="inputFile" type="file" class="form-control" />
                        <input type="button" id="btnUpload" value="开始上传"
                          class="btn btn-sm btn-success" />
                      </div>
                      <asp:Panel runat="server" ID="pnlFileShow">
                        <asp:Literal runat="server" ID="ltrShowPhoto" />
                      </asp:Panel>
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
                    class="form-control" placeholder="本次盘点是否有需要注意的事项..." />
                </div>
              </div>

              <div class="form-group">
                <div class="col-sm-offset-5 col-sm-7">
                  <asp:Button runat="server" ID="btnEdit" Text="确认提交"
                    CssClass="btn btn-primary" OnClientClick="return checkName();"
                    OnClick="btnEdit_Click" />
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
  <script type="text/javascript">
    function checkName() {
      var tbNameSign = document.getElementById("tbNameSign");
      var strName = tbNameSign.value;
      if (strName === "") {
        alert("公司名不能为空！");
        return false;
      }
    }
  </script>
</body>
</html>