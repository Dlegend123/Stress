<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LabAssignment._Default" EnableEventValidation="false" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container h-100">
    <br />
    <br />
        <table class="table container" style="margin-left:auto;margin-right:auto;">
            <tr>
            <th class="text-center"><h1 style="color:whitesmoke">Featured Products</h1></th>
        </tr>
        </table>
    <table class="container">
        
<tr>
    <td>
        <div id="carouselExampleCaptions" class="carousel carousel-dark slide container-fluid" data-bs-ride="carousel" >
  <div class="carousel-indicators">
    <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
    <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="1" aria-label="Slide 2"></button>
    <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="2" aria-label="Slide 3"></button>
    <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="3" aria-label="Slide 4"></button>
  </div>
  <div class="carousel-inner">
    <div class="carousel-item active">
        <a id="Carousel1Link" runat="server" onserverclick="Carousel1Link_ServerClick">
          <!--  <img src="Images/steamDeck/Steam-Deck4K-Resolution.jpg" class="img-fluid" alt="...">-->
            <asp:Image CssClass="img-fluid" runat="server" ID="CarouselImg1" />
        </a>
      
      
    </div>
    <div class="carousel-item">
        <a id="Carousel2Link" runat="server" onserverclick="Carousel2Link_ServerClick">
           <!-- <img src="Images/switchOled/jVc4Rij62TqrC5AGnWRCoR.jpg" class="img-fluid" alt="...">-->
       <asp:Image CssClass="img-fluid" runat="server" ID="CarouselImg2" />
        </a>
      
      
    </div>
    <div class="carousel-item">
        <a id="Carousel3Link" runat="server" onserverclick="Carousel3Link_ServerClick">
           <!-- <img src="Images/Xbox/xbox_series_x_s_pre_order_packaging.jpg" class="img-fluid" alt="...">-->
        <asp:Image CssClass="img-fluid" runat="server" ID="CarouselImg3" />
        
        </a>
    </div>
      <div class="carousel-item">
          <a id="Carousel4Link" runat="server" onserverclick="Carousel4Link_ServerClick">
             <!-- <img src="Images/Playstation5/check.jpeg" class="img-fluid" alt="..."> -->
          <asp:Image CssClass="img-fluid" runat="server" ID="CarouselImg4"  />
        
          </a>      
    </div>
  </div>
  <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide="prev">
    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
    <span class="visually-hidden">Previous</span>
  </button>
  <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide="next">
    <span class="carousel-control-next-icon" aria-hidden="true"></span>
    <span class="visually-hidden">Next</span>
  </button>
</div>
 </td>
    </tr>
        </table>


    <br />
    <table class="table container table-dark table-striped table-bordered">
        <tr>
            <th style="text-align:center"><h2>New Products</h2></th>
            <th style="text-align:center"><h2>Current Deals</h2></th>
        </tr>
        <tr>
            <td style="font-size:larger">Steam Deck ready for preorder</td>
            <td style="font-size:larger">Both Xbox series X & S now 10% off</td>
        </tr>
        <tr>
            <td style="font-size:larger">Nintendo Switch Oled Model</td>
            <td style="font-size:larger">Both Playstation 5 versions now 10% off</td>
        </tr>
    </table>
            
    <br />
    <br />
        </div>
</asp:Content>
