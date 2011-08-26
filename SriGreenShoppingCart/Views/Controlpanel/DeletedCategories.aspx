<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="CCAvenueIntegrationDL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Categories Recyle Bin
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
            List of deleted categories.</h3>
        <table id="Cart_Table">
            <thead>
                <tr>
                    <th>
                        Sno
                    </th>
                    <th style="width: 75%">
                        Name
                    </th>
                    <th style="width: 200px" colspan="2">
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
                    <% if (category.Categorytype == Categorytype.General)
                       {%>
                    <td>
                        <span class="delete_button">
                            <img src="/Images/ico-delete.gif" />
                            delete</span>
                    </td>
                    <td>
                        <span class="restore_button">
                            <img src="/Images/edit.gif" />
                            restore</span>
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
            </tbody>
        </table>
        <%
            }
           else
           {%>
        <h3>
            There is no category in recycle bin.</h3>
        <%}%>
        <div class="controlpanel_home">
            <a href="/controlpanel">go home</a>
        </div>
    </div>

    <script src="/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="/Scripts/deletedcategories.js" type="text/javascript"></script>

</asp:Content>