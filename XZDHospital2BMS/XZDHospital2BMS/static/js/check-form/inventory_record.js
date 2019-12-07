// 检查货品名称和厂家名称不能都为空
function checkNotNull() {
  var bool = true;
  if ($("#tbProductName").val() === "" && $("#tbFactoryName").val() === "") {
    alert("货品名称和厂家名称不能都为空！");
    bool = false;
  }
  return bool;
}

// 添加到盘点清单的ajax操作
function addGoods(intInventoryContractId, intGoodsId, lblInventory, tbAmount) {
  // Label控件转成span标签，取值不是用val()，而是用text()
  var intInventory = $("#" + lblInventory + "").text();
  var intAmount = $("#" + tbAmount + "").val();
  if (!(intAmount > 0)) {
    alert("提货数必须大于0！");
    return false;
  }
  if (intAmount > intInventory) {
    alert("库存不足，提货数不能大于库存！");
    return false;
  }
  var formData = new FormData();
  formData.append("InventoryContractId", intInventoryContractId);
  formData.append("GoodsId", intGoodsId);
  formData.append("AmountReal", intAmount);
  $.ajax({
    url: "/Handler/AddInventoryGoods.ashx",
    type: 'POST',
    dataType: 'json',
    data: formData,
    // 不指定编码方式（默认指定编码 urlencode）
    processData: false,
    // 不处理数据
    contentType: false,
    success: function (result) {
      var code = result.StatusCode;
      var msg = result.Message;
      if (code === "200") {
        intInventory = (intInventory - intAmount).toFixed(2);
        $("#" + lblInventory + "").html(intInventory);
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