<%@ Page Title="Manage Products" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageProduct.aspx.cs" Inherits="LabAssignment.ManageProduct" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid"> 
    <br />  
    <table runat="server" class="table table-dark table-striped table-bordered container-fluid" style="border-width:3px;border-style:solid;color:whitesmoke;max-width:50vw;max-height:60vh;">
      <tr>
          <th colspan="2">
              <ul class="navbar-nav me-auto mb-2 mb-lg-0" style="text-align:center">

              <li class="nav-item">
              <h3>
                  Product
              </h3>
                  </li>
                  <li style="text-align:right;" class="nav-item">
              <asp:DropDownList runat="server">
                  <asp:ListItem Text="Add">

                  </asp:ListItem>
                  <asp:ListItem Text="Edit">

                  </asp:ListItem>
              </asp:DropDownList>
                      </li>
                  </ul>
          </th>
      </tr> 
      <tr>
          <td>

          <table runat="server" id="ManageTable" class="table table-dark table-striped table-bordered container-fluid" style="border-width:3px;border-style:solid;color:whitesmoke;max-width:50vw;max-height:60vh;">
    <tr>
           <td style="text-align:left">
               <h5>
                  ID#
               </h5>
               <asp:TextBox  runat="server" ID="ProductID" >

               </asp:TextBox>
           </td>
           <td style="text-align:left;">
               <h5>
                   Name
               </h5>
               <asp:TextBox  runat="server" ID="ProductName">

               </asp:TextBox>
           </td>
       </tr>
        <tr>
            <td style="text-align:left;">
               <h5>
                   Category
               </h5>
                <asp:TextBox  runat="server" ID="ProductCat">

               </asp:TextBox>
           </td>
            <td style="text-align:left;">
               <h5>
                   Price
               </h5>
                <asp:TextBox  runat="server" ID="ProductPrice">

               </asp:TextBox>
           </td>
       </tr>
        <tr>
            <td style="text-align:left;" colspan="2">
                <h5>
                    Details
                </h5>
                <asp:TextBox  runat="server" ID="ProductDetail">

                </asp:TextBox>
           </td>
            
       </tr>
        <tr>
            <td colspan="2" style="text-align:left" >
                <h5>
                    Video
                </h5>
                <asp:FileUpload ID="ProductVid" runat="server" />

            </td>
        </tr>
        <tr>
            <td style="text-align:left;" colspan="2">
               <h5>
                   Quantity
               </h5>
                <asp:TextBox  runat="server" ID="ProductQuantity">

               </asp:TextBox>
           </td>
            
       </tr>
        <tr>
            <td style="text-align:left;" colspan="2">
               <h5>
                   Images
               </h5>
                <asp:FileUpload runat="server" ID="ProductImage1" ></asp:FileUpload>
                <br />
                <asp:FileUpload runat="server" ID="ProductImage2" ></asp:FileUpload>
                <br />
                <asp:FileUpload runat="server" ID="ProductImage3" ></asp:FileUpload>
           </td>
        </tr>
        <tr>
            <td style="text-align:left;">
               <h5>
                   Page(Desktop)
               </h5>
                <asp:TextBox  runat="server" ID="ProductDesk"></asp:TextBox>
                </td>
                <td style="text-align:left;">
                <h5>
                   Page(Mobile)
               </h5>
                    <asp:TextBox  runat="server" ID="ProductMob"></asp:TextBox>
                
            </td>
       </tr>
              </table>
        </td>
        </tr>
        <tr>
            <td style="text-align:left;" colspan="2">
                <asp:Button runat="server" Text="Go" CssClass="btn btn-outline-warning" ID="ProductAdd" OnClick="ProductAdd_Click"/>
                </td>
            
        </tr>
        </table>
        <br />
        <table runat="server" class="table table-dark table-striped table-bordered container-fluid" style="border-width:3px;border-style:solid;color:whitesmoke;max-width:50vw;max-height:60vh;">
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
