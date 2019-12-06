using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
  public class BllGoods
  {

    
    public static decimal getInventory(string strProductName, string strFactoryName, string strType)
    {
      // 库存量的计算方式是从入库货品表里查找所有【货品名称、厂家名称、规格】完全一致的货品的数量
      // 相加得到这个货品的总量，然后再从出库货品表里查找相应的货品的出货数量总量
      // 两者相减就得到了这种货品的库存量
      return 0;
    }

  }

}
