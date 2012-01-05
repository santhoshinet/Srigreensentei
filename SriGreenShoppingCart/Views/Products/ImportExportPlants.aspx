<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="CCAvenueIntegrationDL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ImportExportPlants
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Content/special_page_products.css" rel="stylesheet" type="text/css" />
    <div class="Content">
        <ul>
            <li>
                <h2>
                    Import and Export of Plants</h2>
            </li>
            <li>
                <p>
                    Sri Green Sentei is in Export and Import of Live plants and Trees. We do Import
                    of Rare Plants like Orhids, Japanese Bonsais, European Cactus, Many Flower seed
                    Bulbs and other Exotic Plants and water plants from Thailand. We do Import of many
                    garden tools and accessories, as well as rare pots and planters.</p>
            </li>
            <li>
                <p>
                    Sri Green Sentei is in Export of Live Trees like Phoenix Palms, Borasus Palms and
                    Coconut palms etc.Also we do export of Many Plants and Tree Species.
                </p>
            </li>
            <li>
                <h3>
                    Contact for your further Information and queries.</h3>
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
            <% if (((List<Product>)ViewData["Products"]).Count > 0)
               {%>
            <li>
                <h2>
                    Gift Plants</h2>
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