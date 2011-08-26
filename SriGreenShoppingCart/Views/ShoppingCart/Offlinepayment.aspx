<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Offlinepayment
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="loginContent">
    <ul>
        <li><h2>Cheque/DD/Cash payment</h2></li>
        <li></li>
        <li>Transfer funds through Net Banking (or) 
You may deposit the Cash at any KARNATAKA BANK branches in India
(or) You may deposit the Cheque /DD at any KARNATAKA BANK branches in India</li>
        <li><p>Note: Please include your transaction-ID with your payment.</p></li>
        <li><h3>Your cart details</h3></li>
        <li><p>Total amount : Rs. <b><%= ViewData["TotalAmount"] %></b>/-   <a href="/ShoppingCart/ViewCart" >View my cart</a></p></li>
        <li><p>Your transaction-ID : <b><%= ViewData["TransactionID"] %></b></p></li>
        <li><h3>Account Details</h3></li>
        <li>
            <p>
                Bank Name:      KARNATAKA BANK <br />
                Account Tye:    Current <br />
                Account No:     1612000100011401 <br />
                Account Branch: West Mambalam <br />
                IFSC CODE:      KAB0000161
            </p>
        </li>
        <li><h3>Address</h3></li>
        <li>
                <p>
                    SRI GREEN SENTEI<br />
                    G&amp;H Udhayam Towers,<br />
                    No.17, P.T. Rajan Salai,
                    <br />
                    KK Nagar, Chennai - 600078.<br />
                    <br />
                    Tel : +91 44 24899943
                    <br />
                    Fax: +91 44 42077128<br />
                    <br />
                    Email id: admin@srigreensentei.com
                   </p>
        </li>
       
    </ul>
    </div>
    <script src="/Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    
    <script src="/Scripts/jquery.mousewheel-3.0.4.pack.js" type="text/javascript"></script>

    <script src="/Scripts/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>

    <script src="/Scripts/OfflinePayment.js" type="text/javascript"></script>
</asp:Content>
