<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="CCAvenueIntegrationDL" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ProductList</title>
</head>
<body>
    <div class="controlpanel_home">
        <a href="/controlpanel">go home</a>
    </div>
    <%
        var products = (List<Product>)ViewData["ProductList"];
        int index = 1;
    %>
    <table>
        <% foreach (var product in products)
           {
        %>
        <tr id="<%= product.Id %>">
            <td>
                <%= index %>
            </td>
            <% if (product.Productfile != null)
               {%>
            <td>
                <img alt="" class="product_image" src="/Products/Photo/<%=product.Productfile.ID%>"
                    title="<%= product.Name %>" />
            </td>
            <%
                }
               else
               {%>
            <td>
                <img alt="" src="#" />
            </td>
            <%
                }%>
            <td>
                <%= product.Name %>
            </td>
            <td>
                <%= product.Description %>
            </td>
            <td>
                <%= product.Price %>
            </td>
            <td style="width: 150px">
                <span class="delete_button">
                    <img src="/Images/ico-delete.gif" />
                    delete</span>
            </td>
            <td style="width: 100px">
                <span class="edit_button" href="<%= "/Controlpanel/editproduct/" + product.Id %>">
                    <img src="/Images/edit.gif" />
                    edit</span>
            </td>
        </tr>
        <%
            index = index + 1;%>
        <%
            } %>
        <tr id="noresultsrow">
            <td colspan="6">
                There is no result found your query.
            </td>
        </tr>
    </table>
    <div class="controlpanel_home">
        <a href="/controlpanel">go home</a>
    </div>
</body>
</html>