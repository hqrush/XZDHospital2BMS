$(function () {
  // 用jquery的$语法给btnUpload按钮加上click事件
  // btnUpload按钮应该是 input type="button"的按钮
  // 不要用button，button会触发服务器事件，做不到无刷新
  $("#btnUpload").on("click", function () {
    // 验证文件选择框里有文件和文件扩展名是图像文件
    if (validFileInput()) {
      inputFile = document.getElementById('inputFile');
      var fileUpload = inputFile.files[0];
      var formData = new FormData();
      formData.append("photo_file", fileUpload);
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
          // 上传成功后清空文件上传框
          inputFile.value = "";
          var url = result.ServerFilePath;
          if (url !== "") {
            i++;
            showImage(i, url);
            addPhotoUrls(url);
            return true;
          } else if (result.Message !== "") {
            return false;
          }
        },
        error: function (error) {
          console.log(error);
          return error;
        }
      });
    } else {
      // console.log('uploadFile() 方法里验证文件输入框没通过！');
    }
  });
});

// 这个变量存储现在有多少张已上传的照片
var i = 0;

// 原来的想法是动态增加多个上传文件框
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
// 动态减少上传文件框
function delFileInput() {
  ElementRoot = document.getElementById("wrapper-file-select");
  if (i > 2) {
    i--;
    ElementLast = document.getElementById('inputFile' + i);
    if (ElementLast) ElementRoot.removeChild(ElementLast);
  }
}

// 上传成功以后显示图片
function showImage(index, url) {
  var str = '<div id="img-' + index + '" class="wrapper-photo-show">';
  str += '<img width="100" height="100" src="' + url + '" /><br />';
  str += '<input type="button" id="btnDelPhoto" class="btn btn-sm btn-warning"' +
    ' onclick="delPhoto(' + index + ')" value="删除" /></div>';
  document.getElementById("wrapper-file-show").insertAdjacentHTML("beforeEnd", str);
}

// 删除图片按钮的事件
function delPhoto(index) {
  var divShow = document.getElementById("img-" + index);
  var img = divShow.getElementsByTagName("IMG")[0];
  // 这里的src是包括域名地址的，返回的值如下：
  // https://localhost:44340/UploadFile/image/2019/11/29/wzocKC4jVxho13.jpg
  var src = img.src;
  //获取https://localhost:44340后面的内容
  var k = find(src, "/", 2);
  src = src.substring(k);

  var divShowParent = document.getElementById("wrapper-file-show");
  console.log(divShowParent);
  for (var j = 0; j < i; j++) {
    if (divShow === divShowParent.childNodes[j]) {
      delPhotoUrls(src);
      divShowParent.removeChild(divShowParent.childNodes[j]);
    }
  }
}

// 查找某个字符（char）在某个字符串（str）中第几次（num）出现的位置
function find(str, char, num) {
  // indexOf() 返回某个指定的字符串值在字符串中首次出现的位置
  // stringObject.indexOf(searchvalue, fromindex)
  // searchvalue	必需。规定需检索的字符串值。
  // fromindex	可选的整数参数。规定在字符串中开始检索的位置
  var x = str.indexOf(char);
  for (var j = 0; j < num; j++) {
    x = str.indexOf(char, x + 1);
  }
  return x;
}

// 将上传到服务器的图片的服务器地址加到一个type="text"的input控件里
// 整个页面的数据提交的时候，contract对象里的photoUrls属性值从这个文本框里读取数据
function addPhotoUrls(url) {
  var tbPhotoUrls = document.getElementById("tbPhotoUrls");
  tbPhotoUrls.value += url + ",";
}

// 删除图像的时候也要从这个文本框中删除相应的url值
function delPhotoUrls(url) {
  var tbPhotoUrls = document.getElementById("tbPhotoUrls");
  var val = tbPhotoUrls.value;
  console.log(val);
  if (checkEndWith(val, ",")) val = val.substring(0, val.length - 1);
  console.log(val);
  aryUrl = val.split(",");
  console.log(aryUrl);
  removeArrayByValue(aryUrl, url);
  console.log(aryUrl);
  tbPhotoUrls.value = aryUrl.join("");
}

// 从数组中删除某个指定的值
function removeArrayByValue(ary, val) {
  if (ary) {
    console.log(ary);
    for (var j = 0; j < ary.length; j++) {
      console.log(ary[j]);
      console.log(val);
      if (ary[j] === val) {
        ary.splice(j, 1);
        console.log(ary);
        break;
      }
    }
  }
}

// 检查某个字符串是否以指定字符结尾
function checkEndWith(str, target) {
  var start = str.length - target.length;
  if (str.substr(start, target.length) === target)
    return true;
  else
    return false;
}

// 验证文件上传框是否有文件和是否是图像文件
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
    var strFileExt = strFilePath.substring(strFilePath.lastIndexOf(".") + 1, strFilePath.length);
    strFileExt = strFileExt.toLowerCase();
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