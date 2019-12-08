<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="add.aspx.cs" Inherits="XZDHospital2BMS.BackManager.medical_record.add" %>

<%@ Register Src="~/BackManager/wucHeader.ascx" TagPrefix="wuc" TagName="wucHeader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>添加病历</title>
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
            <h3 class="panel-title">填写病历资料表单</h3>
          </div>
          <div class="panel-body">

            <form runat="server" class="form-horizontal" role="form">

              <div class="form-group">
                <label for="tbSickbedNumber" class="col-sm-2 control-label">住院号：</label>
                <div class="col-sm-5">
                  <input runat="server" id="tbSickbedNumber" type="text"
                    class="form-control" placeholder="输入住院号...">
                </div>
                <div class="col-sm-5">
                  <asp:Label runat="server" ID="SickbedNumberError" />
                </div>
              </div>

              <div class="form-group">
                <label for="tbNameReal" class="col-sm-2 control-label">
                  <strong class="red">*</strong>姓名：</label>
                <div class="col-sm-5">
                  <input runat="server" id="tbNameReal" type="text"
                    class="form-control" placeholder="输入姓名...">
                </div>
                <div class="col-sm-5">
                  <asp:Label runat="server" ID="NameRealError" />
                </div>
              </div>

              <div class="form-group">
                <label class="col-sm-2 control-label">性别：</label>
                <div class="col-sm-5">
                  <asp:RadioButtonList runat="server" ID="rblSex"
                    RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <asp:ListItem Text="男" Value="男" Selected="True" />
                    <asp:ListItem Text="女" Value="女" />
                  </asp:RadioButtonList>
                </div>
              </div>

              <div class="form-group">
                <label for="tbBirthday" class="col-sm-2 control-label">出生年月：</label>
                <div class="col-sm-5">
                  <input runat="server" id="tbBirthday" type='text' style="width: 200px;"
                    class="form-control datepicker-here" data-language='zh' data-position="right top" />
                </div>
                <div class="col-sm-5">
                  <asp:Label runat="server" ID="BirthdayError" />
                </div>
              </div>

              <div class="form-group">
                <label for="tbDepartment" class="col-sm-2 control-label">科别：</label>
                <div class="col-sm-10">
                  <input runat="server" id="tbDepartment" type="text"
                    class="form-control" placeholder="输入科别...">
                </div>
                <div class="col-sm-5">
                  <asp:Label runat="server" ID="DepartmentError" />
                </div>
              </div>

              <div class="form-group">
                <label for="tbNameDisease" class="col-sm-2 control-label">
                  <strong class="red">*</strong>疾病名称：</label>
                <div class="col-sm-10">
                  <input runat="server" id="tbNameDisease" type="text"
                    class="form-control" placeholder="输入疾病名称...">
                </div>
                <div class="col-sm-5">
                  <asp:Label runat="server" ID="NameDiseaseError" />
                </div>
              </div>

              <div class="form-group">
                <label for="tbTimeIn" class="col-sm-2 control-label">
                  <strong class="red">*</strong>入院日期：</label>
                <div class="col-sm-5">
                  <input runat="server" id="tbTimeIn" type='text' style="width: 200px;"
                    class="form-control datepicker-here" data-language='zh' data-position="right top" />
                </div>
                <div class="col-sm-5">
                  <asp:Label runat="server" ID="TimeInError" />
                </div>
              </div>

              <div class="form-group">
                <label for="tbTimeOut" class="col-sm-2 control-label">
                  <strong class="red">*</strong>出院日期：</label>
                <div class="col-sm-5">
                  <input runat="server" id="tbTimeOut" type='text' style="width: 200px;"
                    class="form-control datepicker-here" data-language='zh' data-position="right top" />
                </div>
                <div class="col-sm-5">
                  <asp:Label runat="server" ID="TimeOutError" />
                </div>
              </div>

              <div class="form-group">
                <label class="col-sm-2 control-label">出院情况：</label>
                <div class="col-sm-5">
                  <asp:CheckBoxList runat="server" ID="cblSituationOut"
                    RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <asp:ListItem Text="治愈" Value="治愈" />
                    <asp:ListItem Text="好转" Value="好转" />
                    <asp:ListItem Text="无变化" Value="无变化" />
                    <asp:ListItem Text="恶化" Value="恶化" />
                    <asp:ListItem Text="未治" Value="未治" />
                    <asp:ListItem Text="死亡" Value="死亡" />
                  </asp:CheckBoxList>
                </div>
              </div>

              <div class="form-group">
                <label for="tbPhotoUrls" class="col-sm-2 control-label">病历相关照片：</label>
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
                    class="form-control" placeholder="本次销售是否有需要注意的事项..." />
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
  <script type="text/javascript" src="/static/js/check-form/medical_record.js"></script>
</body>
</html>
