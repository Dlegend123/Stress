<%@ Page Title="Xbox Series S" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="XboxS.aspx.cs" Inherits="LabAssignment.XboxS" EnableEventValidation="false"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
        <asp:table class="table container table-dark table-striped table-bordered" runat="server" ID="Table1">
            <asp:TableRow>
                <asp:TableCell>
            
         <table class="table container-fluid" style="margin-left:auto;margin-right:auto;">
            <tr>
            <th class="text-center"><h1 style="color:whitesmoke">Xbox Series S</h1></th>
        </tr>
        </table>
    <table class="table container-fluid">
        <tr>
        <td style="vertical-align:central;width:60vw" >
<div id="carousel1" class="carousel carousel-dark slide container-fluid" data-bs-ride="carousel" >
  <div class="carousel-indicators">
    <button type="button" data-bs-target="#carousel1" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
    <button type="button" data-bs-target="#carousel1" data-bs-slide-to="1" aria-label="Slide 2"></button>
    <button type="button" data-bs-target="#carousel1" data-bs-slide-to="2" aria-label="Slide 3"></button>
  </div>
  <div class="carousel-inner">  
    <div class="carousel-item active">
        <img class="img-fluid"  runat="server" id="CarouselImg1" alt="..." />
      <!--<img src="../../Images/Xbox/seriesS/XboxOneSStand.jpg" class="img-fluid" alt="...">-->
    </div>
      <div class="carousel-item">
          <img class="img-fluid"  runat="server" id="CarouselImg2" alt="..."/>
      <!--<img src="../../Images/Xbox/seriesS/ot8wmyVSd8hoDgJsUK9c9a.jpg" class="img-fluid" alt="...">-->
    </div>
      <div class="carousel-item">
          <img class="img-fluid"  runat="server" id="CarouselImg3" alt="..."/>
      <!--<img src="../../Images/Xbox/seriesS/still-image-xbox-series-s-5-horizontal-view-console-controller-1604250614.png" class="img-fluid" alt="...">-->
    </div>
  </div>
  <button class="carousel-control-prev" type="button" data-bs-target="#carousel1" data-bs-slide="prev">
    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
    <span class="visually-hidden">Previous</span>
  </button>
  <button class="carousel-control-next" type="button" data-bs-target="#carousel1" data-bs-slide="next">
    <span class="carousel-control-next-icon" aria-hidden="true"></span>
    <span class="visually-hidden">Next</span>
  </button>
</div>

            </td>
            <td style="vertical-align: middle;text-align:center">
                <h3 style="text-align:center;color:whitesmoke" runat="server" id="Price2" ></h3>
                <button type="button" class="btn btn-secondary btn-sm active" data-bs-toggle="button"><h3 style="width:max-content">Add to cart</h3></button>
                <br />
                <br />
                <button type="button"class="btn btn-secondary btn-sm active" data-bs-toggle="button" ><h3 style="width:max-content;">Buy it now&nbsp;</h3></button>
            </td>


            </tr>

        </table>
        <br />
    <div>
      <button type="button" id="SeriesXHomeLaunch" class="btn btn-secondary btn-sm" runat="server" onserverclick="SeriesXHomeLaunch_ServerClick"><h3>Series X</h3></button>
    <button type="button" class="btn btn-secondary btn-sm  active disabled"  data-bs-toggle="button" aria-pressed="true" aria-disabled="true"><h3>Series S</h3></button>

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



