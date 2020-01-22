<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCFileUploader.ascx.cs"
  Inherits="XZDHospital2BMS.BackManager.WUCFileUploader" %>

<div class="wrapper-file-uploader">
  <div id="wrapper-file-select" class="form-inline">
    <input id="inputFile" type="file" class="form-control" />
    <input type="button" id="btnUpload" value="开始上传"
      class="btn btn-sm btn-success" onclick="uploadPhoto()" />
  </div>
  <div id="wrapper-file-show"></div>
</div>

<script type="text/javascript">

  // 这个变量存储现在有多少张已上传的照片
  var i = 0;

  function uploadPhoto() {
    if (validFileInput()) {
      debugger;
      inputFile = document.getElementById('inputFile');
      var fileUpload = inputFile.files[0];
      var formData = new FormData();
      formData.append("photo_file", fileUpload);
      debugger;
      $.ajax({
        url: "/Handler/UploadHandler.ashx",
        type: 'POST',
        dataType: 'json',
        data: formData,
        async: true,
        // 不指定编码方式（默认指定编码 urlencode）
        processData: false,
        // 不处理数据
        contentType: false,
        success: function (result) {
          debugger;
          var url = result.ServerFilePath;
          debugger;
          if (url !== "") {
            i++;
            debugger;
            showImage(i, url);
            debugger;
            return true;
          } else if (result.Message !== "") {
            debugger;
            console.log(result.Message);
            return false;
          }
        },
        error: function (error) {
          console.log(error);
          return error;
        }
      });
    } else {
      console.log('uploadFile() 方法里验证文件输入框没通过！');
    }
  }

  function addFileInput() {
    if (i < 6) {
      var str = '<div id="inputFile' + i + '" class="form-inline">';
      str += '<input type="file" class="form-control" /></div>';
      document.getElementById('wrapper-file-select').insertAdjacentHTML("beforeEnd", str);
      i++;
    }
    else {
      alert("您一次最多只能上传5张图片！");
    }
  }

  function delFileInput() {
    ElementRoot = document.getElementById("wrapper-file-select")
    if (i > 2) {
      i--;
      ElementLast = document.getElementById('inputFile' + i);
      if (ElementLast) ElementRoot.removeChild(ElementLast);
    }
  }

  function showImage(index, url) {
    var str = '<div id="wrapper-img-' + index + '">';
    str += '<img width="100" height="100" src="' + url + '" />';
    str += '<button class="btn btn-sm btn-warning" onclick="delPhoto(' + index + ')">删除</button></div>';
    document.getElementById('wrapper-file-show').insertAdjacentHTML("beforeEnd", str);
  }

  function delPhoto(index) {

  }

  function validFileInput() {
    if (!hasFile('inputFile')) return false;
    if (!isAllowFileExt('inputFile', "jpg,jpeg,png")) return false;
    return true;
  }

  // 检查是否选择了文件
  function hasFile(strControlID) {
    inputFile = document.getElementById(strControlID);
    if (inputFile.value === "") {
      alert("请选择要上传的文件！");
      return false;
    }
    return true;
  }

  // 检查文件扩展名
  function isAllowFileExt(strControlID, strExt) {
    inputFile = document.getElementById(strControlID);
    var strFilePath = inputFile.value;
    // lastIndexOf 如果没有搜索到说明没有扩展名，则返回为-1
    if (strFilePath.lastIndexOf(".") !== -1) {
      var strFileExt = (strFilePath.substring(strFilePath.lastIndexOf(".") + 1, strFilePath.length)).toLowerCase();
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
