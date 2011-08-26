<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="CCAvenueIntegrationDL" %>
    <div class="loginContent">
        <div class="controlpanel_home">
            <a href="/controlpanel">go home</a>
        </div>

        <%
            var customerList = (List<User>)ViewData["CustomerList"];
            int index = 1; %>
        <% if (customerList.Count > 0)
           {
        %>
        <h3>
            List of customers.</h3>
        <div class="searchbox">
            Search
            <input type="text" id="txtsearchcriteria" />
        </div>
        <form method="post" action="Checkout">
        <table id="Cart_Table" class="Cart_Table">
            <thead>
                <tr>
                    <th style="width:30px;">
                        Sno
                    </th>
                    <th style="width:140px;">
                        Name
                    </th>
                    <th style="width:220px;">
                        Email
                    </th>
                    <th style="width:100px;">
                        Mobile
                    </th>
                    <th style="width:120px;">
                        Address
                    </th>
                    <th style="width:120px;">
                        City
                    </th>
                    <th style="width:75px;">
                        Actions
                    </th>
                </tr>
            </thead>
            <tbody>
                <%
                    foreach (var customer in customerList)
                    {%>
                <tr id='<%= customer.Username %>'>
                    <td><%= index++%></td>
                    <td><%= customer.Username%></td>
                    <td><%= customer.Email %></td>
                    <td><%= customer.Mobileno %></td>
                    <td><%= customer.BillingAddress %></td>
                    <td><%= customer.BillingCity %></td>
                    <td><a href="/controlpanel/customerprofile/<%= customer.Username %>" class="ViewProfile" >Full Profile</a></td>
                </tr>
                <%
                    }%>
                <tr id="noresultsrow">
                    <td colspan="6">
                        There is no result found your query.
                    </td>
                </tr>
            </tbody>
        </table>
        </form>
        <%
            }
           else
           {%>
        <h3>
            There is no Customer registered yet.</h3>
        <%}%>
        <div class="controlpanel_home">
            <a href="/controlpanel">go home</a>
        </div>
    </div>
   
    
    