<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    TransactionList
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.RenderPartial("Transaction_list"); %>
    <% Html.RenderPartial("loadingpanel"); %>

    <script src="/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="/Scripts/jquery.mousewheel-3.0.4.pack.js" type="text/javascript"></script>

    <script src="/Scripts/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>

    <script src="/Scripts/jquery.highlight-3.js" type="text/javascript"></script>

    <script src="/Scripts/jquery.quicksearch.js" type="text/javascript"></script>

    <script src="/Scripts/TransactionList.js" type="text/javascript"></script>

    <script src="/Scripts/TransactionList_lazy_loading.js" type="text/javascript"></script>

</asp:Content>