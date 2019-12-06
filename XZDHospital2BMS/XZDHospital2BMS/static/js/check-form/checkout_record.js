// 检查货品名称和厂家名称不能都为空
function checkNotNull() {
  var bool = true;
  if ($("#tbProductName").val() === "" && $("#tbFactoryName").val() === "") {
    alert("货品名称和厂家名称不能都为空！");
    bool = false;
  }
  return bool;
}

// 添加到出库清单的ajax操作
function addGoods(intCheckoutContractId, intGoodsId, tbAmount) {
  var intAmount = $("#" + tbAmount + "").val();
  if (!(intAmount > 0)) {
    alert("提货数必须大于0！");
    return false;
  }
  var formData = new FormData();
  formData.append("CheckoutContractId", intCheckoutContractId);
  formData.append("GoodsId", intGoodsId);
  formData.append("Amount", intAmount);
  $.ajax({
    url: "/Handler/AddCheckoutGoods.ashx",
    type: 'POST',
    dataType: 'json',
    data: formData,
    // 不指定编码方式（默认指定编码 urlencode）
    processData: false,
    // 不处理数据
    contentType: false,
    success: function (result) {
      alert(result.StatusCode);
      var code = result.StatusCode;
      var msg = result.Message;
      if (code === "200") {
        alert("已添加！");
        return true;
      } else if (code === "500") {
        alert(msg);
        return false;
      }
    },
    error: function (error) {
      console.log(error);
      return error;
    }
  });
}