// 验证表单
function validForm() {
  var tbNameSign = document.getElementById("tbNameSign");
  var strName = tbNameSign.value;
  if (strName === "") {
    alert("公司名不能为空！");
    return false;
  }
  var strTimeStart = $('#tbTimeStart').val();
  if (strTimeStart === "") {
    alert("开始时间不能为空！");
    return false;
  }
  var strTimeEnd = $('#tbTimeEnd').val();
  if (strTimeEnd === "") {
    alert("结束时间不能为空！");
    return false;
  }
  if (!validTime(strTimeStart)) {
    alert("请输入正确的开始时间，如：2019-01-01");
    return false;
  }
  if (!validTime(strTimeEnd)) {
    alert("请输入正确的结束时间，如：2019-01-01");
    return false;
  }
  var timeStart = new Date(strTimeStart);
  var timeEnd = new Date(strTimeEnd);
  if (timeStart >= timeEnd) {
    alert("结束时间应该大于开始时间！");
    return false;
  }
  return true;
}

function validTime(strTime) {
  // 验证时间格式是否正确：（验证通过返回时间戳格式,例如:（2017-01-01,2017,-,01,-,01)，否则返回null）
  strTime = strTime.match(/^(\d{4})(-)(\d{2})(-)(\d{2})$/);
  if (strTime === null) {
    return false;
  }
  // 验证时间是否合法：(注意：此段必须放置在验证时间格式完成之后)
  var b_d = new Date(strTime[1], strTime[3] - 1, strTime[5]);
  var b_num = b_d.getFullYear() === strTime[1] &&
    (b_d.getMonth() + 1) === strTime[3] &&
    b_d.getDate() === strTime[5];
  if (b_num === 0) {
    return false;
  }
  return true;
}

function selectOnChang(obj) {
  // var value = obj.options[obj.selectedIndex].value;
  var text = obj.options[obj.selectedIndex].text + "";
  if (text.startsWith("==")) {
    alert("请选择有效的申请部门！");
    return false;
  }
  $('#tbDepartmentName').val(text);
}