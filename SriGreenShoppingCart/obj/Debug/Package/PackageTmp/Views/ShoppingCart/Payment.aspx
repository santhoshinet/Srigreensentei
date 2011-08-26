<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="CCAvenueIntegrationDL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Payment
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="loginContent">
        <%
            var result = (CCAvenueIntegrationDL.FromCcAvenue.TransactionStatus)ViewData["result"];%>
        <h4>
            <%
                if (result == FromCcAvenue.TransactionStatus.Success)
                {
            %>
            Thank you for your shopping with us. Your transaction has been processed successfully.
            Your products will be delivered to you within 15 business days.
            <%
                }
                else if (result == FromCcAvenue.TransactionStatus.Declined)
                {%>
            Thank you for your shopping with us. Your transaction has been declined.
            <%
                }
                else if (result == FromCcAvenue.TransactionStatus.PostPoned)
                {%>
            Thank you for your shopping with us. Your transaction has been post poned.
            <%
                }
                else
                {%>
            Unable to process your request.
            <%
                }%></h4>
        <h5>
            For more queries contact us by dropping a mail to <a href="mailto:enquery@srigreensentei.com">
                enquery@srigreensentei.com</a>.</h5>
    </div>
</asp:Content>