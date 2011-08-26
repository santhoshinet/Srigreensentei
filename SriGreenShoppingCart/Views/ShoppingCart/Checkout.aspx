<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Checkout
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="loginContent">
        <div class="container">
            <div class="middle">
                Redirecting to payment gateway, Please wait .....</div>
        </div>
    </div>
    <%= ViewData["PostBackData"] %>

    <script type="text/javascript" language="javascript">
        document.forms[0].submit();
    </script>

</asp:Content>