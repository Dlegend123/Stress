<%@ Page Title="PS5" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PS5.aspx.cs" Inherits="LabAssignment.PS5" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
        <asp:table class="table container table-dark table-striped table-bordered" runat="server" ID="Table1">
            <asp:TableRow>
                <asp:TableCell>
         <table class="table container-fluid" style="margin-left:auto;margin-right:auto;">
            <tr>
            <th class="text-center"><h1 style="color:whitesmoke">Playstation 5</h1></th>
        </tr>
        </table>
    <table class="table container-fluid">
        <tr>
        <td style="vertical-align:central;width:60vw" >
<div id="carousel3" class="carousel carousel-dark slide container-fluid" data-bs-ride="carousel" >
  <div class="carousel-indicators">
    <button type="button" data-bs-target="#carousel3" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
    <button type="button" data-bs-target="#carousel3" data-bs-slide-to="1" aria-label="Slide 2"></button>
    <button type="button" data-bs-target="#carousel3" data-bs-slide-to="2" aria-label="Slide 3"></button>
  </div>
  <div class="carousel-inner">  
    <div class="carousel-item active">
        <img class="img-fluid"  runat="server" id="CarouselImg1" alt="..." />
     <!-- <img src="../../Images/Playstation5/Disc/xWZMNYm.jpg" class="img-fluid" alt="...">-->
    </div>
      <div class="carousel-item">
          <img class="img-fluid"  runat="server" id="CarouselImg2" alt="..."/>
        
    <!--  <img src="Images/Playstation5/Disc/PS5Unboxing.jpg" class="img-fluid" alt="...">-->
    </div>
      <div class="carousel-item">
          <img class="img-fluid"  runat="server" id="CarouselImg3" alt="..."/>
        
      <!--<img src="Images/Playstation5/Disc/playstation-5-kutusu-canli-olarak-goruntulendi-technopat-oyun-haber.jpg" class="img-fluid" alt="...">-->
    </div>
  </div>
  <button class="carousel-control-prev" type="button" data-bs-target="#carousel3" data-bs-slide="prev">
    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
    <span class="visually-hidden">Previous</span>
  </button>
  <button class="carousel-control-next" type="button" data-bs-target="#carousel3" data-bs-slide="next">
    <span class="carousel-control-next-icon" aria-hidden="true"></span>
    <span class="visually-hidden">Next</span>
  </button>
</div>

            </td>
            <td style="vertical-align: middle;text-align:center">
                <h3 style="text-align:center;color:whitesmoke" runat="server" id="Price2" ></h3>
                <asp:DropDownList runat="server" ID="QuantityList" CssClass="btn btn-warning dropdown-toggle"></asp:DropDownList><br /><br />
                <div id="HideCart" runat="server" visible="false">
                <button type="button" class="btn btn-secondary btn-sm active" data-bs-toggle="button" runat="server" id="AddToCart" onserverclick="AddToCart_ServerClick"><h3 style="width:max-content">Add to cart</h3></button>
                <br />
                <br />
                </div>
                <button type="button"  class="btn btn-secondary btn-sm active" data-bs-toggle="button" runat="server" id="BuyNow" onserverclick="BuyNow_ServerClick" ><h3 style="width:max-content;">Buy it now&nbsp;</h3></button>
            </td>


            </tr>
            
        </table>
        <br />
    <div>
         <button type="button" class="btn btn-secondary btn-sm active disabled" data-bs-toggle="button" aria-pressed="true" aria-disabled="true"><h3>Disc</h3></button>
         <button type="button" class="btn btn-secondary btn-sm" id="PS5DigitalHomeLaunch" runat="server" onserverclick="PS5DigitalHomeLaunch_ServerClick"><h3>Digital</h3></button>

              
        </div>
    <br />
     <asp:table class="table container table-dark table-striped table-bordered" runat="server" ID="Description">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell>
                <h3 style="text-align:center">Description</h3>
            </asp:TableHeaderCell>
        </asp:TableHeaderRow>

        
        
     </asp:table>
        </asp:TableCell>
                </asp:TableRow>
            </asp:table>
    </asp:Content>

