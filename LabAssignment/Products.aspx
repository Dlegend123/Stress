<%@ Page Title="Products" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="LabAssignment.Products" EnableEventValidation="false" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div runat="server" id="ProductContain" class="h-100" style="margin-left:auto;margin-right:auto;"> 
    <br />                   
    <asp:table ID="ProductTable" runat="server" CssClass="table table-dark table-striped table-bordered h-100 container" BorderStyle="Solid" ForeColor="WhiteSmoke"  BorderWidth="3px" >
       <asp:TableRow>
       </asp:TableRow>
        <asp:TableHeaderRow BorderStyle="Solid" BorderWidth="3px">
           <asp:TableCell HorizontalAlign="Center" BorderStyle="Solid" ColumnSpan="2" >
               <asp:TextBox runat="server" ID="SearchInput"></asp:TextBox>
               &nbsp;
               <asp:Button CssClass="btn btn-outline-warning" runat="server" Text="Search" ID="SearchSubmit" OnClick="SearchSubmit_Click" />

           </asp:TableCell>
       </asp:TableHeaderRow>
        <asp:TableRow BorderStyle="Solid" BorderWidth="3px">
            <asp:TableCell ></asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label1" runat="server" Text="Sort By"></asp:Label>
                &nbsp;
                <asp:DropDownList runat="server" ID="SortBy" AutoPostBack="true" OnSelectedIndexChanged="SortBy_SelectedIndexChanged" >
                    
                    <asp:ListItem Text="Name">

                    </asp:ListItem>
                    <asp:ListItem Text="New Arrivals">

                    </asp:ListItem>
                    <asp:ListItem Text="Price">

                    </asp:ListItem>
                </asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>

            <asp:TableCell BorderStyle="Solid" BorderWidth="3px" CssClass="w-75">
                <asp:Table runat="server" ID="stressTable" CssClass="table h-100" BorderStyle="Solid" ForeColor="WhiteSmoke"  BorderWidth="3px" >

                </asp:Table>
            </asp:TableCell>
            
            <asp:TableCell ColumnSpan="1" RowSpan="0" BorderStyle="Solid" Height="100%" runat="server" ID="ProductFilter">
                <div style="text-align:left">

                    <asp:Label runat="server"><h4>Category</h4></asp:Label>
                <asp:DropDownList runat="server" ID="DropDownCategory" AutoPostBack="true" OnSelectedIndexChanged="Categorize">
                    <asp:ListItem Text="All">

                    </asp:ListItem>
                    <asp:ListItem Text="Handheld">

                    </asp:ListItem>
                    <asp:ListItem Text="Home">

                    </asp:ListItem>
                    <asp:ListItem Text="Hybrid"></asp:ListItem>
                </asp:DropDownList>
                </div>
                 <!--
                <div style="text-align:left">
                    <br />
                   
                <asp:Label runat="server"><h4>Brand</h4></asp:Label>
                <asp:CheckBoxList runat="server">

                    <asp:ListItem Text="Microsoft">

                    </asp:ListItem>

                    <asp:ListItem Text="Nintendo">
                        
                    </asp:ListItem>

                    <asp:ListItem Text="Sony">

                    </asp:ListItem>
                    <asp:ListItem Text="Valve">

                    </asp:ListItem>

                </asp:CheckBoxList>
                </div>
                -->
                
                <div style="text-align:left">
                    <br />
                    <asp:Label runat="server">
                    <h4>Price</h4>

                </asp:Label>
                <asp:TextBox runat="server" CssClass="w-25" ID="MinPrice" Text=""></asp:TextBox>
                &nbsp; - &nbsp;
                <asp:TextBox runat="server" CssClass="w-25" ID="MaxPrice" Text=""></asp:TextBox>
                    &nbsp;
                    <div runat="server" id="BeforeGo" visible="false">
                        
                    </div>
                <asp:Button runat="server" Text="Go" ID="PriceRange" OnClick="PriceRange_Click" CssClass="btn btn-outline-warning" />
                </div>
                
                 <!--
                <div style="text-align:left">
                    <br />
                    <asp:Label runat="server">
                    <h4>Condition</h4>
                </asp:Label>

                <asp:CheckBoxList runat="server">
                  <asp:ListItem Text="New">

                    </asp:ListItem>

                    <asp:ListItem Text="Used">
                        
                    </asp:ListItem>
                </asp:CheckBoxList>
                </div>
                     -->
            </asp:TableCell>

        </asp:TableRow>
        
   </asp:table>
        </div>   
</asp:Content>
