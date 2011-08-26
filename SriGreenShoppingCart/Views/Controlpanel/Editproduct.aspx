<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SriGreenShoppingCart.Models.ProductModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Editproduct</title>
    <link href="/Content/stylesheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="loginContent">
        <% using (Html.BeginForm("Editproduct", "Controlpanel", FormMethod.Post, new { enctype = "multipart/form-data" }))
           {%>
        <div>
            <fieldset>
                <legend>Edit product</legend>
                <div>
                    <%=Html.ValidationSummary(true,"Unable to add product. Please correct the errors and try again.")%>
                </div>
                <div class="editor-label">
                    Product Name
                </div>
                <div class="editor-field">
                    <%=Html.TextBoxFor(m => m.ProductName)%>
                    <%=Html.ValidationMessageFor(m => m.ProductName)%>
                </div>
                <div class="editor-label">
                    Product Description
                </div>
                <div class="editor-field">
                    <%=Html.TextAreaFor(m => m.ProductDescription)%>
                    <%=Html.ValidationMessageFor(m => m.ProductDescription)%>
                </div>
                <div class="editor-label">
                    Product Price
                </div>
                <div class="editor-field">
                    <%=Html.TextBoxFor(m => m.ProductPrice)%>
                    <%=Html.ValidationMessageFor(m => m.ProductPrice)%>
                </div>
                <div class="editor-label">
                    Product Picture/Image [Optional] [Ignore if you want to use previous image]
                </div>
                <div class="editor-field">
                    <input type="file" name="image" />
                    <%=Html.ValidationMessageFor(m => m.Image)%>
                </div>
                <p>
                    <%= Html.HiddenFor(m => m.Id) %>
                </p>
                <p>
                    <input type="submit" value="Edit product" />
                </p>
            </fieldset>
        </div>
        <%
            }%>
    </div>
</body>
</html>