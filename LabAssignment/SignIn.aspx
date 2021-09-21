<%@ Page Title="Sign In" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="LabAssignment.SignIn" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid" >
    <br />
    <br />
        <asp:Table runat="server" HorizontalAlign="Center" ID="SignInTable" CssClass="container-fluid">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center">
    <table runat="server" class="table table-dark table-striped table-bordered" style="max-width:50vw;max-height:60vh;">
       <thead>
           <tr>
               <th style="text-align:center">
           <h3>
        Sign In
    </h3>
               </th>
       </tr>
       </thead>
        <tr>
            <td style="text-align:left">
                <h5>
                    Name
                </h5>

                <asp:TextBox runat="server" Font-Size="Medium" CssClass="w-100" ID="SName">

                </asp:TextBox>
                </td>
            </tr>
        <tr>
            <td style="text-align:left">
                <h5>
                    Password
                </h5>

                <asp:TextBox runat="server" Font-Size="Medium" CssClass="w-100" ID="SPassword" TextMode="Password">

                </asp:TextBox>
                <br />
                <asp:TextBox runat="server" ForeColor="Red" Font-Bold="true" ID="PasswordNotValid" Visible="false" AutoPostBack="true" BackColor="Transparent" BorderStyle="None">

                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align:center">
                <asp:Button runat="server" Text="Sign In" CssClass="btn btn-outline-warning" ID="SignInClick" OnClick="Validate"/>
                </td>
            
        </tr>
    </table>
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
                    <asp:Button runat="server" Text="Create your LI account" CssClass="btn btn-outline-warning" PostBackUrl="~/Registration.aspx"/>
                </asp:TableCell>
            </asp:TableRow>
            </asp:Table>
    <br />
  
    <br />

        </div>
</asp:Content>
