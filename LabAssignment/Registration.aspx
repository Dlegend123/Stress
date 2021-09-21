<%@ Page Title="Registration" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="LabAssignment.Registration" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid" >
    <br />
    <br />
        <asp:Table runat="server" HorizontalAlign="Center" ID="RegistrationTable" CssClass="container-fluid">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center">
    <table runat="server" class="table table-dark table-striped table-bordered" style="max-width:50vw;max-height:60vh;">
       <thead>
           <tr>
               <th style="text-align:center">
           <h3>
        Registration
    </h3>
               </th>
       </tr>
       </thead>
        <tr>
            <td style="text-align:left">
                <h5>
                    Name
                </h5>

                <asp:TextBox runat="server" Font-Size="Medium" CssClass="w-100" ID="Name">

                </asp:TextBox>
                </td>
            </tr>
        <tr>
            <td style="text-align:left">
                <h5>
                    Password
                </h5>

                <asp:TextBox runat="server" Font-Size="Medium" CssClass="w-100" ID="Password" TextMode="Password" OnTextChanged="Password_TextChanged">

                </asp:TextBox>
                <br />
                <asp:TextBox runat="server" ForeColor="Red" Font-Bold="true" ID="PasswordNotValid" Visible="false">

                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
            <h5>
                    Confirm Password
            </h5>

                <asp:TextBox runat="server" Font-Size="Medium" CssClass="w-100" ID="CPassword" CausesValidation="True"  TextMode="Password">

                </asp:TextBox>
                <br />
                <asp:CompareValidator ControlToValidate="CPassword" ControlToCompare="Password" Visible="true" runat="server" ForeColor="Red" Font-Bold="true" ErrorMessage="The passwords do not match" Operator="Equal" ID="PasswordCheck" ValueToCompare="Text" >
                    
                </asp:CompareValidator>
                
                </td>
        </tr>
        <tr>
            <td style="text-align:center">
                <asp:Button runat="server" Text="Go" CssClass="btn btn-outline-warning" ID="RegisterClick" OnClick="RegisterClick_Click"/>
                </td>
            
        </tr>
    </table>
                    </asp:TableCell>
            </asp:TableRow>
            
            </asp:Table>
    <br />
  
    <br />

        </div>
</asp:Content>
