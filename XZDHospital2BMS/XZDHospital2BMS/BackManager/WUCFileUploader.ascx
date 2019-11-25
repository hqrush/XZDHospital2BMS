<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCFileUploader.ascx.cs"
  Inherits="XZDHospital2BMS.BackManager.WUCFileUploader" %>

<div class="wrapper-file-uploader">
  <div id="MyFile">
    <input onclick="addFile()" type="button" value="增加图片"><br />
    <input type="file" name="File" runat="server" style="width: 300px" />
  </div>
  <asp:Button ID="btnUpload" runat="server" Text="开始上传" OnClick="btnUpload_Click" />
</div>

<script type="text/javascript">
  var i = 1
  function addFile() {
    if (i < 8) {
      var str = '<div class="form-inline">';
      str += '<input type="file" name="File" class="form-control" style="width: 300px" /></div>';
      document.getElementById('MyFile').insertAdjacentHTML("beforeEnd", str);
    }
    else {
      alert("您一次最多只能上传8张图片！");
    }
    i++;
  }

  function removeFileInput() {
    document.getElementById("sspp").parentNode.removeChild(document.getElementById("sspp"));
    // 如果清空
    document.getElementById("sspp").innerHTML = ""
    var el = document.getElementById("poly" + idn);
    el.removeNode(1);
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
