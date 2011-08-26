<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="CCAvenueIntegrationDL" %>
<div class="loginContent">
        <%
            var myCarts = (List<MyCart>)ViewData["Cart"];
            int index = 1;
            double TotalAmount = 0.0;%>
        <% if (myCarts.Count > 0)
           {
        %>
        <h3>
            Here is your shopping cart.</h3>
        <form method="post" action="PaymentOptions">
        <table id="Cart_Table">
            <thead>
                <tr>
                    <th>
                        Sno
                    </th>
                    <th style="width: 700px">
                        Name
                    </th>
                    <th style="width: 80px">
                        Price
                    </th>
                    <th>
                        Quantity
                    </th>
                    <th style="width: 120px">
                        Amount
                    </th>
                    <th style="width: 80px">
                    </th>
                </tr>
            </thead>
            <tbody>
                <%
                    foreach (var myCart in myCarts)
                    {%>
                <tr>
                    <td>
                        <%=index++%>
                    </td>
                    <td>
                        <%=myCart.Product.Name%>
                    </td>
                    <td>
                        Rs.
                        <%=myCart.Product.Price%>
                    </td>
                    <td>
                        <input type="text" value='<%=myCart.Quantity %>' price='<%= myCart.Product.Price %>'
                            amount='<%= myCart.Product.Price %>' style="width: 30px" name='<%= myCart.Product.Name %>' />
                    </td>
                    <td class="amount_td">
                        Rs.
                        <%=myCart.Quantity * myCart.Product.Price%>
                    </td>
                    <td>
                        <span id="<%= myCart.Product.Id %>" class="delete_button">
                            <img src="/Images/ico-delete.gif" />
                            delete</span>
                    </td>
                    <%
                        TotalAmount += (myCart.Quantity * myCart.Product.Price);%>
                </tr>
                <%
                    }%>
                <tr>
                    <td colspan="4">
                        Total
                    </td>
                    <td amount='<%=TotalAmount%>'>
                        Rs.
                        <%=TotalAmount%>/-
                    </td>
                    <td>
                    </td>
                </tr>
            </tbody>
        </table>
        <% if (Request.Url.LocalPath.ToLower() == "/shoppingcart/showcart")
{
%>
        <div class="checkout">
            <input type="submit" title="Check out" value="Purchase" />
        </div>
        <%
}%>
        </form>
        <%
            }
           else
           {%>
        <div class="container">
            <div class="middle">
                Your Shopping Cart is empty.</div>
        </div>
        <%}%>
    </div>

    <script src="/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="/Scripts/checkout.js" type="text/javascript"></script>
    