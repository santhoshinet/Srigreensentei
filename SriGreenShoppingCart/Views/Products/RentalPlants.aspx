<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="CCAvenueIntegrationDL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    RentalPlants
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Content/special_page_products.css" rel="stylesheet" type="text/css" />
    <div class="Content">
        <ul>
            <li>
                <h2>
                    Rental Plants</h2>
            </li>
            <li>
                <p>
                    We at Sri Green Sentei we do rent out very attractive plants for the arrangements
                    in party, Functions and Mall exhibitions and all to make the surrounding green and
                    lush appearance. Many customers prefer this kind of set up. We do give concepts
                    as well as rent out the plants for many functions and occations.<br />
            </li>
            <li>
                <h3>
                    Contact for rates and other conditions</h3>
            </li>
            <li>
                <p>
                    SRI GREEN SENTEI<br />
                    G&amp;H Udhayam Towers,<br />
                    No.17, P.T. Rajan Salai,
                    <br />
                    KK Nagar, Chennai - 600078.<br />
                    <br />
                    Tel : +91 44 24899943
                    <br />
                    Fax: +91 44 42077128<br />
                    <br />
                    Email id: admin@srigreensentei.com<br />
                    web: www.srigreensentei.com</p>
            </li>
            <li><p></p></li>
            <% if (((List<Product>)ViewData["Products"]).Count > 0)
               {%>
            <li>
                <h2>
                    Rental Plants</h2>
            </li>
            <li>
                <% Html.RenderPartial("products_list");%>
                <% Html.RenderPartial("loadingpanel"); %>

                <script src="../../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>

                <script src="/Scripts/jquery.mousewheel-3.0.4.pack.js" type="text/javascript"></script>

                <script src="/Scripts/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>

                <script src="../../Scripts/ShopppingCart.js" type="text/javascript"></script>

                <script src="../../Scripts/products_lazy_loading.js" type="text/javascript"></script>

            </li>
            <%
                }%>
        </ul>
    </div>
</asp:Content>