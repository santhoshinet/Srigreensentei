<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SriGreenShoppingCart.Models.LatestnewsEventsModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Latest News and Events
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="loginContent">
        <div class="controlpanel_home">
            <a href="/controlpanel">go home</a>
        </div>
        <h2>
            Manage your News and Events</h2>
        <% using (Html.BeginForm())
           { %>
        <div>
            <fieldset>
                <legend>News and Events</legend>
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

    <script src="/Scripts/jquery-1.6.1.min.js" type="text/javascript" charset="utf-8"></script>

    <script src="/Scripts/jquery-ui-1.8.13.custom.min.js" type="text/javascript" charset="utf-8"></script>

    <link rel="stylesheet" href="/Content/smoothness/jquery-ui-1.8.13.custom.css" type="text/css"
        media="screen" charset="utf-8" />

    <script src="/Scripts/elrte.min.js" type="text/javascript" charset="utf-8"></script>

    <link rel="stylesheet" href="/Content/elrte.min.css" type="text/css" media="screen"
        charset="utf-8" />

    <script src="/Scripts/i18n/elrte.ru.js" type="text/javascript" charset="utf-8"></script>

    <script type="text/javascript" charset="utf-8">
        $().ready(function() {
            var opts = {
                cssClass: 'el-rte',
                // lang     : 'ru',
                height: 450,
                toolbar: 'complete',
                cssfiles: ['css/elrte-inner.css']
            }
            $('.textarea').elrte(opts);
        })
	</script>

</asp:Content>