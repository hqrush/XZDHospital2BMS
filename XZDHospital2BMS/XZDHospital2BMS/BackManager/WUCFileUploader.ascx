<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCFileUploader.ascx.cs"
  Inherits="XZDHospital2BMS.BackManager.WUCFileUploader" %>

<div class="wrapper-file-uploader">
  <div id="AreaBtn">
    <input type="button" class="btn btn-sm btn btn-info" value="增加图片" onclick="addFileInput()" />
    <input type="button" class="btn btn-sm btn btn-info" value="减少图片" onclick="delFileInput()" />
    <input type="button" class="btn btn-sm btn-success" value="开始上传" onclick="uploadfiles()" />
  </div>
  <div id="AreaFileUpload">
    <div id="inputFile1" class="form-inline">
      <input type="file" class="form-control" style="width: 300px" />
    </div>
  </div>
  <div id="AreaFileShow">
    <input type="text" id="tbMsg" class="form-control" value="Info" />
  </div>
</div>

<script type="text/javascript">
  var i = 2
  function addFileInput() {
    if (i < 9) {
      var str = '<div id="inputFile' + i + '" class="form-inline" > ';
      str += '<input type="file" class="form-control" style="width: 300px" /></div>';
      document.getElementById('AreaFileUpload').insertAdjacentHTML("beforeEnd", str);
      i++;
    }
    else {
      alert("您一次最多只能上传8张图片！");
    }
  }

  function delFileInput() {
    ElementRoot = document.getElementById("AreaFileUpload")
    if (i > 2) {
      i--;
      ElementLast = document.getElementById('inputFile' + i);
      if (ElementLast) ElementRoot.removeChild(ElementLast);
    }
  }

  function uploadfiles() {
    tbMsg = document.getElementById('tbMsg');
    if (validFileInput()) {
      tbMsg.value = ajaxUpload();
    } else {
      tbMsg.value = "Error!";
    }
  }

  function ajaxUpload() {
    console.log('ajaxUpload里的i=' + i);
    for (var j = 1; j < i; j++) {
      strControlID = 'inputFile' + j;
      var elDiv = document.getElementById(strControlID);
      inputFile = elDiv.getElementsByTagName("INPUT")[0];
      var fileUpload = inputFile.files[0];
      console.log('ajaxUpload里捕捉到的fileUpload===============' + fileUpload);
      var formData = new FormData();
      formData.append("photo_file", fileUpload);
      $.ajax({
        url: "/Handler/UploadFileHandler.ashx",
        type: 'POST',
        dataType: 'text',
        data: formData,
        async: true,
        processData: false,
        timeout: 30000,
        success: function (result) {
          console.log('Result======' + result);
          return result;
        }
      });
    }
  }

  function validFileInput() {
    console.log('i=' + i);
    for (var j = 1; j < i; j++) {
      strControlID = 'inputFile' + j;
      console.log('validFileInput 里的 strControlID=' + strControlID);
      if (!hasFile(strControlID)) return false;
      if (!isAllowFileExt(strControlID, "jpg,jpeg,png")) return false;
    }
    return true;
  }

  // 检查是否选择了文件
  function hasFile(strControlID) {
    // 上传文件的file类型的input是放在有id的div里的
    // 先找到这个div
    var elDiv = document.getElementById(strControlID);
    // 再找到这个div下面的input，只有一个input
    inputFile = elDiv.getElementsByTagName("INPUT")[0]
    if (inputFile.value == "") {
      alert("请选择要上传的文件！");
      return false;
    }
    return true;
  }

  // 检查文件扩展名
  function isAllowFileExt(strControlID, strExt) {
    var elDiv = document.getElementById(strControlID);
    // 再找到这个div下面的input，只有一个input
    inputFile = elDiv.getElementsByTagName("INPUT")[0]
    console.log('isAllowFileExt 里的 inputFile' + inputFile);
    var strFileUplodPath = inputFile.value;
    //lastIndexOf 如果没有搜索到说明没有扩展名，则返回为-1
    if (strFileUplodPath.lastIndexOf(".") != -1) {
      var strFileExt = (strFileUplodPath.substring(strFileUplodPath.lastIndexOf(".") + 1, strFileUplodPath.length)).toLowerCase();
      var aryAllowExt = strExt.split(",");
      for (var j = 0; j < aryAllowExt.length; j++) {
        if (aryAllowExt[j] === strFileExt) {
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
  }

</script>
