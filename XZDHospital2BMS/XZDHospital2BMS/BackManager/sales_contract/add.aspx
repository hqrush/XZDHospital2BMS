<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="add.aspx.cs" Inherits="XZDHospital2BMS.BackManager.sales_contract.add" %>

<%@ Register Src="~/BackManager/wucHeader.ascx" TagPrefix="wuc" TagName="wucHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>填写入库单</title>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <link rel="stylesheet" href="/static/css/bootstrap.min.css" />
  <link rel="stylesheet" href="/static/css/bootstrap-theme.min.css" />
  <link rel="stylesheet" href="/static/css/datepicker.min.css" />
  <link rel="stylesheet" href="/static/css/uploadfile.css" />
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
            <h3 class="panel-title">填写入库单信息</h3>
          </div>
          <div class="panel-body">

            <form runat="server" class="form-horizontal" role="form">

              <div class="form-group">
                <label for="tbCompanyName" class="col-sm-2 control-label">
                  <strong class="red">*</strong>销售公司：</label>
                <div class="col-sm-5">
                  <asp:TextBox runat="server" ID="tbCompanyName"
                    CssClass="form-control" placeholder="输入销售公司名称..." />
                </div>
                <div class="col-sm-5">
                  <asp:Label runat="server" ID="CompanyNameError" />
                </div>
              </div>
              <div class="form-group">
                <label for="tbTimeSign" class="col-sm-2 control-label">入库时间：</label>
                <div class="col-sm-5">
                  <input runat="server" id="tbTimeSign" type='text'
                    class="datepicker-here" data-language='zh' data-position="right top" />
                </div>
              </div>
              <div class="form-group">
                <label for="tbComment" class="col-sm-2 control-label">备注：</label>
                <div class="col-sm-8">
                  <asp:TextBox runat="server" ID="tbComment" TextMode="MultiLine" Rows="6"
                    class="form-control" placeholder="本次销售是否有需要注意的事项..." />
                </div>
              </div>
              <div class="form-group">
                <label for="tbPhotoUrls" class="col-sm-2 control-label">入库单照片：</label>
                <div class="col-sm-8">

                  <div class="wrapper-uploader">

                    <div id="uploader" class="wu-example">
                      <div class="queueList">
                        <div id="dndArea" class="placeholder">
                          <div id="filePicker" class="webuploader-container">
                            <div class="webuploader-pick">点击选择图片</div>
                            <div id="rt_rt_1dqdlvdoc1a2f25818j111bg1qag1" style="position: absolute; top: 0px; left: 448px; width: 168px; height: 44px; overflow: hidden; bottom: auto; right: auto;">
                              <input type="file" name="file" class="webuploader-element-invisible" multiple="multiple" accept="image/*"><label style="opacity: 0; width: 100%; height: 100%; display: block; cursor: pointer; background: rgb(255, 255, 255);"></label>
                            </div>
                          </div>
                          <p>或将照片拖到这里，单次最多可选300张</p>
                        </div>
                        <ul class="filelist"></ul>
                      </div>
                      <div class="statusBar" style="display: none;">
                        <div class="progress" style="display: none;">
                          <span class="text">0%</span>
                          <span class="percentage" style="width: 0%;"></span>
                        </div>
                        <div class="info">共0张（0B），已上传0张</div>
                        <div class="btns">
                          <div id="filePicker2" class="webuploader-container">
                            <div class="webuploader-pick">继续添加</div>
                            <div id="rt_rt_1dqdlvdofocnjp1qesjtfocq6" style="position: absolute; top: 0px; left: 0px; width: 1px; height: 1px; overflow: hidden;">
                              <input type="file" name="file" class="webuploader-element-invisible" multiple="multiple" accept="image/*"><label style="opacity: 0; width: 100%; height: 100%; display: block; cursor: pointer; background: rgb(255, 255, 255);"></label>
                            </div>
                          </div>
                          <div class="uploadBtn state-pedding">开始上传</div>
                        </div>
                      </div>
                    </div>

                  </div>

                </div>
              </div>

              <div class="form-group">
                <div class="col-sm-offset-5 col-sm-7">
                  <asp:Button runat="server" ID="btnCompanyContractAdd" Text="确认提交"
                    CssClass="btn btn-primary" OnClick="btnCompanyContractAdd_Click" />
                </div>
              </div>

            </form>

          </div>
        </div>

      </div>
    </div>
  </div>

  <script type="text/javascript" src="/static/js/jquery-1.12.4.min.js"></script>
  <script type="text/javascript" src="/static/js/bootstrap.min.js"></script>
  <script type="text/javascript" src="/static/js/datepicker.min.js"></script>
  <script type="text/javascript" src="/static/js/i18n/datepicker.zh.js"></script>
  <script type="text/javascript" src="/static/js/webuploader.nolog.min.js"></script>
  <script type="text/javascript" src="/static/js/uploadfile.js"></script>
</body>
</html>
