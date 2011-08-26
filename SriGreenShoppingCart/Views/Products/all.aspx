<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="CCAvenueIntegrationDL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Shop your plant
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="Content">
        <ul>
            <li>
                <h2>
                    Available plants with us</h2>
            </li>
            <% var categiries = (List<Category>)ViewData["categories"];%>
            <% if (categiries.Count > 0)
               {
                   foreach (var category in categiries)
                   {

            %>
            <li>
                <h4>
                    <a href="/Products/<%= category.Name %>">
                        <%= category.Name %>
                    </a>
                </h4>
            </li>
            <li>
                <p>
                    <%= category.Description %>
                </p>
            </li>
            <%
                }
               }%>
        </ul>
    </div>
</asp:Content>