<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="XZDHospital2BMS.BackManager.sales_contract.edit" %>

<%@ Register Src="~/BackManager/WUCHeader.ascx" TagPrefix="wuc" TagName="wucHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>填写入库单</title>
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
            <h3 class="panel-title">填写入库单信息</h3>
          </div>
          <div class="panel-body">

            <form runat="server" class="form-horizontal" role="form">

              <div class="form-group">
                <label for="tbCompanyName" class="col-sm-2 control-label">
                  <strong class="red">*</strong>销售公司：</label>
                <div class="col-sm-4">
                  <asp:TextBox runat="server" ID="tbCompanyName"
                    CssClass="form-control" placeholder="输入销售公司名称..." />
                </div>
                <div class="col-sm-6">
                  <select id="selectCompanyName" class="form-control"
                    style="width: 150px;" onchange="selectOnChang(this)">
                    <asp:Repeater ID="rptName" runat="server">
                      <ItemTemplate>
                        <option value="<%# Eval("id") %>"><%# Eval("name") %></option>
                      </ItemTemplate>
                    </asp:Repeater>
                  </select>
                </div>
              </div>

              <div class="form-group">
                <label for="tbTimeSign" class="col-sm-2 control-label">
                  <strong class="red">*</strong>入库时间：</label>
                <div class="col-sm-5">
                  <input runat="server" id="tbTimeSign" type='text' style="width: 200px;"
                    class="form-control datepicker-here" data-language='zh' data-position="right top" />
                </div>
              </div>

              <div class="form-group">
                <label for="tbPhotoUrls" class="col-sm-2 control-label">入库单照片：</label>
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
                        <input runat="server" id="tbPhotoUrls" type="hidden" />
                      </div>
                    </div>
                  </div>
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
                <div class="col-sm-offset-5 col-sm-7">
                  <asp:Button runat="server" ID="btnEdit" Text="确认提交"
                    CssClass="btn btn-primary" OnClientClick="return checkNameTime();"
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
    var i = "<% =setPhotoAmount() %>";
    // 验证销售公司名称和入库单时间不能为空和格式
    function checkNameTime() {
      var tbCompanyName = document.getElementById("tbCompanyName");
      var strName = tbCompanyName.value;
      if (strName === "") {
        alert("公司名不能为空！");
        return false;
      }
      var tbTimeSign = document.getElementById("tbTimeSign");
      var strTime = tbTimeSign.value;
      if (strTime === "") {
        alert("入库时间不能为空！");
        return false;
      }
      // 验证时间格式是否正确：（验证通过返回时间戳格式,例如:（2017-01-01,2017,-,01,-,01),否则返回null）
      var strTime = strTime.match(/^(\d{4})(-)(\d{2})(-)(\d{2})$/);
      if (strTime == null) {
        alert("请输入正确的时间格式，如：2019-01-01");
        return false;
      }
      // 验证时间是否合法：(注意：此段必须放置在验证时间格式完成之后)
      var b_d = new Date(strTime[1], strTime[3] - 1, strTime[5]);
      var b_num = b_d.getFullYear() == strTime[1] &&
        (b_d.getMonth() + 1) == strTime[3] &&
        b_d.getDate() == strTime[5];
      if (b_num == 0) {
        alert("请输入正确的时间，如：2019-01-01");
        return false;
      }
    }
    // 销售公司名称下拉列表选择事件
    function selectOnChang(obj) {
      // var value = obj.options[obj.selectedIndex].value;
      var text = obj.options[obj.selectedIndex].text;
      $('#tbCompanyName').val(text);
    }
  </script>
</body>
</html>
