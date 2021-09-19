<%@ Page Title="Error Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ErrorPage1.aspx.cs" Inherits="LabAssignment.CustomErrors.ErrorPage1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="color:whitesmoke;vertical-align:central">
        <br />
        <br />
        <h1>Status Code: <%:Response.StatusCode %></h1>
        <h4>Description: <%:Response.StatusDescription %></h4>
        <h5><br />We are very sorry for the inconvenience caused to you... </h5>
    </div>
        

</asp:Content>
