// 验证表单
function validForm() {
  var cbUnitName1 = document.getElementById("cbUnitName1");
  var cbUnitName2 = document.getElementById("cbUnitName2");
  console.log(cbUnitName1);
  console.log(cbUnitName2.checked);
  if (!cbUnitName1.checked && !cbUnitName2.checked) {
    alert("申请单位至少选择一个！");
    return false;
  }
  var strDepartmentName = $('#tbDepartmentName').val();
  if (strDepartmentName === "") {
    alert("申请部门不能为空！");
    return false;
  }
  var strSignName = $('#tbSignName').val();
  if (strSignName === "") {
    alert("申请人不能为空！");
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