<%@ Page Title="Steam Deck" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SteamDeck.aspx.cs" Inherits="LabAssignment.SteamDeck" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <br />
        <br />
    <table>
        <tr>
        <td >
<div id="carousel6" class="carousel carousel-dark slide container-fluid" data-bs-ride="carousel"  >
  <div class="carousel-indicators">
    <button type="button" data-bs-target="#carousel6" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
    <button type="button" data-bs-target="#carousel6" data-bs-slide-to="1" aria-label="Slide 2"></button>
    <button type="button" data-bs-target="#carousel6" data-bs-slide-to="2" aria-label="Slide 3"></button>
    <button type="button" data-bs-target="#carousel6" data-bs-slide-to="3" aria-label="Slide 4"></button>
  </div>
  <div class="carousel-inner">  
    <div class="carousel-item active">
        <asp:Image CssClass="img-fluid" runat="server" ID="CarouselImg1" />
    <!--  <img src="../../Images/steamDeck/Gear-Steam.jpg" class="img-fluid" alt="...">-->
    </div>
      <div class="carousel-item">
          <asp:Image CssClass="img-fluid" runat="server" ID="CarouselImg2" />
    <!--  <img src="../../Images/steamDeck/Steam-Deck-Rear.jpg" class="img-fluid" alt="...">-->
    </div>
    <div class="carousel-item">
        <asp:Image CssClass="img-fluid" runat="server" ID="CarouselImg3" />
      <!--<img src="../../Images/steamDeck/Steam-Deck4K-Resolution.jpg" class="img-fluid" alt="...">-->
    </div>
      <div class="carousel-item" data-bs-interval="16000">
          <video autoplay muted controls class="ratio-16x9 container-fluid">
              <source  type="video/mp4" runat="server" id="SteamVid" />
          </video>
          <!--src="../Images/steamDeck/hero-banner-sequence-english.mp4"-->
    </div>
  </div>
  <button class="carousel-control-prev" type="button" data-bs-target="#carousel6" data-bs-slide="prev">
    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
    <span class="visually-hidden">Previous</span>
  </button>
  <button class="carousel-control-next" type="button" data-bs-target="#carousel6" data-bs-slide="next">
    <span class="carousel-control-next-icon" aria-hidden="true"></span>
    <span class="visually-hidden">Next</span>
  </button>
</div>
            <br />
            </td>
            <td style="vertical-align: middle;">
                <h3 style="text-align:center;color:whitesmoke" runat="server" id="Price2" ></h3>

                <button type="button" class="btn btn-secondary btn-sm active" data-bs-toggle="button"><h3 style="width:max-content">Add to cart</h3></button>
                <br />
                <br />
                <button type="button" class="btn btn-secondary btn-sm active" data-bs-toggle="button" ><h3 style="width:max-content;">Buy it now&nbsp;</h3></button>
            </td>

            </tr>

        </table>

     <table class="table container table-dark table-striped table-bordered">
        <tr>
            <th colspan="2" style="text-align:center"><h3>Specifications</h3></th>
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
        </div>
    </asp:Content>


