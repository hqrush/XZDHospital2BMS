$(function () {
  $("#btnAdd").on("click", function () {
    var bool = true;
    if (!checkNotNull("tbNameReal", "姓名不能为空！"))
      bool = false;
    if (!checkNotNull("tbNameDisease", "疾病名称不能为空！"))
      bool = false;
    if (!checkNotNull("tbTimeIn", "入院日期不能为空！"))
      bool = false;
    if (!checkNotNull("tbTimeOut", "出院日期不能为空！"))
      bool = false;
    if (!checkTime("tbBirthday"))
      bool = false;
    if (!checkTime("tbTimeIn"))
      bool = false;
    if (!checkTime("tbTimeOut"))
      bool = false;
    return bool;
  });
});

// 检查输入框不能为空，id必须以td开头，如"tdUsername"
function checkNotNull(id, msg) {
  var value = $("#" + id).val();
  // 先清空显示错误信息的Element
  var eleError = "#" + id.substring(2) + "Error";
  $(eleError).text("");
  showError($(eleError));
  // 做判断
  if (!value) {
    // 如果为空，则设置显示错误信息的元素的值为错误信息
    $(eleError).text(msg);
    showError($(eleError));
    $("#" + id).focus();
    return false;
  }
  return true;
}

// 检查时间输入框的值是否为空，以及时间格式是否正确
function checkTime(id) {
  var strTime = $("#" + id).val();
  var eleError = "#" + id.substring(2) + "Error";
  $(eleError).text("");
  showError($(eleError));
  // 验证有没有输入时间值
  if (!strTime) {
    $(eleError).text('请输入正确的时间！');
    showError($(eleError));
    $("#" + id).focus();
    return false;
  }
  // 验证时间格式是否正确：（验证通过返回时间戳格式,例如:（2017-01-01,2017,-,01,-,01),否则返回null）
  strTime = strTime.match(/^(\d{4})(-)(\d{2})(-)(\d{2})$/);
  if (strTime === null) {
    $(eleError).text('请输入正确的时间格式，如：2019-01-01');
    showError($(eleError));
    $("#" + id).focus();
    return false;
  }
  // 验证时间是否合法：(注意：此段必须放置在验证时间格式完成之后)
  var b_d = new Date(strTime[1], strTime[3] - 1, strTime[5]);
  var b_num = b_d.getFullYear() === strTime[1] &&
    (b_d.getMonth() + 1) === strTime[3] &&
    b_d.getDate() === strTime[5];
  if (b_num === 0) {
    $(eleError).text('请输入正确的时间，如：2019-01-01');
    showError($(eleError));
    $("#" + id).focus();
    return false;
  }
  return true;
}

// 显示错误信息
function showError(element) {
  var text = element.text();
  if (!text) {
    element.css("display", "none");
  } else {
    element.css("display", "");
  }
}
