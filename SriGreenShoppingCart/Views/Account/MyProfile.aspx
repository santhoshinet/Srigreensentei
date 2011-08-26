﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SriGreenShoppingCart.Models.RegisterModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    My Profile
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="loginContent">
        <h2>
            My Profile</h2>
        <% using (Html.BeginForm())
           { %>
        <div>
            <fieldset>
                <legend>Account Information</legend>
                <%= Html.ValidationSummary(true, "Profile updation was unsuccessful. Please correct the errors and try again.") %>
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.Email) + "*"%>
                </div>
                <div class="editor-field">
                    <%= Html.TextBoxFor(m => m.Email) %>
                    <%= Html.ValidationMessageFor(m => m.Email) %>
                </div>
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.Mobile) + "*"%>
                </div>
                <div class="editor-field">
                    <%= Html.TextBoxFor(m => m.Mobile)%>
                    <%= Html.ValidationMessageFor(m => m.Mobile)%>
                </div>
                <p>
                    Input Billing Address</p>
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.BillingAddress) + "*"%>
                </div>
                <div class="editor-field">
                    <%= Html.TextAreaFor(m => m.BillingAddress)%>
                    <%= Html.ValidationMessageFor(m => m.BillingAddress)%>
                </div>
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.BillingCity) + "*"%>
                </div>
                <div class="editor-field">
                    <%= Html.TextBoxFor(m => m.BillingCity)%>
                    <%= Html.ValidationMessageFor(m => m.BillingCity)%>
                </div>
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.BillingState) + "*"%>
                </div>
                <div class="editor-field">
                    <%= Html.TextBoxFor(m => m.BillingState)%>
                    <%= Html.ValidationMessageFor(m => m.BillingState)%>
                </div>
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.BillingCountry) + "*"%>
                </div>
                <div class="editor-field">
                    <%= Html.TextBoxFor(m => m.BillingCountry)%>
                    <%= Html.ValidationMessageFor(m => m.BillingCountry)%>
                </div>
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.BillingPin) + "*"%>
                </div>
                <div class="editor-field">
                    <%= Html.TextBoxFor(m => m.BillingPin)%>
                    <%= Html.ValidationMessageFor(m => m.BillingPin)%>
                </div>
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.BillingFaxno)%>
                </div>
                <div class="editor-field">
                    <%= Html.TextBoxFor(m => m.BillingFaxno)%>
                    <%= Html.ValidationMessageFor(m => m.BillingFaxno)%>
                </div>
                <div>
                    Is delivery address is same as above? <span>
                        <input type="radio" name="yes" checked="true" />
                        Yes ,<input type="radio" name="no" />
                        No </span>
                </div>
                <p class="delivery">
                </p>
                <p class="delivery">
                    Input Delivery Address</p>
                <div class="editor-label delivery">
                    <%= Html.LabelFor(m => m.DeliveryAddress)%>
                </div>
                <div class="editor-field delivery">
                    <%= Html.TextAreaFor(m => m.DeliveryAddress)%>
                    <%= Html.ValidationMessageFor(m => m.DeliveryAddress)%>
                </div>
                <div class="editor-label delivery">
                    <%= Html.LabelFor(m => m.DeliveryCity)%>
                </div>
                <div class="editor-field delivery">
                    <%= Html.TextBoxFor(m => m.DeliveryCity)%>
                    <%= Html.ValidationMessageFor(m => m.DeliveryCity)%>
                </div>
                <div class="editor-label delivery">
                    <%= Html.LabelFor(m => m.DeliveryState)%>
                </div>
                <div class="editor-field delivery">
                    <%= Html.TextBoxFor(m => m.DeliveryState)%>
                    <%= Html.ValidationMessageFor(m => m.DeliveryState)%>
                </div>
                <div class="editor-label delivery">
                    <%= Html.LabelFor(m => m.DeliveryCountry)%>
                </div>
                <div class="editor-field delivery">
                    <%= Html.TextBoxFor(m => m.DeliveryCountry)%>
                    <%= Html.ValidationMessageFor(m => m.DeliveryCountry)%>
                </div>
                <div class="editor-label delivery">
                    <%= Html.LabelFor(m => m.DeliveryPin)%>
                </div>
                <div class="editor-field delivery">
                    <%= Html.TextBoxFor(m => m.DeliveryPin)%>
                    <%= Html.ValidationMessageFor(m => m.DeliveryPin)%>
                </div>
                <p>
                    <input type="submit" value="Update my profile" />
                    <input type="button" value="Cancel" onclick="javascript:window.location='/Home/Index';" />
                    <%= Html.HiddenFor(m => m.UserName) %>
                    <%= Html.HiddenFor(m => m.Password) %>
                    <%= Html.HiddenFor(m => m.ConfirmPassword) %>
                </p>
            </fieldset>
        </div>
        <% } %>
    </div>

    <script src="/Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>

    <script src="/Scripts/register.js" type="text/javascript"></script>

</asp:Content>