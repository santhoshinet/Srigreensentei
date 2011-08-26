<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	PaymentOptions
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <div class="loginContent">
        <h2>Choose your payment mode.</h2>
        <p>How do you want to make your payments with us.</p>
        <div class="container">
            <div class="left">
                <a href="javascript:alert('Online payment has been disabled temporarly.');">Pay now Online</a></div> 
            <div class="right">
                <a href="/ShoppingCart/Offlinepayment">Check/DD/Cash Payment</a>
            </div>
        </div>
    </div>
</asp:Content>
