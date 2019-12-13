<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucHeader.ascx.cs"
  Inherits="XZDHospital2BMS.BackManager.WUCHeader" %>

<div class="wrapper-header">

  <div class="container">

    <div class="row">

      <div class="col-xs-6">

        <div class="wrapper-logo">
          <img src="/static/image/logo.png" alt="logo" />
        </div>

      </div>

      <div class="col-xs-6">

        <div class="wrapper-nav">
          <ul class="nav nav-pills">
            <li role="presentation"><a href="/BackManager/home.aspx">系统首页</a></li>
            <li class="dropdown">
              <a class="dropdown-toggle" data-toggle="dropdown" href="#">系统管理<span class="caret"></span></a>
              <ul class="dropdown-menu">
                <li><a href="/BackManager/sales_contract/list.aspx">管理入库单</a></li>
                <li><a href="/BackManager/checkout_contract/list.aspx">管理出库单</a></li>
                <li><a href="/BackManager/inventory_contract/list.aspx">管理盘点单</a></li>
                <asp:Literal runat="server" ID="ltrSuperAdmin" Visible="false" />
              </ul>
            </li>
            <li class="dropdown">
              <a class="dropdown-toggle" data-toggle="dropdown" href="#">系统设置<span class="caret"></span></a>
              <ul class="dropdown-menu">
                <li><a href="/BackManager/sales_company/list.aspx">管理医药销售公司</a></li>
                <li><a href="/BackManager/department/list.aspx">管理科室名称</a></li>
                <asp:Literal runat="server" ID="ltrAdmin" Visible="false" />
                <li><a href="/Handler/LogoutHandler.ashx">退出重登录</a></li>
              </ul>
            </li>
          </ul>
        </div>

      </div>

    </div>

  </div>

</div>
