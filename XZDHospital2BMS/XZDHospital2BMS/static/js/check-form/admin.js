/*  
 * 登录名校验方法  
 */
function validateUsername() {
  var id = "tbUsername";
  var value = $("#" + id).val();
  var ErrorElement = "#" + id.substring(2) + "Error";
  $(ErrorElement).text("");
  showError($(ErrorElement));
  if (!value) {
    $(ErrorElement).text("用户名不能为空！");
    showError($(ErrorElement));
    $("#" + id).focus();
    return false;
  }
  var reg = /^[0-9a-zA-Z]+$/;
  if (!reg.test(value)) {
    $(ErrorElement).text("不能输入除数字、字母以外的字符！");
    showError($(ErrorElement));
    $("#" + id).focus();
    return false;
  }
  if (value.length < 4 || value.length > 12) {
    $(ErrorElement).text("用户名长度必须在4 ~ 12之间！");
    showError($(ErrorElement));
    $("#" + id).focus();
    return false;
  }
  return true;
}

/*  
 * 校验登录名是否已注册
 */
function validateUsernameRename() {
  var id = "tbUsername";
  var value = $("#" + id).val();
  var ErrorElement = "#" + id.substring(2) + "Error";
  $(ErrorElement).text("");
  showError($(ErrorElement));
  $.ajax({
    url: "/Handler/CheckAdminHandler.ashx",
    data: { username: value },
    type: "POST",
    dataType: "text",
    async: false,
    cache: false,
    success: function (result) {
      if ("OK" !== result) {
        $(ErrorElement).text(result);
        showError($(ErrorElement));
        $("#" + id).focus();
        return false;
      }
    }
  });
  return true;
}

/*  
 * 密码校验方法，用于添加管理员时
 */
function validatePasswordAdd() {
  var id = "tbPassword";
  var value = $("#" + id).val();
  var ErrorElement = "#" + id.substring(2) + "Error";
  $(ErrorElement).text("");
  showError($(ErrorElement));
  if (!value) {
    $(ErrorElement).text("密码不能为空！");
    showError($(ErrorElement));
    $("#" + id).focus();
    return false;
  }
  if (value.length < 4 || value.length > 12) {
    $(ErrorElement).text("密码长度必须在4 ~ 12之间！");
    showError($(ErrorElement));
    $("#" + id).focus();
    return false;
  }
  return true;
}

/*  
 * 密码校验方法，用于编辑管理员时
 */
function validatePasswordEdit() {
  var id = "tbPassword";
  var value = $("#" + id).val();
  var ErrorElement = "#" + id.substring(2) + "Error";
  $(ErrorElement).text("");
  showError($(ErrorElement));
  if (value.length > 0 && ((value.length < 4 || value.length > 12))) {
    $(ErrorElement).text("密码长度必须在4 ~ 12之间！");
    showError($(ErrorElement));
    $("#" + id).focus();
    return false;
  }
  return true;
}

/*  
 * 确认密码校验方法，用于添加管理员时
 */
function validatePassword2Add() {
  var id = "tbPassword2";
  var value = $("#" + id).val();
  var ErrorElement = "#" + id.substring(2) + "Error";
  $(ErrorElement).text("");
  showError($(ErrorElement));
  if (!value) {
    $(ErrorElement).text("确认密码不能为空！");
    showError($(ErrorElement));
    $("#" + id).focus();
    return false;
  }
  if (value !== $("#tbPassword").val()) {
    $(ErrorElement).text("两次输入不一致！");
    showError($(ErrorElement));
    $("#" + id).focus();
    return false;
  }
  return true;
}

/*  
 * 确认密码校验方法，用于编辑管理员时
 */
function validatePassword2Edit() {
  var id = "tbPassword2";
  var value = $("#" + id).val();
  var ErrorElement = "#" + id.substring(2) + "Error";
  $(ErrorElement).text("");
  showError($(ErrorElement));
  if (value !== $("#tbPassword").val()) {
    $(ErrorElement).text("两次输入不一致！");
    showError($(ErrorElement));
    $("#" + id).focus();
    return false;
  }
  return true;
}

/*  
 * 判断当前元素是否存在内容，如果存在显示，不存在不显示！
 */
function showError(element) {
  var text = element.text();
  if (!text) {
    element.css("display", "none");
  } else {
    element.css("display", "");
  }
}

$(function () {
  var flagSalesContract = 0;
  var flagSalesGoods = 0;
  var flagCheckoutContract = 0;
  var flagCheckoutRecord = 0;
  var flagInventoryContract = 0;
  var flagInventoryRecord = 0;
  var flagSysAdmin = 0;
  var flagSalesCompany = 0;
  //全选
  $("#cbSalesContract").on("click", function () {
    if (flagSalesContract === 0) {
      //把所有复选框选中
      $("input[id^='cbSalesContract_']").prop("checked", true);
      flagSalesContract = 1;
    } else {
      $("input[id^='cbSalesContract_']").prop("checked", false);
      flagSalesContract = 0;
    }
  });
  $("#cbSalesGoods").on("click", function () {
    if (flagSalesGoods === 0) {
      //把所有复选框选中
      $("input[id^='cbSalesGoods_']").prop("checked", true);
      flagSalesGoods = 1;
    } else {
      $("input[id^='cbSalesGoods_']").prop("checked", false);
      flagSalesGoods = 0;
    }
  });
  $("#cbCheckoutContract").on("click", function () {
    if (flagCheckoutContract === 0) {
      //把所有复选框选中
      $("input[id^='cbCheckoutContract_']").prop("checked", true);
      flagCheckoutContract = 1;
    } else {
      $("input[id^='cbCheckoutContract_']").prop("checked", false);
      flagCheckoutContract = 0;
    }
  });
  $("#cbCheckoutRecord").on("click", function () {
    if (flagCheckoutRecord === 0) {
      //把所有复选框选中
      $("input[id^='cbCheckoutRecord_']").prop("checked", true);
      flagCheckoutRecord = 1;
    } else {
      $("input[id^='cbCheckoutRecord_']").prop("checked", false);
      flagCheckoutRecord = 0;
    }
  });
  $("#cbInventoryContract").on("click", function () {
    if (flagInventoryContract === 0) {
      //把所有复选框选中
      $("input[id^='cbInventoryContract_']").prop("checked", true);
      flagSalesContract = 1;
    } else {
      $("input[id^='cbInventoryContract_']").prop("checked", false);
      flagInventoryContract = 0;
    }
  });
  $("#cbInventoryRecord").on("click", function () {
    if (flagInventoryRecord === 0) {
      //把所有复选框选中
      $("input[id^='cbInventoryRecord_']").prop("checked", true);
      flagInventoryRecord = 1;
    } else {
      $("input[id^='cbInventoryRecord_']").prop("checked", false);
      flagInventoryRecord = 0;
    }
  });
  $("#cbSysAdmin").on("click", function () {
    if (flagSysAdmin === 0) {
      //把所有复选框选中
      $("input[id^='cbSysAdmin_']").prop("checked", true);
      flagSysAdmin = 1;
    } else {
      $("input[id^='cbSysAdmin_']").prop("checked", false);
      flagSysAdmin = 0;
    }
  });
  $("#cbSalesCompany").on("click", function () {
    if (flagSalesCompany === 0) {
      //把所有复选框选中
      $("input[id^='cbSalesCompany_']").prop("checked", true);
      flagSalesCompany = 1;
    } else {
      $("input[id^='cbSalesCompany_']").prop("checked", false);
      flagSalesCompany = 0;
    }
  });

  $("#btnAdd").on("click", function () {
    var bool = true;
    if (!validateUsername()) {
      bool = false;
    }
    if (!validateUsernameRename()) {
      bool = false;
    }
    if (!validatePasswordAdd()) {
      bool = false;
    }
    if (!validatePassword2Add()) {
      bool = false;
    }
    return bool;
  });

  $("#btnEdit").on("click", function () {
    var bool = true;
    if (!validatePasswordEdit()) {
      bool = false;
    }
    if (!validatePassword2Edit()) {
      bool = false;
    }
    return bool;
  });

  $("#btnLogin").on("click", function () {
    var bool = true;
    if (!validateUsername()) {
      bool = false;
    }
    if (!validatePasswordAdd()) {
      bool = false;
    }
    return bool;
  });

});