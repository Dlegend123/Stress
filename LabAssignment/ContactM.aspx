<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ContactM.aspx.cs" Inherits="LabAssignment.ContactM" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="container h-100">
        <br />
    <br />
    <asp:Table runat="server" CssClass="table">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell>
                <h1 style="color:whitesmoke">Contact Us</h1>
            </asp:TableHeaderCell>
        </asp:TableHeaderRow>
    <asp:TableRow>
        <asp:TableCell>
    <asp:Table runat="server" CssClass="table table-dark table-striped table-bordered container">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell  CssClass="rounded shadow" BorderWidth="3px" BorderStyle="Solid">
                <h4 class="title">Legacy Industries </h4>
                <p class="info">Game Consoles, Monitors, Data Storage</p>
                <ul class="list-group"  style="list-style:none">
						<li class="time">
                            <img src="Images/Contact/clock.svg" class="img-fluid" />
                            <strong>&nbsp;Time :&nbsp;

                            </strong>
                            <span class="info"> Monday to Sunday 8 AM – 6 PM CST
						</span>

						</li>
						                                                                               
						<li class="bi bi-telephone">
                            <img src="Images/Contact/telephone.svg" class="img-fluid" />
                            <strong>&nbsp;Telephone :&nbsp;

                            </strong>
                            <span class="info">
                                <a href="tel:876-683-0000" style="text-decoration:none">876-683-0000</a>

                            </span>

						</li>
						                                                                                  
					</ul>
            </asp:TableHeaderCell>
            
        </asp:TableHeaderRow>
        <asp:TableRow>
            <asp:TableCell  CssClass="rounded shadow" BorderWidth="3px">
                <img class="img-fluid"  src="Images/Contact/live-chat_desktop.svg"/>
              <h3 class="mt-2">Chat with Us</h3>
              <div class="justify-content-center">
                <a href="#" target="_blank" style="text-decoration:none">LI Online Chat</a>
              </div>
            </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
            <asp:TableCell  CssClass="rounded shadow" BorderWidth="3px">
                <img class="img-fluid"  src="Images/Contact/warranty-information-l.svg"/>
               <h3 class="mt-2">Warranty Information</h3>
              <div>
                <a href="#" target="_blank" style="text-decoration:none">Warranty Status</a>
                  <br />
                <a href="#" target="_blank" style="text-decoration:none">Purchase Extended Warranty</a>
              </div>
            </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow >
            <asp:TableCell  CssClass="rounded shadow" BorderWidth="3px">
                <img class="img-fluid"  src="Images/Contact/request-a-repair-l.svg"/>              
               <h3 class="mt-2">Repair Services</h3>
              <div class="justify-content-center">
                <a href="#" target="_blank" style="text-decoration:none">Request a Repair</a>
                <br />
                  <a href="#" style="text-decoration:none" target="_blank">Track a Repair</a>
              <br />
                  <a href="#" style="text-decoration:none" target="_blank">Find a Service Center</a>
              </div>
            </asp:TableCell>
            </asp:TableRow >
            <asp:TableRow >
            <asp:TableCell  CssClass="rounded shadow" BorderWidth="3px">
              <img class="img-fluid" src="Images/Contact/center-spot.svg" />               
               <h3 class="mt-2">Find Help with Your Order</h3>
              <div class="justify-content-center">
                <a href="#" target="_blank" style="text-decoration:none">Check Order Status</a>
                  <br />
                <a href="#" target="_blank" style="text-decoration:none">Order FAQs</a>
              </div>
            </asp:TableCell>
            </asp:TableRow >
            <asp:TableRow >
            <asp:TableCell  CssClass="rounded shadow" BorderWidth="3px">
                <img class="img-fluid"  src="Images/Contact/Users-01.svg" style="max-height:8vh;min-height:inherit;"/>              
              <h3 class="mt-2">Contact us on Social Media</h3>
              <div class="justify-content-center">
                <p>Tweet us <a href="#" target="_blank" style="text-decoration:none">@LISupport</a><br>
                Message us on Facebook <a href="#/" target="_blank" style="text-decoration:none">@LISupport</a></p>
              </div>
            </asp:TableCell>
            </asp:TableRow >
            <asp:TableRow >
            <asp:TableCell  CssClass="rounded shadow" BorderWidth="3px">
                <img src="Images/Contact/email_desktop.svg" class="img-fluid"  />
              <h3 class="mt-2">Email Us</h3>
              <div class="justify-content-center">
                  <br />
                <a href="#" target="_blank" style="text-decoration:none">Email Customer Support</a>
              licustomersupport@gmail.com
              </div>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
        </asp:Table>
    </div>
    


</asp:Content>
