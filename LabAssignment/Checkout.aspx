<%@ Page Title="Checkout" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="LabAssignment.Checkout" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container"> 
  
    <br/>   
    <br/>
        <table runat="server" class="table table-dark table-striped table-bordered container" style="max-width:fit-content;">
            <tr>
           <td colspan="2">
               <h3>
                   Checkout
               </h3>
           </td>
       </tr>
            <tr>
                <td style="text-align:left;" colspan="2">
               <h5>
                   Address
               </h5>
                    <asp:TextBox runat="server" placeholder="Street address or P.O Box" ID="Address1">

               </asp:TextBox><br />
                    <asp:TextBox runat="server" placeholder="Apt, Suite, Unit, Building (optional)" ID="Address2">

               </asp:TextBox><br />
                    <asp:TextBox runat="server" placeholder="City" ID="Address3">
                        </asp:TextBox><br />
                    <asp:TextBox runat="server" placeholder="ZIP Code" ID="Address4">
               </asp:TextBox>
           </td>
            </tr>
        <tr>
            <td>
                <asp:Table runat="server" ID="stressTable" CssClass="table h-100 table-dark table-striped table-bordered" BorderStyle="Solid" ForeColor="WhiteSmoke"  BorderWidth="3px" >

                </asp:Table>
           </td>
       </tr>
            <tr>
            <td style="text-align:right;" colspan="2">
                <asp:Button runat="server" Text="Go" CssClass="btn btn-outline-warning" ID="ProcessOrder" OnClick="ProcessOrder_Click"/>
                </td>
            
            </tr>
        </table>
        <br />
        <br />
        </div>
</asp:Content>
