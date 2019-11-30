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
            <li class="active"><a href="/BackManager/home.aspx">系统首页</a></li>
            <li class="dropdown">
              <a class="dropdown-toggle" data-toggle="dropdown" href="#">入库管理<span class="caret"></span></a>
              <ul class="dropdown-menu">
                <li><a href="/BackManager/sales_contract/add.aspx">添加入库单</a></li>
                <li><a href="/BackManager/sales_contract/list.aspx">管理入库单</a></li>
                <li><a href="/BackManager/sales_company/list.aspx">管理医药销售公司</a></li>
              </ul>
            </li>
            <li class="dropdown">
              <a class="dropdown-toggle" data-toggle="dropdown" href="#">出库管理<span class="caret"></span></a>
              <ul class="dropdown-menu">
                <li><a href="#">添加出库单</a></li>
                <li><a href="#">管理出库单</a></li>
              </ul>
            </li>
            <li class="dropdown">
              <a class="dropdown-toggle" data-toggle="dropdown" href="#">盘点管理<span class="caret"></span></a>
              <ul class="dropdown-menu">
                <li><a href="#">添加盘点单</a></li>
                <li><a href="#">管理盘点单</a></li>
              </ul>
            </li>
            <li class="dropdown">
              <a class="dropdown-toggle" data-toggle="dropdown" href="#">系统设置<span class="caret"></span></a>
              <ul class="dropdown-menu">
                <li><a href="/BackManager/admin/add.aspx">添加管理员</a></li>
                <li><a href="/BackManager/admin/list.aspx">管理管理员</a></li>
                <li><a href="/Handler/LogoutHandler.ashx">退出重登录</a></li>
              </ul>
            </li>
          </ul>
        </div>

      </div>

    </div>

  </div>

</div>
