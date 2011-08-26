<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SriGreenShoppingCart.Models.ProductModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Products
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="loginContent">
        <div class="controlpanel_home">
            <a href="/controlpanel">go home</a>
        </div>
        <h3>
            List of products.</h3>
        <div class="searchbox">
            Search
            <input type="text" id="txtsearchcriteria" />
        </div>
        <div class="rightbox">
            <%= Html.DropDownList("Category",Model.CategoryName) %>
            <a href="#">display</a></div>
        <table id="Cart_Table">
            <thead>
                <tr>
                    <th style="width: 60px">
                        Sno
                    </th>
                    <th style="width: 150px;">
                        Photo
                    </th>
                    <th style="width: 400px">
                        Name
                    </th>
                    <th style="width: 450px">
                        Description
                    </th>
                    <th style="width: 150px">
                        Price/each
                    </th>
                    <th colspan="2">
                        Actions
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr class="empty-row">
                    <td colspan="7">
                        Please select category and click 'display' from the above to view list of products.
                    </td>
                </tr>
            </tbody>
        </table>
        <% using (Html.BeginForm("Products", "Controlpanel", FormMethod.Post, new { enctype = "multipart/form-data" }))
           {%>
        <div>
            <fieldset>
                <legend>Add new product</legend>
                <div>
                    <%=Html.ValidationSummary(true,"Unable to add product. Please correct the errors and try again.")%>
                </div>
                <div class="editor-label">
                    Category
                </div>
                <div class="editor-field">
                    <%= Html.DropDownList("CategoryName",Model.CategoryName) %>
                    <%=Html.ValidationMessageFor(m => m.CategoryName)%>
                </div>
                <div class="editor-label">
                    Product Name
                </div>
                <div class="editor-field">
                    <%=Html.TextBoxFor(m => m.ProductName)%>
                    <%=Html.ValidationMessageFor(m => m.ProductName)%>
                </div>
                <div class="editor-label">
                    Product Description
                </div>
                <div class="editor-field">
                    <%=Html.TextAreaFor(m => m.ProductDescription)%>
                    <%=Html.ValidationMessageFor(m => m.ProductDescription)%>
                </div>
                <div class="editor-label">
                    Product Price
                </div>
                <div class="editor-field">
                    <%=Html.TextBoxFor(m => m.ProductPrice)%>
                    <%=Html.ValidationMessageFor(m => m.ProductPrice)%>
                </div>
                <div class="editor-label">
                    Product Picture/Image
                </div>
                <div class="editor-field">
                    <input type="file" name="image" />
                    <%=Html.ValidationMessageFor(m => m.Image)%>
                </div>
                <p>
                </p>
                <p>
                    <input type="submit" value="Add product" />
                </p>
            </fieldset>
        </div>
        <%
            }%>
        <div class="controlpanel_home">
            <a href="/controlpanel">go home</a>
        </div>
    </div>

    <script src="/Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>

    <script src="/Scripts/jquery.mousewheel-3.0.4.pack.js" type="text/javascript"></script>

    <script src="/Scripts/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>

    <script src="/Scripts/jquery.highlight-3.js" type="text/javascript"></script>

    <script src="/Scripts/jquery.quicksearch.js" type="text/javascript"></script>
    
    <script src="/Scripts/Products.js" type="text/javascript"></script>

</asp:Content>