<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageProduct.aspx.cs" Inherits="LabAssignment.ManageProduct" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container h-100"> 
    <br />  
    <asp:table runat="server" CssClass="table table-dark table-striped table-bordered container h-100 w-50" BorderStyle="Solid" ForeColor="WhiteSmoke"  BorderWidth="3px" >
      <asp:TableHeaderRow>
          <asp:TableHeaderCell ColumnSpan="2">
              <h3>
                  Product
              </h3>
          </asp:TableHeaderCell>
      </asp:TableHeaderRow> 
      <asp:TableRow>
           <asp:TableCell HorizontalAlign="Left">
               <h5>
                  ID#
               </h5>
               <asp:TextBox runat="server" ID="ProductID" >

               </asp:TextBox>
           </asp:TableCell>
           <asp:TableCell HorizontalAlign="Left">
               <h5>
                   Name
               </h5>
               <asp:TextBox runat="server" ID="ProductName">

               </asp:TextBox>
           </asp:TableCell>
       </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Left">
               <h5>
                   Category
               </h5>
                <asp:TextBox runat="server" ID="ProductCat">

               </asp:TextBox>
           </asp:TableCell>
            <asp:TableCell HorizontalAlign="Left">
               <h5>
                   Price
               </h5>
                <asp:TextBox runat="server" ID="ProductPrice">

               </asp:TextBox>
           </asp:TableCell>
       </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Left">
                <h5>
                    Details
                </h5>
                <asp:TextBox runat="server" ID="ProductDetail">

                </asp:TextBox>
           </asp:TableCell>
            <asp:TableCell HorizontalAlign="Left">
                <h5>
                    Video
                </h5>
                <asp:FileUpload ID="ProductVid" runat="server" />
            </asp:TableCell>
       </asp:TableRow>
        
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Left">
               <h5>
                   Quantity
               </h5>
                <asp:TextBox runat="server" ID="ProductQuantity">

               </asp:TextBox>
           </asp:TableCell>
            <asp:TableCell HorizontalAlign="Left">
               <h5>
                   Images
               </h5>
                <asp:FileUpload runat="server" ID="ProductImage1" ></asp:FileUpload>
                <br />
                <asp:FileUpload runat="server" ID="ProductImage2" ></asp:FileUpload>
                <br />
                <asp:FileUpload runat="server" ID="ProductImage3" ></asp:FileUpload>
           </asp:TableCell>
       </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Left">
               <h5>
                   Page(Desktop)
               </h5>
                <asp:TextBox runat="server" ID="ProductDesk"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">
                <h5>
                   Page(Mobile)
               </h5>
                    <asp:TextBox runat="server" ID="ProductMob"></asp:TextBox>
                </asp:TableCell>
       </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right" ColumnSpan="2">
                <asp:Button runat="server" Text="Go" CssClass="btn btn-outline-warning" ID="ProductAdd" OnClick="ProductAdd_Click"/>
                </asp:TableCell>
            
        </asp:TableRow>
        </asp:table>
        <br />
        <asp:table runat="server" CssClass="table table-dark table-striped table-bordered container h-100" BorderStyle="Solid" ForeColor="WhiteSmoke"  BorderWidth="3px">
       <asp:TableHeaderRow>
           <asp:TableHeaderCell ColumnSpan="2">
               <h3>
                   Carousel
               </h3>
           </asp:TableHeaderCell>
       </asp:TableHeaderRow>
         <asp:TableRow>
           <asp:TableCell HorizontalAlign="Left">
               
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
           </asp:TableCell>
            <asp:TableCell HorizontalAlign="Left">
               <h5>
                   Image
               </h5>
                <asp:FileUpload runat="server" ID="CarouselImage"></asp:FileUpload>
           </asp:TableCell>
       </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Left">
               <h5>
                   Page(Desktop)
               </h5>
                <asp:TextBox runat="server" ID="CarouselDesk" >

                </asp:TextBox>
                </asp:TableCell>
            <asp:TableCell HorizontalAlign="Left">
                <h5>
                   Page(Mobile)
               </h5>
                <asp:TextBox runat="server" ID="CarouselMob">

                </asp:TextBox>
           </asp:TableCell>
       </asp:TableRow>
            <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right" ColumnSpan="2">
                <asp:Button runat="server" Text="Go" CssClass="btn btn-outline-warning" ID="CarouselAdd" OnClick="CarouselAdd_Click"/>
                </asp:TableCell>
            
            </asp:TableRow>
        </asp:table>
        </div>
</asp:Content>
