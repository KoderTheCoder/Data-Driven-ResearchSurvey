<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StaffSearch.aspx.cs" Inherits="DDA_Survey.StaffSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="heading">Staff Search</h2>
    <div class="row">
        <div class="col-sm-6">
            <h3 class="heading">Search Criteria</h3>
            <div class="row">
                <div class="col-sm-6 registerField">
                    <p class="registerText">First Name: </p>
                </div>
                <div class="col-sm-6">
                    <asp:TextBox ID="FirstNameTextBox" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6 registerField">
                    <p class="registerText">Last Name: </p>
                </div>
                <div class="col-sm-6">
                    <asp:TextBox ID="LastNameTextBox" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6 registerField">
                    <p class="registerText">Age Range: </p>
                </div>
                <div class="col-sm-6" style="border-top-style: solid; border-color: dimgrey;">
                    <asp:CheckBoxList ID="AgeRangeCheckBoxList" runat="server">
                        <asp:ListItem Text="18 - 24" Value="1" Selected="false"></asp:ListItem>
                        <asp:ListItem Text="25 - 30" Value="2" Selected="false"></asp:ListItem>
                        <asp:ListItem Text="30+" Value="3" Selected="false"></asp:ListItem>
                    </asp:CheckBoxList>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6 registerField">
                    <p class="registerText">State: </p>
                </div>
                <div class="col-sm-6" style="border-top-style: solid; border-color: dimgrey;">
                    <asp:CheckBoxList ID="StateCheckBoxList" runat="server">
                        <asp:ListItem Text="NSW" Value="1" Selected="false"></asp:ListItem>
                        <asp:ListItem Text="VIC" Value="2" Selected="false"></asp:ListItem>
                        <asp:ListItem Text="QLD" Value="3" Selected="false"></asp:ListItem>
                        <asp:ListItem Text="NT" Value="4" Selected="false"></asp:ListItem>
                        <asp:ListItem Text="WA" Value="5" Selected="false"></asp:ListItem>
                        <asp:ListItem Text="TAS" Value="6" Selected="false"></asp:ListItem>
                        <asp:ListItem Text="SA" Value="7" Selected="false"></asp:ListItem>
                    </asp:CheckBoxList>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6 registerField">
                    <p class="registerText">State: </p>
                </div>
                <div class="col-sm-6"  style="border-top-style: solid; border-color: dimgrey;">
                    <asp:CheckBoxList ID="GenderCheckBoxList" runat="server">
                        <asp:ListItem Text="Male" Value="1" Selected="false"></asp:ListItem>
                        <asp:ListItem Text="Female" Value="2" Selected="false"></asp:ListItem>
                    </asp:CheckBoxList>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6 registerField">
                    <p class="registerText">Suburb: </p>
                </div>
                <div class="col-sm-6">
                    <asp:TextBox ID="SuburbTextBox" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6 registerField">
                    <p class="registerText">Post Code: </p>
                </div>
                <div class="col-sm-6">
                    <asp:TextBox ID="PostCodeTextBox" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6 registerField" >
                    <p class="registerText">Email: </p>
                </div>
                <div class="col-sm-6">
                    <asp:TextBox ID="EmailTextBox" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6 registerField">
                    <p class="registerText">Banks Used: </p>
                </div>
                <div class="col-sm-6" style="border-top-style: solid; border-color: dimgrey;">
                    <asp:CheckBoxList ID="BanksCheckBoxList" runat="server">
                        <asp:ListItem Text="Commonwealth" Value="1" Selected="false"></asp:ListItem>
                        <asp:ListItem Text="Westpac" Value="2" Selected="false"></asp:ListItem>
                        <asp:ListItem Text="NAB" Value="3" Selected="false"></asp:ListItem>
                        <asp:ListItem Text="ANZ" Value="4" Selected="false"></asp:ListItem>
                    </asp:CheckBoxList>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6 registerField">
                    <p class="registerText">Banks Used: </p>
                </div>
                <div class="col-sm-6" style="border-top-style: solid; border-color: dimgrey;">
                    <asp:CheckBoxList ID="BankServicesCheckBoxList" runat="server">
                        <asp:ListItem Text="Commonwealth" Value="1" Selected="false"></asp:ListItem>
                        <asp:ListItem Text="Westpac" Value="2" Selected="false"></asp:ListItem>
                        <asp:ListItem Text="NAB" Value="3" Selected="false"></asp:ListItem>
                        <asp:ListItem Text="ANZ" Value="4" Selected="false"></asp:ListItem>
                    </asp:CheckBoxList>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6 registerField">
                    <p class="registerText">Newspapers Read: </p>
                </div>
                <div class="col-sm-6" style="border-top-style: solid; border-color: dimgrey;">
                    <asp:CheckBoxList ID="NewspaperCheckBoxList" runat="server">
                        <asp:ListItem Text="The Daily Telegraph" Value="1" Selected="false"></asp:ListItem>
                        <asp:ListItem Text="Sydney Morning Herlad" Value="2" Selected="false"></asp:ListItem>
                        <asp:ListItem Text="The Daily Mail" Value="3" Selected="false"></asp:ListItem>
                        <asp:ListItem Text="Liverpool Champion" Value="4" Selected="false"></asp:ListItem>
                    </asp:CheckBoxList>
                </div>
            </div>
        </div>
        <div class="col-sm-6">
            <h3 class="heading">Search Results</h3>
            <asp:GridView ID="SearchResultsGridView" runat="server">
                
            </asp:GridView>
        </div>
    </div>
</asp:Content>
