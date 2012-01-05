<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="CCAvenueIntegrationDL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    All-Garden-Needs
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Content/special_page_products.css" rel="stylesheet" type="text/css" />
    <div class="Content">
        <ul>
            <li>
                <h2>
                    All Garden Needs</h2>
            </li>
            <li>
                <p>
                    Apart from Plants a Garden need many components to make the garden more aesthetic
                    and lovable. So In Sri Green Sentei, We have all types of Garden Components such
                    as various pots, Mowers, Garden Tools, Organic Manures, Hardscape Items etc
                </p>
            </li>
            <li>
                <ul>
                    <li>
                        <h3>
                            A. Vertical Gardening</h3>
                    </li>
                    <li>
                        <p>
                            A perfect self watering garden on the wall either in outdoor or Indoor we provide
                            them in your premises readymade .the edge cut new technology help us to bring the
                            garden in the walls or we can say garden as the wall in our home a great inspiring
                            garden we make.</p>
                    </li>
                    <li>
                        <h3>
                            B. Lawns</h3>
                    </li>
                    <li>
                        <p>
                            We have readymade lawn varieties of Korean grass, Mexican Grass, Munda grass and
                            Shade Grass verities which can be developed as such in your garden.</p>
                    </li>
                    <li>
                        <h3>
                            C. Pots &amp; Planters</h3>
                        <ul>
                            <li>1. Mud Pots </li>
                            <li>2. Regular Cement Pots</li>
                            <li>3. Self Moisturing Pots</li>
                            <li>4. Imported Ceramic Pots</li>
                            <li>5. Imported Porcelain Pots</li>
                        </ul>
                    </li>
                    <li>
                        <h3>
                            D. Organic Manures Plant Nutrients</h3>
                    </li>
                    <li>
                        <p>
                            We have full range of Garden Foods right from Organic manures to All Plant nutrients
                            which will enrich the garden soil as well as good growth and maintenance of garden.
                            We also have all pest control products.
                        </p>
                    </li>
                    <li>
                        <h3>
                            E. Garden Tools
                        </h3>
                    </li>
                    <li>
                        <p>
                            To maintain a garden in good way a proper intercultural operations are needed on
                            regular intervals. So for that operations a proper set of garden tools are necessary.
                            Hence we have all range of Garden Tools and accessories for sales.
                        </p>
                    </li>
                    <li>
                        <ul>
                            <li>1. Lawn Mowers</li>
                            <li>2. Secateurs and Garden scissors</li>
                            <li>3. Weed cutters&amp; Bush Cutters</li>
                            <li>4. Plant trimmers. etc.</li>
                        </ul>
                    </li>
                    <li>
                        <h3>
                            F. Concrete Pavers
                        </h3>
                    </li>
                    <li>
                        <p>
                            We have all types of pre cast concrete pavers with good quality which is being used
                            in pathways.</p>
                    </li>
                    <li>
                        <h3>
                            G. Natural Rough Granite Slabs</h3>
                    </li>
                    <li>
                        <p>
                            Natural Rough granites have been dressed with many types and these granites can
                            be used in pathways and wall cladding, paving, Garden Benches and so on.</p>
                    </li>
                    <li>
                        <h3>
                            H. Irrigation Equipments</h3>
                    </li>
                    <li>
                        <p>
                            A Well maintained garden needed a proper irrigation set up such as Sprinklers and
                            Drip irrigation systems. We have all range of these products in our Shop.</p>
                    </li>
                    <li>
                        <h3>
                            I. Fountains</h3>
                    </li>
                    <li>
                        <p>
                            We have all range of Fountains components right from Floor fountains, Wall Hanging
                            Fountains, cascades, Mini Table top fountains.</p>
                    </li>
                    <li>
                        <h3>
                            J. Garden Lights</h3>
                    </li>
                    <li>
                        <p>
                            We have all types of Lights which is needed to arrange in the garden as per the
                            customer’s needs. All the products are world class quality products.</p>
                    </li>
                    <li>
                        <h3>
                            K. Children Play Equipments
                        </h3>
                    </li>
                    <li>
                        <p>
                            We have all range of Children play equipments such as Slides, Swings, Garden Benches,
                            Garden Umberallas, and many more.</p>
                    </li>
                </ul>
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