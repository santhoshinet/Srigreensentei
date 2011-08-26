<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="loginContent">
        <h2>
            Control panel</h2>
        <p>
            Manage your site content.</p>
        <div class="container">
            <div class="left">
                <a href="/Controlpanel/categories">Manage Categories</a></div>
            <div class="right">
                <a href="/Controlpanel/products">Manage Products</a>
            </div>
            <div class="right">
                <a href="/Controlpanel/CustomerList">Manage Customers</a>
            </div>
            <div class="right">
                <a href="/Controlpanel/TransactionList">Manage Transactions</a>
            </div>
            <div class="right">
                <a href="/Controlpanel/LatestNews">Manage Latest news</a>
            </div>
            <div class="right">
                <a href="/Controlpanel/LatestNewsEvents">Manage News and Events</a>
            </div>
            <div class="right">
                <a href="/Controlpanel/DeletedCategories">Manage Deleted Categories</a>
            </div>
        </div>
    </div>
</asp:Content>