﻿<%@ Page Title="Error Page 5" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error Page5.aspx.cs" Inherits="LabAssignment.CustomErrors.Error_Page5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
    <div style="color:whitesmoke;text-align:left">
        <br />
        <br />
        <h1>Status : 500 Internal Server Error</h1>
        <h2>Description: The server has encountered a situation it does not know how to handle.</h2>
        <h4 id="ErrorSource" runat="server"></h4>
        <h5 id="InnerEx" runat="server"></h5>
        <h6 id="StackTrace" runat="server"></h6>
        <p runat="server" id="ErrorMessage"><br /> We are very sorry for the inconvenience caused to you... </p>
        
    </div>
        </div>
</asp:Content>
