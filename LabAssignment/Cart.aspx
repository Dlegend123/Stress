<%@ Page Title="Cart" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="LabAssignment.Cart" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
    <br />
    <br />
    <asp:table ID="CartTable" runat="server" CssClass="table table-dark table-striped container" BorderStyle="Solid" ForeColor="WhiteSmoke"  BorderWidth="3px" >
       <asp:TableHeaderRow BorderStyle="Solid" BorderWidth="3px">
           <asp:TableHeaderCell>
               <h3 style="color:whitesmoke;text-align:center;">Shopping Cart</h3>
           </asp:TableHeaderCell>
       </asp:TableHeaderRow>
        <asp:TableRow>
            <asp:TableCell >
                <asp:Label runat="server" Text="GrandTotal: " Font-Size="Large" ForeColor="WhiteSmoke"></asp:Label>
                &nbsp;
                <asp:Label runat="server" ID="GrandTotal" BorderStyle="None" Font-Size="Large" BackColor="Transparent" ForeColor="WhiteSmoke">$0</asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" BorderStyle="Solid">
               <asp:Button CssClass="btn btn-outline-warning" runat="server" Text="Proceed to Checkout" ID="ProceedCheck" OnClick="ProceedCheck_Click" />

           </asp:TableCell>
       </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell BorderStyle="Solid" BorderWidth="3px">
                <asp:Table runat="server" ID="stressTable" CssClass="table table-dark table-striped table-bordered" BorderStyle="Solid" ForeColor="WhiteSmoke"  BorderWidth="3px" HorizontalAlign="Center">

                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:table>
        <br />
        <br />
        </div>
</asp:Content>
