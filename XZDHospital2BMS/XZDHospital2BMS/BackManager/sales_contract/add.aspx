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

                  <div class="wrapper-photos">

                    <div id="MyFile">
                      <input onclick="addFile()" type="button" value="增加图片"><br />
                      <input type="file" name="File" runat="server" style="width: 300px" />
                    </div>

                    <div class="form-inline">
                      <asp:FileUpload runat="server" ID="fuPhoto" Width="300"
                        CssClass="form-control" />
                      <asp:Button runat="server" ID="btnUploadPhoto" Text="上传图片"
                        CssClass="btn btn-primary btn-sm"
                        OnClientClick='return CheckUploadFile("fuPhoto", "jpg,jpeg,png");'
                        OnClick="btnUploadPhoto_Click" />
                    </div>
                    <div class="form-inline">
                      <asp:Image runat="server" ID="imgPhoto" Width="90"
                        CssClass="img-thumbnail" Visible="false" />
                      <asp:Button runat="server" ID="btnDelPhoto" Text="删除图片" Visible="false"
                        CssClass="btn btn-danger btn-sm" OnClick="btnDelPhoto_Click" />
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
  <script type="text/javascript">
    var i = 1
    function addFile() {
      if (i < 8) {
        var str = '<br /><input type="file" name="File" runat="server" style="width: 300px"/>';
        str += '<br />描述：<input name="text" type="text" style="width: 150px" maxlength="20" />';
        document.getElementById('MyFile').insertAdjacentHTML("beforeEnd", str);
      }
      else {
        alert("您一次最多只能上传8张图片！");
      }
      i++;
    }

    function CheckUploadFile(strControlID, strExt) {
      var objFileUpload = document.getElementById(strControlID);
      if (objFileUpload.value == "") {
        alert("还没有选择要上传的文件，请选择要上传的文件！");
        return false;
      }
      var strFileUplodPath = objFileUpload.value;
      //lastIndexOf如果没有搜索到则返回为-1  
      if (strFileUplodPath.lastIndexOf(".") != -1) {
        var strFileExt = (strFileUplodPath.substring(strFileUplodPath.lastIndexOf(".") + 1, strFileUplodPath.length)).toLowerCase();
        //var aryAllowExt = new Array();
        var aryAllowExt = strExt.split(",");
        //aryAllowExt[0] = "xls";
        //aryAllowExt[1] = "xlsx";
        for (var i = 0; i < aryAllowExt.length; i++) {
          if (aryAllowExt[i] == strFileExt) {
            return true;
          } else {
            continue;
          }
        }
        alert("只允许上传" + strExt + "类型的文件！");
        return false;
      } else {
        alert("只允许上传" + strExt + "类型文件！");
        return false;
      }
      return true;
    }
  </script>

</body>
</html>
