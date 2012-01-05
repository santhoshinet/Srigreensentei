<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="CCAvenueIntegrationDL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Gift-Plants
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Content/special_page_products.css" rel="stylesheet" type="text/css" />
    <div class="Content">
        <ul>
            <li>
                <h2>
                    Gift A Plant</h2>
            </li>
            <li>
                <p>
                    A gift is a thing which is being presented to lovable one for many occasions to
                    make the moment immemorable.Now there are many kind of gifts are available in the
                    market. But are they make the moment memorable and as well as worthy? Here is the
                    answer with us.
                </p>
            </li>
            <li>
                <p>
                    We offer the very best kind of Product which is making your Gift as very perfect
                    also each and every one love it.
                </p>
            </li>
            <li>
                <p>
                    We have a wide range of Gift Plants Collections with many varieties which can be
                    selected by the customer and they can be ordered with us and most of all they are
                    at very much affordable cost tag. It will be delivered with neatly packed with gift
                    wrap and it is the Live plant and will be handy size which can be hand gifted to
                    lovable ones to make their environment lively.
                </p>
            </li>
            <li>
                <p>
                    Make your environment Green By developing the habit of gifting the live Plant.
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