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
                <h3>Our inauguration photos</h3>
                <p><img src="/Images/DSC_0012.jpg" width="600px" height="400px" alt="" /></p>
                <p><img src="/Images/DSC_0014.jpg" width="600px" height="400px" alt="" /></p>
                <p><img src="/Images/DSC_0039.jpg" width="600px" height="400px" alt="" /></p>
                <p><img src="/Images/DSC_0068.jpg" width="600px" height="400px" alt="" /></p>
                <p><img src="/Images/DSC_0076.jpg" width="600px" height="400px" alt="" /></p>
                <p><img src="/Images/DSC_0147.jpg" width="600px" height="400px" alt="" /></p>
                <p><img src="/Images/DSC_0163.jpg" width="600px" height="400px" alt="" /></p>
                <p><img src="/Images/DSC_0247.jpg" width="600px" height="400px" alt="" /></p>
                <p><img src="/Images/DSC_0297.jpg" width="600px" height="400px" alt="" /></p>
                <p><img src="/Images/DSC_0317.jpg" width="600px" height="400px" alt="" /></p>
                <p><img src="/Images/DSC_0324.jpg" width="600px" height="400px" alt="" /></p>
                <p><img src="/Images/DSC_0329.jpg" width="600px" height="400px" alt="" /></p>
                <p><img src="/Images/DSC_0397.jpg" width="600px" height="400px" alt="" /></</p>
                <%--
                    <%= ViewData["LatestNews"] %>
                    <iframe frameborder="0" height="500" width="870" scrolling="auto" src="/Home/NewsandEvents"
                    style="padding-left: 20px;"></iframe>--%>
            </li>
        </ul>
    </div>
</asp:Content>