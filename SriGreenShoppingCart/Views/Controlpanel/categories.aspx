<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SriGreenShoppingCart.Models.CategoriesModel>" %>

<%@ Import Namespace="CCAvenueIntegrationDL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Categories
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="loginContent">
        <div class="controlpanel_home">
            <a href="/controlpanel">go home</a>
        </div>
        <%
            var categiries = (List<Category>)ViewData["categories"];
            int index = 1; %>
        <% if (categiries.Count > 0)
           {
        %>
        <h3>
            List of categories.</h3>
        <div class="searchbox">
            Search
            <input type="text" id="txtsearchcriteria" />
        </div>
        <form method="post" action="Checkout">
        <table id="Cart_Table">
            <thead>
                <tr>
                    <th>
                        Sno
                    </th>
                    <th style="width: 40%">
                        Name
                    </th>
                    <th style="width: 40%">
                        Description
                    </th>
                    <th style="width: 160px" colspan="2">
                        Actions
                    </th>
                </tr>
            </thead>
            <tbody>
                <%
                    foreach (var category in categiries)
                    {%>
                <tr id='<%= category.Id %>'>
                    <td>
                        <%=index++%>
                    </td>
                    <td>
                        <%=category.Name%>
                    </td>
                    <td>
                        <%=category.Description%>
                    </td>
                    <% if (category.Categorytype == Categorytype.General)
                       {%>
                    <td>
                        <span class="delete_button">
                            <img src="/Images/ico-delete.gif" />
                            delete</span>
                    </td>
                    <td>
                        <span class="edit_button">
                            <img src="/Images/edit.gif" />
                            edit</span>
                    </td>
                    <%
                        }
                       else
                       {%>
                    <td colspan="2">
                        --[Special]--
                    </td>
                    <%
                        }%>
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
            There is no category created yet.</h3>
        <%}%>
        <% using (Html.BeginForm())
           { %>
        <div>
            <fieldset>
                <legend>Add new category</legend>
                <%= Html.ValidationSummary(true, "Category creation was unsuccessful. Please correct the errors and try again.") %>
                <div class="editor-label">
                    Name
                </div>
                <div class="editor-field">
                    <%= Html.TextBoxFor(m => m.CategoryName)%>
                    <%= Html.ValidationMessageFor(m => m.CategoryName)%>
                </div>
                <div class="editor-label">
                    Description
                </div>
                <div class="editor-field">
                    <%= Html.TextAreaFor(m => m.CategoryDescription)%>
                    <%= Html.ValidationMessageFor(m => m.CategoryDescription)%>
                </div>
                <p>
                </p>
                <p>
                    <input type="submit" value="Create" />
                </p>
            </fieldset>
        </div>
        <% } %>
        <div class="controlpanel_home">
            <a href="/controlpanel">go home</a>
        </div>
    </div>

    <script src="/Scripts/jquery-1.6.1.min.js" type="text/javascript"></script>

    <script src="/Scripts/jquery.quicksearch.js" type="text/javascript"></script>

    <script src="/Scripts/jquery.highlight-3.js" type="text/javascript"></script>

    <script src="/Scripts/categories.js" type="text/javascript"></script>

</asp:Content>