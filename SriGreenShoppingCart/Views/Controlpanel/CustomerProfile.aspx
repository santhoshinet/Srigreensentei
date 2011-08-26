<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="CCAvenueIntegrationDL" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ViewCustomerProfile</title>
     <link href="/Content/stylesheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="loginContent">
        <%
            var customerList = (List<User>)ViewData["CustomerFullDetail"];
            if (customerList.Count > 0)
            {
%>
        <div>
             <h4>Username</h4>
             <div class="editor-label">
                <%= customerList[0].Username %>
             </div>
             <h4>Email</h4>
             <div class="editor-label">
                <%= customerList[0].Email %>
             </div>
             <h4>Mobile</h4>
             <div class="editor-label">
                <%= customerList[0].Mobileno %>
             </div>
             <h4>Billing Address</h4>
             <div class="editor-label">
                <%= customerList[0].BillingAddress %>
             </div>
             <div class="editor-label">
                <%= customerList[0].BillingCity %>
             </div>
             <div class="editor-label">
                <%= customerList[0].BillingState %>
             </div>
             <div class="editor-label">
                <%= customerList[0].BillingCountry %>
             </div>
             <div class="editor-label">
                <%= customerList[0].BillingPin %>
             </div>
             <div class="editor-label">
                <%= customerList[0].BillingFaxno %>
             </div>
             <h4>Delivery   Address</h4>
             <div class="editor-label">
                <%= customerList[0].DeliveryAddress %>
             </div>
             <div class="editor-label">
                <%= customerList[0].DeliveryCity %>
             </div>
             <div class="editor-label">
                <%= customerList[0].DeliveryState %>
             </div>
             <div class="editor-label">
                <%= customerList[0].DeliveryCountry %>
             </div>
             <div class="editor-label">
                <%= customerList[0].DeliveryPin %>
             </div>
        </div>
      <%
            }
            else
            {%>
                <div class="container">
            <div class="middle">
               Customer profile not found may be invalid parameter passed.</div>
        </div>
            <%
            }%>
    </div>
</body>
</html>
