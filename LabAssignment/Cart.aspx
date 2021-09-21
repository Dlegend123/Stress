<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="LabAssignment.Cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:table ID="CartTable" runat="server" CssClass="table table-dark table-striped table-bordered container" BorderStyle="Solid" ForeColor="WhiteSmoke"  BorderWidth="3px" >
       <asp:TableHeaderRow BorderStyle="Solid" BorderWidth="3px">
           <asp:TableHeaderCell>
               <h3 style="color:whitesmoke">Shopping Cart</h3>
           </asp:TableHeaderCell>
       </asp:TableHeaderRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:TextBox runat="server" ID="GrandTotal"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" BorderStyle="Solid" ColumnSpan="2" >
               <asp:Button CssClass="btn btn-outline-warning" runat="server" Text="Proceed to checkout" ID="ProceedCheck" OnClick="ProceedCheck_Click" />

           </asp:TableCell>
       </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell BorderStyle="Solid" BorderWidth="3px" CssClass="container-fluid">
                <asp:Table runat="server" ID="stressTable" CssClass="table h-100" BorderStyle="Solid" ForeColor="WhiteSmoke"  BorderWidth="3px" >

                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:table>
</asp:Content>
