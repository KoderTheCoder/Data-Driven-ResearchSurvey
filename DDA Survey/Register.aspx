<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="DDA_Survey.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="registerForm">
        <div>
        <h1 style="text-align:center;">Register</h1>
        <h3 style="text-align:center;">(Optional)</h3>
    <div class="row">
        <div class="col-sm-6 registerField">
            <p class="registerText">Given Name: </p>
        </div>
        <div class="col-sm-6">
            <asp:TextBox ID="FirstNameTextBox" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-6 registerField">
            <p class="registerText">Last Name: </p>
        </div>
        <div class="col-sm-6 registerField">
            <asp:TextBox ID="LastNameTextBox" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-6 registerField">
            <p class="registerText">Phone Number: </p>
        </div>
        <div class="col-sm-6 registerField">
            <asp:TextBox ID="PhoneNumberTextBox" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-6 registerField">
            <p class="registerText">Date of Birth: </p>
        </div>
        <div class="col-sm-6">
            <asp:TextBox ID="DOBTextBox" runat="server"></asp:TextBox>
        </div>
    </div>
    <div style="display:flex; align-items: center; justify-content: center;">
            <div class="row">
                <div class="col-sm-6" style="display:flex; align-items: center; justify-content: center;">
                    <asp:Button class="btn-lg btn-primary" ID="NextButton" runat="server" Text="Next" onclick="Next_Click" />
                </div>
                <div class="col-sm-6 questionButton" style="display:flex; align-items: center; justify-content: center;">
                    <asp:Button class="btn-lg btn-info" ID="SkipButton" runat="server" Text="Skip" onclick="Skip_Click" />
                </div>
            </div>
        </div>
        </div>
        
    </div>
</asp:Content>
