<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    List of Customers
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.RenderPartial("Customer_List"); %>
    <% Html.RenderPartial("loadingpanel"); %>
    
    <script src="/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="/Scripts/jquery.mousewheel-3.0.4.pack.js" type="text/javascript"></script>

    <script src="/Scripts/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
    
    <script src="/Scripts/jquery.highlight-3.js" type="text/javascript"></script>   

    <script src="/Scripts/jquery.quicksearch.js" type="text/javascript"></script>

    <script src="/Scripts/CustomerList.js" type="text/javascript"></script>

    <script src="/Scripts/customerlist_lazy_loading.js" type="text/javascript"></script>

</asp:Content>