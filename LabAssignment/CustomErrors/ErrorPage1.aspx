<%@ Page Title="Error Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ErrorPage1.aspx.cs" Inherits="LabAssignment.CustomErrors.ErrorPage1" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="color:whitesmoke;vertical-align:central">
        <br />
        <br />
        <h1>Status Code: <%:Response.StatusCode %></h1>
        <h2>Description: <%:Response.StatusDescription %></h2>
        <h4 id="ErrorSource" runat="server"></h4>
        <h5 id="InnerEx" runat="server"></h5>
        <h6 id="StackTrace" runat="server"></h6>
        <p runat="server" id="ErrorMessage"><br /> We are very sorry for the inconvenience caused to you... </p>
        
    </div>
        

</asp:Content>
