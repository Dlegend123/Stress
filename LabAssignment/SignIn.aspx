<%@ Page Title="Sign In" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="LabAssignment.SignIn" EnableEventValidation="false" %>
<%@ MasterType VirtualPath ="~/Site.Master" %>  
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="min-width:300px;height:stretch;">
    <br />
    <br />
        <asp:Table runat="server" CssClass="container" Width="25%">
            <asp:TableRow>
                <asp:TableCell>
    <asp:table runat="server" CssClass="table table-dark table-striped table-bordered" BorderStyle="Solid" ForeColor="WhiteSmoke"  BorderWidth="3px" >
       <asp:TableHeaderRow>
<asp:TableCell>
    <h3>
        Sign In
    </h3>
</asp:TableCell>
       </asp:TableHeaderRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Left">
                <h5>
                    Name
                </h5>

                <asp:TextBox runat="server" Font-Size="Medium" CssClass="w-100" ID="SName">

                </asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Left">
                <h5>
                    Password
                </h5>

                <asp:TextBox runat="server" Font-Size="Medium" CssClass="w-100" ID="SPassword">

                </asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center">
                <asp:Button runat="server" Text="Sign In" CssClass="btn btn-outline-warning" ID="SignInClick" OnClick="Validate"/>
                </asp:TableCell>
            
        </asp:TableRow>
    </asp:table>
                    </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ForeColor="WhiteSmoke" HorizontalAlign="Center">

                       
                    <hr style="border-top:3px solid #bbb;" class="w-25"/>
                    <h5>
                      New to LI online?
                    </h5>
                    <hr style="border-top:3px solid #bbb;" class="w-25"/>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button runat="server" Text="Create your LI account" CssClass="btn btn-outline-warning"/>
                </asp:TableCell>
            </asp:TableRow>
            </asp:Table>
    <br />
  
    <br />

        </div>
</asp:Content>
