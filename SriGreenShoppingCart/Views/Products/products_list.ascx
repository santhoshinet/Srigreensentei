<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="CCAvenueIntegrationDL" %>
<%
    var products = (List<Product>)ViewData["Products"];
    int index = 1;
    if (products.Count > 0)
    {
%>
<div class="full-width">
    <% foreach (var product in products)
       { %>
    <div class="one-fourth">
        <div class="img-sha-217">
            <a class="fancybox zoom-image" href="/Products/Photo/<%=product.Productfile.ID%>">
                <img src="/Products/Photo/<%=product.Productfile.ID%>" title="<%= product.Name %>"
                    alt="<%= product.Name %>" /></a>
        </div>
        <h5>
            <%= product.Name %></h5>
        <p class="prod_desc">
            <%= product.Description %></p>
        <h6>
            MRP - Rs.<%= product.Price.ToString() %>/-</h6>
        <div class="add_cart_btn">
            <img src="/Images/product-images/icon/add.png" alt="" product_id="<%= product.Id.ToString() %>" /></div>
    </div>
    <div id="pageReference">
        <%= "/Products/" + ViewData["Category"] %></div>
    <%
        } %>
</div>
<%
    }
    else
    { %>
<div class="loginContent">
    <div class="container">
        <div class="middle">
            The category[<strong><%= ViewData["Category"].ToString()%></strong>] contains empty
            product list.</div>
    </div>
</div>
<%
    }%>