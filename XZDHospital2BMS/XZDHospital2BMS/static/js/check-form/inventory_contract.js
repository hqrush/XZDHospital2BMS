// 验证表单
function validForm() {
  var tbNameSign = document.getElementById("tbNameSign");
  var strName = tbNameSign.value;
  if (strName === "") {
    alert("公司名不能为空！");
    return false;
  }
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