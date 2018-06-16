<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Question.aspx.cs" Inherits="DDA_Survey.Question" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="registerForm">
        <div>
                <div class="row">
                    <div class="col-sm-12" style="text-align: center;">
                        <asp:Label style="font-size:32px;" ID="QuestionLabel" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12" style="display:flex; align-items: center; justify-content: center;">
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server">

                        </asp:PlaceHolder>
                    </div>
                </div>
                    <br />
            <div style="display:flex; align-items: center; justify-content: center;">
                <div class="row" style="width: 20%;">
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
