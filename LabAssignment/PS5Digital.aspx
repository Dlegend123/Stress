﻿<%@ Page Title="PS5 Digital" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PS5Digital.aspx.cs" Inherits="LabAssignment.PS5Digital" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <br />
        <br />
    <table>
        <tr>
        <td style="vertical-align:central;width:60vw" >
<div id="carousel4" class="carousel carousel-dark slide container-fluid" data-bs-ride="carousel"  >
  <div class="carousel-indicators">
    <button type="button" data-bs-target="#carousel4" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
    <button type="button" data-bs-target="#carousel4" data-bs-slide-to="1" aria-label="Slide 2"></button>
  </div>
  <div class="carousel-inner">  
    <div class="carousel-item active">
        <img class="img-fluid"  runat="server" id="CarouselImg1" alt="..." />
      <!--<img src="Images/Playstation5/NoDisc/Ps5-Digital-Edition-Package-Go-Go-Deals-.jpg" class="img-fluid" alt="...">-->
    </div>
      <div class="carousel-item">
          <img class="img-fluid"  runat="server" id="CarouselImg2" alt="..."/>
      <!--<img src="Images/Playstation5/NoDisc/d4ad0557-5714-4ec8-9373-dc60e4a52789.__CR0,0,1464,600_PT0_SX1464_V1___.jpg" class="img-fluid" alt="...">-->
    </div>
  </div>
  <button class="carousel-control-prev" type="button" data-bs-target="#carousel4" data-bs-slide="prev">
    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
    <span class="visually-hidden">Previous</span>
  </button>
  <button class="carousel-control-next" type="button" data-bs-target="#carousel4" data-bs-slide="next">
    <span class="carousel-control-next-icon" aria-hidden="true"></span>
    <span class="visually-hidden">Next</span>
  </button>
</div>

            </td>
            <td style="vertical-align: middle;padding-left:2px">
                <h3 style="text-align:center;color:whitesmoke" runat="server" id="Price2" ></h3>

                <button type="button" class="btn btn-secondary btn-sm active" data-bs-toggle="button"><h3 style="width:max-content">Add to cart</h3></button>
                <br />
                <br />
                <button type="button" class="btn btn-secondary btn-sm active" data-bs-toggle="button" ><h3 style="width:max-content;">Buy it now&nbsp;</h3></button>
            </td>


            </tr>
            
        </table>
        <br />
    <div>
      <button type="button" class="btn btn-secondary btn-sm" id="PS5HomeLaunch" runat="server" onserverclick="PS5HomeLaunch_ServerClick"><h3>Disc</h3></button>
      <button type="button" class="btn btn-secondary btn-sm active disabled" data-bs-toggle="button" aria-pressed="true" aria-disabled="true"><h3>Digital</h3></button>

        </div>
    <br />
    <asp:table class="table container table-dark table-striped table-bordered" runat="server" ID="Description">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell ColumnSpan="2" HorizontalAlign="Center">
                <h3>Description</h3>
            </asp:TableHeaderCell>
        </asp:TableHeaderRow>

        
        
     </asp:table>
        </div>
    </asp:Content>

