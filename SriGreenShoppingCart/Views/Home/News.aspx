<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    News
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="Content">
        <ul>
            <li>
                <h2>
                    News and Events</h2>
            </li>
            <li>
                <iframe frameborder="0" height="500" width="870" scrolling="auto" src="/Home/NewsandEvents"
                    style="padding-left: 20px;"></iframe>
            </li>
        </ul>
    </div>
</asp:Content>