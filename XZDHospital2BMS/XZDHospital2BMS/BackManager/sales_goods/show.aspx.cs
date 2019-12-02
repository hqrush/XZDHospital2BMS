using Bll;
using Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XZDHospital2BMS.BackManager.sales_goods
{

  public partial class show : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {

      if (!IsPostBack)
      {
        int intAdminId = HelperUtility.hasPurviewPage("SalesGoods_show");
        int intId = HelperUtility.getQueryInt("id");

        ModelSalesGoods model = BllSalesGoods.getById(intId);
        lblProductName.Text = model.name_product;
        lblType.Text = model.type;
        lblFactoryName.Text = model.name_factory;
        lblUnit.Text = model.unit;
        lblAmount.Text = model.amount.ToString();
        lblPriceUnit.Text = model.price_unit.ToString("C");
        lblPriceTotal.Text = model.price_total.ToString("C");
        lblBatchNumber.Text = model.batch_number;
        lblValidityPeriod.Text = model.validity_period.ToString();
        lblComment.Text = model.comment;
        // 设置照片
        string strPhotoUrls = model.photo_urls;
        if (!"".Equals(strPhotoUrls))
        {
          string strImgUrl, strJS;
          List<string> listPhotoUrls = strPhotoUrls.Split(',').ToList();
          for (int i = 0; i < listPhotoUrls.Count; i++)
          {
            strImgUrl = listPhotoUrls[i];
            strJS = "<div id=\"img-" + i + "\" class=\"wrapper-photo-show\">";
            strJS += "<a href=\"" + strImgUrl + "\">";
            strJS += "<img width=\"100\" height=\"100\" src=\"" + strImgUrl + "\" />";
            strJS += "</a></div>";
            ltrShowPhoto.Text += strJS;
          }
        }
      }
    }

  }

}