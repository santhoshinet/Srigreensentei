<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="CCAvenueIntegrationDL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ExoticPlants
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Content/special_page_products.css" rel="stylesheet" type="text/css" />
    <div class="Content">
        <ul>
            <li>
                <h2>
                    Exotic Plants</h2>
            </li>
            <li>
                <p>
                    In Sri Green Sentei We have all types of Plants in our nursery. Apart from those
                    all Indigenous plants, we have many varieties of Exotic Plants to cater the needs
                    rare plants collection lovers, we have carefully collected and brought from various
                    parts of the world.viz.,China,Thailand,Malaysia,Cyprus,Holland etc.</p>
            </li>
            <li>
                <p>
                    In Exotic Plants We have many collection such as Many Varieties of Bonsais, Lucky
                    Bamboos, Various Money Plants, Cactus, Orchids, Exotic Flower Hybrids, Exotic Water
                    Plants., etc
                </p>
            </li>
            <% if (((List<Product>)ViewData["Products"]).Count > 0)
               {%>
            <li>
                <h2>
                    Exotic Plants</h2>
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