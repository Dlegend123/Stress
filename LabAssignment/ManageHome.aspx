<%@ Page Title="Manage Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageHome.aspx.cs" Inherits="LabAssignment.ManageHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" runat="server" id="AdminHouse"> 
  
    <br/>   
    <br/>
        <table runat="server" class="table table-dark table-striped table-bordered container" style="max-width:fit-content;">
            <tr>
           <td colspan="2">
               <h3>
                   Carousel
               </h3>
           </td>
       </tr>
         <tr>
           <td style="text-align:left;" colspan="2">
               
               <h5>
                   Name
               </h5>
               <asp:DropDownList runat="server" ID="CarouselName">
                   <asp:ListItem Text="CarouselImg1" Selected="True">
                       
                   </asp:ListItem>
                   <asp:ListItem Text="CarouselImg2">

                   </asp:ListItem>
                   <asp:ListItem Text="CarouselImg3">

                   </asp:ListItem>
                   <asp:ListItem Text="CarouselImg4">

                   </asp:ListItem>
               </asp:DropDownList>
           </td>
            
       </tr>
            <tr>
                <td style="text-align:left;" colspan="2">
               <h5>
                   Image
               </h5>
                <asp:FileUpload runat="server" ID="CarouselImage"></asp:FileUpload>
           </td>
            </tr>
        <tr>
            <td style="text-align:left;">
               <h5>
                   Page(Desktop)
               </h5>
                <asp:TextBox  runat="server" ID="CarouselDesk" >

                </asp:TextBox>
                </td>
            <td style="text-align:left;">
                <h5>
                   Page(Mobile)
               </h5>
                <asp:TextBox  runat="server" ID="CarouselMob">

                </asp:TextBox>
           </td>
       </tr>
            <tr>
            <td style="text-align:left;" colspan="2">
                <asp:Button runat="server" Text="Go" CssClass="btn btn-outline-warning" ID="CarouselAdd" OnClick="CarouselAdd_Click"/>
                </td>
            
            </tr>
        </table>
        </div>
</asp:Content>
