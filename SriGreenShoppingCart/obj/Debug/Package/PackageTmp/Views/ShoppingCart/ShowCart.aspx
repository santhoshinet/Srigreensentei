<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="CCAvenueIntegrationDL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Your Cart
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="loginContent">
        <%
            var products = (List<Product>) ViewData["Cart"];
            int index = 1;
            double TotalAmount = 0.0;%>
       <% if (products.Count > 0)
          {
              %>
          <h3>Here is your shopping cart.</h3> 
          <form method="post" action="Checkout">
        <table>
            <thead>
                <tr>    
                    <th>Sno</th>
                    <th style="width:700px">Name</th>
                    <th style="width:80px">Price</th>
                    <th>Quantity</th>
                    <th style="width:80px">Amount</th>
                </tr>
            </thead>
            <tbody>
        <%
              foreach (var product in products)
              {%>
        <tr>
            <td><%=index++%></td>
            <td><%=product.Name%></td>
            <td>Rs. <%=product.Price%></td>
            <td> <input type="text" value='<%=product.Quanity%>' style="width:30px" name='<%= product.Name %>' /></td>
            <td>Rs. <%=product.Quanity*product.Price%></td>
            <%
                  TotalAmount += (product.Quanity*product.Price);%>
        </tr>
<%
              }%>
    <tr>
        <td colspan="4">Total</td>
        <td>Rs. <%=TotalAmount%> /-</td>
    </tr>
    </tbody>
</table>
    <div class="checkout">
        <input type="submit" title="Check out" value="Check out" />
    </div>
    </form>
    <%
          }
          else
          {%>
            <h3>Your Shopping Cart is empty.</h3>  
          <%}%>
    </div>
</asp:Content>