function checkNameTime() {
  var strName = $('#tbCompanyName').val();
  if (strName === "") {
    alert("公司名不能为空！");
    return false;
  }
  var strTime = $('#tbTimeSign').val();
  if (strTime === "") {
    alert("入库时间不能为空！");
    return false;
  }
  // 验证时间格式是否正确：（验证通过返回时间戳格式,例如:（2017-01-01,2017,-,01,-,01),否则返回null）
  strTime = strTime.match(/^(\d{4})(-)(\d{2})(-)(\d{2})$/);
  if (strTime === null) {
    alert("请输入正确的时间格式，如：2019-01-01");
    return false;
  }
  // 验证时间是否合法：(注意：此段必须放置在验证时间格式完成之后)
  var b_d = new Date(strTime[1], strTime[3] - 1, strTime[5]);
  var b_num = b_d.getFullYear() === strTime[1] &&
    (b_d.getMonth() + 1) === strTime[3] &&
    b_d.getDate() === strTime[5];
  if (b_num === 0) {
    alert("请输入正确的时间，如：2019-01-01");
    return false;
  }
}

function selectOnChang(obj) {
  // var value = obj.options[obj.selectedIndex].value;
  var text = obj.options[obj.selectedIndex].text;
  $('#tbCompanyName').val(text);
}