<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Controlpanel.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        FileUpload</h2>
    <% using (Html.BeginForm("Create", "Controlpanel", FormMethod.Post, new { enctype = "multipart/form-data" }))
       {%>
    <input type="text" name="Name" />
    <input type="text" name="Price" />
    <input name="uploadFile" type="file" />
    <input type="submit" value="Upload File" />
    <%} %>
</asp:Content>