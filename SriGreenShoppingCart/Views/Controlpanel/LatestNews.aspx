<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SriGreenShoppingCart.Models.LatestnewsModel>"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Manage your latest news/updates
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="loginContent">
        <div class="controlpanel_home">
            <a href="/controlpanel">go home</a>
        </div>
        <h2>
            Manage your latest news/updates</h2>
        <% using (Html.BeginForm())
           { %>
        <div>
            <fieldset>
                <legend>News or Updates</legend>
                <%= Html.ValidationSummary(true, "Unable to save news/updates. Please correct the errors and try again.") %>
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.Latestnews) %>
                </div>
                <div class="editor-field">
                    <%= Html.TextAreaFor(m => m.Latestnews,new {@class = "textarea"}) %>
                    <%= Html.ValidationMessageFor(m => m.Latestnews) %>
                </div>
                <p>
                    <input type="submit" value="Update" />
                </p>
            </fieldset>
        </div>
        <% } %>
        <div class="controlpanel_home">
            <a href="/controlpanel">go home</a>
        </div>
    </div>
</asp:Content>