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
        List of transactions.</h3>
    <div class="searchbox">
        Search
        <input type="text" id="txtsearchcriteria" />
    </div>
    <table id="Cart_Table" class="Cart_Table">
        <thead>
            <tr>
                <th style="width: 30px;">
                    Sno
                </th>
                <th style="width: 140px;">
                    Username
                </th>
                <th style="width: 800px;">
                    Transaction Details
                </th>
            </tr>
        </thead>
        <tbody>
            <%
                foreach (var customer in customerList)
                {%>
            <tr id='<%= customer.Username %>'>
                <td>
                    <%= index++%>
                </td>
                <td>
                    <%= customer.Username%>
                </td>
                <td>
                    <% if (customer.PreTransactionDetailses != null && customer.PreTransactionDetailses.Count > 0)
                       {
                           int subindex = 0;
                    %>
                    <table class="Cart_Table_Sub">
                        <thead>
                            <th style="width: 30px;">
                                Sno
                            </th>
                            <th style="width: 100px;">
                                Transaction-ID
                            </th>
                            <th style="width: 90px;">
                                Created Date
                            </th>
                            <th style="width: 60px;">
                                Cart size
                            </th>
                            <th style="width: 60px;">
                                Status
                            </th>
                            <th style="width: 90px;">
                                Actions
                            </th>
                            <th style="width: 150px;">
                                Modify transaction
                            </th>
                        </thead>
                        <tbody>
                            <%
                                foreach (var preTransactionDetailse in customer.PreTransactionDetailses)
                                {
                                    subindex++;
                            %>
                            <tr>
                                <td>
                                    <%= subindex %>
                                </td>
                                <td>
                                    <%= preTransactionDetailse.TransactionId %>
                                </td>
                                <td>
                                    <%= preTransactionDetailse.EntryTime.ToString("dd MMM yyyy") %>
                                </td>
                                <td>
                                    <%= preTransactionDetailse.MyCarts.Count %>
                                </td>
                                <td>
                                    <%= preTransactionDetailse.TransactionStatus.ToString() %>
                                </td>
                                <td>
                                    <a href="/ShoppingCart/ViewCart/<%= preTransactionDetailse.TransactionId %>">View Cart</a>
                                </td>
                                <td>
                                    <select>
                                        <option>--Select--</option>
                                        <option>Pending</option>
                                        <option>Success</option>
                                        <option>Clear cart</option>
                                    </select>
                                    <input type="button" value="Update" title="Update" />
                                </td>
                            </tr>
                            <%
                                }%>
                        </tbody>
                    </table>
                    <%
                        }
                       else
                       {%>
                    - - - - - - - - - - - - - - - - - - - - - Still products are not added into cart.
                    - - - - - - - - - - - - - - - - - - - - -
                    <%
                        }%>
                </td>
            </tr>
            <%
                }%>
        </tbody>
    </table>
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